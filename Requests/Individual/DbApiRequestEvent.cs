using GTRC_Basics;
using GTRC_Database_Client.Responses;
using GTRC_Basics.Models;

namespace GTRC_Database_Client.Requests
{
    public class DbApiRequestEvent(DbApiConnectionConfig? connection = null) : DbApiRequest<Event>(connection)
    {
        public async Task<DbApiValueResponse<int>> GetNr(int id)
        {
            if (connection is not null) { Response = await connection.SendRequest(Model, HttpRequestType.Get, "/Nr/" + id.ToString()); }
            return new DbApiValueResponse<int>(Response);
        }

        public async Task<DbApiResponse<Event>> GetNext(int seasonId, DateTime? date = null)
        {
            date ??= DateTime.UtcNow;
            if (connection is not null) { Response = await connection.SendRequest(Model, HttpRequestType.Get, "/Next/" + seasonId.ToString(), date, nameof(date)); }
            return await ReturnAsObject(Response);
        }
    }
}
