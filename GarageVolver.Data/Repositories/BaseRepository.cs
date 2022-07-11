using GarageVolver.Data.Context;
using GarageVolver.Domain.Entities;
using GarageVolver.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GarageVolver.Data.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly SQLiteContext _sQLiteContext;

        public BaseRepository(SQLiteContext sQLiteContext)
        {
            _sQLiteContext = sQLiteContext;
        }

        public virtual async Task<bool> Insert(TEntity obj)
        {
            try
            {
                await _sQLiteContext.Set<TEntity>().AddAsync(obj);
                _sQLiteContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }

            return true;
        }

        public virtual async Task<IList<TEntity>> Select()
        {
            var entities = await _sQLiteContext.Set<TEntity>().ToListAsync();
            return entities;
        }

        public virtual async Task<TEntity?> Select(int id)
            => await _sQLiteContext.Set<TEntity>().SingleOrDefaultAsync(x => x.Id == id);

        public virtual async Task<bool> Update(TEntity obj)
        {
            try
            {
                _sQLiteContext.Entry(obj).State = EntityState.Modified;
                await _sQLiteContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }

            return true;
        }

        public virtual async Task<bool> Delete(int id)
        {
            try
            {
                var entity = await Select(id);
                if (entity == null)
                    return false;
                _sQLiteContext.Set<TEntity>().Remove(entity);
                _sQLiteContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }

            return true;
        }
    }
}
