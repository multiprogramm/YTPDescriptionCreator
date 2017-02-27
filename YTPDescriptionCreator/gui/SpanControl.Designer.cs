namespace srcgen
{
    partial class SpanControl
    {
        /// <summary> 
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose( bool disposing )
        {
            if( disposing && ( components != null ) )
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Обязательный метод для поддержки конструктора - не изменяйте 
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent( )
        {
            this.cSpanTextBox = new System.Windows.Forms.MaskedTextBox();
            this.SuspendLayout();
            // 
            // cSpanTextBox
            // 
            this.cSpanTextBox.Location = new System.Drawing.Point( 0, 0 );
            this.cSpanTextBox.Margin = new System.Windows.Forms.Padding( 0 );
            this.cSpanTextBox.Mask = "00:00:00";
            this.cSpanTextBox.Name = "cSpanTextBox";
            this.cSpanTextBox.PromptChar = '0';
            this.cSpanTextBox.Size = new System.Drawing.Size( 50, 20 );
            this.cSpanTextBox.TabIndex = 0;
            this.cSpanTextBox.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludePromptAndLiterals;
            this.cSpanTextBox.TextChanged += new System.EventHandler( this.cSpanTextBox_TextChanged );
            // 
            // SpanControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add( this.cSpanTextBox );
            this.Name = "SpanControl";
            this.Size = new System.Drawing.Size( 50, 20 );
            this.ResumeLayout( false );
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MaskedTextBox cSpanTextBox;
    }
}
