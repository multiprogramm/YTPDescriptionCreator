using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace srcgen
{

public partial class SpanControl : UserControl
{
    public SpanControl( )
    {
        InitializeComponent();
        SetTimeSec( 0 );
    }

    public bool GetTime( out int hh, out int mm, out int ss )
    {
        hh = 0;
        mm = 0;
        ss = 0;

        string time = cSpanTextBox.Text;
        string s_hh = time.Substring( 0, 2 );
        string s_mm = time.Substring( 3, 2 );
        string s_ss = time.Substring( 6, 2 );

        if( !int.TryParse( s_hh, out hh ) )
            return SetIsValid( false );

        if( !int.TryParse( s_mm, out mm ) )
            return SetIsValid( false );
        else if( mm < 0 || mm > 59 )
            return SetIsValid( false );

        if( !int.TryParse( s_ss, out ss ) )
            return SetIsValid( false );
        else if( ss < 0 || ss > 59 )
            return SetIsValid( false );

        return SetIsValid( true );
    }

    public int? GetTimeSec()
    {
        int hh, mm, ss;
        if( !GetTime( out hh, out mm, out ss ) )
            return null;
        return hh * 3600 + mm * 60 + ss;
    }

    public bool SetTime( int hh, int mm, int ss )
    {
        bool is_valid = true;

        if( hh > 99 )
        {
            hh = 99;
            is_valid = false;
        }
        else if( hh < 0 )
        {
            hh = 0;
            is_valid = false;
        }

        if( mm > 59 )
        {
            mm = 59;
            is_valid = false;
        }
        else if( mm < 0 )
        {
            mm = 0;
            is_valid = false;
        }

        if( ss > 59 )
        {
            ss = 59;
            is_valid = false;
        }
        else if( ss < 0 )
        {
            ss = 0;
            is_valid = false;
        }

        cSpanTextBox.Text =
            hh.ToString( "00" ) + ":"
            + mm.ToString( "00" ) + ":"
            + ss.ToString( "00" );

        return is_valid;
    }

    public bool SetTimeSec( int ss )
    {
        bool is_valid = true;

        if( ss < 0 )
        {
            ss = 0;
            is_valid = false;
        }

        int hh = ss / 3600;
        ss %= 3600;

        int mm = ss / 60;
        ss %= 60;

        is_valid &= SetTime( hh, mm, ss );
        return is_valid;
    }

    public void SelectTime()
    {
        cSpanTextBox.Focus();
        cSpanTextBox.SelectAll();
    }

// private:
    private bool IsValidCall()
    {
        int hh, mm, ss;
        return GetTime( out hh, out mm, out ss );
    }

    private bool SetIsValid( bool is_valid )
    {
        cSpanTextBox.ForeColor = is_valid ? Color.Black : Color.Red;
        return is_valid;
    }

    private void cSpanTextBox_TextChanged( object sender, EventArgs e )
    {
        SetIsValid( IsValidCall() );
    }
}

} // namespace srcgen