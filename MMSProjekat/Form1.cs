using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Windows.Forms.DataVisualization.Charting;
using System.IO;

namespace MMSProjekat
{
    public partial class Form1 : Form
    {
        private Bitmap bmp;
        private Bitmap channel1;
        private Bitmap channel2;
        private Bitmap channel3;
        private List<Bitmap> undoBuffer;
        private List<Bitmap> redoBuffer;
        private int maxBufferMemoryMB;
        private bool cmyShown;
        private bool histogramsShown;
        private bool useUnsafeMethods;
        private bool usePreviouslyCalculatedConvolutionalValues;
        private bool grayscaleShown;
        public Form1()
        {
            InitializeComponent();

            bmp = null;
            channel1 = null;
            channel2 = null;
            channel3 = null;

            undoBuffer = new List<Bitmap>();
            redoBuffer = new List<Bitmap>();
            maxBufferMemoryMB = 1000;

            cmyShown = false;
            grayscaleShown = false;
            histogramsShown = false;

            useUnsafeMethods = false;
            usePreviouslyCalculatedConvolutionalValues = false;

            UpdateToolStrips();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;

            if (bmp != null)
            {
                double axisScaleX = (300.0 / (double)bmp.Width);
                int w = 300;
                int h = (int)(bmp.Height * axisScaleX);

                int offsetY = menuStrip1.Height;
                g.DrawImage(bmp, new Rectangle(0, offsetY, w, h));
                if (channel1 != null && channel2 != null && channel3 != null && (cmyShown || grayscaleShown))
                {
                    g.DrawImage(channel1, new Rectangle(w, offsetY, w, h));
                    g.DrawImage(channel2, new Rectangle(0, offsetY + h, w, h));
                    g.DrawImage(channel3, new Rectangle(w, offsetY + h, w, h));
                }
                else if (histogramsShown)
                {
                    displayHistograms(g, w, h);
                }

            }
        }
        private void displayHistograms(Graphics g, int width, int height)
        {
            int offsetY = menuStrip1.Height;

            chart1.ChartAreas[0].AxisX.Maximum = 270;
            chart1.Location = new Point(width + g.RenderingOrigin.X, offsetY + g.RenderingOrigin.Y);
            chart1.Width = width;
            chart1.Height = height;

            int[] histogramValues = Filters.Histogram(channel1, CMY.C);
            chart1.ChartAreas[0].AxisY.Maximum = histogramValues.Max();
            chart1.Series[0].Points.Clear();
            for (int i = 0; i < histogramValues.Length; i++)
            {
                chart1.Series[0].Points.AddXY(i, histogramValues[i]);
            }

            chart2.ChartAreas[0].AxisX.Maximum = 270;
            chart2.Location = new Point(g.RenderingOrigin.X, offsetY + height + g.RenderingOrigin.Y);
            chart2.Width = width;
            chart2.Height = height;

            histogramValues = Filters.Histogram(channel2, CMY.M);
            chart2.ChartAreas[0].AxisY.Maximum = histogramValues.Max();
            chart2.Series[0].Points.Clear();
            for (int i = 0; i < histogramValues.Length; i++)
            {
                chart2.Series[0].Points.AddXY(i, histogramValues[i]);
            }

            chart3.ChartAreas[0].AxisX.Maximum = 270;
            chart3.Location = new Point(width + g.RenderingOrigin.X, offsetY + height + g.RenderingOrigin.Y);
            chart3.Width = width;
            chart3.Height = height;

            histogramValues = Filters.Histogram(channel3, CMY.Y);
            chart3.ChartAreas[0].AxisY.Maximum = histogramValues.Max();
            chart3.Series[0].Points.Clear();
            for (int i = 0; i < histogramValues.Length; i++)
            {
                chart3.Series[0].Points.AddXY(i, histogramValues[i]);
            }
        }
        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            openFileDialog.InitialDirectory = projectDirectory;
            openFileDialog.Filter = "Bitmap files (*.bmp)|*.bmp|Jpeg files (*.jpg)|*.jpg|GIF files(*.gif)|*.gif|PNG files(*.png)|*.png|All valid files|*.bmp;*.jpg;*.gif;*.png";
            openFileDialog.FilterIndex = 5;
            openFileDialog.RestoreDirectory = true;

