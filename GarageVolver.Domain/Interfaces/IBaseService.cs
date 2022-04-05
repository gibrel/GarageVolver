﻿using GarageVolver.Domain.Entities;

namespace GarageVolver.Domain.Interfaces
{
    public interface IBaseService<TEntity> where TEntity : BaseEntity
    {
        public Task<List<TOutputModel>> GetAll<TOutputModel>() where TOutputModel : class;
        public Task<TOutputModel> GetById<TOutputModel>(int id) where TOutputModel : class;
    }
}
