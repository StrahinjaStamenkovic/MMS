using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;

namespace PS_analyzer
{

	public class ps_analyzer : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label text;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox pks;
		private System.Windows.Forms.TextBox sh;
		private System.Windows.Forms.TextBox vp;
		private System.Windows.Forms.TextBox ap;
		private System.Windows.Forms.TextBox ok;
		private System.Windows.Forms.TextBox op;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox psm;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.TextBox f_out;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.ListBox f_in;
		private System.Windows.Forms.Button st;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Button end;
		private System.Windows.Forms.Button refresh;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.TextBox go;

		private PS stream;
		int nr_pks,nr_sh,nr_vp,nr_ap,nr_op,nr_psm;
		bool succes;

		private System.Windows.Forms.Button browse;
		private search brow=new search();

		private System.ComponentModel.Container components = null;

		public ps_analyzer()
		{
			InitializeComponent();
		}


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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ps_analyzer));
			this.text = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.pks = new System.Windows.Forms.TextBox();
			this.sh = new System.Windows.Forms.TextBox();
			this.vp = new System.Windows.Forms.TextBox();
			this.ap = new System.Windows.Forms.TextBox();
			this.ok = new System.Windows.Forms.TextBox();
			this.op = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.psm = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.f_out = new System.Windows.Forms.TextBox();
			this.label12 = new System.Windows.Forms.Label();
			this.f_in = new System.Windows.Forms.ListBox();
			this.st = new System.Windows.Forms.Button();
			this.label13 = new System.Windows.Forms.Label();
			this.end = new System.Windows.Forms.Button();
			this.refresh = new System.Windows.Forms.Button();
			this.label14 = new System.Windows.Forms.Label();
			this.go = new System.Windows.Forms.TextBox();
			this.browse = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// text
			// 
			this.text.Location = new System.Drawing.Point(8, 8);
			this.text.Name = "text";
			this.text.Size = new System.Drawing.Size(432, 16);
			this.text.TabIndex = 0;
			this.text.Text = "This is a very simple program that analyze a Program Stream ( MPEG 2 file )";
			this.text.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(432, 40);
			this.label1.TabIndex = 1;
			this.label1.Text = "Some output info is displayed on screen and individual packets info as well as ot" +
				"her specifications goes to a specified text file in the program home directory. " +
				"";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label2.Location = new System.Drawing.Point(64, 80);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(48, 16);
			this.label2.TabIndex = 2;
			this.label2.Text = "INPUT :";
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label3.Location = new System.Drawing.Point(168, 80);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(64, 16);
			this.label3.TabIndex = 3;
			this.label3.Text = "OUTPUT :";
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label4.Location = new System.Drawing.Point(352, 80);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(48, 16);
			this.label4.TabIndex = 4;
			this.label4.Text = "INFO :";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(264, 112);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(80, 16);
			this.label5.TabIndex = 5;
			this.label5.Text = "Pack headers :";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(264, 138);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(96, 16);
			this.label6.TabIndex = 6;
			this.label6.Text = "System Headers :";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(264, 163);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(96, 16);
			this.label7.TabIndex = 7;
			this.label7.Text = "Video packets :";
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(264, 188);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(96, 16);
			this.label8.TabIndex = 8;
			this.label8.Text = "Audio packets :";
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(264, 256);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(104, 16);
			this.label9.TabIndex = 9;
			this.label9.Text = "Succesfull reading :";
			// 
			// pks
			// 
			this.pks.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(255)), ((System.Byte)(255)));
			this.pks.Location = new System.Drawing.Point(360, 112);
			this.pks.Name = "pks";
			this.pks.ReadOnly = true;
			this.pks.Size = new System.Drawing.Size(72, 20);
			this.pks.TabIndex = 10;
			this.pks.TabStop = false;
			this.pks.Text = "";
			this.pks.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// sh
			// 
			this.sh.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(255)), ((System.Byte)(255)));
			this.sh.Location = new System.Drawing.Point(360, 136);
			this.sh.Name = "sh";
			this.sh.ReadOnly = true;
			this.sh.Size = new System.Drawing.Size(72, 20);
			this.sh.TabIndex = 11;
			this.sh.TabStop = false;
			this.sh.Text = "";
			this.sh.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// vp
			// 
			this.vp.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(255)), ((System.Byte)(255)));
			this.vp.Location = new System.Drawing.Point(360, 160);
			this.vp.Name = "vp";
			this.vp.ReadOnly = true;
			this.vp.Size = new System.Drawing.Size(72, 20);
			this.vp.TabIndex = 12;
			this.vp.TabStop = false;
			this.vp.Text = "";
			this.vp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// ap
			// 
			this.ap.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(255)), ((System.Byte)(255)));
			this.ap.Location = new System.Drawing.Point(360, 184);
			this.ap.Name = "ap";
			this.ap.ReadOnly = true;
			this.ap.Size = new System.Drawing.Size(72, 20);
			this.ap.TabIndex = 13;
			this.ap.TabStop = false;
			this.ap.Text = "";
			this.ap.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// ok
			// 
			this.ok.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(255)), ((System.Byte)(255)));
			this.ok.Location = new System.Drawing.Point(376, 256);
			this.ok.Name = "ok";
			this.ok.ReadOnly = true;
			this.ok.Size = new System.Drawing.Size(40, 20);
			this.ok.TabIndex = 14;
			this.ok.TabStop = false;
			this.ok.Text = "";
			this.ok.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// op
			// 
			this.op.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(255)), ((System.Byte)(255)));
			this.op.Location = new System.Drawing.Point(360, 208);
			this.op.Name = "op";
			this.op.ReadOnly = true;
			this.op.Size = new System.Drawing.Size(72, 20);
			this.op.TabIndex = 16;
			this.op.TabStop = false;
			this.op.Text = "";
			this.op.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(264, 211);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(96, 16);
			this.label10.TabIndex = 15;
			this.label10.Text = "Other packets :";
			// 
			// psm
			// 
			this.psm.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(255)), ((System.Byte)(255)));
			this.psm.Location = new System.Drawing.Point(360, 232);
			this.psm.Name = "psm";
			this.psm.ReadOnly = true;
			this.psm.Size = new System.Drawing.Size(72, 20);
			this.psm.TabIndex = 18;
			this.psm.TabStop = false;
			this.psm.Text = "";
			this.psm.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(264, 235);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(96, 16);
			this.label11.TabIndex = 17;
			this.label11.Text = "PSM packets :";
			// 
			// f_out
			// 
			this.f_out.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(255)), ((System.Byte)(192)));
			this.f_out.Location = new System.Drawing.Point(166, 104);
			this.f_out.MaxLength = 8;
			this.f_out.Name = "f_out";
			this.f_out.Size = new System.Drawing.Size(56, 20);
			this.f_out.TabIndex = 19;
			this.f_out.Text = "info";
			this.f_out.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.f_out.TextChanged += new System.EventHandler(this.verify);
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(224, 108);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(24, 16);
			this.label12.TabIndex = 20;
			this.label12.Text = ".txt";
			// 
			// f_in
			// 
			this.f_in.Location = new System.Drawing.Point(16, 104);
			this.f_in.Name = "f_in";
			this.f_in.Size = new System.Drawing.Size(128, 82);
			this.f_in.Sorted = true;
			this.f_in.TabIndex = 21;
			this.f_in.TabStop = false;
			this.f_in.SelectedIndexChanged += new System.EventHandler(this.verify);
			// 
			// st
			// 
			this.st.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(224)), ((System.Byte)(224)), ((System.Byte)(224)));
			this.st.Enabled = false;
			this.st.Location = new System.Drawing.Point(160, 168);
			this.st.Name = "st";
			this.st.Size = new System.Drawing.Size(72, 24);
			this.st.TabIndex = 22;
			this.st.Text = "START";
			this.st.Click += new System.EventHandler(this.st_Click);
			// 
			// label13
			// 
			this.label13.Font = new System.Drawing.Font("Pristina", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label13.ForeColor = System.Drawing.Color.Blue;
			this.label13.Location = new System.Drawing.Point(64, 208);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(120, 40);
			this.label13.TabIndex = 23;
			this.label13.Text = "Program by Angel April 2004";
			this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// end
			// 
			this.end.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(224)), ((System.Byte)(224)), ((System.Byte)(224)));
			this.end.Location = new System.Drawing.Point(88, 256);
			this.end.Name = "end";
			this.end.Size = new System.Drawing.Size(72, 24);
			this.end.TabIndex = 24;
			this.end.Text = "EXIT";
			this.end.Click += new System.EventHandler(this.end_Click);
			// 
			// refresh
			// 
			this.refresh.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(224)), ((System.Byte)(224)), ((System.Byte)(224)));
			this.refresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.refresh.Location = new System.Drawing.Point(80, 184);
			this.refresh.Name = "refresh";
			this.refresh.Size = new System.Drawing.Size(64, 16);
			this.refresh.TabIndex = 25;
			this.refresh.TabStop = false;
			this.refresh.Text = "REFRESH";
			this.refresh.Click += new System.EventHandler(this.refresh_Click);
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(160, 140);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(56, 16);
			this.label14.TabIndex = 26;
			this.label14.Text = "OK to go :";
			// 
			// go
			// 
			this.go.BackColor = System.Drawing.Color.Red;
			this.go.Location = new System.Drawing.Point(216, 138);
			this.go.Name = "go";
			this.go.Size = new System.Drawing.Size(16, 20);
			this.go.TabIndex = 27;
			this.go.Text = "";
			// 
			// browse
			// 
			this.browse.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(224)), ((System.Byte)(224)), ((System.Byte)(224)));
			this.browse.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.browse.Location = new System.Drawing.Point(16, 184);
			this.browse.Name = "browse";
			this.browse.Size = new System.Drawing.Size(64, 16);
			this.browse.TabIndex = 28;
			this.browse.TabStop = false;
			this.browse.Text = "BROWSE";
			this.browse.Click += new System.EventHandler(this.browse_Click);
			// 
			// ps_analyzer
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(255)), ((System.Byte)(192)));
			this.ClientSize = new System.Drawing.Size(448, 286);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.browse,
																		  this.go,
																		  this.label14,
																		  this.refresh,
																		  this.end,
																		  this.label13,
																		  this.st,
																		  this.f_in,
																		  this.label12,
																		  this.f_out,
																		  this.psm,
																		  this.label11,
																		  this.op,
																		  this.label10,
																		  this.ok,
																		  this.ap,
																		  this.vp,
																		  this.sh,
																		  this.pks,
																		  this.label9,
																		  this.label8,
																		  this.label7,
																		  this.label6,
																		  this.label5,
																		  this.label4,
																		  this.label3,
																		  this.label2,
																		  this.label1,
																		  this.text});
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ps_analyzer";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Analyze Program Stream";
			this.Load += new System.EventHandler(this.ps_analyzer_Load);
			this.ResumeLayout(false);

		}
		#endregion


		[STAThread]
		static void Main() 
		{
			Application.Run(new ps_analyzer());
		}

		private void end_Click(object sender, System.EventArgs e)
		{
			Dispose(true);
		}

		public void refresh_list()
		{
			f_in.Items.Clear();
			string[] files=Directory.GetFiles(brow.path,"*.mpg");
			files=brow.trim_items(files);
			f_in.Items.AddRange(files);
			files=Directory.GetFiles(brow.path,"*.mpeg");
			files=brow.trim_items(files);
			f_in.Items.AddRange(files);			

		}

		private void refresh_Click(object sender, System.EventArgs e)
		{
			refresh_list();
			verify(sender,e);
		}

		private void verify(object sender, System.EventArgs e)
		{
			if ((f_in.Text.Length>0)&&(f_out.Text.Length>0)) 
			{
				st.Enabled=true;
				go.BackColor=Color.Lime;
			}
			else 
			{
				st.Enabled=false;
				go.BackColor=Color.Red;
			}
		}


		private void process_PS()
		{
			int n_pks,n_sp,n_vp,n_ap,n_op,n_psm,cod,jmp;
			int s_pks,s_sp,s_vp,s_ap,s_op,s_psm,dif;
			double PTS,DTS,SCR;
			string stamp;
			bool r_ok,r_end,stp;
			StreamWriter file = new StreamWriter(f_out.Text+".txt");
			
			bit k=new bit();
			byte xx=k.set_bit(4,2);

			r_ok=true;
			r_end=false;
			n_pks=n_sp=n_vp=n_ap=n_op=n_psm=0;
			s_pks=s_sp=s_vp=s_ap=s_op=s_psm=0;
			file.WriteLine("This file list the packets found in "+brow.path+f_in.Text);
			file.WriteLine("");
			stream.fill_buffer(0);
			jmp=0;
			while((!r_end)&&(stream.pos<=stream.buf_len)&&(r_ok))
			{
				cod=stream.read_code();
				if(cod==185) r_end=true;//MPEG_end
				if(cod==0) r_ok=false;//Unknown packet
				if(cod==186) // Pack header 
				{
					SCR=stream.get_SCR();
					stream.skip_info(3); // salt la Pack_Stuffing_Length
					if (!stream.zero())  // Dc. exista indicator de stuffing 
					{
						jmp=stream.ret_bit_value(5,7,true);
						stream.skip_info(jmp); // Salt peste Pack_Stuffing_Bytes
						jmp=jmp+14;
					}
					else 
					{
						stream.go_back(1);
						jmp=12;
					}
					n_pks++;
					s_pks=s_pks+jmp;
					file.WriteLine("PACK header \t\tnr. "+n_pks.ToString()+"\t\tSize :\t"+jmp.ToString()+" bytes"+"\tSCR = "+SCR.ToString());
				}
				if(cod==187) // System header
				{
					jmp=stream.ret_value(2);
					stream.skip_info(jmp); // Salt peste packet
					n_sp++;
					jmp=jmp+6;
					s_sp=s_sp+jmp;
					file.WriteLine("System header \t\tnr. "+n_sp.ToString()+"\t\tSize :\t"+jmp.ToString()+" bytes");
				}
				if(cod==188) //PSM
			{
					jmp=stream.ret_value(2);
					stream.skip_info(jmp); // Salt peste packet
					n_psm++;
					jmp=jmp+6;
					s_psm=s_psm+jmp;
					file.WriteLine("Program Stream Map \t\tnr. "+n_psm.ToString()+"\t\tSize :\t"+jmp.ToString()+" bytes");
				}
				if((cod>=192)&&(cod<=223)) // PES Audio
				{
					jmp=stream.ret_value(2);
					stream.skip_info(1);
					dif=1;
					stp=stream.ret_bit(1);
					stamp="\tNo PTS";
					if (stream.ret_bit(0))
					{
						stream.skip_info(2);
						dif=dif+2;
						PTS=stream.get_PES_stamp();
						stamp="\tPTS = "+PTS.ToString();
						dif=dif+5;
					}
					if (stp)
					{
						DTS=stream.get_PES_stamp();
						stamp+="\t DTS = "+DTS.ToString();
						dif=dif+5;
					}
					else stamp+="\t No DTS";
					stream.skip_info(jmp-dif); // Salt peste restul pachetului
					n_ap++;
					jmp=jmp+6;
					s_ap=s_ap+jmp;
					file.WriteLine("Audio PES \t\tnr. "+n_ap.ToString()+"\t\tSize :\t"+jmp.ToString()+" bytes"+stamp);
				}
				if((cod>=224)&&(cod<=239)) // PES Video
				{
					jmp=stream.ret_value(2);
					stream.skip_info(1);
					dif=1;
					stp=stream.ret_bit(1);
					stamp="\tNo PTS";
					if (stream.ret_bit(0))
					{
						stream.skip_info(2);
						dif=dif+2;
						PTS=stream.get_PES_stamp();
						stamp="\tPTS = "+PTS.ToString();
						dif=dif+5;
					}
					if (stp)
					{
						DTS=stream.get_PES_stamp();
						stamp+="\t DTS = "+DTS.ToString();
						dif=dif+5;
					}
					else stamp+="\t No DTS";
					stream.skip_info(jmp-dif); // Salt peste packet
					n_vp++;
					jmp=jmp+6;
					s_vp=s_vp+jmp;
					file.WriteLine("Video PES \t\tnr. "+n_vp.ToString()+"\t\tSize :\t"+jmp.ToString()+" bytes"+stamp);
				}
				if((cod>=240)&&(cod<=255)) // PES Data
				{
					jmp=stream.ret_value(2);
					stream.skip_info(jmp); // Salt peste packet
					n_op++;
					jmp=jmp+6;
					s_op=s_op+jmp;
					file.WriteLine("Data PES \t\tnr. "+n_op.ToString()+"\t\tSize :\t"+jmp.ToString()+" bytes");
				}
				if(cod==189) // PES Private stream 1
				{
					jmp=stream.ret_value(2);
					stream.skip_info(jmp); // Salt peste packet
					n_op++;
					jmp=jmp+6;
					s_op=s_op+jmp;
					file.WriteLine("Private stream 1 PES \tnr. "+n_op.ToString()+"\t\tSize :\t"+jmp.ToString()+" bytes");
				}
				if(cod==190) // PES Padding
				{
					jmp=stream.ret_value(2);
					stream.skip_info(jmp); // Salt peste packet
					n_op++;
					jmp=jmp+6;
					s_op=s_op+jmp;
					file.WriteLine("Padding stream PES \tnr. "+n_op.ToString()+"\t\tSize :\t"+jmp.ToString()+" bytes");
				}
				if(cod==191) // PES Private stream 2
				{
					jmp=stream.ret_value(2);
					stream.skip_info(jmp); // Salt peste packet
					n_op++;
					jmp=jmp+6;
					s_op=s_op+jmp;
					file.WriteLine("Private stream 2 PES \tnr. "+n_op.ToString()+"\t\tSize :\t"+jmp.ToString()+" bytes");
				}
			}
			if(r_end) file.WriteLine("MPEG_end packet reached");
			else file.WriteLine("No MPEG_end packet encountered !");
			if(r_ok) 
			{
				file.WriteLine("File read completed ok...");
				ok.Text="Yes";
			}
			else 
			{
				file.WriteLine("Error...unknown format");
				ok.Text="No";
			}
			file.WriteLine("");
			file.WriteLine("Statistic :");
			file.WriteLine("-----------");
			file.WriteLine("Pack headers     :  "+n_pks.ToString()+"\t   Total size : "+s_pks.ToString()+" bytes");
			file.WriteLine("System headers   :  "+n_sp.ToString()+"\t\t   Total size : "+s_sp.ToString()+" bytes");
			file.WriteLine("PSM packets      :  "+n_psm.ToString()+"\t\t   Total size : "+s_psm.ToString()+" bytes");
			file.WriteLine("Video packets    :  "+n_vp.ToString()+"\t   Total size : "+s_vp.ToString()+" bytes");
			file.WriteLine("Audio packets    :  "+n_ap.ToString()+"\t\t   Total size : "+s_ap.ToString()+" bytes");
			file.WriteLine("Other packets    :  "+n_op.ToString()+"\t\t   Total size : "+s_op.ToString()+" bytes");
			file.Close();
			stream.end_PS();
			pks.Text=n_pks.ToString();
			sh.Text=n_sp.ToString();
			ap.Text=n_ap.ToString();
			vp.Text=n_vp.ToString();
			psm.Text=n_psm.ToString();
			op.Text=n_op.ToString();
		}

		private void st_Click(object sender, System.EventArgs e)
		{
			st.Enabled=false;
			stream = new PS(brow.path+f_in.Text);
			pks.Text=sh.Text=ap.Text=vp.Text=op.Text=ok.Text=psm.Text="";
			this.Refresh();
			process_PS();
			st.Enabled=true;
		}

		private void ps_analyzer_Load(object sender, System.EventArgs e)
		{
			refresh_list();
		}

		private void browse_Click(object sender, System.EventArgs e)
		{
			brow.Show();
			brow.init(this);
			refresh_list();
		}
	

			}
}
