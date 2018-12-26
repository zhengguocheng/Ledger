using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Windows.Forms;

namespace DMS
{
    public enum CrudMessageType { EntitySaved, EntityUpdated, EntityDeleted, EntitySureDelete }


    public class CustomMessages
    {
        string entityName = string.Empty;

        public CustomMessages(string _entName)
        {
            entityName = _entName;
        }

        #region Crud Messages


        string entitySaved = "{0} saved successfully.";
        string entityUpdated = "{0} updated successfully.";
        string entityDeleted = "{0} deleted successfully.";
        string entitySureDelete = "Are you sure you want to delete {0}.";


        public string GetEntityCrudMessage(CrudMessageType type)
        {
            string mess = "";
            switch (type)
            {
                case CrudMessageType.EntitySaved: mess = entitySaved; break;
                case CrudMessageType.EntityDeleted: mess = entityDeleted; break;
                case CrudMessageType.EntityUpdated: mess = entityUpdated; break;
                case CrudMessageType.EntitySureDelete: mess = entitySureDelete; break;
            }
            string str = string.Format(mess, entityName);
            return str.ToFirstCaps();
        }

        #endregion

        #region Application Level Messages

        public static string DesktopAlertCaption = "Ledger Alert:";
        public static string DesktopAlertWelcome = "Welcome to Lee and Associate's Document Management System.";

        //public static string D
        #endregion

        public static string SavedSuccessfully = "{0} added successfully.";
        public static string UpdatedSuccessfully = "{0} updated successfully.";
        public static string DeletedSuccessfully = "{0} deleted successfully.";

        public static string GenericRecordDeleted = "Record deleted successfully.";
        
        static string EnterValid = "Please enter valid '{0}'.";

        public static string LoginAsAdmin = "Only administrator is allowed to view this page. Please login as administrator.";
        public static string AuthenticationFailed = "Please enter valid user name and password.";
        public static string AreYouSure = "Are you sure you want to {0}.";
        public static string DeleteConfirmation = "Are you sure you want to delete selected record?";
        public static string LoginBeforeProceeding = "Please login before proceeding further.";
        public static string StopTrackingBeforeLogOff = "Please stop time tracking before you loggof.";
        public static string EndCall = "Please end call tracking before you loggof.";
        public static string StopTimeTracking = "Your time tracking is on, Please stop time tracking before adding new entry.";
        public static string NoJobToStop = "There is no tracking job available to stop, Please add tracking job first.";
        public static string UpdateExistingJob = "You are going to update allready saved job. Are you sure you want to proceed?";
        public static string UseStopButton = "You cannot update the time for this job because its time tracking is on, Please use stop button.";
        public static string LessThen = "{0} must be less then {1}.";
        public static string RolledOverSuccessfully = "Record rolled-over successfully.";
        public static string DeRolledOverSuccessfully = "Record de Roll-Over successfully.";
        public static string DeRoll_InfoNotFound = "No information was found to de Roll-Over this record.";

        public static string GetValidationMessage(string lbl)
        {
            string str = string.Format(EnterValid,lbl.ToLower());
            return str;
        }

        public static string GetCustomMessage(string message,string lbl)
        {
            string str = string.Format(message, lbl);
            return str.ToFirstCaps();
        }

        //public static bool ShowQuestionDialoge(string message)
        //{
        //    DialogResult res = MessageBox.Show(message, "DMS", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        //    return (res == DialogResult.Yes);
        //}

    }


}
