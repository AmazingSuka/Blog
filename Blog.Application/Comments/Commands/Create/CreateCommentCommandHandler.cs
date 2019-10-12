using Blog.Domain.Interfaces;
using Blog.Persistance.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blog.Application.Comments.Commands.Create
{
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, Unit>
    {
        private readonly IDataContext dataConext;
        private readonly IComment commentToCreate;

        public CreateCommentCommandHandler(IDataContext dataConext, IComment comment)
        {
            this.dataConext = dataConext;
            commentToCreate = comment;
        }

        public Task<Unit> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            FindTargetPost(request.PostId);
            MapCommentProperties(request);
            dataConext.CommentGateway.Create(commentToCreate);

            return Unit.Task;
        }

        private void MapCommentProperties(CreateCommentCommand request)
        {
            commentToCreate.Content = request.Content;
            commentToCreate.AuthorId = request.AuthorId;
            commentToCreate.PostId = request.PostId;
        }

        private void FindTargetPost(string postId)
        {
            dataConext.PostGateway.FindById(postId);
        }
    }
}
