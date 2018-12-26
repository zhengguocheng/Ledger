using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DAL;

namespace DMS
{
   
    public partial class UcFolderSelecttion : UserControlBase
    {
        ClientController clntCntrl = new ClientController();
        Repository repCntr = new Repository(AppConstants.RecordType.Client);
        long currentFolderID { get; set; }
        bool isPopupMode = false;
        public tblDocumentItem PopupSelection { get; set; }
        List<tblDocumentItem> docList;


        public UcFolderSelecttion(bool _isPopupMode, long prntFolID = 0)
        {
            InitializeComponent();
            this.Caption = "Select Folder";
            isPopupMode = _isPopupMode;
            currentFolderID = prntFolID;
        }

        private void UcNavigation_Load(object sender, EventArgs e)
        {
            docList = repCntr.FetchChildren(currentFolderID);
            docList = docList.OrderByDescending(x => x.IsFolder).ThenBy(x => x.Name).ToList();
            PopulateNodes(docList, treeView1.Nodes);
        }

        #region Tree Load

        private void PopulateSubLevel(long parentid, TreeNode parentNode)
        {
            if (parentNode.Nodes != null && parentNode.Nodes.Count > 0)
            {
                parentNode.Nodes.Clear();
            }
            List<tblDocumentItem> lst = repCntr.FetchChildren((tblDocumentItem)parentNode.Tag);
            lst.RemoveAll(x => x.IsFolder == false);
            lst = lst.OrderByDescending(x => x.IsFolder).OrderBy(x => x.Name).ToList();
            PopulateNodes(lst, parentNode.Nodes);
        }

        private void PopulateNodes(List<tblDocumentItem> lst, TreeNodeCollection nodes)
        {
            foreach (tblDocumentItem itm in lst)
            {
                TreeNode tn = new TreeNode();
                tn.Text = itm.Name.ToString();
                tn.Name = itm.ID.ToString();
                tn.Tag = itm;
                tn.SelectedImageIndex = tn.ImageIndex = (itm.IsFolder) ? 0 : 1;

                if (!itm.IsRootNode)
                {
                    TreeNode[] arr = treeView1.Nodes.Find(itm.ParentID.ToString(), true);
                    if (arr != null && arr.Length > 0)
                        arr[0].Nodes.Add(tn);
                }
                else
                    nodes.Add(tn);
            }
        }

        #endregion

        #region Events
        
        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (!e.Node.IsExpanded)
            {
                tblDocumentItem itm = (tblDocumentItem)e.Node.Tag;
                if (itm.IsFolder)
                    PopulateSubLevel(long.Parse(e.Node.Name), e.Node);
            }
        }
      
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (isPopupMode)
            {
                tblDocumentItem itm = (tblDocumentItem)treeView1.SelectedNode.Tag;
                if (!itm.IsFolder)
                {
                    DisplayManager.DisplayMessage("Please select folder.", MessageType.Error);
                    return;
                }
                else
                {
                    PopupSelection = itm;
                    Form parent = (Form)this.ParentForm;
                    parent.DialogResult = DialogResult.OK;
                }
            }
        }

       
        #endregion

       
    }

}
