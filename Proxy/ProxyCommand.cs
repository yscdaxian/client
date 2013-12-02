using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgentHelper.Proxy
{
    class ProxyCommand
    {

        public virtual String ProxyExtMakeBusy(String ext,Boolean isBusy) 
        {
            StringBuilder cmd = new StringBuilder();
            cmd.Append("{\"cmdType\":3,\"requester\":\"");
            cmd.Append(ext);
            cmd.Append("\",\"ext\":\"");
            cmd.Append(ext);
            cmd.Append("\",\"isBusy\":");
            cmd.Append(isBusy.ToString().ToLower());
            cmd.Append("}");
            return cmd.ToString() + "\n";
          
        }

        /* 发送坐席登录消息 
         * cmdType=1
         */
        public virtual String ProxyAgentLogin(String agentId)
        {
            StringBuilder cmd = new StringBuilder();
            cmd.Append("{\"cmdType\":1,\"requester\":\"");
            cmd.Append(agentId);
            cmd.Append("\",\"agent\":\"");
            cmd.Append(agentId);
            cmd.Append("\"}");
            return cmd.ToString() + "\n";     
        }

        /* 发送坐席登录消息 
         * cmdType=2
         */
        public virtual String ProxyAgentLoginOut(String agentId)
        {
            StringBuilder cmd = new StringBuilder();
            cmd.Append("{\"cmdType\":2,\"requester\":\"");
            cmd.Append(agentId);
            cmd.Append("\",\"agent\":\"");
            cmd.Append(agentId);
            cmd.Append("\"}");
            return cmd.ToString() + "\n";
        }

        public virtual String ProxyQueueLogin(String agentId, String queueId)
        {
            return "";
        }
        public virtual String ProxyQueuePause(String agentId,String queueId,Boolean isPaused) 
        {
            StringBuilder cmd = new StringBuilder();
            cmd.Append("{\"cmdType\":5,\"agent\":\"");
            cmd.Append(agentId);
            cmd.Append("\",\"queue\":\"");
            cmd.Append(queueId);
            cmd.Append("\",\"isPaused\":");
            cmd.Append(isPaused.ToString().ToLower());
            cmd.Append("}");
        
            return cmd.ToString() + "\n";
        }
        public virtual String ProxyQueueRemove(String agentId, String queueId)
        {
            return "";
        }
        public virtual String ProxyTransfer(String oldExt, String newExt, int mod) 
        {
            StringBuilder cmd = new StringBuilder();
            cmd.Append("{\"cmdType\":7,\"oldExt\":\"");
            cmd.Append(oldExt);
            cmd.Append("\",\"newExt\":\"");
            cmd.Append(newExt);
            cmd.Append("\",\"mod\":\"");
            cmd.Append(mod.ToString());
            cmd.Append("\"}");
            return cmd.ToString() + "\n";
        }
        public virtual String ProxyNwayCallStart(String agentId, String conference) {
            StringBuilder cmd = new StringBuilder();
            cmd.Append("{\"cmdType\":8,\"agent\":\"");
            cmd.Append(agentId);
            cmd.Append("\",\"conference\":\"");
            cmd.Append(conference);

            cmd.Append("\"}");
            return cmd.ToString() + "\n";
        }

        public virtual String ProxyNwayCallAddOne(String another, String conference,String callerid="98888",int time=30) {
            StringBuilder cmd = new StringBuilder();
            cmd.Append("{\"cmdType\":9,\"another\":\"");
            cmd.Append(another);
            cmd.Append("\",\"conference\":\"");
            cmd.Append(conference);
            cmd.Append("\",\"callerId\":\"");
            cmd.Append(callerid);
            cmd.Append("\",\"time\":\"");
            cmd.Append(time.ToString());
            cmd.Append("\"}");
            return cmd.ToString() + "\n";
        }
        public virtual String ProxyTreePartCall(String agentId,String thirdPart) {
            StringBuilder cmd = new StringBuilder();
            cmd.Append("{\"cmdType\":10,\"requester\":\"");
            cmd.Append(agentId);
            cmd.Append("\",\"thirdPart\":\"");
            cmd.Append(thirdPart);
            cmd.Append("\"}");
            return cmd.ToString() + "\n";
        }
        public virtual String ProxyForceInsertCall(String agentId, String thirdPart)
        {
            StringBuilder cmd = new StringBuilder();
            cmd.Append("{\"cmdType\":11,\"requester\":\"");
            cmd.Append(agentId);
            cmd.Append("\",\"targetAgent\":\"");
            cmd.Append(thirdPart);
            cmd.Append("\"}");
            return cmd.ToString() + "\n";
        }
        public virtual String ProxyHangupCall(String agentId, String targetAgent) {
            StringBuilder cmd = new StringBuilder();
            cmd.Append("{\"cmdType\":12,\"requester\":\"");
            cmd.Append(agentId);
            cmd.Append("\",\"targetAgent\":\"");
            cmd.Append(targetAgent);
            cmd.Append("\"}");
            return cmd.ToString() + "\n";
        }

        public virtual String ProxySpyCall(String agentId, String targetAgent)
        {
            StringBuilder cmd = new StringBuilder();
            cmd.Append("{\"cmdType\":13,\"requester\":\"");
            cmd.Append(agentId);
            cmd.Append("\",\"targetAgent\":\"");
            cmd.Append(targetAgent);
            cmd.Append("\"}");
            return cmd.ToString() + "\n";
        }

    }
}
