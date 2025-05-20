using Microsoft.EntityFrameworkCore;
using PTP.DataAccess.Abstractions;
using PTP.EntityLayer.Models;

namespace PTP.DataAccess.Repositories
{
    public class DocumentRepository : GenericRepository<Document>, IDocumentRepository
    {
        private readonly AppDbContext _context;
        public DocumentRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public List<Document> GetDocumentsByProjectId(int projectId)
        {
            return _context.Documents
                   .Where(d => d.ProjectId == projectId)
                   .ToList();
        }
    }
}
