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
    public partial class UcGroup : UserControlBase
    {
        GroupController grpCntr = new GroupController();
        public tblGroup SelectedGroup { get; set;}

        public UcGroup()
        {
            InitializeComponent();
            this.Caption = "Group";
        }

        private void UcGroup_Load(object sender, EventArgs e)
        {
            //drpSelGrp.DataSource = grpCntr.FetchAll();
            //drpSelGrp.DisplayMember = "GroupName";
            //drpSelGrp.ValueMember = "ID";

            if (SelectedGroup == null)
                SelectedGroup = new tblGroup();

            if (SelectedGroup != null && SelectedGroup.ID > 0)
            {
                txtGrpName.Text = SelectedGroup.GroupName;
                txtDescription.Text = SelectedGroup.Description;
            }
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
                SelectedGroup.GroupName = txtGrpName.Text.Trim();
                SelectedGroup.Description = txtDescription.Text.Trim();
                grpCntr.Save(SelectedGroup);
                DisplayManager.DisplayMessage("Group added successfully.", MessageType.Success);
                GoBack();
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
            //SelectedGroup = (tblGroup)drpSelGrp.SelectedItem.DataBoundItem;
            //this.ParentForm.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            GoBack();
        }

        void GoBack()
        {
            DisplayManager.LoadControl(new UcGroupList());
        }
    }
}
