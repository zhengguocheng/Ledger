using DAL;
using DMS.CustomClasses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Telerik.WinControls.UI;
using unvell.ReoGrid.Drawing;

namespace DMS.UserControls
{
    public class LedgerSheetBase : UserControlBase
    {
        public string TableName;
        public long DocumentItemID;
        public const string VATCodeColumnName = "VAT Code";
        LedgerGrid legGrid;

        public void CommonSettings(LedgerGrid _legGrid, RadButton btnSearch, RadButton btnClear, RadButton btnBack)
        {
            

            legGrid = _legGrid;
            btnSearch.MouseClick += btnSearch_MouseClick;
            btnBack.MouseClick += btnBack_MouseClick;
            btnClear.MouseClick += btnClear_MouseClick;
            this.Load += LedgerSheetBase_Load;
        }

        void LedgerSheetBase_Load(object sender, EventArgs e)
        {
            
        }

        void btnClear_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            legGrid.sRemoveBgColor();
        }

        void btnBack_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            LoadPreviousPage();
            //TryNavigateBack();
        }

        void btnSearch_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            legGrid.sSearchContent();
        }

        public DataTable GetData()
        {
            var dtSrc = LedgerGridData.GetDataTable(TableName, DocumentItemID);
            return dtSrc;
        }

    }

    public class LedgerSheetBaseTrialBal : UserControlBase
    {
        public string TableName;
        public long DocumentItemID;
        public const string VATCodeColumnName = "VAT Code";
        TrialGrid legGrid;

        public void CommonSettings(TrialGrid _legGrid, RadButton btnSearch, RadButton btnClear, RadButton btnBack)
        {
            legGrid = _legGrid;
            btnSearch.MouseClick += btnSearch_MouseClick;
            btnBack.MouseClick += btnBack_MouseClick;
            btnClear.MouseClick += btnClear_MouseClick;
            this.Load += LedgerSheetBase_Load;
        }

        void LedgerSheetBase_Load(object sender, EventArgs e)
        {

        }

        void btnClear_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            legGrid.sRemoveBgColor();
        }

        void btnBack_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            LoadPreviousPage();
        }

        void btnSearch_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            legGrid.sSearchContent();
        }

        public DataTable GetData()
        {
            var dtSrc = LedgerGridData.GetDataTable(TableName, DocumentItemID);
            return dtSrc;
        }

    }

    public class LedgerSheetBaseClosingTrialBal : UserControlBase
    {
        public string TableName;
        public long DocumentItemID;
        public const string VATCodeColumnName = "VAT Code";
        ClosingTrialGrid legGrid;

        public void CommonSettings(ClosingTrialGrid _legGrid, RadButton btnSearch, RadButton btnClear, RadButton btnBack)
        {
            legGrid = _legGrid;
            btnSearch.MouseClick += btnSearch_MouseClick;
            btnBack.MouseClick += btnBack_MouseClick;
            btnClear.MouseClick += btnClear_MouseClick;
            this.Load += LedgerSheetBase_Load;
        }

        void LedgerSheetBase_Load(object sender, EventArgs e)
        {

        }

        void btnClear_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            legGrid.sRemoveBgColor();
        }

        void btnBack_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            LoadPreviousPage();
        }

        void btnSearch_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            legGrid.sSearchContent();
        }

        public DataTable GetData()
        {
            var dtSrc = LedgerGridData.GetDataTable(TableName, DocumentItemID);
            return dtSrc;
        }
    }

    public class LedgerSheetBaseJournal : UserControlBase
    {
        public string TableName;
        public long DocumentItemID;
        JournalsGrid legGrid;

        public void CommonSettings(JournalsGrid _legGrid, RadButton btnSearch, RadButton btnClear, RadButton btnBack)
        {
            legGrid = _legGrid;
            btnSearch.MouseClick += btnSearch_MouseClick;
            btnBack.MouseClick += btnBack_MouseClick;
            btnClear.MouseClick += btnClear_MouseClick;
            this.Load += LedgerSheetBase_Load;
        }

        void LedgerSheetBase_Load(object sender, EventArgs e)
        {

        }

        void btnClear_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            legGrid.sRemoveBgColor();
        }

        void btnBack_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            LoadPreviousPage();
        }

        void btnSearch_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            legGrid.sSearchContent();
        }

        public DataTable GetData()
        {
            var dtSrc = LedgerGridData.GetDataTable(TableName, DocumentItemID);
            return dtSrc;
        }

    }

    public class LedgerSheetBaseMultipleEntJournal : UserControlBase
    {
        public string TableName;
        public long DocumentItemID;
        MultipleEntJournalsGrid legGrid;

        public void CommonSettings(MultipleEntJournalsGrid _legGrid, RadButton btnSearch, RadButton btnClear, RadButton btnBack)
        {
            legGrid = _legGrid;
            btnSearch.MouseClick += btnSearch_MouseClick;
            btnBack.MouseClick += btnBack_MouseClick;
            btnClear.MouseClick += btnClear_MouseClick;
            this.Load += LedgerSheetBase_Load;
        }

        void LedgerSheetBase_Load(object sender, EventArgs e)
        {

        }

        void btnClear_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            legGrid.sRemoveBgColor();
        }

        void btnBack_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            LoadPreviousPage();
        }

        void btnSearch_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            legGrid.sSearchContent();
        }

        public DataTable GetData()
        {
            var dtSrc = LedgerGridData.GetDataTable(TableName, DocumentItemID);
            return dtSrc;
        }

    }

    public class LedgerSheetBase3 : UserControlBase
    {
        public string TableName;
        public long DocumentItemID;
        public const string VATCodeColumnName = "VAT Code";
        SDBGrid legGrid;

        public void CommonSettings(SDBGrid _legGrid, RadButton btnSearch, RadButton btnClear, RadButton btnBack)
        {


            legGrid = _legGrid;
            btnSearch.MouseClick += btnSearch_MouseClick;
            btnBack.MouseClick += btnBack_MouseClick;
            btnClear.MouseClick += btnClear_MouseClick;
            this.Load += LedgerSheetBase_Load;
        }

        void LedgerSheetBase_Load(object sender, EventArgs e)
        {

        }

        void btnClear_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            legGrid.sRemoveBgColor();
        }

        void btnBack_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            LoadPreviousPage();
        }

        void btnSearch_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            legGrid.sSearchContent();
        }

        public DataTable GetData()
        {
            var dtSrc = LedgerGridData.GetDataTable(TableName, DocumentItemID);
            return dtSrc;
        }

    }

    public class LedgerSheetBaseBankReconcile : UserControlBase
    {
        public string TableName;
        public long DocumentItemID;
        BankReconcileGrid legGrid;

        public void CommonSettings(BankReconcileGrid _legGrid, RadButton btnSearch, RadButton btnClear, RadButton btnBack)
        {
            legGrid = _legGrid;
            btnSearch.MouseClick += btnSearch_MouseClick;
            btnBack.MouseClick += btnBack_MouseClick;
            btnClear.MouseClick += btnClear_MouseClick;
            this.Load += LedgerSheetBase_Load;
        }

        void LedgerSheetBase_Load(object sender, EventArgs e)
        {

        }

        void btnClear_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            legGrid.sRemoveBgColor();
        }

        void btnBack_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            //LoadPreviousPage();
            DisplayManager.LoadControl(new UcNavigation(false), true);
        }

        void btnSearch_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            legGrid.sSearchContent();
        }

        public virtual DataTable GetData()
        {
            var dtSrc = LedgerGridData.GetDataTable(TableName, DocumentItemID);
            return dtSrc;
        }

    }

}
