using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Application.Posts.Commands.Create
{
    public class CreatePostCommand : IRequest
    {
        public string Title { get; private set; }
        public string Content { get; private set; }
        public string ImageFileName { get; private set; }
        public string AuthorId { get; private set; }

        public CreatePostCommand(string title, string content, string imageFileName, string authorId)
        {
            Title = title;
            Content = content;
            ImageFileName = imageFileName;
            AuthorId = authorId;
        }
    }
}
