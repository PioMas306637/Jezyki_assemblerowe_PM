using System.Drawing.Imaging;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml;
using System.Threading;

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
                //zamiast import to load
                for (int i = 0; i < selectedNoOfThreads; i++)
                {
                    int amountOfPixels = calculateNoOfPixels(pixelRowCount, selectedNoOfThreads, i);
                    int byteOffset = CalculateBottomRange(pixelRowCount, selectedNoOfThreads, i)
                        * pixelColumnCount * bytesForOnePixel;
                    //int ret = GrayPixelsVector(1, 2, 3, 6,
                    //   amountOfPixels, bytesForOnePixel, byteOffset, arrayForAss);
                    Thread t = new Thread(() =>
                    {
                        [DllImport(@"C:\Users\Pioter\source\repos\Jezyki_assemblerowe_PM\projekt\szkielet\x64\Debug\dll_assembler.dll")]
                        unsafe static extern int GrayPixelsVector(int wB, int wG, int wR, int wS,
                        int arrayS, int increment, int startingIndex, byte[] pointer);
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
                [DllImport(@"C:\Users\Pioter\source\repos\Jezyki_assemblerowe_PM\projekt\szkielet\x64\Debug\dll_cpp.dll")]
                unsafe static extern int grayPixels(int wB, int wG, int wR, int wS,
                int arrayS, int increment, int startingIndex, byte[] pointer);
                for (int i = 0; i < selectedNoOfThreads; i++)
                {
                    int amountOfPixels = calculateNoOfPixels(pixelRowCount, selectedNoOfThreads, i);
                    int byteOffset = CalculateBottomRange(pixelRowCount, selectedNoOfThreads, i)
                        * pixelColumnCount * bytesForOnePixel;
                    //int ret = grayPixels(wB, wR, wG, wS,
                    //   amountOfPixels, bytesForOnePixel, byteOffset, arrayForAss);
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
                        label_error.Text = " ";
                        runCpp = true;
                        prepVars = true;
                        break;
                    case "ass":
                        label_error.Text = " ";
                        runAss = true;
                        prepVars = true;
                        break;
                    default:
                        label_error.Text = "select one dll to run!";
                        break;
                }
            }
            else
            {
                label_error.Text = "select a picture to run!";
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
                    label_error.Text = "conversion took " + czasMili.ToString() + " ms";
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
                    label_error.Text = "conversion took " + czasMili.ToString() + " ms";

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
                label_error.Text = "select a VALID picture to run!";
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
                    label_error.Text = "no image to save";
                }
            }
            catch
            {
                label_error.Text = "something went wrong";
            }
        }

    }
}
