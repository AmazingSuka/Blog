namespace Blog.Application.Posts.Commands.Delete
{
    using Blog.Domain.Interfaces;
    using Blog.Persistance.Interfaces;
    using MediatR;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Transactions;

    public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, Unit>
    {
        private readonly IDataContext dataContext;

        public DeletePostCommandHandler(IDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public Task<Unit> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            IPost targetPost = dataContext.PostGateway.FindById(request.Id);
            DeletePostTransaction(targetPost.Id);

            return Unit.Task;
        }

        private void DeletePostTransaction(string postId)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                DeleteRelatedEntities(postId);
                DeletePost(postId);
                scope.Complete();
            }
        }

        /// <summary>
        /// Delete related entities.
        /// </summary>
        /// <param name="postId">Post primary field <see cref="string"/></param>
        private void DeleteRelatedEntities(string postId)
        {
            DeleteRelatedLikes(postId);
            DeleteRelatedCommentsLikes(GetRelatedComments(postId));
            DeleteRelatedComments(postId);
        }

        /// <summary>
        /// Delete all related Comment`s likes from database
        /// </summary>
        /// <param name="comments">The comments collection <see cref="IEnumerable{IComment}"/></param>
        private void DeleteRelatedCommentsLikes(IEnumerable<IComment> comments)
        {
            foreach (IComment comment in comments)
            {
                DeleteRelatedLikes(comment.Id);
            }
        }

        /// <summary>
        /// Get all related Comments records from database.
        /// </summary>
        /// <param name="postId">Post primary field <see cref="string"/></param>
        /// <returns>The <see cref="IEnumerable{IComment}"/></returns>
        private IEnumerable<IComment> GetRelatedComments(string postId)
        {
            return dataContext.CommentGateway.Find(record => record.PostId == postId);
        }

        /// <summary>
        /// Delete all related comments records from database.
        /// </summary>
        /// <param name="postId">Post primary field <see cref="string"/></param>
        private void DeleteRelatedComments(string postId)
        {
            dataContext.CommentGateway.DeleteMany(record => record.PostId == postId);
        }

        /// <summary>
        /// Delete all related likes records from database.
        /// </summary>
        /// <param name="targetEntityId">Likable Entity primary field <see cref="string"/></param>
        private void DeleteRelatedLikes(string targetEntityId)
        {
            dataContext.LikeGateway.DeleteMany(record => record.LikedElement == targetEntityId);
        }

        /// <summary>
        /// Delete post from database.
        /// </summary>
        /// <param name="postId">Post primary field <see cref="string"/></param>
        private void DeletePost(string postId)
        {
            dataContext.PostGateway.Delete(postId);
        }
    }
}
