using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SinemaOtomasyonuOdev.AdminEkrani
{
    public partial class adminsayfa : Form
    {
       
      
        public adminsayfa()
        {
            InitializeComponent();
        }
        
    
        private void adminsayfa_Load(object sender, EventArgs e)
        {          
            label1.Text = adminBilgileri.isim;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form1 form = new Form1();
            this.Hide();
            form.ShowDialog();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SalonEkranı sln = new SalonEkranı();
            this.Hide();
            sln.ShowDialog();
            this.Dispose();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            FilmSayfa flms = new FilmSayfa();
            this.Hide();
            flms.ShowDialog();
            this.Close();
        }
    }
}
