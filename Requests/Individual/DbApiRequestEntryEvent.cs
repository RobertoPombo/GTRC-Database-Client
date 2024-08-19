using GTRC_Basics;
using GTRC_Database_Client.Responses;
using GTRC_Basics.Models;
using GTRC_Basics.Models.DTOs;

namespace GTRC_Database_Client.Requests
{
    public class DbApiRequestEntryEvent(DbApiConnectionConfig? connection = null) : DbApiRequest<EntryEvent>(connection)
    {
        public async Task<DbApiObjectResponse<EntryEvent>> GetAnyByUniqProps(EntryEventUniqPropsDto0 objDto)
        {
            if (connection is not null) { Response = await connection.SendRequest(Model, HttpRequestType.Get, "/ByUniqProps/0/Any", objDto); }
            return await ReturnAsObject(Response);
        }

        public async Task<DbApiValueResponse<byte>> GetSignOutsCount(int entryId, int eventId)
        {
            if (connection is not null) { Response = await connection.SendRequest(Model, HttpRequestType.Get, "/SignOutsCount/" + entryId.ToString() + "/" + eventId.ToString()); }
            return new DbApiValueResponse<byte>(Response);
        }

        public async Task<DbApiValueResponse<byte>> GetNoShowsCount(int entryId, int eventId)
        {
            if (connection is not null) { Response = await connection.SendRequest(Model, HttpRequestType.Get, "/NoShowsCount/" + entryId.ToString() + "/" + eventId.ToString()); }
            return new DbApiValueResponse<byte>(Response);
        }
    }
}
