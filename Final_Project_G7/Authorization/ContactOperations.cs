using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.VisualBasic;

//----------------Final_Project_G7------------------------

namespace Final_Project_G7.Authorization
{
    // Class defining authorization operations for Contact entities
    public static class ContactOperations
    {
        // Authorization requirement for creating a Contact
        public static OperationAuthorizationRequirement Create =
          new OperationAuthorizationRequirement { Name = Constants.CreateOperationName };

        // Authorization requirement for reading a Contact
        public static OperationAuthorizationRequirement Read =
          new OperationAuthorizationRequirement { Name = Constants.ReadOperationName };

        // Authorization requirement for updating a Contact
        public static OperationAuthorizationRequirement Update =
          new OperationAuthorizationRequirement { Name = Constants.UpdateOperationName };

        // Authorization requirement for deleting a Contact
        public static OperationAuthorizationRequirement Delete =
          new OperationAuthorizationRequirement { Name = Constants.DeleteOperationName };

        // Authorization requirement for approving a Contact
        public static OperationAuthorizationRequirement Approve =
          new OperationAuthorizationRequirement { Name = Constants.ApproveOperationName };

        // Authorization requirement for rejecting a Contact
        public static OperationAuthorizationRequirement Reject =
          new OperationAuthorizationRequirement { Name = Constants.RejectOperationName };
    }

    // Class defining constants for authorization roles and operation names
    public class Constants
    {
        public static readonly string CreateOperationName = "Create";
        public static readonly string ReadOperationName = "Read";
        public static readonly string UpdateOperationName = "Update";
        public static readonly string DeleteOperationName = "Delete";
        public static readonly string ApproveOperationName = "Approve";
        public static readonly string RejectOperationName = "Reject";

        public static readonly string ContactAdministratorsRole =
                                                              "ContactAdministrators";
        public static readonly string ContactManagersRole = "ContactManagers";
    }
}
