using GTRC_Basics;
using GTRC_Database_Client.Responses;
using GTRC_Basics.Models;

namespace GTRC_Database_Client.Requests
{
    public class DbApiRequestSession(DbApiConnectionConfig? connection = null) : DbApiRequest<Session>(connection)
    {
        public async Task<DbApiListResponse<Session>> GetRelated(int id)
        {
            if (connection is not null) { Response = await connection.SendRequest(Model, HttpRequestType.Get, "/Related/" + id.ToString()); }
            return await ReturnAsList(Response);
        }
    }
}
