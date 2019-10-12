using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Domain.Interfaces
{
    // В классе сущности создать методы подгрузки Лайков или Комментов и удаления их
    // Выполняя это только через запросы к медиаторам.
    // К примеру IComment: 
    /* 
     public Task DeleteRelatedLikes(IMediator mediator) 
     {
        await mediator.Send(new DeleteLikesCommand(this.Id))
     }
    */       
    public interface IComment
    {
        string Id { get; set; }
        string Content { get; set; }
        string AuthorId { get; set; }
        string PostId { get; set; }
        IList<ILike> LikesCollection { get; set; }
    }
}
