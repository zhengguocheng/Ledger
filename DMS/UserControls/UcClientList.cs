using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DMS.CustomClasses;
using DAL;
using Telerik.WinControls.UI;

namespace DMS.UserControls
{
    public partial class UcClientList : UserControlBase
    {
        ClientController cntrl = new ClientController();

        public RadGridView ClientGrid { get; set; }

        public UcClientList()
        {
            InitializeComponent();
            this.Caption = "Client Documents";
        }

        private void UcClientList_Load(object sender, EventArgs e)
        {
            ClientGrid = grdClients;
            this.DisableCellHighlighting(grdClients);
            RefreshGrid();
        }

        void RefreshGrid()
        {
            grdClients.DataSource = cntrl.FetchAll();
            DAL.Client cnt = new DAL.Client();
            
        }

        private void grdClients_SelectionChanged(object sender, EventArgs e)
        {
            UcClientDocuments cd = (UcClientDocuments)this.Parent.Parent.Parent;
            cd.LoadDocuments();
        }
    }
}
