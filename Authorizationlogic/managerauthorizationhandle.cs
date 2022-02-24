
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Authorization.Infrastructure;
    using Microsoft.AspNetCore.Identity;

    namespace Manager.Authorization
    {
        public class ManagerAuthorizationHandler :
            AuthorizationHandler<OperationAuthorizationRequirement>
        {
            protected override Task
                HandleRequirementAsync(AuthorizationHandlerContext context,
                                       OperationAuthorizationRequirement requirement)
                                       
            {
                if (context.User == null)
                {
                    return Task.CompletedTask;
                }


                // Managers can approve or reject.
                if (context.User.IsInRole(Constants.ManagersRole))
                {
                    context.Succeed(requirement);
                }

                return Task.CompletedTask;
            }
        }
   
        public class AdministratorsAuthorizationHandler
                        : AuthorizationHandler<OperationAuthorizationRequirement>
        {
            protected override Task HandleRequirementAsync(
                                                    AuthorizationHandlerContext context,
                                        OperationAuthorizationRequirement requirement)
                                            
            {
                if (context.User == null)
                {
                    return Task.CompletedTask;
                }

                // Administrators can do anything.
                if (context.User.IsInRole(Constants.AdministratorsRole))
                {
                    context.Succeed(requirement);
                }

                return Task.CompletedTask;
            }
        }
    }
    