            if (DialogResult.OK == openFileDialog.ShowDialog())
            {
                bmp = (Bitmap)Bitmap.FromFile(openFileDialog.FileName, false);

                if (bmp.PixelFormat != System.Drawing.Imaging.PixelFormat.Format24bppRgb)
                {
                    Bitmap newBmp = new Bitmap(bmp);
                    bmp = (Bitmap)newBmp.Clone(new Rectangle(0, 0, newBmp.Width, newBmp.Height), System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                }
                this.AutoScroll = true;

                
                if (channel1 != null && channel2 != null && channel3 != null)
                    calculateChannels();
                this.Invalidate();
            }

            clearBuffers();
            UpdateToolStrips();

        }
        private void clearBuffers()
        {
            undoBuffer.Clear();
            redoBuffer.Clear();
        }

        private void UpdateToolStrips()
        {
            saveToolStripMenuItem.Enabled = bmp != null;
            showToolStripMenuItem.Enabled = bmp != null;
            invertToolStripMenuItem.Enabled = bmp != null;

            embossLaplacianToolStripMenuItem.Enabled = bmp != null;
            x3ToolStripMenuItem.Enabled = bmp != null;
            x5ToolStripMenuItem.Enabled = bmp != null;
            x7ToolStripMenuItem.Enabled = bmp != null;

            randomJitterToolStripMenuItem.Enabled = bmp != null;
            gammaToolStripMenuItem.Enabled = bmp != null;
            edgeDetectDifferenceToolStripMenuItem.Enabled = bmp != null;
            clampChannelValuesToolStripMenuItem.Enabled = bmp != null && (cmyShown || histogramsShown);

            floydSteinbergDitherToolStripMenuItem.Enabled = bmp != null;
            stuckiDitherToolStripMenuItem.Enabled = bmp != null;
            orderedDitheringToolStripMenuItem.Enabled = bmp != null;
            x2ToolStripMenuItem.Enabled = bmp != null;
            x4ToolStripMenuItem.Enabled = bmp != null;
            x8ToolStripMenuItem.Enabled = bmp != null;

            switchBetweenGrayscaleAndCmyToolStripMenuItem.Enabled = bmp != null && !histogramsShown && (cmyShown || grayscaleShown);
            switchBetweenGrayscaleAndCmyToolStripMenuItem.Text = grayscaleShown ? "Switch to CMY" : "Switch to grayscale";

            switchChannelsToolStripMenuItem.Enabled = cmyShown || histogramsShown;
            switchChannelsToolStripMenuItem.Text = histogramsShown ? "Switch to CMY" : "Switch to histograms";

            colorizeGrayscaleChannelsToolStripMenuItem.Enabled = grayscaleShown;
            defaultMappingToolStripMenuItem.Enabled = grayscaleShown;
            mapAccordingToImageToolStripMenuItem.Enabled = grayscaleShown;
            crossdomainColorizeToolStripMenuItem.Enabled = grayscaleShown;

            kuwaharaToolStripMenuItem.Enabled = bmp != null;

            undoToolStripMenuItem.Enabled = undoBuffer.Count > 0;
            redoToolStripMenuItem.Enabled = redoBuffer.Count > 0;

            showToolStripMenuItem.Text = cmyShown ? "Hide CMY" : "Show CMY";
            showToolStripMenuItem.Enabled = bmp != null && !grayscaleShown && !histogramsShown;

            downsampleToolStripMenuItem.Enabled = bmp != null && cmyShown;
        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            saveFileDialog.InitialDirectory = projectDirectory;
            saveFileDialog.Filter = "Bitmap files (*.bmp)|*.bmp|Jpeg files (*.jpg)|*.jpg|All valid files (*.bmp/*.jpg)|*.bmp;*.jpg";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;

            if (DialogResult.OK == saveFileDialog.ShowDialog())
            {
                bmp.Save(saveFileDialog.FileName);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void invertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveToUndoBuffer((Bitmap)bmp.Clone());
            emptyRedoBuffer();
            bool result = false;
            if (useUnsafeMethods)
                result = Filters.InvertUnsafe(bmp);
            else
                result = Filters.InvertSafe(bmp);

            if (result)
            {
                
                if (channel1 != null && channel2 != null && channel3 != null)
                    calculateChannels();
                this.Invalidate();
            }

        }

        private void gammaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveToUndoBuffer((Bitmap)bmp.Clone());
            emptyRedoBuffer();

            GammaInput dlg = new GammaInput();
            dlg.red = dlg.green = dlg.blue = 1;

            if (DialogResult.OK == dlg.ShowDialog())
            {
                if (Filters.Gamma(bmp, (double)dlg.red, (double)dlg.green, (double)dlg.blue))
                {
                    
                    if (channel1 != null && channel2 != null && channel3 != null)
                        calculateChannels();
                    this.Invalidate();
                }
            }
        }

