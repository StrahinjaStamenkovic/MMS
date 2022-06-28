using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MMSProjekat
{
    public partial class GammaInput : Form
    {
        public decimal red
        {
            get { return redNud.Value; }
            set { redNud.Value = value; }
        }
        public decimal green
        {
            get { return greenNud.Value; }
            set { greenNud.Value = value; }
        }
        public decimal blue
        {
            get { return blueNud.Value; }
            set { blueNud.Value = value; }
        }
        public GammaInput()
        {
            InitializeComponent();
        }
    }
}
