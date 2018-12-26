using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DAL;

namespace DMS.UserControls
{
    public partial class UcClientDocuments : UserControlBase
    {

        UcClientList clntList;
        UcDocumentList docList;

        public UcClientDocuments()
        {
            InitializeComponent();
            this.Caption = "Client List";
        }

        private void UcClientDocuments_Load(object sender, EventArgs e)
        {
            clntList = new UcClientList();
            docList = new UcDocumentList();

            splitContainer1.Panel1.Controls.Add(clntList);
            splitContainer1.Panel2.Controls.Add(docList);
            clntList.Dock = docList.Dock = DockStyle.Fill;
        }

        public void LoadDocuments()
        {
            if (clntList.ClientGrid.SelectedRows.Count > 0)
            {
                docList.SelectedClient = (Client)clntList.ClientGrid.SelectedRows[0].DataBoundItem;
                docList.RefreshGrid(true);
            }
        }
    }
}
