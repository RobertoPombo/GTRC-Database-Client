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

        public async Task<DbApiObjectResponse<Event>> GetByNr(int seasonId, int nr)
        {
            if (connection is not null) { Response = await connection.SendRequest(Model, HttpRequestType.Get, "/ByNr/" + seasonId.ToString() + "/" + nr.ToString()); }
            return await ReturnAsObject(Response);
        }

        public async Task<DbApiObjectResponse<Event>> GetNext(int seasonId, DateTime? date = null)
        {
            string _date = (date ?? DateTime.UtcNow).ToString("MM/dd/yyyy HH:mm:ss");
            if (connection is not null) { Response = await connection.SendRequest(Model, HttpRequestType.Get, "/Next/" + seasonId.ToString(), _date, nameof(date)); }
            return await ReturnAsObject(Response);
        }

        public async Task<DbApiObjectResponse<Event>> GetFirst(int seasonId)
        {
            if (connection is not null) { Response = await connection.SendRequest(Model, HttpRequestType.Get, "/First/" + seasonId.ToString()); }
            return await ReturnAsObject(Response);
        }

        public async Task<DbApiObjectResponse<Event>> GetFinal(int seasonId)
        {
            if (connection is not null) { Response = await connection.SendRequest(Model, HttpRequestType.Get, "/Final/" + seasonId.ToString()); }
            return await ReturnAsObject(Response);
        }
    }
}
