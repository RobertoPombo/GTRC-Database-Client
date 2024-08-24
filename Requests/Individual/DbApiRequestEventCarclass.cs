using GTRC_Basics;
using GTRC_Database_Client.Responses;
using GTRC_Basics.Models;

namespace GTRC_Database_Client.Requests
{
    public class DbApiRequestEventCarclass(DbApiConnectionConfig? connection = null) : DbApiRequest<EventCarclass>(connection)
    {
        public async Task<DbApiValueResponse<byte>> GetSlotsAvailable(int id)
        {
            if (connection is not null) { Response = await connection.SendRequest(Model, HttpRequestType.Get, "/SlotsAvailable/" + id.ToString()); }
            return new DbApiValueResponse<byte>(Response);
        }

        public async Task<DbApiValueResponse<byte>> GetSlotsAvailable(int id, int carclassId)
        {
            if (connection is not null) { Response = await connection.SendRequest(Model, HttpRequestType.Get, "/SlotsAvailable/" + id.ToString() + "/" + carclassId.ToString()); }
            return new DbApiValueResponse<byte>(Response);
        }
    }
}
