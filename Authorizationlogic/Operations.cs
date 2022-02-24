using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace Manager.Authorization
{
    public static class Operations
    {
        public static OperationAuthorizationRequirement warehouse_operations =
          new OperationAuthorizationRequirement { Name = Constants.view_warehouse };
        
    }

    public class Constants
    {
        public static readonly string view_warehouse = "view_warehouse";
        

        public static readonly string AdministratorsRole = "AdministratorRole";
        public static readonly string ManagersRole = "ManagerRole";
    }
}
