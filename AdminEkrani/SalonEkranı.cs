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
    public partial class SalonEkranı : Form
    {
        SqlConnection con = VeritabaniIslemleri.BaglantiDondur();
        public SalonEkranı()
        {
            InitializeComponent();
            ComboKutusu();
            Combo2Kutusu();
            label3.Text = adminBilgileri.isim;
        }

        private void SalonEkranı_Load(object sender, EventArgs e)
        {

        }
        void ComboKutusu()
        {
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            
            for(int i = 30; i < 100; i++)
            {
            comboBox1.Items.Add(i.ToString());
            }
        }
        void Combo2Kutusu()
        {
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            con.Open();
            SqlCommand komut1 = new SqlCommand("SELECT salonAdi from Salonlar",con);
            SqlDataReader rdr = komut1.ExecuteReader();
            while(rdr.Read())
            {
                comboBox2.Items.Add(rdr["salonAdi"].ToString());
            }
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "" || comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Lütfen Alanları Doldurun");

            }
            else
            {
                con.Open();
                SqlCommand komut = new SqlCommand("INSERT INTO Salonlar(salonAdi,koltukSayisi)VALUES(@salonAdi,@koltukSayisi)",con);           
                komut.Parameters.AddWithValue("@salonAdi", textBox1.Text);
                komut.Parameters.AddWithValue("@koltukSayisi", comboBox1.SelectedItem);
                komut.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Salon Eklendi");
                textBox1.Clear();
                comboBox1.SelectedIndex = -1;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == -1)
            {
                MessageBox.Show("Silmek İstediğiniz Salonu Seçin");
            }
            else
            {
                con.Open();
                SqlCommand komut2 = new SqlCommand("DELETE FROM Salonlar WHERE = '" + comboBox2.SelectedItem.ToString() + "'", con);
                komut2.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Salon Başarıyla Silindi");
                comboBox2.SelectedIndex = -1;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            adminsayfa ad = new adminsayfa();
            this.Hide();
            ad.ShowDialog();
            this.Dispose();
        }
    }
}
