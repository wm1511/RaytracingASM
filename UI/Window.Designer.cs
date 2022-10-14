namespace UI
{
    partial class Window
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
            this.libraryBox = new System.Windows.Forms.GroupBox();
            this.libCs = new System.Windows.Forms.RadioButton();
            this.libAsm = new System.Windows.Forms.RadioButton();
            this.sizeLabel = new System.Windows.Forms.Label();
            this.size = new System.Windows.Forms.NumericUpDown();
            this.timeList = new System.Windows.Forms.ListBox();
            this.thread = new System.Windows.Forms.TrackBar();
            this.spp = new System.Windows.Forms.TrackBar();
            this.threadLabel = new System.Windows.Forms.Label();
            this.sppLabel = new System.Windows.Forms.Label();
            this.configBox = new System.Windows.Forms.GroupBox();
            this.maxDepthNumber = new System.Windows.Forms.Label();
            this.maxDepthLabel = new System.Windows.Forms.Label();
            this.maxDepth = new System.Windows.Forms.TrackBar();
            this.threadNumber = new System.Windows.Forms.Label();
            this.sppNumber = new System.Windows.Forms.Label();
            this.timeLabel = new System.Windows.Forms.Label();
            this.renderButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.image = new System.Windows.Forms.PictureBox();
            this.libraryBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.size)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.thread)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spp)).BeginInit();
            this.configBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maxDepth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.image)).BeginInit();
            this.SuspendLayout();
            // 
            // libraryBox
            // 
            this.libraryBox.Controls.Add(this.libCs);
            this.libraryBox.Controls.Add(this.libAsm);
            this.libraryBox.Location = new System.Drawing.Point(12, 12);
            this.libraryBox.Name = "libraryBox";
            this.libraryBox.Size = new System.Drawing.Size(280, 90);
            this.libraryBox.TabIndex = 0;
            this.libraryBox.TabStop = false;
            this.libraryBox.Text = "Typ biblioteki";
            // 
            // libCs
            // 
            this.libCs.AutoSize = true;
            this.libCs.Checked = true;
            this.libCs.Location = new System.Drawing.Point(6, 56);
            this.libCs.Name = "libCs";
            this.libCs.Size = new System.Drawing.Size(48, 24);
            this.libCs.TabIndex = 1;
            this.libCs.TabStop = true;
            this.libCs.Text = "C#";
            this.libCs.UseVisualStyleBackColor = true;
            // 
            // libAsm
            // 
            this.libAsm.AutoSize = true;
            this.libAsm.Location = new System.Drawing.Point(6, 26);
            this.libAsm.Name = "libAsm";
            this.libAsm.Size = new System.Drawing.Size(61, 24);
            this.libAsm.TabIndex = 0;
            this.libAsm.Text = "ASM";
            this.libAsm.UseVisualStyleBackColor = true;
            // 
            // sizeLabel
            // 
            this.sizeLabel.AutoSize = true;
            this.sizeLabel.Location = new System.Drawing.Point(12, 109);
            this.sizeLabel.Name = "sizeLabel";
            this.sizeLabel.Size = new System.Drawing.Size(94, 20);
            this.sizeLabel.TabIndex = 2;
            this.sizeLabel.Text = "Rozmiar (px)";
            // 
            // size
            // 
            this.size.Increment = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.size.Location = new System.Drawing.Point(12, 132);
            this.size.Maximum = new decimal(new int[] {
            10240,
            0,
            0,
            0});
            this.size.Minimum = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.size.Name = "size";
            this.size.Size = new System.Drawing.Size(280, 27);
            this.size.TabIndex = 0;
            this.size.Value = new decimal(new int[] {
            640,
            0,
            0,
            0});
            // 
            // timeList
            // 
            this.timeList.FormattingEnabled = true;
            this.timeList.ItemHeight = 20;
            this.timeList.Location = new System.Drawing.Point(12, 197);
            this.timeList.Name = "timeList";
            this.timeList.Size = new System.Drawing.Size(280, 464);
            this.timeList.TabIndex = 2;
            // 
            // thread
            // 
            this.thread.Location = new System.Drawing.Point(6, 66);
            this.thread.Maximum = 64;
            this.thread.Minimum = 1;
            this.thread.Name = "thread";
            this.thread.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.thread.Size = new System.Drawing.Size(56, 427);
            this.thread.TabIndex = 3;
            this.thread.Value = 1;
            this.thread.Scroll += new System.EventHandler(this.threadCount_Scroll);
            // 
            // spp
            // 
            this.spp.Location = new System.Drawing.Point(218, 66);
            this.spp.Maximum = 100;
            this.spp.Minimum = 1;
            this.spp.Name = "spp";
            this.spp.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.spp.Size = new System.Drawing.Size(56, 427);
            this.spp.SmallChange = 3;
            this.spp.TabIndex = 4;
            this.spp.Value = 8;
            this.spp.Scroll += new System.EventHandler(this.sppCount_Scroll);
            // 
            // threadLabel
            // 
            this.threadLabel.AutoSize = true;
            this.threadLabel.Location = new System.Drawing.Point(6, 33);
            this.threadLabel.Name = "threadLabel";
            this.threadLabel.Size = new System.Drawing.Size(46, 20);
            this.threadLabel.TabIndex = 5;
            this.threadLabel.Text = "Wątki";
            // 
            // sppLabel
            // 
            this.sppLabel.Location = new System.Drawing.Point(194, 23);
            this.sppLabel.Name = "sppLabel";
            this.sppLabel.Size = new System.Drawing.Size(80, 40);
            this.sppLabel.TabIndex = 6;
            this.sppLabel.Text = "Próbki na pixel";
            this.sppLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // configBox
            // 
            this.configBox.Controls.Add(this.maxDepthNumber);
            this.configBox.Controls.Add(this.maxDepthLabel);
            this.configBox.Controls.Add(this.maxDepth);
            this.configBox.Controls.Add(this.threadNumber);
            this.configBox.Controls.Add(this.sppNumber);
            this.configBox.Controls.Add(this.spp);
            this.configBox.Controls.Add(this.sppLabel);
            this.configBox.Controls.Add(this.thread);
            this.configBox.Controls.Add(this.threadLabel);
            this.configBox.Location = new System.Drawing.Point(970, 12);
            this.configBox.Name = "configBox";
            this.configBox.Size = new System.Drawing.Size(280, 520);
            this.configBox.TabIndex = 7;
            this.configBox.TabStop = false;
            this.configBox.Text = "Konfiguracja";
            // 
            // maxDepthNumber
            // 
            this.maxDepthNumber.AutoSize = true;
            this.maxDepthNumber.Location = new System.Drawing.Point(112, 496);
            this.maxDepthNumber.Name = "maxDepthNumber";
            this.maxDepthNumber.Size = new System.Drawing.Size(25, 20);
            this.maxDepthNumber.TabIndex = 13;
            this.maxDepthNumber.Text = "32";
            // 
            // maxDepthLabel
            // 
            this.maxDepthLabel.Location = new System.Drawing.Point(88, 18);
            this.maxDepthLabel.Name = "maxDepthLabel";
            this.maxDepthLabel.Size = new System.Drawing.Size(100, 41);
            this.maxDepthLabel.TabIndex = 12;
            this.maxDepthLabel.Text = "Maksymalna ilość odbić";
            this.maxDepthLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // maxDepth
            // 
            this.maxDepth.LargeChange = 4;
            this.maxDepth.Location = new System.Drawing.Point(112, 66);
            this.maxDepth.Maximum = 64;
            this.maxDepth.Minimum = 1;
            this.maxDepth.Name = "maxDepth";
            this.maxDepth.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.maxDepth.Size = new System.Drawing.Size(56, 427);
            this.maxDepth.TabIndex = 11;
            this.maxDepth.Value = 32;
            this.maxDepth.Scroll += new System.EventHandler(this.depth_Scroll);
            // 
            // threadNumber
            // 
            this.threadNumber.AutoSize = true;
            this.threadNumber.Location = new System.Drawing.Point(6, 496);
            this.threadNumber.Name = "threadNumber";
            this.threadNumber.Size = new System.Drawing.Size(17, 20);
            this.threadNumber.TabIndex = 10;
            this.threadNumber.Text = "1";
            // 
            // sppNumber
            // 
            this.sppNumber.AutoSize = true;
            this.sppNumber.Location = new System.Drawing.Point(218, 496);
            this.sppNumber.Name = "sppNumber";
            this.sppNumber.Size = new System.Drawing.Size(17, 20);
            this.sppNumber.TabIndex = 8;
            this.sppNumber.Text = "8";
            // 
            // timeLabel
            // 
            this.timeLabel.AutoSize = true;
            this.timeLabel.Location = new System.Drawing.Point(12, 174);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(113, 20);
            this.timeLabel.TabIndex = 8;
            this.timeLabel.Text = "Czas wykonania";
            // 
            // renderButton
            // 
            this.renderButton.Location = new System.Drawing.Point(970, 538);
            this.renderButton.Name = "renderButton";
            this.renderButton.Size = new System.Drawing.Size(280, 60);
            this.renderButton.TabIndex = 9;
            this.renderButton.Text = "Renderuj";
            this.renderButton.UseVisualStyleBackColor = true;
            this.renderButton.Click += new System.EventHandler(this.renderButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(970, 604);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(280, 60);
            this.saveButton.TabIndex = 10;
            this.saveButton.Text = "Zapisz do pliku";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "bmp";
            this.saveFileDialog.Filter = "24-bit bitmap (*.bmp)|*.bmp";
            this.saveFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog_FileOk);
            // 
            // image
            // 
            this.image.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.image.Location = new System.Drawing.Point(311, 21);
            this.image.Name = "image";
            this.image.Size = new System.Drawing.Size(640, 640);
            this.image.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.image.TabIndex = 11;
            this.image.TabStop = false;
            // 
            // Window
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1262, 673);
            this.Controls.Add(this.image);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.size);
            this.Controls.Add(this.sizeLabel);
            this.Controls.Add(this.renderButton);
            this.Controls.Add(this.timeLabel);
            this.Controls.Add(this.configBox);
            this.Controls.Add(this.timeList);
            this.Controls.Add(this.libraryBox);
            this.Name = "Window";
            this.Text = "RayTracer";
            this.libraryBox.ResumeLayout(false);
            this.libraryBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.size)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.thread)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spp)).EndInit();
            this.configBox.ResumeLayout(false);
            this.configBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maxDepth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.image)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GroupBox libraryBox;
        private RadioButton libCs;
        private RadioButton libAsm;
        private Label sizeLabel;
        private NumericUpDown size;
        private ListBox timeList;
        private TrackBar thread;
        private TrackBar spp;
        private Label threadLabel;
        private Label sppLabel;
        private GroupBox configBox;
        private Label sppNumber;
        private Label timeLabel;
        private Label threadNumber;
        private Button renderButton;
        private Button saveButton;
        private SaveFileDialog saveFileDialog;
        private PictureBox image;
        private TrackBar maxDepth;
        private Label maxDepthLabel;
        private Label maxDepthNumber;
    }
}