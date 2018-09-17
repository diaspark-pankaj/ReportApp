
namespace YoelProject.Common.Constants
{
    public class CommonConstants
    {
        #region Validation Message

        public const string Required = "Required";
        public const string MatterRegularExp = "([^<>^])*";
        public const string MatterInvalidCharacters = "Please remove invalid characters like <,>,^,*";
        public const string Success = "Success";
        public const string Error = "Error";
        public const string InvalidFilePdf = "Please select a valid .pdf file.";
        public const string InvalidFileXls = "Please select a valid .xls file.";
        public const string FileUploadSize = "File upload size exceeds the maximum allowed. Please compress & upload again";
        public const string InvalidModel = "Invalid data posted, please send a valid request!";
        public const string UpdateSuccessful = "Data updated successfully!";

        #endregion

        #region Success Message

        public const string FileUploadSuccess = "File uploaded successfully";

        #endregion

        #region Error Message
        public const string SaveError = "Some error occured while saving. Please try again";
        public const string ModelInvalidError = "Invalid data posted.";
        public const string FieldAlreadyExists = "<Field> already exists.";
        #endregion

        public const string Yes = "Yes";
        public const string No = "No";
        public const string PleaseTypeHere = "Please type here";
        public const string PleaseUploadHere = "Please upload documents here";

        #region Common-Const-UserAccess
        public const string ROLE_ADMIN = "Admin";
        public const string ROLE_BU = "BusinessUnit";
        public const string ROLE_GL = "GroupLegal";
        public const string ROLE_VENDOR = "Vendor";
        public const string ROLE_GUEST = "Guest";

        public const string URL_Admin_DASHBOARD = "/Main/SelectMatter";
        public const string URL_BU_DASHBOARD = "/BU/Dashboard";
        public const string URL_GL_DASHBOARD = "/Main/Dashboard";
        public const string URL_DFLT_DASHBOARD = "/Main/Dashboard";
        public const string URL_VENDOR_DASHBOARD = "/Vendor/Dashboard";

        public const string SESSION_USER = "User";
        public const string Currency = "$";
        #endregion
    }
}
