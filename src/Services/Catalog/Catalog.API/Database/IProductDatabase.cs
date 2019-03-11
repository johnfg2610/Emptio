using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Catalog.API.Models;

namespace Catalog.API.Database
{
    public interface IProductDatabase
    {
        Task<ProductModel> GetSingleAsync(Expression<Func<ProductModel, bool>> predicate);

        Task<List<ProductModel>> GetListAsync(Expression<Func<ProductModel, bool>> predicate);

        Task<Guid> CreateAsync(ProductModel productModel);

        Task UpdateAsync(Expression<Func<ProductModel, bool>> predicate, string updateJson);
        Task UpdateAsync(Expression<Func<ProductModel, bool>> predicate, object updateObject);

        Task DeleteAsync(Expression<Func<ProductModel, bool>> predicate);
    }
}
