using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace szkielet
{

    public partial class Form1 : Form
    {
        private bool pictureIsLoaded = false;
        unsafe private int hellishCast(byte[] toCast){ return (int)&toCast;}
        private void GrayscalePixels(byte pointer, int height, int width,
            int weightB, int weightG, int weightR)
        {

        }
        public byte[] ImageToByte(Bitmap img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }
        private int CalculateTopRange(int totalHeight,
            int noOfThreads,
            int threadNumber)
        {
            int value = 0;
            if (threadNumber == noOfThreads)
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

            if (pictureIsLoaded)
            {
                //prepare range
                int pixelRowCount = pictureBox_before_grayscale.Image.Height;
                int pixelcolumnCount = pictureBox_before_grayscale.Image.Width;
                int i = 0; //number of current thread - locked to 0 for testing
                const int arrayStartOffset = 53;
                int selectedNoOfThreads = (int)numericUpDown_no_of_threads.Value;
                int top = CalculateTopRange(pixelRowCount, selectedNoOfThreads, i);
                int bot = CalculateBottomRange(pixelRowCount, selectedNoOfThreads, i);
                int arrSize = (top - bot) * pixelcolumnCount;

                MemoryStream stream = new MemoryStream();
                pictureBox_before_grayscale.Image.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
                byte[] bytes = stream.ToArray();
                //byte array order - 0-53 additional info 54+ pixels in B-G-R-alpha order


                switch (comboBox_select_dll.Text)
                {
                    case "cpp":
                        label_error.Text = " ";
                        //run dll

                        //save results

                        //present results

                        break;
                    case "ass":
                        label_error.Text = " ";
                        [DllImport(@"C:\Users\Pioter\source\repos\Jezyki_assemblerowe_PM\projekt\szkielet\x64\Debug\dll_assembler.dll")]
                        static extern int GrayPixels(int wB, int wG, int wR, int wS, int pointer, int arrayS);
                        int x = 1;
                        int wB = (int)numericUpDown_blue.Value;
                        int wG = (int)numericUpDown_green.Value;
                        int wR = (int)numericUpDown_red.Value;
                        int hellishCast1 = hellishCast(bytes);
                        int wS = wB + wG + wR;
                        int retVal = GrayPixels(wB, wG, wR, wS,
                            hellishCast1,
                            arrSize);
                        int j = 0;
                        //GrayscalePixels(bytes, (top - bot), pixelcolumnCount,
                        //    (int)numericUpDown_blue.Value,
                        //    (int)numericUpDown_green.Value,
                        //    (int)numericUpDown_red.Value);


                        break;
                    default:
                        label_error.Text = "select one dll to run!";

                        break;
                }
            } else
            {
                label_error.Text = "select a picture to run!";
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
