using GTRC_Basics;
using GTRC_Database_Client.Responses;
using GTRC_Basics.Models;

namespace GTRC_Database_Client.Requests
{
    public class DbApiRequestCar(DbApiConnectionConfig? connection = null) : DbApiRequest<Car>(connection)
    {
        public async Task<DbApiValueResponse<bool>> GetIsLatestModel(int id)
        {
            if (connection is not null) { Response = await connection.SendRequest(Model, HttpRequestType.Get, "/IsLatestModel/" + id.ToString()); }
            return new DbApiValueResponse<bool>(Response);
        }
    }
}
