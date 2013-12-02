using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sipek.Common;
using WaveLib.AudioMixer;
using WaveLib.WaveServices;
using AgentHelper.Properties;

namespace AgentHelper.SipPhone
{
    public partial class settingForm : Form
    {
        private int _lastMicVolume = 0;
        private AgentHelper.SipPhone.SipekResources _resources = null;
        private bool _reregister = false;
        private bool _restart = false;
        private Mixers mMixers;
        private bool mAvoidEvents;

        public AgentHelper.SipPhone.SipekResources SipekResources
        {
            get
            {
                return this._resources;
            }
        }
        private bool ReregisterRequired
        {
            get
            {
                return this._reregister;
            }
            set
            {
                this._reregister = value;
            }
        }

        private bool RestartRequired
        {
            get
            {
                return this._restart;
            }
            set
            {
                this._restart = value;
            }
        }
        public settingForm(AgentHelper.SipPhone.SipekResources resources)
        {
            InitializeComponent();
            this._resources = resources;
        }

        private void comboBoxSIPTransport_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = this.comboBoxAccounts.SelectedIndex;
            if ((selectedIndex >= 0) && (selectedIndex < 5))
            {
                this.SipekResources.Configurator.Accounts[selectedIndex].TransportMode = (ETransportMode)this.comboBoxSIPTransport.SelectedIndex;
            }

        }
        private void updateAccountList()
        {
            int count = this.SipekResources.Configurator.Accounts.Count;
            this.comboBoxAccounts.Items.Clear();
            for (int i = 0; i < count; i++)
            {
                IAccount account = this.SipekResources.Configurator.Accounts[i];
                if (account.AccountName.Length == 0)
                {
                    this.comboBoxAccounts.Items.Add("--empty--");
                }
                else
                {
                    this.comboBoxAccounts.Items.Add(account.AccountName);
                }
            }
        }
        private float adjustValues(MixerLine line, TrackBar tBar)
        {
            float num = -1f;
            MixerLine tag = (MixerLine)tBar.Tag;
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
                    tBar.Value = volume;
                }
            }
            return num;
        }
        private void mMixer_MixerLineChanged(Mixer mixer, MixerLine line)
        {
            this.mAvoidEvents = true;
            try
            {
                float num;
                if (line.Direction == MixerType.Playback)
                {
                    num = this.adjustValues(line, this.trackBarPlaybackVolume);
                    if ((num != -1f) && (((MixerLine)this.trackBarPlaybackBalance.Tag) == line))
                    {
                        this.trackBarPlaybackBalance.Value = (int)(this.trackBarPlaybackBalance.Maximum * num);
                    }
                    this.checkBoxPlaybackMute.Checked = line.Mute;
                }
                else if (line.Direction == MixerType.Recording)
                {
                    line.Channel = Channel.Uniform;
                    num = this.adjustValues(line, this.trackBarRecordingVolume);
                    this.checkBoxRecordingMute.Checked = line.Volume == 0;
                }
            }
            finally
            {
                this.mAvoidEvents = false;
            }
        }
        private void LoadDeviceCombos(Mixers mixers)
        {
            MixerDetail item = new MixerDetail
            {
                DeviceId = -1,
                MixerName = "Default",
                SupportWaveOut = true
            };
            this.comboBoxPlaybackDevices.Items.Add(item);
            foreach (MixerDetail detail2 in mixers.Playback.Devices)
            {
                this.comboBoxPlaybackDevices.Items.Add(detail2);
            }
            this.comboBoxPlaybackDevices.SelectedIndex = 0;
            item = new MixerDetail
            {
                DeviceId = -1,
                MixerName = "Default",
                SupportWaveIn = true
            };
            this.comboBoxRecordingDevices.Items.Add(item);
            foreach (MixerDetail detail2 in mixers.Recording.Devices)
            {
                this.comboBoxRecordingDevices.Items.Add(detail2);
            }
            this.comboBoxRecordingDevices.SelectedIndex = 0;
        }
        private void settingForm_Load(object sender, EventArgs e)
        {
            this.updateAccountList();
            this.comboBoxAccounts.SelectedIndex = this.SipekResources.Configurator.DefaultAccountIndex;
            this.checkBoxDND.Checked = this.SipekResources.Configurator.DNDFlag;
            this.checkBoxAA.Checked = this.SipekResources.Configurator.AAFlag;
            this.checkBoxCFU.Checked = this.SipekResources.Configurator.CFUFlag;
            this.checkBoxCFNR.Checked = this.SipekResources.Configurator.CFNRFlag;
            this.checkBoxCFB.Checked = this.SipekResources.Configurator.CFBFlag;
            this.checkBoxPopup.Checked = this.SipekResources.Configurator.IfPopup;
            this.textBoxurl.Text = this.SipekResources.Configurator.PopupUrl;
            this.textBoxCFU.Text = this.SipekResources.Configurator.CFUNumber;
            this.textBoxCFNR.Text = this.SipekResources.Configurator.CFNRNumber;
            this.textBoxCFB.Text = this.SipekResources.Configurator.CFBNumber;
            this.textBoxListenPort.Text = this.SipekResources.Configurator.SIPPort.ToString();
            this.textBoxStunServerAddress.Text = this.SipekResources.Configurator.StunServerAddress;
            this.comboBoxDtmfMode.SelectedIndex = (int)this.SipekResources.Configurator.DtmfMode;
            this.checkBoxPublish.Checked = this.SipekResources.Configurator.PublishEnabled;
            this.textBoxExpires.Text = this.SipekResources.Configurator.Expires.ToString();
            this.checkBoxVAD.Checked = this.SipekResources.Configurator.VADEnabled;
            this.textBoxECTail.Text = this.SipekResources.Configurator.ECTail.ToString();
            this.textBoxNameServer.Text = this.SipekResources.Configurator.NameServer;
            try
            {
                this.mMixers = new Mixers();
                this.mMixers.Playback.MixerLineChanged += new Mixer.MixerLineChangeHandler(this.mMixer_MixerLineChanged);
                this.mMixers.Recording.MixerLineChanged += new Mixer.MixerLineChangeHandler(this.mMixer_MixerLineChanged);
                this.LoadDeviceCombos(this.mMixers);
            }
            catch (Exception exception)
            {
                new ErrorDialog("Initialize Error " + exception.Message, "Audio Mixer cannot initialize! \r\nCheck audio configuration and start again!").ShowDialog();
            }
            if (this.SipekResources.StackProxy.IsInitialized)
            {
                int num = this.SipekResources.StackProxy.getNoOfCodecs();
                for (int i = 0; i < num; i++)
                {
                    string item = this.SipekResources.StackProxy.getCodec(i);
                    this.listBoxDisCodecs.Items.Add(item);
                }
                List<string> codecList = this.SipekResources.Configurator.CodecList;
                foreach (string str2 in codecList)
                {
                    if (this.listBoxDisCodecs.Items.Contains(str2))
                    {
                        this.listBoxDisCodecs.Items.Remove(str2);
                        this.listBoxEnCodecs.Items.Add(str2);
                    }
                }
            }
            this.ReregisterRequired = false;
            this.RestartRequired = false;

            this.textMainPage.Text = Settings.Default.cfgMainPage;
            this.textProxyAddr.Text = Settings.Default.cfgProxyAddr;
            this.textProxyPort.Text = Settings.Default.cfgProxyPort.ToString();
            this.textBoxPopupShowTime.Text = Settings.Default.cfgPopupShowTime.ToString();
            this.checkBoxPopupAnswerCall.Checked = Settings.Default.cfgPopupAnswerCall;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            this.buttonApply_Click(sender, e);
            this.SipekResources.Configurator.Save();
            if (this.SipekResources.StackProxy.IsInitialized)
            {
                foreach (string str in this.listBoxDisCodecs.Items)
                {
                    this.SipekResources.StackProxy.setCodecPriority(str, 0);
                }
                int num = 0;
                foreach (string str in this.listBoxEnCodecs.Items)
                {
                    this.SipekResources.StackProxy.setCodecPriority(str, 0x80 - num);
                    num++;
                }
            }
            if (this.RestartRequired)
            {
                this.SipekResources.StackProxy.initialize();
            }
            if (this.ReregisterRequired)
            {
                this.SipekResources.Registrar.registerAccounts();
            }
            this.SipekResources.StackProxy.setSoundDevice(this.mMixers.Playback.DeviceDetail.MixerName, this.mMixers.Recording.DeviceDetail.MixerName);
            base.Close();

        }

        private void buttonEnable_Click(object sender, EventArgs e)
        {
            if (this.listBoxDisCodecs.SelectedItems.Count > 0)
            {
                this.listBoxEnCodecs.Items.Add(this.listBoxDisCodecs.SelectedItem);
                this.listBoxDisCodecs.Items.Remove(this.listBoxDisCodecs.SelectedItem);
            }

        }

        private void buttonDisable_Click(object sender, EventArgs e)
        {
            if (this.listBoxEnCodecs.SelectedItems.Count > 0)
            {
                this.listBoxDisCodecs.Items.Add(this.listBoxEnCodecs.SelectedItem);
                this.listBoxEnCodecs.Items.Remove(this.listBoxEnCodecs.SelectedItem);
            }

        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            int num = 0;
            if (num >= 0)
            {
                IAccount account = this.SipekResources.Configurator.Accounts[num];
                account.Enabled = this.checkBoxAccountEnabled.Checked;
                account.HostName = this.textBoxRegistrarAddress.Text;
                account.ProxyAddress = this.textBoxProxyAddress.Text;
                account.AccountName = this.textBoxAccountName.Text;
                account.DisplayName = this.textBoxDisplayName.Text;
                account.Id = this.textBoxUsername.Text;
                account.UserName = this.textBoxUsername.Text;
                account.Password = this.textBoxPassword.Text;
                account.DomainName = this.textBoxDomain.Text;
                account.TransportMode = (ETransportMode)this.comboBoxSIPTransport.SelectedIndex;
                if (this.checkBoxDefault.Checked)
                {
                    this.SipekResources.Configurator.DefaultAccountIndex = num;
                }
            }
            this.SipekResources.Configurator.DNDFlag = this.checkBoxDND.Checked;
            this.SipekResources.Configurator.AAFlag = this.checkBoxAA.Checked;
            this.SipekResources.Configurator.CFUFlag = this.checkBoxCFU.Checked;
            this.SipekResources.Configurator.CFNRFlag = this.checkBoxCFNR.Checked;
            this.SipekResources.Configurator.CFBFlag = this.checkBoxCFB.Checked;
            this.SipekResources.Configurator.CFUNumber = this.textBoxCFU.Text;
            this.SipekResources.Configurator.CFNRNumber = this.textBoxCFNR.Text;
            this.SipekResources.Configurator.CFBNumber = this.textBoxCFB.Text;
            this.SipekResources.Configurator.SIPPort = short.Parse(this.textBoxListenPort.Text);
            this.SipekResources.Configurator.StunServerAddress = this.textBoxStunServerAddress.Text;
            this.SipekResources.Configurator.PublishEnabled = this.checkBoxPublish.Checked;
            this.SipekResources.Configurator.Expires = int.Parse(this.textBoxExpires.Text);
            this.SipekResources.Configurator.VADEnabled = this.checkBoxVAD.Checked;
            this.SipekResources.Configurator.ECTail = int.Parse(this.textBoxECTail.Text);
            this.SipekResources.Configurator.NameServer = this.textBoxNameServer.Text;
            this.SipekResources.Configurator.PopupUrl = this.textBoxurl.Text;
            this.SipekResources.Configurator.IfPopup = this.checkBoxPopup.Checked;
            if (this.SipekResources.StackProxy.IsInitialized)
            {
                if (this.listBoxEnCodecs.Items.Count == 0)
                {
                    new ErrorDialog("Settings Warning", "No codec selected!").ShowDialog();
                }
                else
                {
                    List<string> list = new List<string>();
                    foreach (string str in this.listBoxEnCodecs.Items)
                    {
                        list.Add(str);
                    }
                    this.SipekResources.Configurator.CodecList = list;
                }
            }

            Settings.Default.cfgMainPage = this.textMainPage.Text;
            Settings.Default.cfgProxyAddr = this.textProxyAddr.Text;
            Settings.Default.cfgProxyPort = short.Parse(this.textProxyPort.Text);
            Settings.Default.cfgPopupShowTime = short.Parse(this.textBoxPopupShowTime.Text);
            Settings.Default.cfgPopupAnswerCall = this.checkBoxPopupAnswerCall.Checked;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            base.Close();
        }
        private void reregistrationRequired_TextChanged(object sender, EventArgs e)
        {
            this.ReregisterRequired = true;
        }

        private void comboBoxAccounts_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = this.comboBoxAccounts.SelectedIndex;
            if (this.SipekResources.Configurator.DefaultAccountIndex == selectedIndex)
            {
                this.checkBoxDefault.Checked = true;
            }
            else
            {
                this.checkBoxDefault.Checked = false;
            }
            IAccount account = this.SipekResources.Configurator.Accounts[selectedIndex];
            if (account == null)
            {
                this.clearAll();
            }
            else
            {
                this.checkBoxAccountEnabled.Checked = account.Enabled;
                this.textBoxAccountName.Text = account.AccountName;
                this.textBoxDisplayName.Text = account.DisplayName;
                this.textBoxUsername.Text = account.UserName;
                this.textBoxPassword.Text = account.Password;
                this.textBoxRegistrarAddress.Text = account.HostName;
                this.textBoxProxyAddress.Text = account.ProxyAddress;
                this.textBoxDomain.Text = account.DomainName;
                this.comboBoxSIPTransport.SelectedIndex = (int)account.TransportMode;
            }
        }

        private void clearAll()
        {
            this.textBoxAccountName.Text = "";
            this.textBoxDisplayName.Text = "";
            this.textBoxUsername.Text = "";
            this.textBoxPassword.Text = "";
            this.textBoxRegistrarAddress.Text = "";
            this.textBoxProxyAddress.Text = "";
            this.textBoxDomain.Text = "*";
        }
        private void comboBoxPlaybackDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadValues(MixerType.Playback);
        }
        private void LoadValues(MixerType mixerType)
        {
            MixerLine mixerFirstLineByComponentType;
            if (mixerType == MixerType.Recording)
            {
                this.mMixers.Recording.DeviceId = ((MixerDetail)this.comboBoxRecordingDevices.SelectedItem).DeviceId;
                mixerFirstLineByComponentType = this.mMixers.Recording.UserLines.GetMixerFirstLineByComponentType(MIXERLINE_COMPONENTTYPE.SRC_MICROPHONE);
                this.trackBarRecordingVolume.Tag = mixerFirstLineByComponentType;
                this.trackBarRecordingBalance.Tag = mixerFirstLineByComponentType;
                this.checkBoxRecordingMute.Tag = mixerFirstLineByComponentType;
                this._lastMicVolume = mixerFirstLineByComponentType.Volume;
                mixerFirstLineByComponentType.Selected = true;
                this.checkBoxRecordingMute.Checked = mixerFirstLineByComponentType.Volume == 0;
            }
            else
            {
                this.mMixers.Playback.DeviceId = ((MixerDetail)this.comboBoxPlaybackDevices.SelectedItem).DeviceId;
                mixerFirstLineByComponentType = this.mMixers.Playback.UserLines.GetMixerFirstLineByComponentType(MIXERLINE_COMPONENTTYPE.DST_SPEAKERS);
                this.trackBarPlaybackVolume.Tag = mixerFirstLineByComponentType;
                this.trackBarPlaybackBalance.Tag = mixerFirstLineByComponentType;
                this.checkBoxPlaybackMute.Tag = mixerFirstLineByComponentType;
            }
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
            if (mixerType == MixerType.Recording)
            {
                if (volume >= 0)
                {
                    this.trackBarRecordingVolume.Value = volume;
                }
                else
                {
                    this.trackBarRecordingVolume.Enabled = false;
                }
                if (mixerFirstLineByComponentType.Channels != 2)
                {
                    this.trackBarRecordingBalance.Enabled = false;
                }
                else
                {
                    this.trackBarRecordingBalance.Value = (int)(this.trackBarRecordingBalance.Maximum * num2);
                }
            }
            else
            {
                if (volume >= 0)
                {
                    this.trackBarPlaybackVolume.Value = volume;
                }
                else
                {
                    this.trackBarPlaybackVolume.Enabled = false;
                }
                if (mixerFirstLineByComponentType.Channels != 2)
                {
                    this.trackBarPlaybackBalance.Enabled = false;
                }
                else
                {
                    this.trackBarPlaybackBalance.Value = (int)(this.trackBarPlaybackBalance.Maximum * num2);
                }
            }
            this.checkBoxPlaybackMute.Checked = mixerFirstLineByComponentType.Mute;
        }
        private void comboBoxRecordingDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadValues(MixerType.Recording);
        }
        private void trackBarPlaybackBalance_ValueChanged(object sender, EventArgs e)
        {
            if (!this.mAvoidEvents)
            {
                TrackBar bar = (TrackBar)sender;
                MixerLine tag = (MixerLine)bar.Tag;
                if (tag.Channels == 2)
                {
                    TrackBar trackBarPlaybackVolume = this.trackBarPlaybackVolume;
                    MixerLine line2 = (MixerLine)trackBarPlaybackVolume.Tag;
                    if (line2 == tag)
                    {
                        if (bar.Value == 0)
                        {
                            tag.Channel = Channel.Uniform;
                            tag.Volume = trackBarPlaybackVolume.Value;
                        }
                        if (bar.Value <= 0)
                        {
                            tag.Channel = Channel.Left;
                            tag.Volume = trackBarPlaybackVolume.Value;
                            tag.Channel = Channel.Right;
                            tag.Volume = (int)(trackBarPlaybackVolume.Value * (1f + (((float)bar.Value) / ((float)bar.Maximum))));
                        }
                        else
                        {
                            tag.Channel = Channel.Right;
                            tag.Volume = trackBarPlaybackVolume.Value;
                            tag.Channel = Channel.Left;
                            tag.Volume = (int)(trackBarPlaybackVolume.Value * (1f - (((float)bar.Value) / ((float)bar.Maximum))));
                        }
                    }
                }
            }
        }
        private void trackBarPlaybackVolume_ValueChanged(object sender, EventArgs e)
        {
            if (!this.mAvoidEvents)
            {
                TrackBar bar = (TrackBar)sender;
                MixerLine tag = (MixerLine)bar.Tag;
                if (tag.Channels != 2)
                {
                    tag.Channel = Channel.Uniform;
                    tag.Volume = bar.Value;
                }
                else
                {
                    TrackBar trackBarPlaybackBalance = this.trackBarPlaybackBalance;
                    MixerLine line2 = (MixerLine)trackBarPlaybackBalance.Tag;
                    if (line2 == tag)
                    {
                        if (trackBarPlaybackBalance.Value == 0)
                        {
                            tag.Channel = Channel.Uniform;
                            tag.Volume = bar.Value;
                        }
                        if (trackBarPlaybackBalance.Value <= 0)
                        {
                            tag.Channel = Channel.Left;
                            tag.Volume = bar.Value;
                            tag.Channel = Channel.Right;
                            tag.Volume = (int)(bar.Value * (1f + (((float)trackBarPlaybackBalance.Value) / ((float)trackBarPlaybackBalance.Maximum))));
                        }
                        else
                        {
                            tag.Channel = Channel.Right;
                            tag.Volume = bar.Value;
                            tag.Channel = Channel.Left;
                            tag.Volume = (int)(bar.Value * (1f - (((float)trackBarPlaybackBalance.Value) / ((float)trackBarPlaybackBalance.Maximum))));
                        }
                    }
                }
            }
        }

        private void checkBoxPlaybackMute_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox box = (CheckBox)sender;
            MixerLine tag = (MixerLine)box.Tag;
            if (tag.Direction == MixerType.Recording)
            {
                tag.Channel = Channel.Uniform;
                if (this.checkBoxRecordingMute.Checked)
                {
                    this._lastMicVolume = tag.Volume;
                    tag.Volume = 0;
                }
                else
                {
                    tag.Volume = this._lastMicVolume;
                }
            }
            if (tag.ContainsMute)
            {
                tag.Mute = box.Checked;
            }
        }

        private void trackBarRecordingVolume_ValueChanged(object sender, EventArgs e)
        {
            if (!this.mAvoidEvents)
            {
                TrackBar bar = (TrackBar)sender;
                MixerLine tag = (MixerLine)bar.Tag;
                tag.Channel = Channel.Uniform;
                tag.Volume = bar.Value;
                this._lastMicVolume = tag.Volume;
                this.checkBoxRecordingMute.Checked = tag.Volume == 0;
            }
        }

    }
}
