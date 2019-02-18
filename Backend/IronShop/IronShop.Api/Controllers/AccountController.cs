using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Google.Apis.Auth;
using IronShop.Api.Core.Dtos;
using IronShop.Api.Core.Entities;
using IronShop.Api.Core.Entities.Base;
using IronShop.Api.Core.IServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace IronShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly ILogger _logger = Log.ForContext<AccountController>();


        public AccountController(IUserService service)
        {
            _service = service;
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
                var token = await _service.Login(userToLogin);

                return Created("", token);
            }
            catch (Exception ex)
            {
                //TODO: Change for return 500 code not only 400
                HandleException(ex);
            }

            return BadRequest(ModelState);
        }

        //POST: api/account/login/google
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [HttpPost("login/google")]
        public async Task<ActionResult<IronToken>> Google(LoginGoogleDto user)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var userGoogle = new UserGoogle(user.Email, user.TokenGoogle);
                IronToken token = await _service.Login(userGoogle);
                return Created("", token);
            }
            catch (InvalidJwtException)
            {
                ModelState.AddModelError("", "Invalid credentials");
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