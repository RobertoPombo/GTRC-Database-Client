using GTRC_Basics;
using GTRC_Database_Client.Responses;
using GTRC_Basics.Models;

namespace GTRC_Database_Client.Requests
{
    public class DbApiRequestOrganizationUser(DbApiConnectionConfig? connection = null) : DbApiRequest<OrganizationUser>(connection)
    {
        public async Task<DbApiResponse<OrganizationUser>> GetAdmins(int organizationId)
        {
            if (connection is not null) { Response = await connection.SendRequest(Model, HttpRequestType.Get, "/Admins/" + organizationId.ToString()); }
            return await ReturnAsObject(Response);
        }
    }
}
