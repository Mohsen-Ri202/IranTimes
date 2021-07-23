using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewShop
{
   public interface ICommentRepository
    {
        public List<Comment> GetCommentById(int id);
        public void AddComment(Comment comment);
        public void Save();
    }
}
