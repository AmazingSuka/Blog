using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Application.Likes.Commands.Delete
{
    public class DeleteLikeCommand : IRequest
    {
        public string LikedElementId { get; private set; }
        public string OwnerId { get; private set; }

        public DeleteLikeCommand(string likedElementId, string ownerId)
        {
            LikedElementId = likedElementId;
            OwnerId = ownerId;
        }
    }
}
