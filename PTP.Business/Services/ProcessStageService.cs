using PTP.Business.Abstractions;
using PTP.DataAccess.Repositories;
using PTP.EntityLayer.Models;

namespace PTP.Business.Services
{
    public class ProcessStageService : IService<ProcessStage>
    {
        private readonly ProcessStageRepository _processStageRepository;

        public ProcessStageService(ProcessStageRepository processStageRepository)
        {
            _processStageRepository = processStageRepository;
        }

        public void Add(ProcessStage entity)
        {
            _processStageRepository.Add(entity);
        }

        public void Delete(int id)
        {
            _processStageRepository.Delete(id);
        }

        public IEnumerable<ProcessStage>? GetAll()
        {
            return _processStageRepository.GetAll();
        }

        public ProcessStage? GetByID(int id)
        {
            return _processStageRepository.GetById(id);
        }

        public void Update(ProcessStage entity)
        {
            _processStageRepository.Update(entity);
        }
    }
}
