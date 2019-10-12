using Blog.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Application.Posts.Queries.GetAll
{
    public class GetAllPostsQuery : IRequest<IList<IPost>>
    {
        public PostQuerySettings Settings { get; private set; }

        public GetAllPostsQuery(PostQuerySettings settings)
        {
            Settings = settings;
        }
    }
}
