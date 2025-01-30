using System.Drawing.Imaging;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml;
using System.Threading;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;


namespace szkielet
{

    public partial class Form1 : Form
    {
        private class VarsForThread
        {
            int top;
            int bot;
            int pixelCount;
            public VarsForThread(int curTop, int curBot, int curPixelCount)
            {
                top = curTop;
                bot = curBot;
                pixelCount = curPixelCount;
            }
            public int getPixelCount() { return pixelCount; }
        };
        private class GrayCoordinator
        {
            private List<Thread> threadList = new List<Thread>();
            int threads = 0; //number of current thread

            int arrayStartOffset;
            int bytesForOnePixel;
            int selectedNoOfThreads = 0;
            int pixelRowCount;
            int pixelColumnCount;
            int wB;
            int wG;
            int wR;
            int wS;
            int commonArrayByteCount;
            byte[] arrayForAss;
            byte[] bytesFromPic;
            //byte array order:
            //0-53 - additional info
            //54+ - pixels in B-G-R-alpha order
            Image image;


            private int CalculateTopRange(int totalHeight, int noOfThreads, int threadNumber)
            {
                int value = 0;
                if (threadNumber == noOfThreads - 1)
                    value = totalHeight;
                else
                    value = (totalHeight / noOfThreads) * threadNumber;
                return value;
            }
            private int CalculateBottomRange(int totalHeight, int noOfThreads, int threadNumber)
            {
                int value = (totalHeight / noOfThreads) * threadNumber;
                return value;
            }
            private int calculateNoOfPixels(int totalHeight, int noOfThreads, int threadNumber)
            {
                int output = 0;
                if ((threadNumber + 1) == noOfThreads)
                    output = totalHeight - (totalHeight / noOfThreads) * threadNumber;
                else
                    output = (totalHeight / noOfThreads);
                return output * pixelColumnCount;
            }
            public void prepCommonVars(int bForOnePixel, int sNoOfThreads,
                int pRowCount, int pColumnCount, float b, float g, float r)
            {
                arrayStartOffset = 53;
                bytesForOnePixel = bForOnePixel;
                selectedNoOfThreads = sNoOfThreads;
                pixelRowCount = pRowCount;
                pixelColumnCount = pColumnCount;
                wB = (int)(b * 1000.0f);
                wG = (int)(g * 1000.0f);
                wR = (int)(r * 1000.0f);
                wS = wB + wG + wR;
                wS = (wS == 0) ? 1 : wS;

                int curTop = CalculateTopRange(pixelRowCount, 1, 0);
                int curBot = CalculateBottomRange(pixelRowCount, 1, 0);
                int curArrayPixelCount = (curTop - curBot) * pixelColumnCount;
                commonArrayByteCount = curArrayPixelCount * bytesForOnePixel;
            }
            public void prepArray()
            {
                MemoryStream stream = new MemoryStream();
                image.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
                bytesFromPic = stream.ToArray();
                arrayForAss = new byte[commonArrayByteCount];
                Array.ConstrainedCopy(bytesFromPic, arrayStartOffset + 1, arrayForAss, 0, commonArrayByteCount);
            }
            public void RunAssemblerMultiThreadWithXMM()
            {

                #if DEBUG
                [DllImport(@"..\..\..\..\..\x64\Debug\dll_assembler.dll")]
                #else
                [DllImport(@"dll_assembler.dll")] 
                #endif
                unsafe static extern int GrayPixelsVector(int wB, int wG, int wR, int wS,
                int arrayS, int increment, int startingIndex, byte[] pointer);
                for (int i = 0; i < selectedNoOfThreads; i++)
                {
                    int amountOfPixels = calculateNoOfPixels(pixelRowCount, selectedNoOfThreads, i);
                    int byteOffset = CalculateBottomRange(pixelRowCount, selectedNoOfThreads, i)
                        * pixelColumnCount * bytesForOnePixel;
                    Thread t = new Thread(() =>
                    {
                        GrayPixelsVector(wB, wR, wG, wS, amountOfPixels, bytesForOnePixel, byteOffset, arrayForAss);
                    });
                    threadList.Add(t);
                }
                for (int i = 0; i < selectedNoOfThreads; i++)
                {
                    threadList[i].Start();
                }
                for (int i = 0; i < selectedNoOfThreads; i++)
                {
                    threadList[i].Join();
                }
                threadList.Clear();
            }
            public void RunCppMultiThread()
            {

#if DEBUG
                [DllImport(@"..\..\..\..\..\x64\Debug\dll_cpp.dll")]
#else
                [DllImport(@"dll_cpp.dll")]
#endif
                unsafe static extern int grayPixels(int wB, int wG, int wR, int wS,
                int arrayS, int increment, int startingIndex, byte[] pointer);
                for (int i = 0; i < selectedNoOfThreads; i++)
                {
                    int amountOfPixels = calculateNoOfPixels(pixelRowCount, selectedNoOfThreads, i);
                    int byteOffset = CalculateBottomRange(pixelRowCount, selectedNoOfThreads, i)
                        * pixelColumnCount * bytesForOnePixel;
                    Thread t = new Thread(() =>
                    {
                        grayPixels(wB, wR, wG, wS, amountOfPixels, bytesForOnePixel, byteOffset, arrayForAss);
                    });
                    threadList.Add(t);
                }
                for (int i = 0; i < selectedNoOfThreads; i++)
                {
                    threadList[i].Start();
                }
                for (int i = 0; i < selectedNoOfThreads; i++)
                {
                    threadList[i].Join();
                }
                threadList.Clear();
            }

