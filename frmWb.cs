using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MdiTabStrip;
using AgentHelper.Log;
using AgentHelper.SipPhone;
using AgentHelper.Properties;
using WaveLib.AudioMixer;
using WaveLib.WaveServices;
using Sipek.Common;
using Sipek.Common.CallControl;
using AgentHelper.Proxy;
using mshtml;
using System.Runtime.InteropServices;
using System.Reflection;

namespace AgentHelper
{
  
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public partial class frmWb : Form, ICallByWebJs
    {
        [DllImport("csExWBDLMan.dll")]
        public static extern int DllRegisterServer();//注册时用
        [DllImport("csExWBDLMan.dll")]
        public static extern int DllUnregisterServer();

        public ProxyClient proxyClient;
        private SipekResources _resources=null;
        private int _lastMicVol=0;
        private bool mAvoidEvents;
        private Mixers mMixers;
        private Timer tmr = new Timer();
        private TaskbarNotifier taskbar;
        private ProxyCommand proxyCommand;
    
        
        private delegate void DRefreshForm();
        private delegate void DIncomingCall(int sessionId, string number, string info);
        private delegate void DCallJs(int type, string msg);
        private delegate void DRefreshFormUI(bool flag);

        [DllImport("user32.dll", EntryPoint = "ShowWindow", CharSet = CharSet.Auto)]

        public static extern int ShowWindow(IntPtr hwnd, int nCmdShow);

        private AgentHelper.SipPhone.SipekResources SipekResources
        {
            get
            {
                return this._resources;
            }
        }

        public bool IsInitialized
        {
            get
            {
                return this.SipekResources.StackProxy.IsInitialized;
            }
        }


        public frmWb()
        {
            DllRegisterServer();
            InitializeComponent();
            if (Settings.Default.cfgUpdgradeSettings)
            {
                Settings.Default.Upgrade();
                Settings.Default.cfgUpdgradeSettings = false;
            }
            this._resources = new AgentHelper.SipPhone.SipekResources(this);
           
        }

      
        private void OnMakeBusyUpdateUI(bool paused)
        {    
            if (paused)
                this.toolStripButtonMakeBusy.Image = AgentHelper.Properties.Resources.busy;
            else
                this.toolStripButtonMakeBusy.Image = AgentHelper.Properties.Resources.unbusy;
            Settings.Default.isExtBusy = paused;

        }
    

        private void OnProxyEventHandle(String msg)
        {
           
            try
            {
                ProxyEventData proxyEventData = fastJSON.JSON.Instance.ToObject<ProxyEventData>(msg);
              
                if (proxyEventData.eventId == 7){
                    String paused = proxyEventData.eventBody[this.SipekResources.Configurator.Accounts[0].DisplayName];
                    if (base.InvokeRequired)
                        base.Invoke(new DRefreshFormUI(OnMakeBusyUpdateUI), new object[] {Convert.ToBoolean(paused) });
                    else
                        OnMakeBusyUpdateUI(Convert.ToBoolean(paused));
                }

               
                if (proxyEventData.eventId == 1) {
                    string eventData = "{";
                    eventData += "eventId:1";
                    eventData += ",eventName:'AgentStateChanged'";
                    eventData += ",exten:'" + proxyEventData.eventBody["exten"] + "'";
                    eventData += ",releatedNum:'" + proxyEventData.eventBody["relatedCallNum"] + "'";
                    eventData += ",uniqueid:'" + proxyEventData.eventBody["uniqueid"] + "'";
                    eventData += ",floatInfo:'" + proxyEventData.eventBody["floatInfo"] + "'";
                    eventData += ",status:'" + proxyEventData.eventBody["status"] + "'";
                    eventData += "}";
                    msg = eventData;
                }

                if (frmIeWindow.m_CurWB.InvokeRequired){
                    frmIeWindow.m_CurWB.BeginInvoke(new DCallJs(this.CsExWBCallJs), new object[] { 1, msg });
                }
                else
                {
                    this.CsExWBCallJs(1, msg);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.ToString());
            }
            
        }      
        
        private void CsExWBCallJs(int type, string msg)
        {     
            System.Diagnostics.Trace.WriteLine(msg);      
            string jsFunction="onProxyEvent("+type+",\""+msg+"\")";
            try
            {   /*
                IHTMLDocument2 doc2 = (IHTMLDocument2)frmIeWindow.m_CurWB.GetActiveDocument();
                if (!(doc2 == null))
                {                    
                    IHTMLWindow2 wc = doc2.parentWindow;
                    if (!(wc == null))
                        wc.GetType().InvokeMember("onProxyEvent", BindingFlags.InvokeMethod, null, wc, new object[] { type, msg });
                }  
                */

                Settings.Default.callCenterWeb.InvokeScript("onProxyEvent", new object[] { type, msg });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.ToString());
            }
            
        }
        
