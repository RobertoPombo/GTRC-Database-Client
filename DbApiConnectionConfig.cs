using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;

using GTRC_Basics;

namespace GTRC_Database_Client
{
    public class DbApiConnectionConfig : ConnectionConfig
    {
        public static readonly List<DbApiConnectionConfig> List = [];

        public DbApiConnectionConfig() { List.Add(this); Name = name; }

        private string name = "Preset #1";
        private bool isActive = false;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                int nr = 1;
                string delimiter = " #";
                string defName = name;
                string[] defNameList = defName.Split(delimiter);
                if (defNameList.Length > 1 && Int32.TryParse(defNameList[^1], out _)) { defName = defName[..^(defNameList[^1].Length + delimiter.Length)]; }
                while (!IsUniqueName())
                {
                    name = defName + delimiter + nr.ToString();
                    nr++; if (nr == int.MaxValue) { break; }
                }
            }
        }

        public bool IsActive
        {
            get { return isActive; }
            set { if (value != isActive) { if (value) { DeactivateAllConnections(); } isActive = value; } }
        }

        public bool IsUniqueName()
        {
            int listIndexThis = List.IndexOf(this);
            for (int conNr = 0; conNr < List.Count; conNr++)
            {
                if (List[conNr].Name == name && conNr != listIndexThis) { return false; }
            }
            return true;
        }

        public static DbApiConnectionConfig? GetActiveConnection()
        {
            foreach (DbApiConnectionConfig con in List) { if (con.IsActive) { return con; } }
            return null;
        }

        public static void DeactivateAllConnections()
        {
            DbApiConnectionConfig? con = GetActiveConnection();
            if (con is not null) { con.IsActive = false; }
        }

        [JsonIgnore] public string BaseUrl
        {
            get
            {
                string baseUrl = string.Empty;
                if (ProtocolType != ProtocolType.None) { baseUrl += ProtocolType.ToString() + "://"; }
                if (NetworkType == NetworkType.Localhost) { baseUrl += NetworkType.ToString().ToLower(); }
                else if (IpAdressType == IpAdressType.IPv4) { baseUrl += Ipv4.ToString(); }
                else if (IpAdressType == IpAdressType.IPv6) { baseUrl += "[" + Ipv6.ToString() + "]"; }
                return baseUrl + ":" + Port.ToString();
            }
        }

        public async Task<Tuple<HttpStatusCode, string>> SendHttpRequest(string modelTypename, HttpRequestType requestType, string? path = null, dynamic? objDto = null)
        {
            HttpResponseMessage? _response = null;
            path ??= string.Empty;
            string url = string.Join("/", [BaseUrl, modelTypename, requestType.ToString()]);
            Tuple<HttpStatusCode, string> response = Tuple.Create(HttpStatusCode.InternalServerError, string.Empty);
            using HttpClient httpClient = new();
            {
                try
                {
                    switch (requestType)
                    {
                        case HttpRequestType.Get:
                            if (objDto is null) { _response = await httpClient.GetAsync(url + path); }
                            else { _response = await httpClient.PutAsync(url + path, JsonContent.Create(objDto)); }
                            break;
                        case HttpRequestType.Delete:
                            if (objDto is null) { _response = await httpClient.DeleteAsync(url + path); }
                            else { _response = await httpClient.DeleteAsync(url + path, JsonContent.Create(objDto)); }
                            break;
                        case HttpRequestType.Add: if (objDto is not null) { _response = await httpClient.PostAsync(url + path, JsonContent.Create(objDto)); } break;
                        case HttpRequestType.Update: if (objDto is not null) { _response = await httpClient.PutAsync(url + path, JsonContent.Create(objDto)); } break;
                        default: _response = null; break;
                    }
                }
                catch (HttpRequestException) { GlobalValues.CurrentLogText = "Connection to GTRC-Database-API failed!"; }
                if (_response is not null)
                {
                    HttpStatusCode status = _response.StatusCode;
                    string message = await _response.Content.ReadAsStringAsync();
                    response = Tuple.Create(status, message);
                }
            }
            return response;
        }
    }
}
