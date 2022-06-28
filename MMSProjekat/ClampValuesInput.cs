﻿using System;
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
    public partial class ClampValuesInput : Form
    {
        public byte Min { get { return (byte)minNud.Value; } set { minNud.Value = value; } }
        public byte Max { get { return (byte)maxNud.Value; } set { maxNud.Value = value; } }

        public ClampValuesInput()
        {
            InitializeComponent();
        }
    }
}
