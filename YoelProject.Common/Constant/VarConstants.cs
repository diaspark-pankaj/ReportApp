using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoelProject.Common.Constants
{
    public class VarConstants
    {
		public const string SpecialUsersList = "SpecialUsers";
        public const string UsersList = "UsersList";
        public const string AllUsersList = "AllUsersList";
        public const string AllLegalPracticeAcreaList = "AllLegalPracticeAcreaList";
        public const string AllReportClassCat = "AllReportClassCat";
        public const string AllReportClass = "AllReportClass";
        public const string AllBUList = "AllBUList";
        public const string AllVendorList = "AllVendorList";
        public const string AllVendorInstructionList = "AllVendorInstructionList";

        public const string LawFirmsList = "LawFirmsList";
        public const string MatterRef = "APP";
        public const string Hyphen = "-";
        public const string MatterModel = "MatterModel";
        public const string System = "Chameleon inform";
        public const string SystemDetails = "System";
        public const string InvoiceSearch = "InvoiceSearch";
        public const string Vendor = "Vendor";
        public const string Audit = "Audit";
        public const string Inv = "Inv";
        public const string Approval = "Approval";
        public const string Ready = "Ready";
        public const string Sent = "Sent";
        public const string Grv = "Grv";
        public const string POTask = "POTask";
        public const string ReportableMatters = "ReportableMatters";
        public const string GRVTask = "GRVTask";
        public const string MatterRefBU = "BM";
        public const string XLS = ".xlsx";
        public const string TermsAndConditions = "TermsAndConditions";
        public const string TermsFileName = "GLSA PELFA 2017.pdf";
        public const string InvoiceDocument = "Main/InvoiceDocument";
        public const string GRVTasks = "Main/GRVTasks";
        public const string POTasks = "Main/POTasks";
        public const string InsOverView = "Section/Instructions/Overview";
        public const string TSTasks = "Main/TimesheetCaptureTask";
        public const string RFQTasks = "Main/RFQApprovalTask";
        public const string AuditTasks = "Main/InvoiceAuditTask";
        public const string Timesheet = "Timesheet";
        public const string SubCost = "SubCost";
        public const string Disbursement = "Disbursement";
        public const string Invoice = "Invoice";
        public const string Invoices = "Invoices";

        //Constants used for generating instruction reference number.
        public const string InstructionRefGL = "I";
        public const string InstructionRefBU = "BI";

        //Constants acts as the type of instructions.
        public const string GroupLegalInstruction = "GroupLegalInstruction";
        public const string BusinessUnitInstruction = "BusinessUnitInstruction";

        #region Status Messages for Instruction Approval Task

        /// <summary>
        /// Instruction Approval Task is Approved.
        /// </summary>
        public const string InstructionTask_Pending_Approval = "Pending Approval";

        /// <summary>
        /// Instruction Approval Task is Rejected.
        /// </summary>
        public const string InstructionTask_Rejected = "Rejected";

        /// <summary>
        /// Instruction Approval Task is Approved.
        /// </summary>
        public const string InstructionTask_Approved = "Approved";

        /// <summary>
        /// Instruction Approval Task is "Complete" (rejected/Approved).
        /// </summary>
        public const string InstructionTask_Complete = "Complete";


        #endregion

        #region Status Messages for PO/GRV Contact Request Task

        /// <summary>
        /// PO/GRV Contact Request Task is Pending Contacts.
        /// </summary>
        public const string POContactTask_Pending_Contacts = "Pending Contacts";

        /// <summary>
        /// PO/GRV Contact Request Task is "Complete" (rejected/Approved).
        /// </summary>
        public const string POContactTask_Complete = "Complete";


        #endregion

        #region Status Messages for PO/GRV Contact Request Task

        /// <summary>
        /// Instructions Confirm PO Task is Pending.
        /// </summary>
        public const string InstructionsConfirmPOTask_Pending = "Pending";

        #endregion

        #region Various status belonging to the instructions.

        /// <summary>
        /// Instruction to be approved by admin and ready to send to discipline lead
        /// </summary>
        public const string Instruction_Pending_Review = "Pending Review";
        /// <summary>
        /// Instruction is pending for PO/GRV contact information task to complete.
        /// </summary>
        public const string Instruction_Pending_Contacts = "Pending Contacts";
        /// <summary>
        /// Instruction is rejected.
        /// </summary>
        public const string Instruction_Rejected = "Rejected";
        /// <summary>
        /// Used for business unit - awaiting approval
        /// </summary>
        public const string Instruction_Awaiting_Approval = "Awaiting Approval";
        /// <summary>
        /// Instruction is active at vendor side.
        /// </summary>
        public const string Instruction_Active = "Active";
        /// <summary>
        /// Instruction is closed.
        /// </summary>
        public const string Instruction_Closed = "Closed";

        /// <summary>
        /// Instruction sent to discipline lead for approval.
        /// </summary>
        public const string Instruction_Pending_Approval = "Pending Approval";

        public const string Instruction_Pending_PO = "Pending PO";
        public const string Instruction_PO_Increase_Required = "PO Increase Required";
        public const string Instruction_Pending_PO_Amendment = "Pending PO Amendment";
        public const string Instruction_Pending_PO_Increase = "Pending PO Increase";

        /// <summary>
        /// Indication of sending an instruction to next step.
        /// Used only for internal purpose of application.
        /// </summary>
        public const string Send_Instruction = "Send Instruction";
        #endregion

        public const string ReadonlyFieldMessage = "This field cannot be edited due to references, please contact system admin!";
        public const string NoMoreRecord = "No more records available";
        public const string Context = "Please insert context.";
        public const string MatterDescription = "Please insert matter description.";
        public const string NatureRisk = "Please insert nature of identified risk.";
        public const string RecommendedSteps = "Please insert recommended mitigation steps.";
        public const string ImpactGroupBU = "Please insert impact on groupBU.";
        public const string CurrentStep = "Please insert current next step.";
        public const string ClientAction = "Please insert client action.";
        public const string ClientActions = "Client Action (which could also include the implementation of any identified learnings), please provide a full narrative with the details of:• The specific required action to be taken• Who you have requested to take action• When you made this request";
        public const string MatterNameClassBU = "Matter Name; Primary Classification, BU, Operation, governmental unit conducting the investigation and/or taking the administrative action, Lead Lawyer and External Counsel. If the matter is not in SA or not subject to SA Law then you must also state as much and indicate the applicable country.";
        public const string PreviousUnderstanding = "Brief description of the matter and context and include Description of Commercial Issues and the Agreement Title or the Title of the Transactional Project.";
        public const string IncludeStatement = "Include details of:• Statement of Possible Impact • Assessment of Materiality of Risks";
        public const string MustImpacts = "Must be one of the following: none, impacts sustainability of operations, impacts security of tenure in assets or impacts delivery against key initiatives and explanation is required.";
        public const string RemainsTheSame = "Describe the procedural stage and next steps. Ensure that you have included:• a statement on where the matter is • what activities are taking place • the current and anticipated future stages and timing.";
        public const string IncludeMatterName = "Matter Name, Classification, BU, Operation, IDM Project Name, Lead Lawyer, and External Counsel (if applicable). If the matter is not in SA or not subject to SA Law then you must state as much and indicate the applicable country.";
        public const string IncludeMatterNameOperation = "Matter Name; Primary Classification, BU, Operation, Lead Lawyer and External Counsel. If the matter is not in SA or not subject to SA Law then you must also state as much and indicate the applicable country.";
        public const string IncludeMatterNameLL = "Please Include Matter Name, Classification, BU, operation (If applicable), Lead Lawyer and External Counsel. If not in SA, please state as much and the location.";
        public const string AsPreviousUnderstanding = "Brief description of the matter and context.";
        public const string LicensePermit = "Brief description of the matter and context. Ensure that you have include a description of the license or permit in question.";
        public const string ButMustInclude = "Include details of:• Statement of Principle Legal Issues reflected in the Stage Gate Report ";
        public const string IncludeRegulatorDetails = "Brief description of the matter and context. Ensure that you have included Regulator Details as well as the Legal Basis for Investigation/Action.";
        public const string IncludeDescription = "Brief description of the matter and context. Ensure that you have included the nature and status of change in law regulation.";
        public const string IncludeCourt = "Brief description of the matter and context. Ensure that you have included the Amount Claimed and Relief Requested.";
        public const string PrincipleLegal = "Include details of:• Nature of the Identified Risk and any Consequential Risks, • Statement of principle Legal and Factual Issues, • Assessment of Materiality of Risks.";
        public const string ManagementRisks = "Brief description of any steps taken in mitigation. Ensure to include:• a Statement of Initiatives Underway to Address Impact • a Statement of Obstacles to Mitigation  • Approach to Management of Risk.";
        public const string UnderWayAddress = "Brief description of any steps taken in mitigation. Ensure to include a Statement of Initiative Underway to Address Impact such as including lobbying, Chamber engagement, addressing compliance shortfalls and possible litigation.";
        public const string InsuranceImpact = "State and briefly explain the impact on the Group and/or Business Unit.";
        public const string HighlyConfidential = "Brief description of the matter and context. The field must start with the following words: “This is a highly confidential matters and is subject to legal privilege”.";
        public const string Contracts = "Contracts and Commercial";
        public const string IDM = "IDM Projects";
        public const string Investigations = "Investigations and Administrative Actions";
        public const string Regulations = "New and changed Laws and Regulations";
        public const string Licenses = "Licenses and Permits";
        public const string Litigation = "Litigation";
        public const string OtherMatter = "Other Matters";
        public const string PressReleases = "Press releases and Group/BU Policy";
        public const string Confidential = "Highly Confidential Matters";
        public const string Transactions = "Unusual transactions, corruption or sanctions";
        //New Tooltip
        public const string ContextAdvisoryWork = "In completing this field please set out the matter name, the primary classification, the relevant business unit or function, the relevant BU operations, the country to which the matter relates, the AAGL lead lawyer on the matter and if assisting team members and indicate the name of the external firm and external attorney or counsel on brief.";
        public const string ContextAllAgreements = "In completing this field please set out the matter name, the primary classification, the relevant business unit or function, the relevant BU operations, the country to which the matter relates, the AAGL lead lawyer on the matter and if assisting team members and indicate the name of the external firm and external attorney or counsel on brief.";
        public const string ContextRegulatoryChangeinLaw = "In completing this field please set out the matter name, the primary classification, the relevant business unit or function, the relevant BU operations, the country to which the matter relates, the AAGL lead lawyer on the matter and if assisting team members and indicate the name of the external firm and external attorney or counsel on brief.";
        public const string ContextLitigationandDisputes = "In completing this field please set out the matter name, the primary classification, the relevant business unit or function, the relevant BU operations, the country to which the matter relates, the AAGL lead lawyer on the matter and if assisting team members and indicate the name of the external firm and external attorney or counsel on brief. Please further indicate in which court or institution adjudicating on the matter.";

        public const string MatterDescriptionAdvisoryWork = "In completing this field please provide a brief description of the matter and the legal context as well as a description of the legal risk issues.";
        public const string MatterDescriptionAllAgreements = "In completing this field please provide a brief description of the matter and the legal context as well as a description of the legal risk issues. Please further indicate the agreement title (parties) or the title of the transactional project";
        public const string MatterDescriptionRegulatoryChangeinLaw = "In completing this field please provide a brief description of the matter and the legal context as well as a description of the legal risk issues.";
        public const string MatterDescriptionLitigationandDisputes = "In completing this field please provide a brief description of the matter and the legal context as well as a description of the legal risk issues. Please further indicate the amount claimed or the relief sought.";

        public const string NatureofIdentifiedAdvisoryWork = "In completing this field please include the statement of the possible impact and your assessment of materiality of the risks.";
        public const string NatureofIdentifiedAllAgreements = "In completing this field please include the statement of the possible impact and your assessment of materiality of the risks.";
        public const string NatureofIdentifiedRegulatoryChangeinLaw = "In completing this field please set out the nature of the identified risk and any consequential risks, the principle legal and factual issues  and your assessment of materiality of the risks.";
        public const string NatureofIdentifiedLitigationandDisputes = "In completing this field please set out the nature of the identified risk and any consequential risks, the principle legal and factual issues  and your assessment of materiality of the risks.";

        public const string RecommendedMitigationAdvisoryWork = "";
        public const string RecommendedMitigationAllAgreements = "";
        public const string RecommendedMitigationRegulatoryChangeinLaw = "In completing this field please provide a brief description of any steps taken in mitigation. Please ensure that you include a statement of any initiatives underway to address the impact, a statement of obstacles to mitigation and the approach taken to management of the risk.";
        public const string RecommendedMitigationLitigationandDisputes = "";

        public const string ImpactonGroupAdvisoryWork = "In completing this field, please state and briefly explain the potential impact on the Group and/or Business Unit.";
        public const string ImpactonGroupAllAgreements = "In completing this field, please state and briefly explain the potential impact on the Group and/or Business Unit.";
        public const string ImpactonGroupRegulatoryChangeinLaw = "In completing this field, please state and briefly explain the potential impact on the Group and/or Business Unit as well as the probable outcome on the matter.";
        public const string ImpactonGroupLitigationandDisputes = "In completing this field, please state and briefly explain the potential impact on the Group and/or Business Unit as well as the probable outcome on the matter. Please ensure that you consider and include any insurance impact.";

        public const string ProceduralStageAdvisoryWork = "In completing this field, ensure that you describe the procedural stage and next steps which includes a statement on where the matter is, what activities are taking place internally and externally and the current and anticipated future stages and timing.";
        public const string ProceduralStageAllAgreements = "In completing this field, ensure that you describe the procedural stage and next steps which includes a statement on where the matter is, what activities are taking place internally and externally and the current and anticipated future stages and timing.";
        public const string ProceduralStageRegulatoryChangeinLaw = "In completing this field, ensure that you describe the procedural stage and next steps which includes a statement on where the matter is, what activities are taking place internally and externally and the current and anticipated future stages and timing.";
        public const string ProceduralStageLitigationandDisputes = "In completing this field, ensure that you describe the procedural stage and next steps which includes a statement on where the matter is, what activities are taking place internally and externally and the current and anticipated future stages and timing.";

        public const string ClientActionRequiredAdvisoryWork = "In completing this field, please indicate any required actions from client to move the matter forward (which could also include the implementation of any identified learnings), please provide a full narrative with the details of the specific required action to be taken, who you have requested to take action and when you made this request";
        public const string ClientActionRequiredAllAgreements = "In completing this field, please indicate any required actions from client to move the matter forward (which could also include the implementation of any identified learnings), please provide a full narrative with the details of the specific required action to be taken, who you have requested to take action and when you made this request";
        public const string ClientActionRequiredRegulatoryChangeinLaw = "In completing this field, please indicate any required actions from client to move the matter forward (which could also include the implementation of any identified learnings), please provide a full narrative with the details of the specific required action to be taken, who you have requested to take action and when you made this request";
        public const string ClientActionRequiredLitigationandDisputes = "In completing this field, please indicate any required actions from client to move the matter forward (which could also include the implementation of any identified learnings), please provide a full narrative with the details of the specific required action to be taken, who you have requested to take action and when you made this request";

		//Various status belonging to the Invoice
		public const string InvoiceReassigned_Fail = "INV_FAIL";
		public const string InvoiceReassigned_Success = "INV_SUCCESS";
        public const string SentForPayment = "Sent For Payment";
        public const string Credited = "Credited";
        public const string Paid = "Paid";
        public const string Pending = "Pending";
        public const string AwaitingGRV = "Awaiting GRV";
        public const string ReadyForPayment = "Ready for Payment";
        public const string AwaitingAudit = "Awaiting Audit";
        public const string AwaitingApproval = "Awaiting Approval";
        public const string InvoiceCredited = "Invoice Credited";
        public const string SentForPaymentMsg = "Please note that this invoice has already been sent for payment. Please contact Exigent to credit this invoice.";
        public const string PaidMsg = "Please note that this invoice has already been paid. Please contact Exigent to credit this invoice.";
        public const string CreditedMsg = "Please note that this invoice has already been credited.";
        public const string NoInvoice = "Please note that no such invoice exists.";
        public const string AlreadyInvoiceNumber = "This invoice number has already been submitted. Please contact Exigent for further queries.";
        public const string NoSuchInstruction = "No such instruction exists for {Vendor}. Please refer to the instruction for the correct reference number.";
        public const string Closed = "Closed";
        public const string InstructionClosed = "This instruction has been closed. Please contact the instructing counsel for this matter to get more information.";
        public const string FinalInvoice = "The final invoice has already been submitted for this instruction.";
        public const string InvoiceTotal = "Invalid Invoice total, must be Pre Tax total + Tax amount";
        //public const string DisbursementTotal = "Invalid Invoice total, must be (Attorney Fee + Advocate Fee + 3rd Party Fee + Disbursement Fee + VAT amount) + Non-Vatable Disbursement";
        public const string AttorneyTotalFee = "ELF Lawyer Fees detail must be equals to ELF Lawyer Fees of invoice";
        //public const string AttorneyFeeTotal = "Invalid Invoice Pre-VAT Total, must be Attorney Fee + Advocate Fee + 3rd Party Fee + Disbursement Fee + Non-Vatable Disbursement";
        public const string HDSAPercentage = "Please note that the Black South Africa spend percentage should be between 0 and 100.";
        public const string AdvocateRecommendation = "Advocate recommendation cannot be applicable if advocate fee is to be billed.";
        public const string InvoiceTotalPreVat = "Invoice total must be greater than or equal to Pre Tax total";
        public const string NotApplicable = "Not Applicable";
        public const string TimesheetDocuments = "Timesheet documents must be uploaded if ELF Lawyer Fees is to be billed.";
        public const string SubmitCreditNote = "Please note you have rejected invoices that need to be credited. Please submit a credit note.";
        public const string GRVInvoice = "Please urgently GRV Invoice";
        public const string PendingIncrease = "Pending Increase PO Task";
        public const string PendingAmendment = "Pending PO Amendment Task";
        public const string NoGRV = "No GRV contact on instruction";
        public const string SendPOIncrease = "Send PO Increase Required";
        public const string Cancelled = "Cancelled";
        public const string POIncrease = "PO Increase Required";
        public const string InvoiceSubmit = "Invoice submited successfully.";
        public const string InvoiceUpdated = "Invoice updated successfully.";
        public const string InvoiceAudit = "Please enter the issue found on this invoice below.";
        public const string AmendmentSubject = "Purchase Order Amendment Request";
        public const string CreditNotesDirectory = "Credit Notes";
        //grv tasks
        public const string Complete = "Complete";
        public const string Rejected = "Rejected";
        public const string Approved = "Approved";
        public const string GrvCompleted = "Please note that this task has already been completed.";
        public const string forceComments = "Invoice rejected by admin forcefully.";

        //po tasks
        public const string PendingNew = "Pending New PO Task";
        public const string PoCompleted = "Please note that this task has already been completed.";
        public const string IncreaseComplete = "Increase Complete";
        public const string AmendmentComplete = "Amendment Complete";
        public const string POAmended = "PO Amended";
        public const string POIncreased = "PO Increased";
        public const string POReceived = "PO Received";
        public const string IncreasePurchaseOrder = "Increase Purchase Order Request";
        public const string NewPurchaseOrder = "New Purchase Order Request";
        public const string Active = "Active";
        public const string OnHold = "OnHold";

        public const string ReportableUpdatedSuccess = "Matters updated successfully.";
        public const string ReportableUpdatedError = "Matters already sent for update, please make sure if selection is correct!";
        public const string ReportableApproveError = "Selected matters can't be sent for approval, please make sure if matters are updated!";
        public const string ReportableYesError = "Matters already sent for reporting, please make sure if selection is correct!";
        public const string ReportableReportingSuccess = "Matters sent for reporting successfully.";

        #region Reportable Matter - Reportable Status Values

        public const string Reportable_SentforUpdate = "Sent for Update";
        public const string Reportable_Updated = "Updated";
        public const string Reportable_PendingApproval = "Pending Approval";
        public const string Reportable_Approved = "Approved";
        public const string Reportable_Rejected = "Rejected";

        #endregion

        //timesheet
        public const string TimesheetStatus = "Complete";

        #region User Management
        public const string FullNameExits = "Full Name already exists. Try for another.";
        public const string UserNameExists = "User Name already exists. Try for another.";
        public const string UserNameFullNameExist = "User Name and Full Name already exist. Try for another.";
        public const string HODUserExists = "HOD user already exists. Choose a different User Type or deactivate an existing HOD user to create a new HOD user.";
        public const string ProfileUpdated = "Profile updated successfully.";
        public const string DefaultDashboardSelect = "Default Dashboard must be selected in User Access dashboard list.";
        public const string DefaultDashboardSelectHOD = "Main Dashboard must be selected as Default Dashboard for HOD user.";
        public const string MustSelectBU = "Business Unit must be selected for Business Unit user.";
        public const string MustSelectMT = "Legal Discipline must be selected for Business Unit user.";
        public const string UserAccessSelectGLS = "Main Dashboard must be selected in User Access dashboard list for HOD user.";
        public const string InvalidMonth = "Please enter current month";
        #endregion

        #region Login

        public const string UserNameNotExists = "Username does not exist. Please try again";
        public const string UserAlreadyLoggedIn = "User already logged into the system. Please try again after some time.";
        public const string SelectRespectiveDashboard = "Unable to log-in to the system. Please select your respective dashboard.";
        public const string NoAccess = "You don't have access in the system. Please contact to the administrator.";
        public const string AccountSuspended = "Account suspended";
        public const string AccountNotActive = "Your Account is not Active. Please contact to the administrator.";
        public const string PasswordChanged = "Your Password has been changed. Please login again";
        public const string EmailNotExists = "Incorrect Email or Username";
        public const string UrlNotMatched = "Url is not matched, Please try again";
        public const string PasswordChangedSuccess = "Password Changed Successfully";
        #endregion

        #region BUTimeRecording
        public const string PercentageError = "The total percentage split among business units must not exceed 100%";
        public const string TimeAdded = "Time recorded successfully";
        public const string RecordExists = "Time record already exists.";


        #endregion

        #region Two Factor Authentication
        public const string IncorrectCode = "Invalid Verification Code";
        public const string CodeExpired = "Verification Code has expired.";
        public const string SendErrorOccurred = "Some error has occured while sending verification code. Please request for verfication code again";
        #endregion
    }
}
