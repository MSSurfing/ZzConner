using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Zz.Http.Core.Mvc.Results
{
    // Todo, MessageResult:ActionResult
    public class MessageResult
    {
        [JsonProperty("code", NullValueHandling = NullValueHandling.Ignore)]
        public int Code { get; set; }

        [JsonProperty("message", NullValueHandling = NullValueHandling.Ignore)]
        public object Message { get; set; }

        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public object Data { get; set; }
    }
}
