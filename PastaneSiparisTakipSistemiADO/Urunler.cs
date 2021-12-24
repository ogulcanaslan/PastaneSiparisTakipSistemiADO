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
    public partial class Urunler : Form
    {
        SqlConnection baglanti = new SqlConnection("Server = .;Database=Pastane;uid=sa;pwd=11072010");
        public Urunler()
        {
            InitializeComponent();
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

        private void anaSayfaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 git = new Form1();
            git.Show();
            this.Hide();
        }

        private void Urunler_Load(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secim = dataGridView1.SelectedCells[0].RowIndex;
            string UrunNo = dataGridView1.Rows[secim].Cells[0].Value.ToString();
            string UrunAdi = dataGridView1.Rows[secim].Cells[1].Value.ToString();
            string UrunFiyati = dataGridView1.Rows[secim].Cells[2].Value.ToString();
            string UrunSKT = dataGridView1.Rows[secim].Cells[3].Value.ToString();
            string UrunUretimTarihi = dataGridView1.Rows[secim].Cells[4].Value.ToString();
            string UrunResim = dataGridView1.Rows[secim].Cells[5].Value.ToString();
            string SaticiNo = dataGridView1.Rows[secim].Cells[6].Value.ToString();


            textUrunNo.Text = UrunNo;
            textUrunAdi.Text = UrunAdi;
            textUrunFiyati.Text = UrunFiyati;
            dateTimeSKT.Text = UrunSKT;
            dateTimeUretimTarihi.Text = UrunUretimTarihi;
            textResim.Text = UrunResim;
            textSaticiNo.Text = SaticiNo;

        }

        private void btnListele_Click(object sender, EventArgs e)
        {
            Listele("SELECT * FROM Urunler");
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand command = new SqlCommand("INSERT INTO Urunler(UAdi,UFiyat,USonKulTar,UUretimTarihi,UResim,SaticiNo)values(@UAdi,@UFiyat,@USonKulTar,@UUretimTarihi,@UResim,@SaticiNo)",baglanti);
            command.Parameters.AddWithValue("@UAdi", textUrunAdi.Text);
            command.Parameters.AddWithValue("@UFiyat", textUrunFiyati.Text);
            command.Parameters.AddWithValue("@USonKulTar", dateTimeSKT.Text);/////////////////////////////////VALUE
            command.Parameters.AddWithValue("@UUretimTarihi", dateTimeUretimTarihi.Text);/////////////////////VALUE
            command.Parameters.AddWithValue("@UResim", textResim.Text);
            command.Parameters.AddWithValue("@SaticiNo", textSaticiNo.Text);
            command.ExecuteNonQuery();
            baglanti.Close();
            Listele("SELECT * FROM Urunler");

        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand command = new SqlCommand("DELETE from Urunler where UNo = @UNo", baglanti);
            command.Parameters.AddWithValue("@UNo", textUrunNo.Text);
            command.ExecuteNonQuery();
            baglanti.Close();
            Listele("SELECT * FROM Urunler");
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand command = new SqlCommand("Update Urunler set UAdi='" + textUrunAdi.Text.ToString() + "',UFiyat='" + textUrunFiyati.Text.ToString() + "',USonKulTar='" + dateTimeSKT.Text.ToString() + "',UUretimTarihi='" + dateTimeUretimTarihi.Text.ToString() + "',UResim='" + textResim.Text.ToString() + "',SaticiNo='" + textSaticiNo.Text.ToString() + "'where UNo='" + textUrunNo.Text.ToString() + "'", baglanti);
            command.ExecuteNonQuery();
            Listele("Select * from Urunler");
            baglanti.Close();
        }

        private void btnResim_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            pictureBox1.ImageLocation = openFileDialog1.FileName;
            textResim.Text = openFileDialog1.FileName;
        }

        private void btnAra_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand command = new SqlCommand("Select * from Urunler where UAdi like '%" + textUrunAdi.Text + "%'", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            baglanti.Close();
        }
    }
}
