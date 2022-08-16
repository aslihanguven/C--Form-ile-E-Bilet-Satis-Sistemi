namespace Pamukkale_E_Bilet_Satış_Sistemi
{
    partial class kisilerPanel
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
            this.components = new System.ComponentModel.Container();
            this.kadinPicture = new System.Windows.Forms.PictureBox();
            this.erkekPicture = new System.Windows.Forms.PictureBox();
            this.mouseCursorTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.kadinPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.erkekPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // kadinPicture
            // 
            this.kadinPicture.BackColor = System.Drawing.Color.Transparent;
            this.kadinPicture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.kadinPicture.Image = global::Pamukkale_E_Bilet_Satış_Sistemi.Properties.Resources.kadin;
            this.kadinPicture.Location = new System.Drawing.Point(26, 7);
            this.kadinPicture.Name = "kadinPicture";
            this.kadinPicture.Size = new System.Drawing.Size(58, 78);
            this.kadinPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.kadinPicture.TabIndex = 0;
            this.kadinPicture.TabStop = false;
            this.kadinPicture.Click += new System.EventHandler(this.kadinPicture_Click);
            this.kadinPicture.MouseLeave += new System.EventHandler(this.kadinPicture_MouseLeave);
            this.kadinPicture.MouseHover += new System.EventHandler(this.kadinPicture_MouseHover);
            // 
            // erkekPicture
            // 
            this.erkekPicture.BackColor = System.Drawing.Color.Transparent;
            this.erkekPicture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.erkekPicture.Image = global::Pamukkale_E_Bilet_Satış_Sistemi.Properties.Resources.erkek;
            this.erkekPicture.Location = new System.Drawing.Point(97, 7);
            this.erkekPicture.Name = "erkekPicture";
            this.erkekPicture.Size = new System.Drawing.Size(58, 78);
            this.erkekPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.erkekPicture.TabIndex = 0;
            this.erkekPicture.TabStop = false;
            this.erkekPicture.Click += new System.EventHandler(this.erkekPicture_Click);
            this.erkekPicture.MouseLeave += new System.EventHandler(this.erkekPicture_MouseLeave);
            this.erkekPicture.MouseHover += new System.EventHandler(this.erkekPicture_MouseHover);
            // 
            // mouseCursorTimer
            // 
            this.mouseCursorTimer.Enabled = true;
            this.mouseCursorTimer.Interval = 10;
            this.mouseCursorTimer.Tick += new System.EventHandler(this.mouseCursorTimer_Tick);
            // 
            // kisilerPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.LavenderBlush;
            this.BackgroundImage = global::Pamukkale_E_Bilet_Satış_Sistemi.Properties.Resources.kisiler;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(181, 129);
            this.Controls.Add(this.erkekPicture);
            this.Controls.Add(this.kadinPicture);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "kisilerPanel";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "kisilerPanel";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.LavenderBlush;
            this.Load += new System.EventHandler(this.kisilerPanel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kadinPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.erkekPicture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox kadinPicture;
        private System.Windows.Forms.PictureBox erkekPicture;
        private System.Windows.Forms.Timer mouseCursorTimer;

    }
}