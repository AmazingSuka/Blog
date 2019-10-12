using Blog.Domain.Interfaces;
using Blog.Persistance.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blog.Application.Comments.Commands.Update
{
    public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand, Unit>
    {
        private readonly IDataContext dataContext;

        public UpdateCommentCommandHandler(IDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public Task<Unit> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
        {
            IComment targetComment = GetComment(request.Id);
            ConfigureCommentProperties(targetComment, request);
            dataContext.CommentGateway.Update(targetComment.Id, targetComment);

            return Unit.Task;
        }

        private IComment GetComment(string id)
        {
            return dataContext.CommentGateway.FindById(id);
        }

        private void ConfigureCommentProperties(IComment comment, UpdateCommentCommand request)
        {
            comment.Content = request.Content;
        }
    }
}
