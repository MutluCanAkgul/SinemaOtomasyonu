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
   
    public partial class FilmGuncellemeEkranı : Form
    {
        SqlConnection con = VeritabaniIslemleri.BaglantiDondur();
        int puan=0;
        public FilmGuncellemeEkranı()
        {
            InitializeComponent();
            VeriTabanidanTemaAdlariniAl();
            VeritabanindanFilmİsimleriniAl();
           
            foreach (Control control in groupBox1.Controls)
            {
                control.Enabled = false;
            }

        }

        private void FilmGuncellemeEkranı_Load(object sender, EventArgs e)
        {
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        int filmId;
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox2.Text == "")
            {
                MessageBox.Show("Güncellemek İstediğiniz Film Adını Seçin");
                foreach (Control control in groupBox1.Controls)
                {
                    control.Enabled = false;
                }
            }
            else
            {
                con.Open();
                SqlCommand komut1 = new SqlCommand("SELECT * FROM filmler WHERE filmAdi = '"+comboBox2.SelectedItem.ToString()+"'", con);
                SqlDataReader rdr = komut1.ExecuteReader();
                while (rdr.Read())
                {
                    filmId = Convert.ToInt32( rdr["int"]);
                    textBox1.Text = rdr["filmAdi"].ToString();
                    maskedTextBox1.Text = rdr["filmTarih"].ToString();
                    comboBox1.Text = rdr["filmTema"].ToString();
                    pictureBox1.ImageLocation = rdr["filmResim"].ToString() ;
                    textBox3.Text = rdr["filmSure"].ToString();
                    textBox4.Text = rdr["filmIcerik"].ToString();
                    puan = Convert.ToInt32(rdr["filmPuan"]);
                }
                con.Close();
                foreach (Control control in groupBox1.Controls)
                {
                    control.Enabled = true;
                }
            }
        }
       
        private void button1_Click(object sender, EventArgs e)
            
        {
            string filmPuan = puan.ToString();
            if (textBox1.Text != "")
            {
                con.Open();
                string sorgu = "UPDATE filmler SET filmAdi = @filmAdi WHERE int = '"+filmId+"'";
                SqlCommand komut2 = new SqlCommand(sorgu, con);
                komut2.Parameters.AddWithValue("@filmAdi", textBox1.Text);
                komut2.ExecuteNonQuery();
                con.Close();
            }
            else
            {
                MessageBox.Show("Film İsmi Girin");
            }
            if (comboBox1.SelectedIndex != -1)
            {
                con.Open();
                string sorgu = "UPDATE filmler SET filmTema = @filmTema WHERE int = '"+filmId+"'";
                SqlCommand komut2 = new SqlCommand(sorgu, con);
                komut2.Parameters.AddWithValue("@filmTema", comboBox1.SelectedItem);
                komut2.ExecuteNonQuery();
                con.Close();
            }
            else
            {
                MessageBox.Show("Film Teması Girin");
            }
            if (maskedTextBox1.Text != "")
            {
                con.Open();
                string sorgu = "UPDATE filmler SET filmTarih = @filmTarih WHERE int = '"+filmId+"'";
                SqlCommand komut2 = new SqlCommand(sorgu, con);
                komut2.Parameters.AddWithValue("@filmTarih", maskedTextBox1.Text);
                komut2.ExecuteNonQuery();
                con.Close();
            }
            else
            {
                MessageBox.Show("Film Tarihi Girin");
            }
            if (textBox3.Text != "")
            {
                con.Open();
                string sorgu = "UPDATE filmler SET filmSure = @filmSure WHERE int = '"+filmId+"'";
                SqlCommand komut2 = new SqlCommand(sorgu, con);
                komut2.Parameters.AddWithValue("@filmSure", textBox3.Text);
                komut2.ExecuteNonQuery();
                con.Close();
            }
            else
            {
                MessageBox.Show("Film Süresini Girin");
            }
            if (textBox4.Text != "")
            {
                con.Open();
                string sorgu = "UPDATE filmler SET filmIcerik = @filmIcerik WHERE int = '"+filmId+"'";
                SqlCommand komut2 = new SqlCommand(sorgu, con);
                komut2.Parameters.AddWithValue("@filmIcerik", textBox4.Text);
                komut2.ExecuteNonQuery();
                con.Close();
            }
            else
            {
                MessageBox.Show("Film Detayını Doldurun");
            }
            if (puan == 0) {
                MessageBox.Show("Lüften IMDB Puanı Seçin");
            }
            else
            {
                con.Open();
                string sorgu = "UPDATE filmler SET filmPuan = @filmPuan WHERE int = '"+filmId+"'";
                SqlCommand komut2 = new SqlCommand(sorgu, con);
                komut2.Parameters.AddWithValue("@filmPuan", filmPuan);
                komut2.ExecuteNonQuery();
                con.Close();
            }
            label7.Text = "Film Başarıyla Güncellendi";
            label7.ForeColor = System.Drawing.Color.Green;
            con.Open();
            string sorgu1 = "UPDATE filmler SET filmResim = @filmResim WHERE int = '" + filmId + "'";
            SqlCommand komut3 = new SqlCommand(sorgu1, con);
            komut3.Parameters.AddWithValue("@filmResim", resimYolu);
            komut3.ExecuteNonQuery();
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
            FilmSayfa flms = new FilmSayfa();
            this.Hide();
            flms.ShowDialog();  
            this.Close();
        }
    }
}
