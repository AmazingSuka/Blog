using Blog.Application.Likes.Queries.GetRelatedLikes;
using Blog.Domain.Interfaces;
using Blog.Persistance.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blog.Application.Comments.Queries.GetRelated
{
    public class GetRelatedCommentsQueryHandler : IRequestHandler<GetRelatedCommentsQuery, IList<IComment>>
    {
        private readonly IDataContext dataContext;
        private readonly IMediator mediator;

        public GetRelatedCommentsQueryHandler(IDataContext dataContext, IMediator mediator)
        {
            this.dataContext = dataContext;
            this.mediator = mediator;
        }

        public async Task<IList<IComment>> Handle(GetRelatedCommentsQuery request, CancellationToken cancellationToken)
        {
            return await GetComments(request.TargetPostId);
        }

        private async Task<IList<IComment>> GetComments(string postId)
        {
            IList<IComment> comments = dataContext.CommentGateway.Find(record => record.PostId == postId);
            
            foreach (IComment comment in comments)
            {
                comment.LikesCollection = await LoadRelatedLikesAsync(comment.Id);
            }

            return comments;
        }

        public async Task<IList<ILike>> LoadRelatedLikesAsync(string commentId)
        {
            return await mediator.Send(new GetRelatedLikesQuery(commentId));
        }
    }
}
