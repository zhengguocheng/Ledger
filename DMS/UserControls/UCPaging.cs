using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DAL;

namespace FrontEnd.PagingControl
{
   

    public partial class UCPaging : UserControl
    {
        bool selectedChangedFromCode = false;

        int currentPage = 1;
        public int CurrentPage 
        {
            get { return currentPage; } 
        }

        long _totalRec;
        public long TotalRecords 
        {
            get { return _totalRec; }
            set
            {
                _totalRec = value;
                LoadCombo();
            }
        }

        public event OnPagingCompleteHandler onPagingEvent;
        public delegate void OnPagingCompleteHandler(UCPaging cntPaging, EventArgs e); 

        public UCPaging()
        {
            InitializeComponent();
        }
        
        private void UCPaging_Load(object sender, EventArgs e)
        {
        
        }

        public void InitializeValues()
        {
            currentPage = 0;
            LoadCombo();
        }

        void LoadCombo()
        {
            
            List<DropdownItem> lst = new List<DropdownItem>();
            
            for (int i = 1; i <= GetMaxPages(); i++)
            {
                lst.Add(new DropdownItem{ ID = i, Name = i.ToString()});
            }



            CheckPageLimit();

            if (lst.Count > 0)
            {
                selectedChangedFromCode = true;

                drpCurrentPage.DataSource = lst;
                drpCurrentPage.DisplayMember = "Name";
                drpCurrentPage.ValueMember = "ID";
                
                
                int selVal = lst.First(x => x.ID == currentPage).ID;
                drpCurrentPage.SelectedValue = selVal;
                selectedChangedFromCode = false;
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            currentPage--;
            CheckPageLimit();
            onPagingEvent(this, new EventArgs());

        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            currentPage++;
            CheckPageLimit();
            onPagingEvent(this, new EventArgs());

        }

        void CheckPageLimit()
        {
            if (currentPage <= 1)
            {
                currentPage = 1;
            }
            double maxPages = GetMaxPages();
            if (currentPage > maxPages)
            {
                currentPage = (int)maxPages;
            }
        }

        double GetMaxPages()
        {
            double maxPages = Math.Ceiling((TotalRecords * 1.0) / AppConstants.RecordPerPage);
            return maxPages;
        }

        //void LoadPage(int pageNo)
        //{
        //    currentPage = pageNo;
        //    CheckPageLimit();
        //    drpCurrentPage.SelectedItem = currentPage;
        //}

        //private void drpCurrentPage_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (!selectedChangedFromCode)
        //    {
        //        currentPage = (int)drpCurrentPage.SelectedItem;
        //        onPagingEvent(this, new EventArgs());
        //    }
        //}

        //private void drpCurrentPage_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        //{

        //}

        private void drpCurrentPage_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!selectedChangedFromCode)
            {
                Telerik.WinControls.UI.Data.ValueChangedEventArgs arg = (Telerik.WinControls.UI.Data.ValueChangedEventArgs)e;
                currentPage = Convert.ToInt32(arg.OldValue);
                onPagingEvent(this, new EventArgs());
            }
        }

        private void drpCurrentPage_TextChanged(object sender, EventArgs e)
        {
            if (!selectedChangedFromCode)
            {
                //Telerik.WinControls.UI.Data.ValueChangedEventArgs arg = (Telerik.WinControls.UI.Data.ValueChangedEventArgs)e;
                currentPage = Convert.ToInt32(drpCurrentPage.Text);
                onPagingEvent(this, new EventArgs());
            }
        }

    }

    public class DropdownItem
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