        private void InitProxyClient()
        {
            this.proxyClient = new ProxyClient();
            this.proxyClient.Host = Settings.Default.cfgProxyAddr;
            this.proxyClient.Port = Settings.Default.cfgProxyPort;
            this.proxyClient.OnProxyEventHandle += OnProxyEventHandle;
            this.proxyClient.init();
            
        }
        private void frmWb_Load(object sender, EventArgs e)
        {
           // fastJSON.JSON.Instance.SerializeNullValues = true;
           // fastJSON.JSON.Instance.ShowReadOnlyProperties = true;
            //fastJSON.JSON.Instance.UseUTCDateTime = true;
            //fastJSON.JSON.Instance.IndentOutput = false;
            //fastJSON.JSON.Instance.UsingGlobalTypes = false;
          
            frmIeWindow.mdiform = this;
            this.proxyCommand = new ProxyCommand();

            InitPage();
            InitSipPhone();
            InitProxyClient();
            InitTimer();

            updateMutexButtonImage();
           
        }
        private void updateMutexButtonImage(){
            if (this.mMixers.Recording.UserLines.GetMixerFirstLineByComponentType(MIXERLINE_COMPONENTTYPE.SRC_MICROPHONE).Mute)
                this.toolStripButtonMutex.Image = AgentHelper.Properties.Resources.mutexRed;
            else
                this.toolStripButtonMutex.Image = AgentHelper.Properties.Resources.mutexGreen;
        }
        private void InitTimer()
        {
            System.Timers.Timer tcpHolderTime =   
            new System.Timers.Timer(10000);  
            //实例化Timer类，设置间隔时间为10000毫秒；   
            tcpHolderTime.Elapsed +=
            new System.Timers.ElapsedEventHandler(tcpHolder_Tick);  
            //到达时间的时候执行事件；   
            tcpHolderTime.AutoReset = true;  
            //设置是执行一次（false）还是一直执行(true)；   
            tcpHolderTime.Enabled = true;  
            //是否执行System.Timers.Timer.Elapsed事件；     
        }

        private void InitPage()
        {
            //显示首页        
            new frmIeWindow(this.tsbx_url,Settings.Default.cfgMainPage) { MdiParent = this }.Show();
            Settings.Default.callCenterWeb = frmIeWindow.m_CurWB;
        }

