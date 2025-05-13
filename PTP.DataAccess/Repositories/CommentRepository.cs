using PTP.EntityLayer.Models;

namespace PTP.DataAccess.Repositories
{
    public class CommentRepository : GenericRepository<Comment>
    {
        public CommentRepository(AppDbContext context) : base(context)
        {
        }
    }
}
