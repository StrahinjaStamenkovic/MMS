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
    public partial class ImageInput : Form
    {
        public int ImageNumber { get { return (int)imageNud.Value; } set { imageNud.Value = value; } }
        public ImageInput()
        {
            InitializeComponent();
        }
    }
}
