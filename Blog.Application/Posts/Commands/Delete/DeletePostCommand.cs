using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Application.Posts.Commands.Delete
{
    public class DeletePostCommand : IRequest
    {
        public string Id { get; private set; }

        public DeletePostCommand(string id)
        {
            Id = id;
        }
    }
}
