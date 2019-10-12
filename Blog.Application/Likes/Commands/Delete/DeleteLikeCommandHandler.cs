using Blog.Domain.Interfaces;
using Blog.Persistance.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blog.Application.Likes.Commands.Delete
{
    public class DeleteLikeCommandHandler : IRequestHandler<DeleteLikeCommand, Unit>
    {
        private readonly IDataContext dataContext;

        public DeleteLikeCommandHandler(IDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public Task<Unit> Handle(DeleteLikeCommand request, CancellationToken cancellationToken)
        {
            ILike targetLike = FindTargetLike(request.LikedElementId, request.OwnerId);
            dataContext.LikeGateway.Delete(targetLike.Id);

            return Unit.Task;
        }

        private ILike FindTargetLike(string targetElementId, string ownerId)
        {
            return dataContext.LikeGateway.Single(record => record.LikedElement == targetElementId && record.OwnerId == ownerId);
        }
    }
}
