using Blog.Application.Comments.Queries;
using Blog.Application.Comments.Queries.GetRelated;
using Blog.Application.Likes.Queries.GetRelatedLikes;
using Blog.Domain.Interfaces;
using Blog.Persistance.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Blog.Application.Posts.Queries.GetSingle
{
    public class GetSinglePostQueryHandler : IRequestHandler<GetSinglePostQuery, IPost>
    {
        private readonly IDataContext dataContext;
        private readonly IMediator mediator;

        public GetSinglePostQueryHandler(IDataContext dataContext, IMediator mediator)
        {
            this.dataContext = dataContext;
            this.mediator = mediator;
        }

        public async Task<IPost> Handle(GetSinglePostQuery request, CancellationToken cancellationToken)
        {
            return await GetPostAsync(request.Id, request.Settings);
        }

        private async Task<IPost> GetPostAsync(string postId, PostQuerySettings settings)
        {
            IPost post = dataContext.PostGateway.FindById(postId);

            switch (settings)
            {
                case PostQuerySettings.Empty:
                    break;
                case PostQuerySettings.Full:
                    post.LikesCollection = await LoadRelatedLikesAsync(post.Id);
                    post.CommentsCollection = await LoadRelatedCommentsAsync(post.Id);
                    break;
                case PostQuerySettings.WithLikes:
                    post.LikesCollection = await LoadRelatedLikesAsync(post.Id);
                    break;
                default:
                    break;
            }

            return post;
        }

        private async Task<IList<IComment>> LoadRelatedCommentsAsync(string targetPostId)
        {
            return await mediator.Send(new GetRelatedCommentsQuery(targetPostId));
        }

        private async Task<IList<ILike>> LoadRelatedLikesAsync(string targetId)
        {
            return await mediator.Send(new GetRelatedLikesQuery(targetId));
        }
    }
}
