using AutoMapper;
using DatinApp.API.Data;
using DatinApp.API.Dtos;
using DatinApp.API.Helpers;
using DatinApp.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DatinApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly UserManager<User> _userManager;


        private readonly IDatingRepository _repo;
        private readonly IMapper _mapper;

        public AdminController(DataContext context, UserManager<User> userManager,
            IDatingRepository repo,
            IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
            _context = context;
            _userManager = userManager;
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("usersWithRoles")]
        public async Task<IActionResult> GetUsersWithRoles()
        {
            var userList = await (from user in _context.Users
                                  orderby user.UserName
                                  select new
                                  {
                                      Id = user.Id,
                                      UserName = user.UserName,
                                      Roles = (from userRole in user.UserRoles
                                               join role in _context.Roles
                                               on userRole.RoleId equals role.Id
                                               select role.Name
                                              ).ToList()
                                  }).ToListAsync();

            return Ok(userList);
        }

        [HttpPost("editRoles/{userName}")]
        public async Task<IActionResult> EditRoles(string userName, RoleEditDto roleEditDto)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var userRoles = await _userManager.GetRolesAsync(user);
            var selectedRoles = roleEditDto.RolesNames;
            selectedRoles = selectedRoles ?? new string[] { };
            var result = await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles));

            if (!result.Succeeded)
                return BadRequest("Failed to add roles");

            result = await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles));

            if (!result.Succeeded)
                return BadRequest("Failed to remove roles");

            return Ok(await _userManager.GetRolesAsync(user));
        }


        [Authorize(Policy = "ModeratePhotoRole")]
        [HttpGet("photosForModeration")]
        public async Task<IActionResult> GetPhotosForModeration([FromQuery]PaggingParam param)
        {
            var photos = await _repo.GetPhotoForApprove(param);
            var photosToReturn = _mapper.Map<PagedList<PhotoForReturnDto>>(photos);

            return Ok(photosToReturn);
        }

        [Authorize(Policy = "ModeratePhotoRole")]
        [HttpPost("approvePhoto/{id}")]
        public async Task<IActionResult> ApprovePhoto(int id)
        {
            var photo = await _repo.GetPhoto(id, true);

            if (photo != null)
                photo.IsApproved = true;

            if (await _repo.SaveAll())
                return NoContent();

            throw new Exception($"Error while approving photo with id {id}");
        }

        [Authorize(Policy = "ModeratePhotoRole")]
        [HttpPost("denyPhoto/{id}")]
        public async Task<IActionResult> DenyPhoto(int id)
        {
            var photo = await _repo.GetPhoto(id, true);

            if (photo != null)
                _repo.Delete(photo);

            if (await _repo.SaveAll())
                return NoContent();

            throw new Exception($"Error while approving photo with id {id}");
        }
    }
}