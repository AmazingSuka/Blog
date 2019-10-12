using System;
using System.Collections.Generic;
using System.Text;
using Blog.Domain.Interfaces;
using MediatR;

namespace Blog.Application.Comments.Queries.GetRelated
{
    public class GetRelatedCommentsQuery : IRequest<IList<IComment>>
    {
        public string TargetPostId { get; private set; }

        public GetRelatedCommentsQuery(string targetPostId)
        {
            TargetPostId = targetPostId;
        }
    }
}
