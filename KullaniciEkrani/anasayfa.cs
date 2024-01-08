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
   
    public partial class anasayfa : Form
    {
       SqlConnection con = VeritabaniIslemleri.BaglantiDondur();
       
        public anasayfa()
        {
            InitializeComponent();
            
            
        }

        private void anasayfa_Load(object sender, EventArgs e)
        {
            Veriler();
            label1.Text = kullaniciBilgileri.isim;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form1 frm = new Form1();
            this.Hide();
            frm.ShowDialog();
            this.Close();
        }
        private void Veriler()
        {
            
            con.Open();
            SqlCommand komut = new SqlCommand("SELECT * FROM filmler ORDER BY filmAdi ASC",con);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                FilmListe arac = new FilmListe();
                arac.pictureBox1.ImageLocation = dr["filmResim"].ToString();
                arac.label1.Text = dr["filmAdi"].ToString();
                arac.label2.Text = dr["filmTema"].ToString();
                arac.idNo.Text = dr["int"].ToString();
                anaekran1.Controls.Add(arac);
               

            }
            con.Close();

        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            anaekran1.Controls.Clear();
            con.Open();
            SqlCommand komut2 = new SqlCommand("SELECT * FROM filmler WHERE filmAdi LIKE '%"+textBox1.Text+"%' ORDER BY filmAdi ASC",con);
            SqlDataReader oku = komut2.ExecuteReader(); 
            while (oku.Read())
            {
                FilmListe filmListe = new FilmListe();
                filmListe.pictureBox1.ImageLocation = oku["filmResim"].ToString() ;
                filmListe.label1.Text = oku["filmAdi"].ToString();
                filmListe.label2.Text = oku["filmTema"].ToString();    
                filmListe.idNo.Text = oku["int"].ToString();
                anaekran1.Controls.Add(filmListe);
            }
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BiletSayfası blt = new BiletSayfası();
            this.Hide();
            blt.ShowDialog();
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Biletlerim blt = new Biletlerim();
            this.Hide();           
            blt.ShowDialog();
            this.Dispose();
        }
    }
}
