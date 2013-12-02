namespace AgentHelper
{
    partial class frmIeWindow
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
            this.cexweb = new csExWB.cEXWB();
            this.SuspendLayout();
            // 
            // cexweb
            // 
            this.cexweb.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cexweb.Border3DEnabled = false;
            this.cexweb.DocumentSource = "<HTML><HEAD></HEAD>\r\n<BODY></BODY></HTML>";
            this.cexweb.DocumentTitle = "";
            this.cexweb.DownloadActiveX = true;
            this.cexweb.DownloadFrames = true;
            this.cexweb.DownloadImages = true;
            this.cexweb.DownloadJava = true;
            this.cexweb.DownloadScripts = true;
            this.cexweb.DownloadSounds = true;
            this.cexweb.DownloadVideo = true;
            this.cexweb.FileDownloadDirectory = "C:\\Users\\Admin\\Documents\\";
            this.cexweb.Location = new System.Drawing.Point(-2, 2);
            this.cexweb.LocationUrl = "about:blank";
            this.cexweb.Name = "cexweb";
            this.cexweb.ObjectForScripting = null;
            this.cexweb.OffLine = false;
            this.cexweb.RegisterAsBrowser = false;
            this.cexweb.RegisterAsDropTarget = false;
            this.cexweb.RegisterForInternalDragDrop = true;
            this.cexweb.ScrollBarsEnabled = true;
            this.cexweb.SendSourceOnDocumentCompleteWBEx = false;
            this.cexweb.Silent = false;
            this.cexweb.Size = new System.Drawing.Size(971, 512);
            this.cexweb.TabIndex = 0;
            this.cexweb.Text = "cEXWB1";
            this.cexweb.TextSize = IfacesEnumsStructsClasses.TextSizeWB.Medium;
            this.cexweb.UseInternalDownloadManager = true;
            this.cexweb.WBDOCDOWNLOADCTLFLAG = 112;
            this.cexweb.WBDOCHOSTUIDBLCLK = IfacesEnumsStructsClasses.DOCHOSTUIDBLCLK.DEFAULT;
            this.cexweb.WBDOCHOSTUIFLAG = 262276;
            this.cexweb.DocumentComplete += new csExWB.DocumentCompleteEventHandler(this.cexweb_DocumentComplete);
            this.cexweb.DownloadBegin += new csExWB.DownloadBeginEventHandler(this.cexweb_DownloadBegin);
            this.cexweb.DownloadComplete += new csExWB.DownloadCompleteEventHandler(this.cexweb_DownloadComplete);
            this.cexweb.NavigateComplete2 += new csExWB.NavigateComplete2EventHandler(this.cexweb_NavigateComplete2);
            this.cexweb.NewWindow2 += new csExWB.NewWindow2EventHandler(this.cexweb_NewWindow2);
            this.cexweb.NewWindow3 += new csExWB.NewWindow3EventHandler(this.cexweb_NewWindow3);
            this.cexweb.ProgressChange += new csExWB.ProgressChangeEventHandler(this.cexweb_ProgressChange);
            this.cexweb.StatusTextChange += new csExWB.StatusTextChangeEventHandler(this.cexweb_StatusTextChange);
            this.cexweb.TitleChange += new csExWB.TitleChangeEventHandler(this.cexweb_TitleChange);
            this.cexweb.DocumentCompleteEX += new csExWB.DocumentCompleteExEventHandler(this.cexweb_DocumentCompleteEX);
            // 
            // frmIeWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(971, 516);
            this.Controls.Add(this.cexweb);
            this.Name = "frmIeWindow";
            this.Text = "about:blank";
            this.Activated += new System.EventHandler(this.frmIeWindow_Activated);
            this.Load += new System.EventHandler(this.frmIeWindow_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private csExWB.cEXWB cexweb;

    }
}