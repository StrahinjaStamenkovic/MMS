using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace CSharpFilters
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		
		private System.Drawing.Bitmap m_Bitmap;
		private System.Drawing.Bitmap m_Undo;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem FileLoad;
		private System.Windows.Forms.MenuItem FileSave;
		private System.Windows.Forms.MenuItem FileExit;
        private System.Windows.Forms.MenuItem FilterInvert;
		private System.Windows.Forms.MenuItem FilterBrightness;
		private System.Windows.Forms.MenuItem FilterContrast;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem Zoom25;
		private double Zoom = 1.0;
		private System.Windows.Forms.MenuItem Zoom50;
		private System.Windows.Forms.MenuItem Zoom100;
		private System.Windows.Forms.MenuItem Zoom150;
		private System.Windows.Forms.MenuItem Zoom200;
		private System.Windows.Forms.MenuItem Zoom300;
		private System.Windows.Forms.MenuItem Zoom500;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem Undo;
		private System.Windows.Forms.MenuItem menuItem15;
		private System.Windows.Forms.MenuItem menuItem16;
		private System.Windows.Forms.MenuItem menuItem17;
		private System.Windows.Forms.MenuItem menuItem18;
		private System.Windows.Forms.MenuItem menuItem19;
		private System.Windows.Forms.MenuItem menuItem10;
		private System.Windows.Forms.MenuItem menuItem11;
		private System.Windows.Forms.MenuItem menuItem12;
		private System.Windows.Forms.MenuItem menuItem13;
		private System.Windows.Forms.MenuItem menuItem14;
		private System.Windows.Forms.MenuItem menuItem20;
		private IContainer components;

		public Form1()
		{
			InitializeComponent();

			m_Bitmap= new Bitmap(2, 2);
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
            this.components = new System.ComponentModel.Container();
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.FileLoad = new System.Windows.Forms.MenuItem();
            this.FileSave = new System.Windows.Forms.MenuItem();
            this.FileExit = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.Undo = new System.Windows.Forms.MenuItem();
            this.menuItem15 = new System.Windows.Forms.MenuItem();
            this.menuItem16 = new System.Windows.Forms.MenuItem();
            this.menuItem17 = new System.Windows.Forms.MenuItem();
            this.menuItem18 = new System.Windows.Forms.MenuItem();
            this.menuItem19 = new System.Windows.Forms.MenuItem();
            this.menuItem10 = new System.Windows.Forms.MenuItem();
            this.menuItem11 = new System.Windows.Forms.MenuItem();
            this.menuItem12 = new System.Windows.Forms.MenuItem();
            this.menuItem13 = new System.Windows.Forms.MenuItem();
            this.menuItem20 = new System.Windows.Forms.MenuItem();
            this.menuItem14 = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.FilterInvert = new System.Windows.Forms.MenuItem();
            this.FilterBrightness = new System.Windows.Forms.MenuItem();
            this.FilterContrast = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.Zoom25 = new System.Windows.Forms.MenuItem();
            this.Zoom50 = new System.Windows.Forms.MenuItem();
            this.Zoom100 = new System.Windows.Forms.MenuItem();
            this.Zoom150 = new System.Windows.Forms.MenuItem();
            this.Zoom200 = new System.Windows.Forms.MenuItem();
            this.Zoom300 = new System.Windows.Forms.MenuItem();
            this.Zoom500 = new System.Windows.Forms.MenuItem();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1,
            this.menuItem5,
            this.menuItem15,
            this.menuItem4,
            this.menuItem2});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.FileLoad,
            this.FileSave,
            this.FileExit});
            this.menuItem1.Text = "File";
            // 
            // FileLoad
            // 
            this.FileLoad.Index = 0;
            this.FileLoad.Shortcut = System.Windows.Forms.Shortcut.CtrlL;
            this.FileLoad.Text = "Load";
            this.FileLoad.Click += new System.EventHandler(this.File_Load);
            // 
            // FileSave
            // 
            this.FileSave.Index = 1;
            this.FileSave.Shortcut = System.Windows.Forms.Shortcut.CtrlS;
            this.FileSave.Text = "Save";
            this.FileSave.Click += new System.EventHandler(this.File_Save);
            // 
            // FileExit
            // 
            this.FileExit.Index = 2;
            this.FileExit.Text = "Exit";
            this.FileExit.Click += new System.EventHandler(this.File_Exit);
            // 
            // menuItem5
            // 
            this.menuItem5.Index = 1;
            this.menuItem5.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.Undo});
            this.menuItem5.Text = "Edit";
            // 
            // Undo
            // 
            this.Undo.Index = 0;
            this.Undo.Text = "Undo";
            this.Undo.Click += new System.EventHandler(this.OnUndo);
            // 
            // menuItem15
            // 
            this.menuItem15.Index = 2;
            this.menuItem15.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem16,
            this.menuItem17,
            this.menuItem18,
            this.menuItem19,
            this.menuItem10,
            this.menuItem11,
            this.menuItem12,
            this.menuItem13,
            this.menuItem20,
            this.menuItem14});
            this.menuItem15.Text = "Filter Palette";
            // 
            // menuItem16
            // 
            this.menuItem16.Index = 0;
            this.menuItem16.Text = "Invert";
            this.menuItem16.Click += new System.EventHandler(this.menuItem16_Click);
            // 
            // menuItem17
            // 
            this.menuItem17.Index = 1;
            this.menuItem17.Text = "GrayScale";
            this.menuItem17.Click += new System.EventHandler(this.menuItem17_Click);
            // 
            // menuItem18
            // 
            this.menuItem18.Index = 2;
            this.menuItem18.Text = "Brightness";
            this.menuItem18.Click += new System.EventHandler(this.menuItem18_Click);
            // 
            // menuItem19
            // 
            this.menuItem19.Index = 3;
            this.menuItem19.Text = "Contrast";
            this.menuItem19.Click += new System.EventHandler(this.menuItem19_Click);
            // 
            // menuItem10
            // 
            this.menuItem10.Index = 4;
            this.menuItem10.Text = "Red";
            this.menuItem10.Click += new System.EventHandler(this.menuItem10_Click);
            // 
            // menuItem11
            // 
            this.menuItem11.Index = 5;
            this.menuItem11.Text = "Green";
            this.menuItem11.Click += new System.EventHandler(this.menuItem11_Click);
            // 
            // menuItem12
            // 
            this.menuItem12.Index = 6;
            this.menuItem12.Text = "Blue";
            this.menuItem12.Click += new System.EventHandler(this.menuItem12_Click);
            // 
            // menuItem13
            // 
            this.menuItem13.Index = 7;
            this.menuItem13.Text = "To BW";
            this.menuItem13.Click += new System.EventHandler(this.menuItem13_Click);
            // 
            // menuItem20
            // 
            this.menuItem20.Index = 8;
            this.menuItem20.Text = "To Gray4";
            this.menuItem20.Click += new System.EventHandler(this.menuItem20_Click);
            // 
            // menuItem14
            // 
            this.menuItem14.Index = 9;
            this.menuItem14.Text = "To Gray16";
            this.menuItem14.Click += new System.EventHandler(this.menuItem14_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 3;
            this.menuItem4.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.FilterInvert,
            this.FilterBrightness,
            this.FilterContrast});
            this.menuItem4.Text = "False Color Image";
            // 
            // FilterInvert
            // 
            this.FilterInvert.Index = 0;
            this.FilterInvert.Text = "Invert";
            this.FilterInvert.Click += new System.EventHandler(this.Filter_Invert);
            // 
            // FilterBrightness
            // 
            this.FilterBrightness.Index = 1;
            this.FilterBrightness.Text = "Brightness";
            this.FilterBrightness.Click += new System.EventHandler(this.Filter_Brightness);
            // 
            // FilterContrast
            // 
            this.FilterContrast.Index = 2;
            this.FilterContrast.Text = "Contrast";
            this.FilterContrast.Click += new System.EventHandler(this.Filter_Contrast);
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 4;
            this.menuItem2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.Zoom25,
            this.Zoom50,
            this.Zoom100,
            this.Zoom150,
            this.Zoom200,
            this.Zoom300,
            this.Zoom500});
            this.menuItem2.Text = "Zoom";
            // 
            // Zoom25
            // 
            this.Zoom25.Index = 0;
            this.Zoom25.Text = "25%";
            this.Zoom25.Click += new System.EventHandler(this.OnZoom25);
            // 
            // Zoom50
            // 
            this.Zoom50.Index = 1;
            this.Zoom50.Text = "50%";
            this.Zoom50.Click += new System.EventHandler(this.OnZoom50);
            // 
            // Zoom100
            // 
            this.Zoom100.Index = 2;
            this.Zoom100.Text = "100%";
            this.Zoom100.Click += new System.EventHandler(this.OnZoom100);
            // 
            // Zoom150
            // 
            this.Zoom150.Index = 3;
            this.Zoom150.Text = "150%";
            this.Zoom150.Click += new System.EventHandler(this.OnZoom150);
            // 
            // Zoom200
            // 
            this.Zoom200.Index = 4;
            this.Zoom200.Text = "200%";
            this.Zoom200.Click += new System.EventHandler(this.OnZoom200);
            // 
            // Zoom300
            // 
            this.Zoom300.Index = 5;
            this.Zoom300.Text = "300%";
            this.Zoom300.Click += new System.EventHandler(this.OnZoom300);
            // 
            // Zoom500
            // 
            this.Zoom500.Index = 6;
            this.Zoom500.Text = "500%";
            this.Zoom500.Click += new System.EventHandler(this.OnZoom500);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(616, 324);
            this.Menu = this.mainMenu1;
            this.Name = "Form1";
            this.Text = "Image Filters 8b BMP";
            this.Load += new System.EventHandler(this.Form1_Load);
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

		protected override void OnPaint (PaintEventArgs e)
		{
			Graphics g = e.Graphics;

			
			g.DrawImage(m_Bitmap, new Rectangle(this.AutoScrollPosition.X, this.AutoScrollPosition.Y, (int)(m_Bitmap.Width*Zoom), (int)(m_Bitmap.Height * Zoom)));
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{
		}

		private void File_Load(object sender, System.EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();

			openFileDialog.InitialDirectory = "c:\\" ;
			openFileDialog.Filter = "Bitmap files (*.bmp)|*.bmp|Jpeg files (*.jpg)|*.jpg|GIF files(*.gif)|*.gif|PNG files(*.png)|*.png|All valid files|*.bmp/*.jpg/*.gif/*.png";
			openFileDialog.FilterIndex = 1 ;
			openFileDialog.RestoreDirectory = true ;

			if(DialogResult.OK == openFileDialog.ShowDialog())
			{
				m_Bitmap = (Bitmap)Bitmap.FromFile(openFileDialog.FileName, false);
				BitmapFilter.filepath = openFileDialog.FileName;
				
				this.AutoScroll = true;
				this.AutoScrollMinSize = new Size ((int)(m_Bitmap.Width * Zoom), (int)(m_Bitmap.Height * Zoom));
				this.Invalidate();
			}
		}

		private void File_Save(object sender, System.EventArgs e)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();

			saveFileDialog.InitialDirectory = "c:\\" ;
			saveFileDialog.Filter = "Bitmap files (*.bmp)|*.bmp|Jpeg files (*.jpg)|*.jpg|All valid files (*.bmp/*.jpg)|*.bmp/*.jpg" ;
			saveFileDialog.FilterIndex = 1 ;
			saveFileDialog.RestoreDirectory = true ;

			if(DialogResult.OK == saveFileDialog.ShowDialog())
			{
				m_Bitmap.Save(saveFileDialog.FileName);
			}
		}

		private void File_Exit(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void Filter_Invert(object sender, System.EventArgs e)
		{
			m_Undo = (Bitmap)m_Bitmap.Clone();

			if(BitmapFilter.InvertImg(m_Bitmap))
				this.Invalidate();
		}

		private void Filter_GrayScale(object sender, System.EventArgs e)
		{
			ColorInput dlg = new ColorInput();
			dlg.red = dlg.green = dlg.blue = 0;

			if (DialogResult.OK == dlg.ShowDialog())
			{
				m_Undo = (Bitmap)m_Bitmap.Clone();
				if(BitmapFilter.GrayScale(m_Bitmap, dlg.red, dlg.green, dlg.blue))
					this.Invalidate();
			}
			
		}

		private void Filter_Brightness(object sender, System.EventArgs e)
		{
			Parameter dlg = new Parameter();
			dlg.nValue = 0;

			if (DialogResult.OK == dlg.ShowDialog())
			{
				m_Undo = (Bitmap)m_Bitmap.Clone();
				if(BitmapFilter.BrightnessImg(m_Bitmap, dlg.nValue))
					this.Invalidate();
			}
		}

		private void Filter_Contrast(object sender, System.EventArgs e)
		{
			Parameter dlg = new Parameter();
			dlg.nValue = 0;

			if (DialogResult.OK == dlg.ShowDialog())
			{
				m_Undo = (Bitmap)m_Bitmap.Clone();
				if(BitmapFilter.ContrastImg(m_Bitmap, (sbyte)dlg.nValue))
					this.Invalidate();
			}
		
		}

		
		private void OnZoom25(object sender, System.EventArgs e)
		{
			Zoom = .25;
			this.AutoScrollMinSize = new Size ((int)(m_Bitmap.Width * Zoom), (int)(m_Bitmap.Height * Zoom));
			this.Invalidate();
		}

		private void OnZoom50(object sender, System.EventArgs e)
		{
			Zoom = .5;
			this.AutoScrollMinSize = new Size ((int)(m_Bitmap.Width * Zoom), (int)(m_Bitmap.Height * Zoom));
			this.Invalidate();
		}

		private void OnZoom100(object sender, System.EventArgs e)
		{
			Zoom = 1.0;
			this.AutoScrollMinSize = new Size ((int)(m_Bitmap.Width * Zoom), (int)(m_Bitmap.Height * Zoom));
			this.Invalidate();
		}

		private void OnZoom150(object sender, System.EventArgs e)
		{
			Zoom = 1.5;
			this.AutoScrollMinSize = new Size ((int)(m_Bitmap.Width * Zoom), (int)(m_Bitmap.Height * Zoom));
			this.Invalidate();
		}

		private void OnZoom200(object sender, System.EventArgs e)
		{
			Zoom = 2.0;
			this.AutoScrollMinSize = new Size ((int)(m_Bitmap.Width * Zoom), (int)(m_Bitmap.Height * Zoom));
			this.Invalidate();
		}

		private void OnZoom300(object sender, System.EventArgs e)
		{
			Zoom = 3.0;
			this.AutoScrollMinSize = new Size ((int)(m_Bitmap.Width * Zoom), (int)(m_Bitmap.Height * Zoom));
			this.Invalidate();
		}

		private void OnZoom500(object sender, System.EventArgs e)
		{
			Zoom = 5;
			this.AutoScrollMinSize = new Size ((int)(m_Bitmap.Width * Zoom), (int)(m_Bitmap.Height * Zoom));
			this.Invalidate();
		}		

		private void OnUndo(object sender, System.EventArgs e)
		{
			Bitmap temp = (Bitmap)m_Bitmap.Clone();
			m_Bitmap = (Bitmap)m_Undo.Clone();
			m_Undo = (Bitmap)temp.Clone();
			this.Invalidate();
		}
			

		private void menuItem16_Click(object sender, System.EventArgs e)
		{
			m_Undo = (Bitmap)m_Bitmap.Clone();

			if(BitmapFilter.Invert(m_Bitmap))
				this.Invalidate();
		}

		private void menuItem18_Click(object sender, System.EventArgs e)
		{
			Parameter dlg = new Parameter();
			dlg.nValue = 0;

			if (DialogResult.OK == dlg.ShowDialog())
			{
				m_Undo = (Bitmap)m_Bitmap.Clone();
				if(BitmapFilter.Brightness(m_Bitmap, dlg.nValue))
					this.Invalidate();
			}
		}

		private void menuItem19_Click(object sender, System.EventArgs e)
		{
			Parameter dlg = new Parameter();
			dlg.nValue = 0;

			if (DialogResult.OK == dlg.ShowDialog())
			{
				m_Undo = (Bitmap)m_Bitmap.Clone();
				if(BitmapFilter.Contrast(m_Bitmap, (sbyte)dlg.nValue))
					this.Invalidate();
			}
		}

		private void menuItem17_Click(object sender, System.EventArgs e)
		{
			ColorInput dlg = new ColorInput();
			dlg.red = dlg.green = dlg.blue = 0;

			if (DialogResult.OK == dlg.ShowDialog())
			{
				m_Undo = (Bitmap)m_Bitmap.Clone();
				if(BitmapFilter.GrayScale(m_Bitmap, dlg.red, dlg.green, dlg.blue))
					this.Invalidate();
			}
			
		}

				

		private void menuItem10_Click(object sender, System.EventArgs e)
		{
			m_Undo = (Bitmap)m_Bitmap.Clone();
			if(BitmapFilter.RedChannel(m_Bitmap))
				this.Invalidate();
		}

		private void menuItem11_Click(object sender, System.EventArgs e)
		{
			
			m_Undo = (Bitmap)m_Bitmap.Clone();
			if(BitmapFilter.GreenChannel(m_Bitmap))
				this.Invalidate();
		}

		private void menuItem12_Click(object sender, System.EventArgs e)
		{
			
			m_Undo = (Bitmap)m_Bitmap.Clone();
			if(BitmapFilter.BlueChannel(m_Bitmap))
				this.Invalidate();
		}

		private void menuItem13_Click(object sender, System.EventArgs e)
		{
			m_Undo = (Bitmap)m_Bitmap.Clone();
			if(BitmapFilter.DiversityTo2(m_Bitmap))
				this.Invalidate();
		}

		private void menuItem14_Click(object sender, System.EventArgs e)
		{
			m_Undo = (Bitmap)m_Bitmap.Clone();
			if(BitmapFilter.DiversityTo16(m_Bitmap))
				this.Invalidate();
		}

		private void menuItem20_Click(object sender, System.EventArgs e)
		{
			m_Undo = (Bitmap)m_Bitmap.Clone();
			if(BitmapFilter.DiversityTo4(m_Bitmap))
				this.Invalidate();
		}

		
	
	}
}

