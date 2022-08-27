using Mst.DexterCfg.Factory;
using SimpleFileLogging;
using SimpleFileLogging.Enums;
using SimpleFileLogging.Interfaces;
using SimpleInfra.Mapping;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlServerCe;

namespace BookmarksStocker.Source
{
    /// <summary>
    /// Simple Base Business class.
    /// </summary>
    public abstract class SimpleBaseBusiness
    {
        protected IDbConnection GetDbConnection()
        {
            string connectionString = DxCfgConnectionFactory.Instance["connection-string"];
            // return new SqlCeConnection("Password=mybookmarks; Data Source=C:/FreeORM/Bookmarks/Bookmarks.sdf;");
            // return new SqlCeConnection("Password=mybookmarks; Data Source=../../DataSources/Bookmarks/Bookmarks.sdf;");
            return new SqlCeConnection(connectionString);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleBaseBusiness"/> class.
        /// </summary>
        protected SimpleBaseBusiness()
        {
            Logger = SimpleFileLogger.Instance;
            Logger.LogDateFormatType = SimpleLogDateFormats.Hour;
            DayLogger = SimpleFileLogger.Instance;
            DayLogger.LogDateFormatType = SimpleLogDateFormats.Hour;
        }

        /// <summary>
        /// Gets Logger instance.
        /// </summary>
        public ISimpleLogger Logger
        { get; protected set; }

        public ISimpleLogger DayLogger
        { get; protected set; }

        /// <summary>
        /// Maps entity to data transfer object.
        /// </summary>
        /// <typeparam name="TEntity">.</typeparam>
        /// <typeparam name="TDto">.</typeparam>
        /// <param name="entity">.</param>
        /// <returns>.</returns>
        protected TDto Map<TEntity, TDto>(TEntity entity)
            where TDto : class, new()
            where TEntity : class
        {
            var dto = SimpleMapper.Map<TEntity, TDto>(entity);
            return dto;
        }

        /// <summary>
        /// Maps data transfer object to entity.
        /// </summary>
        /// <typeparam name="TDto">.</typeparam>
        /// <typeparam name="TEntity">.</typeparam>
        /// <param name="dto">.</param>
        /// <returns>.</returns>
        protected TEntity MapReverse<TDto, TEntity>(TDto dto)
            where TDto : class
            where TEntity : class, new()
        {
            var entity = SimpleMapper.Map<TDto, TEntity>(dto);
            return entity;
        }

        /// <summary>
        /// Maps entity to data transfer object.
        /// </summary>
        /// <typeparam name="TDto">.</typeparam>
        /// <typeparam name="TEntity">.</typeparam>
        /// <param name="entity">.</param>
        /// <param name="dto">.</param>
        protected void MapTo<TDto, TEntity>(TEntity entity, TDto dto)
            where TDto : class
            where TEntity : class
        {
            SimpleMapper.MapTo(entity, dto);
        }

        /// <summary>
        /// Maps data transfer object to entity.
        /// </summary>
        /// <typeparam name="TDto">.</typeparam>
        /// <typeparam name="TEntity">.</typeparam>
        /// <param name="dto">.</param>
        /// <param name="entity">.</param>
        protected void MapToReverse<TDto, TEntity>(TDto dto, TEntity entity)
            where TDto : class
            where TEntity : class
        {
            SimpleMapper.MapTo(dto, entity);
        }

        /// <summary>
        /// Maps entity list to data transfer object list.
        /// </summary>
        /// <typeparam name="TEntity">.</typeparam>
        /// <typeparam name="TDto">.</typeparam>
        /// <param name="entityList">.</param>
        /// <returns>.</returns>
        protected List<TDto> MapList<TEntity, TDto>(List<TEntity> entityList)
            where TDto : class, new()
            where TEntity : class
        {
            var dtos = SimpleMapper.MapList<TEntity, TDto>(entityList);
            return dtos;
        }

        /// <summary>
        /// Maps data transfer object list to entity list.
        /// </summary>
        /// <typeparam name="TDto">.</typeparam>
        /// <typeparam name="TEntity">.</typeparam>
        /// <param name="entityList">.</param>
        /// <returns>.</returns>
        protected List<TEntity> MapListReverse<TDto, TEntity>(List<TDto> entityList)
            where TDto : class
            where TEntity : class, new()
        {
            var entities = SimpleMapper.MapList<TDto, TEntity>(entityList);
            return entities;
        }

        /// <summary>
        /// Defines the disposed.
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// The Dispose.
        /// </summary>
        /// <param name="disposing">.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
            }

            disposed = true;
        }

        /// <summary>
        /// Disposes object.
        /// </summary>
        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}