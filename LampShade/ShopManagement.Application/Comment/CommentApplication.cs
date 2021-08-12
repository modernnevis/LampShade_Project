using System.Collections.Generic;
using _0_Framework.Application;
using ShopManagement.Application.Contracts.Comment;
using ShopManagement.Domain.CommentAgg;

namespace ShopManagement.Application.Comment
{
    public class CommentApplication : ICommentApplication
    {
        private readonly ICommentRepository _commentRepository;

        public CommentApplication(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public OperationResult Create(AddComment command)
        {
            var operationResult = new OperationResult();
            var comment = new Domain.CommentAgg.Comment(command.Name, command.Email, command.Message,command.ProductId);

            _commentRepository.Create(comment);
            _commentRepository.SaveChanges();
            return operationResult.Succeeded();
        }

        public OperationResult Confirm(long id)
        {
            var operationResult = new OperationResult();

            var comment = _commentRepository.Get(id);
            if (comment == null)
                return operationResult.Failed(ApplicationMessages.NotFound);

            comment.Confirm();
            _commentRepository.SaveChanges();
            return operationResult.Succeeded();

        }

        public OperationResult Cancel(long id)
        {
            var operationResult = new OperationResult();

            var comment = _commentRepository.Get(id);
            if (comment == null)
                return operationResult.Failed(ApplicationMessages.NotFound);

            comment.Cancel();
            _commentRepository.SaveChanges();
            return operationResult.Succeeded();

        }

        public List<CommentViewModel> Search(CommentSearchModel searchModel)
        {
            return _commentRepository.Search(searchModel);
        }
    }
}