        private void LoadAudioValues()
        {
            try
            {
                this.mMixers = new Mixers();
            }
            catch (Exception exception)
            {
                new ErrorDialog("Initialize Error " + exception.Message, "Audio Mixer cannot initialize! \r\nCheck audio configuration and start again!").ShowDialog();
                return;
            }
            this.mMixers.Playback.MixerLineChanged += new Mixer.MixerLineChangeHandler(this.mMixer_MixerLineChanged);
            this.mMixers.Recording.MixerLineChanged += new Mixer.MixerLineChangeHandler(this.mMixer_MixerLineChanged);
            MixerLine mixerFirstLineByComponentType = this.mMixers.Playback.UserLines.GetMixerFirstLineByComponentType(MIXERLINE_COMPONENTTYPE.SRC_WAVEOUT);
            this.tss_volumn.Tag = mixerFirstLineByComponentType;
            this.tss_volumn.Text = ((100 * mixerFirstLineByComponentType.Volume) / 0xffff) + "%";
            MixerLine line2 = this.mMixers.Recording.UserLines.GetMixerFirstLineByComponentType(MIXERLINE_COMPONENTTYPE.SRC_MICROPHONE);
            int volume = 0;
            float num2 = 0f;
            if (mixerFirstLineByComponentType.Channels != 2)
            {
                volume = mixerFirstLineByComponentType.Volume;
            }
            else
            {
                mixerFirstLineByComponentType.Channel = Channel.Left;
                int num3 = mixerFirstLineByComponentType.Volume;
                mixerFirstLineByComponentType.Channel = Channel.Right;
                int num4 = mixerFirstLineByComponentType.Volume;
                if (num3 > num4)
                {
                    volume = num3;
                    num2 = (volume > 0) ? -(1f - (((float)num4) / ((float)num3))) : 0f;
                }
                else
                {
                    volume = num4;
                    num2 = (volume > 0) ? (1f - (((float)num3) / ((float)num4))) : 0f;
                }
            }
            if (volume < 0)
            {
            }
            this._lastMicVol = line2.Volume;
        }
        private void mMixer_MixerLineChanged(Mixer mixer, MixerLine line)
        {
            this.mAvoidEvents = true;
            try
            {
                updateMutexButtonImage();
                
                
                float num = -1f;
                MixerLine tag = (MixerLine)this.sb_urlinfo.Tag;
                if (tag == line)
                {
                    int volume = 0;
                    if (line.Channels != 2)
                    {
                        volume = line.Volume;
                    }
                    else
                    {
                        line.Channel = Channel.Left;
                        int num3 = line.Volume;
                        line.Channel = Channel.Right;
                        int num4 = line.Volume;
                        if (num3 > num4)
                        {
                            volume = num3;
                            if ((num3 != 0) && (num4 != 0))
                            {
                                num = (volume > 0) ? -(1f - (((float)num4) / ((float)num3))) : 0f;
                            }
                        }
                        else
                        {
                            volume = num4;
                            if ((num3 != 0) && (num4 != 0))
                            {
                                num = (volume > 0) ? (1f - (((float)num3) / ((float)num4))) : 0f;
                            }
                        }
                    }
                    if (volume >= 0)
                    {
                        this.tss_volumn.Text = ((100 * volume) / 0xffff) + "%";
                    }
                }
            }
            finally
            {
                this.mAvoidEvents = false;
            }
        }
        private void CallManager_IncomingCallNotification(int sessionId, string number, string info)
        {
            this.BeginInvoke(new DIncomingCall(this.OnIncomingCall), new object[] { sessionId, number, info });
            if (base.InvokeRequired)
            {
                //this.BeginInvoke(new DIncomingCall(this.OnIncomingCall), new object[] { sessionId, number, info });
            }
            else
            {
                //this.OnIncomingCall(sessionId, number, info);
            }
             
        }

        public  void onAccountStateChanged(int accId, int accState)
        {
            if (base.InvokeRequired)
            {
                base.BeginInvoke(new DRefreshForm(this.UpdateAccountState));
            }
            else
            {
                this.UpdateAccountState();
            }
        }

        private void UpdateAccountState()
        {
            try
            {
                string accountName;
                string str2;
                IAccount account = this.SipekResources.Configurator.Accounts[0];
                if (account.AccountName.Length == 0)
                {
                    accountName = "--no account--";
                }
                else
                {
                    accountName = account.AccountName;
                }
                switch (account.RegState)
                {
                    case -1:
                        if (account.HostName.Length == 0)
                        {
                            str2 = "Empty";
                        }
                        else
                        {
                            str2 = "Error";
                        }
                        break;

                    case 0:
                        str2 = "Trying";
                        break;

                    case 200:
                        str2 = "Idle";
                        this.ts_keybad.Enabled = true;
                        this.ts_answer.Enabled = true;
                        break;

                    default:
                        str2 = "Registration Error  (" + account.RegState.ToString() + ")";
                        break;
                }
                this.tss_accounts.Text = accountName + "<" + str2 + ">";
            }
            catch
            {
            }
        }
        public  void onBuddyStateChanged(int buddyId, int status, string text)
        {
            if (!base.InvokeRequired)
            {
            }
        }
        public  void onCallStateChanged(int sessionId)
        {
            if (base.InvokeRequired)
            {
                base.BeginInvoke(new DRefreshForm(this.UpdateCallLines));
            }
            else
            {
                this.UpdateCallLines();
            }
        }

