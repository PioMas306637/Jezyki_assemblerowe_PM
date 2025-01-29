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
        /// 
        private void InitializeComponent()
        {
            label_error = new Label();
            openFileDialog1 = new OpenFileDialog();
            saveFileDialog1 = new SaveFileDialog();
            tabControl1 = new TabControl();
            conversion = new TabPage();
            button1 = new Button();
            button_histos = new Button();
            label_error_2 = new Label();
            button_save = new Button();
            label_no_of_cores = new Label();
            pictureBox_after_grayscale = new PictureBox();
            pictureBox_before_grayscale = new PictureBox();
            label_pick_dll = new Label();
            button_run = new Button();
            label_blue = new Label();
            label_green = new Label();
            label_red = new Label();
            numericUpDown_blue = new NumericUpDown();
            numericUpDown_green = new NumericUpDown();
            numericUpDown_red = new NumericUpDown();
            label_color_weights = new Label();
            label_detected_cores = new Label();
            label_no_of_threads = new Label();
            numericUpDown_no_of_threads = new NumericUpDown();
            comboBox_select_dll = new ComboBox();
            button_pick_picture = new Button();
            tabPage2 = new TabPage();
            button_save_2 = new Button();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            pictureBox_chart_b = new PictureBox();
            pictureBox_chart_g = new PictureBox();
            pictureBox_chart_r = new PictureBox();
            label_color_or_gs = new Label();
            tabControl1.SuspendLayout();
            conversion.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox_after_grayscale).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox_before_grayscale).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown_blue).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown_green).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown_red).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown_no_of_threads).BeginInit();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox_chart_b).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox_chart_g).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox_chart_r).BeginInit();
            SuspendLayout();
            // 
            // label_error
            // 
            label_error.AutoSize = true;
            label_error.Location = new Point(18, 518);
            label_error.Name = "label_error";
            label_error.Size = new Size(17, 20);
            label_error.TabIndex = 19;
            label_error.Text = "  ";
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(conversion);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Location = new Point(-3, 1);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(806, 689);
            tabControl1.TabIndex = 23;
            // 
            // conversion
            // 
            conversion.Controls.Add(button1);
            conversion.Controls.Add(button_histos);
            conversion.Controls.Add(label_error_2);
            conversion.Controls.Add(button_save);
            conversion.Controls.Add(label_no_of_cores);
            conversion.Controls.Add(pictureBox_after_grayscale);
            conversion.Controls.Add(pictureBox_before_grayscale);
            conversion.Controls.Add(label_pick_dll);
            conversion.Controls.Add(button_run);
            conversion.Controls.Add(label_blue);
            conversion.Controls.Add(label_green);
            conversion.Controls.Add(label_red);
            conversion.Controls.Add(numericUpDown_blue);
            conversion.Controls.Add(numericUpDown_green);
            conversion.Controls.Add(numericUpDown_red);
            conversion.Controls.Add(label_color_weights);
            conversion.Controls.Add(label_detected_cores);
            conversion.Controls.Add(label_no_of_threads);
            conversion.Controls.Add(numericUpDown_no_of_threads);
            conversion.Controls.Add(comboBox_select_dll);
            conversion.Controls.Add(button_pick_picture);
            conversion.Location = new Point(4, 29);
            conversion.Name = "conversion";
            conversion.Padding = new Padding(3);
            conversion.Size = new Size(798, 656);
            conversion.TabIndex = 0;
            conversion.Text = "conversion";
            conversion.UseVisualStyleBackColor = true;
            conversion.Click += tabPage1_Click;
            // 
            // button1
            // 
            button1.Location = new Point(26, 482);
            button1.Name = "button1";
            button1.Size = new Size(212, 29);
            button1.TabIndex = 46;
            button1.Text = "histo. from grayscale";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button_histos
            // 
            button_histos.Location = new Point(26, 447);
            button_histos.Name = "button_histos";
            button_histos.Size = new Size(212, 29);
            button_histos.TabIndex = 45;
            button_histos.Text = "histo. from color";
            button_histos.UseVisualStyleBackColor = true;
            button_histos.Click += button_histos_Click;
            // 
            // label_error_2
            // 
            label_error_2.AutoSize = true;
            label_error_2.Location = new Point(26, 519);
            label_error_2.Name = "label_error_2";
            label_error_2.Size = new Size(107, 20);
            label_error_2.TabIndex = 44;
            label_error_2.Text = "label for errors";
            // 
            // button_save
            // 
            button_save.Location = new Point(26, 412);
            button_save.Name = "button_save";
            button_save.Size = new Size(214, 29);
            button_save.TabIndex = 43;
            button_save.Text = "save";
            button_save.UseVisualStyleBackColor = true;
            button_save.Click += button_save_Click;
            // 
            // label_no_of_cores
            // 
            label_no_of_cores.AutoSize = true;
            label_no_of_cores.Location = new Point(222, 148);
            label_no_of_cores.Name = "label_no_of_cores";
            label_no_of_cores.Size = new Size(16, 20);
            label_no_of_cores.TabIndex = 42;
            label_no_of_cores.Text = "?";
            // 
            // pictureBox_after_grayscale
            // 
            pictureBox_after_grayscale.Location = new Point(314, 242);
            pictureBox_after_grayscale.Name = "pictureBox_after_grayscale";
            pictureBox_after_grayscale.Size = new Size(449, 318);
            pictureBox_after_grayscale.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox_after_grayscale.TabIndex = 41;
            pictureBox_after_grayscale.TabStop = false;
            // 
            // pictureBox_before_grayscale
            // 
            pictureBox_before_grayscale.Location = new Point(314, 38);
            pictureBox_before_grayscale.Name = "pictureBox_before_grayscale";
            pictureBox_before_grayscale.Size = new Size(449, 164);
            pictureBox_before_grayscale.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox_before_grayscale.TabIndex = 40;
            pictureBox_before_grayscale.TabStop = false;
            // 
            // label_pick_dll
            // 
            label_pick_dll.AutoSize = true;
            label_pick_dll.Location = new Point(20, 75);
            label_pick_dll.Name = "label_pick_dll";
            label_pick_dll.Size = new Size(57, 20);
            label_pick_dll.TabIndex = 39;
            label_pick_dll.Text = "pick dll";
            // 
            // button_run
            // 
            button_run.Location = new Point(26, 377);
            button_run.Name = "button_run";
            button_run.Size = new Size(214, 29);
            button_run.TabIndex = 38;
            button_run.Text = "run!";
            button_run.UseVisualStyleBackColor = true;
            button_run.Click += button_run_Click;
            // 
            // label_blue
            // 
            label_blue.AutoSize = true;
            label_blue.Location = new Point(20, 327);
            label_blue.Name = "label_blue";
            label_blue.Size = new Size(38, 20);
            label_blue.TabIndex = 37;
            label_blue.Text = "blue";
            // 
            // label_green
            // 
            label_green.AutoSize = true;
            label_green.Location = new Point(20, 286);
            label_green.Name = "label_green";
            label_green.Size = new Size(47, 20);
            label_green.TabIndex = 36;
            label_green.Text = "green";
            // 
            // label_red
            // 
            label_red.AutoSize = true;
            label_red.Location = new Point(20, 249);
            label_red.Name = "label_red";
            label_red.Size = new Size(31, 20);
            label_red.TabIndex = 35;
            label_red.Text = "red";
            // 
            // numericUpDown_blue
            // 
            numericUpDown_blue.DecimalPlaces = 4;
            numericUpDown_blue.Increment = new decimal(new int[] { 1, 0, 0, 262144 });
            numericUpDown_blue.Location = new Point(163, 325);
            numericUpDown_blue.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDown_blue.Name = "numericUpDown_blue";
            numericUpDown_blue.Size = new Size(77, 27);
            numericUpDown_blue.TabIndex = 34;
            numericUpDown_blue.Value = new decimal(new int[] { 722, 0, 0, 262144 });
            // 
            // numericUpDown_green
            // 
            numericUpDown_green.DecimalPlaces = 4;
            numericUpDown_green.Increment = new decimal(new int[] { 1, 0, 0, 262144 });
            numericUpDown_green.Location = new Point(163, 286);
            numericUpDown_green.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDown_green.Name = "numericUpDown_green";
            numericUpDown_green.Size = new Size(77, 27);
            numericUpDown_green.TabIndex = 33;
            numericUpDown_green.Value = new decimal(new int[] { 7152, 0, 0, 262144 });
            // 
            // numericUpDown_red
            // 
            numericUpDown_red.DecimalPlaces = 4;
            numericUpDown_red.Increment = new decimal(new int[] { 1, 0, 0, 262144 });
            numericUpDown_red.Location = new Point(163, 247);
            numericUpDown_red.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDown_red.Name = "numericUpDown_red";
            numericUpDown_red.RightToLeft = RightToLeft.No;
            numericUpDown_red.Size = new Size(77, 27);
            numericUpDown_red.TabIndex = 32;
            numericUpDown_red.Value = new decimal(new int[] { 2126, 0, 0, 262144 });
            // 
            // label_color_weights
            // 
            label_color_weights.AutoSize = true;
            label_color_weights.Location = new Point(20, 218);
            label_color_weights.Name = "label_color_weights";
            label_color_weights.Size = new Size(121, 20);
            label_color_weights.TabIndex = 31;
            label_color_weights.Text = "set color weights";
            // 
            // label_detected_cores
            // 
            label_detected_cores.AutoSize = true;
            label_detected_cores.Location = new Point(20, 148);
            label_detected_cores.Name = "label_detected_cores";
            label_detected_cores.Size = new Size(167, 20);
            label_detected_cores.TabIndex = 28;
            label_detected_cores.Text = "no. of detected cores = ";
            // 
            // label_no_of_threads
            // 
            label_no_of_threads.AutoSize = true;
            label_no_of_threads.Location = new Point(20, 114);
            label_no_of_threads.Name = "label_no_of_threads";
            label_no_of_threads.Size = new Size(100, 20);
            label_no_of_threads.TabIndex = 27;
            label_no_of_threads.Text = "no. of threads";
            // 
            // numericUpDown_no_of_threads
            // 
            numericUpDown_no_of_threads.Location = new Point(173, 109);
            numericUpDown_no_of_threads.Maximum = new decimal(new int[] { 64, 0, 0, 0 });
            numericUpDown_no_of_threads.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDown_no_of_threads.Name = "numericUpDown_no_of_threads";
            numericUpDown_no_of_threads.Size = new Size(67, 27);
            numericUpDown_no_of_threads.TabIndex = 26;
            numericUpDown_no_of_threads.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // comboBox_select_dll
            // 
            comboBox_select_dll.FormattingEnabled = true;
            comboBox_select_dll.Items.AddRange(new object[] { "cpp", "ass" });
            comboBox_select_dll.Location = new Point(126, 75);
            comboBox_select_dll.Name = "comboBox_select_dll";
            comboBox_select_dll.Size = new Size(114, 28);
            comboBox_select_dll.TabIndex = 25;
            comboBox_select_dll.Text = "cpp";
            // 
            // button_pick_picture
            // 
            button_pick_picture.Location = new Point(20, 38);
            button_pick_picture.Name = "button_pick_picture";
            button_pick_picture.Size = new Size(220, 29);
            button_pick_picture.TabIndex = 24;
            button_pick_picture.Text = "pick picture";
            button_pick_picture.UseVisualStyleBackColor = true;
            button_pick_picture.Click += button_pick_picture_Click;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(label_color_or_gs);
            tabPage2.Controls.Add(button_save_2);
            tabPage2.Controls.Add(label3);
            tabPage2.Controls.Add(label2);
            tabPage2.Controls.Add(label1);
            tabPage2.Controls.Add(pictureBox_chart_b);
            tabPage2.Controls.Add(pictureBox_chart_g);
            tabPage2.Controls.Add(pictureBox_chart_r);
            tabPage2.Location = new Point(4, 29);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(798, 656);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "histogrammes";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // button_save_2
            // 
            button_save_2.Location = new Point(93, 501);
            button_save_2.Name = "button_save_2";
            button_save_2.Size = new Size(266, 32);
            button_save_2.TabIndex = 6;
            button_save_2.Text = "save histogrammes from";
            button_save_2.UseVisualStyleBackColor = true;
            button_save_2.Click += button_save_2_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(17, 345);
            label3.Name = "label3";
            label3.Size = new Size(38, 20);
            label3.TabIndex = 5;
            label3.Text = "blue";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(17, 189);
            label2.Name = "label2";
            label2.Size = new Size(47, 20);
            label2.TabIndex = 4;
            label2.Text = "green";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(17, 33);
            label1.Name = "label1";
            label1.Size = new Size(31, 20);
            label1.TabIndex = 3;
            label1.Text = "red";
            // 
            // pictureBox_chart_b
            // 
            pictureBox_chart_b.Location = new Point(93, 345);
            pictureBox_chart_b.Name = "pictureBox_chart_b";
            pictureBox_chart_b.Size = new Size(600, 150);
            pictureBox_chart_b.TabIndex = 2;
            pictureBox_chart_b.TabStop = false;
            // 
            // pictureBox_chart_g
            // 
            pictureBox_chart_g.Location = new Point(93, 189);
            pictureBox_chart_g.Name = "pictureBox_chart_g";
            pictureBox_chart_g.Size = new Size(600, 150);
            pictureBox_chart_g.TabIndex = 1;
            pictureBox_chart_g.TabStop = false;
            // 
            // pictureBox_chart_r
            // 
            pictureBox_chart_r.Location = new Point(93, 33);
            pictureBox_chart_r.Name = "pictureBox_chart_r";
            pictureBox_chart_r.Size = new Size(600, 150);
            pictureBox_chart_r.TabIndex = 0;
            pictureBox_chart_r.TabStop = false;
            // 
            // label_color_or_gs
            // 
            label_color_or_gs.AutoSize = true;
            label_color_or_gs.Location = new Point(365, 501);
            label_color_or_gs.Name = "label_color_or_gs";
            label_color_or_gs.Size = new Size(104, 20);
            label_color_or_gs.TabIndex = 7;
            label_color_or_gs.Text = "color_or_grays";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 686);
            Controls.Add(tabControl1);
            Controls.Add(label_error);
            Name = "Form1";
            Text = "Grayscale filter project";
            Load += Form1_Load;
            tabControl1.ResumeLayout(false);
            conversion.ResumeLayout(false);
            conversion.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox_after_grayscale).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox_before_grayscale).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown_blue).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown_green).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown_red).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown_no_of_threads).EndInit();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox_chart_b).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox_chart_g).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox_chart_r).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label_error;
        private OpenFileDialog openFileDialog1;
        private SaveFileDialog saveFileDialog1;
        private TabControl tabControl1;
        private TabPage conversion;
        private TabPage tabPage2;
        private Button button_save;
        private Label label_no_of_cores;
        private PictureBox pictureBox_after_grayscale;
        private PictureBox pictureBox_before_grayscale;
        private Label label_pick_dll;
        private Button button_run;
        private Label label_blue;
        private Label label_green;
        private Label label_red;
        private NumericUpDown numericUpDown_blue;
        private NumericUpDown numericUpDown_green;
        private NumericUpDown numericUpDown_red;
        private Label label_color_weights;
        private Label label_detected_cores;
        private Label label_no_of_threads;
        private NumericUpDown numericUpDown_no_of_threads;
        private ComboBox comboBox_select_dll;
        private Button button_pick_picture;
        private Label label_error_2;
        private Button button_histos;
        private Label label3;
        private Label label2;
        private Label label1;
        private PictureBox pictureBox_chart_b;
        private PictureBox pictureBox_chart_g;
        private PictureBox pictureBox_chart_r;
        private Button button1;
        private Button button_save_2;
        private Label label_color_or_gs;
    }
}
