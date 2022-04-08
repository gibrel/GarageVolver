using GarageVolver.Data.Context;
using GarageVolver.Domain.Entities;
using GarageVolver.Domain.Interfaces;

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
                _sQLiteContext.Set<TEntity>().Add(obj);
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
            var entities = _sQLiteContext.Set<TEntity>().ToList();
            return entities;
        }

        public virtual async Task<TEntity> Select(int id)
            => _sQLiteContext.Set<TEntity>().SingleOrDefault(x => x.Id == id);

        public virtual async Task<bool> Update(TEntity obj)
        {
            try
            {
                _sQLiteContext.Entry(obj).State =
                    Microsoft.EntityFrameworkCore.EntityState.Modified;
                _sQLiteContext.SaveChanges();
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
                _sQLiteContext.Set<TEntity>().Remove(await Select(id));
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
