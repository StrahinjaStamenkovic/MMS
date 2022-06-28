using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MMSProjekat
{
    public partial class HueSaturationInput : Form
    {
        public decimal Hue { get { return hueNud.Value; } set { hueNud.Value = value; } }
        public bool UseSaturationValue { get { return useSaturationChx.Checked; } set { useSaturationChx.Checked = value; } }
        public decimal Saturation { get { return saturationNud.Value; } set { saturationNud.Value = value; } }

        public HueSaturationInput()
        {
            InitializeComponent();
        }

        private void useSaturationChx_CheckedChanged(object sender, EventArgs e)
        {
            saturationNud.Enabled = useSaturationChx.Checked;
        }
    }
}
