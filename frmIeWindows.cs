using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using csExWB;
using AgentHelper.Proxy;
namespace AgentHelper
{
     
    public partial class frmIeWindow : Form
    {
        public static List<frmIeWindow> all_Pages;
        public static frmIeWindow m_CurPage;
        public static cEXWB m_CurWB;
        public static frmWb mdiform;

        public static int m_iCountWB;
        public static int m_iCurMenu;
        public static int m_iCurTab;
        public string url;
        public ToolStripComboBox    tscb;
     
        static frmIeWindow()
        {
            m_CurWB = null;
            m_CurPage = null;
            all_Pages = new List<frmIeWindow>();
            m_iCurTab = 0;
            m_iCurMenu = 0;
            m_iCountWB = 1;
            mdiform = null;
        }
      
        public frmIeWindow(ToolStripComboBox tscb)
        {
            InitializeComponent();    
            m_CurWB = this.cexweb;
            m_CurPage = this;
            all_Pages.Add(this);
            
            this.url = "";
            this.tscb = tscb;
        }
        public frmIeWindow(ToolStripComboBox tscb, string url)
        {
            InitializeComponent();
            m_CurWB = this.cexweb;
            m_CurPage = this;
            all_Pages.Add(this);
            this.tscb = tscb;
            this.url = url;
           
        }

        protected override void OnFormClosing(FormClosingEventArgs e)//重载关闭函数
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (this.cexweb == AgentHelper.Properties.Settings.Default.callCenterWeb) {
                    e.Cancel = true;
                    MessageBox.Show("呼叫中心页禁止关闭");
                }        
               
            }
        }

        private void frmIeWindow_Load(object sender, EventArgs e)
        {
            /*2011-12-09添加js call c#*/
            this.cexweb.ObjectForScripting = (ICallByWebJs)mdiform;
            if (this.url != "")
            {
                this.cexweb.Navigate(this.url);
            }
        }

        private void frmIeWindow_Activated(object sender, EventArgs e)
        {
            m_CurWB = this.cexweb;
            this.tscb.Text = this.url;
            m_CurPage = this;
            mdiform.SetGoBackBtn();
            ChangeTitle(this.Text);
        }
        private void cexweb_TitleChange(object sender, TitleChangeEventArgs e)
        {
            ChangeTitle(e.title);
        }
        private void cexweb_DocumentComplete(object sender, DocumentCompleteEventArgs e)
        {
            this.url = e.url;
            mdiform.SetGoBackBtn();
        }
        private void cexweb_DocumentCompleteEX(object sender, DocumentCompleteExEventArgs e)
        {
            this.url = e.url;
            mdiform.SetGoBackBtn();
        }
        private void ChangeTitle(string title)
        {
            mdiform.Text = title + "-呼叫中心助手";
            this.Text = title;
        }

        private void cexweb_NewWindow2(object sender, NewWindow2EventArgs e)
        {
            e.Cancel = true;
        }
        private void cexweb_NewWindow3(object sender, NewWindow3EventArgs e)
        {
            this.NewWindow(e.url);
            this.AssignPopup(ref e.browser);
            e.Cancel = false;
        }
        private void AssignPopup(ref object obj)
        {
            if (m_CurWB != null)
            {
                if (!m_CurWB.RegisterAsBrowser)
                {
                    m_CurWB.RegisterAsBrowser = true;
                }
                obj = m_CurWB.WebbrowserObject;
            }
        }


        private void NewWindow(string url)
        {
            new frmIeWindow(this.tscb, url) { MdiParent = mdiform}.Show();
            this.ChangeTitle(url);
            this.tscb.Text = url;
        }

        private void cexweb_NavigateComplete2(object sender, NavigateComplete2EventArgs e)
        {
            mdiform.SetGoBackBtn();
        }

        private void cexweb_StatusTextChange(object sender, StatusTextChangeEventArgs e)
        {
            mdiform.sb_urlinfo.Text = e.text;
        }
        private void cexweb_DownloadBegin(object sender)
        {
            mdiform.tsProgress.Visible = true;
        }

        private void cexweb_DownloadComplete(object sender)
        {
            mdiform.tsProgress.Visible = false;
        }
        private void cexweb_ProgressChange(object sender, ProgressChangeEventArgs e)
        {
            if (sender == m_CurWB)
            {
                ToolStripProgressBar tsProgress = mdiform.tsProgress;
                try
                {
                    if ((e.progress == -1) || (e.progressmax == e.progress))
                    {
                        tsProgress.Value = 0;
                        tsProgress.Maximum = 0;
                    }
                    else if (((e.progressmax > 0) && (e.progress > 0)) && (e.progress < e.progressmax))
                    {
                        tsProgress.Maximum = e.progressmax;
                        tsProgress.Value = e.progress;
                    }
                }
                catch (Exception)
                {
                }
            }
        }
        
    }

}
