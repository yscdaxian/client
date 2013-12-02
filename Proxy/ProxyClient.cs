using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Diagnostics;


namespace AgentHelper.Proxy
{
  

    public delegate void OnProxyEvent(String msg);
    class ProxyClient
    {
        private TcpClient tcp;
        private NetworkStream workStream;
        private ManualResetEvent connectDone;
        private bool connected;
        private String host;
        private int port;
        private String eventData;
       
        private char[] HexDigits;

        public event OnProxyEvent OnProxyEventHandle;
        public ProxyClient()
        {   
            this.tcp = null;
            this.workStream = null;     
            this.Host = "127.0.0.1";
            this.Port = 5138;
            this.connectDone = new ManualResetEvent(false);     
            this.connected = false;
           
        }

        public String Host
        {
            get
            {
               return this.host;
            }
            set
            {
                this.host = value;
            }
        }

        public int Port
        {
            get
            {
                return this.port;
            }
            set
            {
                this.port = value;
            }
        }
        public bool Connected
        {
            get
            {
                return this.connected;
            }
            set
            {
                this.connected = value;
               
            }
        }
        private void TCPReadCallBack(IAsyncResult ar)
        {
            StateObject asyncState = (StateObject)ar.AsyncState;
            if ((asyncState.client != null) && asyncState.client.Connected)
            {
                NetworkStream stream = asyncState.client.GetStream();
                try{
                    int length = stream.EndRead(ar);

                    asyncState.totalBytesRead += length;
                    if (length > 0)
                    {
                        byte[] destinationArray = new byte[length];
                        Array.Copy(asyncState.buffer, 0, destinationArray, 0, length);
                        this.OnGetData(destinationArray);
                        stream.BeginRead(asyncState.buffer, 0, 0x400, new AsyncCallback(this.TCPReadCallBack), asyncState);
                    }
                    else
                    {
                        stream.Close();
                        asyncState.client.Close();
                        stream = null;
                        asyncState = null;
                    }
                }
                catch (Exception ex) {
                    System.Diagnostics.Trace.WriteLine(ex.ToString());
                }

            }
        }
        private void asyncread(TcpClient sock)
        {
            StateObject state = new StateObject
            {
                client = sock
            };
            NetworkStream stream = sock.GetStream();
            if (stream.CanRead)
            {
                try
                {
                    IAsyncResult result = stream.BeginRead(state.buffer, 0, 0x400, new AsyncCallback(this.TCPReadCallBack), state);
                }
                catch (Exception)
                {
                }
            }
        }

        private void ConnectCallback(IAsyncResult ar)
        {
            this.connectDone.Set();
            TcpClient asyncState = (TcpClient)ar.AsyncState;
            try
            {
                if (asyncState.Connected)
                {
                    this.Connected = true;
                    asyncState.EndConnect(ar);
                }
                else
                {
                    this.Connected = false;
                    asyncState.EndConnect(ar);
                }
            }
            catch (SocketException)
            {
                this.Connected = false;
            }
        }
        public void Close(){
            this.tcp.Close();
        }
        public void init()
        {
            if ((this.tcp == null) || !this.tcp.Connected)
            {
                try
                {
                    this.tcp = new TcpClient();
                    this.tcp.ReceiveTimeout = 10;
                    this.connectDone.Reset();
                    this.tcp.BeginConnect(this.host, this.port, new AsyncCallback(this.ConnectCallback), this.tcp);
                    this.connectDone.WaitOne();
                    if ((this.tcp != null) && this.tcp.Connected)
                    {
                        this.workStream = this.tcp.GetStream();
                        this.asyncread(this.tcp);
                    }
                   
                }
                catch (Exception)
                {
                }
            }
            
        }

        public Boolean isConnected(){
            if (!this.tcp.Connected){
                return false;
            }
            else {
                return true;
            }
        }

        public void reConnect() {
            try
            {
                this.init();

            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.ToString());
            }
        }

        public void sendCommand(String txtCmd)
        {
            String cmd = txtCmd;
            if (this.workStream != null)
            {
                byte[] bytes;  
                bytes = Encoding.UTF8.GetBytes(cmd);
                try{
                    this.workStream.Write(bytes, 0, bytes.Length);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Trace.WriteLine(ex.ToString());
                }
                this.workStream.Flush();
            }
        }
  

        private void OnGetData(byte[] data)
        {
            String str = System.Text.Encoding.UTF8.GetString(data);
            this.eventData += str;
            if (this.OnProxyEventHandle != null) 
            {
                String[] strBuffer=this.eventData.Split('\n');
                foreach(String onEvent in strBuffer){
                    if (onEvent.EndsWith("}"))
                        this.OnProxyEventHandle(onEvent);
                    else
                        this.eventData = onEvent;
                    
                }
                
            }

        }
       
    }
    
    internal class StateObject
    {
        public byte[] buffer = new byte[0x400];
        public const int BufferSize = 0x400;
        public TcpClient client = null;
        public StringBuilder messageBuffer = new StringBuilder();
        public string readType = null;
        public int totalBytesRead = 0;
    }
}
