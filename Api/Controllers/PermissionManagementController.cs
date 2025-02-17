using Api.Repositories;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Share.Models;
using System.Collections.Generic;

namespace Api.Controllers
{
    [Route("/api")]
    [ApiController]
    public class PermissionManagementController : ControllerBase
    {
        private readonly IPermissionManagementRepository permissionManagementRepository;
        private readonly IUserPermissionRepository userPermissionRepository;
        private readonly IGroupPermissionRepository groupPermissionRepository;
        public PermissionManagementController(IPermissionManagementRepository permissionManagementRepository, IUserPermissionRepository userPermissionRepository, IGroupPermissionRepository groupPermissionRepository)
        {
            this.permissionManagementRepository = permissionManagementRepository;
            this.userPermissionRepository = userPermissionRepository;
            this.groupPermissionRepository = groupPermissionRepository;
        }
        [HttpGet("/allpermission")]
        public async Task<ActionResult<List<PermissionManagement>>> GetAllGroupsPermissionAsync(CancellationToken cancellation)
        {
            var content = await permissionManagementRepository.GetAll("PermissionAll");
            //List < (UserPermission, GroupPermission)> _content = new();
            if (!content.Any() || content is null) return NotFound("Item is not found");
            return Ok(content);
        }
        // create user permission
        [HttpPost("/users/create")]
        public async Task<ActionResult<UserPermission>> CreateUsersPermission(UserPermission body, CancellationToken cancellation)
        {
           
            var content = await userPermissionRepository.Create(body, "PermissionAll", body.Phone!);
            return Ok(content);

        }
        // create group permission
        [HttpPost("/groups/create")]
        public async Task<ActionResult<GroupPermission>> CreateGroupsPermission(GroupPermission body, CancellationToken cancellation)
        {
            var id = Guid.NewGuid().ToString();
            body.Id = id;
            var content = await groupPermissionRepository.Create(body, "PermissionAll", id);
            return Ok(content);
        }
        // get specific user permission
        [HttpGet("/user/{id}")]
        public async Task<ActionResult> GetUserPermissionById(string id)
        {
            var content = await userPermissionRepository.GetById(id);
            return Ok(content);

        }
        // get specific group permission
        [HttpGet("/group/{id}")]
        public async Task<ActionResult> GetGroupPermissionById(string id)
        {
            var content = await groupPermissionRepository.GetById(id);
            return Ok(content);

        }
        [HttpPut("/user/{id}/update")]
        public async Task<ActionResult> UpdateUserPermission(string id, UserPermission body)
        {
            var content = await userPermissionRepository.UpdateUserPermission(id, body);
            return Ok(content);
        }
        [HttpPut("/group/{id}/update")]
        public async Task<ActionResult> UpdateUserPermission(string id, GroupPermission body)
        {
            var content = await groupPermissionRepository.UpdateGroupPermission(id, body);
            return Ok(content);
        }
        [HttpDelete("/user/{id}/delete")]
        public async Task<ActionResult> DeleteUserPermission(string id)
        {
            await userPermissionRepository.DeleteUserPermission(id);
            return Ok();
        }
        [HttpDelete("/group/{id}/delete")]
        public async Task<ActionResult> DeleteGroupPermission(string id)
        {
            await groupPermissionRepository.DeleteGroupPermission(id);
            return Ok();
        }

    }
}
