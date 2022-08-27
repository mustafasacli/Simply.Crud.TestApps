using Bookmarks.Project.Entity;
using SimpleFileLogging.Enums;
using SimpleFileLogging.Interfaces;
using Coddie.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;

namespace Bookmarks.Project.Context
{
    /// <summary>
    /// Defines the <see cref="BookmarksProjectDbContext"/>.
    /// </summary>
    public class BookmarksProjectDbContext : DbContext
    {
        /// <summary>
        /// Defines the logger.
        /// </summary>
        private readonly ISimpleLogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookmarksProjectDbContext"/> class.
        /// </summary>
        public BookmarksProjectDbContext() : base($"name={nameof(BookmarksProjectDbContext)}")
        {
            Database.SetInitializer<BookmarksProjectDbContext>(null);
            logger =
            SimpleFileLogging.SimpleFileLogger.Instance;
            logger.LogDateFormatType = SimpleLogDateFormats.None;
            this.Database.Log = LogQueries;
        }

        /// <summary>
        /// The LogQueries
        /// </summary>
        /// <param name="q">The q <see cref="string"/>.</param>
        protected void LogQueries(string q)
        {
            logger?.Debug(q);
        }

        /// <summary>
        /// The OnModelCreating method.
        /// </summary>
        /// <param name="modelBuilder">The modelBuilder<see cref="DbModelBuilder"/>.</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("");
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());

            #region [ Ignoring Tables Have No Key property ]

            var types = new List<Type>();

            this.GetType()
            .GetRuntimeProperties()
            .Where(o =>
                o.PropertyType.IsGenericType &&
                o.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>) &&
                o.PropertyType.GenericTypeArguments.Count() > 0)
                .Select(q => q.PropertyType.GenericTypeArguments)
                .ToList()
                .ForEach(q =>
                {
                    types.AddRange(q);
                });
            types = types.Distinct().ToList();
            types =
            types.Where(q => q.GetKeysOfType().Count() < 1)
            .ToList() ?? new List<Type>();
            modelBuilder.Ignore(types);

            # endregion [ Ignoring Tables Have No Key property ]

            #region [ Mapping Entities To Tables ]



            # endregion [ Mapping Entities To Tables ]
        }

        #region [ Tables List ]

        /// <summary>
        /// Gets or sets the Bookmarks.
        /// </summary>
        public virtual DbSet<Bookmarks> Bookmarks
        { get; set; }

        /// <summary>
        /// Gets or sets the Browsers.
        /// </summary>
        public virtual DbSet<Browsers> Browsers
        { get; set; }

        #endregion [ Tables List ]
    }
}