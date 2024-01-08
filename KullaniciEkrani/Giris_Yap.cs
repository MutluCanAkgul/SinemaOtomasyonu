using SinemaOtomasyonuOdev.KullaniciEkrani;
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

namespace SinemaOtomasyonuOdev
{
    public partial class Giris_Yap : Form
    {
        public Giris_Yap()
        {
            InitializeComponent();
            textBox2.PasswordChar = '*';
        }
        SqlConnection con = VeritabaniIslemleri.BaglantiDondur();
       
        private void Giris_Yap_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Kayıt_Ol kyt = new Kayıt_Ol();
            this.Hide();
            kyt.ShowDialog();
            this.Dispose();
        }

        public void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Alanların Doldurulması Zorunludur !!");
            }
            else
            {
               
                con.Open();
                SqlCommand komut = new SqlCommand("SELECT * FROM kullanici WHERE email='"+textBox1.Text + "' and password='"+textBox2.Text+"'",con);
                SqlDataReader dt = komut.ExecuteReader();
                if (dt.Read())
                {
                    kullaniciBilgileri.isim = dt["name"].ToString();
                    kullaniciBilgileri.telNo = dt["telNo"].ToString();
                    anasayfa frm = new anasayfa();
                    this.Hide();
                    frm.ShowDialog();
                    this.Dispose();
                }
                else
                {
                    MessageBox.Show("email veya şifreniz hatalı");
                    textBox1.Clear();
                    textBox2.Clear();
                }
                con.Close();
            }
        }

        private void Giris_Yap_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                button1_Click(sender, e);
            }
        }
    }
}
