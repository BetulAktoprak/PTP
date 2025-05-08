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
    public class ProjectPersonnelService : IService<ProjectPersonnel>
    {
        private readonly ProjectPersonnelRepository _projectPersonnelRepository;

        public ProjectPersonnelService(ProjectPersonnelRepository projectPersonnelRepository)
        {
            _projectPersonnelRepository = projectPersonnelRepository;
        }

        public void Add(ProjectPersonnel entity)
        {
            _projectPersonnelRepository.Add(entity);
        }

        public void Delete(int id)
        {
            _projectPersonnelRepository.Delete(id);
        }

        public IEnumerable<ProjectPersonnel>? GetAll()
        {
            return _projectPersonnelRepository.GetAll();
        }

        public ProjectPersonnel? GetByID(int id)
        {
            return _projectPersonnelRepository.GetById(id);
        }

        public void Update(ProjectPersonnel entity)
        {
            _projectPersonnelRepository.Update(entity);
        }
    }
}