        private void saveToUndoBuffer(Bitmap b)
        {
            double memoryTakenMB = 0;
            foreach (Bitmap bmp in undoBuffer)
            {
                memoryTakenMB += bmp.Width * bmp.Height * 3;
            }
            memoryTakenMB += b.Width * b.Height * 3;
            memoryTakenMB /= 1048576;

            while (memoryTakenMB > maxBufferMemoryMB && undoBuffer.Count > 0)
            {
                Bitmap lastBmp = undoBuffer[0];
                undoBuffer.RemoveAt(0);
                memoryTakenMB -= (int)Math.Floor(((double)((double)lastBmp.Width * (double)lastBmp.Height) * 3) / 1048576.0);

                //foreach (Bitmap bmp in undoBuffer)
                //{
                //    memoryTakenMB += bmp.Width * bmp.Height;
                //}
                //memoryTakenMB /= 1000;
            }
            undoBuffer.Add(b);
            UpdateToolStrips();
        }
        private void emptyRedoBuffer()
        {
            redoBuffer.Clear();
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cmyShown = !cmyShown;
            UpdateToolStrips();

            calculateChannels();
            this.Invalidate();
        }
        private void calculateChannels()
        {
            channel1 = (Bitmap)bmp.Clone();
            channel2 = (Bitmap)bmp.Clone();
            channel3 = (Bitmap)bmp.Clone();

            if (cmyShown || histogramsShown)
            {
                Filters.ConvertBGRtoCMY(channel1, CMY.C);
                Filters.ConvertBGRtoCMY(channel2, CMY.M);
                Filters.ConvertBGRtoCMY(channel3, CMY.Y);
            }
            else if (grayscaleShown)
            {
                Bitmap g1 = (Bitmap)bmp.Clone();
                Bitmap g2 = (Bitmap)bmp.Clone();
                Bitmap g3 = (Bitmap)bmp.Clone();


                if (!Filters.GrayscaleMax(g1))
                    return;
                if (!Filters.GrayscaleMaxMin(g2))
                    return;
                if (!Filters.GrayscaleMean(g3))
                    return;

                channel1 = g1;
                channel2 = g2;
                channel3 = g3;
            }

        }

        private void useUnsafeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            useUnsafeMethods = !useUnsafeMethods;
            enableUnsafeToolStripMenuItem.Text = useUnsafeMethods ? "Disable unsafe methods" : "Enable unsafe methods";
        }

