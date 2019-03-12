using Cart.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Cart.API.Database
{
    public interface ICartDatabase
    {
        Task<CartModel> GetSingleAsync(Expression<Func<CartModel, bool>> predicate);

        Task<List<CartModel>> GetListAsync(Expression<Func<CartModel, bool>> predicate);

        Task CreateAsync(CartModel cartModel);

        Task UpdateAsync(Expression<Func<CartModel, bool>> predicate, string updateJson);

        Task UpdateAsync(Expression<Func<CartModel, bool>> predicate, object updateObject);
        
        Task DeleteAsync(Expression<Func<CartModel, bool>> predicate);
    }
}
