using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;
using System.Web;
using System.Xml;
using System.Net;
using System.Runtime.InteropServices;
using System.Drawing.Text;
/*
 * @author Ahmet Ozan Tekin
 *                    2015 
 * 
 * ahmetozantekin@gmail.com
 * 
 */


namespace stok_programi
{
    public partial class Form1 : Form
    {
       
        public Form1()
        {
            InitializeComponent();
        }

        // veritabani baglantisi *->* data.accdb
        public OleDbConnection connect     = new OleDbConnection("Provider=Microsoft.Ace.Oledb.12.0;Data Source=/*EXAMPLE.accdb*/");
        public DataTable tablo             = new DataTable();
        public OleDbDataAdapter adtr       = new OleDbDataAdapter(); /* veritabanına sorgu çekmek için kullanılır */
        public OleDbCommand comand         = new OleDbCommand();    /*                                           */    


        string DosyaAdi = "";
        int id;
     
        // DATAGRİDVİEW GORUNTULEME
        public void urunListele()
        {
            tablo.Clear();
            connect.Open();
            OleDbDataAdapter adtr = new OleDbDataAdapter("select stokSeriNo,stokAdi,stokMarka,stokModeli,stokAdedi,stokTedarikci,stokAciklama,stokTarih,kayitYapan,stokUcret From stokbil", connect);
            adtr.Fill(tablo);
            dataGridView1.DataSource = tablo;
            adtr.Dispose();
            connect.Close();

            try
            {
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                //datagridview1'deki tüm satırı seç              


                dataGridView1.Columns[0].HeaderText = "STOK KODU";
                dataGridView1.Columns[1].HeaderText = "STOK ADI";
                dataGridView1.Columns[2].HeaderText = "STOK MARKA";
                dataGridView1.Columns[3].HeaderText = "STOK MODEL";
                dataGridView1.Columns[4].HeaderText = "STOK ADET";
                dataGridView1.Columns[5].HeaderText = "TEDARİKÇİ";
                dataGridView1.Columns[6].HeaderText = "AÇIKLAMA";
                dataGridView1.Columns[7].HeaderText = "TARİH";
                dataGridView1.Columns[8].HeaderText = "EKLEYEN KİŞİ";
                dataGridView1.Columns[9].HeaderText = "UCRET($)";
                //gatagridview sütunlarındaki textleri değiştirme
                
                dataGridView1.Columns[0].Width = 90; 
                dataGridView1.Columns[1].Width = 120;
                dataGridView1.Columns[2].Width = 120;
                dataGridView1.Columns[3].Width = 80;
                dataGridView1.Columns[4].Width = 50;
                dataGridView1.Columns[5].Width = 120;
                dataGridView1.Columns[6].Width = 120;
                dataGridView1.Columns[7].Width = 80;
                dataGridView1.Columns[8].Width = 80;
                dataGridView1.Columns[9].Width = 70;
                //sütunların genişliğini belirleme
            } catch { ; }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            urunListele();
                      
        }
    
