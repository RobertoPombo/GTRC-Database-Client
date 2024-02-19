using GTRC_Basics;
using GTRC_Database_Client.Responses;
using GTRC_Basics.Models;
using GTRC_Basics.Models.DTOs;

namespace GTRC_Database_Client.Requests
{
    public class DbApiRequestEntryDatetime(DbApiConnectionConfig? connection = null) : DbApiRequest<EntryDatetime>(connection)
    {
        public async Task<DbApiResponse<EntryDatetime>> GetAnyByUniqProps(EntryDatetimeUniqPropsDto0 objDto)
        {
            if (connection is not null) { Response = await connection.SendRequest(Model, HttpRequestType.Get, "/ByUniqProps/0/Any", objDto); }
            return await ReturnAsObject(Response);
        }
    }
}
