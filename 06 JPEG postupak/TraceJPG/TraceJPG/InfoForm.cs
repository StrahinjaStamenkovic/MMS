using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace TraceJPG
{
	/// <summary>
	/// Summary description for InfoForm.
	/// </summary>
	public class InfoForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListBox listBox1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public InfoForm(string headerType, byte[] bytesToProcess)
		{
			InitializeComponent();

			this.listBox1.Items.Clear();
			for (int i = 0; i < bytesToProcess.Length; i++)
			{
				this.listBox1.Items.Add(i.ToString() + ":" + bytesToProcess[i]);

			}

		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.SuspendLayout();
			// 
			// listBox1
			// 
			this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listBox1.Location = new System.Drawing.Point(0, 0);
			this.listBox1.Name = "listBox1";
			this.listBox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
			this.listBox1.Size = new System.Drawing.Size(560, 498);
			this.listBox1.TabIndex = 0;
			// 
			// InfoForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(560, 498);
			this.Controls.Add(this.listBox1);
			this.Name = "InfoForm";
			this.Text = "InfoForm";
			this.ResumeLayout(false);

		}
		#endregion
	}
}
