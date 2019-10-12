using Blog.Domain.Interfaces;
using Blog.Persistance.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blog.Application.Posts.Commands.Update
{
    public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand, Unit>
    {
        private IDataContext dataContext;
        public UpdatePostCommandHandler(IDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public Task<Unit> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
        {
            IPost targetPost = GetPost(request.Id);
            ConfigurePostProperties(targetPost, request);
            dataContext.PostGateway.Update(targetPost.Id, targetPost);

            return Unit.Task;
        }

        private IPost GetPost(string id)
        {
            return dataContext.PostGateway.FindById(id);
        }

        private void ConfigurePostProperties(IPost post, UpdatePostCommand request)
        {
            post.Title = request.Title;
            post.Content = request.Content;
            post.ImageFileName = request.ImageFileName;
            post.EditorId = request.EditorId;
            post.EditedTimestamp = DateTime.UtcNow;
        }
    }
}
