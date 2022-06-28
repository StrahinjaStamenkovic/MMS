using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MMSProjekat
{
    public partial class Threshold : Form
    {
        public int thresholdValue
        {
            get { return (int)thresholdNud.Value; }
            set { thresholdNud.Value = Math.Max(1,Math.Min(255,value)); }
        }

        public Threshold()
        {
            InitializeComponent();
        }
    }
}