        // EKLEME
        private void btnStokEkle_Click(object sender, EventArgs e)
        {
            try
            {   
                // bos gecilemez uyarilari
                if (textBox1.Text.Trim() == "")
                {
                    errorProvider1.SetError(textBox1, "Boş geçilmez");
                }
                else { errorProvider1.SetError(textBox1, ""); }


                if (textBox2.Text.Trim() == "")
                {
                    errorProvider1.SetError(textBox2, "Boş geçilmez");
                }
                else { errorProvider1.SetError(textBox2, ""); }


                if (textBox3.Text.Trim() == "")
                {
                    errorProvider1.SetError(textBox3, "Boş geçilmez");
                }
                else { errorProvider1.SetError(textBox3, ""); }


                if (textBox4.Text.Trim() == "")
                {
                    errorProvider1.SetError(textBox4, "Boş geçilmez");
                }
                else { errorProvider1.SetError(textBox4, ""); }


                if (textBox1.Text.Trim() != "" && textBox2.Text.Trim() != "" && textBox3.Text.Trim() != "" && textBox4.Text.Trim() != "" && textBox5.Text.Trim() != "")
                {
                    connect.Open();
                    comand.Connection = connect;

                    //sql sorgusu, insert into table(...) values (...)
                    comand.CommandText = "INSERT INTO stokbil(stokAdi,stokModeli,stokSeriNo,stokAdedi,stokTarih,kayitYapan,stokMarka,stokTedarikci,stokAciklama,stokUcret,dosyaAdi) VALUES ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + dateTimePicker1.Text + "','" + textBox5.Text + "','" + textBox8.Text + "','" + textBox9.Text+ "','" + richTextBox1.Text+ "','"+ textBox11.Text+ "','" + DosyaAdi + "') ";
                    comand.ExecuteNonQuery();
                    comand.Dispose();
                    connect.Close();

                    for (int i = 0; i < this.Controls.Count; i++)
                    {
                        if (this.Controls[i] is TextBox) this.Controls[i].Text = "";
                    }
                    urunListele();

                    if (DosyaAdi != "") File.WriteAllBytes(DosyaAdi, File.ReadAllBytes(DosyaAc.FileName));
                    MessageBox.Show("Kayıt İşlemi Tamamlandı ! ", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //information messagebox
                }
              
            }
            catch 
            {
                MessageBox.Show("Kayıtlı Stok Kodu !");
               connect.Close();           
            }           
        }        
        
        // DATAGRİDVİEW CELL CLİCK
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //datagridview'de seçilen ssatirdaki değerleri, ilgili textboxların içine yazdırır.
         


            textBox3.Text        = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox1.Text        = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox8.Text        = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox2.Text        = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox4.Text        = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            textBox9.Text        = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            richTextBox1.Text    = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            dateTimePicker1.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            textBox5.Text        = dataGridView1.CurrentRow.Cells[8].Value.ToString();
            textBox11.Text       = dataGridView1.CurrentRow.Cells[9].Value.ToString();
            try
            {
                comand = new OleDbCommand("SELECT * FROM stokbil WHERE stokSeriNo='" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'", connect);
                connect.Open();

                //
                // datagridview'de tıklanan satırın veritabani tarafında id'si tespit edilir.
                // bu id daha sonra güncelleme işleminde gerekli olacak.
                //
                OleDbDataReader oku = comand.ExecuteReader();
                oku.Read();
                if (oku.HasRows == true)
                {
                    id=Convert.ToInt32(oku[0].ToString());
                }
                connect.Close();
            }
                
            catch
            {
                connect.Close();
            }
        }
        
        
        // İSME GORE ARAMA
        private void btnStokAdiAra_Click(object sender, EventArgs e)
        {
            OleDbDataAdapter adtr = new OleDbDataAdapter("select * From stokbil", connect);
            if (textBox6.Text.Trim()== "")
            {
                tablo.Clear();
                comand.Connection = connect;
                comand.CommandText = "Select * from stokbil";
                adtr.SelectCommand = comand;
                adtr.Fill(tablo);               
            }
            if (Convert.ToBoolean(connect.State) == false)
            {
                connect.Open();
            }
            if (textBox6.Text.Trim() != "")
            {
                // stok ada göre veri tabanından arama 
                // sql ** SELECT * FROM stokbil WHERE stokAdi LIKE '%  ??  %';

                adtr.SelectCommand.CommandText = " Select * From stokbil WHERE stokAdi LIKE '%" + textBox6.Text + "%'"; //sql sorgusu 
                tablo.Clear();
                adtr.Fill(tablo);
                connect.Close();
            }
        }
        
        // MODELE GORE ARAMA
        private void btnStokModelAra_Click(object sender, EventArgs e)
        {
            OleDbDataAdapter adtr = new OleDbDataAdapter("select * From stokbil", connect);
            if (textBox7.Text.Trim() == "")
            {
                tablo.Clear();
                comand.Connection = connect;
                comand.CommandText = "Select * from stokbil";
                adtr.SelectCommand = comand;
                adtr.Fill(tablo);               
            }
            if (Convert.ToBoolean(connect.State) == false)
            {
                connect.Open();
            }
            if (textBox7.Text.Trim() != "")
            {
                // stok modele göre veri tabanından arama 
                // sql ** SELECT * FROM stokbil WHERE stokModeli = '??';

                adtr.SelectCommand.CommandText = " Select * From stokbil WHERE stokModeli LIKE '%" + textBox7.Text + "%'";
                tablo.Clear();
                adtr.Fill(tablo);
                connect.Close();
            }
        }
      
        // SİLME
        private void btnStokSil_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult cevap;

                //YES/NO messagebox ekranı gelir.
                cevap = MessageBox.Show("Kaydı silmek istediğinizden emin misiniz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
              
                //messagebox -> YES ve stokSeriNo alanı dolu ise silme işlemini yapar.
                if (cevap == DialogResult.Yes && dataGridView1.CurrentRow.Cells[0].Value.ToString().Trim() != "")
                {
                    connect.Open();
                    comand.Connection = connect;

                    // veritabanından veri silme
                    // DELETE FROM stokbil WHERE seriNo='datagridviewde seçilen seri no";
                    // stokSeriNo'nun datagridviewdeki değerine göre silinir.
                    // çünkü her ürünün bir seri numarası olmalıdır

                    comand.CommandText = "DELETE from stokbil WHERE stokSeriNo='" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "' ";
                    comand.ExecuteNonQuery();
                    comand.Dispose();
                    connect.Close();
                    urunListele();
                }
            } catch{ ; }
        } 
     
