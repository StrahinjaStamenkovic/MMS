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
		private System.Windows.Forms.MenuItem FilterGrayScale;
		private System.Windows.Forms.MenuItem FilterBrightness;
		private System.Windows.Forms.MenuItem FilterContrast;
		private System.Windows.Forms.MenuItem FilterGamma;
        private System.Windows.Forms.MenuItem FilterColor;
        private double Zoom = 1.0;
		private System.Windows.Forms.MenuItem menuItem5;
        private System.Windows.Forms.MenuItem Undo;
		private MenuItem menuItem15;
        private MenuItem menuItem16;
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
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.FilterInvert = new System.Windows.Forms.MenuItem();
            this.FilterGrayScale = new System.Windows.Forms.MenuItem();
            this.FilterBrightness = new System.Windows.Forms.MenuItem();
            this.FilterContrast = new System.Windows.Forms.MenuItem();
            this.FilterGamma = new System.Windows.Forms.MenuItem();
            this.FilterColor = new System.Windows.Forms.MenuItem();
            this.menuItem15 = new System.Windows.Forms.MenuItem();
            this.menuItem16 = new System.Windows.Forms.MenuItem();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1,
            this.menuItem5,
            this.menuItem4});
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
            // menuItem4
            // 
            this.menuItem4.Index = 2;
            this.menuItem4.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.FilterInvert,
            this.FilterGrayScale,
            this.FilterBrightness,
            this.FilterContrast,
            this.FilterGamma,
            this.FilterColor,
            this.menuItem15,
            this.menuItem16});
            this.menuItem4.Text = "Filter";
            // 
            // FilterInvert
            // 
            this.FilterInvert.Index = 0;
            this.FilterInvert.Text = "Invert";
            this.FilterInvert.Click += new System.EventHandler(this.Filter_Invert);
            // 
            // FilterGrayScale
            // 
            this.FilterGrayScale.Index = 1;
            this.FilterGrayScale.Text = "GrayScale";
            this.FilterGrayScale.Click += new System.EventHandler(this.Filter_GrayScale);
            // 
            // FilterBrightness
            // 
            this.FilterBrightness.Index = 2;
            this.FilterBrightness.Text = "Brightness";
            this.FilterBrightness.Click += new System.EventHandler(this.Filter_Brightness);
            // 
            // FilterContrast
            // 
            this.FilterContrast.Index = 3;
            this.FilterContrast.Text = "Contrast";
            this.FilterContrast.Click += new System.EventHandler(this.Filter_Contrast);
            // 
            // FilterGamma
            // 
            this.FilterGamma.Index = 4;
            this.FilterGamma.Text = "Gamma";
            this.FilterGamma.Click += new System.EventHandler(this.Filter_Gamma);
            // 
            // FilterColor
            // 
            this.FilterColor.Index = 5;
            this.FilterColor.Text = "Color";
            this.FilterColor.Click += new System.EventHandler(this.Filter_Color);
            // 
            // menuItem15
            // 
            this.menuItem15.Index = 6;
            this.menuItem15.Text = "InvertUnsafe";
            this.menuItem15.Click += new System.EventHandler(this.menuItem15_Click);
            // 
            // menuItem16
            // 
            this.menuItem16.Index = 7;
            this.menuItem16.Text = "PaintY";
            this.menuItem16.Click += new System.EventHandler(this.menuItem16_Click);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(616, 304);
            this.Menu = this.mainMenu1;
            this.Name = "Form1";
            this.Text = "Image Filters ";
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
			openFileDialog.FilterIndex = 2 ;
			openFileDialog.RestoreDirectory = true ;

			if(DialogResult.OK == openFileDialog.ShowDialog())
			{
				m_Bitmap = (Bitmap)Bitmap.FromFile(openFileDialog.FileName, false);
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
            //m_Undo = m_Bitmap.Clone() as Bitmap;
            //if (m_Undo == null)
            //    return;
			m_Undo = (Bitmap)m_Bitmap.Clone();

			if(BitmapFilter.Invert(m_Bitmap))
				this.Invalidate();
		}
				

		delegate bool ImageProcessingMethod(Bitmap b);

		private void ProcessImage(ImageProcessingMethod methodToExecute, Bitmap b)
		{
			m_Undo = (Bitmap)m_Bitmap.Clone();
			if (methodToExecute(b))
				this.Invalidate();
		}

		private void menuItem15_Click(object sender, EventArgs e)
		{
			ProcessImage(BitmapFilter.InvertH, m_Bitmap);
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
				if(BitmapFilter.Brightness(m_Bitmap, dlg.nValue))
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
				if(BitmapFilter.Contrast(m_Bitmap, (sbyte)dlg.nValue))
					this.Invalidate();
			}
		
		}

		private void Filter_Gamma(object sender, System.EventArgs e)
		{
			GammaInput dlg = new GammaInput();
			dlg.red = dlg.green = dlg.blue = 1;

			if (DialogResult.OK == dlg.ShowDialog())
			{
				m_Undo = (Bitmap)m_Bitmap.Clone();
				if(BitmapFilter.Gamma(m_Bitmap, dlg.red, dlg.green, dlg.blue))
					this.Invalidate();
			}
		}

		private void Filter_Color(object sender, System.EventArgs e)
		{
			ColorInput dlg = new ColorInput();
			dlg.red = dlg.green = dlg.blue = 0;

			if (DialogResult.OK == dlg.ShowDialog())
			{
				m_Undo = (Bitmap)m_Bitmap.Clone();
				if(BitmapFilter.Color(m_Bitmap, dlg.red, dlg.green, dlg.blue))
					this.Invalidate();
			}
		
		}
        

		private void OnUndo(object sender, System.EventArgs e)
		{
			Bitmap temp = (Bitmap)m_Bitmap.Clone();
			m_Bitmap = (Bitmap)m_Undo.Clone();
			m_Undo = (Bitmap)temp.Clone();
			this.Invalidate();
		}
				

        private void menuItem16_Click(object sender, EventArgs e)
        {
            m_Undo = (Bitmap)m_Bitmap.Clone();
            BitmapFilter.PrintYBitmapUnsafe(m_Bitmap);
                this.Invalidate();	
        }

		
	}
}