        private void enableConvValuesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            usePreviouslyCalculatedConvolutionalValues = !usePreviouslyCalculatedConvolutionalValues;
            enableConvValuesToolStripMenuItem.Text = usePreviouslyCalculatedConvolutionalValues ? "Disable conv. values" : "Enable conv. values";

        }

        private void randomJitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveToUndoBuffer((Bitmap)bmp.Clone());
            emptyRedoBuffer();
            if (FiltersDisplacement.RandomJitter(bmp))
            {
                
                if (channel1 != null && channel2 != null && channel3 != null)
                    calculateChannels();
                this.Invalidate();
            }
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            redoBuffer.Add((Bitmap)bmp.Clone());
            bmp = undoBuffer[undoBuffer.Count - 1];
            undoBuffer.RemoveAt(undoBuffer.Count - 1);

            UpdateToolStrips();
            
            if (channel1 != null && channel2 != null && channel3 != null)
                calculateChannels();
            this.Invalidate();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveToUndoBuffer((Bitmap)bmp.Clone());
            bmp = redoBuffer[redoBuffer.Count - 1];
            redoBuffer.RemoveAt(redoBuffer.Count - 1);

            UpdateToolStrips();
            
            if (channel1 != null && channel2 != null && channel3 != null)
                calculateChannels();
            this.Invalidate();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings dlg = new Settings(maxBufferMemoryMB);
            if (DialogResult.OK == dlg.ShowDialog())
            {
                maxBufferMemoryMB = dlg.memorySize;
                resizeUndoBuffer();
                UpdateToolStrips();
            }
        }
        private void resizeUndoBuffer()
        {
            double memoryTakenMB = 0.0;
            foreach (Bitmap bmp in undoBuffer)
            {
                memoryTakenMB += bmp.Width * bmp.Height * 3;
            }
            memoryTakenMB /= 1048576.0;
            while (memoryTakenMB > maxBufferMemoryMB && undoBuffer.Count > 0)
            {
                Bitmap lastBmp = undoBuffer[0];
                undoBuffer.RemoveAt(0);
                memoryTakenMB -= (int)Math.Floor(((double)((double)lastBmp.Width * (double)lastBmp.Height) * 3) / 1048576.0);
                //foreach (Bitmap bmp in undoBuffer)
                //{
                //    memoryTakenMB += bmp.Width * bmp.Height;
                //}
                //memoryTakenMB /= 1000;
            }
            UpdateToolStrips();
        }

        private void edgeDetectDifferenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Threshold dlg = new Threshold();
            if (DialogResult.OK == dlg.ShowDialog())
            {
                saveToUndoBuffer((Bitmap)bmp.Clone());
                emptyRedoBuffer();

                if (FiltersEdgeDetection.EdgeDetectDifference(bmp, (byte)dlg.thresholdValue))
                {
                    
                    if (channel1 != null && channel2 != null && channel3 != null)
                        calculateChannels();
                    this.Invalidate();
                }
            }
        }

        private void x3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveToUndoBuffer((Bitmap)bmp.Clone());
            emptyRedoBuffer();

            FiltersConvolution.ConvMatrix m = new FiltersConvolution.ConvMatrix(FiltersConvolution.EmbossLaplacian3x3, 3);
            m.Offset = 127;

            if (FiltersConvolution.ConvolutionFilter(bmp, m, usePreviouslyCalculatedConvolutionalValues))
            {
                
                if (channel1 != null && channel2 != null && channel3 != null)
                    calculateChannels();
                this.Invalidate();
            }
        }

        //Nisam uspeo da nadjem dobru vrednost za offset za 5x5 i 7x7 kernel
        private void x5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveToUndoBuffer((Bitmap)bmp.Clone());
            emptyRedoBuffer();

            FiltersConvolution.ConvMatrix m = new FiltersConvolution.ConvMatrix(FiltersConvolution.EmbossLaplacian5x5, 5);
  
            m.Offset = 690;
            if (FiltersConvolution.ConvolutionFilter(bmp, m, usePreviouslyCalculatedConvolutionalValues))
            {
                
                if (channel1 != null && channel2 != null && channel3 != null)
                    calculateChannels();
                this.Invalidate();
            }
        }

        private void x7ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveToUndoBuffer((Bitmap)bmp.Clone());
            emptyRedoBuffer();
            FiltersConvolution.ConvMatrix m = new FiltersConvolution.ConvMatrix(FiltersConvolution.EmbossLaplacian7x7, 7);

            m.Offset = 1100;
            if (FiltersConvolution.ConvolutionFilter(bmp, m, usePreviouslyCalculatedConvolutionalValues))
            {
                
                if (channel1 != null && channel2 != null && channel3 != null)
                    calculateChannels();
                this.Invalidate();
            }
        }

        private void switchChannelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            histogramsShown = !histogramsShown;
            cmyShown = !cmyShown;

            chart1.Visible = !chart1.Visible;
            chart2.Visible = !chart2.Visible;
            chart3.Visible = !chart3.Visible;

            UpdateToolStrips();
            this.Invalidate();
        }

        private void clampChannelValuesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClampValuesInput dlg = new ClampValuesInput();

            if (DialogResult.OK == dlg.ShowDialog())
            {
                saveToUndoBuffer((Bitmap)bmp.Clone());
                emptyRedoBuffer();

                byte min = dlg.Min, max = dlg.Max;

                Filters.HistogramFilter(channel1, CMY.C, min, max);
                Filters.HistogramFilter(channel2, CMY.M, min, max);
                Filters.HistogramFilter(channel3, CMY.Y, min, max);

                Filters.ConvertCMYtoBGR(bmp, channel1, channel2, channel3);

                this.Invalidate();
            }
        }

        private void switchBetweenGrayscaleAndCMYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cmyShown = !cmyShown;
            histogramsShown = false;
            grayscaleShown = !grayscaleShown;

            UpdateToolStrips();

            calculateChannels();
            this.Invalidate();
        }


        private void x2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveToUndoBuffer((Bitmap)bmp.Clone());
            emptyRedoBuffer();
            // add a form to input the palette 
            if (Filters.OrderedDithering(bmp, Filters.OrderedDitherMap2x2, 32))
            {
                
                if (channel1 != null && channel2 != null && channel3 != null)
                    calculateChannels();
                this.Invalidate();
            }
        }

        private void x4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveToUndoBuffer((Bitmap)bmp.Clone());
            emptyRedoBuffer();
            if (Filters.OrderedDithering(bmp, Filters.OrderedDitherMap4x4, 32))
            {
                
                if (channel1 != null && channel2 != null && channel3 != null)
                    calculateChannels();
                this.Invalidate();
            }
        }

        private void x8ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveToUndoBuffer((Bitmap)bmp.Clone());
            emptyRedoBuffer();
            if (Filters.OrderedDithering(bmp, Filters.OrderedDitherMap8x8, 32))
            {
                
                if (channel1 != null && channel2 != null && channel3 != null)
                    calculateChannels();
                this.Invalidate();
            }
        }

        private void floydSteinbergDitherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveToUndoBuffer((Bitmap)bmp.Clone());
            emptyRedoBuffer();
            if (Filters.FloydSteinbergDither(bmp, 8))
            {
                
                if (channel1 != null && channel2 != null && channel3 != null)
                    calculateChannels();
                this.Invalidate();
            }
        }

        private void stuckiDitherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveToUndoBuffer((Bitmap)bmp.Clone());
            emptyRedoBuffer();
            if (Filters.StuckiDither(bmp, 64))
            {
                
                if (channel1 != null && channel2 != null && channel3 != null)
                    calculateChannels();
                this.Invalidate();
            }

        }
        private void defaultMappingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Filters.ColorizeDefault(channel1, Filters.DefaultGrayscaleToRGBMap))
                return;

            if (!Filters.ColorizeDefault(channel2, Filters.DefaultGrayscaleToRGBMap))
                return;

            if (!Filters.ColorizeDefault(channel3, Filters.DefaultGrayscaleToRGBMap))
                return;
            this.Invalidate();
        }

        private void mapAccordingToImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            openFileDialog.InitialDirectory = projectDirectory;
            openFileDialog.Filter = "Bitmap files (*.bmp)|*.bmp|Jpeg files (*.jpg)|*.jpg|GIF files(*.gif)|*.gif|PNG files(*.png)|*.png|All valid files|*.bmp;*.jpg;*.gif;*.png";
            openFileDialog.FilterIndex = 5;
            openFileDialog.RestoreDirectory = true;

            if (DialogResult.OK == openFileDialog.ShowDialog())
            {
                Bitmap mappingSource = (Bitmap)Bitmap.FromFile(openFileDialog.FileName, false);
                SortedDictionary<byte, byte[]> mappings = Filters.CalculateGrayscaleToRGBMappings(mappingSource);

                if (!Filters.ColorizeAccordingToSample(channel1, mappings))
                    return;

                if (!Filters.ColorizeAccordingToSample(channel2, mappings))
                    return;

                if (!Filters.ColorizeAccordingToSample(channel3, mappings))
                    return;
                this.Invalidate();
            }
        }

        private void crossdomainColorizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HueSaturationInput dlg = new HueSaturationInput();

            if (DialogResult.OK == dlg.ShowDialog())
            {
                if (!Filters.CrossDomainColorize(channel1, ((double)dlg.Hue), dlg.UseSaturationValue, ((double)dlg.Saturation)))
                    return;
                if (!Filters.CrossDomainColorize(channel2, ((double)dlg.Hue), dlg.UseSaturationValue, ((double)dlg.Saturation)))
                    return;
                if (!Filters.CrossDomainColorize(channel3, ((double)dlg.Hue), dlg.UseSaturationValue, ((double)dlg.Saturation)))
                    return;
                this.Invalidate();
            }
        }

        private void safeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveToUndoBuffer((Bitmap)bmp.Clone());
            emptyRedoBuffer();

            bmp = Filters.KuwaharaSafe(bmp, 5);
            
            if (channel1 != null && channel2 != null && channel3 != null)
                calculateChannels();
            this.Invalidate();
        }

        private void unsafeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveToUndoBuffer((Bitmap)bmp.Clone());
            emptyRedoBuffer();
            if (Filters.KuwaharaUnsafe(bmp, 5))
            {
                
                if (channel1 != null && channel2 != null && channel3 != null)
                    calculateChannels();
                this.Invalidate();
            }
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (bmp == null) return;
            //limit clicks only to image bounds
            double axisScaleX = (300.0 / (double)bmp.Width);
            int w = 300;
            int h = (int)(bmp.Height * axisScaleX);
            if (e.Location.X >= 0 && e.Location.X < w && e.Location.Y >= menuStrip1.Height && e.Location.Y < h + menuStrip1.Height)
            {

                ColorDialog dlg = new ColorDialog();
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    Threshold thresholdDlg = new Threshold();
                    if (thresholdDlg.ShowDialog() == DialogResult.OK)
                    {
                        saveToUndoBuffer((Bitmap)bmp.Clone());
                        emptyRedoBuffer();

                        int denormalizedX = (int)(((double)e.Location.X / (double)w) * (double)bmp.Width);
                        int denormalizedY = (int)(((double)(e.Location.Y - menuStrip1.Height) / (double)h) * (double)bmp.Height);

                        if (Filters.EqualizeColors(bmp, denormalizedX, denormalizedY, thresholdDlg.thresholdValue, dlg.Color.R, dlg.Color.G, dlg.Color.B))
                        {
                            
                            if (channel1 != null && channel2 != null && channel3 != null)
                                calculateChannels();
                            this.Invalidate();
                        }
                    }
                }
            }
        }

        private void downsampleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Bitmap cDownsampled = (Bitmap)channel1.Clone();
            //Bitmap mDownsampled = (Bitmap)channel2.Clone();
            //Bitmap yDownsampled = (Bitmap)channel3.Clone();

            //if (!Filters.Downsample(cDownsampled))
            //return;
            //if (!Filters.Downsample(mDownsampled))
            //return;
            //if (!Filters.Downsample(yDownsampled))
            //return;

            //Bitmap Cmy = (Bitmap)channel1.Clone();
            //Bitmap cMy = (Bitmap)channel2.Clone();
            //Bitmap cmY = (Bitmap)channel3.Clone();


            //Filters.ConvertCMYtoBGR(channel1, Cmy, mDownsampled, yDownsampled);
            //Filters.ConvertCMYtoBGR(channel2, cDownsampled, cMy, yDownsampled);
            //Filters.ConvertCMYtoBGR(channel3, cDownsampled, mDownsampled, cmY);
            CustomImageFormat imgC = new CustomImageFormat();
            imgC.FromBitmap(ref bmp);
            imgC.Downsample(CMY.C);

            CustomImageFormat imgM = new CustomImageFormat();
            imgM.FromBitmap(ref bmp);
            imgM.Downsample(CMY.M);

            CustomImageFormat imgY = new CustomImageFormat();
            imgY.FromBitmap(ref bmp);
            imgY.Downsample(CMY.Y);

            channel1 = imgC.ToBitmap();
            channel2 = imgM.ToBitmap();
            channel3 = imgY.ToBitmap();

            this.Invalidate();

            ImageInput dlg = new ImageInput();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                int ImgNum = dlg.ImageNumber;

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                string workingDirectory = Environment.CurrentDirectory;
                string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
                saveFileDialog.InitialDirectory = projectDirectory;
                saveFileDialog.Filter = "Binary file (*.bin)|*.bin";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;

                if (DialogResult.OK == saveFileDialog.ShowDialog())
                {
                    switch (ImgNum)
                    {
                        case 1:
                            imgC.SaveAndCompress(saveFileDialog.FileName);
                            break;
                        case 2:
                            imgM.SaveAndCompress(saveFileDialog.FileName);
                            break;
                        case 3:
                            imgY.SaveAndCompress(saveFileDialog.FileName);
                            break;
                    }
                }
            }
        }

        private void loadCompressedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            openFileDialog.InitialDirectory = projectDirectory;
            openFileDialog.Filter = "Binary files (*.bin)|*.bin";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            if (DialogResult.OK == openFileDialog.ShowDialog())
            {
                CustomImageFormat img = new CustomImageFormat();
                img.ReadAndDecompress(openFileDialog.FileName);
                bmp = img.ToBitmap();

                UpdateToolStrips();
                this.Invalidate();
            }
        }

    }
}