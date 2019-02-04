using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IronShop.Api.Core.Entities
{
    public class OrderItem
    {
        public int OrderItemId { get; private set; }
        public int ProductId { get; private set; }
        public int Units { get; private set; }
        public decimal UnitPrice { get; private set; }

        public Order Order { get; private set; }
        public Product Product { get; private set; }


        public OrderItem()
        {

        }

        public OrderItem(int productId, int units, decimal unitPrice)
        {
            if (units <= 0)
                throw new ValidationException("units must be upper zero");

            if (unitPrice <= 0)
                throw new ValidationException("unit price must be upper zero");

            ProductId = productId;
            Units = units;
            UnitPrice = unitPrice;
        }

    }
}
