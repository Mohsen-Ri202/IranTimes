using NewShop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IranTimes
{
    public class CommentRepository : ICommentRepository
    {
        private NewCmsContext _Context;
        public CommentRepository(NewCmsContext context)
        {
            _Context = context;
        }

        public void AddComment(Comment comment)
        {
            _Context.Comments.Add(comment);
        }

        public List<Comment> GetCommentById(int id)
        {
            var result= _Context.Comments.Where(s => s.PageID == id).ToList();
            return result;
        }

        public void Save()
        {
            _Context.SaveChanges();
        }
    }
}
