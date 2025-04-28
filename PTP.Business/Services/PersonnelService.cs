using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PTP.Business.Abstractions;
using PTP.DataAccess.Repositories;
using PTP.EntityLayer.Models;

namespace PTP.Business.Services
{
    public class PersonnelService : IService<Personnel>
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

        public Personnel? GetByID(int id)
        {
            return _personnelRepository.GetById(id);
        }

        public void Update(Personnel entity)
        {
            
            _personnelRepository.Update(entity);
        }
    }
}
