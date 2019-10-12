using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Application.Comments.Commands.Create
{
    public class CreateCommentCommand : IRequest
    {
        public string Content { get; private set; }
        public string AuthorId { get; private set; }
        public string PostId { get; private set; }

        public CreateCommentCommand(string content, string authorId, string postId)
        {
            Content = content;
            AuthorId = authorId;
            PostId = postId;
        }
    }
}
