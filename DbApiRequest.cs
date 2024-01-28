using Newtonsoft.Json;
using System.Net;

using GTRC_Basics;
using GTRC_Basics.Models.Common;
using GTRC_Basics.Models.DTOs;

namespace GTRC_Database_Client
{
    public class DbApiRequest<ModelType>(DbApiConnectionConfig? connection = null) where ModelType : class, IBaseModel, new()
    {
        private static readonly string model = typeof(ModelType).Name;

        public HttpResponseMessage? Response;

        public static async Task<DbApiResponse<ModelType>> ReturnAsObject(HttpResponseMessage? response)
        {
            DbApiResponse<ModelType> obj = new();
            if (response is not null)
            {
                obj.Status = response.StatusCode;
                obj.Obj = JsonConvert.DeserializeObject<ModelType>(await response.Content.ReadAsStringAsync()) ?? new();
            }
            return obj;
        }

        public static async Task<DbApiResponse<ModelType>> ReturnAsList(HttpResponseMessage? response)
        {
            DbApiResponse<ModelType> obj = new();
            if (response is not null)
            {
                obj.Status = response.StatusCode;
                obj.List = JsonConvert.DeserializeObject<List<ModelType>>(await response.Content.ReadAsStringAsync()) ?? [];
            }
            return obj;
        }

        public async Task<DbApiResponse<ModelType>> GetAll()
        {
            if (connection is not null) { Response = await connection.SendRequest(model, HttpRequestType.Get); }
            return await ReturnAsList(Response);
        }

        public async Task<DbApiResponse<ModelType>> GetById(int id)
        {
            if (connection is not null) { Response = await connection.SendRequest(model, HttpRequestType.Get, "/" + id.ToString()); }
            return await ReturnAsObject(Response);
        }

        public async Task<DbApiResponse<ModelType>> GetByUniqProps(UniqPropsDto<ModelType> objDto)
        {
            if (connection is not null) { Response = await connection.SendRequest(model, HttpRequestType.Get, "/ByUniqProps/" + objDto.Index.ToString(), objDto.Dto); }
            return await ReturnAsObject(Response);
        }

        public async Task<DbApiResponse<ModelType>> GetByProps(AddDto<ModelType> objDto)
        {
            if (connection is not null) { Response = await connection.SendRequest(model, HttpRequestType.Get, "/ByProps", objDto.Dto); }
            return await ReturnAsList(Response);
        }

        public async Task<DbApiResponse<ModelType>> GetByFilter(FilterDtos<ModelType> objDto)
        {
            if (connection is not null) { Response = await connection.SendRequest(model, HttpRequestType.Get, "/ByFilter", objDto.Dto); }
            return await ReturnAsList(Response);
        }

        public async Task<DbApiResponse<ModelType>> GetTemp()
        {
            if (connection is not null) { Response = await connection.SendRequest(model, HttpRequestType.Get, "/Temp"); }
            return await ReturnAsObject(Response);
        }
        
        public async Task<DbApiResponse<ModelType>> Add(AddDto<ModelType> objDto)
        {
            if (connection is not null) { Response = await connection.SendRequest(model, HttpRequestType.Add, objDto: objDto.Dto); }
            return await ReturnAsObject(Response);
        }

        public async Task<HttpStatusCode> Delete(int id, bool force = false)
        {
            if (connection is not null) { Response = await connection.SendRequest(model, HttpRequestType.Delete, "/" + id.ToString() + "/" + force.ToString()); }
            return Response?.StatusCode ?? HttpStatusCode.InternalServerError;
        }

        public async Task<DbApiResponse<ModelType>> Update(UpdateDto<ModelType> objDto)
        {
            if (connection is not null) { Response = await connection.SendRequest(model, HttpRequestType.Update, objDto: objDto.Dto); }
            return await ReturnAsObject(Response);
        }

        public async Task<bool> HasChildObjects(int id)
        {
            if (connection is not null) { Response = await connection.SendRequest(model, HttpRequestType.Get, "/HasChildObjects/" + id.ToString()); }
            bool content = false;
            if (Response is not null) { _ = bool.TryParse(await Response.Content.ReadAsStringAsync() ?? false.ToString(), out content); }
            return content;
        }

        public async Task<DbApiResponse<ModelType>> GetChildObjects(Type parentModelType, int parentId)
        {
            if (connection is not null) { Response = await connection.SendRequest(parentModelType.Name, HttpRequestType.Get, "/" + model + "/" + parentId.ToString()); }
            return await ReturnAsList(Response);
        }
    }
}
