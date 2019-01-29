using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IronShop.Api.Core;
using IronShop.Api.Core.Dtos;
using IronShop.Api.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IronShop.Api.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserController(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: api/User
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<List<UserDto>>> GetAll()
        {
            var users = await _unitOfWork.Users.GetAll();
            var model = MapEntityToDto(users);
            return model;
        }

        // GET: api/User/GetByEmail/test@test.com.ar
        [HttpGet("GetByEmail/{email}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<UserDto>> GetByEmail(string email)
        {
            var user = await _unitOfWork.Users.GetByEmail(email);
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
            var user = await _unitOfWork.Users.GetById(id);
            if (user == null)
            {
                return NotFound();
            }

            var model = MapEntityToDto(user);
            return model;
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<UserDto>> Create(UserDto user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userEntity = MapDtoToEntity(user);
            await _unitOfWork.Users.Add(userEntity);
            await _unitOfWork.Commit();

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
    }
}