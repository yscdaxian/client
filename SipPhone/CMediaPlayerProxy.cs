namespace AgentHelper.SipPhone
{
    using Sipek.Common;
    using System;
    using System.Media;

    public class CMediaPlayerProxy : IMediaProxyInterface
    {
        private SoundPlayer player = new SoundPlayer();

        public int playTone(ETones toneId)
        {
            string str;
            switch (toneId)
            {
                case ETones.EToneDial:
                    str = "Sounds/dial.wav";
                    break;

                case ETones.EToneCongestion:
                    //str = "Sounds/congestion.wav";
                    str = "";
                    break;

                case ETones.EToneRingback:
                    str = "Sounds/ringback.wav";
                    break;

                case ETones.EToneRing:
                    str = "Sounds/ring.wav";
                    break;

                default:
                    str = "";
                    break;
            }
            if (!str.Equals(""))
            {
                this.player.SoundLocation = str;
                this.player.Load();
                this.player.PlayLooping();
            }
            return 1;
        }

        public int stopTone()
        {
            this.player.Stop();
            return 1;
        }
    }
}
