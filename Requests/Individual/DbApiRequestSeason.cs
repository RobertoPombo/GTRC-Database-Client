using GTRC_Basics;
using GTRC_Database_Client.Responses;
using GTRC_Basics.Models;

namespace GTRC_Database_Client.Requests
{
    public class DbApiRequestSeason(DbApiConnectionConfig? connection = null) : DbApiRequest<Season>(connection)
    {
        public async Task<DbApiValueResponse<int>> GetNr(int id)
        {
            if (connection is not null) { Response = await connection.SendRequest(Model, HttpRequestType.Get, "/Nr/" + id.ToString()); }
            return new DbApiValueResponse<int>(Response);
        }

        public async Task<DbApiObjectResponse<Season>> GetByNr(int seriesId, int nr)
        {
            if (connection is not null) { Response = await connection.SendRequest(Model, HttpRequestType.Get, "/ByNr/" + seriesId.ToString() + "/" + nr.ToString()); }
            return await ReturnAsObject(Response);
        }

        public async Task<DbApiObjectResponse<Season>> GetCurrent(int seriesId, DateTime? date = null)
        {
            string _date = (date ?? DateTime.UtcNow).ToString("MM/dd/yyyy HH:mm:ss");
            if (connection is not null) { Response = await connection.SendRequest(Model, HttpRequestType.Get, "/Current/" + seriesId.ToString(), _date, nameof(date)); }
            return await ReturnAsObject(Response);
        }
    }
}
