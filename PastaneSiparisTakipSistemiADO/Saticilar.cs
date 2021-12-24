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
    public partial class Saticilar : Form
    {
        SqlConnection baglanti = new SqlConnection("Server = .;Database=Pastane;uid=sa;pwd=11072010");
        public Saticilar()
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

        private void btnListele_Click(object sender, EventArgs e)
        {
            Listele("SELECT * FROM Saticilar");
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secim = dataGridView1.SelectedCells[0].RowIndex;
            string SaticiNo = dataGridView1.Rows[secim].Cells[0].Value.ToString();
            string SaticiAdi = dataGridView1.Rows[secim].Cells[1].Value.ToString();
            string SaticiAdres = dataGridView1.Rows[secim].Cells[2].Value.ToString();
            string SaticiIl = dataGridView1.Rows[secim].Cells[3].Value.ToString();
            string SaticiIlce = dataGridView1.Rows[secim].Cells[4].Value.ToString();
            

            comboSaticiNo.Text = SaticiNo;
            textSaticiAdi.Text = SaticiAdi;
            richTextSaticiAdres.Text = SaticiAdres;
            textSaticiIl.Text = SaticiIl;
            textSaticiIlce.Text = SaticiIlce;
            
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand command = new SqlCommand("INSERT INTO Saticilar(SAdi,SAdres,SIl,SIlce)values(@SAdi,@SAdres,@SIl,@SIlce)", baglanti);
            command.Parameters.AddWithValue("@SAdi", textSaticiAdi.Text);
            command.Parameters.AddWithValue("@SAdres", richTextSaticiAdres.Text);
            command.Parameters.AddWithValue("@SIl", textSaticiIl.Text);
            command.Parameters.AddWithValue("@SIlce", textSaticiIlce.Text);
            command.ExecuteNonQuery();
            baglanti.Close();
            Listele("SELECT * FROM Urunler");
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand command = new SqlCommand("DELETE from Saticilar where SNo = @SNo", baglanti);
            command.Parameters.AddWithValue("@SNo", comboSaticiNo.Text);
            command.ExecuteNonQuery();
            baglanti.Close();
            Listele("SELECT * FROM Urunler");
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand command = new SqlCommand("Update Saticilar set SAdi='" + textSaticiAdi.Text.ToString() + "',SAdres='" + richTextSaticiAdres.Text.ToString() + "',SIl='" + textSaticiIl.Text.ToString() + "',SIlce='" + textSaticiIlce.Text.ToString() +  "'where SNo='" + comboSaticiNo.Text.ToString() + "'", baglanti);
            command.ExecuteNonQuery();
            Listele("Select * from Saticilar");
            baglanti.Close();
        }

        private void btnAra_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand command = new SqlCommand("Select * from Saticilar where SAdi like '%" + textSaticiAdi.Text + "%'", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            baglanti.Close();
        }

        private void Saticilar_Load(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Select SNo from Saticilar");
            komut.Connection = baglanti;


            SqlDataReader dr;
            baglanti.Open();
            dr = komut.ExecuteReader();
            while (dr.Read())
            {

                comboSaticiNo.Items.Add(dr["SNo"]);
            }
            baglanti.Close();
        }
    }
}
