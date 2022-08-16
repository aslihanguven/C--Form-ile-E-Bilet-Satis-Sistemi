namespace Pamukkale_E_Bilet_Satış_Sistemi
{
    partial class aracForm2
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
            this.aracKoltukDizilimi = new System.Windows.Forms.FlowLayoutPanel();
            this.aracPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.aracPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // aracKoltukDizilimi
            // 
            this.aracKoltukDizilimi.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.aracKoltukDizilimi.BackColor = System.Drawing.Color.Transparent;
            this.aracKoltukDizilimi.FlowDirection = System.Windows.Forms.FlowDirection.BottomUp;
            this.aracKoltukDizilimi.Location = new System.Drawing.Point(153, 94);
            this.aracKoltukDizilimi.Name = "aracKoltukDizilimi";
            this.aracKoltukDizilimi.Size = new System.Drawing.Size(685, 212);
            this.aracKoltukDizilimi.TabIndex = 2;
            // 
            // aracPictureBox
            // 
            this.aracPictureBox.BackgroundImage = global::Pamukkale_E_Bilet_Satış_Sistemi.Properties.Resources.koltuk_dizilimi;
            this.aracPictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.aracPictureBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.aracPictureBox.Location = new System.Drawing.Point(0, 0);
            this.aracPictureBox.Name = "aracPictureBox";
            this.aracPictureBox.Size = new System.Drawing.Size(915, 360);
            this.aracPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.aracPictureBox.TabIndex = 0;
            this.aracPictureBox.TabStop = false;
            this.aracPictureBox.Click += new System.EventHandler(this.aracPictureBox_Click);
            // 
            // aracForm2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(915, 411);
            this.Controls.Add(this.aracKoltukDizilimi);
            this.Controls.Add(this.aracPictureBox);
            this.MaximizeBox = false;
            this.Name = "aracForm2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "aracForm2";
            this.Load += new System.EventHandler(this.aracForm2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.aracPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox aracPictureBox;
        public System.Windows.Forms.FlowLayoutPanel aracKoltukDizilimi;

    }
}