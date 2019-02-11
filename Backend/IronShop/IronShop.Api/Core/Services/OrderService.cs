using IronShop.Api.Core.Dtos;
using IronShop.Api.Core.Entities;
using IronShop.Api.Core.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IronShop.Api.Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;

        public OrderService(IUnitOfWork unitOfWork,
                             IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
        }

        public async Task<Order> Create(Order newOrden)
        {
            var nextOrderNumber = await GetNextOrderNumber();
            var currentUser = await _userService.GetCurrentUser();
            var order = new Order(0, DateTime.Now, nextOrderNumber, currentUser.UserId);

            foreach (var item in newOrden.Items)
            {
                order.AddItem(new OrderItem(item.ProductId, 
                    item.Units, 
                    item.UnitPrice));
            }

             _unitOfWork.Orders.Add(order);

            await _unitOfWork.Commit();
            return order;

        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            return await _unitOfWork.Orders.GetAll();
        }

        public async Task<Order> GetById(int id)
        {
            return await _unitOfWork.Orders.GetById(id);
        }

        private async Task<long> GetNextOrderNumber()
        {
            var maxNumber = await _unitOfWork.Orders.GetMaxOrderNumber();

            return ++maxNumber;
        }
    }
}
