using Newtonsoft.Json;
using System.Net.Http.Json;

using GTRC_Basics;
using GTRC_Basics.Models;

namespace GTRC_Database_Client
{
    public class DbApiConnectionConfig : ConnectionConfig
    {
        public static readonly List<DbApiConnectionConfig> List = [];

        public DbApiConnectionConfig() { List.Add(this); Name = name; UpdateDbApiRequests(); }

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
            foreach (DbApiConnectionConfig con in List) { if (con.IsActive) { con.UpdateDbApiRequests(); return con; } }
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

        public async Task<HttpResponseMessage?> SendRequest(string modelTypename, HttpRequestType requestType, string? path = null, dynamic? objDto = null)
        {
            HttpResponseMessage? response = null;
            path ??= string.Empty;
            string url = string.Join("/", [BaseUrl, modelTypename, requestType.ToString()]);
            using HttpClient httpClient = new();
            {
                try
                {
                    switch (requestType)
                    {
                        case HttpRequestType.Get:
                            if (objDto is null) { response = await httpClient.GetAsync(url + path); }
                            else { response = await httpClient.PutAsync(url + path, JsonContent.Create(objDto)); }
                            break;
                        case HttpRequestType.Delete:
                            if (objDto is null) { response = await httpClient.DeleteAsync(url + path); }
                            else { response = await httpClient.DeleteAsync(url + path, JsonContent.Create(objDto)); }
                            break;
                        case HttpRequestType.Add: if (objDto is not null) { response = await httpClient.PostAsync(url + path, JsonContent.Create(objDto)); } break;
                        case HttpRequestType.Update: if (objDto is not null) { response = await httpClient.PutAsync(url + path, JsonContent.Create(objDto)); } break;
                        default: response = null; break;
                    }
                }
                catch (HttpRequestException) { GlobalValues.CurrentLogText = "Connection to GTRC-Database-API failed!"; }
            }
            return response;
        }

        public void UpdateDbApiRequests()
        {
            Color = new(this);
            Sim = new(this);
            User = new(this);
            Track = new(this);
            Carclass = new(this);
            Manufacturer = new(this);
            Car = new(this);
            Role = new(this);
            UserRole = new(this);
            Bop = new(this);
            BopTrackCar = new(this);
            Series = new(this);
            Season = new(this);
            SeasonCarclass = new(this);
            Organization = new(this);
            OrganizationUser = new(this);
            Team = new(this);
            Event = new(this);
            EventCarclass = new(this);
            EventCar = new(this);
        }

        [JsonIgnore] public DbApiRequest<Color> Color { get; set; } = new();
        [JsonIgnore] public DbApiRequest<Sim> Sim { get; set; } = new();
        [JsonIgnore] public DbApiRequest<User> User { get; set; } = new();
        [JsonIgnore] public DbApiRequest<Track> Track { get; set; } = new();
        [JsonIgnore] public DbApiRequest<Carclass> Carclass { get; set; } = new();
        [JsonIgnore] public DbApiRequest<Manufacturer> Manufacturer { get; set; } = new();
        [JsonIgnore] public DbApiRequest<Car> Car { get; set; } = new();
        [JsonIgnore] public DbApiRequest<Role> Role { get; set; } = new();
        [JsonIgnore] public DbApiRequest<UserRole> UserRole { get; set; } = new();
        [JsonIgnore] public DbApiRequest<Bop> Bop { get; set; } = new();
        [JsonIgnore] public DbApiRequest<BopTrackCar> BopTrackCar { get; set; } = new();
        [JsonIgnore] public DbApiRequest<Series> Series { get; set; } = new();
        [JsonIgnore] public DbApiRequest<Season> Season { get; set; } = new();
        [JsonIgnore] public DbApiRequest<SeasonCarclass> SeasonCarclass { get; set; } = new();
        [JsonIgnore] public DbApiRequest<Organization> Organization { get; set; } = new();
        [JsonIgnore] public DbApiRequest<OrganizationUser> OrganizationUser { get; set; } = new();
        [JsonIgnore] public DbApiRequest<Team> Team { get; set; } = new();
        [JsonIgnore] public DbApiRequest<Event> Event { get; set; } = new();
        [JsonIgnore] public DbApiRequest<EventCarclass> EventCarclass { get; set; } = new();
        [JsonIgnore] public DbApiRequest<EventCar> EventCar { get; set; } = new();
    }
}
