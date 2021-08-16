using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using CommentManagement.Application.Contracts.Comment;
using CommentManagement.Domain.CommentAgg;
using Microsoft.EntityFrameworkCore;

namespace CommentManagement.Infrastructure.EfCore.Repository
{
    public class CommentRepository : RepositoryBase<long,Comment> , ICommentRepository
    {
        private readonly CommentContext _context;
        public CommentRepository(CommentContext context) : base(context)
        {
            _context = context;
        }

        public List<CommentViewModel> Search(CommentSearchModel searchModel)
        {
            var query = _context.Comments.Select(x => new CommentViewModel
            {
                Id = x.Id,
                Canceled = x.Canceled,
                CommentDate = x.CreationDate.ToFarsi(),
                Confirmed = x.Confirmed,
                Email = x.Email,
                Message = x.Message,
                Name = x.Name,
             
            });

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(x => x.Name.Contains(searchModel.Name));

            if (!string.IsNullOrWhiteSpace(searchModel.Email))
                query = query.Where(x => x.Email.Contains(searchModel.Email));

            if (searchModel.OwnerRecordId != 0)
                query = query.Where(x => x.OwnerRecordId == searchModel.OwnerRecordId);

            return query.OrderByDescending(x => x.Id).AsNoTracking().ToList();
        }
    }
}
