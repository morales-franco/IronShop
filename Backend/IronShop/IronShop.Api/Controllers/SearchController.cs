using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IronShop.Api.Core.Dtos;
using IronShop.Api.Core.Dtos.Index;
using IronShop.Api.Core.Entities;
using IronShop.Api.Core.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace IronShop.Api.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    [Authorize]
    public class SearchController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger = Log.ForContext<SearchController>();

        public SearchController(IUserService userService,
            IProductService productService,
            IMapper mapper)
        {
            _userService = userService;
            _productService = productService;
            _mapper = mapper;
        }


        [HttpGet("{term}")]
        [ProducesResponseType(200)]
        public async Task<dynamic> GetAll(string term)
        {
            if (string.IsNullOrEmpty(term))
                return BadRequest("term is required");

            var usersFilter = new List<User>();
            var productsFilter = new List<User>();

            //Parallell Process 
            var usersPromise = _userService.GetAll();
            var productPromise = _productService.GetAll();

            var usersData = (await usersPromise).Where(u => u.FullName.ToLower().Contains(term.ToLower()) || 
                                                            u.Email.ToLower().Contains(term.ToLower()));

            var productData = (await productPromise).Where(p => p.Title.ToLower().Contains(term.ToLower())) ;


            return new
            {
                Users = _mapper.Map<List<UserIndexDto>>(usersData),
                Products = _mapper.Map<List<ProductDto>>(productData)
            };
        }

    }
}