namespace Blog.Persistance.DataContext
{
    using Blog.Domain.Interfaces;
    using Blog.Domain.MongoEntities;
    using Blog.Persistance.Gateways;
    using Blog.Persistance.Interfaces;
    using Microsoft.Extensions.Options;
    using MongoDB.Bson.Serialization;
    using MongoDB.Bson.Serialization.Serializers;
    using MongoDB.Driver;

    /// <summary>
    /// Defines the <see cref="MongoContext" />
    /// </summary>
    public class MongoContext : IDataContext
    {
        /// <summary>
        /// Defines the database
        /// </summary>
        private readonly IMongoDatabase database;

        /// <summary>
        /// Defines the mongoSettings
        /// </summary>
        private readonly IOptions<Settings> mongoSettings;

        /// <summary>
        /// Gets the PostGateway
        /// </summary>
        public ITableDataGateway<IPost> PostGateway => GetTableDataGateway<IPost>(database, mongoSettings.Value.postCollection);

        /// <summary>
        /// Gets the CommentGateway
        /// </summary>
        public ITableDataGateway<IComment> CommentGateway => GetTableDataGateway<IComment>(database, mongoSettings.Value.commentCollection);

        /// <summary>
        /// Gets the LikeGateway
        /// </summary>
        public ITableDataGateway<ILike> LikeGateway => GetTableDataGateway<ILike>(database, mongoSettings.Value.likeCollection);

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoContext"/> class.
        /// </summary>
        /// <param name="mongoSettings">The mongoSettings<see cref="IOptions{Settings}"/></param>
        public MongoContext(IOptions<Settings> mongoSettings)
        {
            RegistrateInterfacesSerializers();
            database = new MongoClient(mongoSettings.Value.connectionString).GetDatabase(mongoSettings.Value.database);
            this.mongoSettings = mongoSettings;
            RegistrateClassesToMapping();
        }

        /// <summary>
        /// The GetTableDataGateway
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="database">The database<see cref="IMongoDatabase"/></param>
        /// <param name="collectionName">The collectionName<see cref="string"/></param>
        /// <returns>The <see cref="ITableDataGateway{TEntity}"/></returns>
        private ITableDataGateway<TEntity> GetTableDataGateway<TEntity>(IMongoDatabase database, string collectionName)
        {
            return new MongoTableDataGateway<TEntity>(GetMongoCollection<TEntity>(database, collectionName));
        }

        /// <summary>
        /// The GetMongoCollection
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="database">The database<see cref="IMongoDatabase"/></param>
        /// <param name="collectionName">The collectionName<see cref="string"/></param>
        /// <returns>The <see cref="IMongoCollection{TEntity}"/></returns>
        private IMongoCollection<TEntity> GetMongoCollection<TEntity>(IMongoDatabase database, string collectionName)
        {
            return database.GetCollection<TEntity>(collectionName);
        }

        private void RegistrateClassesToMapping()
        {
            BsonClassMap.RegisterClassMap<Post>();
            BsonClassMap.RegisterClassMap<Like>();
            BsonClassMap.RegisterClassMap<Comment>();
        }

        private void RegistrateInterfacesSerializers()
        {
            BsonSerializer.RegisterSerializer<IPost>(new ImpliedImplementationInterfaceSerializer<IPost, Post>());
            BsonSerializer.RegisterSerializer<ILike>(new ImpliedImplementationInterfaceSerializer<ILike, Like>());
            BsonSerializer.RegisterSerializer<IComment>(new ImpliedImplementationInterfaceSerializer<IComment, Comment>());
        }
    }
}
