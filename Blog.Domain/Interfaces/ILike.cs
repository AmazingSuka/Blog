using System.Collections;

namespace Blog.Domain.Interfaces
{
    public interface ILike
    {
        string Id { get; set; }
        string LikedElement { get; set; }
        string OwnerId { get; set; }
    }
}