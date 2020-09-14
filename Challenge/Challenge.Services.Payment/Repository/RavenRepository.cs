using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Challenge.Services.Payment.Type;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;

namespace Challenge.Services.Payment.Repository
{

    public class RavenRepository<TEntity> where TEntity : IIdentifiable
    {
        private readonly IDocumentStore _store;
        protected readonly IAsyncDocumentSession SessionAsync;
        private readonly IMapper _mapper;

        public RavenRepository(IDocumentStore store, IMapper mapper)
        {
            _mapper = mapper;
            _store = store;

            SessionAsync = _store.OpenAsyncSession();
        }

        public async Task<TEntity> GetAsync(string id)
            => await GetAsync(e => e.Id == id);

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
            => await SessionAsync.Query<TEntity>().Where(predicate).SingleOrDefaultAsync();

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
            => await SessionAsync.Query<TEntity>().Where(predicate).ToListAsync();


        public async Task<TEntity> AddAsync(TEntity entity)
        {
            entity.Id = Guid.NewGuid().ToString();
            await SessionAsync.StoreAsync(entity);
            await SessionAsync.SaveChangesAsync();
            return entity;
        }
        public async Task UpdateAsync(TEntity entity)
        {
            var document = await SessionAsync.LoadAsync<TEntity>(entity.Id);
            entity.CopyPropertiesTo(document);
            await SessionAsync.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            SessionAsync.Delete(id);
            await SessionAsync.SaveChangesAsync();
        }

    }


}
