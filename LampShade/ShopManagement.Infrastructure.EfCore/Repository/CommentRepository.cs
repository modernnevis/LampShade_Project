using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.Comment;
using ShopManagement.Domain.CommentAgg;

namespace ShopManagement.Infrastructure.EfCore.Repository
{
    public class CommentRepository : RepositoryBase<long,Comment> , ICommentRepository
    {
        private readonly ShopContext _context;
        public CommentRepository(ShopContext context) : base(context)
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
                Product = x.Product.Name,
                ProductId = x.ProductId,
            });

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(x => x.Name.Contains(searchModel.Name));

            if (!string.IsNullOrWhiteSpace(searchModel.Email))
                query = query.Where(x => x.Email.Contains(searchModel.Email));

            if (searchModel.ProductId != 0)
                query = query.Where(x => x.ProductId == searchModel.ProductId);

            return query.OrderByDescending(x => x.Id).AsNoTracking().ToList();
        }
    }
}
