﻿using GTRC_Basics;
using GTRC_Database_Client.Responses;
using GTRC_Basics.Models;

namespace GTRC_Database_Client.Requests
{
    public class DbApiRequestCarclass(DbApiConnectionConfig? connection = null) : DbApiRequest<Carclass>(connection)
    {
        public async Task<DbApiListResponse<Carclass>> GetBySeason(int seasonId)
        {
            if (connection is not null) { Response = await connection.SendRequest(Model, HttpRequestType.Get, "/BySeason/" + seasonId.ToString()); }
            return await ReturnAsList(Response);
        }
    }
}
