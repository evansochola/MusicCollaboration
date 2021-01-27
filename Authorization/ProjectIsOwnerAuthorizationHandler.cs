using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using MusicCollaboration.Areas.Identity.Data;
using MusicCollaboration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCollaboration.Authorization
{
    //verifies that a user acting on a resource owns the resource
    public class ProjectIsOwnerAuthorizationHandler
        : AuthorizationHandler<OperationAuthorizationRequirement, Collaboration>
    {
        UserManager<MusicCollaborationUser> _userManager;

        public ProjectIsOwnerAuthorizationHandler(UserManager<MusicCollaborationUser>
            userManager)
        {
            _userManager = userManager;
        }

        protected override Task
            HandleRequirementAsync(AuthorizationHandlerContext context,
                                   OperationAuthorizationRequirement requirement,
                                   Collaboration resource)
        {
            if (context.User == null || resource == null)
            {
                return Task.CompletedTask;
            }

            //If not asking for CRUD permission return
            if (requirement.Name != Constants.CreateOperationName &&
                requirement.Name != Constants.ReadOperationName &&
                requirement.Name != Constants.UpdateOperationName &&
                requirement.Name != Constants.DeleteOperationName)
            {
                return Task.CompletedTask;
            }

            if (resource.OwnerID == _userManager.GetUserId(context.User))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
