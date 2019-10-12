using Blog.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Application.Likes.Queries.GetRelatedLikes
{
    public class GetRelatedLikesQuery : IRequest<IList<ILike>>
    {
        public string TargetElementId { get; private set; }

        public GetRelatedLikesQuery(string targetElementId)
        {
            this.TargetElementId = targetElementId;
        }
    }
}
