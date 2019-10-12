using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Application.Comments.Commands.Update
{
    public class UpdateCommentCommand : IRequest
    {
        public string Id { get; private set; }
        public string Content { get; private set; }

        public UpdateCommentCommand(string id, string content)
        {
            Id = id;
            Content = content;
        }
    }
}
