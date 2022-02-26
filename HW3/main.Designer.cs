namespace HW3
{
    partial class main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.load_image = new System.Windows.Forms.Button();
            this.smoothed = new System.Windows.Forms.Button();
            this.gradient = new System.Windows.Forms.Button();
            this.edge_detection = new System.Windows.Forms.Button();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // load_image
            // 
            this.load_image.Location = new System.Drawing.Point(12, 12);
            this.load_image.Name = "load_image";
            this.load_image.Size = new System.Drawing.Size(83, 23);
            this.load_image.TabIndex = 0;
            this.load_image.Text = "Load Image";
            this.load_image.UseVisualStyleBackColor = true;
            this.load_image.Click += new System.EventHandler(this.load_image_Click);
            // 
            // smoothed
            // 
            this.smoothed.Location = new System.Drawing.Point(12, 41);
            this.smoothed.Name = "smoothed";
            this.smoothed.Size = new System.Drawing.Size(83, 41);
            this.smoothed.TabIndex = 1;
            this.smoothed.Text = "Show Smoothed";
            this.smoothed.UseVisualStyleBackColor = true;
            this.smoothed.Click += new System.EventHandler(this.smoothed_Click);
            // 
            // gradient
            // 
            this.gradient.Location = new System.Drawing.Point(12, 88);
            this.gradient.Name = "gradient";
            this.gradient.Size = new System.Drawing.Size(83, 40);
            this.gradient.TabIndex = 2;
            this.gradient.Text = "Show Gradient";
            this.gradient.UseVisualStyleBackColor = true;
            this.gradient.Click += new System.EventHandler(this.gradient_Click);
            // 
            // edge_detection
            // 
            this.edge_detection.Location = new System.Drawing.Point(12, 134);
            this.edge_detection.Name = "edge_detection";
            this.edge_detection.Size = new System.Drawing.Size(83, 42);
            this.edge_detection.TabIndex = 3;
            this.edge_detection.Text = "Edge Detection";
            this.edge_detection.UseVisualStyleBackColor = true;
            this.edge_detection.Click += new System.EventHandler(this.edge_detection_Click);
            // 
            // pictureBox
            // 
            this.pictureBox.Location = new System.Drawing.Point(101, 12);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(648, 497);
            this.pictureBox.TabIndex = 4;
            this.pictureBox.TabStop = false;
            // 
            // main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(761, 521);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.edge_detection);
            this.Controls.Add(this.gradient);
            this.Controls.Add(this.smoothed);
            this.Controls.Add(this.load_image);
            this.Name = "main";
            this.Text = "CS 469 - Garett - Perform Canny Edge Detection on selected image";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Button load_image;
        private Button smoothed;
        private Button gradient;
        private Button edge_detection;
        private PictureBox pictureBox;
    }
}