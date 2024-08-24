using GTRC_Basics;
using GTRC_Database_Client.Responses;
using GTRC_Basics.Models;

namespace GTRC_Database_Client.Requests
{
    public class DbApiRequestEntryUserEvent(DbApiConnectionConfig? connection = null) : DbApiRequest<EntryUserEvent>(connection)
    {
        public async Task<DbApiListResponse<EntryUserEvent>> UpdateNames3Digits(int eventId)
        {
            if (connection is not null) { Response = await connection.SendRequest(Model, HttpRequestType.Update, "/Names3Digits/" + eventId.ToString()); }
            return await ReturnAsList(Response);
        }
    }
}
