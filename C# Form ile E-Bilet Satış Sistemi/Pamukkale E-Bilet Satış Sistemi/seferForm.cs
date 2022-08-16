using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Reflection;
//form layotla layot panelin içini doldurarak kutucukları oluşturdum
//tooltiple mesela fiyatın üzerine geldiğimizde indirim zımbırtısı çıkıyor 
namespace Pamukkale_E_Bilet_Satış_Sistemi
{
    public partial class seferForm : Form
    {

        public static void SetDoubleBuffered(Control c)
        {
            if (SystemInformation.TerminalServerSession) { return; }
            PropertyInfo aProp = typeof(Control).GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance);
            aProp.SetValue(c, true, null);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;//scrooları gizledim yukarı aşağı kaymaması için
            }
        }

        public seferForm()
        {
            InitializeComponent();
            DoubleBuffered = true;
        }


        MatchCollection seferNoktalariMatchs;
        int seferListFullHeight = 0;
        int scroll = 0;//mauseun tekerleğinin kaydırma miktarını ayarlamak için scroll atadım
        bool seferListWheel = false;
        Point mouseCursor = Cursor.Position;


        struct gunlerStil1
        {
            public List<Color> renkler;
            public List<Font> fontlar;
            public List<Size> boyutlar;
            public List<Point> konumlar;
            public int otele;
        }

        struct gunlerStil2
        {
            public List<Color> renkler;
            public List<Font> fontlar;
        }

        DateTime seferTarihi;
        private void seferForm_Load(object sender, EventArgs e)
        {
            seferTarihi = (Application.OpenForms["mainForm"] as mainForm).dateTimePicker1.Value;

            SetDoubleBuffered(seferPanelListesi);
            //hem yukarıdaki hemde aşağıdaki yazıların stili rengi vssi ni ayarladım tenk tonlarını painte buldum
            gunlerStil1 stil1 = new gunlerStil1();
            gunlerStil2 stil2 = new gunlerStil2();

            stil1.renkler = new List<Color>(){
                    Color.FromArgb(120, 120, 120),
                    Color.FromArgb(227, 227, 227)
            };
            stil1.fontlar = new List<Font>(){
                new Font("Candara", 14, FontStyle.Regular),
                new Font("Arial", 10, FontStyle.Regular)
            };
            stil1.boyutlar = new List<Size>(){
                new Size(125, 25),
                new Size(125, 21)
            };
            stil1.konumlar = new List<Point>(){
                new Point(18, 80),
                new Point(18, 107)
            };
            stil1.otele = 10;

            stil2.renkler = new List<Color>(){
                Color.White,
                Color.FromArgb(227, 9, 19)
            };
            stil2.fontlar = new List<Font>(){
                new Font("Candara", 14, FontStyle.Bold),
                new Font("Arial", 10, FontStyle.Bold)
            };


            Text = Application.ProductName + "    >--[ Uygun Seferler ]--<    ";


            seferNoktalariMatchs = (Application.OpenForms["mainForm"] as mainForm).seferNoktalariMtchs;

            string kalkisNoktasi = (Application.OpenForms["mainForm"] as mainForm).kalkisNoktalariCB.Text;
            string varisNoktasi = (Application.OpenForms["mainForm"] as mainForm).varisNoktalariCB.Text;
            string tarih = (Application.OpenForms["mainForm"] as mainForm).dateTimePicker1.Value.ToString("dd.MM.yyyy");

            kalkisVarisLB.Text = kalkisNoktasi + " \U0000279C " + varisNoktasi;

            for (int i = 0; i < 7; i++)
            {
                Label lb = new Label();
                if (i == 0)
                {
                    lb.ForeColor = stil2.renkler[0];
                    lb.BackColor = stil2.renkler[1];
                    lb.Font = stil2.fontlar[0];
                    lb.Cursor = Cursors.Default;

                }
                else
                {
                    lb.ForeColor = stil1.renkler[0];
                    lb.BackColor = stil1.renkler[1];
                    lb.Font = stil1.fontlar[0];
                    lb.Cursor = Cursors.Hand;
                    lb.MouseClick += new MouseEventHandler(SeferSec_MouseClick);
                }
                lb.Name = "ÜstLabel" + gunlerPanel.Controls.Count.ToString();
                lb.Size = stil1.boyutlar[0];
                lb.Location = stil1.konumlar[0];
                lb.Margin = new Padding(stil1.otele, 0, 0, 0);
                lb.TextAlign = ContentAlignment.MiddleCenter;
                lb.Text = seferTarihi.AddDays(i).ToString("dddd");
                gunlerPanel.Controls.Add(lb);
            }
            for (int i = 0; i < 7; i++)
            {
                Label lb = new Label();
                if (i == 0)
                {
                    lb.ForeColor = stil2.renkler[0];
                    lb.BackColor = stil2.renkler[1];
                    lb.Font = stil2.fontlar[1];
                    lb.Cursor = Cursors.Default;
                }
                else
                {
                    lb.ForeColor = stil1.renkler[0];
                    lb.BackColor = stil1.renkler[1];
                    lb.Font = stil1.fontlar[1];
                    lb.Cursor = Cursors.Hand;
                    lb.MouseClick += new MouseEventHandler(SeferSec_MouseClick);
                }
                lb.Name = "ÜstLabel" + gunlerPanel.Controls.Count.ToString();
                lb.Size = stil1.boyutlar[1];
                lb.Location = stil1.konumlar[1];
                lb.Margin = new Padding(stil1.otele, 0, 0, 0);
                lb.TextAlign = ContentAlignment.MiddleCenter;
                lb.Text = seferTarihi.AddDays(i).ToString("dd/MM/yyyy");
                gunlerPanel.Controls.Add(lb);
            }
            for (int i = 0; i < 7; i++)
            {
                Label lb = new Label();
                if (i == 0)
                {
                    lb.BackColor = Color.FromArgb(141, 0, 7);
                }
                else
                {
                    lb.BackColor = Color.FromArgb(170, 170, 170);
                }
                lb.Name = "ÜstLabel" + gunlerPanel.Controls.Count.ToString();
                lb.Size = new Size(stil1.boyutlar[0].Width, 2);
                lb.Location = stil1.konumlar[1];
                lb.Margin = new Padding(stil1.otele, 0, 0, 0);
                gunlerPanel.Controls.Add(lb);
            }

            int objCount = 0;
            foreach (Match sefer in seferNoktalariMatchs)
            {
                TableLayoutPanel seferPanel = new TableLayoutPanel();
                Label seferSuresi = new Label();
                Label seferGunAdi = new Label();
                Label seferSaat = new Label();
                Label peronBilgisi = new Label();
                PictureBox aracModeliPic = new PictureBox();
                PictureBox aracDonanimPic = new PictureBox();
                Label seferFiyat = new Label();
                Label seferSec = new Label();
                objCount++;

                string _seferSuresi = sefer.Groups[4].Value;
                string _seferGunAdi = sefer.Groups[2].Value;
                string _seferSaat = sefer.Groups[1].Value;
                string _seferId = sefer.Groups[9].Value;

                Image modelPic = null;
                string modelInfo = "";
                if (sefer.Groups[6].Value == "pamukyol2")
                {
                    modelPic = Properties.Resources.pamukyol2_1;
                    modelInfo = "Pamukyol 2+1";
                }
                else if (sefer.Groups[6].Value == "anadolu21")
                {
                    modelPic = Properties.Resources.anadolu2_1;
                    modelInfo = "Anadolu 2+1";
                }

                Image donanimPic = Properties.Resources.donanimlar;

                string _peronBilgisi = Regex.Match(sefer.Groups[8].Value, "<i class=\"fa fa-thumb-tack\"></i>([A-Z0-9-_ ]+)\\s+</div>").Groups[1].Value;//peron bilgisi
                string _seferFiyat = Regex.Match(sefer.Groups[8].Value, "<p class=\"internetfiyat\" style=\"(?:[^\"].*?)\">([0-9]+)<small style=\"(?:[^\"].*?)\"> TL</small></p>").Groups[1].Value;//sefer fiyatlarını çektim

                // seferSuresi
                seferSuresi.BackColor = Color.FromArgb(209, 209, 209);
                seferSuresi.Dock = DockStyle.Fill;
                seferSuresi.Font = new Font("Arial", 11F, FontStyle.Regular);
                seferSuresi.ForeColor = Color.FromArgb(223, 36, 47);
                seferSuresi.Location = new Point(46, 87);
                seferSuresi.Margin = new Padding(0);
                seferSuresi.Name = "seferSuresi" + objCount.ToString();
                seferSuresi.Size = new Size(131, 22);
                seferSuresi.TabIndex = 0;
                seferSuresi.Text = _seferSuresi.EndsWith("Dakika") ? _seferSuresi : _seferSuresi + " 0 Dakika";
                toolTip1.SetToolTip(seferSuresi, "Tahmini Sefer Süresi");
                seferSuresi.TextAlign = ContentAlignment.MiddleCenter;
                objCount++;

                // seferGunAdi
                seferGunAdi.BackColor = Color.FromArgb(209, 209, 209);
                seferPanel.SetColumnSpan(seferGunAdi, 2);
                seferGunAdi.Dock = DockStyle.Fill;
                seferGunAdi.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
                seferGunAdi.ForeColor = Color.FromArgb(85, 85, 85);
                seferGunAdi.Location = new Point(22, 68);
                seferGunAdi.Margin = new Padding(0);
                seferGunAdi.Name = "seferGunAdi" + objCount.ToString();
                seferGunAdi.Size = new Size(155, 19);
                seferGunAdi.TabIndex = 1;
                seferGunAdi.Text = _seferGunAdi;
                seferGunAdi.TextAlign = ContentAlignment.TopCenter;
                objCount++;

                // seferSaat
                seferSaat.BackColor = Color.FromArgb(209, 209, 209);
                seferPanel.SetColumnSpan(seferSaat, 2);
                seferSaat.Dock = DockStyle.Fill;
                seferSaat.Font = new Font("Arial", 32F, FontStyle.Bold);
                seferSaat.ForeColor = Color.FromArgb(85, 85, 85);
                seferSaat.Location = new Point(22, 12);
                seferSaat.Margin = new Padding(0);
                seferSaat.Name = "seferSaat" + objCount.ToString();
                seferPanel.SetRowSpan(seferSaat, 2);
                seferSaat.Size = new Size(155, 56);
                seferSaat.TabIndex = 2;
                seferSaat.Text = _seferSaat;
                toolTip1.SetToolTip(seferSaat, "Sefer Saati: " + _seferSaat);
                seferSaat.TextAlign = ContentAlignment.MiddleCenter;
                objCount++;

                // peronBilgisi
                peronBilgisi.BackColor = Color.FromArgb(136, 136, 136);
                seferPanel.SetColumnSpan(peronBilgisi, 5);
                peronBilgisi.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Regular);
                peronBilgisi.ForeColor = Color.White;
                peronBilgisi.Image = Properties.Resources.igne;
                peronBilgisi.ImageAlign = ContentAlignment.MiddleLeft;
                peronBilgisi.Location = new Point(177, 87);
                peronBilgisi.Margin = new Padding(0);
                peronBilgisi.Name = "peronBilgisi" + objCount.ToString();
                peronBilgisi.Padding = new Padding(20, 0, 0, 0);
                peronBilgisi.Size = new Size(334, 22);
                peronBilgisi.TabIndex = 4;
                peronBilgisi.Text = "     " + (_peronBilgisi.Contains("PERON") ? _peronBilgisi : "PERON Belirsiz");
                if (_seferSaat.StartsWith("00") || _seferSaat.StartsWith("01") || _seferSaat.StartsWith("02"))
                {
                    string ilkGun = seferTarihi.ToString("dddd");
                    string ikinciGun = seferTarihi.AddDays(1).ToString("dddd");
                    //gece otobüsleri için mesela pazartesi gecesi olursa sonuna salıya bağlayan gece yazıyor
                    ilkGun = ilkGun.Replace("Pazartesi", "Pazartesiyi ");
                    if (ilkGun.Split(' ').Length == 2) { goto atla1; }
                    ilkGun = ilkGun.Replace("Salı", "Salıyı ");
                    if (ilkGun.Split(' ').Length == 2) { goto atla1; }
                    ilkGun = ilkGun.Replace("Çarşamba", "Çarşambayı ");
                    if (ilkGun.Split(' ').Length == 2) { goto atla1; }
                    ilkGun = ilkGun.Replace("Perşembe", "Perşembeyi ");
                    if (ilkGun.Split(' ').Length == 2) { goto atla1; }
                    ilkGun = ilkGun.Replace("Cumartesi", "Cumartesiyi ");
                    if (ilkGun.Split(' ').Length == 2) { goto atla1; }
                    ilkGun = ilkGun.Replace("Cuma", "Cumayı ");
                    if (ilkGun.Split(' ').Length == 2) { goto atla1; }
                    ilkGun = ilkGun.Replace("Pazar", "Pazarı ");
                atla1:
                    ikinciGun = ikinciGun.Replace("Pazartesi", "Pazartesiye");
                    if (ikinciGun.Split(' ').Length == 2) { goto atla2; }
                    ikinciGun = ikinciGun.Replace("Salı", "Salıya ");
                    if (ikinciGun.Split(' ').Length == 2) { goto atla2; }
                    ikinciGun = ikinciGun.Replace("Çarşamba", "Çarşambaya ");
                    if (ikinciGun.Split(' ').Length == 2) { goto atla2; }
                    ikinciGun = ikinciGun.Replace("Perşembe", "Perşembeye ");
                    if (ikinciGun.Split(' ').Length == 2) { goto atla2; }
                    ikinciGun = ikinciGun.Replace("Cumartesi", "Cumartesiye ");
                    if (ikinciGun.Split(' ').Length == 2) { goto atla2; }
                    ikinciGun = ikinciGun.Replace("Cuma", "Cumaya ");
                    if (ikinciGun.Split(' ').Length == 2) { goto atla2; }
                    ikinciGun = ikinciGun.Replace("Pazar", "Pazara ");
                atla2:
                    peronBilgisi.Text = "     " + ilkGun + ikinciGun + "bağlayan gece.";
                    peronBilgisi.ForeColor = Color.Orange;
                }

                if (modelInfo.Contains("Anadolu"))
                {
                    peronBilgisi.Text = "     " + "Firma: Anadolu Turizm";
                    peronBilgisi.ForeColor = Color.Yellow;
                }
                peronBilgisi.TextAlign = ContentAlignment.MiddleLeft;
                objCount++;

                // aracModeliPic
                aracModeliPic.BackColor = Color.Transparent;
                aracModeliPic.Dock = DockStyle.Fill;
                aracModeliPic.Image = modelPic;
                aracModeliPic.Location = new Point(179, 38);
                aracModeliPic.Margin = new Padding(2, 0, 0, 0);
                aracModeliPic.Name = "aracModeliPic" + objCount.ToString();
                aracModeliPic.Size = new Size(233, 30);
                aracModeliPic.SizeMode = PictureBoxSizeMode.Zoom;
                aracModeliPic.TabIndex = 3;
                toolTip1.SetToolTip(aracModeliPic, modelInfo);
                aracModeliPic.TabStop = false;
                objCount++;

                // aracDonanimPic
                aracDonanimPic.BackColor = Color.Transparent;
                aracDonanimPic.Dock = DockStyle.Fill;
                aracDonanimPic.Image = donanimPic;
                aracDonanimPic.SizeMode = PictureBoxSizeMode.CenterImage;
                aracDonanimPic.Location = new Point(473, 38);
                aracDonanimPic.Margin = new Padding(0);
                aracDonanimPic.Name = "aracDonanimPic" + objCount.ToString();
                aracDonanimPic.Size = new Size(114, 30);
                aracDonanimPic.TabIndex = 5;
                toolTip1.SetToolTip(aracDonanimPic, "TV, 220V Priz, Android, 10inç Ekran");
                aracDonanimPic.TabStop = false;
                objCount++;

                // seferFiyat
                seferFiyat.Anchor = AnchorStyles.Top;
                seferFiyat.BackColor = Color.Transparent;
                seferFiyat.Font = new Font("Arial", 22F, FontStyle.Bold);
                seferFiyat.ForeColor = Color.FromArgb(51, 51, 51);
                seferFiyat.Location = new Point(687, 38);
                seferFiyat.Margin = new Padding(0);
                seferFiyat.Name = "seferFiyat" + objCount.ToString();
                seferFiyat.Size = new Size(71, 30);
                seferFiyat.TabIndex = 6;
                seferFiyat.Text = _seferFiyat;
                toolTip1.SetToolTip(seferFiyat, "Kart ile tek seferde (%10 indirim): " + (int.Parse(_seferFiyat) * 0.90).ToString("0.##"));
                seferFiyat.TextAlign = ContentAlignment.BottomRight;
                objCount++;

                // seferSec
                seferSec.Dock = DockStyle.Fill;
                seferSec.Location = new Point(846, 68);
                seferSec.Name = "seferSec" + objCount.ToString();
                seferSec.Size = new Size(94, 19);
                seferSec.Font = new Font("Microsoft Sans Serif", 14, FontStyle.Regular);

                seferSec.TabIndex = 7;
                seferSec.Tag = _seferId + "#" + _seferFiyat + "#" + modelInfo;
                if (sefer.Groups[8].Value.Contains(" SEFER SEÇ</button>"))
                {
                    seferSec.ForeColor = Color.White;
                    seferSec.Text = "\U00002714 SEFER SEÇ";
                    seferSec.Cursor = Cursors.Hand;
                    seferSec.MouseClick += new MouseEventHandler(seferSec_AracKoltuk);
                }
                else if (sefer.Groups[8].Value.Contains(" DOLU </button>"))
                {
                    seferSec.ForeColor = Color.PaleVioletRed;
                    seferSec.Text = "\U00002718 DOLU";
                    seferSec.Cursor = Cursors.No;
                }
                else
                {
                    seferSec.Text = "\U00002757 YOK";
                    seferSec.ForeColor = Color.PaleVioletRed;
                    seferSec.Cursor = Cursors.No;
                }

                seferSec.TextAlign = ContentAlignment.MiddleLeft;
                objCount++;

                // seferPanel
                seferPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
                seferPanel.BackColor = Color.Transparent;
                seferPanel.BackgroundImage = Properties.Resources.sefer_back;
                seferPanel.BackgroundImageLayout = ImageLayout.None;
                seferPanel.ColumnCount = 11;
                seferPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 22));//genişlikleri ayarladım
                seferPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 24));
                seferPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 131));
                seferPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 235));
                seferPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 61));
                seferPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 114));
                seferPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 99));
                seferPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 74));
                seferPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 73));
                seferPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100));
                seferPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 38));
                seferPanel.Controls.Add(seferFiyat, 7, 2);
                seferPanel.Controls.Add(seferSuresi, 2, 4);
                seferPanel.Controls.Add(seferGunAdi, 1, 3);
                seferPanel.Controls.Add(seferSaat, 1, 1);
                seferPanel.Controls.Add(peronBilgisi, 3, 4);
                seferPanel.Controls.Add(aracModeliPic, 3, 2);
                seferPanel.Controls.Add(aracDonanimPic, 5, 2);
                seferPanel.Controls.Add(seferSec, 9, 3);
                seferPanel.Location = new Point(1, 0);
                seferPanel.Margin = new Padding(1, 0, 1, 0);
                seferPanel.Name = "seferPanel" + objCount.ToString();

                seferPanel.RowCount = 6;
                seferPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 12F));
                seferPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 26F));
                seferPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
                seferPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 19F));
                seferPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 22F));
                seferPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 12F));
                seferPanel.Size = new Size(981, 118);
                seferListFullHeight += seferPanel.Height;
                //seferPanel.MouseHover += new EventHandler(seferPanel_MouseHover);
                //seferPanel.MouseLeave += new EventHandler(seferPanel_MouseLeave);

                seferPanelListesi.Controls.Add(seferPanel);
            }

            seferListFullHeight -= seferPanelListesi.Height;
            seferPanelListesi.VerticalScroll.Maximum = seferListFullHeight;
            seferPanelListesi.MouseWheel += new MouseEventHandler(seferListesi_MouseWheel);
        }


        private void seferListesi_MouseWheel(object sender, MouseEventArgs e)
        {
            if (!seferListWheel) { return; }

            int scrolling = (int)(seferPanelListesi.Controls[0].Height / 4);

            if (e.Delta > 0)
            {
                scroll -= scrolling;
                if (scroll <= 0)
                {
                    scroll = 0;
                    seferPanelListesi.Controls[0].Margin = new Padding(1, scrolling, 0, 0);
                }
            }
            else
            {
                scroll += scrolling;
                if (scroll >= seferListFullHeight)
                {
                    scroll = seferListFullHeight;
                    seferPanelListesi.Controls[0].Margin = new Padding(1, -scrolling, 0, 0);
                }
            }

            seferPanelListesi.VerticalScroll.Value = scroll;
        }

        private void mouseCursorTimer_Tick(object sender, EventArgs e)
        {
            mouseCursor = seferPanelListesi.PointToClient(MousePosition);


            if (seferPanelListesi.ClientRectangle.Contains(mouseCursor))
            {
                seferListWheel = true;
                seferPanelListesi.Focus();
            }
            else
            {
                seferListWheel = false;
                ActiveControl = null;
            }
        }

        private void seferPanel_MouseHover(object sender, EventArgs e)
        {
            (sender as TableLayoutPanel).BorderStyle = BorderStyle.Fixed3D;
        }

        private void seferPanel_MouseLeave(object sender, EventArgs e)
        {
            (sender as TableLayoutPanel).BorderStyle = BorderStyle.None;
        }

        Control lastSeferPanel;

        private void mouseHoverTimer_Tick(object sender, EventArgs e)
        {
            foreach (Control ctrl in seferPanelListesi.Controls)
            {
                mouseCursor = ctrl.PointToClient(MousePosition);

                if (!(ctrl is TableLayoutPanel)) { continue; }

                if (lastSeferPanel != null)
                {
                    (lastSeferPanel as TableLayoutPanel).BorderStyle = BorderStyle.None;
                }

                if (ctrl.ClientRectangle.Contains(mouseCursor))
                {
                    (ctrl as TableLayoutPanel).BorderStyle = BorderStyle.Fixed3D;
                    lastSeferPanel = ctrl;
                    return;
                }
            }
        }

        private void SeferSec_MouseClick(object sender, MouseEventArgs e)
        {
            int index = int.Parse((sender as Label).Name.Replace("ÜstLabel", ""));
            index = index % 7;
            seferTarihi = seferTarihi.AddDays(index);
            (Application.OpenForms["mainForm"] as mainForm).dateTimePicker1.Value = seferTarihi;
            (Application.OpenForms["mainForm"] as mainForm).button1.PerformClick();
            this.Dispose();

        }

        
        public string seferId = "";
        public string kalkisId = "";
        public string varisId = "";
        public string seferFiyat1 = "";
        public string seferAracAdi = "";

        private void seferSec_AracKoltuk(object sender, MouseEventArgs e)
        {
            seferId = (sender as Label).Tag.ToString().Split('#')[0];
            kalkisId = (Application.OpenForms["mainForm"] as mainForm).kalkisId;
            varisId = (Application.OpenForms["mainForm"] as mainForm).varisId;
            seferFiyat1 = (sender as Label).Tag.ToString().Split('#')[1];
            seferAracAdi = (sender as Label).Tag.ToString().Split('#')[2];


            aracForm2 frm = new aracForm2();
            frm.ShowDialog();

        }

        
    }
}
