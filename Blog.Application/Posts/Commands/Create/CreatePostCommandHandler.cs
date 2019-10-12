using Blog.Domain.Interfaces;
using Blog.Persistance.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blog.Application.Posts.Commands.Create
{
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, Unit>
    {
        private readonly IDataContext dataContext;
        // Зарегистрировать это в DI и получать через конструктор
        // Для убирания зависимостей MongoEntities.
        // Получаем пустые и заполняем с Request и выполняем все нужное в Handler
        private IPost postToCreate; 
        public CreatePostCommandHandler(IDataContext dataContext, IPost post)
        {
            this.dataContext = dataContext;
            postToCreate = post;
        }
        public Task<Unit> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            MapPostProperties(request);
            dataContext.PostGateway.Create(postToCreate);

            return Unit.Task;
        }

        private void MapPostProperties(CreatePostCommand request)
        {
            postToCreate.Title = request.Title;
            postToCreate.Content = request.Content;
            postToCreate.ImageFileName = request.ImageFileName;
            postToCreate.AuthorId = request.AuthorId;
            postToCreate.CreatedTimestamp = DateTime.UtcNow;
        }
    }
}
