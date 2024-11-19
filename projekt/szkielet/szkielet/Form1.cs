using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace szkielet
{

    public partial class Form1 : Form
    {
        private bool pictureIsLoaded = false;
        //unsafe private int hellishCast(byte[] toCast){ return (int)&toCast;}
        private void GrayscalePixels(byte pointer, int height, int width,
            int weightB, int weightG, int weightR)
        {

        }

        private int CalculateTopRange(int totalHeight,
            int noOfThreads,
            int threadNumber)
        {
            int value = 0;
            if (threadNumber == noOfThreads - 1)
                value = totalHeight;
            else
                value = (totalHeight / noOfThreads) * threadNumber;
            return value;
        }
        private int CalculateBottomRange(int totalHeight,
            int noOfThreads,
            int threadNumber)
        {
            int value = (totalHeight / noOfThreads) * threadNumber;
            return value;
        }


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

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
                const int arrayStartOffset = 53;
                int bytesForOnePixel;
                if (pictureBox_before_grayscale.Image.PixelFormat.HasFlag(PixelFormat.Alpha))
                    bytesForOnePixel = 4;
                else
                    bytesForOnePixel = 3;

                int selectedNoOfThreads = (int)numericUpDown_no_of_threads.Value;
                int threads = 0; //number of current thread - locked to 0 for testing

                int pixelRowCount = pictureBox_before_grayscale.Image.Height;
                int pixelcolumnCount = pictureBox_before_grayscale.Image.Width;
                int top = CalculateTopRange(pixelRowCount, selectedNoOfThreads, threads);
                int bot = CalculateBottomRange(pixelRowCount, selectedNoOfThreads, threads);
                int arrayPixelCount = (top - bot) * pixelcolumnCount;
                int arrayByteCount = arrayPixelCount * bytesForOnePixel;


                if (runCpp)
                {
                    //run dll

                    //save results

                    //present results
                }
                if (runAss)
                {
                    MemoryStream stream = new MemoryStream();
                    pictureBox_before_grayscale.Image.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
                    byte[] bytes = stream.ToArray();
                    byte[] arrayForAss = new byte[arrayByteCount];
                    Array.ConstrainedCopy(bytes, arrayStartOffset + 1, arrayForAss, 0, arrayByteCount);
                    //byte array order:
                    //0-53 - additional info
                    //54+ - pixels in B-G-R-alpha order
                    [DllImport(@"C:\Users\Pioter\source\repos\Jezyki_assemblerowe_PM\projekt\szkielet\x64\Debug\dll_assembler.dll")]
                    unsafe static extern int GrayPixels(int wB, int wG, int wR, int wS, byte[] pointer, int arrayS, int increment);
                    int wB = (int)numericUpDown_blue.Value;
                    int wG = (int)numericUpDown_green.Value;
                    int wR = (int)numericUpDown_red.Value;
                    int wS = wB + wG + wR;
                    int retVal = GrayPixels(wB, wG, wR, wS,
                        arrayForAss,
                        arrayPixelCount,
                        bytesForOnePixel);

                    byte[] arrayForNewPic = new byte[bytes.Length];
                    Array.ConstrainedCopy(bytes, 0, arrayForNewPic, 0, arrayStartOffset);
                    Array.ConstrainedCopy(arrayForAss, 0, arrayForNewPic, arrayStartOffset + 1, arrayForAss.Length);
                    Bitmap bmp;
                    using (var ms = new MemoryStream(arrayForNewPic))
                    {
                        bmp = new Bitmap(ms);
                    }
                    pictureBox_after_grayscale.Image = bmp;
                    //GrayscalePixels(bytes, (top - bot), pixelcolumnCount,
                    //    (int)numericUpDown_blue.Value,
                    //    (int)numericUpDown_green.Value,
                    //    (int)numericUpDown_red.Value);

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
