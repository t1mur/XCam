using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace XCamera
{
    class EnabledButton : Button
    {
        private bool enabled;
        public event EventHandler MyEnabled_Changed;
        private string enabledString = "Enabled";
        private string disabledString = "Disabled";

        public EnabledButton()
            : base()
        {
            MyEnabled = true;
            Click += new EventHandler(EnabledButton_Click);
        }

        void EnabledButton_Click(object sender, EventArgs e)
        {
            MyEnabled = !enabled;
        }
        
        public bool MyEnabled
        {
            get { return enabled; }
            set
            {
                if (enabled != value)
                {
                    enabled = value;
                    if (MyEnabled_Changed != null)
                        MyEnabled_Changed(this, null);
                }
                if (value == true)
                {
                    BackColor = Color.Lime;
                    Text = enabledString;
                    FlatStyle = FlatStyle.Flat;
                }
                else
                {
                    BackColor = Color.Red;
                    Text = disabledString;
                    FlatStyle = FlatStyle.Standard;
                }
            }
        }


        public string EnabledString
        {
            get { return enabledString; }
            set { enabledString = value; }
        }
        public string DisabledString
        {
            get { return disabledString; }
            set { disabledString = value; }
        }

    }
}
