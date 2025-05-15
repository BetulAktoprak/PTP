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
    public class ProjectService : IService<Project>
    {
        private readonly ProjectRepository _projectRepository;

        public ProjectService(ProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public void Add(Project entity)
        {
            _projectRepository.Add(entity);
        }

        public void Delete(int id)
        {
            _projectRepository.Delete(id);
        }

        public IEnumerable<Project>? GetAll()
        {
            return _projectRepository.GetAll();
        }

        public IEnumerable<Project> GetAll(Expression<Func<Project, bool>>? filter = null)
        {
            return _projectRepository.GetAll(filter);
        }

        public Project? GetByID(int id)
        {
            return _projectRepository.GetById(id);
        }

        public void Update(Project entity)
        {
            _projectRepository.Update(entity);
        }
    }
}
