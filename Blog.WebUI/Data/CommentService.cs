using Blog.Application.Comments.Commands.Create;
using Blog.Application.Comments.Commands.Delete;
using Blog.Application.Comments.Commands.Update;
using Blog.Application.Likes.Commands.Create;
using Blog.WebUI.TransferModels.CommentModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.WebUI.Data
{
    public class CommentService
    {
        private readonly IMediator mediator;

        public CommentService(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task CreateComment(CreateCommentTransferModel model)
        {
            await mediator.Send(new CreateCommentCommand(model.Content, model.AuthorId, model.PostId));
        }

        public async Task UpdateComment(UpdateCommentTransferModel model)
        {
            await mediator.Send(new UpdateCommentCommand(model.Id, model.Content));
        }

        public async Task DeleteComment(DeleteCommentTransferModel model)
        {
            await mediator.Send(new DeleteCommentCommand(model.Id));
        }
    }
}
