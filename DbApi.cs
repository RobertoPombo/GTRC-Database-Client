namespace GTRC_Database_Client
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
