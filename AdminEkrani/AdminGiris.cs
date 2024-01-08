using SinemaOtomasyonuOdev.AdminEkrani;
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
    public partial class AdminGiris : Form
    {
       
        public AdminGiris()
        {
            InitializeComponent();
            textBox2.PasswordChar = '*';
        }
        SqlConnection con = VeritabaniIslemleri.BaglantiDondur();
        private void AdminGiris_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Bu alanların girilmesi zorunludur");
            }
            else
            {
                con.Open();
                SqlCommand komut = new SqlCommand("SELECT * FROM admin WHERE isim ='"+ textBox1.Text +"'and password='"+textBox2.Text+"'", con);
                SqlDataReader rdr = komut.ExecuteReader();
                if(rdr.Read())
                {
                  adminBilgileri.isim = rdr["isim"].ToString();    
                  adminsayfa frm = new adminsayfa();
                  this.Hide();
                  frm.ShowDialog();
                  this.Dispose(); 
                }
                else
                {
                    textBox1.Clear();
                    textBox2.Clear();
                    MessageBox.Show("isim yada şifreniz hatalı");
                }
                con.Close();
            }

        }

        private void AdminGiris_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                button1_Click(sender,e);
            }
        }
    }
}
