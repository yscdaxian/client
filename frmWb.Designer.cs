namespace AgentHelper
{
    partial class frmWb
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmWb));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.ts_bk = new System.Windows.Forms.ToolStripButton();
            this.ts_go = new System.Windows.Forms.ToolStripSplitButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.ts_setting = new System.Windows.Forms.ToolStripButton();
            this.tsbx_url = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ts_keybad = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonMutex = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonMakeBusy = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonTransfer = new System.Windows.Forms.ToolStripButton();
            this.ts_hungup = new System.Windows.Forms.ToolStripButton();
            this.ts_telnum = new System.Windows.Forms.ToolStripComboBox();
            this.ts_answer = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.mdiTabStrip1 = new MdiTabStrip.MdiTabStrip();
            this.ieLabelRightKeyMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmi_as_callcenterWeb = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.新建ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_close_curpage = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_close_other_all = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_close_all = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmi_refreash = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.sb_urlinfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.tss_accounts = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tss_volumn = new System.Windows.Forms.ToolStripStatusLabel();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mdiTabStrip1)).BeginInit();
            this.ieLabelRightKeyMenu.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.MenuBar;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ts_bk,
            this.ts_go,
            this.toolStripSeparator6,
            this.toolStripButton2,
            this.ts_setting,
            this.tsbx_url,
            this.toolStripSeparator1,
            this.ts_keybad,
            this.toolStripSeparator2,
            this.toolStripButtonMutex,
            this.toolStripButtonMakeBusy,
            this.toolStripButtonTransfer,
            this.ts_hungup,
            this.ts_telnum,
            this.ts_answer,
            this.toolStripSeparator3});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(978, 39);
            this.toolStrip1.Stretch = true;
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // ts_bk
            // 
            this.ts_bk.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ts_bk.Image = ((System.Drawing.Image)(resources.GetObject("ts_bk.Image")));
            this.ts_bk.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_bk.Name = "ts_bk";
            this.ts_bk.Size = new System.Drawing.Size(36, 36);
            this.ts_bk.Text = "前进";
            this.ts_bk.Click += new System.EventHandler(this.ts_bk_Click);
            // 
            // ts_go
            // 
            this.ts_go.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ts_go.Image = ((System.Drawing.Image)(resources.GetObject("ts_go.Image")));
            this.ts_go.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_go.Name = "ts_go";
            this.ts_go.Size = new System.Drawing.Size(48, 36);
            this.ts_go.Text = "后退";
            this.ts_go.ButtonClick += new System.EventHandler(this.ts_go_ButtonClick);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 39);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(36, 36);
            this.toolStripButton2.Text = "主页";
            // 
            // ts_setting
            // 
            this.ts_setting.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.ts_setting.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ts_setting.Image = ((System.Drawing.Image)(resources.GetObject("ts_setting.Image")));
            this.ts_setting.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_setting.Name = "ts_setting";
            this.ts_setting.Size = new System.Drawing.Size(36, 36);
            this.ts_setting.Text = "设置";
            this.ts_setting.Click += new System.EventHandler(this.ts_setting_Click);
            // 
            // tsbx_url
            // 
            this.tsbx_url.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.tsbx_url.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.AllUrl;
            this.tsbx_url.Name = "tsbx_url";
            this.tsbx_url.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tsbx_url.Size = new System.Drawing.Size(352, 39);
            this.tsbx_url.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tsbx_url_KeyUp);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // ts_keybad
            // 
            this.ts_keybad.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.ts_keybad.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ts_keybad.Image = ((System.Drawing.Image)(resources.GetObject("ts_keybad.Image")));
            this.ts_keybad.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_keybad.Name = "ts_keybad";
            this.ts_keybad.Size = new System.Drawing.Size(36, 36);
            this.ts_keybad.Text = "键盘";
            this.ts_keybad.Click += new System.EventHandler(this.ts_keybad_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 39);
            // 
            // toolStripButtonMutex
            // 
            this.toolStripButtonMutex.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonMutex.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonMutex.Image")));
            this.toolStripButtonMutex.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonMutex.Name = "toolStripButtonMutex";
            this.toolStripButtonMutex.Size = new System.Drawing.Size(36, 36);
            this.toolStripButtonMutex.Text = "toolStripButtonMutex";
            this.toolStripButtonMutex.ToolTipText = "静音";
            this.toolStripButtonMutex.Click += new System.EventHandler(this.toolStripButtonMutex_Click);
            // 
            // toolStripButtonMakeBusy
            // 
            this.toolStripButtonMakeBusy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonMakeBusy.Image = global::AgentHelper.Properties.Resources.unbusy;
            this.toolStripButtonMakeBusy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonMakeBusy.Name = "toolStripButtonMakeBusy";
            this.toolStripButtonMakeBusy.Size = new System.Drawing.Size(36, 36);
            this.toolStripButtonMakeBusy.Text = "toolStripButtonMakeBusy";
            this.toolStripButtonMakeBusy.ToolTipText = "示忙/示闲";
            this.toolStripButtonMakeBusy.Click += new System.EventHandler(this.toolStripButtonBusy_Click);
            // 
            // toolStripButtonTransfer
            // 
            this.toolStripButtonTransfer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonTransfer.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonTransfer.Image")));
            this.toolStripButtonTransfer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonTransfer.Name = "toolStripButtonTransfer";
            this.toolStripButtonTransfer.Size = new System.Drawing.Size(36, 36);
            this.toolStripButtonTransfer.Text = "toolStripButtonTransfer";
            this.toolStripButtonTransfer.ToolTipText = "转接电话";
            this.toolStripButtonTransfer.Click += new System.EventHandler(this.toolStripButtonTransfer_Click);
            // 
            // ts_hungup
            // 
            this.ts_hungup.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.ts_hungup.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ts_hungup.Image = ((System.Drawing.Image)(resources.GetObject("ts_hungup.Image")));
            this.ts_hungup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_hungup.Name = "ts_hungup";
            this.ts_hungup.Size = new System.Drawing.Size(36, 36);
            this.ts_hungup.Text = "挂断";
            this.ts_hungup.Click += new System.EventHandler(this.ts_hungup_Click);
            // 
            // ts_telnum
            // 
            this.ts_telnum.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.ts_telnum.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.ts_telnum.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.HistoryList;
            this.ts_telnum.Name = "ts_telnum";
            this.ts_telnum.Size = new System.Drawing.Size(121, 39);
            // 
            // ts_answer
            // 
            this.ts_answer.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.ts_answer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ts_answer.Image = ((System.Drawing.Image)(resources.GetObject("ts_answer.Image")));
            this.ts_answer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_answer.Name = "ts_answer";
            this.ts_answer.Size = new System.Drawing.Size(36, 36);
            this.ts_answer.Text = "应答";
            this.ts_answer.Click += new System.EventHandler(this.ts_answer_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 39);
            // 
            // mdiTabStrip1
            // 
            this.mdiTabStrip1.ActiveTabFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.mdiTabStrip1.AllowDrop = true;
            this.mdiTabStrip1.ContextMenuStrip = this.ieLabelRightKeyMenu;
            this.mdiTabStrip1.Dock = System.Windows.Forms.DockStyle.Top;
            this.mdiTabStrip1.InactiveTabFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.mdiTabStrip1.Location = new System.Drawing.Point(0, 39);
            this.mdiTabStrip1.MdiWindowState = MdiTabStrip.MdiChildWindowState.Maximized;
            this.mdiTabStrip1.MinimumSize = new System.Drawing.Size(50, 33);
            this.mdiTabStrip1.MouseOverTabFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.mdiTabStrip1.Name = "mdiTabStrip1";
            this.mdiTabStrip1.Padding = new System.Windows.Forms.Padding(5, 3, 20, 5);
            this.mdiTabStrip1.Size = new System.Drawing.Size(978, 35);
            this.mdiTabStrip1.TabIndex = 2;
            this.mdiTabStrip1.TabPermanence = MdiTabStrip.MdiTabPermanence.LastOpen;
            this.mdiTabStrip1.Text = "mdiTabStrip1";
            // 
            // ieLabelRightKeyMenu
            // 
            this.ieLabelRightKeyMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_as_callcenterWeb,
            this.toolStripSeparator4,
            this.新建ToolStripMenuItem,
            this.tsmi_close_curpage,
            this.tsmi_close_other_all,
            this.tsmi_close_all,
            this.toolStripSeparator5,
            this.tsmi_refreash});
            this.ieLabelRightKeyMenu.Name = "ieLabelRightKeyMenu";
            this.ieLabelRightKeyMenu.Size = new System.Drawing.Size(173, 148);
            // 
            // tsmi_as_callcenterWeb
            // 
            this.tsmi_as_callcenterWeb.Name = "tsmi_as_callcenterWeb";
            this.tsmi_as_callcenterWeb.Size = new System.Drawing.Size(172, 22);
            this.tsmi_as_callcenterWeb.Text = "设置为呼叫中心页";
            this.tsmi_as_callcenterWeb.Click += new System.EventHandler(this.tsmi_as_callcenterWeb_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(169, 6);
            // 
            // 新建ToolStripMenuItem
            // 
            this.新建ToolStripMenuItem.Name = "新建ToolStripMenuItem";
            this.新建ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.新建ToolStripMenuItem.Text = "新建标签";
            this.新建ToolStripMenuItem.Click += new System.EventHandler(this.NewLabelToolStripMenuItem_Click);
            // 
            // tsmi_close_curpage
            // 
            this.tsmi_close_curpage.Name = "tsmi_close_curpage";
            this.tsmi_close_curpage.Size = new System.Drawing.Size(172, 22);
            this.tsmi_close_curpage.Text = "关闭当前页";
            this.tsmi_close_curpage.Click += new System.EventHandler(this.tsmi_close_curpage_Click);
            // 
            // tsmi_close_other_all
            // 
            this.tsmi_close_other_all.Name = "tsmi_close_other_all";
            this.tsmi_close_other_all.Size = new System.Drawing.Size(172, 22);
            this.tsmi_close_other_all.Text = "关闭其他所有";
            this.tsmi_close_other_all.Click += new System.EventHandler(this.tsmi_close_other_all_Click);
            // 
            // tsmi_close_all
            // 
            this.tsmi_close_all.Name = "tsmi_close_all";
            this.tsmi_close_all.Size = new System.Drawing.Size(172, 22);
            this.tsmi_close_all.Text = "关闭所有";
            this.tsmi_close_all.Click += new System.EventHandler(this.tsmi_close_all_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(169, 6);
            // 
            // tsmi_refreash
            // 
            this.tsmi_refreash.Name = "tsmi_refreash";
            this.tsmi_refreash.Size = new System.Drawing.Size(172, 22);
            this.tsmi_refreash.Text = "刷新";
            this.tsmi_refreash.Click += new System.EventHandler(this.tsmi_refreash_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sb_urlinfo,
            this.tsProgress,
            this.tss_accounts,
            this.toolStripStatusLabel1,
            this.tss_volumn});
            this.statusStrip1.Location = new System.Drawing.Point(0, 487);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(978, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // sb_urlinfo
            // 
            this.sb_urlinfo.Name = "sb_urlinfo";
            this.sb_urlinfo.Size = new System.Drawing.Size(891, 17);
            this.sb_urlinfo.Spring = true;
            this.sb_urlinfo.Text = "完成";
            this.sb_urlinfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tsProgress
            // 
            this.tsProgress.Name = "tsProgress";
            this.tsProgress.Size = new System.Drawing.Size(100, 16);
            this.tsProgress.Visible = false;
            // 
            // tss_accounts
            // 
            this.tss_accounts.Name = "tss_accounts";
            this.tss_accounts.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(32, 17);
            this.toolStripStatusLabel1.Text = "音量";
            // 
            // tss_volumn
            // 
            this.tss_volumn.Name = "tss_volumn";
            this.tss_volumn.Size = new System.Drawing.Size(40, 17);
            this.tss_volumn.Text = "100%";
            // 
            // frmWb
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.MenuBar;
            this.ClientSize = new System.Drawing.Size(978, 509);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.mdiTabStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "frmWb";
            this.Text = "呼叫中心助手";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmWb_FormClosed);
            this.Load += new System.EventHandler(this.frmWb_Load);
            this.Layout += new System.Windows.Forms.LayoutEventHandler(this.frmWb_Layout);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mdiTabStrip1)).EndInit();
            this.ieLabelRightKeyMenu.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripComboBox tsbx_url;
        private MdiTabStrip.MdiTabStrip mdiTabStrip1;
        private System.Windows.Forms.ToolStripButton ts_bk;
        private System.Windows.Forms.ToolStripSplitButton ts_go;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ContextMenuStrip ieLabelRightKeyMenu;
        private System.Windows.Forms.ToolStripMenuItem 新建ToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton ts_setting;
        private System.Windows.Forms.ToolStripButton ts_keybad;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton ts_hungup;
        private System.Windows.Forms.ToolStripButton ts_answer;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripStatusLabel tss_accounts;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel tss_volumn;
        public System.Windows.Forms.ToolStripStatusLabel sb_urlinfo;
        public System.Windows.Forms.ToolStripProgressBar tsProgress;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem tsmi_close_curpage;
        private System.Windows.Forms.ToolStripMenuItem tsmi_close_other_all;
        private System.Windows.Forms.ToolStripMenuItem tsmi_close_all;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem tsmi_refreash;
        public System.Windows.Forms.ToolStripComboBox ts_telnum;
        private System.Windows.Forms.ToolStripButton toolStripButtonMakeBusy;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton toolStripButtonMutex;
        private System.Windows.Forms.ToolStripButton toolStripButtonTransfer;
        private System.Windows.Forms.ToolStripMenuItem tsmi_as_callcenterWeb;

    }
}