        // KEYPRESSLER
        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        { 
            //
            //isdigit
            // e.Handled = char.IsDigit(e.KeyChar) == false && e.KeyChar != (char)08 && e.KeyChar != (char)44;
            //
            if (char.IsDigit(e.KeyChar) == false && e.KeyChar != (char)08 && e.KeyChar != (char)44)
            {
                e.Handled = true;
                MessageBox.Show("Sadece Sayı Girişi Yapabilirsiniz ! ", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);                           
            }            
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //
            //isletter
            // e.Handled = char.IsLetter(e.KeyChar) == false && e.KeyChar != (char)08 && e.KeyChar != (char)44;
            //
            if (char.IsLetter(e.KeyChar) == false && e.KeyChar != (char)08 && e.KeyChar != (char)44)
            {
                e.Handled = true;
                MessageBox.Show("Sadece Harf Girişi Yapabilirsiniz ! ", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }        
        }
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) == false && e.KeyChar != (char)08 && e.KeyChar != (char)44)
            {
                e.Handled = true;
                MessageBox.Show("Sadece Harf Girişi Yapabilirsiniz ! ", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }      
        }
        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) == false && e.KeyChar != (char)08 && e.KeyChar != (char)44)
            {
                e.Handled = true;
                MessageBox.Show("Sadece Harf Girişi Yapabilirsiniz ! ", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }      
        }
        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) == false && e.KeyChar != (char)08 && e.KeyChar != (char)44)
            {
                e.Handled = true;
                MessageBox.Show("Sadece Harf Girişi Yapabilirsiniz ! ", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }      
        }
        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) == false && e.KeyChar != (char)08 && e.KeyChar != (char)44)
            {
                e.Handled = true;
                MessageBox.Show("Sadece Harf Girişi Yapabilirsiniz ! ", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }      
        }
        private void textBox11_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) == false && e.KeyChar != (char)08 && e.KeyChar != (char)44)
            {
                e.Handled = true;
                MessageBox.Show("Sadece Sayı Girişi Yapabilirsiniz ! ", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }            
        }
       
        
        // CIKIS BUTONU
        private void btnCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // GUNCELLEME
        private void btnStokGuncelle_Click(object sender, EventArgs e)
        {

            // textboxlarda boş alan olup olmadığı kontrol eden if döngüsü.
            if (textBox1.Text.Trim() != "" && textBox2.Text.Trim() != "" && textBox3.Text.Trim() != "" && textBox4.Text.Trim() != "" && textBox5.Text.Trim() != "" && textBox8.Text.Trim() != "" && textBox9.Text.Trim() != "")
            {
                    //veritabanı güncelleme sorgusu -->> UPDATE tablo SET sutun='deger', sutun2='deger' .... WHERE ....
                    string sorgu =  "UPDATE stokbil SET stokAdi='" + textBox1.Text + 
                                    "',stokModeli='"    + textBox2.Text         + 
                                    "',stokSeriNo='"    + textBox3.Text         + 
                                    "',stokAdedi='"     + textBox4.Text         +
                                    "',stokTarih='"     + dateTimePicker1.Text  + 
                                    "',kayitYapan='"    + textBox5.Text         + 
                                    "',stokMarka='"     + textBox8.Text         + 
                                    "',stokTedarikci='" + textBox9.Text         + 
                                    "',stokAciklama='"  + richTextBox1.Text     +
                                    "',stokUcret='"     + textBox11.Text        +
                                    "',dosyaAdi='"      + DosyaAdi              +
                                    "' WHERE id=" + id;
                    
                   // tıkladığımız satırın id'sine göre textboxlara yazılan değerleri eski verilerle günceller.

                    OleDbCommand comand = new OleDbCommand(sorgu,connect);
                    connect.Open();
                    comand.ExecuteNonQuery();
                    comand.Dispose();
                    connect.Close();
                    urunListele();
                    if (DosyaAdi != "") File.WriteAllBytes(DosyaAdi, File.ReadAllBytes(DosyaAc.FileName));
                    MessageBox.Show("Güncelleme İşlemi Tamamlandı !","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show("Boş Alan Bırakmayınız !");
                }
            
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Ürünü kayıt ettiğiniz isme göre arayınız.\nAlttaki alanı doldurup, butona basınız.");
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        // TUMUNU GOSTER
        private void button1_Click(object sender, EventArgs e)
        {
            //tümünü göster butonu, bütün datagridviewi listeleyecek.
            urunListele();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Ürünü kayıt ettiğiniz modele göre arayınız.\nAlttaki alanı doldurup, butona basınız.");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ///
            ///ÜRÜNÜN FOTOĞRAFINI ÇEKMEK ÜZERE WEBCAMDE KULLANILACAK KODLAR.
            ///eMU
            ///******
            //CommonDialogClass dialog = new CommonDialogClass();
            //Device camera = dialog.ShowSelectDevice(WiaDeviceType.CameraDeviceType, true, false);
            //// take the photo 
            //Item item = camera.ExecuteCommand(CommandID.wiaCommandTakePicture);
            //ImageFile image = (ImageFile)item.Transfer(FormatID.wiaFormatJPEG);
            //// filename and saving 
            //image.SaveFile("Test.jpg");
        //    CameraCaptureUI cameraUI = new CameraCaptureUI();

        //    cameraUI.PhotoSettings.AllowCropping = false;
        //    cameraUI.PhotoSettings.MaxResolution = CameraCaptureUIMaxPhotoResolution.MediumXga;

        //    Windows.Storage.StorageFile capturedMedia =
        //        await cameraUI.CaptureFileAsync(CameraCaptureUIMode.Photo);

        //    if (capturedMedia != null)
        //    {
        //        using (var streamCamera = await capturedMedia.OpenAsync(FileAccessMode.Read))
        //        {

        //            BitmapImage bitmapCamera = new BitmapImage();
        //            bitmapCamera.SetSource(streamCamera);
        //            // To display the image in a XAML image object, do this:
        //            // myImage.Source = bitmapCamera;

        //            // Convert the camera bitap to a WriteableBitmap object, 
        //            // which is often a more useful format.

        //            int width = bitmapCamera.PixelWidth;
        //            int height = bitmapCamera.PixelHeight;

        //            WriteableBitmap wBitmap = new WriteableBitmap(width, height);

        //            using (var stream = await capturedMedia.OpenAsync(FileAccessMode.Read))
        //            {
        //                wBitmap.SetSource(stream);
        //            }
        //        }
        //    }
        }

        // ADA GÖRE SIRALA
        private void button2_Click_1(object sender, EventArgs e)
        {
           
            OleDbDataAdapter adtr = new OleDbDataAdapter("SELECT * FROM stokbil ORDER BY stokAdi", connect);
                tablo.Clear();
                comand.Connection = connect;
                //SELECT * FROM tabloadı ORDER BY sutun  (siralama)
                comand.CommandText = "SELECT * FROM stokbil ORDER BY stokAdi";
                adtr.SelectCommand = comand;
                adtr.Fill(tablo);
          
        }

        // MARKAYA GÖRE SIRALA
        private void button3_Click(object sender, EventArgs e)
        {
            OleDbDataAdapter adtr = new OleDbDataAdapter("SELECT * FROM stokbil ORDER BY stokMarka", connect);
            tablo.Clear();
            comand.Connection = connect;
            comand.CommandText = "SELECT * FROM stokbil ORDER BY stokMarka";
            adtr.SelectCommand = comand;
            adtr.Fill(tablo);
        }

        // TEDARİKÇİYE GÖRE SIRALA
        private void button4_Click(object sender, EventArgs e)
        {
            OleDbDataAdapter adtr = new OleDbDataAdapter("SELECT * FROM stokbil ORDER BY stokTedarikci", connect);
            tablo.Clear();
            comand.Connection = connect;
            comand.CommandText = "SELECT * FROM stokbil ORDER BY stokTedarikci";
            adtr.SelectCommand = comand;
            adtr.Fill(tablo);

        }
        
        // MARKAYA GORE ARA
        private void button5_Click(object sender, EventArgs e)
        {
            OleDbDataAdapter adtr = new OleDbDataAdapter("select * From stokbil", connect);
            if (textBox10.Text.Trim() == "")
            {
                tablo.Clear();
                comand.Connection = connect;
                comand.CommandText = "Select * from stokbil";
                adtr.SelectCommand = comand;
                adtr.Fill(tablo);
            }
            if (Convert.ToBoolean(connect.State) == false)
            {
                connect.Open();
            }
            if (textBox10.Text.Trim() != "")
            {
                // stok modele göre veri tabanından arama 
                // sql ** SELECT * FROM stokbil WHERE stokModeli = '??';

                adtr.SelectCommand.CommandText = " Select * From stokbil WHERE stokMarka LIKE '%" + textBox10.Text + "%'";
                tablo.Clear();
                adtr.Fill(tablo);
                connect.Close();
            }
        }

        // TARİHE GÖRE SIRALA
        private void button6_Click(object sender, EventArgs e)
        {
            OleDbDataAdapter adtr = new OleDbDataAdapter("SELECT * FROM stokbil ORDER BY stokTedarikci", connect);
            tablo.Clear();
            comand.Connection = connect;
            comand.CommandText = "SELECT * FROM stokbil ORDER BY stokTarih";
            adtr.SelectCommand = comand;
            adtr.Fill(tablo);
        }

        //DOLARI CEVİRME
        private void button7_Click(object sender, EventArgs e)
        {
            //doların değerini çeviren web request API
           // char tl = '\u00A8'; /* TL SİMGESİ */
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://currencies.apps.grandtrunk.net/getlatest/usd/try");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            var reader = new StreamReader(response.GetResponseStream());
            string json_result = reader.ReadToEnd();

            MessageBox.Show("1 $ = " + json_result + /*tl*/" TL");
            

        }

        //RESİME TIKLANDIGINDA SİTEYE YONLENDİR
        //picture event
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.porte.com.tr/");
        }

        private void label14_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Ahmet Ozan Tekin\n\nSakarya Üniversitesi\nBilgisayar ve Bilişim Bilimleri Fakültesi\nBilgisayar Mühendisliği\n2015\n\nahmetozantekin@gmail.com\n", "©");
        }

        private void label14_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("Ahmet Ozan Tekin\n\nSakarya Üniversitesi\nBilgisayar ve Bilişim Bilimleri Fakültesi\nBilgisayar Mühendisliği\n2015\n\nahmetozantekin@gmail.com\n", "©");
        }
       
     


        // ÜCRETE GÖRE SIRALA
        //private void button7_Click(object sender, EventArgs e)
        //{
        //    int ucret = Convert.ToInt32(textBox11.Text);

        //    OleDbDataAdapter adtr = new OleDbDataAdapter("SELECT * FROM stokbil ORDER BY stokTedarikci", connect);
        //    tablo.Clear();
        //    comand.Connection = connect;
        //    //comand.CommandText = "SELECT * FROM stokbil ORDER BY stokUcret";
        //    comand.CommandText = "SELECT * FROM stokbil ORDER BY CAST(stokUcret AS Numeric(10,0))";
        //    //

        //    adtr.SelectCommand = comand;
        //    adtr.Fill(tablo);
        //}

      
    }
}

//#