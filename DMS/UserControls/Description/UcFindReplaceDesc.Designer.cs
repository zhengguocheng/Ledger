namespace DMS.UserControls
{
    partial class UcFindReplaceDesc
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtFind = new Telerik.WinControls.UI.RadTextBox();
            this.btnClear = new Telerik.WinControls.UI.RadButton();
            this.btnReplace = new Telerik.WinControls.UI.RadButton();
            this.label7 = new System.Windows.Forms.Label();
            this.txtReplaceWith = new Telerik.WinControls.UI.RadTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.txtFind)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnReplace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReplaceWith)).BeginInit();
            this.SuspendLayout();
            // 
            // txtFind
            // 
            this.txtFind.Location = new System.Drawing.Point(87, 38);
            this.txtFind.Name = "txtFind";
            this.txtFind.Size = new System.Drawing.Size(304, 20);
            this.txtFind.TabIndex = 98;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(159, 108);
            this.btnClear.Margin = new System.Windows.Forms.Padding(2);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(68, 21);
            this.btnClear.TabIndex = 96;
            this.btnClear.Text = "Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnReplace
            // 
            this.btnReplace.Location = new System.Drawing.Point(87, 108);
            this.btnReplace.Margin = new System.Windows.Forms.Padding(2);
            this.btnReplace.Name = "btnReplace";
            this.btnReplace.Size = new System.Drawing.Size(68, 21);
            this.btnReplace.TabIndex = 95;
            this.btnReplace.Text = "Replace";
            this.btnReplace.Click += new System.EventHandler(this.btnReplace_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(52, 42);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(30, 13);
            this.label7.TabIndex = 97;
            this.label7.Text = "Find:";
            // 
            // txtReplaceWith
            // 
            this.txtReplaceWith.Location = new System.Drawing.Point(87, 73);
            this.txtReplaceWith.Name = "txtReplaceWith";
            this.txtReplaceWith.Size = new System.Drawing.Size(304, 20);
            this.txtReplaceWith.TabIndex = 99;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(8, 77);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 100;
            this.label1.Text = "Replace With:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(8, 10);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(414, 13);
            this.label2.TabIndex = 101;
            this.label2.Text = "This operation will replace all the descriptions all Payment & Receipt sheets in " +
    "database.";
            // 
            // UcFindReplaceDesc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtReplaceWith);
            this.Controls.Add(this.txtFind);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnReplace);
            this.Controls.Add(this.label7);
            this.Name = "UcFindReplaceDesc";
            this.Size = new System.Drawing.Size(729, 322);
            ((System.ComponentModel.ISupportInitialize)(this.txtFind)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnReplace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReplaceWith)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadTextBox txtFind;
        private Telerik.WinControls.UI.RadButton btnClear;
        private Telerik.WinControls.UI.RadButton btnReplace;
        private System.Windows.Forms.Label label7;
        private Telerik.WinControls.UI.RadTextBox txtReplaceWith;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}