            public void prepFinalImage()
            {
                byte[] arrayForNewPic = new byte[bytesFromPic.Length];
                Array.ConstrainedCopy(bytesFromPic, 0, arrayForNewPic, 0, arrayStartOffset);
                Array.ConstrainedCopy(arrayForAss, 0, arrayForNewPic, arrayStartOffset + 1, arrayForAss.Length);
                Bitmap bmp;
                using (var ms = new MemoryStream(arrayForNewPic))
                {
                    bmp = new Bitmap(ms);
                }
                image = bmp;
            }
            public void setImage(Image img)
            {
                image = img;
            }
            public Image getImage()
            {
                return image;
            }
        };
        private bool pictureIsLoaded = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int wykrytaIlosc = Environment.ProcessorCount;
            label_no_of_cores.Text = wykrytaIlosc.ToString();
            numericUpDown_no_of_threads.Value = wykrytaIlosc - 1;
        }

        private void button_run_Click(object sender, EventArgs e)
        {
            bool runCpp = false;
            bool runAss = false;
            bool prepVars = false;
            if (pictureIsLoaded)
            {
                switch (comboBox_select_dll.Text)
                {
                    case "cpp":
                        label_error_2.Text = " ";
                        runCpp = true;
                        prepVars = true;
                        break;
                    case "ass":
                        label_error_2.Text = " ";
                        runAss = true;
                        prepVars = true;
                        break;
                    default:
                        label_error_2.Text = "select one dll to run!";
                        break;
                }
            }
            else
            {
                label_error_2.Text = "select a picture to run!";
            }

            if (prepVars)
            {
                GrayCoordinator koordynator = new GrayCoordinator();

                int bytesForOnePixel = (pictureBox_before_grayscale.Image.PixelFormat.HasFlag(PixelFormat.Alpha)) ? 4 : 3;
                int selectedNoOfThreads = (int)numericUpDown_no_of_threads.Value;
                int pixelRowCount = pictureBox_before_grayscale.Image.Height;
                int pixelColumnCount = pictureBox_before_grayscale.Image.Width;
                float wB = (float)numericUpDown_blue.Value;
                float wG = (float)numericUpDown_green.Value;
                float wR = (float)numericUpDown_red.Value;

                koordynator.prepCommonVars(bytesForOnePixel, selectedNoOfThreads,
                    pixelRowCount, pixelColumnCount, wB, wG, wR);
                if (runCpp)
                {
                    koordynator.setImage(pictureBox_before_grayscale.Image);
                    koordynator.prepArray();
                    var watch = System.Diagnostics.Stopwatch.StartNew();
                    koordynator.RunCppMultiThread();
                    watch.Stop();
                    var czasMili = watch.ElapsedMilliseconds;
                    koordynator.prepFinalImage();
                    pictureBox_after_grayscale.Image = koordynator.getImage();
                    label_error_2.Text = "function execution took " + czasMili.ToString() + " ms";
                }
                if (runAss)
                {
                    koordynator.setImage(pictureBox_before_grayscale.Image);
                    koordynator.prepArray();
                    var watch = System.Diagnostics.Stopwatch.StartNew();
                    koordynator.RunAssemblerMultiThreadWithXMM();
                    watch.Stop();
                    var czasMili = watch.ElapsedMilliseconds;
                    koordynator.prepFinalImage();
                    pictureBox_after_grayscale.Image = koordynator.getImage();
                    label_error_2.Text = "function execution took " + czasMili.ToString() + " ms";

                }
            }

        }

