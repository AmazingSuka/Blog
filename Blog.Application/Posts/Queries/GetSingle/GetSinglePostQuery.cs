using Blog.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Application.Posts.Queries.GetSingle
{
    public class GetSinglePostQuery : IRequest<IPost>
    {
        public string Id { get; private set; }
        public PostQuerySettings Settings { get; private set; }

        public GetSinglePostQuery(string id, PostQuerySettings settings = PostQuerySettings.Empty)
        {
            Id = id;
            Settings = settings;
        }
    }
}
