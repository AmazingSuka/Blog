using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Application.Posts.Commands.Update
{
    public class UpdatePostCommand : IRequest
    {
        public string Id { get; private set; }
        public string Title { get; private set; }
        public string Content { get; private set; }
        public string ImageFileName { get; private set; }
        public string EditorId { get; private set; }

        public UpdatePostCommand(string id, string title, string content, string imageFileName, string editorId)
        {
            Id = id;
            Title = title;
            Content = content;
            ImageFileName = imageFileName;
            EditorId = editorId;
        }
    }
}
