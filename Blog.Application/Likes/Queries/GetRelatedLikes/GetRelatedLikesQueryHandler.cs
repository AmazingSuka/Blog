using Blog.Domain.Interfaces;
using Blog.Persistance.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blog.Application.Likes.Queries.GetRelatedLikes
{
    public class GetRelatedLikesQueryHandler : IRequestHandler<GetRelatedLikesQuery, IList<ILike>>
    {
        private readonly IDataContext dataContext;

        public GetRelatedLikesQueryHandler(IDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public Task<IList<ILike>> Handle(GetRelatedLikesQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(dataContext.LikeGateway.Find(record => record.LikedElement == request.TargetElementId));
        }
    }
}
