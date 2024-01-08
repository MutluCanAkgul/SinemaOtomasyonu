using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;


namespace SinemaOtomasyonuOdev.KullaniciEkrani
{
    public partial class BiletSayfası : Form
    {
        SqlConnection con = VeritabaniIslemleri.BaglantiDondur();
        private CheckBox[] checkBoxes;
        string seciliCheckBox;
        string koltukSayisi;
        string GelenKoltuk;
        public BiletSayfası()
        {
            InitializeComponent();
            checkBoxes = new CheckBox[] { checkBox1, checkBox2, checkBox3 };

            foreach (CheckBox checkBox in checkBoxes)
            {
                checkBox.Click += CheckBox_Click;
            }
            filmAdiGetir();
            BiletNoOlustur();
            salonAdiOlustur();
            FilmTuruBelirle();
        }

        private void BiletSayfası_Load(object sender, EventArgs e)
        {
            
            lstBelirlenen.Visible = false;
            lstGelenKoltuk.Visible = false;
            
        }

        void filmAdiGetir()
        {
            con.Open();
            SqlCommand komut1 = new SqlCommand("SELECT filmAdi FROM filmler ", con);
            SqlDataReader rdr = komut1.ExecuteReader();
            while (rdr.Read())
            {
                comboBox1.Items.Add(rdr["filmAdi"].ToString());
                
            }
            con.Close();
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;  
         
        }


        void filmTarihGetir()
        {
            if (comboBox1.SelectedIndex != -1)
            {
                con.Open();
                SqlCommand komut2 = new SqlCommand("SELECT * FROM filmler WHERE filmAdi = '" + comboBox1.SelectedItem.ToString() + "'", con);
                SqlDataReader rdr = komut2.ExecuteReader();
                if (rdr.Read())
                {
                    comboBox2.Items.Add(rdr["filmTarih"].ToString());
                }
                con.Close();
            }
        }


        void BiletNoOlustur()
        {
            Random rnd = new Random();
            string karakter = "012345678998765432102345678901234567890";
            string kod = "";
            for(int i = 0;i < 11;i++)
            {
                kod += karakter[rnd.Next(karakter.Length)];
            }
            textBox1.Text = kod;
            textBox1.Enabled = false;
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            filmTarihGetir();
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
        }


