namespace AgentHelper.SipPhone
{
    using Sipek.Common;
    using System;
    using AgentHelper.Properties;

    public class SipekAccount : IAccount
    {
        private int _accountIdentification = -1;
        private int _index = -1;

        public SipekAccount(int index)
        {
            this._index = index;
        }

        public string AccountName
        {
            get
            {
                return Settings.Default.cfgSipAccountNames[this._index];
            }
            set
            {
                Settings.Default.cfgSipAccountNames[this._index] = value;
            }
        }

        public string DisplayName
        {
            get
            {
                return Settings.Default.cfgSipAccountDisplayName[this._index];
            }
            set
            {
                Settings.Default.cfgSipAccountDisplayName[this._index] = value;
            }
        }

        public string DomainName
        {
            get
            {
                return Settings.Default.cfgSipAccountDomains[this._index];
            }
            set
            {
                Settings.Default.cfgSipAccountDomains[this._index] = value;
            }
        }

        public bool Enabled
        {
            get
            {
                bool flag;
                return (bool.TryParse(Settings.Default.cfgSipAccountEnabled[this._index], out flag) && flag);
            }
            set
            {
                Settings.Default.cfgSipAccountEnabled[this._index] = value.ToString();
            }
        }

        public string HostName
        {
            get
            {
                return Settings.Default.cfgSipAccountAddresses[this._index];
            }
            set
            {
                Settings.Default.cfgSipAccountAddresses[this._index] = value;
            }
        }

        public string Id
        {
            get
            {
                return Settings.Default.cfgSipAccountIds[this._index];
            }
            set
            {
                Settings.Default.cfgSipAccountIds[this._index] = value;
            }
        }

        public int Index
        {
            get
            {
                int num;
                if (int.TryParse(Settings.Default.cfgSipAccountIndex[this._index], out num))
                {
                    return num;
                }
                return -1;
            }
            set
            {
                Settings.Default.cfgSipAccountIndex[this._index] = value.ToString();
            }
        }

        public string Password
        {
            get
            {
                return Settings.Default.cfgSipAccountPassword[this._index];
            }
            set
            {
                Settings.Default.cfgSipAccountPassword[this._index] = value;
            }
        }

        public string ProxyAddress
        {
            get
            {
                return Settings.Default.cfgSipAccountProxyAddresses[this._index];
            }
            set
            {
                Settings.Default.cfgSipAccountProxyAddresses[this._index] = value;
            }
        }

        public int RegState
        {
            get
            {
                int num;
                if (int.TryParse(Settings.Default.cfgSipAccountState[this._index], out num))
                {
                    return num;
                }
                return 0;
            }
            set
            {
                Settings.Default.cfgSipAccountState[this._index] = value.ToString();
            }
        }

        public ETransportMode TransportMode
        {
            get
            {
                int num;
                if (int.TryParse(Settings.Default.cfgSipAccountTransport[this._index], out num))
                {
                    return (ETransportMode) num;
                }
                return ETransportMode.TM_UDP;
            }
            set
            {
                Settings.Default.cfgSipAccountTransport[this._index] = ((int) value).ToString();
            }
        }

        public string UserName
        {
            get
            {
                return Settings.Default.cfgSipAccountNames[this._index];
            }
            set
            {
                Settings.Default.cfgSipAccountNames[this._index] = value;
            }
        }
    }
}
