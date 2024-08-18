using GTRC_Basics;
using GTRC_Database_Client.Responses;
using GTRC_Basics.Models;

namespace GTRC_Database_Client.Requests
{
    public class DbApiRequestTeam(DbApiConnectionConfig? connection = null) : DbApiRequest<Team>(connection)
    {
        public async Task<DbApiListResponse<Team>> GetBySeason(int seasonId)
        {
            if (connection is not null) { Response = await connection.SendRequest(Model, HttpRequestType.Get, "/BySeason/" + seasonId.ToString()); }
            return await ReturnAsList(Response);
        }

        public async Task<DbApiListResponse<Team>> GetViolationsMinEntriesPerTeam(int seasonId)
        {
            if (connection is not null) { Response = await connection.SendRequest(Model, HttpRequestType.Get, "/Violations/MinEntriesPerTeam/" + seasonId.ToString()); }
            return await ReturnAsList(Response);
        }

        public async Task<DbApiListResponse<Team>> GetViolationsMaxEntriesPerTeam(int seasonId)
        {
            if (connection is not null) { Response = await connection.SendRequest(Model, HttpRequestType.Get, "/Violations/MaxEntriesPerTeam/" + seasonId.ToString()); }
            return await ReturnAsList(Response);
        }
    }
}
