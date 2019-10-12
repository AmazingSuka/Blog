using Blog.Application.Posts.Commands.Create;
using Blog.Application.Posts.Commands.Delete;
using Blog.Application.Posts.Commands.Update;
using Blog.Application.Posts.Queries;
using Blog.Application.Posts.Queries.GetAll;
using Blog.Application.Posts.Queries.GetSingle;
using Blog.Domain.Interfaces;
using Blog.WebUI.TransferModels.PostModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.WebUI.Data
{
    public class PostService
    {
        private readonly IMediator mediator;
        public PostService(IMediator mediator)
        {
            this.mediator = mediator;
        }

        #region Commands
        public async Task CreatePost(CreatePostTransferModel model)
        {
            await mediator.Send(new CreatePostCommand(model.Title, model.Content, model.ImageFileName, model.AuthorId));
        }

        public async Task UpdatePost(UpdatePostTransferModel model)
        {
            await mediator.Send(new UpdatePostCommand(model.Id, model.Title, model.Content, model.ImageFileName, model.EditorId));
        }

        public async Task DeletePost(DeletePostTransferModel model)
        {
            await mediator.Send(new DeletePostCommand(model.Id));
        }
        #endregion

        #region Queries
        public async Task<IPost> GetSinglePost(string postId, PostQuerySettings settings)
        {
            return await mediator.Send(new GetSinglePostQuery(postId, settings));
        }

        public async Task<IList<IPost>> GetAllPostsAsync(PostQuerySettings settings)
        {
            return await mediator.Send(new GetAllPostsQuery(settings));
        }
        #endregion
    }
}
