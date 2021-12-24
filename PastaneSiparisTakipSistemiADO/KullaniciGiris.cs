using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PastaneSiparisTakipSistemiADO
{
    public partial class KullaniciGiris : Form
    {
        public KullaniciGiris()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Server = .;Database=Pastane;uid=sa;pwd=11072010");
        private void KullaniciGiris_Load(object sender, EventArgs e)
        {
            grpUyeOl.Visible = false;

        }

        private void checkBoxUyeOl_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxUyeOl.Checked==true)
            {
                grpUyeOl.Visible = true;
            }
            else
            {
                grpUyeOl.Visible = false;
            }
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {

            
            SqlCommand command = new SqlCommand("select * from Kullanicilar where KullaniciAdi =@kad and KullaniciSifre=@sifre ", baglanti);

            command.Parameters.AddWithValue("@kad", textGirisKullAdi.Text);
            command.Parameters.AddWithValue("@sifre", textGirisSifre.Text);
            //command.ExecuteNonQuery();
            baglanti.Open();
            SqlDataReader dr;

            dr = command.ExecuteReader();
            if (dr.Read())
            {
                MessageBox.Show("Terbrikler! Başarılı bir Şekilde Giriş Yaptınız");
                Form1 go = new Form1();
                go.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Kullanıcı Adı ve Şifrenizi Kontrol Ediniz");
                textGirisKullAdi.Clear();
                textGirisSifre.Clear();
            }
        }

        private void btnUyeOl_Click(object sender, EventArgs e)
        {
            if (textUyeOlSifre.Text==textUyeOlSifreTekrar.Text)
            {
                baglanti.Open();
                SqlCommand command = new SqlCommand("INSERT INTO Kullanicilar(KullaniciAdi,KullaniciSifre)values(@KullaniciAdi,@KullaniciSifre)", baglanti);
                command.Parameters.AddWithValue("@KullaniciAdi", textUyeOlKullAdi.Text);
                command.Parameters.AddWithValue("@KullaniciSifre", textUyeOlSifre.Text);
                command.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Başarıyla Kayıt Oldunuz");
            }
            else
            {
                MessageBox.Show("Şifreler Uyumsuz");
            }
            
        }
    }
}
