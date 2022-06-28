using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;

namespace TraceJPG
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.ListBox listBox1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.ListBox listBox2;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
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
			this.button1 = new System.Windows.Forms.Button();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			this.button5 = new System.Windows.Forms.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.listBox2 = new System.Windows.Forms.ListBox();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.Location = new System.Drawing.Point(464, 16);
			this.button1.Name = "button1";
			this.button1.TabIndex = 0;
			this.button1.Text = "Open JPG";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// listBox1
			// 
			this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left)));
			this.listBox1.HorizontalScrollbar = true;
			this.listBox1.Location = new System.Drawing.Point(24, 8);
			this.listBox1.Name = "listBox1";
			this.listBox1.ScrollAlwaysVisible = true;
			this.listBox1.Size = new System.Drawing.Size(264, 264);
			this.listBox1.TabIndex = 1;
			// 
			// button2
			// 
			this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.button2.Location = new System.Drawing.Point(24, 288);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(32, 23);
			this.button2.TabIndex = 2;
			this.button2.Text = "<<";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button3
			// 
			this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.button3.Location = new System.Drawing.Point(64, 288);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(32, 23);
			this.button3.TabIndex = 3;
			this.button3.Text = "<";
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// button4
			// 
			this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.button4.Location = new System.Drawing.Point(216, 288);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(32, 23);
			this.button4.TabIndex = 4;
			this.button4.Text = ">";
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// button5
			// 
			this.button5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.button5.Location = new System.Drawing.Point(256, 288);
			this.button5.Name = "button5";
			this.button5.Size = new System.Drawing.Size(32, 23);
			this.button5.TabIndex = 5;
			this.button5.Text = ">>";
			this.button5.Click += new System.EventHandler(this.button5_Click);
			// 
			// textBox1
			// 
			this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.textBox1.Location = new System.Drawing.Point(112, 288);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(80, 20);
			this.textBox1.TabIndex = 6;
			this.textBox1.Text = "0";
			// 
			// listBox2
			// 
			this.listBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.listBox2.HorizontalScrollbar = true;
			this.listBox2.Location = new System.Drawing.Point(296, 8);
			this.listBox2.Name = "listBox2";
			this.listBox2.ScrollAlwaysVisible = true;
			this.listBox2.Size = new System.Drawing.Size(152, 264);
			this.listBox2.TabIndex = 7;
			this.listBox2.DoubleClick += new System.EventHandler(this.listBox2_DoubleClick);
			this.listBox2.SelectedIndexChanged += new System.EventHandler(this.listBox2_SelectedIndexChanged);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(568, 322);
			this.Controls.Add(this.listBox2);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.button5);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.listBox1);
			this.Controls.Add(this.button1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		byte[] a;
		int currentPage = 0;
		int pageLength = 64;
		int lastPage = 0;

		private void button1_Click(object sender, System.EventArgs e)
		{
			if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
			{
				FileStream fsr = System.IO.File.OpenRead(this.openFileDialog1.FileName);
				a = new byte[fsr.Length];
				fsr.Read(a, 0, Convert.ToInt32(fsr.Length));

				fsr.Close();
				ShowPage();
				lastPage = a.Length/pageLength;
				this.FindMarkers();
			}
		}

		private void FindMarkers()
		{
			this.listBox2.Items.Clear();
			int startIndex = 0;
			while (startIndex >= 0)
			{
				startIndex = Array.IndexOf(a, Convert.ToByte("ff", 16), startIndex);
				string nextByte = Convert.ToString(a[startIndex + 1], 16).ToLower();
				if (nextByte == "d8")
				{
					this.listBox2.Items.Add(nextByte + ": Start image marker :" + startIndex.ToString());
				}
				if (nextByte[0] == 'e')
				{
					this.listBox2.Items.Add(nextByte + ": Start JFIF marker :" + startIndex.ToString());
				}
				if (nextByte == "db")
				{
					this.listBox2.Items.Add(nextByte + ": Start Quantization table marker :" + startIndex.ToString());
				}
				if (nextByte == "c4")
				{
					this.listBox2.Items.Add(nextByte + ": Start Huffman table marker :" + startIndex.ToString());
				}
				if (nextByte == "c0")
				{
					this.listBox2.Items.Add(nextByte + ": Start frame marker :" + startIndex.ToString());
				}
				if (nextByte == "da")
				{
					this.listBox2.Items.Add(nextByte + ": Start scan marker :" + startIndex.ToString());
				}
				if (nextByte == "fe")
				{
					this.listBox2.Items.Add(nextByte + ": Start comment marker :" + startIndex.ToString());
				}
				if (nextByte == "d9")
				{
					this.listBox2.Items.Add(nextByte + ": End of image marker :" + startIndex.ToString());
				}
				if (startIndex >= 0)
				startIndex++;
			}
		}

		private void ShowPage()
		{
			int startIndex = currentPage* pageLength;

			int endIndex = startIndex + pageLength - 1;

			listBox1.Items.Clear();

			for (int i = startIndex; i <= endIndex; i++)
			{
				if (i < a.Length)
				{
					listBox1.Items.Add(i.ToString() + ": " + System.Convert.ToString(a[i], 16));
				}
			}
			this.textBox1.Text = this.currentPage.ToString();
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			this.currentPage = 0;
			this.ShowPage();
		}

		private void button3_Click(object sender, System.EventArgs e)
		{
			if (currentPage > 0)
				currentPage--;
			this.ShowPage();
		}

		private void button4_Click(object sender, System.EventArgs e)
		{
			if (currentPage < lastPage)
				currentPage++;
			this.ShowPage();
		}

		private void button5_Click(object sender, System.EventArgs e)
		{
			this.currentPage = this.lastPage;
			this.ShowPage();
		}

		private void listBox2_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}

		private void listBox2_DoubleClick(object sender, System.EventArgs e)
		{
			string[] comps = this.listBox2.SelectedItem.ToString().Split(':');
			string nextByte = comps[0];
			
			int startPos = Convert.ToInt32(comps[2]);
			int len = a[startPos + 2] * 256 + a[startPos + 3];
			byte[] subArray = new byte[len + 2];
			Array.Copy(a, startPos, subArray, 0, len + 2);
			InfoForm ifform = new InfoForm(nextByte, subArray);
			ifform.ShowDialog();
		}
	}
}
