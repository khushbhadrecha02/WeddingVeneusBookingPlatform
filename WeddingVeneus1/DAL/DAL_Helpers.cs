using System.Data;

namespace WeddingVeneus1.DAL
{
    public abstract class DAL_Helpers
    {
        public static string ConnString = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("myConnectionStrings");
        public abstract void  RejectEntity(int entityId);
        public abstract DataTable SelectUserIDByEntityID(int entityId);
    }
}
