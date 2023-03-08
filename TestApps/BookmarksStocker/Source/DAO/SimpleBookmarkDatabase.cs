using Mst.DexterCfg.Factory;
using Simply.Data.Database;
using System.Data.SqlServerCe;

namespace BookmarksStocker.DAO
{
    public class SimpleBookmarkDatabase : SimpleDatabase
    {
        public SimpleBookmarkDatabase() : base(Create<SqlCeConnection>(DxCfgConnectionFactory.Instance["connection-string"]))
        {
        }
    }
}