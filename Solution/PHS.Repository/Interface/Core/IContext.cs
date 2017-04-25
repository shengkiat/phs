
namespace PHS.Repository.Interface.Core
{
    using System;
    using System.Data.Common;
    using System.Data.Entity;

    public interface IContext : IDisposable
    {
        bool IsAuditEnabled { get; set; }
        IDbSet<TEntity> GetEntitySet<TEntity>() where TEntity : class;
        void ChangeState<TEntity>(TEntity entity, EntityState state) where TEntity : class;
        DbTransaction BeginTransaction();
        int Commit();
    }
}