        private void button_pick_picture_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.ShowDialog();
                Bitmap picture =
                    new Bitmap(openFileDialog1.FileName);
                pictureBox_before_grayscale.Image = picture;
                pictureIsLoaded = true;
            }
            catch (System.ArgumentException ex)
            {
                label_error_2.Text = "select a VALID picture to run!";
            }
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            try
            {
                saveFileDialog1.FileName = Path.GetFileNameWithoutExtension(openFileDialog1.FileName)
                    + "_" + comboBox_select_dll.Text + "_"
                    + numericUpDown_no_of_threads.Value + "_threads"
                    + ".jpg";
                saveFileDialog1.ShowDialog();
                if (pictureBox_after_grayscale.Image != null)
                {
                    using (Bitmap bmp = new Bitmap(pictureBox_after_grayscale.Image))
                    {
                        bmp.Save(saveFileDialog1.FileName, ImageFormat.Jpeg);
                    }
                }
                else
                {
                    label_error_2.Text = "no image to save";
                }
            }
            catch
            {
                label_error_2.Text = "something went wrong";
            }
        }

        private void label_grayscale_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }


        private void button_histos_Click(object sender, EventArgs e)
        {
            try
            {
                label_color_or_gs.Text = "color";
                int[] red = new int[256];
                int[] green = new int[256];
                int[] blue = new int[256];
                for (int x = 0; x < pictureBox_before_grayscale.Image.Width; x++)
                {
                    for (int y = 0; y < pictureBox_before_grayscale.Image.Height; y++)
                    {
                        Color pixel = ((Bitmap)pictureBox_before_grayscale.Image).GetPixel(x, y);
                        red[pixel.R]++;
                        green[pixel.G]++;
                        blue[pixel.B]++;
                    }
                }
                Chart chartr = new Chart();
                Chart chartg = new Chart();
                Chart chartb = new Chart();
                chartr.Series.Add("red");
                chartg.Series.Add("green");
                chartb.Series.Add("blue");
                chartr.Series["red"].Points.Clear();
                chartg.Series["green"].Points.Clear();
                chartb.Series["blue"].Points.Clear();
                chartr.Series["red"].ChartType = SeriesChartType.Line;
                chartg.Series["green"].ChartType = SeriesChartType.Line;
                chartb.Series["blue"].ChartType = SeriesChartType.Line;
                for (int i = 0; i < 256; i++)
                {
                    chartr.Series["red"].Points.AddXY(i, red[i]);
                    chartg.Series["green"].Points.AddXY(i, green[i]);
                    chartb.Series["blue"].Points.AddXY(i, blue[i]);
                }
                //chart.BackColor = Color.Black;
                //chart.ForeColor = Color.White;
                //chart.chart.ChartAreas.Add(new ChartArea());

                chartr.ChartAreas.Add(new ChartArea());
                chartr.ChartAreas[0].AxisX.Title = "Intensity";
                chartr.ChartAreas[0].AxisY.Title = "Frequency";
                chartg.ChartAreas.Add(new ChartArea());
                chartg.ChartAreas[0].AxisX.Title = "Intensity";
                chartg.ChartAreas[0].AxisY.Title = "Frequency";
                chartb.ChartAreas.Add(new ChartArea());
                chartb.ChartAreas[0].AxisX.Title = "Intensity";
                chartb.ChartAreas[0].AxisY.Title = "Frequency";
                chartr.Scale(new SizeF(2, 0.5f));
                chartg.Scale(new SizeF(2, 0.5f));
                chartb.Scale(new SizeF(2, 0.5f));
                chartr.Series[0].Color = Color.Red;
                chartg.Series[0].Color = Color.Green;
                chartb.Series[0].Color = Color.Blue;

                float maxFrequency = Math.Max(Math.Max(red.Max(), green.Max()), blue.Max());

                chartr.ChartAreas[0].AxisY.Minimum = 0;
                chartr.ChartAreas[0].AxisY.Maximum = maxFrequency;

                chartg.ChartAreas[0].AxisY.Minimum = 0;
                chartg.ChartAreas[0].AxisY.Maximum = maxFrequency;

                chartb.ChartAreas[0].AxisY.Minimum = 0;
                chartb.ChartAreas[0].AxisY.Maximum = maxFrequency;

                // Create a bitmap to draw the chart to
                Bitmap bitmapr = new Bitmap(chartr.Width, chartr.Height);
                Bitmap bitmapg = new Bitmap(chartg.Width, chartg.Height);
                Bitmap bitmapb = new Bitmap(chartb.Width, chartb.Height);

                // Draw the chart to the bitmap
                chartr.DrawToBitmap(bitmapr, new Rectangle(0, 0, chartr.Width, chartr.Height));
                chartg.DrawToBitmap(bitmapg, new Rectangle(0, 0, chartg.Width, chartg.Height));
                chartb.DrawToBitmap(bitmapb, new Rectangle(0, 0, chartb.Width, chartb.Height));

                // Set the image of the PictureBox to the drawn bitmap
                pictureBox_chart_r.Image = bitmapr;
                pictureBox_chart_g.Image = bitmapg;
                pictureBox_chart_b.Image = bitmapb;



                chartr.Invalidate();
                chartg.Invalidate();
                chartb.Invalidate();
            }
            catch
            {
                label_error_2.Text = "failed to generate historammes";
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                label_color_or_gs.Text = "grays";
                int[] red = new int[256];
                int[] green = new int[256];
                int[] blue = new int[256];
                for (int x = 0; x < pictureBox_after_grayscale.Image.Width; x++)
                {
                    for (int y = 0; y < pictureBox_after_grayscale.Image.Height; y++)
                    {
                        Color pixel = ((Bitmap)pictureBox_after_grayscale.Image).GetPixel(x, y);
                        red[pixel.R]++;
                        green[pixel.G]++;
                        blue[pixel.B]++;
                    }
                }
                Chart chartr = new Chart();
                Chart chartg = new Chart();
                Chart chartb = new Chart();
                chartr.Series.Add("red");
                chartg.Series.Add("green");
                chartb.Series.Add("blue");
                chartr.Series["red"].Points.Clear();
                chartg.Series["green"].Points.Clear();
                chartb.Series["blue"].Points.Clear();
                chartr.Series["red"].ChartType = SeriesChartType.Line;
                chartg.Series["green"].ChartType = SeriesChartType.Line;
                chartb.Series["blue"].ChartType = SeriesChartType.Line;
                for (int i = 0; i < 256; i++)
                {
                    chartr.Series["red"].Points.AddXY(i, red[i]);
                    chartg.Series["green"].Points.AddXY(i, green[i]);
                    chartb.Series["blue"].Points.AddXY(i, blue[i]);
                }
                //chart.BackColor = Color.Black;
                //chart.ForeColor = Color.White;
                //chart.chart.ChartAreas.Add(new ChartArea());

                chartr.ChartAreas.Add(new ChartArea());
                chartr.ChartAreas[0].AxisX.Title = "Intensity";
                chartr.ChartAreas[0].AxisY.Title = "Frequency";
                chartg.ChartAreas.Add(new ChartArea());
                chartg.ChartAreas[0].AxisX.Title = "Intensity";
                chartg.ChartAreas[0].AxisY.Title = "Frequency";
                chartb.ChartAreas.Add(new ChartArea());
                chartb.ChartAreas[0].AxisX.Title = "Intensity";
                chartb.ChartAreas[0].AxisY.Title = "Frequency";
                chartr.Scale(new SizeF(2, 0.5f));
                chartg.Scale(new SizeF(2, 0.5f));
                chartb.Scale(new SizeF(2, 0.5f));
                chartr.Series[0].Color = Color.Red;
                chartg.Series[0].Color = Color.Green;
                chartb.Series[0].Color = Color.Blue;

                float maxFrequency = Math.Max(Math.Max(red.Max(), green.Max()), blue.Max());

                chartr.ChartAreas[0].AxisY.Minimum = 0;
                chartr.ChartAreas[0].AxisY.Maximum = maxFrequency;

                chartg.ChartAreas[0].AxisY.Minimum = 0;
                chartg.ChartAreas[0].AxisY.Maximum = maxFrequency;

                chartb.ChartAreas[0].AxisY.Minimum = 0;
                chartb.ChartAreas[0].AxisY.Maximum = maxFrequency;

                // Create a bitmap to draw the chart to
                Bitmap bitmapr = new Bitmap(chartr.Width, chartr.Height);
                Bitmap bitmapg = new Bitmap(chartg.Width, chartg.Height);
                Bitmap bitmapb = new Bitmap(chartb.Width, chartb.Height);

                // Draw the chart to the bitmap
                chartr.DrawToBitmap(bitmapr, new Rectangle(0, 0, chartr.Width, chartr.Height));
                chartg.DrawToBitmap(bitmapg, new Rectangle(0, 0, chartg.Width, chartg.Height));
                chartb.DrawToBitmap(bitmapb, new Rectangle(0, 0, chartb.Width, chartb.Height));

                // Set the image of the PictureBox to the drawn bitmap
                pictureBox_chart_r.Image = bitmapr;
                pictureBox_chart_g.Image = bitmapg;
                pictureBox_chart_b.Image = bitmapb;



                chartr.Invalidate();
                chartg.Invalidate();
                chartb.Invalidate();
            }
            catch
            {
                label_error_2.Text = "failed to generate historammes";
            }

        }

        private void button_save_2_Click(object sender, EventArgs e)
        {
            try
            {
                saveFileDialog1.FileName = Path.GetFileNameWithoutExtension(openFileDialog1.FileName)
                    + "_" + comboBox_select_dll.Text + "_"
                    + numericUpDown_no_of_threads.Value + "_threads_histo_"
                    + label_color_or_gs.Text
                    + ".jpg";
                saveFileDialog1.ShowDialog();
                if (pictureBox_chart_r.Image != null && pictureBox_chart_g.Image != null && pictureBox_chart_b.Image != null)
                {
                    int width = pictureBox_chart_r.Image.Width;
                    int height = pictureBox_chart_r.Image.Height;
                    int totalHeight = height * 3;
                    using (Bitmap finalBitmap = new Bitmap(width, totalHeight))
                    {
                        using (Graphics g = Graphics.FromImage(finalBitmap))
                        {
                            g.Clear(Color.White);
                            g.DrawImage(pictureBox_chart_r.Image, 0, 0);
                            g.DrawImage(pictureBox_chart_g.Image, 0, height);
                            g.DrawImage(pictureBox_chart_b.Image, 0, height * 2);
                            finalBitmap.Save(saveFileDialog1.FileName, ImageFormat.Jpeg);
                        }
                    }
                }
                else
                {
                    label_error_2.Text = "no image to save";
                }
            }
            catch
            {
                label_error_2.Text = "something went wrong";
            }
        }
    }
}
