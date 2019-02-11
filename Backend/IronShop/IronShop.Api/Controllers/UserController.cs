using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using IronShop.Api.Core;
using IronShop.Api.Core.Dtos;
using IronShop.Api.Core.Dtos.Index;
using IronShop.Api.Core.Entities;
using IronShop.Api.Core.Entities.Base;
using IronShop.Api.Core.IServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IronShop.Api.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly IMapper _mapper;

        public UserController(IUserService service,
            IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET: api/User
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<PaginableList<UserIndexDto>>> GetAll(int? rowsPerPage, int? pageNumber, string sort = null, string dir = null, string fullName = null, string email = null, string role = null)
        {
            #region test membesrhip
            var currentUser = HttpContext.User;

            var hasUserId = currentUser.HasClaim(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier);
            var hasEmailAddress = currentUser.HasClaim(c => c.Type == System.Security.Claims.ClaimTypes.Email);
            var hasRole = currentUser.HasClaim(c => c.Type == System.Security.Claims.ClaimTypes.Role);

            var roleT = currentUser.FindFirst(System.Security.Claims.ClaimTypes.Role);
            var emailT = currentUser.FindFirst(System.Security.Claims.ClaimTypes.Email);
            var userId = currentUser.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);

            var isAdmin = currentUser.IsInRole("ADMIN");
            #endregion

            PaginableList<User> users = await _service.GetAll(GetIndexParameters(rowsPerPage, pageNumber, sort, dir,fullName, email, role));

            return MapEntityToIndex(users);
        }

        private PageParameters<User> GetIndexParameters(int? rowsPerPage, int? pageNumber, string sort, string dir,string fullName, string email, string role)
        {
            Expression<Func<User, bool>> filter = u => (string.IsNullOrEmpty(fullName) || u.FullName.ToLower().Contains(fullName.ToLower())) &&
                                                       (string.IsNullOrEmpty(email) || u.Email.ToLower().Contains(email.ToLower())) &&
                                                       (string.IsNullOrEmpty(role) || u.Email.ToLower().Contains(email.ToLower()));

            Expression<Func<User, dynamic>> orderBy = null;

            switch (sort.ToLower())
            {
                case "fullname":
                    orderBy = u => u.FullName;
                    break;
                case "email":
                    orderBy = u => u.Email;
                    break;
                case "role":
                    orderBy = u => u.Role;
                    break;
                default:
                    break;
            }

            PageParameters<User> pageParameters = new PageParameters<User>(rowsPerPage, pageNumber, filter, orderBy, dir);
            return pageParameters;
        }



        // GET: api/User/GetByEmail/test@test.com.ar
        [HttpGet("GetByEmail/{email}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<UserDto>> GetByEmail(string email)
        {
            var user = await _service.GetByEmail(email);
            if (user == null)
            {
                return NotFound();
            }


            var model = MapEntityToDto(user);
            return model;
        }

        //GET: api/User/id
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<UserDto>> GetById(int id)
        {
            var user = await _service.GetById(id);
            if (user == null)
            {
                return NotFound();
            }

            var model = MapEntityToDto(user);
            return model;
        }

        //POST: api/Register
        [HttpPost("Register")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<UserDto>> Register(RegisterUserDto user)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var userEntity = new User(user.FullName, user.Email, user.Password, user.Role);
                await _service.Register(userEntity);

                return CreatedAtAction(nameof(GetById),
                    new { id = userEntity.UserId }, userEntity);
            }
            catch (Exception ex)
            {
                //TODO: Change for return 500 code not only 400
                HandleException(ex);
            }

            return BadRequest(ModelState);
        }

        private void HandleException(Exception ex)
        {
            if (ex is ValidationException)
            {
                ModelState.AddModelError("", ex.Message);
            }
            else
            {
                //Utils.Services.Logger.Error(ex);
                ModelState.AddModelError("", "Se ha producido un error interno por favor consulte al administrador");
            }
        }

        // PUT: api/User/1
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Update(long id, ChangeUserDto user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userToChange = new User(user.UserId, user.FullName, user.Email, user.Role);
            await _service.Update(userToChange);

            return NoContent();
        }

        // DELETE: api/User/1
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _service.GetById(id);
            if (user == null)
            {
                return NotFound();
            }

            await _service.Delete(user);

            return NoContent();
        }

        //POST: api/ChangePassword
        [HttpPost("ChangePassword")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<UserDto>> ChangePassword(ResetPasswordDto user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userEntity = new User(user.UserId, user.Password);
            await _service.ChangePassword(userEntity);

            return CreatedAtAction(nameof(GetById),
                new { id = userEntity.UserId }, userEntity);
        }

        private UserDto MapEntityToDto(User user)
        {
            return _mapper.Map<User, UserDto>(user);
        }

        private List<UserDto> MapEntityToDto(IEnumerable<User> users)
        {
            return _mapper.Map<IEnumerable<User>, List<UserDto>>(users);
        }

        private User MapDtoToEntity(UserDto user)
        {
            return _mapper.Map<UserDto, User>(user);
        }

        private PaginableList<UserIndexDto> MapEntityToIndex(PaginableList<User> users)
        {
            PaginableList<UserIndexDto> dto = new PaginableList<UserIndexDto>();
            dto.Rows = _mapper.Map<IEnumerable<User>, List<UserIndexDto>>(users.Rows);
            dto.TotalRows = users.TotalRows;
            return dto;
        }


    }
}