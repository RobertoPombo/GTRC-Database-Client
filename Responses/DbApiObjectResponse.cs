using System.Net;

using GTRC_Basics.Models.Common;

namespace GTRC_Database_Client.Responses
{
    public class DbApiObjectResponse<ModelType> where ModelType : class, IBaseModel, new()
    {
        public HttpStatusCode Status { get; set; } = HttpStatusCode.InternalServerError;
        public ModelType Object { get; set; } = new();
    }
}
