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
    public class ProjectPersonnelService : IService<ProjectPersonnel>, IProjectPersonnelService
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

        public IEnumerable<ProjectPersonnel> GetAll(Expression<Func<ProjectPersonnel, bool>>? filter = null)
        {
            return _projectPersonnelRepository.GetAll(filter);
        }

        public List<ProjectPersonnel> GetAllWithPersonnelByProjectId(int projectId)
        {
            return _projectPersonnelRepository.GetAllWithPersonnelByProjectId(projectId);
        }

        public ProjectPersonnel? GetByID(int id)
        {
            return _projectPersonnelRepository.GetById(id);
        }

        public async Task<ProjectPersonnel> GetProjectPersonnelByUserIdAndProjectIdAsync(int personnelId, int projectId)
        {
            return await _projectPersonnelRepository.GetProjectPersonnelByUserIdAndProjectIdAsync(personnelId, projectId);
        }

        public async Task<List<Project>> GetProjectsByUserIdAsync(int userId)
        {
            return await _projectPersonnelRepository.GetProjectsByUserIdAsync(userId);
        }

        public void Update(ProjectPersonnel entity)
        {
            _projectPersonnelRepository.Update(entity);
        }
    }
}
