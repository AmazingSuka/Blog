using Blog.Application.Comments.Queries.GetRelated;
using Blog.Application.Likes.Queries.GetRelatedLikes;
using Blog.Domain.Interfaces;
using Blog.Persistance.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Blog.Application.Posts.Queries.GetAll
{
    public class GetAllPostsQueryHandler : IRequestHandler<GetAllPostsQuery, IList<IPost>>
    {
        private readonly IDataContext dataContext;
        private readonly IMediator mediator;

        public GetAllPostsQueryHandler(IDataContext dataContext, IMediator mediator)
        {
            this.dataContext = dataContext;
            this.mediator = mediator;
        }

        public async Task<IList<IPost>> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
        {
            return await GetPosts(request.Settings);
        }

        private async Task<IList<IPost>> GetPosts(PostQuerySettings settings)
        {
            IList<IPost> posts = dataContext.PostGateway.Find(x => true);

            switch (settings)
            {
                case PostQuerySettings.Empty:
                    return posts;
                case PostQuerySettings.Full:
                    foreach (IPost post in posts)
                    {
                        post.LikesCollection = await LoadRelatedLikesAsync(post.Id);
                        post.CommentsCollection = await LoadRelatedCommentsAsync(post.Id);
                    }
                    break;
                case PostQuerySettings.WithLikes:
                    foreach (IPost post in posts)
                    {
                        post.LikesCollection = await LoadRelatedLikesAsync(post.Id);
                    }
                    break;
                default:
                    break;
            }

            return posts;
        }

        private async Task<IList<IComment>> LoadRelatedCommentsAsync(string postId)
        {
            return await mediator.Send(new GetRelatedCommentsQuery(postId));
        }

        private async Task<IList<ILike>> LoadRelatedLikesAsync(string postId)
        {
            return await mediator.Send(new GetRelatedLikesQuery(postId));
        }
    }
}
