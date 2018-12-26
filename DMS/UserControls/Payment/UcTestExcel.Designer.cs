namespace DMS.UserControls
{
    partial class UcTestExcel
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
            this.excelGrid = new unvell.ReoGrid.ReoGridControl();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // excelGrid
            // 
            this.excelGrid.BackColor = System.Drawing.Color.White;
            this.excelGrid.ColumnHeaderContextMenuStrip = null;
            this.excelGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.excelGrid.LeadHeaderContextMenuStrip = null;
            this.excelGrid.Location = new System.Drawing.Point(0, 63);
            this.excelGrid.Name = "excelGrid";
            this.excelGrid.RowHeaderContextMenuStrip = null;
            this.excelGrid.Script = null;
            this.excelGrid.SheetTabContextMenuStrip = null;
            this.excelGrid.SheetTabControlWidth = 60;
            this.excelGrid.Size = new System.Drawing.Size(779, 294);
            this.excelGrid.TabIndex = 0;
            this.excelGrid.Text = "Excel Sheet";
            // 
            // pnlHeader
            // 
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(779, 63);
            this.pnlHeader.TabIndex = 1;
            this.pnlHeader.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlHeader_Paint);
            // 
            // pnlFooter
            // 
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 357);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(779, 82);
            this.pnlFooter.TabIndex = 2;
            // 
            // UcTestExcel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.excelGrid);
            this.Controls.Add(this.pnlFooter);
            this.Controls.Add(this.pnlHeader);
            this.Name = "UcTestExcel";
            this.Size = new System.Drawing.Size(779, 439);
            this.Load += new System.EventHandler(this.UcPayment_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private unvell.ReoGrid.ReoGridControl excelGrid;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Panel pnlFooter;
    }
}
