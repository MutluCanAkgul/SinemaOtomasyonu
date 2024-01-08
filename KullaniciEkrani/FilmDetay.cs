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

namespace SinemaOtomasyonuOdev.KullaniciEkrani
{
    public partial class FilmDetay : Form
    {
        public FilmDetay()
        {
            InitializeComponent();
        }
        public string idnum = "";
        private void FilmDetay_Load(object sender, EventArgs e)
        {
            Veriler();
        }
        void Veriler()
        {
            SqlConnection con = VeritabaniIslemleri.BaglantiDondur();
            con.Open();
            SqlCommand komut = new SqlCommand("SELECT * FROM filmler WHERE int = @idNum", con);         
            komut.Parameters.AddWithValue("@idNum", idnum);
            SqlDataReader rdr = komut.ExecuteReader();
            if (rdr.Read())
            {
                label5.Text = rdr["filmAdi"].ToString();
                label6.Text = rdr["filmTema"].ToString();
                label7.Text = rdr["filmTarih"].ToString();
                label8.Text = rdr["filmPuan"].ToString();
                label9.Text = rdr["filmSure"].ToString();
                pictureBox1.ImageLocation = rdr["filmResim"].ToString();
                label12.Text = rdr["filmIcerik"].ToString();
            }
            con.Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            anasayfa ana = new anasayfa();
            this.Hide();
            ana.ShowDialog();
            this.Dispose();
        }
    }
}
