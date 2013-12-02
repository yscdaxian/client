namespace AgentHelper.SipPhone
{
    using Sipek.Common;
    using System;
    using System.Timers;
    using AgentHelper;

    public class GUITimer : ITimer
    {
        private TimerExpiredCallback _elapsed;
        private frmWb _form;
        private Timer _guiTimer;
        private int _interval;

        public GUITimer(frmWb mf)
        {
            this._form = mf;
            this._guiTimer = new Timer();
            if (this.Interval > 0)
            {
                this._guiTimer.Interval = this.Interval;
            }
            this._guiTimer.Interval = 100.0;
            this._guiTimer.Enabled = true;
            this._guiTimer.Elapsed += new ElapsedEventHandler(this._guiTimer_Tick);
        }

        private void _guiTimer_Tick(object sender, EventArgs e)
        {
            this._guiTimer.Stop();
            if ((!this._form.IsDisposed && !this._form.Disposing) && this._form.IsInitialized)
            {
                this._form.Invoke(this._elapsed, new object[] { sender, e });
            }
        }

        public bool Start()
        {
            this._guiTimer.Start();
            return true;
        }

        public bool Stop()
        {
            this._guiTimer.Stop();
            return true;
        }

        public TimerExpiredCallback Elapsed
        {
            set
            {
                this._elapsed = value;
            }
        }

        public int Interval
        {
            get
            {
                return this._interval;
            }
            set
            {
                this._interval = value;
                this._guiTimer.Interval = value;
            }
        }
    }
}
