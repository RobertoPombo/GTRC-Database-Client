using System.Net;

using GTRC_Basics.Models.Common;

namespace GTRC_Database_Client.Responses
{
    public class DbApiListResponse<ModelType> where ModelType : class, IBaseModel, new()
    {
        public HttpStatusCode Status { get; set; } = HttpStatusCode.InternalServerError;
        public List<ModelType> List { get; set; } = [];
    }
}
