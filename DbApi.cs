using GTRC_Database_Client;

namespace GTRC_Web_Backend
{
    public class DbApi
    {
        public static readonly DbApiConnectionConfig Connection;

        static DbApi()
        {
            DbApiConnectionConfig.LoadJson();
            Connection = DbApiConnectionConfig.GetActiveConnection() ?? DbApiConnectionConfig.List[0];
        }
    }
}
