using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgentHelper.Proxy
{
    interface ICallByWebJs
    {
        void AgentCall(String agentId,String phoneNumber);
        void AgentHangup(String agentId);
        void AgentQueueLogin(String agentId, String pwd,String ext);
        void AgentQueuePause(String agnetId,String queueId,Boolean isPaused);
        void AgentMakeBusy(String agentId, Boolean busy);
        void AgentTransfer(String oldExt, String newExt, int mod);
        void AgentNwayCallStart(String agentId, String conference);
        void AgentNwayCallAddOne(String agentId, String another, String conference, String CallerId="988888",int time=30);
        void AgentForceInsert(String agentId, String targetAgent);
        void AgentHangupCall(String agentId, String targetAgent);
        void AgentSpyCall(String agentId, String targetAgent);
        void ExtLogin(String ext, String pwd, String host="");
        void ExtLoginOut(String ext);
    }

}
