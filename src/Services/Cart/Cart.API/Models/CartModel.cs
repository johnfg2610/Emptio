using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cart.API.Models
{
    public class CartModel
    {
        public Guid UserId { get; set; }

        public List<Guid> ProductList { get; set; }
    }
}
