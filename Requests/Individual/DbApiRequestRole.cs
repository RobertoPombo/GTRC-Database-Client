using GTRC_Basics;
using GTRC_Database_Client.Responses;
using GTRC_Basics.Models;

namespace GTRC_Database_Client.Requests
{
    public class DbApiRequestRole(DbApiConnectionConfig? connection = null) : DbApiRequest<Role>(connection)
    {
        public async Task<DbApiListResponse<Role>> GetByUser(int userId)
        {
            if (connection is not null) { Response = await connection.SendRequest(Model, HttpRequestType.Get, "/ByUser/" + userId.ToString()); }
            return await ReturnAsList(Response);
        }
    }
}
