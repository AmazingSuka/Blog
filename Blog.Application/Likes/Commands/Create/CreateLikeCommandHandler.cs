using Blog.Domain.Interfaces;
using Blog.Persistance.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blog.Application.Likes.Commands.Create
{
    public class CreateLikeCommandHandler : IRequestHandler<CreateLikeCommand, Unit>
    {
        private readonly IDataContext dataContext;
        private readonly ILike likeToCreate;

        public CreateLikeCommandHandler(IDataContext dataContext, ILike like)
        {
            this.dataContext = dataContext;
            likeToCreate = like;
        }

        public Task<Unit> Handle(CreateLikeCommand request, CancellationToken cancellationToken)
        {
            FindTargetEntity(request.TargetElementId, request.TargetElementType);
            MapLikePropetries(request.TargetElementId, request.OwnerId);
            dataContext.LikeGateway.Create(likeToCreate);

            return Unit.Task;
        }

        private void MapLikePropetries(string targetElementId, string ownerId)
        {
            likeToCreate.LikedElement = targetElementId;
            likeToCreate.OwnerId = ownerId;
        }

        private void FindTargetEntity(string targetElementId, LikableType type)
        {
            switch (type)
            {
                case LikableType.Comment:
                    FindComment(targetElementId);
                    break;
                case LikableType.Post:
                    FindPost(targetElementId);
                    break;
                default:
                    throw new ArgumentException("Wrong type to operation.");
            }
        }

        private void FindPost(string id)
        {
            dataContext.PostGateway.FindById(id);
        }

        private void FindComment(string id)
        {
            dataContext.CommentGateway.FindById(id);
        }
    }
}
