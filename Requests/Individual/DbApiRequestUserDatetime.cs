using GTRC_Basics;
using GTRC_Database_Client.Responses;
using GTRC_Basics.Models;
using GTRC_Basics.Models.DTOs;

namespace GTRC_Database_Client.Requests
{
    public class DbApiRequestUserDatetime(DbApiConnectionConfig? connection = null) : DbApiRequest<UserDatetime>(connection)
    {
        public async Task<DbApiObjectResponse<UserDatetime>> GetAnyByUniqProps(UserDatetimeUniqPropsDto0 objDto)
        {
            if (connection is not null) { Response = await connection.SendRequest(Model, HttpRequestType.Get, "/ByUniqProps/0/Any", objDto); }
            return await ReturnAsObject(Response);
        }
    }
}
