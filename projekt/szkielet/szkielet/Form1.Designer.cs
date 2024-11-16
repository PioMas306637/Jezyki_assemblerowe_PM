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
            numericUpDown1 = new NumericUpDown();
            label_no_of_threads = new Label();
            label_detected_cores = new Label();
            label_gen_histo = new Label();
            checkBox_gen_hist = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
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
            button_pick_picture.Size = new Size(94, 29);
            button_pick_picture.TabIndex = 1;
            button_pick_picture.Text = "pick picture";
            button_pick_picture.UseVisualStyleBackColor = true;
            // 
            // comboBox_select_dll
            // 
            comboBox_select_dll.FormattingEnabled = true;
            comboBox_select_dll.Items.AddRange(new object[] { "cpp", "ass" });
            comboBox_select_dll.Location = new Point(12, 67);
            comboBox_select_dll.Name = "comboBox_select_dll";
            comboBox_select_dll.Size = new Size(151, 28);
            comboBox_select_dll.TabIndex = 3;
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(137, 101);
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(67, 27);
            numericUpDown1.TabIndex = 4;
            // 
            // label_no_of_threads
            // 
            label_no_of_threads.AutoSize = true;
            label_no_of_threads.Location = new Point(12, 98);
            label_no_of_threads.Name = "label_no_of_threads";
            label_no_of_threads.Size = new Size(100, 20);
            label_no_of_threads.TabIndex = 5;
            label_no_of_threads.Text = "no. of threads";
            // 
            // label_detected_cores
            // 
            label_detected_cores.AutoSize = true;
            label_detected_cores.Location = new Point(12, 131);
            label_detected_cores.Name = "label_detected_cores";
            label_detected_cores.Size = new Size(167, 20);
            label_detected_cores.TabIndex = 6;
            label_detected_cores.Text = "no. of detected cores = ";
            // 
            // label_gen_histo
            // 
            label_gen_histo.AutoSize = true;
            label_gen_histo.Location = new Point(12, 161);
            label_gen_histo.Name = "label_gen_histo";
            label_gen_histo.Size = new Size(153, 20);
            label_gen_histo.TabIndex = 7;
            label_gen_histo.Text = "generate histogrames";
            // 
            // checkBox_gen_hist
            // 
            checkBox_gen_hist.AutoSize = true;
            checkBox_gen_hist.Location = new Point(186, 164);
            checkBox_gen_hist.Name = "checkBox_gen_hist";
            checkBox_gen_hist.Size = new Size(18, 17);
            checkBox_gen_hist.TabIndex = 8;
            checkBox_gen_hist.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(checkBox_gen_hist);
            Controls.Add(label_gen_histo);
            Controls.Add(label_detected_cores);
            Controls.Add(label_no_of_threads);
            Controls.Add(numericUpDown1);
            Controls.Add(comboBox_select_dll);
            Controls.Add(button_pick_picture);
            Controls.Add(label_grayscale);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label_grayscale;
        private Button button_pick_picture;
        private ComboBox comboBox_select_dll;
        private NumericUpDown numericUpDown1;
        private Label label_no_of_threads;
        private Label label_detected_cores;
        private Label label_gen_histo;
        private CheckBox checkBox_gen_hist;
    }
}
