namespace szkielet
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label_grayscale = new Label();
            button_pick_picture = new Button();
            comboBox_select_dll = new ComboBox();
            numericUpDown_no_of_threads = new NumericUpDown();
            label_no_of_threads = new Label();
            label_detected_cores = new Label();
            label_gen_histo = new Label();
            checkBox_gen_hist = new CheckBox();
            label_color_weights = new Label();
            numericUpDown_red = new NumericUpDown();
            numericUpDown_green = new NumericUpDown();
            numericUpDown_blue = new NumericUpDown();
            label_red = new Label();
            label_green = new Label();
            label_blue = new Label();
            button_run = new Button();
            label_pick_dll = new Label();
            pictureBox_before_grayscale = new PictureBox();
            label_error = new Label();
            openFileDialog1 = new OpenFileDialog();
            pictureBox_after_grayscale = new PictureBox();
            label_no_of_cores = new Label();
            ((System.ComponentModel.ISupportInitialize)numericUpDown_no_of_threads).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown_red).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown_green).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown_blue).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox_before_grayscale).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox_after_grayscale).BeginInit();
            SuspendLayout();
            // 
            // label_grayscale
            // 
            label_grayscale.AutoSize = true;
            label_grayscale.Location = new Point(12, 9);
            label_grayscale.Name = "label_grayscale";
            label_grayscale.Size = new Size(72, 20);
            label_grayscale.TabIndex = 0;
            label_grayscale.Text = "Grayscale";
            // 
            // button_pick_picture
            // 
            button_pick_picture.Location = new Point(12, 32);
            button_pick_picture.Name = "button_pick_picture";
            button_pick_picture.Size = new Size(220, 29);
            button_pick_picture.TabIndex = 1;
            button_pick_picture.Text = "pick picture";
            button_pick_picture.UseVisualStyleBackColor = true;
            button_pick_picture.Click += button_pick_picture_Click;
            // 
            // comboBox_select_dll
            // 
            comboBox_select_dll.FormattingEnabled = true;
            comboBox_select_dll.Items.AddRange(new object[] { "cpp", "ass", "test" });
            comboBox_select_dll.Location = new Point(118, 69);
            comboBox_select_dll.Name = "comboBox_select_dll";
            comboBox_select_dll.Size = new Size(114, 28);
            comboBox_select_dll.TabIndex = 3;
            // 
            // numericUpDown_no_of_threads
            // 
            numericUpDown_no_of_threads.Location = new Point(165, 103);
            numericUpDown_no_of_threads.Maximum = new decimal(new int[] { 64, 0, 0, 0 });
            numericUpDown_no_of_threads.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDown_no_of_threads.Name = "numericUpDown_no_of_threads";
            numericUpDown_no_of_threads.Size = new Size(67, 27);
            numericUpDown_no_of_threads.TabIndex = 4;
            numericUpDown_no_of_threads.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label_no_of_threads
            // 
            label_no_of_threads.AutoSize = true;
            label_no_of_threads.Location = new Point(12, 108);
            label_no_of_threads.Name = "label_no_of_threads";
            label_no_of_threads.Size = new Size(100, 20);
            label_no_of_threads.TabIndex = 5;
            label_no_of_threads.Text = "no. of threads";
            // 
            // label_detected_cores
            // 
            label_detected_cores.AutoSize = true;
            label_detected_cores.Location = new Point(12, 142);
            label_detected_cores.Name = "label_detected_cores";
            label_detected_cores.Size = new Size(167, 20);
            label_detected_cores.TabIndex = 6;
            label_detected_cores.Text = "no. of detected cores = ";
            // 
            // label_gen_histo
            // 
            label_gen_histo.AutoSize = true;
            label_gen_histo.Location = new Point(12, 171);
            label_gen_histo.Name = "label_gen_histo";
            label_gen_histo.Size = new Size(145, 20);
            label_gen_histo.TabIndex = 7;
            label_gen_histo.Text = "generate histograms";
            // 
            // checkBox_gen_hist
            // 
            checkBox_gen_hist.AutoSize = true;
            checkBox_gen_hist.Location = new Point(214, 174);
            checkBox_gen_hist.Name = "checkBox_gen_hist";
            checkBox_gen_hist.Size = new Size(18, 17);
            checkBox_gen_hist.TabIndex = 8;
            checkBox_gen_hist.UseVisualStyleBackColor = true;
            // 
            // label_color_weights
            // 
            label_color_weights.AutoSize = true;
            label_color_weights.Location = new Point(12, 212);
            label_color_weights.Name = "label_color_weights";
            label_color_weights.Size = new Size(121, 20);
            label_color_weights.TabIndex = 9;
            label_color_weights.Text = "set color weights";
            // 
            // numericUpDown_red
            // 
            numericUpDown_red.DecimalPlaces = 4;
            numericUpDown_red.Increment = new decimal(new int[] { 1, 0, 0, 262144 });
            numericUpDown_red.Location = new Point(155, 241);
            numericUpDown_red.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDown_red.Name = "numericUpDown_red";
            numericUpDown_red.RightToLeft = RightToLeft.No;
            numericUpDown_red.Size = new Size(77, 27);
            numericUpDown_red.TabIndex = 10;
            numericUpDown_red.Value = new decimal(new int[] { 2126, 0, 0, 262144 });
            // 
            // numericUpDown_green
            // 
            numericUpDown_green.DecimalPlaces = 4;
            numericUpDown_green.Increment = new decimal(new int[] { 1, 0, 0, 262144 });
            numericUpDown_green.Location = new Point(155, 280);
            numericUpDown_green.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDown_green.Name = "numericUpDown_green";
            numericUpDown_green.Size = new Size(77, 27);
            numericUpDown_green.TabIndex = 11;
            numericUpDown_green.Value = new decimal(new int[] { 7152, 0, 0, 262144 });
            // 
            // numericUpDown_blue
            // 
            numericUpDown_blue.DecimalPlaces = 4;
            numericUpDown_blue.Increment = new decimal(new int[] { 1, 0, 0, 262144 });
            numericUpDown_blue.Location = new Point(155, 319);
            numericUpDown_blue.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDown_blue.Name = "numericUpDown_blue";
            numericUpDown_blue.Size = new Size(77, 27);
            numericUpDown_blue.TabIndex = 12;
            numericUpDown_blue.Value = new decimal(new int[] { 722, 0, 0, 262144 });
            // 
            // label_red
            // 
            label_red.AutoSize = true;
            label_red.Location = new Point(12, 243);
            label_red.Name = "label_red";
            label_red.Size = new Size(31, 20);
            label_red.TabIndex = 13;
            label_red.Text = "red";
            // 
            // label_green
            // 
            label_green.AutoSize = true;
            label_green.Location = new Point(12, 280);
            label_green.Name = "label_green";
            label_green.Size = new Size(47, 20);
            label_green.TabIndex = 14;
            label_green.Text = "green";
            // 
            // label_blue
            // 
            label_blue.AutoSize = true;
            label_blue.Location = new Point(12, 321);
            label_blue.Name = "label_blue";
            label_blue.Size = new Size(38, 20);
            label_blue.TabIndex = 15;
            label_blue.Text = "blue";
            // 
            // button_run
            // 
            button_run.Location = new Point(18, 371);
            button_run.Name = "button_run";
            button_run.Size = new Size(214, 29);
            button_run.TabIndex = 16;
            button_run.Text = "run!";
            button_run.UseVisualStyleBackColor = true;
            button_run.Click += button_run_Click;
            // 
            // label_pick_dll
            // 
            label_pick_dll.AutoSize = true;
            label_pick_dll.Location = new Point(12, 69);
            label_pick_dll.Name = "label_pick_dll";
            label_pick_dll.Size = new Size(57, 20);
            label_pick_dll.TabIndex = 17;
            label_pick_dll.Text = "pick dll";
            // 
            // pictureBox_before_grayscale
            // 
            pictureBox_before_grayscale.Location = new Point(306, 32);
            pictureBox_before_grayscale.Name = "pictureBox_before_grayscale";
            pictureBox_before_grayscale.Size = new Size(449, 164);
            pictureBox_before_grayscale.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox_before_grayscale.TabIndex = 18;
            pictureBox_before_grayscale.TabStop = false;
            // 
            // label_error
            // 
            label_error.AutoSize = true;
            label_error.Location = new Point(18, 421);
            label_error.Name = "label_error";
            label_error.Size = new Size(17, 20);
            label_error.TabIndex = 19;
            label_error.Text = "  ";
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // pictureBox_after_grayscale
            // 
            pictureBox_after_grayscale.Location = new Point(306, 236);
            pictureBox_after_grayscale.Name = "pictureBox_after_grayscale";
            pictureBox_after_grayscale.Size = new Size(449, 378);
            pictureBox_after_grayscale.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox_after_grayscale.TabIndex = 20;
            pictureBox_after_grayscale.TabStop = false;
            // 
            // label_no_of_cores
            // 
            label_no_of_cores.AutoSize = true;
            label_no_of_cores.Location = new Point(214, 142);
            label_no_of_cores.Name = "label_no_of_cores";
            label_no_of_cores.Size = new Size(16, 20);
            label_no_of_cores.TabIndex = 21;
            label_no_of_cores.Text = "?";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 643);
            Controls.Add(label_no_of_cores);
            Controls.Add(pictureBox_after_grayscale);
            Controls.Add(label_error);
            Controls.Add(pictureBox_before_grayscale);
            Controls.Add(label_pick_dll);
            Controls.Add(button_run);
            Controls.Add(label_blue);
            Controls.Add(label_green);
            Controls.Add(label_red);
            Controls.Add(numericUpDown_blue);
            Controls.Add(numericUpDown_green);
            Controls.Add(numericUpDown_red);
            Controls.Add(label_color_weights);
            Controls.Add(checkBox_gen_hist);
            Controls.Add(label_gen_histo);
            Controls.Add(label_detected_cores);
            Controls.Add(label_no_of_threads);
            Controls.Add(numericUpDown_no_of_threads);
            Controls.Add(comboBox_select_dll);
            Controls.Add(button_pick_picture);
            Controls.Add(label_grayscale);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)numericUpDown_no_of_threads).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown_red).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown_green).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown_blue).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox_before_grayscale).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox_after_grayscale).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label_grayscale;
        private Button button_pick_picture;
        private ComboBox comboBox_select_dll;
        private NumericUpDown numericUpDown_no_of_threads;
        private Label label_no_of_threads;
        private Label label_detected_cores;
        private Label label_gen_histo;
        private CheckBox checkBox_gen_hist;
        private Label label_color_weights;
        private NumericUpDown numericUpDown_red;
        private NumericUpDown numericUpDown_green;
        private NumericUpDown numericUpDown_blue;
        private Label label_red;
        private Label label_green;
        private Label label_blue;
        private Button button_run;
        private Label label_pick_dll;
        private PictureBox pictureBox_before_grayscale;
        private Label label_error;
        private OpenFileDialog openFileDialog1;
        private PictureBox pictureBox_after_grayscale;
        private Label label_no_of_cores;
    }
}
