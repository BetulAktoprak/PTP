﻿using System.Linq.Expressions;
using PTP.Business.Abstractions;
using PTP.DataAccess.Repositories;
using PTP.EntityLayer.Models;

namespace PTP.Business.Services
{
    public class PersonnelService : IService<Personnel>, IPersonnelService
    {
        private readonly PersonnelRepository _personnelRepository;

        public PersonnelService(PersonnelRepository personnelRepository)
        {
            _personnelRepository = personnelRepository;
        }

        public void Add(Personnel entity)
        {
            _personnelRepository.Add(entity);
        }

        public void Delete(int id)
        {
            _personnelRepository.Delete(id);
        }

        public IEnumerable<Personnel>? GetAll()
        {
            return _personnelRepository.GetAll();
        }

        public IEnumerable<Personnel> GetAll(Expression<Func<Personnel, bool>>? filter = null)
        {
            return _personnelRepository.GetAll(filter);
        }

        public Personnel? GetByID(int id)
        {
            return _personnelRepository.GetById(id);
        }

        public async Task<Personnel> GetPersonnelByUserIdAsync(int userId)
        {
            return await _personnelRepository.GetPersonnelByUserIdAsync(userId);
        }

        public void Update(Personnel entity)
        {
            
            _personnelRepository.Update(entity);
        }
    }
}
