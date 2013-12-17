using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgentHelper.Proxy
{
   public class ProxyEventDataAgentStatus:ProxyEventData
   {
      public class AgentStatus{
            public String agentName { get; set; }   
            public String isPaused  { get; set; }
            public String isLogin { get; set; }
            public String loginTime { get; set; }
            public String agentStatus { get; set;}
            public String agentStatusChangeTime { get; set; }
       }


       public List<AgentStatus> eventEx{get;set;}  
       

   }
}
