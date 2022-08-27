using Bookmarks.Project.Entity;
using Microsoft.EntityFrameworkCore;
using Coddie.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Bookmarks.Project.CoreContext
{
    /// <summary>
    /// Defines the <see cref="BookmarksProjectCoreDbContext"/>.
    /// </summary>
    public class BookmarksProjectCoreDbContext : DbContext
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseOracle("#CONNECTION_STRING#");
        }

        /// <summary>
        /// The OnModelCreating method.
        /// </summary>
        /// <param name="modelBuilder">The modelBuilder<see cref="ModelBuilder"/>.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BookmarksProjectCoreDbContext).Assembly);

            /**
             * Ef Core Entity type 'XXX' has composite primary key defined with data annotations. To set composite primary key, use fluent API.
             * hatasý için sýnýfta birden fazla Key attribute  varsa onlarý tanýmlamak gerekiyor ve bu iþlem aþaðýda yapýlýyor.
             */
            //modelBuilder.Entity<KullaniciKullanicirol>().HasKey(c => new { c.KullaniciFk, c.RolFk });
            //modelBuilder.Entity<KullaniciRoluygulama>().HasKey(c => new { c.UygulamaFk, c.RolFk });


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

            types.ForEach(q => { modelBuilder.Ignore(q); });

            # endregion [ Ignoring Tables Have No Key property ]

            #region [ Mapping Entities To Tables ]
/*

*/
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