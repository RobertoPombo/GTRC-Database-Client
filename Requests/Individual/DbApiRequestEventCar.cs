using GTRC_Basics;
using GTRC_Database_Client.Responses;
using GTRC_Basics.Models;

namespace GTRC_Database_Client.Requests
{
    public class DbApiRequestEventCar(DbApiConnectionConfig? connection = null) : DbApiRequest<EventCar>(connection)
    {
        public async Task<DbApiListResponse<EventCar>> UpdateBop(int eventId)
        {
            if (connection is not null) { Response = await connection.SendRequest(Model, HttpRequestType.Update, "/Bop/" + eventId.ToString()); }
            return await ReturnAsList(Response);
        }
    }
}
