using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PTP.Business.Abstractions;
using PTP.DataAccess.Repositories;
using PTP.EntityLayer.Models;

namespace PTP.Business.Services
{
    public class CommentService : IService<Comment>
    {
        private readonly CommentRepository _commentRepository;

        public CommentService(CommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public void Add(Comment entity)
        {
            _commentRepository.Add(entity);
        }

        public void Delete(int id)
        {
            _commentRepository.Delete(id);
        }

        public IEnumerable<Comment>? GetAll()
        {
            return _commentRepository.GetAll();
        }

        public IEnumerable<Comment> GetAll(Expression<Func<Comment, bool>>? filter = null)
        {
            return _commentRepository.GetAll(filter);
        }

        public Comment? GetByID(int id)
        {
            return _commentRepository.GetById(id);
        }

        public void Update(Comment entity)
        {
            _commentRepository.Update(entity);
        }
    }
}
