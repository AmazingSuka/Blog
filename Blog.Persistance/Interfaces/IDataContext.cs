using Blog.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Persistance.Interfaces
{
    public interface IDataContext
    {
        ITableDataGateway<IPost> PostGateway { get; }
        ITableDataGateway<IComment> CommentGateway { get; }
        ITableDataGateway<ILike> LikeGateway { get; }
    }
}
