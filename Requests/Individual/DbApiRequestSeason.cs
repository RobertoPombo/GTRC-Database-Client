using GTRC_Basics;
using GTRC_Database_Client.Responses;
using GTRC_Basics.Models;

namespace GTRC_Database_Client.Requests
{
    public class DbApiRequestSeason(DbApiConnectionConfig? connection = null) : DbApiRequest<Season>(connection)
    {
        public async Task<DbApiValueResponse<DateTime>> GetDateFirstEvent(int id)
        {
            if (connection is not null) { Response = await connection.SendRequest(Model, HttpRequestType.Get, "/DateFirstEvent/" + id.ToString()); }
            return new DbApiValueResponse<DateTime>(Response);
        }

        public async Task<DbApiValueResponse<DateTime>> GetDateFinalEvent(int id)
        {
            if (connection is not null) { Response = await connection.SendRequest(Model, HttpRequestType.Get, "/DateFinalEvent/" + id.ToString()); }
            return new DbApiValueResponse<DateTime>(Response);
        }
    }
}
