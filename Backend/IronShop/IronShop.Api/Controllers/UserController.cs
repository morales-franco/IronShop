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

        //TODO: Get all 
        // GET: api/User/GetAll
        [HttpGet]
        [HttpGet("GetAll")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<List<UserIndexDto>>> GetAll()
        {
            var users = await _service.GetAll();
            return MapEntityToIndex(users);
        }

        //TODO: Get all 
        // GET: api/User/GetAllSP
        [HttpGet]
        [HttpGet("GetAllSP")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<List<UserIndexDto>>> GetAllSP()
        {
            var procedureName = "Index" + typeof(User).Name;
            var rows = await _service.GetList<UserIndexDto>(procedureName, ParseIndexQueryString(Request.Query));
            return rows.ToList();
        }

        //TODO: Get all Paging with E.F pure (using expression's)
        // GET: api/User/GetAllPaged
        [HttpGet("GetAllPaged")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<PaginableList<UserIndexDto>>> GetAllPaged(int? pageSize, int? pageNumber, string sort = null, string dir = null, string fullName = null, string email = null, string role = null)
        {
            PaginableList<User> users = await _service.GetAll(GetIndexParameters(pageSize, pageNumber, sort, dir,fullName, email, role));
            return MapEntityToIndex(users);
        }

        //TODO: Get all Paging with SP
        // GET: api/User/GetAllPagedSP
        [HttpGet("GetAllPagedSP")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<PaginableList<UserIndexDto>>> GetAllPagedSP()
        {
            var procedureName = "IndexPaged" + typeof(User).Name;
            var rows = await _service.GetList<UserIndexDto>(procedureName, ParseIndexQueryString(Request.Query));

            PaginableList<UserIndexDto> paginableList = new PaginableList<UserIndexDto>();
            paginableList.Rows = rows.ToList();
            paginableList.TotalRows = rows.Any() ? rows.FirstOrDefault().TotalRows : 0;
            return paginableList;
        }

        public virtual KeyValuePair<string, object>[] ParseIndexQueryString(IQueryCollection queryString)
        {
            List<KeyValuePair<string, object>> values = new List<KeyValuePair<string, object>>();
            List<string> ignoredKeys = new List<string>() { "Length", "X-Requested-With", "_", "page", "__swhg", "sort", "sortdir" };

            foreach (var key in queryString.Keys)
            {
                if (!string.IsNullOrEmpty(key) &&
                    !string.IsNullOrEmpty(queryString[key]) &&
                    !ignoredKeys.Contains(key))
                {
                    //Parseo de valores boolean
                    switch (queryString[key])
                    {
                        case "true":
                            values.Add(new KeyValuePair<string, object>(key, true));
                            break;
                        case "false":
                            values.Add(new KeyValuePair<string, object>(key, false));
                            break;
                        default:
                            values.Add(new KeyValuePair<string, object>(key, queryString[key]));
                            break;
                    }
                }
            }
            return values.ToArray();
        }

        private PageParameters<User> GetIndexParameters(int? pageSize, int? pageNumber, string sort, string dir,string fullName, string email, string role)
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

            PageParameters<User> pageParameters = new PageParameters<User>(pageSize, pageNumber, filter, orderBy, dir);
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

        private List<UserIndexDto> MapEntityToIndex(IEnumerable<User> users)
        {
            return _mapper.Map<IEnumerable<User>, List<UserIndexDto>>(users);
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