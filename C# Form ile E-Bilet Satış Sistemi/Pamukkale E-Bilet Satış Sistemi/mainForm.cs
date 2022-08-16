using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pamukkale_E_Bilet_Satış_Sistemi
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
        }


        string anaSite = "https://www.pamukkale.com.tr/bilet/";

        string varisDurakList = "https://www.pamukkale.com.tr/ajax.php?islem=varis-durak-list&id=";

        string guzergahList = "https://www.pamukkale.com.tr/ajax.php?islem=guzergah-sef-link&guzergah=";

        string seferList = "https://www.pamukkale.com.tr/bilet/";

        Dictionary<string, string> kalkisNoktalari = new Dictionary<string, string>();
        Dictionary<string, string> varisNoktalari = new Dictionary<string, string>();
        Dictionary<string, string> seferNoktalari = new Dictionary<string, string>();

        public MatchCollection seferNoktalariMtchs;

        public string kalkisId = "";
        public string varisId = "";

        private void Form1_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;//programın doğru bir şekilde urlye bağlanmasını sağlıyor
            this.Text = Application.ProductName + "    >--[ ORÇUN BABUŞCU ]--<    " + DateTime.Now.Year.ToString();

            if (IsCheckUri(anaSite))
            {
                string anaSiteHTML = new WebClient().DownloadString(anaSite);
                anaSiteHTML = anaSiteHTML.Replace("'", "\"");
                anaSiteHTML = Encoding.UTF8.GetString(Encoding.Default.GetBytes(anaSiteHTML));
                kalkisNoktalari.Clear();
                kalkisNoktalariCB.Items.Clear();

                MatchCollection kalkisNoktalariMtchs = Regex.Matches(Regex.Matches(anaSiteHTML, "<select class=\"select-box\" id=\"kalkis-durak-sefer-list\" name=\"kalkis\"(.*?)>(.*?)</select>", RegexOptions.Singleline)[0].Groups[2].ToString(), "<option(.*?) value=\"(.*?)\">(.*?)</option>", RegexOptions.None);

                foreach (Match mtch in kalkisNoktalariMtchs)
                {
                    if (mtch.Groups[2].Value.ToString() != "-1") //ilk değerse, id si -1 dir.
                    {
                        kalkisNoktalari.Add(mtch.Groups[2].Value, mtch.Groups[3].Value);
                        kalkisNoktalariCB.Items.Add(mtch.Groups[3].Value);
                    }
                }

            }
            else
            {
                MessageBox.Show("İnternet Bağlantınızda Sorun Var!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }

            pictureLoader.Parent = this;
        }

        public bool IsCheckUri(string url)
        {
            try
            {
                var request = WebRequest.Create(url);//urlnin sağlamlığını kontrol ediyruz bağlantıya bakıyor
                request.Timeout = 5000;
                request.Method = "HEAD";

                var response = (HttpWebResponse)request.GetResponse();
                {
                    response.Close();
                    return response.StatusCode == HttpStatusCode.OK;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void kalkisNoktalariCB_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (IsCheckUri(anaSite))
            {
                Task startedTask = Task.Factory.StartNew(() =>
                {
                    pictureLoader.Height = 140;

                    kalkisId = kalkisNoktalari.Where(item => item.Value == kalkisNoktalariCB.Text).Select(item => item.Key).First().ToString();

                    string varisDurakSiteHTML = new WebClient().DownloadString(varisDurakList + kalkisId);
                    varisDurakSiteHTML = varisDurakSiteHTML.Replace("'", "\"");
                    System.Threading.Thread.Sleep(500); //bu taskı 0.5 sn uyut,karşıdan bilgiyi indirdiği için 

                    varisDurakSiteHTML = Encoding.UTF8.GetString(Encoding.Default.GetBytes(varisDurakSiteHTML));
                    varisNoktalari.Clear();
                    varisNoktalariCB.Items.Clear();

                    MatchCollection varisNoktalariMtchs = Regex.Matches(varisDurakSiteHTML, "<option(.*?) value=\"(.*?)\">(.*?)</option>", RegexOptions.Singleline);

                    System.Threading.Thread.Sleep(500);

                    foreach (Match mtch in varisNoktalariMtchs)
                    {
                        varisNoktalari.Add(mtch.Groups[2].Value, mtch.Groups[3].Value);
                        varisNoktalariCB.Items.Add(mtch.Groups[3].Value);
                    }

                    System.Threading.Thread.Sleep(500);

                    dateTimePicker1.Value = DateTime.Now;
                    dateTimePicker1.MinDate = dateTimePicker1.Value;

                    pictureLoader.Height = 0;
                });

            }
            else
            {
                MessageBox.Show("İnternet Bağlantınızda Sorun Var!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }

            pictureLoader.Height = 0;
        }

        private void kalkisNoktalariCB_DrawItem(object sender, DrawItemEventArgs e)
        {

            if (e.Index == -1) { return; }

            e.DrawBackground();

            Brush zeminRengi, yaziRengi;
            Graphics g = e.Graphics;

            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                zeminRengi = new SolidBrush(panel2.BackColor); //panelin kırmızı rengi 
                yaziRengi = new SolidBrush(Color.White);
            }
            else
            {
                zeminRengi = new SolidBrush(Color.White);
                yaziRengi = new SolidBrush(Color.FromArgb(38, 38, 38));
            }

            g.FillRectangle(zeminRengi, e.Bounds);

            StringFormat format = new StringFormat();
            format.LineAlignment = StringAlignment.Center; //Dikeyde ortada
            format.Alignment = StringAlignment.Near; //Yatayda solda
            g.DrawString((sender as ComboBox).Items[e.Index].ToString(), e.Font, yaziRengi, e.Bounds, format);

            zeminRengi.Dispose();
            yaziRengi.Dispose();

        }

        private void kalkisNoktalariCB_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            Size textSize = e.Graphics.MeasureString((sender as ComboBox).Items[e.Index].ToString(), (sender as ComboBox).Font).ToSize(); //İçerisindeki yazıya göre size hesapla

            e.ItemWidth = textSize.Width;
            e.ItemHeight = 50;

        }



        private void button1_Click(object sender, EventArgs e)
        {

            if (kalkisNoktalariCB.SelectedIndex < 0)
            {
                MessageBox.Show("Lütfen kalkış noktası seçiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (varisNoktalariCB.SelectedIndex < 0)
            {
                MessageBox.Show("Lütfen varış noktası seçiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //if (dateTimePicker1.Value <= DateTime.Now)
            //{
            //    MessageBox.Show("Lütfen gidiş tarihi seçiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}

            if (IsCheckUri(varisDurakList))
            {
                Task startedTask = Task.Factory.StartNew(() =>
                {
                    pictureLoader.Height = 140;

                    varisId = varisNoktalari.Where(item => item.Value == varisNoktalariCB.Text).Select(item => item.Key).First().ToString();

                    string guzergahSiteHTML = new WebClient().DownloadString(guzergahList + kalkisId + "-" + varisId);
                    guzergahSiteHTML = Encoding.UTF8.GetString(Encoding.Default.GetBytes(guzergahSiteHTML));

                    System.Threading.Thread.Sleep(1000); //bu taskı 1 sn uyut

                    if (guzergahSiteHTML == "" || !guzergahSiteHTML.Contains("-"))
                    {
                        MessageBox.Show("Kalkış ile Varış noktası arasında uygun sefer bulunamadı!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        kalkisNoktalariCB.Select();
                        kalkisNoktalariCB.SelectionStart = kalkisNoktalariCB.Text.Length;
                        kalkisNoktalariCB.SelectionLength = 0;
                        return;
                    }

                    string seferSiteHTML = new WebClient().DownloadString(seferList + guzergahSiteHTML + "/" + dateTimePicker1.Value.ToString("dd-MM-yyyy") + "/");
                    seferSiteHTML = Encoding.UTF8.GetString(Encoding.Default.GetBytes(seferSiteHTML));

                    seferSiteHTML = seferSiteHTML.Replace("'", "\"");

                    System.Threading.Thread.Sleep(500); //bu taskı 0.5 sn uyut


                    if (seferSiteHTML == "" || seferSiteHTML.Contains("<b>SEFER BULUNAMADI</b>"))
                    {
                        pictureLoader.Height = 0;
                        MessageBox.Show("Seçilen tarihte uygun sefer bulunamadı! Başka tarih seçiniz", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        dateTimePicker1.Focus();
                        return;
                    }

                    seferNoktalari.Clear();
                    System.IO.File.WriteAllText("text.txt", seferSiteHTML);// regexone da öğrendim regexstormda çevirdim
                    seferNoktalariMtchs = Regex.Matches(seferSiteHTML, "<div class=\"row sefer-list-kutu\">\\s+<div class=\"(?:[a-z0-9 -]+) sefer-list-kutu-tarih\">\\s+<div class=\"sefersaat\" title=\"(?:[^\"]*?)\">([0-9:]+)</div>\\s+<span class=\"sefergun (?:[a-z0-9 -]+)\">(\\w+)</span>\\s+<span class=\"sefertarih (?:[a-z0-9 -]+)\">([0-9/]+)</span>\\s+<span class=\"sefertarih (?:[a-z0-9 -]+)\" data-toggle=\"tooltip\" data-placement=\"top\" title=\"(?:[^\"]*?)\" style=\"(?:[^\"]*?)\"><i class=\"fa fa-clock-o\" aria-hidden=\"true\" style=\"(?:[^\"]*?)\"></i>([a-zA-Z0-9 ]+)</span>\\s+<div class=\"sefertarih (?:[a-z0-9 -]+)\" title=\"(?:[^\"]*?)\" style=\"(?:[^\"]*?)\"><i class=\"fa fa-clock-o\" aria-hidden=\"true\" style=\"(?:[^\"]*?)\"></i>([a-zA-Z0-9 ]+)</div>\\s+</div>\\s+<div class=\"(?:.*?) pd0\">\\s+<div class=\"(?:[^\"].*?) sefer-list-kutu-model text-center mobilmt\">\\s+<h5 class=\"(?:.*?) arac-margin\">ARAÇ MODELİ</h5>\\s+<div class=\"aracmodel pd0\">\\s+<img src=\"(?:.*?)/([a-z0-9]+).png\" alt=\"(?:.*?)\" title=\"(?:[^\"]*?)\">\\s+(?:[^\\0]*?)</div>\\s+</div>\\s+<div class=\"(?:[^\"]*?)\">\\s+<h5 class=\"(?:[^\"]*?)\">ARAÇ DONANIMI</h5>\\s+([^\\0]*?)\\s+</div>\\s+</div>\\s+<div class=\"(?:[^\\0\"]*?)\"><h5 class=\"(?:[^\\0\"]*?)\" style=\"(?:[^\"]*?)\">BİLET FİYATI</h5>([^\\0]*?)<div class=\"(?:[^\"]*?)\" style=\"(?:[^\"]*?)\" id=\"detay-([^\"]*?)\" data-yon=\"gidis\">", RegexOptions.None);

                    System.Threading.Thread.Sleep(500);

                    pictureLoader.Height = 0;// gifi koydum 0,5 sn dönen aralardaki

                    seferForm frm = new seferForm();
                    frm.ShowDialog();

                });

            }
            else
            {
                MessageBox.Show("İnternet Bağlantınızda Sorun Var!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }

            pictureLoader.Height = 0;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1.PerformClick();//butn1in görevini yap
        }

       





    }
}





//https://www.pamukkale.com.tr/ajax.php?islem=varis-durak-list&id=3401
//https://www.pamukkale.com.tr/ajax.php?islem=guzergah-sef-link&guzergah=3401-4100
//https://www.pamukkale.com.tr/bilet/istanbul-alibeykoy-izmit-kocaeli-otobus-bileti/12-03-2019/
//Regex attrib delete  --->  (\s+)((?!(class|src).)\w+)="(.*?)"