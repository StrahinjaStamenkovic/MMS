using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MMSProjekat
{
    public partial class Settings : Form
    {
        public int memorySize
        {
            get { return (int)memorySizeNud.Value; }
            set { memorySizeNud.Value = value; }
        }
        private int currentValue;

        public Settings(int memSize)
        {
            currentValue = memSize;
            InitializeComponent();
            info3Lbl.Text += currentValue.ToString() + "MB";
        }
        
    }
}
