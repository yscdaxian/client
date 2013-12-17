using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AgentHelper.Proxy;

namespace AgentHelper
{
    public partial class AgentForm : Form
    {
        public frmWb mainForm;
        private delegate void DRefreshFormUI(ProxyEventDataAgentStatus data);
        public AgentForm()
        {
            InitializeComponent();
        }

        public void OnProxyEventHandle(String msg)
        {
            try{
                ProxyEventData proxyEventData = fastJSON.JSON.Instance.ToObject<ProxyEventData>(msg);
                if (proxyEventData.eventId == 8) {
                    ProxyEventDataAgentStatus proxyEventDataAgentStatus = fastJSON.JSON.Instance.ToObject<ProxyEventDataAgentStatus>(msg);
                    base.Invoke(new DRefreshFormUI(updateAgentListView), new object[] { proxyEventDataAgentStatus });
                  
                }
            
            }
            catch(Exception ex){
                System.Diagnostics.Trace.WriteLine(ex.ToString());
            }
        }

        private void updateAgentListView(ProxyEventDataAgentStatus data){
            //纠正listview的数量和坐席的数量相同
            if (agentListView.Items.Count > data.eventEx.Count) {
                //删除多余的
                int needCount = data.eventEx.Count-agentListView.Items.Count;
                int startPoint = agentListView.Items.Count;
                for (int i = 1; i <= needCount; i++) {
                    agentListView.Items.RemoveAt(startPoint-i);
                }
            }
            else if (agentListView.Items.Count < data.eventEx.Count){   
                //添加不够
                int needCount =data.eventEx.Count- agentListView.Items.Count;
                for (int i = 1; i <= needCount; i++){
                    addCustomListViewItem(i);
                }
               
            }

            //update列表
            for (int i = 0; i < data.eventEx.Count; i++){
                this.agentListView.Items[i].Text = data.eventEx[i].agentName;
                this.agentListView.Items[i].SubItems[1].Text = data.eventEx[0].loginTime;
            }
         
        }
        private void AgentForm_FormClosed(object sender, FormClosedEventArgs e){
            this.mainForm.proxyClient.OnProxyEventHandle -= this.OnProxyEventHandle;
        }

        private void addCustomListViewItem(int pos) {
            ListViewItem tItem = new ListViewItem("");
            tItem.SubItems.Add("");
            tItem.SubItems.Add("3");
            Button transfer = new Button();
            transfer.Text = "转接";
            transfer.BackColor = SystemColors.Control;
            transfer.Font = this.Font;
            //b.Click += new EventHandler(b_Click);
            // Put it in the first column of the fourth row
            this.agentListView.Items.Add(tItem);
            this.agentListView.AddEmbeddedControl(transfer, 2, --pos);
        }

        private void AgentForm_Load(object sender, EventArgs e)
        {
            ImageList iList = new ImageList();
            iList.ImageSize = new Size(1, 24);//宽度和高度值必须大于等于1且不超过256
            this.agentListView.SmallImageList = iList;
        }
    }
}
