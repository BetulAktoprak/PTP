using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using PTP.Business.Abstractions;
using PTP.DataAccess.Repositories;
using PTP.EntityLayer.Models;

namespace PTP.Business.Services
{
    public class ProcessPersonnelService : IService<ProcessPersonnel>
    {
        private readonly ProcessPersonnelRepository _processPersonnelRepository;

        public ProcessPersonnelService(ProcessPersonnelRepository processPersonnelRepository)
        {
            _processPersonnelRepository = processPersonnelRepository;
        }

        public void Add(ProcessPersonnel entity)
        {
            _processPersonnelRepository.Add(entity);
        }

        public void Delete(int id)
        {
            _processPersonnelRepository.Delete(id);
        }

        public IEnumerable<ProcessPersonnel>? GetAll()
        {
            return _processPersonnelRepository.GetAll();
        }

        public IEnumerable<ProcessPersonnel> GetAll(Expression<Func<ProcessPersonnel, bool>>? filter = null)
        {
            return _processPersonnelRepository.GetAll(filter);
        }

        public ProcessPersonnel? GetByID(int id)
        {
            return _processPersonnelRepository.GetById(id);
        }

        public void Update(ProcessPersonnel entity)
        {
            _processPersonnelRepository.Update(entity);
        }
    }
}
