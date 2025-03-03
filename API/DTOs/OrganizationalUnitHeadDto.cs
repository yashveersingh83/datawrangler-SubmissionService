//using System.Buffers;

//namespace SubmissionService.Application.DTOs;

//public class OrganizationalUnitHeadDto
//{


//    public Guid Id { get; set; }

//    public string DDSUCode
//    {
//        get; set;
//    }

//    public string Division
//    {
//        get; set;
//    }

//    public string DivisionCode
//    {
//        get; set;
//    }

//    public string SectionCode
//    {
//        get; set;
//    }




//    // return the full name based on the condition with DelegateFullName
//    public string FullName
//    {
//        get;
//        set;
//    }





//    public string FunctionalTitle
//    {
//        get; set;
//    }


//    // Represents if the person has approval role(SIR_APPROVERS)
//    public bool ApproverRole
//    {
//        get; set;
//    }

//    // Represents if the person has contributors role(SIR_CONTRIBUTORS)
//    public bool ContributorRole
//    {
//        get; set;
//    }

//    // comma separated values of the person who has access to more than one role assinged(SIR_CONTRIBUTORS) to home or other divisions
//    public string CoordinatorRoles
//    {
//        get; set;
//    }

//    // checks if role has been delegated to someone then delegated personnel number should be used
//    public string PersonnelNumber
//    {
//        get; set;
//    }
//    public string ManagerComment
//    {
//        get; set;
//    }

//    // show warning message as a title, when there is no approver role to home division , no contributor role to home division and has other department coordinator role
//    public bool ShowWarningIcon => ApproverRole && ContributorRole && !OtherDepartments.Any();

//    // generate the warning title based on the conditions so that it is shown as a tooltip on the grid warning icon
//    public string WarningTitle
//    {
//        get
//        {
//            string title = string.Empty;
//            if (!ApproverRole)
//            {
//                title = "Has no Approver AM role assigned";
//            }

//            if (!HasHomeDepartmentCoordinatorRole)
//            {
//                title = string.Concat(title, Environment.NewLine + "Has no Coordinator AM role for home division assigned");
//            }

//            if (OtherDepartments.Any())
//            {
//                title = string.Concat(title, Environment.NewLine + "Has Coordinator AM role assigned to other division");
//            }

//            return title;
//        }
//    }

//    // Used for excel export where we need to show 'Y' and 'N' char
//    public string ApproverRoleForExcel => ApproverRole ? "Yes" : "No";

//    // list of other departments other than home department
//    private string[] OtherDepartments
//    {
//        get
//        {
//            // check if other roles, if empty return empty string[] else check if string contrain ',' then split it and return an array with all the divisions
//            // otherwise convert single text to an array(many CoordinatorRoles is always comma separated)
//            string[] otherDivisions = string.IsNullOrWhiteSpace(CoordinatorRoles) ? Enumerable.Empty<string>().ToArray() : (
//                CoordinatorRoles.IndexOf(',') > -1 ? CoordinatorRoles.Split(',') : new string[] { CoordinatorRoles });

//            // filter other divisions by excluding home divison
//            otherDivisions = otherDivisions.Where(o => !o.Equals($"SIR_{Division.ToUpper()}_COORDINATOR", StringComparison.OrdinalIgnoreCase)).ToArray();

//            return otherDivisions;
//        }
//    }

//    // checks if user has home department contributor role assigned to it or not
//    private bool HasHomeDepartmentCoordinatorRole
//    {
//        get
//        {
//            return ContributorRole;
//            //return string.IsNullOrWhiteSpace(CoordinatorRoles) ? false : CoordinatorRoles.Any( C => C.ToString().ToUpper().Equals( $"SIR_{Division}_COORDINATOR")) ? true : false;
//        }
//    }
//}