        private void UpdateCallLines()
        {
            try
            {
                int num = 0;
                Dictionary<int, IStateMachine> callList = this.SipekResources.CallManager.CallList;
                if (callList.Count <= 0)
                {
                    this.tss_accounts.Text = "IDLE" + "";
                    this.ts_answer.Enabled = true;
                    this.ts_hungup.Enabled = false;
                }
                foreach (KeyValuePair<int, IStateMachine> pair in callList)
                {
                    if (++num > 1)
                    {
                        return;
                    }
                    this.ts_answer.Enabled = false;
                    this.ts_hungup.Enabled = false;
                    EStateId stateId = pair.Value.StateId;
                    if (stateId <= EStateId.RELEASED)
                    {
                        switch (stateId)
                        {
                            case EStateId.NULL:
                                break;

                            case EStateId.IDLE:
                                {
                                    this.ts_answer.Enabled = true;
                                    this.ts_hungup.Enabled = false;
                                }
                                break;

                            case EStateId.CONNECTING:
                                {
                                    if (pair.Value.Incoming)
                                    {
                                        this.ts_answer.Enabled = true;
                                    }
                                    this.ts_hungup.Enabled = true;
                                    break;
                                   
                                }
                            case EStateId.ALERTING:
                                if (pair.Value.Incoming)
                                {
                                    this.ts_answer.Enabled = true;
                                }
                                break;

                            case EStateId.ACTIVE:
                                this.ts_hungup.Enabled = true;
                                if (this._resources.Configurator.IfPopup && pair.Value.Incoming)
                                {
                                    // string msg = EventToCallWebJs.CreateIncomingCallBySipPhoneEvent(pair.Value.CallingNumber);
                                    //frmIeWindow.m_CurWB.BeginInvoke(new DCallJs(this.CsExWBCallJs), new object[] { 10, msg }); 
                                    //string url = string.Format(this._resources.Configurator.PopupUrl, pair.Value.CallingNumber);
                                    //new frmIeWindow(this.tsbx_url, url) { MdiParent = this }.Show();
                                }
                                break;

                            case EStateId.RELEASED:
                                {
                                    this.ts_answer.Enabled = true;
                                }
                                break;
                        }
                    }
                    else
                    {
                        if (stateId == EStateId.INCOMING)
                        {
                            if (pair.Value.Incoming)
                            {
                                this.ts_hungup.Enabled = true;
                                this.ts_answer.Enabled = true;
                            }

                        }
                        if ((stateId != EStateId.HOLDING) && (stateId == EStateId.TERMINATED))
                        {
                            this.ts_answer.Enabled = true;
                        }
                    }
                    this.tss_accounts.Text = pair.Value.StateId + "";
                   
                }
                
            }
            catch
            {
            }
        }
        public  void onMessageReceived(string from, string message)
        {
            if (!base.InvokeRequired)
            {
            }
        }
        public  void onMessageWaitingIndication(int mwi, string text)
        {
            if (!base.InvokeRequired)
            {
            }
        }
        private void InitSipPhone()
        {
            this.ts_answer.Enabled = false;
            this.ts_hungup.Enabled = false;
            this.ts_keybad.Enabled = false;
            this.LoadAudioValues();
            this.SipekResources.CallManager.CallStateRefresh += new DCallStateRefresh(this.onCallStateChanged);
            this.SipekResources.CallManager.IncomingCallNotification += new DIncomingCallNotification(this.CallManager_IncomingCallNotification);
            this.SipekResources.Messenger.MessageReceived += new DMessageReceived(this.onMessageReceived);
            this.SipekResources.Messenger.BuddyStatusChanged += new DBuddyStatusChanged(this.onBuddyStateChanged);
            this.SipekResources.Registrar.AccountStateChanged += new DAccountStateChanged(this.onAccountStateChanged);
            this.SipekResources.StackProxy.MessageWaitingIndication += new DMessageWaitingNotification(this.onMessageWaitingIndication);
            int num = this.SipekResources.CallManager.Initialize();
            this.SipekResources.CallManager.CallLogger = this.SipekResources.CallLogger;
            
            if (num != 0)
            {
                new ErrorDialog("Initialize Error", "Init SIP stack problem! \r\nPlease, check configuration and start again! \r\nStatus code " + num).ShowDialog();
            }
            else
            {
                //this.SipekResources.Registrar.registerAccounts();
                int num2 = this.SipekResources.StackProxy.getNoOfCodecs();
                for (int i = 0; i < num2; i++)
                {
                    string item = this.SipekResources.StackProxy.getCodec(i);
                    if (this.SipekResources.Configurator.CodecList.Contains(item))
                    {
                        this.SipekResources.StackProxy.setCodecPriority(item, 0x80);
                    }
                    else
                    {
                        this.SipekResources.StackProxy.setCodecPriority(item, 0);
                    }
                }
                
                this.tmr.Interval = 0x3e8;
                this.taskbar = new TaskbarNotifier();
                this.taskbar.CloseClickable = true;
                this.taskbar.TitleClickable = false;
                this.taskbar.ContentClickable = true;
                this.taskbar.EnableSelectionRectangle = true;
                this.taskbar.KeepVisibleOnMousOver = false;
                this.taskbar.ReShowOnMouseOver = true;
                this.taskbar.SetBackgroundBitmap(Resources.skin, Color.FromArgb(0xff, 0, 0xff));
                this.taskbar.SetCloseBitmap(Resources.busy1, Color.FromArgb(0xff, 0xff, 0xff), new Point(280,0));
                this.taskbar.TitleRectangle = new Rectangle(80, 55, 0xb0, 0x10);
                this.taskbar.ContentRectangle = new Rectangle(60, 35, 250, 100);
                this.taskbar.ContentClick += new EventHandler(this.taskbar_ContentClick);
                this.taskbar.CloseClick += new EventHandler(this.taskbar_CloseClick);
                this.taskbar.TopMost = true;
            }
        }

