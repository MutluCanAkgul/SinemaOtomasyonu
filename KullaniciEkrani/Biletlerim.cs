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
    public partial class Biletlerim : Form
    {
        SqlConnection con = VeritabaniIslemleri.BaglantiDondur();
        public Biletlerim()
        {
            InitializeComponent();
        }

        private void Biletlerim_Load(object sender, EventArgs e)
        {
            VerileriGetir();
        }
        void VerileriGetir()
        {
            
            con.Open();
            SqlCommand komut = new SqlCommand("SELECT * FROM biletler WHERE ad = '"+kullaniciBilgileri.isim+"'", con);
            SqlDataReader rdr = komut.ExecuteReader();
            while (rdr.Read())
            {
                bilet bilet = new bilet();
                bilet.label2.Text = rdr["biletNo"].ToString();
                bilet.label11.Text = rdr["ad"].ToString();
                bilet.label12.Text = rdr["telNo"].ToString();
                bilet.label17.Text = rdr["secilenKoltuklar"].ToString();
                bilet.label13.Text = rdr["filmAdi"].ToString();
                bilet.label14.Text = rdr["tarih"].ToString();
                bilet.label16.Text = rdr["saat"].ToString(); 
                bilet.label15.Text = rdr["salonAdi"].ToString();
                bilet.label5.Text = rdr["biletTur"].ToString();
                flowLayoutPanel1.Controls.Add(bilet);
            }
            con.Close();
           
            
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
