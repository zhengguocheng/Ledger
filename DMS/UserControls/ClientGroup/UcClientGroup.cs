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
    public partial class UcClientGroup : UserControlBase
    {
        GroupController grpCntr = new GroupController();
        public tblGroup SelectedGroup { get; set;}

        public UcClientGroup()
        {
            InitializeComponent();
            this.Caption = "Client Groups";
        }

        private void UcClientGroup_Load(object sender, EventArgs e)
        {
            drpSelGrp.DataSource = grpCntr.FetchAll();
            drpSelGrp.DisplayMember = "GroupName";
            drpSelGrp.ValueMember = "ID";
        }

        private void btnAddGroup_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtGrpName.Text))
            {
                this.ShowValidationError(txtGrpName, CustomMessages.GetValidationMessage("Group Name"));
                return;
            }
            try
            {
                tblGroup grp = new tblGroup();
                grp.GroupName = txtGrpName.Text.Trim();
                grpCntr.Save(grp);
                txtGrpName.Text = string.Empty;
                DisplayManager.DisplayMessage("Group added successfully.", MessageType.Success);
                UcClientGroup_Load(null, null);
            }
            catch (Exception ecp)
            {
                if (ecp.InnerException != null && ecp.InnerException.Message.Contains("Unique_GroupName"))
                {
                    DisplayManager.DisplayMessage("Group Name you entered already exists. Please enter different group name.", MessageType.Error);
                }
                else
                    DisplayManager.DisplayMessage(ecp.Message, MessageType.Error);

            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SelectedGroup = (tblGroup)drpSelGrp.SelectedItem.DataBoundItem;
            this.ParentForm.DialogResult = DialogResult.OK;
        }

        
    }
}
