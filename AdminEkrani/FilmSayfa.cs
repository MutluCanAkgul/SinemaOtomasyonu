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

namespace SinemaOtomasyonuOdev.AdminEkrani
{
    public partial class FilmSayfa : Form
    {
        SqlConnection con = VeritabaniIslemleri.BaglantiDondur();
        public FilmSayfa()
        {
            InitializeComponent();
            VeritabanindanFilmİsimleriniAl();
            VeriTabanidanTemaAdlariniAl();
            label1.Text = adminBilgileri.isim;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            adminsayfa adm = new adminsayfa();
            this.Hide();
            adm.ShowDialog();
            this.Close();
        }

        private void FilmSayfa_Load(object sender, EventArgs e)
        {
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime suAnkiTime = DateTime.Now;
            DateTime girilenTarihi;

            string secilenDeger = puan.ToString(); ;
            if (textBox1.Text == "" || comboBox1.Text == "" || textBox3.Text == "" || textBox4.Text == "" || maskedTextBox1.Text == "" || puan == 0)
            {
                MessageBox.Show("Bu alanların doldurulması zorunludur");
            }

            else
            {
                if (DateTime.TryParse(maskedTextBox1.Text, out girilenTarihi))
                {
                    if (girilenTarihi < suAnkiTime)
                    {
                        MessageBox.Show("Geçmiş Tarihlere ait Film Eklenmesi Yapılamaz");
                    }
                    else
                    {

                        string sorgu = "insert into filmler(filmAdi,filmTema,filmIcerik,filmSure,filmPuan,filmResim,filmTarih)values(@filmAdi,@filmTema,@filmIcerik,@filmSure,@filmPuan,@filmResim,@filmTarih)";
                        SqlCommand komut2 = new SqlCommand(sorgu, con);
                        komut2.Parameters.AddWithValue("@filmAdi", textBox1.Text);
                        komut2.Parameters.AddWithValue("@filmTema", comboBox1.Text);
                        komut2.Parameters.AddWithValue("@filmIcerik", textBox4.Text);
                        komut2.Parameters.AddWithValue("@filmSure", textBox3.Text);
                        komut2.Parameters.AddWithValue("@filmPuan", secilenDeger);
                        komut2.Parameters.AddWithValue("@filmResim", resimYolu);
                        komut2.Parameters.AddWithValue("@filmTarih", maskedTextBox1.Text);
                        con.Open();
                        komut2.ExecuteNonQuery();
                        textBox1.Clear();
                        maskedTextBox1.Clear();
                        textBox4.Clear();
                        textBox3.Clear();
                        con.Close();
                        label7.Text = "Film Başarıyla Eklendi";
                        label7.ForeColor = Color.Green;
                        pictureBox1.Image = Properties.Resources.cinema_kapak;
                        comboBox1.SelectedIndex = -1;
                        puan = 0;

                    }
                }

            }
        }
        private void VeriTabanidanTemaAdlariniAl()
        {
            con.Open();
            SqlCommand komut1 = new SqlCommand("SELECT * from FilmTemalar", con);
            SqlDataReader rdr = komut1.ExecuteReader();
            while (rdr.Read())
            {
                comboBox1.Items.Add(rdr["FilmTemasi"].ToString());
            }
            con.Close();
        }
        private void VeritabanindanFilmİsimleriniAl()
        {
            con.Open();
            SqlCommand komut2 = new SqlCommand("SELECT filmAdi FROM filmler ", con);
            SqlDataReader rdr = komut2.ExecuteReader();
            while (rdr.Read())
            {
                comboBox2.Items.Add(rdr["filmAdi"].ToString());
            }
            con.Close();
        }
        string resimYolu;
        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Resim Seçin";
            ofd.Filter = "PNG | *.png | JPG-JPEG | *.jpg;*.jpeg | All Files | *.*";
            ofd.FilterIndex = 3;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                resimYolu = ofd.FileName.ToString();
                pictureBox1.Image = Image.FromFile(ofd.FileName);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }

        }
        int puan = 0;
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            puan = 1;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            puan = 2;
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            puan = 3;
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            puan = 4;
        }

        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {
            puan = 5;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            puan = 6;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            puan = 7;
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            puan = 8;
        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            puan = 9;
        }

        private void radioButton10_CheckedChanged(object sender, EventArgs e)
        {
            puan = 10;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == -1)
            {
                MessageBox.Show("Silmek İstediğiniz Filmi Seçin");
            }
            else
            {
                con.Open();
                SqlCommand komut3 = new SqlCommand("DELETE FROM filmler WHERE filmAdi = '" + comboBox2.SelectedItem.ToString() + "'", con);
                komut3.ExecuteNonQuery();
                con.Close();
                label9.Text = "Film Başarıyla Silindi";
                label9.ForeColor = Color.Red;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FilmGuncellemeEkranı gune = new FilmGuncellemeEkranı();
            this.Hide();
            gune.ShowDialog();
            this.Close();
        }
    }
}
