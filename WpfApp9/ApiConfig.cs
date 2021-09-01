using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WpfApp9
{
    public struct ApiConfig
    {
        [JsonProperty("token")]
        public string Token { get; set; }
    }
}
