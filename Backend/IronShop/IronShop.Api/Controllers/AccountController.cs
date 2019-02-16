using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using IronShop.Api.Core.Dtos;
using IronShop.Api.Core.Entities;
using IronShop.Api.Core.Entities.Base;
using IronShop.Api.Core.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IronShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly ITokenService _tokenService;

        public AccountController(IUserService service,
                                 ITokenService tokenService)
        {
            _service = service;
            _tokenService = tokenService;
        }

        //POST: api/account/login
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [HttpPost("login")]
        public async Task<ActionResult<IronToken>> Login(LoginDto user)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var userToLogin = new User(user.Email, user.Password);
                var userLoginSuccess = await _service.Login(userToLogin);
                var token = _tokenService.Generate(userLoginSuccess);

                return Created("", token);
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
                ModelState.AddModelError("", ex.Message);
            else
                throw ex; //Delegate error 500 at Middleware
        }
    }
}