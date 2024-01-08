using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SinemaOtomasyonuOdev
{
    public partial class Kayıt_Ol : Form
    {
       
        public Kayıt_Ol()
        {
            InitializeComponent();
            textBox5.PasswordChar = '*';
            textBox6.PasswordChar = '*';
        }
        SqlConnection con = VeritabaniIslemleri.BaglantiDondur();
    
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void Kayıt_Ol_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Giris_Yap grs = new Giris_Yap();
            this.Hide();
            grs.ShowDialog();
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String password = textBox5.Text;
            String passwordSuccess = textBox6.Text;
            isnameCntrl(textBox1.Text);
            isnameCntrl(textBox2.Text);
            isemailCntrl(textBox4.Text);
            if (textBox1.Text == "" || textBox2.Text == "" || maskedTextBox1.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "")
            {
                MessageBox.Show("Alanların doldurulması zorunludur");
            }
            else
            {
             if(password.Count() < 8)
                {
                    MessageBox.Show("Şifreniz en az 7 karakterli olmalıdır");
                } 
             else
                {
                    if(password == passwordSuccess)
                    {
                        string sorgu = "insert into kullanici(name,surname,telNo,email,password)values(@name,@surname,@telNo,@email,@password)";
                        SqlCommand komut = new SqlCommand(sorgu, con);
                        komut.Parameters.AddWithValue("@name" ,textBox1.Text);
                        komut.Parameters.AddWithValue("@surname", textBox2.Text);
                        komut.Parameters.AddWithValue("@telNo", maskedTextBox1.Text);
                        komut.Parameters.AddWithValue("@email",textBox4.Text);
                        komut.Parameters.AddWithValue("@password", textBox5.Text);
                        con.Open();
                        komut.ExecuteNonQuery();
                        MessageBox.Show("Kaydınız Başarıyla Oluşturulmuştır");
                        textBox1.Clear();
                        textBox2.Clear();
                        maskedTextBox1.Clear();
                        textBox4.Clear();
                        textBox5.Clear();
                        textBox6.Clear();
                        con.Close();
                    }
                    else
                    {
                        MessageBox.Show("Şifreler Uyuşmuyor");
                        textBox5.Clear();
                        textBox6.Clear();
                    }
                }
                
            }
          
         

        }
        private bool isemailCntrl(string email)
        {
            Regex check = new Regex(@"^\w+[\w-\.]+\@\w{5}\.[a-z]{2,3}$");
            bool valid = false;
            valid = check.IsMatch(email);
            if (valid == true)
            {
                return true;
            }
            else
            {
                MessageBox.Show("Yazdığınız metin e-posta türünde değil");
                return false;
            }
        }
        private bool isnameCntrl(string n)
        {
            Regex check = new Regex(@"^[a-zA-ZğĞüÜşŞıİöÖçÇ]+$");
            bool valid = false;
            valid = check.IsMatch(n);
            if (valid == true)
            {
                return true;
            }
            else
            {
                MessageBox.Show("Yazdığınız isim veya soyisim rakam içermemelidir");
                return false;
            }
        }
    }
}
