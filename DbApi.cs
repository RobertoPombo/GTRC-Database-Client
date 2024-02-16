namespace GTRC_Database_Client
{
    public class DbApi
    {
        public static readonly DbApiConnectionConfig Connection;

        public static DbApiConnectionConfig DynConnection { get { return DbApiConnectionConfig.GetActiveConnection() ?? DbApiConnectionConfig.List[0]; } }

        static DbApi()
        {
            DbApiConnectionConfig.LoadJson();
            Connection = DbApiConnectionConfig.GetActiveConnection() ?? DbApiConnectionConfig.List[0];
        }
    }
}
