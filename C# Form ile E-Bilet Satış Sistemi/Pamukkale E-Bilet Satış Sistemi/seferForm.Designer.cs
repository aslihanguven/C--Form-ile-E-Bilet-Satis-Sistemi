namespace Pamukkale_E_Bilet_Satış_Sistemi
{
    partial class seferForm
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
            this.seferPanelListesi = new System.Windows.Forms.FlowLayoutPanel();
            this.mouseCursorTimer = new System.Windows.Forms.Timer(this.components);
            this.mouseHoverTimer = new System.Windows.Forms.Timer(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.ustPanel = new System.Windows.Forms.Panel();
            this.gunlerPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.kalkisVarisLB = new System.Windows.Forms.Label();
            this.ustPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // seferPanelListesi
            // 
            this.seferPanelListesi.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.seferPanelListesi.BackColor = System.Drawing.SystemColors.Control;
            this.seferPanelListesi.Location = new System.Drawing.Point(2, 136);
            this.seferPanelListesi.Margin = new System.Windows.Forms.Padding(1, 0, 0, 0);
            this.seferPanelListesi.Name = "seferPanelListesi";
            this.seferPanelListesi.Size = new System.Drawing.Size(982, 360);
            this.seferPanelListesi.TabIndex = 1;
            // 
            // mouseCursorTimer
            // 
            this.mouseCursorTimer.Enabled = true;
            this.mouseCursorTimer.Interval = 10;
            this.mouseCursorTimer.Tag = "mausenin hangisinin üzerine geldiğine bakıyr";
            this.mouseCursorTimer.Tick += new System.EventHandler(this.mouseCursorTimer_Tick);
            // 
            // mouseHoverTimer
            // 
            this.mouseHoverTimer.Tag = "farenin hangisine tıkladığına bakıyor";
            this.mouseHoverTimer.Tick += new System.EventHandler(this.mouseHoverTimer_Tick);
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 5000;
            this.toolTip1.InitialDelay = 100;
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ReshowDelay = 100;
            this.toolTip1.Tag = "gifi yapmamı sağladı";
            // 
            // ustPanel
            // 
            this.ustPanel.BackgroundImage = global::Pamukkale_E_Bilet_Satış_Sistemi.Properties.Resources.sefer_üst;
            this.ustPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ustPanel.Controls.Add(this.gunlerPanel);
            this.ustPanel.Controls.Add(this.kalkisVarisLB);
            this.ustPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.ustPanel.Location = new System.Drawing.Point(0, 0);
            this.ustPanel.Name = "ustPanel";
            this.ustPanel.Size = new System.Drawing.Size(986, 140);
            this.ustPanel.TabIndex = 0;
            // 
            // gunlerPanel
            // 
            this.gunlerPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gunlerPanel.BackColor = System.Drawing.SystemColors.Control;
            this.gunlerPanel.Location = new System.Drawing.Point(15, 79);
            this.gunlerPanel.Margin = new System.Windows.Forms.Padding(0);
            this.gunlerPanel.Name = "gunlerPanel";
            this.gunlerPanel.Size = new System.Drawing.Size(955, 57);
            this.gunlerPanel.TabIndex = 1;
            // 
            // kalkisVarisLB
            // 
            this.kalkisVarisLB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.kalkisVarisLB.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.kalkisVarisLB.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.kalkisVarisLB.Location = new System.Drawing.Point(130, 9);
            this.kalkisVarisLB.Name = "kalkisVarisLB";
            this.kalkisVarisLB.Size = new System.Drawing.Size(636, 42);
            this.kalkisVarisLB.TabIndex = 2;
            this.kalkisVarisLB.Text = "Kalkış - Varış";
            this.kalkisVarisLB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
          
            // 
            // seferForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(986, 505);
            this.Controls.Add(this.seferPanelListesi);
            this.Controls.Add(this.ustPanel);
            this.MaximizeBox = false;
            this.Name = "seferForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "seferForm";
            this.Load += new System.EventHandler(this.seferForm_Load);
            this.ustPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel seferPanelListesi;
        private System.Windows.Forms.Timer mouseCursorTimer;
        private System.Windows.Forms.Timer mouseHoverTimer;
        private System.Windows.Forms.Label kalkisVarisLB;
        private System.Windows.Forms.FlowLayoutPanel gunlerPanel;
        private System.Windows.Forms.Panel ustPanel;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}