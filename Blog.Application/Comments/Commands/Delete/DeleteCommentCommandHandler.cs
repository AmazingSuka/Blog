using Blog.Domain.Interfaces;
using Blog.Persistance.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace Blog.Application.Comments.Commands.Delete
{
    public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, Unit>
    {
        private readonly IDataContext dataContext;

        public DeleteCommentCommandHandler(IDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public Task<Unit> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            IComment targetComment = GetComment(request.Id);
            DeleteCommentTransaction(targetComment.Id);

            return Unit.Task;
        }

        private void DeleteCommentTransaction(string id)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                DeleteRelatedLikes(id);
                DeleteComment(id);
                scope.Complete();
            }
        }

        private void DeleteComment(string id)
        {
            dataContext.CommentGateway.Delete(id);
        }

        private void DeleteRelatedLikes(string id)
        {
            dataContext.LikeGateway.DeleteMany(record => record.LikedElement == id);
        }

        private IComment GetComment(string id)
        {
            return dataContext.CommentGateway.FindById(id);
        }
    }
}
