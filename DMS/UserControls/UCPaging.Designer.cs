namespace FrontEnd.PagingControl
{
    partial class UCPaging
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
            this.btnBack = new Telerik.WinControls.UI.RadButton();
            this.btnForward = new Telerik.WinControls.UI.RadButton();
            this.drpCurrentPage = new Telerik.WinControls.UI.RadDropDownList();
            ((System.ComponentModel.ISupportInitialize)(this.btnBack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnForward)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.drpCurrentPage)).BeginInit();
            this.SuspendLayout();
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(3, 3);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(29, 24);
            this.btnBack.TabIndex = 105;
            this.btnBack.Text = "<";
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnForward
            // 
            this.btnForward.Location = new System.Drawing.Point(119, 3);
            this.btnForward.Name = "btnForward";
            this.btnForward.Size = new System.Drawing.Size(30, 24);
            this.btnForward.TabIndex = 106;
            this.btnForward.Text = ">";
            this.btnForward.Click += new System.EventHandler(this.btnForward_Click);
            // 
            // drpCurrentPage
            // 
            this.drpCurrentPage.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
            this.drpCurrentPage.Location = new System.Drawing.Point(38, 3);
            this.drpCurrentPage.Name = "drpCurrentPage";
            this.drpCurrentPage.ShowImageInEditorArea = true;
            this.drpCurrentPage.Size = new System.Drawing.Size(75, 25);
            this.drpCurrentPage.TabIndex = 107;
            this.drpCurrentPage.TextChanged += new System.EventHandler(this.drpCurrentPage_TextChanged);
            // 
            // UCPaging
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.drpCurrentPage);
            this.Controls.Add(this.btnForward);
            this.Controls.Add(this.btnBack);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "UCPaging";
            this.Size = new System.Drawing.Size(154, 35);
            this.Load += new System.EventHandler(this.UCPaging_Load);
            ((System.ComponentModel.ISupportInitialize)(this.btnBack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnForward)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.drpCurrentPage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadButton btnBack;
        private Telerik.WinControls.UI.RadButton btnForward;
        private Telerik.WinControls.UI.RadDropDownList drpCurrentPage;

    }
}
