using FluentValidation;
using GarageVolver.Domain.Entities;

namespace GarageVolver.Domain.Interfaces
{
    public interface IBaseService<TEntity> where TEntity : BaseEntity
    {
        public Task<TOutputModel> Add<TInputModel, TOutputModel, TValidator>(TInputModel inputModel)
            where TValidator : AbstractValidator<TEntity>
            where TInputModel : class
            where TOutputModel : class;
        public Task<List<TOutputModel>> GetAll<TOutputModel>() where TOutputModel : class;
        public Task<TOutputModel> GetById<TOutputModel>(int id) where TOutputModel : class;
    }
}
