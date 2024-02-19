using GTRC_Basics;
using GTRC_Database_Client.Responses;
using GTRC_Basics.Models;

namespace GTRC_Database_Client.Requests
{
    public class DbApiRequestUser(DbApiConnectionConfig? connection = null) : DbApiRequest<User>(connection)
    {
        public async Task<DbApiResponse<User>> GetByEntry(int entryId)
        {
            if (connection is not null) { Response = await connection.SendRequest(Model, HttpRequestType.Get, "/ByEntry/" + entryId.ToString()); }
            return await ReturnAsList(Response);
        }

        public async Task<DbApiResponse<User>> GetByEntryEvent(int entryId, int eventId)
        {
            if (connection is not null) { Response = await connection.SendRequest(Model, HttpRequestType.Get, "/ByEntryEvent/" + entryId.ToString() + "/" + eventId.ToString()); }
            return await ReturnAsList(Response);
        }
    }
}
