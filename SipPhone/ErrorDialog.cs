using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AgentHelper.SipPhone
{
    public partial class ErrorDialog : Form
    {
        public ErrorDialog(string errMsg)
        {
            InitializeComponent();
            
        }
        public ErrorDialog(string errTitle,string errMsg)
        {
            InitializeComponent();
            this.Text = errTitle;
            this.textBox.Text = errMsg;
        }
    }
}
