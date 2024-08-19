using GTRC_Basics;
using GTRC_Database_Client.Responses;
using GTRC_Basics.Models;
using GTRC_Basics.Models.DTOs;

namespace GTRC_Database_Client.Requests
{
    public class DbApiRequestEventCarclass(DbApiConnectionConfig? connection = null) : DbApiRequest<EventCarclass>(connection)
    {
        public async Task<DbApiObjectResponse<EventCarclass>> GetAnyByUniqProps(EventCarclassUniqPropsDto0 objDto)
        {
            if (connection is not null) { Response = await connection.SendRequest(Model, HttpRequestType.Get, "/ByUniqProps/0/Any", objDto); }
            return await ReturnAsObject(Response);
        }
    }
}
