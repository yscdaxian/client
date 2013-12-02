namespace AgentHelper.SipPhone
{
    using Sipek.Common;
    using Sipek.Sip;
    using System;
    using System.Collections.Generic;
    using AgentHelper.Properties;

    public class SipekConfigurator : IConfiguratorInterface
    {
        public void Save()
        {
            Settings.Default.Save();
        }

        public bool AAFlag
        {
            get
            {
                return Settings.Default.cfgAAFlag;
            }
            set
            {
                Settings.Default.cfgAAFlag = value;
            }
        }

        public List<IAccount> Accounts
        {
            get
            {
                List<IAccount> list = new List<IAccount>();
                for (int i = 0; i < Settings.Default.cfgSipAccountMaxnum; i++)
                {
                    IAccount item = new SipekAccount(i);
                    list.Add(item);
                }
                return list;
            }
        }

        public bool CFBFlag
        {
            get
            {
                return Settings.Default.cfgCFBFlag;
            }
            set
            {
                Settings.Default.cfgCFBFlag = value;
            }
        }

        public string CFBNumber
        {
            get
            {
                return Settings.Default.cfgCFBNumber;
            }
            set
            {
                Settings.Default.cfgCFBNumber = value;
            }
        }

        public bool CFNRFlag
        {
            get
            {
                return Settings.Default.cfgCFNRFlag;
            }
            set
            {
                Settings.Default.cfgCFNRFlag = value;
            }
        }

        public string CFNRNumber
        {
            get
            {
                return Settings.Default.cfgCFNRNumber;
            }
            set
            {
                Settings.Default.cfgCFNRNumber = value;
            }
        }

        public bool CFUFlag
        {
            get
            {
                return Settings.Default.cfgCFUFlag;
            }
            set
            {
                Settings.Default.cfgCFUFlag = value;
            }
        }

        public string CFUNumber
        {
            get
            {
                return Settings.Default.cfgCFUNumber;
            }
            set
            {
                Settings.Default.cfgCFUNumber = value;
            }
        }

        public List<string> CodecList
        {
            get
            {
                List<string> list = new List<string>();
                foreach (string str in Settings.Default.cfgCodecList)
                {
                    list.Add(str);
                }
                return list;
            }
            set
            {
                Settings.Default.cfgCodecList.Clear();
                List<string> list = value;
                foreach (string str in list)
                {
                    Settings.Default.cfgCodecList.Add(str);
                }
            }
        }

        public int DefaultAccountIndex
        {
            get
            {
                return Settings.Default.cfgSipAccountDefault;
            }
            set
            {
                Settings.Default.cfgSipAccountDefault = value;
            }
        }

        public bool DNDFlag
        {
            get
            {
                return Settings.Default.cfgDNDFlag;
            }
            set
            {
                Settings.Default.cfgDNDFlag = value;
            }
        }

        public EDtmfMode DtmfMode
        {
            get
            {
                return (EDtmfMode) Settings.Default.cfgDtmfMode;
            }
            set
            {
                Settings.Default.cfgDtmfMode = (int) value;
            }
        }

        public int ECTail
        {
            get
            {
                SipConfigStruct.Instance.ECTail = Settings.Default.cfgECTail;
                return Settings.Default.cfgECTail;
            }
            set
            {
                Settings.Default.cfgECTail = value;
                SipConfigStruct.Instance.ECTail = value;
            }
        }

        public int Expires
        {
            get
            {
                SipConfigStruct.Instance.expires = Settings.Default.cfgRegistrationTimeout;
                return Settings.Default.cfgRegistrationTimeout;
            }
            set
            {
                Settings.Default.cfgRegistrationTimeout = value;
                SipConfigStruct.Instance.expires = value;
            }
        }

        public bool IfPopup
        {
            get
            {
                return Settings.Default.cfgIfPopup;
            }
            set
            {
                Settings.Default.cfgIfPopup = value;
            }
        }

        public bool IsNull
        {
            get
            {
                return false;
            }
        }

        public string NameServer
        {
            get
            {
                SipConfigStruct.Instance.nameServer = Settings.Default.cfgNameServer;
                return Settings.Default.cfgNameServer;
            }
            set
            {
                Settings.Default.cfgNameServer = value;
                SipConfigStruct.Instance.nameServer = value;
            }
        }

        public string PopupUrl
        {
            get
            {
                return Settings.Default.cfgPopupUrl;
            }
            set
            {
                Settings.Default.cfgPopupUrl = value;
            }
        }

        public bool PublishEnabled
        {
            get
            {
                SipConfigStruct.Instance.publishEnabled = Settings.Default.cfgSipPublishEnabled;
                return Settings.Default.cfgSipPublishEnabled;
            }
            set
            {
                SipConfigStruct.Instance.publishEnabled = value;
                Settings.Default.cfgSipPublishEnabled = value;
            }
        }

        public int SIPPort
        {
            get
            {
                return Settings.Default.cfgSipPort;
            }
            set
            {
                Settings.Default.cfgSipPort = value;
            }
        }

        public string StunServerAddress
        {
            get
            {
                SipConfigStruct.Instance.stunServer = Settings.Default.cfgStunServerAddress;
                return Settings.Default.cfgStunServerAddress;
            }
            set
            {
                Settings.Default.cfgStunServerAddress = value;
                SipConfigStruct.Instance.stunServer = value;
            }
        }

        public bool VADEnabled
        {
            get
            {
                SipConfigStruct.Instance.VADEnabled = Settings.Default.cfgVAD;
                return Settings.Default.cfgVAD;
            }
            set
            {
                Settings.Default.cfgVAD = value;
                SipConfigStruct.Instance.VADEnabled = value;
            }
        }
    }
}
