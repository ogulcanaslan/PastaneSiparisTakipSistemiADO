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
    public partial class Siparisler : Form
    {
        SqlConnection baglanti = new SqlConnection("Server = .;Database=Pastane;uid=sa;pwd=11072010");
        public Siparisler()
        {
            InitializeComponent();
        }

        private void Siparisler_Load(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Select UNo from Urunler");
            komut.Connection = baglanti;


            SqlDataReader dr;
            baglanti.Open();
            dr = komut.ExecuteReader();
            while (dr.Read())
            {

                comboUrunNo.Items.Add(dr["UNo"]);
            }
            baglanti.Close();
        }
        public void Listele(string ulas)
        {
            SqlDataAdapter goruntule = new SqlDataAdapter(ulas, baglanti);
            DataSet doldur = new DataSet();
            goruntule.Fill(doldur);
            dataGridView1.DataSource = doldur.Tables[0];

            //DataTable doldur = new DataTable();
            //goruntule.Fill(doldur);
            //dataGridView1.DataSource = doldur,
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secim = dataGridView1.SelectedCells[0].RowIndex;
            string SiparisNo = dataGridView1.Rows[secim].Cells[0].Value.ToString();
            string SiparisAdi = dataGridView1.Rows[secim].Cells[1].Value.ToString();
            string SiparisAdres = dataGridView1.Rows[secim].Cells[2].Value.ToString();
            string SiparisAdet = dataGridView1.Rows[secim].Cells[3].Value.ToString();
            string SiparisFiyat = dataGridView1.Rows[secim].Cells[4].Value.ToString();
            string UrunNo = dataGridView1.Rows[secim].Cells[5].Value.ToString();
            //string SiparisTutar = dataGridView1.Rows[secim].Cells[5].Value.ToString();
            string SiparisDurum = dataGridView1.Rows[secim].Cells[7].Value.ToString();



            textSiparisNo.Text = SiparisNo;
            textSiparisAdi.Text = SiparisAdi;
            textSiparisAdres.Text = SiparisAdres;
            textSiparisAdet.Text = SiparisAdet;
            textSiparisFiyat.Text = SiparisFiyat;
            comboUrunNo.Text = UrunNo;
            //textSiparisTutar.Text = SiparisTutar;
            comboSiparisDurum.Text = SiparisDurum;
            baglanti.Open();
            int adet = Convert.ToInt32(textSiparisAdet.Text);
            decimal siparisFiyat = Convert.ToDecimal(textSiparisFiyat.Text);
            decimal toplamTutar = adet * siparisFiyat;
            textSiparisTutar.Text = toplamTutar.ToString();


            SqlCommand command = new SqlCommand("Update Siparis set SiparisTutar='" + textSiparisTutar.Text.ToString() + "'Where SiparisNo='" + textSiparisNo.Text.ToString() + "'", baglanti);
            command.Parameters.AddWithValue("@SiparisTutar", textSiparisTutar.Text);
            command.ExecuteNonQuery();

            baglanti.Close();
            Listele("Select * from Siparis");




        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnListele_Click(object sender, EventArgs e)
        {
            Listele("SELECT * FROM Siparis");
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand command = new SqlCommand("INSERT INTO Siparis(SiparisAdi,SiparisAdres,SiparisAdet,SiparisFiyat,UrunNo,SiparisTutar,SiparisDurum)values(@SiparisAdi,@SiparisAdres,@SiparisAdet,@SiparisFiyat,@UrunNo,@SiparisTutar,@SiparisDurum)", baglanti);
            command.Parameters.AddWithValue("@SiparisAdi", textSiparisAdi.Text);
            command.Parameters.AddWithValue("@SiparisAdres", textSiparisAdres.Text);
            command.Parameters.AddWithValue("@SiparisAdet", textSiparisAdet.Text);
            command.Parameters.AddWithValue("@SiparisFiyat", textSiparisFiyat.Text);
            command.Parameters.AddWithValue("@UrunNo", comboUrunNo.Text);
            command.Parameters.AddWithValue("@SiparisTutar", textSiparisTutar.Text);
            command.Parameters.AddWithValue("@SiparisDurum", comboSiparisDurum.Text);
            command.ExecuteNonQuery();
            baglanti.Close();
            Listele("SELECT * FROM Siparis");
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand command = new SqlCommand("DELETE from Siparis where SiparisNo = @SiparisNo", baglanti);
            command.Parameters.AddWithValue("@SiparisNo", textSiparisNo.Text);
            command.ExecuteNonQuery();
            baglanti.Close();
            Listele("SELECT * FROM Siparis");
        }

        private void btnAra_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand command = new SqlCommand("Select * from Siparis where SiparisAdi like '%" + textSiparisAdi.Text + "%'", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            baglanti.Close();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand command = new SqlCommand("Update Siparis set SiparisAdi='" + textSiparisAdi.Text.ToString() + "',SiparisAdres='" + textSiparisAdres.Text.ToString() + "',SiparisAdet='" + textSiparisAdet.Text.ToString() + "',SiparisFiyat='" + textSiparisFiyat.Text.ToString() + "',UrunNo='" + comboUrunNo.Text.ToString() + "',SiparisTutar='" + textSiparisTutar.Text.ToString() + "',SiparisDurum='" + comboSiparisDurum.Text.ToString() + "'where SiparisNo='" + comboUrunNo.Text.ToString() + "'", baglanti);
            command.ExecuteNonQuery();
            Listele("Select * from Siparis");
            baglanti.Close();
        }

        private void anaSayfaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }
    }
}
