using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;

namespace PS_analyzer
{

	public class search : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox input;
		private System.Windows.Forms.Button set;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ListBox drive;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ListBox folders;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ListBox files;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label8;

		public string path;
		private bool sel;
		public ps_analyzer my;

		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Button def;

		private System.ComponentModel.Container components = null;

		public search()
		{
			InitializeComponent();
			path=input.Text=Directory.GetCurrentDirectory()+"\\";
		}


		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				this.Hide();
			}
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(search));
			this.label1 = new System.Windows.Forms.Label();
			this.input = new System.Windows.Forms.TextBox();
			this.set = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.drive = new System.Windows.Forms.ListBox();
			this.label3 = new System.Windows.Forms.Label();
			this.folders = new System.Windows.Forms.ListBox();
			this.label4 = new System.Windows.Forms.Label();
			this.files = new System.Windows.Forms.ListBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.def = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(152, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(128, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "Current input directory :";
			// 
			// input
			// 
			this.input.Location = new System.Drawing.Point(16, 24);
			this.input.Name = "input";
			this.input.ReadOnly = true;
			this.input.Size = new System.Drawing.Size(336, 20);
			this.input.TabIndex = 1;
			this.input.TabStop = false;
			this.input.Text = "";
			// 
			// set
			// 
			this.set.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(224)), ((System.Byte)(224)), ((System.Byte)(224)));
			this.set.Location = new System.Drawing.Point(360, 14);
			this.set.Name = "set";
			this.set.Size = new System.Drawing.Size(56, 24);
			this.set.TabIndex = 2;
			this.set.Text = "SET";
			this.set.Click += new System.EventHandler(this.set_Click);
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label2.Location = new System.Drawing.Point(16, 56);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(56, 16);
			this.label2.TabIndex = 3;
			this.label2.Text = "Drives :";
			// 
			// drive
			// 
			this.drive.Location = new System.Drawing.Point(8, 80);
			this.drive.Name = "drive";
			this.drive.Size = new System.Drawing.Size(56, 108);
			this.drive.Sorted = true;
			this.drive.TabIndex = 4;
			this.drive.SelectedIndexChanged += new System.EventHandler(this.ch_drv);
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label3.Location = new System.Drawing.Point(88, 56);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(152, 16);
			this.label3.TabIndex = 5;
			this.label3.Text = "Folders in current location :";
			// 
			// folders
			// 
			this.folders.Location = new System.Drawing.Point(88, 79);
			this.folders.Name = "folders";
			this.folders.Size = new System.Drawing.Size(152, 108);
			this.folders.Sorted = true;
			this.folders.TabIndex = 6;
			this.folders.DoubleClick += new System.EventHandler(this.ch_dir);
			this.folders.SelectedIndexChanged += new System.EventHandler(this.sel_dir);
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label4.Location = new System.Drawing.Point(259, 56);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(160, 16);
			this.label4.TabIndex = 7;
			this.label4.Text = "Target files in current folder :";
			// 
			// files
			// 
			this.files.Location = new System.Drawing.Point(263, 79);
			this.files.Name = "files";
			this.files.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.files.Size = new System.Drawing.Size(153, 108);
			this.files.Sorted = true;
			this.files.TabIndex = 8;
			this.files.TabStop = false;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(0, 192);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(64, 32);
			this.label5.TabIndex = 9;
			this.label5.Text = "Click to select";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(104, 192);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(128, 32);
			this.label6.TabIndex = 10;
			this.label6.Text = "Click to select Doubleclick to open";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(272, 192);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(136, 32);
			this.label7.TabIndex = 11;
			this.label7.Text = "List of posible target files in current input directory";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// button1
			// 
			this.button1.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(224)), ((System.Byte)(224)), ((System.Byte)(224)));
			this.button1.Location = new System.Drawing.Point(256, 232);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(64, 24);
			this.button1.TabIndex = 12;
			this.button1.Text = "EXIT";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(328, 236);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(96, 16);
			this.label8.TabIndex = 13;
			this.label8.Text = "without saving";
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(104, 235);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(136, 16);
			this.label9.TabIndex = 15;
			this.label9.Text = "program home directory";
			// 
			// def
			// 
			this.def.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(224)), ((System.Byte)(224)), ((System.Byte)(224)));
			this.def.Location = new System.Drawing.Point(32, 232);
			this.def.Name = "def";
			this.def.Size = new System.Drawing.Size(64, 24);
			this.def.TabIndex = 14;
			this.def.Text = "Default";
			this.def.Click += new System.EventHandler(this.def_Click);
			// 
			// search
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(255)), ((System.Byte)(255)));
			this.ClientSize = new System.Drawing.Size(424, 266);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.label9,
																		  this.def,
																		  this.label8,
																		  this.button1,
																		  this.label7,
																		  this.label6,
																		  this.label5,
																		  this.files,
																		  this.label4,
																		  this.folders,
																		  this.label3,
																		  this.drive,
																		  this.label2,
																		  this.set,
																		  this.input,
																		  this.label1});
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "search";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Set input directory";
			this.ResumeLayout(false);

		}
		#endregion

		private void button1_Click(object sender, System.EventArgs e)
		{
			this.Hide();
		}

		public string get_item(string x)
		{
			int i=x.LastIndexOf("\\");
			return x.Remove(0,i+1);
		}

		public string[] trim_items(string[] x)
		{
			string[] ret=new string[x.Length];
			for(int i=0;i<x.Length;i++) ret[i]=get_item(x[i]);
			return ret;
		}

		public string trim_last_folder(string x)
		{
			x=x.Remove(x.Length-1,1);
			return x.Remove(x.LastIndexOf("\\")+1,x.Length-x.LastIndexOf("\\")-1);
		}

		public string trim_drive(string x)
		{
		return x.Remove(x.IndexOf("\\")+1,x.Length-x.IndexOf("\\")-1);
		}

		public void init(ps_analyzer from)
		{
			input.Text=path;
			set_drives();
			set_folders();
			my=from;
		}

		public void set_drives()
		{
			string[] drv=Environment.GetLogicalDrives();
			drive.Items.Clear();
			drive.Items.AddRange(drv);
			sel=false;
		}

		public void set_folders()
		{
			string[] dir=Directory.GetDirectories(input.Text);
			folders.Items.Clear();
			dir=trim_items(dir);
			folders.Items.AddRange(dir);
			folders.Items.Add(".");
			folders.Items.Add("..");
			set_files();
			sel=false;
		}

		public void set_files()
		{
			string[] fil=Directory.GetFiles(input.Text,"*.mpg");
			files.Items.Clear();
			fil=trim_items(fil);
			files.Items.AddRange(fil);
			fil=Directory.GetFiles(input.Text,"*.mpeg");
			fil=trim_items(fil);
			files.Items.AddRange(fil);
		}

		private void ch_drv(object sender, System.EventArgs e)
		{
			input.Text=drive.Text;
			set_folders();
		}

		private void sel_dir(object sender, System.EventArgs e)
		{
			if(sel) 
			{
				input.Text=trim_last_folder(input.Text);
			}
			if((folders.Text!=".")&&(folders.Text!=".."))
			{
				input.Text+=folders.Text+"\\";
				set_files();
				sel=true;
			}
		}

		private void ch_dir(object sender, System.EventArgs e)
		{
			int i=input.Text.IndexOf("\\");
			int k=input.Text.Length;
			if((folders.Text!=".")&&(folders.Text!=".."))
			{
				set_folders();
			}
			else
			{
				if(folders.Text==".")
				{
					input.Text=trim_drive(input.Text);
					set_folders();
				}
				else if((folders.Text=="..")&&(i!=k-1))
				{
					input.Text=trim_last_folder(input.Text);
					set_folders();
				}
			}
	}

		private void set_Click(object sender, System.EventArgs e)
		{
			path=input.Text;
			this.Hide();
			my.refresh_list();
		}

		private void def_Click(object sender, System.EventArgs e)
		{
			input.Text=Directory.GetCurrentDirectory()+"\\";
			set_folders();
		}
	}
}
