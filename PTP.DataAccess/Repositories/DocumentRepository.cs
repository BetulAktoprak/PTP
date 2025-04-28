using PTP.EntityLayer.Models;

namespace PTP.DataAccess.Repositories
{
    public class DocumentRepository : GenericRepository<Document>
    {
        public DocumentRepository(AppDbContext context) : base(context)
        {
        }
    }
}
