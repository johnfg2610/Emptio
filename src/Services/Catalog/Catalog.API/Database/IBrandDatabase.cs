using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Catalog.API.Models;

namespace Catalog.API.Database
{
    public interface IBrandDatabase
    {
        Task<BrandModel> GetSingleAsync(Expression<Func<BrandModel, bool>> predicate);

        Task<List<BrandModel>> GetListAsync(Expression<Func<BrandModel, bool>> predicate);

        Task<Guid> CreateAsync(BrandModel productModel);

        Task UpdateAsync(Expression<Func<BrandModel, bool>> predicate, string updateJson);

        Task UpdateAsync(Expression<Func<BrandModel, bool>> predicate, object updateObject);

        Task UpdateAsync(Guid id, BrandModel updateObject);

        Task DeleteAsync(Expression<Func<BrandModel, bool>> predicate);
    }
}
