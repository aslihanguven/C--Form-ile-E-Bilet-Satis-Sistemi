using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Pamukkale_E_Bilet_Satış_Sistemi
{
    public partial class kisilerPanel : Form
    {

        public kisilerPanel()
        {
            InitializeComponent();
        }

        aracForm2 _aracForm2 = (Application.OpenForms["aracForm2"] as aracForm2);

        private void kisilerPanel_Load(object sender, EventArgs e)
        {
            this.Location = new Point(Cursor.Position.X - 90, Cursor.Position.Y - 150);
            Cursor.Position = new Point(Cursor.Position.X, Cursor.Position.Y - 30);
        }

        private void mouseCursorTimer_Tick(object sender, EventArgs e)
        {
            if (this.ClientRectangle.Contains(this.PointToClient(MousePosition))) { return; }

            _aracForm2.koltukDurumDegistir(null);
                        
            this.Dispose();
        }

        private void kadinPicture_Click(object sender, EventArgs e)
        {
            try
            {
                _aracForm2.koltukDurumDegistir("kadin");
            }
            catch (Exception) { }

            this.Dispose();
        }

        private void erkekPicture_Click(object sender, EventArgs e)
        {
            try
            {
                _aracForm2.koltukDurumDegistir("erkek");
            }
            catch (Exception) { }

            this.Dispose();
        }

        private void kadinPicture_MouseHover(object sender, EventArgs e)
        {
            (sender as PictureBox).Image = Properties.Resources.kadin2;
        }

        private void erkekPicture_MouseHover(object sender, EventArgs e)
        {
            (sender as PictureBox).Image = Properties.Resources.erkek2;
        }

        private void kadinPicture_MouseLeave(object sender, EventArgs e)
        {
            (sender as PictureBox).Image = Properties.Resources.kadin;
        }

        private void erkekPicture_MouseLeave(object sender, EventArgs e)
        {
            (sender as PictureBox).Image = Properties.Resources.erkek;
        }

    }
}
