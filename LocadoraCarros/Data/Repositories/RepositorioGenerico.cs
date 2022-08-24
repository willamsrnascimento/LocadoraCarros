using LocadoraCarros.Data.Interfaces;
using LocadoraCarros.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LocadoraCarros.Data.Repositories
{
    public class RepositorioGenerico<TEntity> : IRepositorioGenerico<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public RepositorioGenerico(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task Atualizar(TEntity entity)
        {
            try
            {
                _applicationDbContext.Update(entity);
                await _applicationDbContext.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task Excluir(int id)
        {
            try
            {
                var entity = await PegarPeloId(id);
                _applicationDbContext.Set<TEntity>().Remove(entity);
                await _applicationDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Excluir(string id)
        {
            try
            {
                var entity = await PegarPeloId(id);
                _applicationDbContext.Set<TEntity>().Remove(entity);
                await _applicationDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Inserir(TEntity entity)
        {
            try
            {
                _applicationDbContext.Set<TEntity>().Add(entity);
                await _applicationDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TEntity> PegarPeloId(int id)
        {
            try
            {
                return await _applicationDbContext.Set<TEntity>().FindAsync(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TEntity> PegarPeloId(string id)
        {
            try
            {
                return await _applicationDbContext.Set<TEntity>().FindAsync(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<TEntity> PegarTodos()
        {
            try
            {
                return _applicationDbContext.Set<TEntity>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
