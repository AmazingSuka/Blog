using Blog.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Application.Likes.Commands.Create
{
    public class CreateLikeCommand : IRequest
    {
        public string TargetElementId { get; private set; }
        public LikableType TargetElementType { get; private set; }
        public string OwnerId { get; private set; }

        public CreateLikeCommand(string id, string ownerId, LikableType targetElementType)
        {
            OwnerId = ownerId;
            TargetElementId = id;
            TargetElementType = targetElementType;
        }
    }
}
