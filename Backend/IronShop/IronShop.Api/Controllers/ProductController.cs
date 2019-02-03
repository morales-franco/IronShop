using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IronShop.Api.Core.Dtos;
using IronShop.Api.Core.Entities;
using IronShop.Api.Core.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IronShop.Api.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;
        private readonly IMapper _mapper;

        public ProductController(IProductService service,
            IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET: api/product
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(200)]
        public async Task<ActionResult<List<ProductDto>>> GetAll()
        {

            var products = await _service.GetAll();
            var model = MapEntityToDto(products);
            return model;
        }

        //GET: api/User/id
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ProductDto>> GetById(int id)
        {
            var product = await _service.GetById(id);
            if (product == null)
            {
                return NotFound();
            }

            var model = MapEntityToDto(product);
            return model;
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
                ModelState.AddModelError("", "Internal Server Error. Please contact your administrator");
            }
        }

        //POST: api/product
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ProductDto>> Create(ProductDto product)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var productEntity = new Product(product.ProductId, product.Category, product.Price, product.Title, product.Description);
                await _service.Create(productEntity);

                return CreatedAtAction(nameof(GetById),
                    new { id = productEntity.ProductId }, productEntity);
            }
            catch (Exception ex)
            {
                //TODO: Change for return 500 code not only 400
                HandleException(ex);
            }

            return BadRequest(ModelState);
        }

        // PUT: api/product/1
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Update(long id, ProductDto product)
        {
            if (!product.ProductId.HasValue || product.ProductId.Value == 0)
            {
                ModelState.AddModelError("", "Product Id is required");
                return BadRequest(ModelState);
            }

            if (id != product.ProductId.Value)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userToChange = new Product(product.ProductId, product.Category, product.Price, product.Title, product.Description);
            await _service.Update(userToChange);

            return NoContent();
        }

        // DELETE: api/product/1
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _service.GetById(id);
            if (product == null)
            {
                return NotFound();
            }

            await _service.Delete(product);

            return NoContent();
        }

        private List<ProductDto> MapEntityToDto(IEnumerable<Product> products)
        {
            return _mapper.Map<IEnumerable<Product>, List<ProductDto>>(products);
        }

        private ProductDto MapEntityToDto(Product product)
        {
            return _mapper.Map<Product, ProductDto>(product);
        }


    }
}