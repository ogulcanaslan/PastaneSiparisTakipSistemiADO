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
    
    public partial class Musteriler : Form
    {
        SqlConnection baglanti = new SqlConnection("Server = .;Database=Pastane;uid=sa;pwd=11072010");
        public Musteriler()
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secim = dataGridView1.SelectedCells[0].RowIndex;
            string MNo = dataGridView1.Rows[secim].Cells[0].Value.ToString();
            string MAdres = dataGridView1.Rows[secim].Cells[1].Value.ToString();
            string MIl = dataGridView1.Rows[secim].Cells[2].Value.ToString();
            string MIlce = dataGridView1.Rows[secim].Cells[3].Value.ToString();
            string SiparisNo = dataGridView1.Rows[secim].Cells[4].Value.ToString();
            


            textMNo.Text = MNo;
            richTextMAdres.Text = MAdres;
            textMIl.Text = MIl;
            textMIlce.Text = MIlce;
            textMSiparisNo.Text = SiparisNo;
            
        }

        private void btnListele_Click(object sender, EventArgs e)
        {
            Listele("SELECT * FROM Musteriler");
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand command = new SqlCommand("INSERT INTO Musteriler(MAdres,MIl,MIlce,SiparisNo)values(@MAdres,@MIl,@MIlce,@SiparisNo)", baglanti);
            command.Parameters.AddWithValue("@MAdres", richTextMAdres.Text);
            command.Parameters.AddWithValue("@MIl", textMIl.Text);
            command.Parameters.AddWithValue("@MIlce", textMIlce.Text);
            command.Parameters.AddWithValue("@SiparisNo", textMSiparisNo.Text);
            
            command.ExecuteNonQuery();
            baglanti.Close();
            Listele("SELECT * FROM Musteriler");
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand command = new SqlCommand("DELETE from Musteriler where MNo = @MNo", baglanti);
            command.Parameters.AddWithValue("@MNo", textMNo.Text);
            command.ExecuteNonQuery();
            baglanti.Close();
            Listele("SELECT * FROM Musteriler");
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand command = new SqlCommand("Update Musteriler set MAdres='" + richTextMAdres.Text.ToString() + "',MIl='" + textMIl.Text.ToString() + "',MIlce='" + textMIlce.Text.ToString() + "',SiparisNo='" + textMSiparisNo.Text.ToString()  + "'where MNo='" + textMNo.Text.ToString() + "'", baglanti);
            command.ExecuteNonQuery();
            Listele("Select * from Musteriler");
            baglanti.Close();
        }

        private void btnAra_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand command = new SqlCommand("Select * from Musteriler where MNo like '%" + textMNo.Text + "%'", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            baglanti.Close();
        }

        private void btnMusteriSirala_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand command = new SqlCommand("select * from Musteriler order by MIl asc)", baglanti);
        }
    }
}
