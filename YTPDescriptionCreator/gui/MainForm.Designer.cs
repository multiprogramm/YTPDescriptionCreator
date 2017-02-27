namespace srcgen
{
    partial class OptionsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if( disposing && ( components != null ) )
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent( )
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( OptionsForm ) );
            this.cNeedLimits = new System.Windows.Forms.CheckBox();
            this.cSaveButton = new System.Windows.Forms.Button();
            this.cSetFirstSourceTime = new System.Windows.Forms.CheckBox();
            this.cListPrintScheme = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cLabelBegin = new System.Windows.Forms.Label();
            this.cLabelEnd = new System.Windows.Forms.Label();
            this.cEndTime = new srcgen.SpanControl();
            this.cBeginTime = new srcgen.SpanControl();
            this.cFirstSource = new srcgen.SpanControl();
            this.cVkLink = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // cNeedLimits
            // 
            this.cNeedLimits.AutoSize = true;
            this.cNeedLimits.Location = new System.Drawing.Point( 12, 42 );
            this.cNeedLimits.Name = "cNeedLimits";
            this.cNeedLimits.Size = new System.Drawing.Size( 322, 17 );
            this.cNeedLimits.TabIndex = 10;
            this.cNeedLimits.Text = "Задать границы проекта, внутри которых нужно описание";
            this.cNeedLimits.UseVisualStyleBackColor = true;
            this.cNeedLimits.CheckedChanged += new System.EventHandler( this.cCBByAllProject_CheckedChanged );
            // 
            // cSaveButton
            // 
            this.cSaveButton.Location = new System.Drawing.Point( 235, 124 );
            this.cSaveButton.Name = "cSaveButton";
            this.cSaveButton.Size = new System.Drawing.Size( 111, 33 );
            this.cSaveButton.TabIndex = 18;
            this.cSaveButton.Text = "Сохранить в файл";
            this.cSaveButton.UseVisualStyleBackColor = true;
            this.cSaveButton.Click += new System.EventHandler( this.cSaveButton_Click );
            // 
            // cSetFirstSourceTime
            // 
            this.cSetFirstSourceTime.AutoSize = true;
            this.cSetFirstSourceTime.Location = new System.Drawing.Point( 12, 86 );
            this.cSetFirstSourceTime.Name = "cSetFirstSourceTime";
            this.cSetFirstSourceTime.Size = new System.Drawing.Size( 215, 17 );
            this.cSetFirstSourceTime.TabIndex = 19;
            this.cSetFirstSourceTime.Text = "Задать время отсчёта первого сурса";
            this.cSetFirstSourceTime.UseVisualStyleBackColor = true;
            this.cSetFirstSourceTime.CheckedChanged += new System.EventHandler( this.cSetFirstSourceTime_CheckedChanged );
            // 
            // cListPrintScheme
            // 
            this.cListPrintScheme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cListPrintScheme.FormattingEnabled = true;
            this.cListPrintScheme.Location = new System.Drawing.Point( 99, 6 );
            this.cListPrintScheme.Name = "cListPrintScheme";
            this.cListPrintScheme.Size = new System.Drawing.Size( 247, 21 );
            this.cListPrintScheme.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point( 9, 9 );
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size( 84, 13 );
            this.label3.TabIndex = 23;
            this.label3.Text = "Вид сурслиста:";
            // 
            // cLabelBegin
            // 
            this.cLabelBegin.AutoSize = true;
            this.cLabelBegin.Location = new System.Drawing.Point( 27, 64 );
            this.cLabelBegin.Name = "cLabelBegin";
            this.cLabelBegin.Size = new System.Drawing.Size( 20, 13 );
            this.cLabelBegin.TabIndex = 28;
            this.cLabelBegin.Text = "От";
            // 
            // cLabelEnd
            // 
            this.cLabelEnd.AutoSize = true;
            this.cLabelEnd.Location = new System.Drawing.Point( 106, 64 );
            this.cLabelEnd.Name = "cLabelEnd";
            this.cLabelEnd.Size = new System.Drawing.Size( 19, 13 );
            this.cLabelEnd.TabIndex = 29;
            this.cLabelEnd.Text = "до";
            // 
            // cEndTime
            // 
            this.cEndTime.AutoSize = true;
            this.cEndTime.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.cEndTime.Location = new System.Drawing.Point( 126, 61 );
            this.cEndTime.Name = "cEndTime";
            this.cEndTime.Size = new System.Drawing.Size( 50, 20 );
            this.cEndTime.TabIndex = 31;
            // 
            // cBeginTime
            // 
            this.cBeginTime.AutoSize = true;
            this.cBeginTime.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.cBeginTime.Location = new System.Drawing.Point( 48, 61 );
            this.cBeginTime.Name = "cBeginTime";
            this.cBeginTime.Size = new System.Drawing.Size( 50, 20 );
            this.cBeginTime.TabIndex = 30;
            // 
            // cFirstSource
            // 
            this.cFirstSource.AutoSize = true;
            this.cFirstSource.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.cFirstSource.Location = new System.Drawing.Point( 227, 85 );
            this.cFirstSource.Name = "cFirstSource";
            this.cFirstSource.Size = new System.Drawing.Size( 50, 20 );
            this.cFirstSource.TabIndex = 24;
            // 
            // cVkLink
            // 
            this.cVkLink.AutoSize = true;
            this.cVkLink.Location = new System.Drawing.Point( 12, 144 );
            this.cVkLink.Name = "cVkLink";
            this.cVkLink.Size = new System.Drawing.Size( 66, 13 );
            this.cVkLink.TabIndex = 32;
            this.cVkLink.TabStop = true;
            this.cVkLink.Text = "Группа в вк";
            this.cVkLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler( this.cVkLink_LinkClicked );
            // 
            // OptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size( 358, 169 );
            this.Controls.Add( this.cVkLink );
            this.Controls.Add( this.cEndTime );
            this.Controls.Add( this.cBeginTime );
            this.Controls.Add( this.cLabelBegin );
            this.Controls.Add( this.cLabelEnd );
            this.Controls.Add( this.cFirstSource );
            this.Controls.Add( this.cListPrintScheme );
            this.Controls.Add( this.label3 );
            this.Controls.Add( this.cSetFirstSourceTime );
            this.Controls.Add( this.cSaveButton );
            this.Controls.Add( this.cNeedLimits );
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ( (System.Drawing.Icon)( resources.GetObject( "$this.Icon" ) ) );
            this.Name = "OptionsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "[title]";
            this.ResumeLayout( false );
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cNeedLimits;
        private System.Windows.Forms.Button cSaveButton;
        private System.Windows.Forms.CheckBox cSetFirstSourceTime;
        private System.Windows.Forms.ComboBox cListPrintScheme;
        private System.Windows.Forms.Label label3;
        private SpanControl cFirstSource;
        private SpanControl cEndTime;
        private SpanControl cBeginTime;
        private System.Windows.Forms.Label cLabelBegin;
        private System.Windows.Forms.Label cLabelEnd;
        private System.Windows.Forms.LinkLabel cVkLink;
    }
}