        void salonAdiOlustur()
        {
            con.Open();
            SqlCommand komut3 = new SqlCommand("SELECT * FROM Salonlar", con);
            SqlDataReader rdr = komut3.ExecuteReader();
            while (rdr.Read())
            {
                comboBox3.Items.Add(rdr["salonAdi"].ToString());
            }
            con.Close();
            comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
            
        }
        void FilmTuruBelirle()
        {

            comboBox4.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
           
        }
        private void CheckBox_Click(object sender, EventArgs e)
        {
            CheckBox clickedCheckBox = (CheckBox)sender;

          
            if (clickedCheckBox.Checked)
            {
                
                foreach (CheckBox otherCheckBox in checkBoxes)
                {
                    if (otherCheckBox != clickedCheckBox)
                    {
                        otherCheckBox.Checked = false;
                    }
                }
                seciliCheckBox = clickedCheckBox.Text;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            anasayfa ana = new anasayfa();
            this.Hide();
            ana.ShowDialog();
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            if (textBox1.Text == "" || comboBox1.SelectedIndex == -1 || comboBox2.SelectedIndex == -1 ||
                comboBox3.SelectedIndex == -1 || comboBox4.SelectedIndex == -1) 
            {
                MessageBox.Show("Bu alanların doldurulması zorunludur");
            }
            else
            
            {

                if (!string.IsNullOrEmpty(seciliCheckBox))
                {
                    con.Open();
                    SqlCommand komut4 = new SqlCommand("INSERT INTO biletler(biletNo,filmAdi,tarih,saat,salonAdi,secilenKoltuklar,ad,telNo,biletTur)VALUES(@biletNo,@filmAdi,@tarih,@saat,@salonAdi,@secilenKoltuklar,@ad,@telNo,@biletTur)", con);
                    komut4.Parameters.AddWithValue("@biletNo", textBox1.Text);
                    komut4.Parameters.AddWithValue("@filmAdi", comboBox1.SelectedItem);
                    komut4.Parameters.AddWithValue("@tarih", comboBox2.SelectedItem);
                    komut4.Parameters.AddWithValue("@saat", seciliCheckBox);
                    komut4.Parameters.AddWithValue("@salonAdi", comboBox3.SelectedItem);
                    komut4.Parameters.AddWithValue("@secilenKoltuklar", label6.Text);
                    komut4.Parameters.AddWithValue("@ad", kullaniciBilgileri.isim);
                    komut4.Parameters.AddWithValue("@telNo", kullaniciBilgileri.telNo);
                    komut4.Parameters.AddWithValue("@biletTur", comboBox4.SelectedItem);
                    komut4.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Biletiniz Ayırtıldı İyi Seyirler :D");
                    comboBox1.SelectedIndex = -1;
                    comboBox2.SelectedIndex = -1;
                    comboBox3.SelectedIndex = -1;
                    comboBox4.SelectedIndex = -1;
                    textBox1.Text = "";
                    lstBelirlenen.Items.Clear();
                    label6.Text = "";
                }
                else
                {
                    MessageBox.Show("Lütfen Saat Girin");
                }

            }
        }

        void KoltukSayisinaUlasim()
        {
            if (comboBox3.SelectedIndex != -1)
            { 
            con.Open();
            SqlCommand komut5 = new SqlCommand("SELECT * FROM Salonlar WHERE salonAdi = '" + comboBox3.SelectedItem.ToString() + "'", con);
            SqlDataReader rdr = komut5.ExecuteReader();
            while (rdr.Read())
            {
                koltukSayisi = rdr["koltukSayisi"].ToString();
            }
            con.Close();
        
            }
            
        }
        void Koltuklar()
        {
            int koltukSayisiInt = Convert.ToInt16(koltukSayisi);
            flowLayoutPanel1.Controls.Clear();
            for (int i = 1; i <= koltukSayisiInt; i++)
            {
                Button btn = new Button();
                btn.Text = i.ToString();
                btn.Size = new Size(50, 50);
                btn.FlatStyle = FlatStyle.Flat;
                btn.Name = i.ToString();
              
                if (lstGelenKoltuk.Items.Contains(btn.Text))
                {
                    btn.BackColor = System.Drawing.Color.Red;
                    btn.Enabled = false;
                }
                else
                {
                    btn.BackColor = System.Drawing.Color.Green;
                }
                            
                flowLayoutPanel1.Controls.Add(btn);
                btn.Click += Btn_Click;
                KoltukGetir();
                KoltukAyirma();
                           
            }
            
        }
        private void Btn_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if(button.BackColor == System.Drawing.Color.Green)
            {
                button.BackColor = System.Drawing.Color.Yellow;
                lstBelirlenen.Items.Add(button.Text);
                label6.Text = "";
                foreach(string item in lstBelirlenen.Items)
                {
                    label6.Text += " ," + item;
                }
                if(label6.Text.Length > 2)
                {
                    label6.Text = label6.Text.Substring(2);
                }
            }
            else if (button.BackColor == System.Drawing.Color.Yellow)
            {
                button.BackColor = System.Drawing.Color.Green;
                lstBelirlenen.Items.Remove(button.Text);
                label6.Text = "";
                foreach (string item in lstBelirlenen.Items)
                {
                    label6.Text += " ," + item;
                }
                if (label6.Text.Length > 2)
                {
                    label6.Text = label6.Text.Substring(2);
                }
            }
           
        }
        void KoltukGetir()
        {
            GelenKoltuk = "";
            if (comboBox3.SelectedItem != null)
            {
                con.Open();
                SqlCommand komut6 = new SqlCommand("SELECT * FROM biletler WHERE  filmAdi = @filmAdi AND tarih = @tarih AND salonAdi = @salonAdi AND saat = @saat", con);
                komut6.Parameters.AddWithValue("@filmAdi",comboBox1.SelectedItem.ToString());
                komut6.Parameters.AddWithValue("@tarih", comboBox2.SelectedItem.ToString());
                komut6.Parameters.AddWithValue("@saat", seciliCheckBox);
                komut6.Parameters.AddWithValue("@salonAdi", comboBox3.SelectedItem.ToString());
                SqlDataReader rdr = komut6.ExecuteReader();
                while (rdr.Read())
                {
                    GelenKoltuk = " ," + rdr["secilenKoltuklar"].ToString();
                    if (GelenKoltuk.Length > 2)
                    {
                        GelenKoltuk = GelenKoltuk.Substring(2);
                    }
                }
                con.Close();
            }

        }
        void KoltukAyirma()
        {
          
            lstGelenKoltuk.Items.Clear();
            string no = "";
            string[] sec;
            no = GelenKoltuk;
            sec = no.Split(',');
            foreach(string s in sec) 
            {
               
                lstGelenKoltuk.Items.Add(s);
              
            }
           
           
        }
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            KoltukSayisinaUlasim();
            Koltuklar();
           
            
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}
