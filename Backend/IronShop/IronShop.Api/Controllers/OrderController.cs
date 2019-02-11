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
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _service;
        private readonly IMapper _mapper;

        public OrderController(IOrderService service,
            IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET: api/order
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<List<OrderDto>>> GetAll()
        {
            var orders = await _service.GetAll();
            var model = MapEntityToDto(orders);
            return model;
        }

        //GET: api/order/id
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<OrderDto>> GetById(int id)
        {
            var order = await _service.GetById(id);
            if (order == null)
            {
                return NotFound();
            }

            var model = MapEntityToDto(order);
            return model;
        }

        //POST: api/order
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<OrderDto>> Create([FromBody]IList<OrderItemDto> items)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (items == null || !items.Any())
                    throw new ValidationException("Items are required");

                var newOrder = new Order(MapItemsDtoToEntity(items));

                newOrder = await _service.Create(newOrder);

                return CreatedAtAction(nameof(GetById),
                    new { id = newOrder.OrderId }, MapEntityToDto(newOrder));
            }
            catch (Exception ex)
            {
                //TODO: Change for return 500 code not only 400
                HandleException(ex);
            }

            return BadRequest(ModelState);
        }

        private List<OrderItem> MapItemsDtoToEntity(IList<OrderItemDto> items)
        {
            return _mapper.Map<IList<OrderItemDto>, List<OrderItem>>(items);
        }

        private Order MapDtoToEntity(OrderDto order)
        {
            return _mapper.Map<OrderDto, Order>(order);
        }

        private List<OrderDto> MapEntityToDto(IEnumerable<Order> orders)
        {
            return _mapper.Map<IEnumerable<Order>, List<OrderDto>>(orders);
        }

        private OrderDto MapEntityToDto(Order order)
        {
            return _mapper.Map<Order, OrderDto>(order);
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
    }
}