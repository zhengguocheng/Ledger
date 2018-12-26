using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DAL;
using DAL.CustomClasses;

namespace DMS.UserControls
{
    public partial class UcCalender : UserControlBase
    {
        public DAL.tblDocumentItem SelectedDoc { get; set; }

        public UcCalender()
        {
            InitializeComponent();
            this.crudMessage = new CustomMessages("Year End");
            this.Caption = "Year End";
            radDateTimePicker1.ApplyDMSSettings();
            radDateTimePicker1.SetDate(DateTime.Now);
        }

        public static List<string> ModifyFolderNames()
        {
            List<string> lstError = new List<string>();

            //AppConstants.RecordType rtype = AppConstants.IsLedger ? AppConstants.RecordType.Ledger : AppConstants.RecordType.Client;
            LedgerRepository rep = new LedgerRepository();

            ClientController con = new ClientController();
            List<Client> lst = con.FetchAll();

            foreach (Client cl in lst)
            {
                try
                {
                    //if (cl.ID == 564)
                    //    return null;
                    List<tblDocumentItem> lstFolYrEnd = rep.FetchAllYearEndByClient(cl.ID);
                    if (lstFolYrEnd != null && lstFolYrEnd.Count > 0)
                    {
                        foreach (var yrEndFol in lstFolYrEnd)
                        {
                            tblDocumentItem doc = new tblDocumentItem();
                            doc.RecordID = yrEndFol.RecordID;//set client id
                            doc.ParentID = yrEndFol.ID;
                            doc.Name = AppConstants.LedgerFolder.Notes;
                            doc.IsFolder = true;
                            doc.Notes = null;
                            doc.AddTag(Tags.TagType.NotesFolder);
                            rep.AddFolder(doc);
                        }
                    }
                }
                catch (Exception ecp)
                {
                    lstError.Add(ecp.Message);
                }
            }

            return lstError;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!radDateTimePicker1.IsNull())
            {
                AppConstants.RecordType rtype = AppConstants.IsLedger ? AppConstants.RecordType.Ledger : AppConstants.RecordType.Client;
                Repository docController = new Repository(rtype);
                string folName = "Year Ended - " + radDateTimePicker1.Value.ToString(AppConstants.DateFormatYearEnd).Trim();

                if (!docController.DocumentExist(folName, SelectedDoc.ParentID, SelectedDoc.RecordID))
                {
                    tblDocumentItem doc = new tblDocumentItem();
                    doc.RecordID = SelectedDoc.RecordID;
                    doc.ParentID = SelectedDoc.ParentID;
                    doc.Name = folName;
                    doc.IsFolder = true;
                    doc.Notes = null;
                    doc.YearEndDate = radDateTimePicker1.Value;
                    doc.AddTag(Tags.TagType.YearEndFolder);

                    if (docController.AddFolder(doc))
                    {
                        foreach (string name in AppConstants.LedgerFolder.FolderList)
                        {
                            tblDocumentItem fol = new tblDocumentItem();
                            fol.RecordID = doc.RecordID;
                            fol.ParentID = doc.ID;
                            fol.Name = name;
                            fol.IsFolder = true;
                            fol.Notes = null;

                            if (fol.Name == AppConstants.LedgerFolder.Notes)
                            {
                                fol.AddTag(Tags.TagType.NotesFolder);
                            }
                            
                            docController.AddFolder(fol);

                            if(name == AppConstants.LedgerFolder.Reports)
                            {
                                SpecialFiles.AddReports(fol.ID);
                            }
                            if (name == AppConstants.LedgerFolder.Trial_Balance)
                            {
                                SpecialFiles.AddTrialBalanceFiles(fol.ID);
                            }
                            if (name == AppConstants.LedgerFolder.Journals)
                            {
                                SpecialFiles.AddJournalsFiles(fol.ID);
                            }
                            if (name == AppConstants.LedgerFolder.Reconcile)
                            {
                                SpecialFiles.AddReconcileFiles(fol.ID);
                            }
                        }

                        GlobalLogger.logger.LogMessage("Going to copy data.");
                        try
                        {
                            YearEndCopyData.CreateNewData(doc.ID);
                        }
                        catch(Exception ecp)
                        {
                            GlobalLogger.logger.LogException(ecp);
                        }
                    }
                }
                else
                {
                    DisplayManager.DisplayMessage("Folder with same name already exist. Please specify another name.", MessageType.Error);
                }

                DisplayManager.CloseDialouge(DialogResult.OK);
            }
            else
            {
                DisplayManager.DisplayMessage("Please select date.", MessageType.Error);
            }
        }
    }
}
