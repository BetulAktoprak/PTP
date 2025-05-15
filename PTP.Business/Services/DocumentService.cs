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
    public class DocumentService : IService<Document>
    {
        private readonly DocumentRepository _documentRepository;

        public DocumentService(DocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        public void Add(Document entity)
        {
            _documentRepository.Add(entity);
        }

        public void Delete(int id)
        {
            _documentRepository.Delete(id);
        }

        public IEnumerable<Document>? GetAll()
        {
            return _documentRepository.GetAll();
        }

        public IEnumerable<Document> GetAll(Expression<Func<Document, bool>>? filter = null)
        {
            return _documentRepository.GetAll(filter);
        }

        public Document? GetByID(int id)
        {
            return _documentRepository.GetById(id);
        }

        public void Update(Document entity)
        {
            _documentRepository.Update(entity);
        }
    }
}
