using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Domain.Interfaces
{
    public interface IPost
    {
        string Id { get; set; }
        string Title { get; set; }
        string Content { get; set; }
        string ImageFileName { get; set; }
        DateTime CreatedTimestamp { get; set; }
        DateTime EditedTimestamp { get; set; }
        string EditorId { get; set; }
        string AuthorId { get; set; }

        IList<ILike> LikesCollection { get; set; }
        IList<IComment> CommentsCollection { get; set; }
    }
}
