using System;
using System.Collections.Generic;

using System.Text;

namespace AgentHelper.Proxy
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using fastJSON;

    public class ProxyEventData
    {
        public String           eventName { get; set; }
        public int              eventId   { get; set; }
        public bool             status    { get; set; }
        public List<Object> eventEx { get; set; }
        public Dictionary<string, string> eventBody { get; set;}
    }
}
