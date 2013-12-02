namespace AgentHelper.SipPhone
{
    using Sipek.Common;
    using Sipek.Common.CallControl;
    using Sipek.Sip;
    using System;
    using AgentHelper;

    public class SipekResources : AbstractFactory
    {
        private ICallLogInterface _callLogger = new CCallLog();
        private CCallManager _callManager = CCallManager.Instance;
        private SipekConfigurator _config = new SipekConfigurator();
        private frmWb _form;
        private IMediaProxyInterface _mediaProxy = new CMediaPlayerProxy();
        private IPresenceAndMessaging _messenger = pjsipPresenceAndMessaging.Instance;
        private IRegistrar _registrar = pjsipRegistrar.Instance;
        private pjsipStackProxy _stackProxy = pjsipStackProxy.Instance;

        public SipekResources(frmWb mf)
        {
            this._form = mf;
            SipConfigStruct.Instance.stunServer = this.Configurator.StunServerAddress;
            SipConfigStruct.Instance.publishEnabled = this.Configurator.PublishEnabled;
            SipConfigStruct.Instance.expires = this.Configurator.Expires;
            SipConfigStruct.Instance.VADEnabled = this.Configurator.VADEnabled;
            SipConfigStruct.Instance.ECTail = this.Configurator.ECTail;
            SipConfigStruct.Instance.nameServer = this.Configurator.NameServer;
            this._callManager.StackProxy = this._stackProxy;
            this._callManager.Config = this._config;
            //this._callManager.Factory = this;
            this._callManager.MediaProxy = this._mediaProxy;
            this._stackProxy.Config = this._config;
            this._registrar.Config = this._config;
            this._messenger.Config = this._config;
        }

        public IStateMachine createStateMachine()
        {
            return new CStateMachine();
        }

        public ITimer createTimer()
        {
            return new GUITimer(this._form);
        }

        public ICallLogInterface CallLogger
        {
            get
            {
                return this._callLogger;
            }
            set
            {
            }
        }

        public CCallManager CallManager
        {
            get
            {
                return CCallManager.Instance;
            }
        }

        public SipekConfigurator Configurator
        {
            get
            {
                return this._config;
            }
            set
            {
            }
        }

        public IMediaProxyInterface MediaProxy
        {
            get
            {
                return this._mediaProxy;
            }
            set
            {
            }
        }

        public IPresenceAndMessaging Messenger
        {
            get
            {
                return this._messenger;
            }
        }

        public IRegistrar Registrar
        {
            get
            {
                return this._registrar;
            }
        }

        public pjsipStackProxy StackProxy
        {
            get
            {
                return this._stackProxy;
            }
            set
            {
                this._stackProxy = value;
            }
        }
    }
}
