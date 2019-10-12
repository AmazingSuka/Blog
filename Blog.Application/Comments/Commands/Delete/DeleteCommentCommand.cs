using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Application.Comments.Commands.Delete
{
    public class DeleteCommentCommand : IRequest
    {
        public string Id { get; private set; }

        public DeleteCommentCommand(string id)
        {
            Id = id;
        }
    }
}
