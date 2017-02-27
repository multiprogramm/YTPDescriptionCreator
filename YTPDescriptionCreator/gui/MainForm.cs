using Sony.Vegas;
using System.Windows.Forms;
using System;
using srcgen;
using System.Collections.Generic;
using System.Reflection;
using System.Diagnostics;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace srcgen
{
    public partial class OptionsForm : Form
    {
        Options mPrintOptions = null;

        public OptionsForm( )
        {
            InitializeComponent();
            InitControls();
        }

        // TODO Добавить список расширений через запятую и выбор схемы экспорта

        private void InitControls()
        {
            this.Text = ThisProgram.GetTitle();

            int i_start_time = VegWrapper.StartTime();
            int i_end_time = VegWrapper.EndTime();

            cBeginTime.SetTimeSec( i_start_time );
            cEndTime.SetTimeSec( i_end_time );
            cFirstSource.SetTimeSec( 0 );

            bool is_sel_exist = ( i_start_time != i_end_time );
            cNeedLimits.Checked = is_sel_exist;
            SetIntervalsVisible( cNeedLimits.Checked );

            cSetFirstSourceTime.Checked = is_sel_exist && ( i_start_time != 0);
            SetFirstSourceTimeVisible( cSetFirstSourceTime.Checked );

            LoadOptions();
            SaveIconIfNeed();
            FillListPrintScheme();
        }

        void LoadOptions( )
        {
            string options_file = ThisProgram.GetOptionsFilePath();
            mPrintOptions = Options.Load( options_file );

            if( mPrintOptions == null
                || mPrintOptions.GetEnabledShemes().Count == 0 )
            {
                mPrintOptions = Options.GetStandart();

                try
                {
                    mPrintOptions.Save( options_file );
                }
                catch
                {
                    Dlg.Error( "Не удалось сохранить настройки по-умолчанию в файл '"
                        + options_file + "'." );
                }
            }
        }

        private void SaveIconIfNeed( )
        {
            string ico_path = ThisProgram.PngIcoPath();
            if( File.Exists( ico_path ) )
                return;

            try
            {
                Bitmap ico = this.Icon.ToBitmap();
                if( ico == null )
                    throw new NullReferenceException( "Icon is null." );

                if( ico.Height != 16 && ico.Width != 16 )
                {
                    Bitmap new_ico = new Bitmap( ico, new Size( 16, 16 ) );
                    ico.Dispose();
                    ico = new_ico;
                }

                ico.Save( ico_path, ImageFormat.Png );
            }
            catch( Exception ex )
            {
                Dlg.Error( "Не удалось сохранить значок в файл '"
                    + ico_path + "': " + ex.Message );
            }
        }

        void FillListPrintScheme()
        {
            if( mPrintOptions == null )
                return;
            cListPrintScheme.Items.Clear();

            var schemes = mPrintOptions.GetEnabledShemes();
            foreach( var scheme in schemes )
                cListPrintScheme.Items.Add( scheme.Name );
            cListPrintScheme.SelectedIndex = 0;
        }

        private void cCBByAllProject_CheckedChanged( object sender, System.EventArgs e )
        {
            SetIntervalsVisible( cNeedLimits.Checked );
        }
        private void cSetFirstSourceTime_CheckedChanged( object sender, EventArgs e )
        {
            SetFirstSourceTimeVisible( cSetFirstSourceTime.Checked );
        }

        private void SetIntervalsVisible( bool is_visible )
        {
            cLabelBegin.Visible = is_visible;
            cLabelEnd.Visible = is_visible;
            cBeginTime.Visible = is_visible;
            cEndTime.Visible = is_visible;
        }

        private void SetFirstSourceTimeVisible( bool is_visible )
        {
            cFirstSource.Visible = is_visible;
        }


        private void cSaveButton_Click( object sender, EventArgs e )
        {
            int sheme_num = cListPrintScheme.SelectedIndex;
            var schemes = mPrintOptions.GetEnabledShemes();
            Scheme scheme = schemes[sheme_num];

            int? i_start_time = null;
            int? i_end_time = null;
            if( cNeedLimits.Checked )
            {
                i_start_time = GetSpanControlTime( cBeginTime, "Некорректное время начало чтения." );
                if( i_start_time.Value < 0 )
                    return;

                i_end_time = GetSpanControlTime( cEndTime, "Некорректное время конца чтения." );
                if( i_end_time.Value < 0 )
                    return;

                if( i_start_time.Value > i_end_time.Value )
                {
                    var tmp = i_end_time;
                    i_end_time = i_start_time;
                    i_start_time = tmp;
                }
            }

            int? i_delta_time = null;
            if( cSetFirstSourceTime.Checked )
            {
                i_delta_time = cFirstSource.GetTimeSec();
                if( !i_delta_time.HasValue )
                {
                    cFirstSource.SelectTime();
                    Dlg.Error( "Некорректное время для смещения." );
                    return;
                }
            }

            //Здесь получение файлика для сохранения
            string file_ext = scheme.FileExtension;
            if( file_ext == null || file_ext == "" )
                file_ext = "txt";
            else
                file_ext = file_ext.ToLower().Replace( ".", "" );

            string default_file_name = VegWrapper.GetProjectFilePath();
            if( default_file_name != "" )
            {
                if( PathFuncs.UpperExtByPath( default_file_name ) == "VEG" )
                    default_file_name = default_file_name.Substring( 0, default_file_name.Length - 4 );
                default_file_name += " " + scheme.Name + "." + file_ext;
            }

            string filter = scheme.Name + " (*." + file_ext + ")|*." + file_ext;

            string file_name = Dlg.Save( default_file_name, filter );
            if( file_name == "" )
                return;

            var sources = VegWrapper.GetSourceList(
                scheme.Video.GetExtensions(),
                scheme.Audio.GetExtensions(),
                i_start_time, i_end_time, i_delta_time );

            SourcesWriter file_writer = new SourcesWriter();
            file_writer.WriteFile( file_name, sources, scheme );
            this.Close();
        }


        private int GetSpanControlTime( SpanControl span_control, string error_text )
        {
            const int ERROR_VALUE = -1;

            int? time_sec = span_control.GetTimeSec();
            if( !time_sec.HasValue )
            {
                span_control.SelectTime();
                Dlg.Error( error_text );
                return ERROR_VALUE;
            }

            return time_sec.Value;
        }

        private void cVkLink_LinkClicked( object sender, LinkLabelLinkClickedEventArgs e )
        {
            Process.Start( ThisProgram.GetVkPageLink() );
        }
    }
}