        private void frmWb_Layout(object sender, LayoutEventArgs e)
        {
            int height = this.tsbx_url.Height;
            this.tsbx_url.Size = new Size(base.Width -548, height);
        }

        private void tsbx_url_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Return)
                {
                    e.Handled = true;
                    frmIeWindow.m_CurWB.Navigate(this.tsbx_url.Text);
                }
            }
            catch (Exception exception)
            {
                Loger.WriteLog(exception.ToString());
            }

        }

        private void NewLabelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmIeWindow(this.tsbx_url,this.tsbx_url.Text) { MdiParent = this }.Show();
        }

        public  void SetGoBackBtn()
        {
            this.ts_bk.Enabled = frmIeWindow.m_CurWB.CanGoBack;
            this.ts_go.Enabled = frmIeWindow.m_CurWB.CanGoForward;
        }

        private void ts_bk_Click(object sender, EventArgs e)
        {
            frmIeWindow.m_CurWB.GoBack();
        }

        private void ts_go_ButtonClick(object sender, EventArgs e)
        {
            frmIeWindow.m_CurWB.GoForward();
        }

        private void ts_setting_Click(object sender, EventArgs e)
        {
            new settingForm(this._resources).ShowDialog();
        }

        private void OnIncomingCall(int sessionId, string number, string info)
        {
              UpdateCallLines();
              this.PopupCallin(sessionId, number, info);
  
        }
        private void PopupCallin(int sessionId, string number, string info)
        {
            int nTimeToShow = 500;
            int nTimeToStay = Settings.Default.cfgPopupShowTime;
            int nTimeToHide = 500;
            this.ts_telnum.Text = "";
            this.taskbar.Show("",  number + "来电", nTimeToShow, nTimeToStay, nTimeToHide);
            //this.taskbar.Show("","", nTimeToShow, nTimeToStay, nTimeToHide);
            this.taskbar.Tag = sessionId;
            try
            {
                string msg = EventToCallWebJs.CreateIncomingCallBySipPhoneEvent(number);             
                frmIeWindow.m_CurWB.BeginInvoke(new DCallJs(this.CsExWBCallJs), new object[] { 0, msg });
            }catch(Exception ex){
                
            }

        }

      
        private void ts_hungup_Click(object sender, EventArgs e)
        {
            if (this.ts_hungup.Enabled)
            {
                try
                {
                    Dictionary<int, IStateMachine> callList = this.SipekResources.CallManager.CallList;
                    foreach (KeyValuePair<int, IStateMachine> pair in callList)
                    {
                        this.SipekResources.CallManager.OnUserRelease(pair.Value.Session);
                    }
                    this.ts_answer.Enabled = true;
                    this.ts_hungup.Enabled = false;
                    this.ts_telnum.Text = "";
                }
                catch (Exception ex)
                { 
                }
            }
        }

        private void ts_keybad_Click(object sender, EventArgs e)
        {
            KeyboardForm form = new KeyboardForm(this);
            form.Location = new Point(form.PointToClient(Control.MousePosition).X,Control.MousePosition.Y);
            form.ShowDialog();

        }
        public  void onUserDialDigit(string digits)
        {   //如果是
            if (digits.Equals("c") || digits.Equals("b"))
            {
                if (digits.Equals("c"))
                    this.ts_telnum.Text = "";
                if (digits.Equals("b") && this.ts_telnum.Text.ToString().Count() >0)
                {
                    this.ts_telnum.Text=this.ts_telnum.Text.Remove(this.ts_telnum.Text.ToString().Count() - 1);
                }
                return;
            }

            if (this.SipekResources.CallManager.CallList.Count <= 0)
            {
                this.ts_telnum.Text = this.ts_telnum.Text + digits;
            }
            else
            {
                int num = 0;
                Dictionary<int, IStateMachine> callList = this.SipekResources.CallManager.CallList;
                foreach (KeyValuePair<int, IStateMachine> pair in callList)
                {
                    if (++num > 1)
                    {
                        break;
                    }
                    if (pair.Value.StateId == EStateId.ACTIVE)
                    {
                        this.SipekResources.CallManager.OnUserDialDigit(pair.Value.Session, digits, EDtmfMode.DM_Outband);
                    }
                }
                this.ts_telnum.Text = this.ts_telnum.Text + digits;
            }
        }
        private void ts_answer_Click(object sender, EventArgs e)
        {
            if (this.ts_answer.Enabled)
            {
                int num = 0;
                if (this.SipekResources.CallManager.CallList.Count <= 0)
                {
                    this.SipekResources.CallManager.CreateSmartOutboundCall(this.ts_telnum.Text, 0);
                    this.ts_telnum.Items.Add(this.ts_telnum.Text);
                    
                }
                else
                {
                    Dictionary<int, IStateMachine> callList = this.SipekResources.CallManager.CallList;
                    foreach (KeyValuePair<int, IStateMachine> pair in callList)
                    {
                        if (++num > 1)
                        {
                            break;
                        }
                        if (pair.Value.StateId == EStateId.IDLE)
                        {
                            this.SipekResources.CallManager.CreateSmartOutboundCall(this.ts_telnum.Text, 0);
                        }
                        else
                        {
                            this.SipekResources.CallManager.OnUserAnswer(pair.Value.Session);
                        }
                    }
                }
                this.ts_answer.Enabled = false;
                this.ts_hungup.Enabled = true;
            }
        }

        private void tsmi_close_curpage_Click(object sender, EventArgs e)
        {
            frmIeWindow.m_CurPage.Close();
        }

        private void tsmi_close_other_all_Click(object sender, EventArgs e)
        {
            foreach (frmIeWindow window in frmIeWindow.all_Pages)
            {
                if (window != frmIeWindow.m_CurPage)
                {
                    window.Close();
                }
            }
        }

        private void tsmi_close_all_Click(object sender, EventArgs e)
        {
            foreach (frmIeWindow window in frmIeWindow.all_Pages)
            {
                window.Close();
            }
        }

        private void tsmi_refreash_Click(object sender, EventArgs e)
        {
            frmIeWindow.m_CurWB.Refresh();
        }

        private void taskbar_CloseClick(object sender, EventArgs e)
        {
            this.taskbar.Hide();
        }
        private void toolStripButtonHold_Click(object sender, EventArgs e)
        {
            int num = 0;
            if (this.SipekResources.CallManager.CallList.Count > 0)
            {
                Dictionary<int, IStateMachine> callList = this.SipekResources.CallManager.CallList;
                foreach (KeyValuePair<int, IStateMachine> pair in callList)
                {
                    if (++num > 1)
                    {
                        break;
                    }
                    if (pair.Value.StateId == EStateId.ACTIVE || pair.Value.StateId == EStateId.HOLDING)
                    {
                        this.SipekResources.CallManager.OnUserHoldRetrieve(pair.Value.Session);
                    }
                }
            }
        }
        private void toolStripButtonBusy_Click(object sender, EventArgs e)
        {
            String cmd = this.proxyCommand.ProxyExtMakeBusy(Settings.Default.cfgSipAccountNames[0], !Settings.Default.isExtBusy);
            this.proxyClient.sendCommand(cmd);
            String eventData=EventToCallWebJs.CreateOnClickMakeBusyButtonEvent((!Settings.Default.isExtBusy).ToString().ToLower());
           
            try
            {
                if (frmIeWindow.m_CurWB.InvokeRequired)
                {
                    frmIeWindow.m_CurWB.BeginInvoke(new DCallJs(this.CsExWBCallJs), new object[] { 0, eventData });
                }
                else
                {
                    this.CsExWBCallJs(0, eventData);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.ToString());
            }
        }
        private void taskbar_ContentClick(object sender, EventArgs e)
        {
            ShowWindow(this.Handle,3);
            if (Settings.Default.cfgPopupAnswerCall) {
                this.ts_answer_Click(null,null);
            }
            
        }


        //供web调用的接口
        public void AgentCall(String agentId,String phoneNumber)
        {
            if (!phoneNumber.Equals(""))
            {
                ts_telnum.Text = phoneNumber;
                ts_answer_Click(null, null);
            }
        }

        public void AgentHangup(String agentId) {
            try
            {
                Dictionary<int, IStateMachine> callList = this.SipekResources.CallManager.CallList;
                foreach (KeyValuePair<int, IStateMachine> pair in callList)
                {
                    this.SipekResources.CallManager.OnUserRelease(pair.Value.Session);
                }
                this.ts_answer.Enabled = true;
                this.ts_hungup.Enabled = false;
                this.ts_telnum.Text = "";
            }
            catch (Exception ex)
            {
            }
           
        }
       
        public void AgentQueueLogin(String agentId, String pwd, String ext)
        {
          
        }
        public void AgentQueuePause(String agnetId, String queueId,Boolean isPaused)
        {    
            this.proxyClient.sendCommand( this.proxyCommand.ProxyQueuePause(agnetId,queueId,isPaused));
        }
        public void AgentTransfer(String oldExt, String newExt, int mod) 
        {
            this.proxyClient.sendCommand(this.proxyCommand.ProxyTransfer(oldExt,newExt,mod));
        }

        public void AgentMakeBusy(String agentId, Boolean busy) 
        {
            if (agentId != ""){
                String cmd = this.proxyCommand.ProxyExtMakeBusy(agentId, busy);
                this.proxyClient.sendCommand(cmd);
            }

        }
        public void AgentNwayCallStart(String agentId, String conference)
        {
            this.proxyClient.sendCommand(this.proxyCommand.ProxyNwayCallStart(agentId, conference));
        }

        public void AgentNwayCallAddOne(String agentId, String another, String conference, String callerId="98888",int time=6000)
        {
            this.proxyClient.sendCommand(this.proxyCommand.ProxyNwayCallAddOne(another,conference,callerId,time));
        }

        public void AgentForceInsert(String agentId, String targetAgent){
            this.proxyClient.sendCommand(this.proxyCommand.ProxyForceInsertCall(agentId, targetAgent));
        }
        public void AgentHangupCall(String agentId, String targetAgent) {
            this.proxyClient.sendCommand(this.proxyCommand.ProxyHangupCall(agentId, targetAgent));
        }
        public void AgentSpyCall(String agentId, String targetAgent) {
            this.proxyClient.sendCommand(this.proxyCommand.ProxySpyCall(agentId, targetAgent));
        }

        public void ExtLogin(String ext, String pwd, String host="") {
            if (host == "")
                host = this.SipekResources.Configurator.Accounts[0].HostName;
            this.SipekResources.Configurator.Accounts[0].AccountName = ext;
            this.SipekResources.Configurator.Accounts[0].DisplayName = ext;
            this.SipekResources.Configurator.Accounts[0].UserName = ext;
            this.SipekResources.Configurator.Accounts[0].HostName = host;
            this.SipekResources.Configurator.Accounts[0].ProxyAddress = host;
            this.SipekResources.Configurator.Accounts[0].Password = pwd;
         
            this.SipekResources.Registrar.registerAccounts();

            //向代理发送坐席登录消息
            this.proxyClient.sendCommand(this.proxyCommand.ProxyAgentLogin(ext));
        }

        public void ExtLoginOut(String ext) {   
            //向代理发送坐席登录消息
            this.proxyClient.sendCommand(this.proxyCommand.ProxyAgentLoginOut(ext));
            this.SipekResources.Registrar.unregisterAccounts();
        }

        private void tcpHolder_Tick(object sender, EventArgs e)
        {
            if (this.proxyClient != null && !this.proxyClient.isConnected()){
               
                this.proxyClient.reConnect();       
            }      
        }

        private void frmWb_FormClosed(object sender, FormClosedEventArgs e)
        {  
            //挂断通话
            this.ts_hungup_Click(null, null);
            ExtLoginOut(Settings.Default.cfgSipAccountNames[0]);
            //注销sip
            this.SipekResources.Registrar.unregisterAccounts();

            System.Threading.Thread.Sleep(500); 
            this.proxyClient.Close();
        }


        private void toolStripButtonTransfer_Click(object sender, EventArgs e)
        {
            /*
            String eventData=EventToCallWebJs.CreateOnClickTransferButonBeginEvent();
            try{
                if (frmIeWindow.m_CurWB.InvokeRequired){
                    frmIeWindow.m_CurWB.BeginInvoke(new DCallJs(this.CsExWBCallJs), new object[] { 0, eventData });
                }
                else
                {
                    this.CsExWBCallJs(0, eventData);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.ToString());
            }
             */
            AgentForm agentForm = new AgentForm();
            agentForm.mainForm = this;
            this.proxyClient.OnProxyEventHandle += agentForm.OnProxyEventHandle;
            agentForm.StartPosition = FormStartPosition.CenterParent;
            agentForm.ShowDialog();
            
        }

        private void tsmi_as_callcenterWeb_Click(object sender, EventArgs e)
        {
            Settings.Default.callCenterWeb = frmIeWindow.m_CurWB;
            
        }

        private void toolStripButtonMutex_Click(object sender, EventArgs e)
        {
            if (this.mMixers.Recording.UserLines.GetMixerFirstLineByComponentType(MIXERLINE_COMPONENTTYPE.SRC_MICROPHONE).Mute)
                this.mMixers.Recording.UserLines.GetMixerFirstLineByComponentType(MIXERLINE_COMPONENTTYPE.SRC_MICROPHONE).Mute = false;
           else
               this.mMixers.Recording.UserLines.GetMixerFirstLineByComponentType(MIXERLINE_COMPONENTTYPE.SRC_MICROPHONE).Mute = true;
        }
       
       
    }
}
