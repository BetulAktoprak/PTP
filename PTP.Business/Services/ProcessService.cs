using PTP.Business.Abstractions;
using PTP.DataAccess.Repositories;
using PTP.EntityLayer.Models;

namespace PTP.Business.Services
{
    public class ProcessService : IService<Process>, IProcessService
    {
        private readonly ProcessRepository _processRepository;

        public ProcessService(ProcessRepository processRepository)
        {
            _processRepository = processRepository;
        }

        public void Add(Process entity)
        {
            _processRepository.Add(entity);
        }

        public void Delete(int id)
        {
            _processRepository.Delete(id);
        }

        public IEnumerable<Process>? GetAll()
        {
            return _processRepository.GetAll();
        }

        public async Task<List<Process>> GetAllByProjectIdAsync(int projectId)
        {
            return await _processRepository.GetAllByProjectIdAsync(projectId);
        }

        public Process? GetByID(int id)
        {
            return _processRepository.GetById(id);
        }

        public void Update(Process entity)
        {
            _processRepository.Update(entity);
        }
    }
}
