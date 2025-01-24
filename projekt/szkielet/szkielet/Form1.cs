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
            private List<VarsForThread> nonCommonVars = new List<VarsForThread>();
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
                int pRowCount, int pColumnCount, int b, int g, int r)
            {
                arrayStartOffset = 53;
                bytesForOnePixel = bForOnePixel;
                selectedNoOfThreads = sNoOfThreads;
                pixelRowCount = pRowCount;
                pixelColumnCount = pColumnCount;
                wB = b;
                wG = g;
                wR = r;
                wS = wB + wG + wR;
                wS = (wS == 0) ? 1 : wS;

                int curTop = CalculateTopRange(pixelRowCount, 1, 0);
                int curBot = CalculateBottomRange(pixelRowCount, 1, 0);
                int curArrayPixelCount = (curTop - curBot) * pixelColumnCount;
                commonArrayByteCount = curArrayPixelCount * bytesForOnePixel;
            }
            public void prepNonCommonVars()
            {
                int curTop;
                int curBot;
                int arrayPixelCount;
                //int arrayByteCount;
                for (int i = 0; i < selectedNoOfThreads; i++)
                {
                    curTop = CalculateTopRange(pixelRowCount, selectedNoOfThreads, i);
                    curBot = CalculateBottomRange(pixelRowCount, selectedNoOfThreads, i);
                    arrayPixelCount = (curTop - curBot) * pixelColumnCount;
                    //arrayByteCount = arrayPixelCount * bytesForOnePixel;
                    nonCommonVars.Add(new VarsForThread(curTop, curBot, arrayPixelCount));
                }
            }
            public void prepArray()
            {
                MemoryStream stream = new MemoryStream();
                image.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
                bytesFromPic = stream.ToArray();
                arrayForAss = new byte[commonArrayByteCount];
                Array.ConstrainedCopy(bytesFromPic, arrayStartOffset + 1, arrayForAss, 0, commonArrayByteCount);
            }
            public void executeGrayfication()
            {
                [DllImport(@"C:\Users\Pioter\source\repos\Jezyki_assemblerowe_PM\projekt\szkielet\x64\Debug\dll_assembler.dll")]
                unsafe static extern int GrayPixels(int wB, int wG, int wR, int wS, byte[] pointer, int arrayS, int increment);
                for (int i = 0; i < selectedNoOfThreads; i++)
                {
                    int retVal = GrayPixels(wB, wG, wR, wS,
                        arrayForAss,
                        nonCommonVars[i].getPixelCount(),
                        bytesForOnePixel);
                }
            }
            public void MultiThreadexecuteGrayfication()
            {
                [DllImport(@"C:\Users\Pioter\source\repos\Jezyki_assemblerowe_PM\projekt\szkielet\x64\Debug\dll_assembler.dll")]
                unsafe static extern int GrayPixelsMulti(int wB, int wG, int wR, int wS, byte[] pointer, int arrayS, int increment, int startingIndex);
                for (int i = 0; i < selectedNoOfThreads; i++)
                {
                    int amountOfPixels = calculateNoOfPixels(pixelRowCount, selectedNoOfThreads, i);
                    int byteOffset = CalculateBottomRange(pixelRowCount, selectedNoOfThreads, i) * pixelColumnCount * bytesForOnePixel;
                    // int retVal = GrayPixelsMulti(wB, wG, wR, wS, arrayForAss, amountOfPixels, bytesForOnePixel, byteOffset);

                    Thread t = new Thread(() =>
                    {
                        GrayPixelsMulti(wB, wG, wR, wS, arrayForAss, amountOfPixels, bytesForOnePixel, byteOffset);
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
            public void setArray(byte[] input)
            {
                arrayForAss = input;
            }
            public byte[] getArray()
            {
                return arrayForAss;
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
                int wB = (int)numericUpDown_blue.Value;
                int wG = (int)numericUpDown_green.Value;
                int wR = (int)numericUpDown_red.Value;

                koordynator.prepCommonVars(bytesForOnePixel,
                    selectedNoOfThreads,
                    pixelRowCount,
                    pixelColumnCount,
                    wB, wG, wR);
                koordynator.prepNonCommonVars();
                if (runCpp)
                {
                    //run dll

                    //save results

                    //present results
                }
                if (runAss)
                {
                    var watch = System.Diagnostics.Stopwatch.StartNew();

                    koordynator.setImage(pictureBox_before_grayscale.Image);
                    koordynator.prepArray();
                    //koordynator.executeGrayfication();
                    koordynator.MultiThreadexecuteGrayfication();
                    koordynator.prepFinalImage();
                    pictureBox_after_grayscale.Image = koordynator.getImage();

                    watch.Stop();
                    var czasMili = watch.ElapsedMilliseconds;
                    label_error.Text = "zamiana zajê³a " + czasMili.ToString() + " ms";
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

    }
}
