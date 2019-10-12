using Blog.Application.Likes.Commands.Create;
using Blog.Application.Likes.Commands.Delete;
using Blog.Domain.Interfaces;
using Blog.WebUI.TransferModels.LikeModels;
using MediatR;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.WebUI.Data
{
    public class LikeService
    {
        private readonly IMediator mediator;
        public LikeService(IMediator mediator)
        {
            this.mediator = mediator;
        }
        
        public async Task AppendLikeToPost(CreateLikeTransferModel model)
        {
            await mediator.Send(new CreateLikeCommand(model.TargetElementId, model.OwnerId, LikableType.Post));
        }

        public async Task AppendLikeToComment(CreateLikeTransferModel model)
        {
            await mediator.Send(new CreateLikeCommand(model.TargetElementId, model.OwnerId, LikableType.Comment));
        }

        public async Task DeleteLike(DeleteLikeTransferModel model)
        {
            await mediator.Send(new DeleteLikeCommand(model.TargetElementId, model.OwnerId));
        }
    }
}
