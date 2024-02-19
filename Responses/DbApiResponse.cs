using System.Net;

using GTRC_Basics.Models.Common;

namespace GTRC_Database_Client.Responses
{
    public class DbApiResponse<ModelType> where ModelType : class, IBaseModel, new()
    {
        private HttpStatusCode status = HttpStatusCode.InternalServerError;
        private ModelType obj = new();
        private List<ModelType> list = [];

        public HttpStatusCode Status { get { return status; } set { status = value; } }
        public ModelType Obj { get { return obj; } set { obj = value; list = [obj]; } }
        public List<ModelType> List { get { return list; } set { list = value; if (list.Count > 0) { obj = list[0]; } } }
    }
}
