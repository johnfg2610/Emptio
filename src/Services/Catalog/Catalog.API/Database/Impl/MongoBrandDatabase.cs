using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Catalog.API.Models;
using Common;
using MongoDB.Driver;

namespace Catalog.API.Database.Impl
{
    public class MongoBrandDatabase : IBrandDatabase
    {
        private readonly IMongoCollection<BrandModel> _brandCollection;
        private readonly IdGenerator idGenerator;

        public MongoBrandDatabase(IMongoCollection<BrandModel> brandCollection, IdGenerator idGenerator) {         
            _brandCollection = brandCollection;
            this.idGenerator = idGenerator;
        }

        public async Task<BrandModel> GetSingleAsync(Expression<Func<BrandModel, bool>> predicate)
        {
            return await(await _brandCollection.FindAsync(predicate)).FirstOrDefaultAsync();
        }

        public async Task<List<BrandModel>> GetListAsync(Expression<Func<BrandModel, bool>> predicate)
        {
            return await(await _brandCollection.FindAsync(predicate)).ToListAsync();
        }

        public async Task<Guid> CreateAsync(BrandModel brandModel)
        {
            var id = idGenerator.GenerateID();
            brandModel.Id = id;
            await _brandCollection.InsertOneAsync(brandModel);
            return id;
        }

        public Task UpdateAsync(Expression<Func<BrandModel, bool>> predicate, string updateJson)
        {
            return _brandCollection.FindOneAndUpdateAsync(predicate, new JsonUpdateDefinition<BrandModel>(updateJson));
        }

        public Task UpdateAsync(Expression<Func<BrandModel, bool>> predicate, object updateObject)
        { 

            return _brandCollection.FindOneAndUpdateAsync(predicate, new ObjectUpdateDefinition<BrandModel>(updateObject));
        }

        public Task DeleteAsync(Expression<Func<BrandModel, bool>> predicate)
        {
            return _brandCollection.DeleteOneAsync(predicate);
        }

        public Task UpdateAsync(Guid id, BrandModel updateModel)
        {
            updateModel.Id = id;
            return _brandCollection.UpdateOneAsync(it => it.Id == id, new ObjectUpdateDefinition<BrandModel>(updateModel));
        }
    }
}
