using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IronShop.Api.Core.Entities
{
    public class Order
    {
        public int OrderId { get; private set; }
        public DateTime OrderDate { get; private set; }
        public long OrderNumber { get; private set; }
        public ICollection<OrderItem> Items { get; private set; }
        public User User { get; private set; }
        public int UserId { get; private set; }

        public Order()
        {

        }

        public Order(int? orderId, DateTime orderDate, long orderNumber, int userId)
        {
            OrderId = orderId??0;
            OrderDate = orderDate;
            OrderNumber = orderNumber;
            UserId = userId;
            Items = new List<OrderItem>();
        }

        public void AddItem(OrderItem item)
        {
            Items.Add(item);
        }

        public decimal Total()
        {
            var total = 0m;
            foreach (var item in Items)
            {
                total += item.UnitPrice * item.Units;
            }
            return total;
        }
    }
}
