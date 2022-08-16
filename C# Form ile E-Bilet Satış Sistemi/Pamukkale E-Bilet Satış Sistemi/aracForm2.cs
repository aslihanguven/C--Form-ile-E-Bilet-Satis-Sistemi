using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Pamukkale_E_Bilet_Satış_Sistemi
{
    public partial class aracForm2 : Form
    {
        public aracForm2()
        {
            InitializeComponent();
        }

        string aracAdi;
        string dizilim;
        int koltukSayisi;
        int dizilimOrani;
        int solDizilimOrani;
        int sagDizilimOrani;
        int satirAdeti;
        int sutunAdeti = 13;
        int icBosluk;
        int koltukGenisligi;
        int koltukYuksekligi;
        int koltukNumarasi;


        string seferId = "";
        string kalkisId = "";
        string varisId = "";
        string seferFiyat1 = "";
        string seferFiyat2 = "";
        MatchCollection seferKoltuklari;


        private void aracForm2_Load(object sender, EventArgs e)
        {
            var frm =(Application.OpenForms["seferForm"] as seferForm);
            seferId = frm.seferId;
            kalkisId = frm.kalkisId;
            varisId = frm.varisId;
            seferFiyat1 = frm.seferFiyat1;
            seferFiyat2 = "undefined";
            aracAdi = frm.seferAracAdi;

            string seferKoltukUrl = String.Format("https://www.pamukkale.com.tr/tek-sefer"+(aracAdi.Contains("Anadolu")?"-anadolu":"")+".php?SEFERID={0}&KALKIS={1}&VARIS={2}&FIYAT={3}&FIYAT2={4}", seferId, kalkisId, varisId, seferFiyat1, seferFiyat2);

           

            dizilim = "1+2";

            aracKoltukDizilimi.Size = new Size(685, 212);

            int kapiBoslugu = koltukBilgileriniAl(seferKoltukUrl);
            koltuklariDiz(dizilim, kapiBoslugu);

            koltukBilgileriniDiz();

        }

        public Image rotateImage(Image image, string deg)
        {

            RotateFlipType rfType = RotateFlipType.RotateNoneFlipNone;

            if (deg == "0")
                rfType = RotateFlipType.RotateNoneFlipNone;
            if (deg == "90")
                rfType = RotateFlipType.Rotate90FlipNone;
            if (deg == "180")
                rfType = RotateFlipType.Rotate180FlipNone;
            if (deg == "270")
                rfType = RotateFlipType.Rotate270FlipNone;

            image.RotateFlip(rfType);
            return image;
        }

        private void yokKoltuk(FlowLayoutPanel arac)
        {
            Button koltukBT = new Button();
            koltukBT.BackColor = SystemColors.Control;
            koltukBT.Enabled = false;
            koltukBT.FlatStyle = FlatStyle.Flat;
            koltukBT.Tag = "Yok";
            koltukBT.FlatAppearance.BorderSize = 0;
            koltukBT.Size = new Size(koltukGenisligi, koltukYuksekligi);
            koltukBT.Margin = new Padding(icBosluk); //sol, üst, sağ, alt
            arac.Controls.Add(koltukBT);
        }

        private void koridorKoltuk(FlowLayoutPanel arac, int kolNum)
        {
            Button koltukBT = new Button();
            koltukBT.BackColor = SystemColors.Control;
            koltukBT.Enabled = false;
            koltukBT.FlatStyle = FlatStyle.Flat;
            //koltukBT.Text = kolNum.ToString().PadLeft(2, '0');
            koltukBT.Tag = "Koridor";
            koltukBT.Name = "Koltuk" + kolNum.ToString().PadLeft(2, '0');
            koltukBT.FlatAppearance.BorderSize = 0;
            koltukBT.Size = new Size(koltukGenisligi, koltukYuksekligi);
            koltukBT.Margin = new Padding(icBosluk); //sol, üst, sağ, alt
            arac.Controls.Add(koltukBT);
        }

        private void bosKoltuk(FlowLayoutPanel arac, int kolNum)
        {
            Button koltukBT = new Button();
            koltukBT.BackColor = Color.Transparent;
            koltukBT.ForeColor = Color.FromArgb(85, 85, 85);
            koltukBT.Size = new Size(koltukGenisligi, koltukYuksekligi);
            koltukBT.Margin = new Padding(icBosluk); //sol, üst, sağ, alt
            koltukBT.Text = kolNum.ToString().PadLeft(2, '0');
            koltukBT.Tag = "Boş";
            koltukBT.FlatStyle = FlatStyle.Flat;
            koltukBT.FlatAppearance.MouseOverBackColor = SystemColors.Control;
            koltukBT.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            koltukBT.FlatAppearance.BorderSize = 0;
            koltukBT.FlatAppearance.BorderColor = SystemColors.Control; koltukBT.TabStop = false;
            koltukBT.Name = "Koltuk" + kolNum.ToString().PadLeft(2, '0');
            koltukBT.Image = rotateImage(Properties.Resources.koltuk, "270");
            koltukBT.Font = new Font("Arial", 15, FontStyle.Regular);
            koltukBT.MouseClick += new MouseEventHandler(koltukBT_MouseClick);
            koltukBT.MouseHover += new EventHandler(koltukBT_MouseHover);
            koltukBT.MouseLeave += new EventHandler(koltukBT_MouseLeave);
            arac.Controls.Add(koltukBT);
        }

        private void koltuklariDiz(string dizilim, int kapiBoslugu)
        {

            aracKoltukDizilimi.Controls.Clear();

            //------------------------------------------ 
            solDizilimOrani = int.Parse(dizilim.Split('+')[0]);
            sagDizilimOrani = int.Parse(dizilim.Split('+')[1]);
            dizilimOrani = solDizilimOrani + sagDizilimOrani;
            satirAdeti = dizilimOrani + 1;
            koltukSayisi = satirAdeti * sutunAdeti;
            icBosluk = 4;
            int koltukDegeri = (int)((aracKoltukDizilimi.Height - 4) / satirAdeti) - (icBosluk * 2);
            koltukGenisligi = koltukDegeri;
            koltukYuksekligi = koltukDegeri;

            //------------------------------------------

            koltukNumarasi = 0;


            for (int sut = 1; sut <= sutunAdeti; sut++)
            {
                for (int sat = 1; sat <= satirAdeti; sat++)
                {
                    if (sut == kapiBoslugu && sat == solDizilimOrani + 1)
                    {
                        koltukNumarasi++;
                        koridorKoltuk(aracKoltukDizilimi, koltukNumarasi);
                    }
                    else if (sut == kapiBoslugu && sat > solDizilimOrani + 1)
                    {
                        yokKoltuk(aracKoltukDizilimi);
                    }
                    else if (kapiBoslugu == 7 && sut == kapiBoslugu + 1 && sat == solDizilimOrani)
                    {
                        koltukNumarasi += 3;
                        bosKoltuk(aracKoltukDizilimi, koltukNumarasi);
                    }
                    else if (kapiBoslugu == 7 && sut == kapiBoslugu + 1 && sat == solDizilimOrani + 1)
                    {
                        koltukNumarasi++;
                        koridorKoltuk(aracKoltukDizilimi, koltukNumarasi);
                    }
                    else if (kapiBoslugu == 7 && sut == kapiBoslugu + 1 && sat == solDizilimOrani + sagDizilimOrani)
                    {
                        koltukNumarasi -= 3;
                        bosKoltuk(aracKoltukDizilimi, koltukNumarasi);
                    }
                    else if (kapiBoslugu == 7 && sut == kapiBoslugu + 1 && sat == solDizilimOrani + sagDizilimOrani + 1)
                    {
                        koltukNumarasi++;
                        bosKoltuk(aracKoltukDizilimi, koltukNumarasi);
                        koltukNumarasi += 2;
                    }
                    else if (sut != kapiBoslugu && sat == solDizilimOrani + 1)
                    {
                        koltukNumarasi++;
                        koridorKoltuk(aracKoltukDizilimi, koltukNumarasi);   
                    }
                    else
                    {
                        koltukNumarasi++;
                        bosKoltuk(aracKoltukDizilimi, koltukNumarasi);
                    }
                }
            }
        }
       
        private int koltukBilgileriniAl(string seferKoltukUrl)
        {

            string seferKoltukHtml = new WebClient().DownloadString(seferKoltukUrl);
            
            seferKoltukHtml = seferKoltukHtml.Replace("'", "\"");
            seferKoltukHtml = Encoding.UTF8.GetString(Encoding.Default.GetBytes(seferKoltukHtml));

            string seferKoltukPattern = "";




            if (aracAdi.Contains("Anadolu"))
            {

                seferKoltukPattern = "<a href=\"(?:[^\"]*?)\" data-guz=\"([^\"].*?)\" data-firma=\"(?:[^\"].*?)\" data-koltuk-fiyat=\"([^\"].*?)\" data-sefer-id=\"(?:[^\"].*?)\" data-sabit=([^ ].*?) class=\"(?:[^\"].*?)\" data-uyari=\"(?:[^\"].*?)\" data-id=\"([^\"].*?)\">([^<].*?)</a>";
            }
            else
            {
                seferKoltukPattern = "<a href=\"(?:[^\"]*?)\" data-guz=\"([^\"].*?)\" data-koltuk-fiyat=\"([^\"].*?)\" data-sefer-id=\"(?:[^\"].*?)\" title=\"([^\"].*?)\" data-sabit=(?:[^ ].*?) class=\"(?:[^\"].*?)\" data-uyari=\"(?:[^\"].*?)\" data-id=\"([^\"].*?)\">([^<].*?)</a>";
            }
            
            seferKoltuklari = Regex.Matches(seferKoltukHtml, seferKoltukPattern);

            if (seferKoltuklari.Count < 37)
            {
                MessageBox.Show("Sefere ait koltuk bilgileri okunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Dispose();
            }

            for (int klt = 0; klt < seferKoltuklari.Count - 1; klt++)
            {
                if(seferKoltuklari[klt].Groups[4].Value == "25" && seferKoltuklari[klt+1].Groups[4].Value == "29") {
                    return 7;    
                }else if(seferKoltuklari[klt].Groups[4].Value == "25" && seferKoltuklari[klt+1].Groups[4].Value == "27") {
                    return 8;    
                }
            }

            return 0;
           
        }

         private void koltukBilgileriniDiz()        {              

            for (int klt = 0; klt < seferKoltuklari.Count - 1; klt++)
            {
                Image img = Properties.Resources.koltuk;

                if (seferKoltuklari[klt].Groups[3].Value == (aracAdi.Contains("Anadolu") ? "Erkek" : "Dolu Koltuk Erkek"))
                {
                    img = rotateImage(Properties.Resources.koltuk_erkek_dolu, "270");
                }
                else if (seferKoltuklari[klt].Groups[3].Value == (aracAdi.Contains("Anadolu")?"Bayan":"Dolu Koltuk Kadın"))
                {
                    img = rotateImage(Properties.Resources.koltuk_kadin_dolu, "270");
                }
                else if (seferKoltuklari[klt].Groups[3].Value == (aracAdi.Contains("Anadolu") ? "Bos" : "Bos Koltuk"))
                {
                    img = rotateImage(Properties.Resources.koltuk, "270");
                }

                (aracKoltukDizilimi.Controls["Koltuk" + seferKoltuklari[klt].Groups[4].Value] as Button).Image = img;

            }
        }

        private void koltukBT_MouseClick(object sender, MouseEventArgs e)
        {
            selectedButton = sender as Button;

            if (e.Button != System.Windows.Forms.MouseButtons.Left) { return; }

            if (selectedButton.Tag == null || selectedButton.Tag.ToString() == "overed")
            {
                koltukDurumDegistir("selected");
            }
            else
            {
                koltukDurumDegistir(null);
            }

            kisilerPanel frm = new kisilerPanel();
            frm.Location = MousePosition;
            frm.Show();
        }

        private void koltukBT_MouseHover(object sender, EventArgs e)
        {

            koltukDurumDegistir("overed");
        }

        private void koltukBT_MouseLeave(object sender, EventArgs e)
        {
            selectedButton = sender as Button;
            koltukDurumDegistir(null);
        }


        public Button selectedButton = new Button();

        public void koltukDurumDegistir(string status)
        {
            if (selectedButton.Tag == null) { selectedButton.Tag = " "; }

            if (status == "")
            {
                selectedButton.Image = rotateImage(Properties.Resources.koltuk, "270");
            }
            else if (selectedButton.Tag.ToString() != "erkek" && selectedButton.Tag.ToString() != "kadin" && status == "selected")
            {
                selectedButton.Image = rotateImage(Properties.Resources.koltuk_secili, "270");
            }
            else if (selectedButton.Tag.ToString() != "erkek" && selectedButton.Tag.ToString() != "kadin" && status == "overed")
            {
                selectedButton.Image = rotateImage(Properties.Resources.koltuk_over, "270");
            }
            else if (status == "erkek")
            {
                selectedButton.Image = rotateImage(Properties.Resources.koltuk_erkek_dolu, "270");
            }
            else if (status == "kadin")
            {
                selectedButton.Image = rotateImage(Properties.Resources.koltuk_kadin_dolu, "270");
            }
        }

        private int textFindCount(string text, string find)
        {

            int count = 0;

            if (text.Contains(find))
            {
                for (int c = 0; c < text.Length; c++)
                {
                    if (c + find.Length > text.Length - 1) { break; }
                    if (text.Substring(c, find.Length) == find)
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        private void aracPictureBox_Click(object sender, EventArgs e)
        {

        }

    }
}