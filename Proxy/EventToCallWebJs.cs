using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgentHelper.Proxy
{
    class EventToCallWebJs
    {
        public static String CreateCallBridgedEvent(String caller, String called, String callType,String callId) {
            return "";
        }

        public static String CreateOnClickTransferButonBeginEvent()
        {
            String eventData = "{eventId:21,eventName:'onUiClickTransferButton',status:'begin'}";
            return eventData;
        }

        public static String CreateOnClickMakeBusyButtonEvent(String sbusy)
        {
            String eventData = "{eventId:20,eventName:'onUiMakeBusy',busy:" +sbusy+ "}";
            return eventData;
        }


        public static String CreateIncomingCallBySipPhoneEvent(String callerId)
        {
            string msg = "{eventId:9,eventName:'SipPhoneCalling',callerId:'" + callerId + "'}";
            return msg;
        }

    }
}
