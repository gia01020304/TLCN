using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace General
{
    public static class MessageHelper
    {
        #region Success
        public static string SaveSuccess = "Successfully saved!";
        public static string DelSuccess = "Deleted Successfully!";
        #endregion

        #region Error
        public static string SaveNotSuccess = "Save is not successful , please try again!";
        public static string DelNotSuccess = "Delete is not successful , please try again!";
        #endregion

        public static string DeleteNotification = "Deleted Successfully.";

        public static string CanNotDeleteNotification = "Can not delete this item, Please try again.";

        public static string SaveConfigurationSuccess = "Successfully saved configurations.";

        public static string FailtoSaveConfigNotification = "Failed to save configurations";

        public static string ImportSuccessNotification = "Imported Successfully.";

        public static string FailToImportNotification = "Can not import this file, please try another file";

        public static string SelectFileImportNotification = "Please select a file to import";

        public static string UserinformationChange = "User is existed and password is changed successful.";

        public static string SaveUserSuccess = "User has been successfully created. ";

        public static string SaveUserError = "Create is not successful , please try again. ";

        public static string DeleteUserSuccess = "User has been successfully deleted.";

        public static string UpdateUserSuccess = "User has been successfully updated.";

        public static object TemplateMsgResultImport(int rowImport, int rowNotImport)
        {
            dynamic obj = new ExpandoObject();
            string formatMsgSuccess = "{0} row(s) import success.";
            string formatMsgError = "{0} row(s) import error.";
            obj.msg = string.Format(formatMsgSuccess, rowImport) + "<br/>" + string.Format(formatMsgError, rowNotImport);
            obj.typeMsg = 2;
            if (rowImport == 0 && rowNotImport > 0)
            {
                obj.typeMsg = 1;
                return obj;
            }

            if (rowImport >= 0 && rowNotImport == 0)
            {
                obj.typeMsg = 0;
                return obj;
            }
            return obj;
        }
    }
}
