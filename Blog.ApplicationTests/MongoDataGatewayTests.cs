using Blog.Application.Comments.Queries.GetRelated;
using Blog.Application.Likes.Queries.GetRelatedLikes;
using Blog.Domain.Interfaces;
using Blog.Domain.MongoEntities;
using Blog.Persistance;
using Blog.Persistance.DataContext;
using Blog.Persistance.Gateways;
using Blog.Persistance.Interfaces;
using Blog.WebUI.Data;
using MediatR;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Blog.ApplicationTests
{
    public class MongoDataGatewayTests
    {
        public IPost post { get; set; }
        public IComment comment { get; set; }

        public IDataContext GetDataContext()
        {
            IDataContext dataContext = new MongoContext(Options.Create<Settings>(new Settings
            {
                commentCollection = "comments",
                likeCollection = "likes",
                postCollection = "posts",
                database = "blog_test",
                connectionString = "mongodb://localhost:27017"
            }));

            dataContext.CommentGateway.DeleteMany(x => true);
            dataContext.PostGateway.DeleteMany(x => true);
            dataContext.LikeGateway.DeleteMany(x => true);

            return dataContext;
        }

        [Fact]
        public void CreatePostTest()
        {
            IDataContext dataContext = GetDataContext();
            var mockPost = new Mock<IPost>();
            mockPost.Setup(x => x.Id).Returns("5d9ed1b49a5b063be45b4a3z");
            mockPost.Setup(x => x.Title).Returns("Title test");
            mockPost.Setup(x => x.Content).Returns("Test Create Post Content test");
            mockPost.Setup(x => x.ImageFileName).Returns("bbgon");
            mockPost.Setup(x => x.CreatedTimestamp).Returns(DateTime.UtcNow);
            mockPost.Setup(x => x.EditedTimestamp).Returns(DateTime.UtcNow);
            mockPost.Setup(x => x.EditorId).Returns("5d9ed23b9a5b063be45b4a3f");
            mockPost.Setup(x => x.AuthorId).Returns("5d9ed2479a5b063be45b4a40");
            mockPost.Setup(x => x.LikesCollection).Returns(new List<ILike>());
            mockPost.Setup(x => x.CommentsCollection).Returns(new List<IComment>());

            dataContext.PostGateway.Create(mockPost.Object);
            var result = dataContext.PostGateway.Find(x => true);

            Assert.NotEmpty(result);
            Assert.Contains(mockPost.Object, result);
        }

        [Fact]
        public void CreateComment()
        {
            IDataContext dataContext = GetDataContext();
            IComment comment = new Comment
            {
                Id = "5d9cb76cd2372b3431dabc78",
                Content = "Test Create comment content",
                AuthorId = "5d9ed2479a5b063be45b4a40",
                PostId = "5d9ed1b49a5b063be45b4a3z"
            };

        }
    }
}
