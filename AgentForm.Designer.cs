namespace AgentHelper
{
    partial class AgentForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.agentListView = new AgentHelper.ListViewEx();
            this.columnAgentName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnAgentStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnTransferBt = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // agentListView
            // 
            this.agentListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnAgentName,
            this.columnAgentStatus,
            this.columnTransferBt});
            this.agentListView.Location = new System.Drawing.Point(0, -3);
            this.agentListView.Name = "agentListView";
            this.agentListView.Size = new System.Drawing.Size(351, 303);
            this.agentListView.TabIndex = 0;
            this.agentListView.UseCompatibleStateImageBehavior = false;
            this.agentListView.View = System.Windows.Forms.View.Details;
            // 
            // columnAgentName
            // 
            this.columnAgentName.Text = "坐席";
            // 
            // columnAgentStatus
            // 
            this.columnAgentStatus.Text = "状态";
            this.columnAgentStatus.Width = 55;
            // 
            // columnTransferBt
            // 
            this.columnTransferBt.Text = "";
            // 
            // AgentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(352, 300);
            this.Controls.Add(this.agentListView);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AgentForm";
            this.Text = "AgentForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AgentForm_FormClosed);
            this.Load += new System.EventHandler(this.AgentForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private AgentHelper.ListViewEx agentListView;
        private System.Windows.Forms.ColumnHeader columnAgentName;
        private System.Windows.Forms.ColumnHeader columnAgentStatus;
        private System.Windows.Forms.ColumnHeader columnTransferBt;
    }
}