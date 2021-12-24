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
    public partial class Form1 : Form
    {
        SqlConnection baglanti = new SqlConnection("Server = .;Database=Pastane;uid=sa;pwd=11072010");
        public Form1()
        {
            InitializeComponent();
        }
        



        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void comboSecim_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboSecim.SelectedItem == "Ürünler")
            {
                Urunler git = new Urunler();
                git.Show();
                this.Hide();
            }
            if (comboSecim.SelectedItem == "Satıcılar")
            {
                Saticilar git = new Saticilar();
                git.Show();
                this.Hide();
            }
            if (comboSecim.SelectedItem == "Siparişler")
            {
                Siparisler git = new Siparisler();
                git.Show();
                this.Hide();

            }
            if (comboSecim.SelectedItem == "Müşteriler")
            {
                Musteriler git = new Musteriler();
                git.Show();
                this.Hide();
            }
        }
    }
}
