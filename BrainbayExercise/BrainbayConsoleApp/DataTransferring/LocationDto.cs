﻿using Newtonsoft.Json;

namespace BrainbayConsoleApp.DataTransferring
{
    public class LocationDto
    {

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}