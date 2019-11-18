using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;

namespace RestApi
{
    class Usuario
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("nome")]
        public string nome { get; set; }

        [JsonProperty("telefone")]
        public string telefone { get; set; }

        [JsonProperty("email")]
        public string email { get; set; }
    }
}