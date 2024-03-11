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

namespace NotKayıtSistemi
{
    public partial class FrmÖğretmenDetay : Form
    {
        public FrmÖğretmenDetay()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-GizeM\SQLEXPRESS01;Initial Catalog=DbNotKayit;Integrated Security=True");
        private void FrmÖğretmenDetay_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dbNotKayitDataSet.TBLDERS' table. You can move, or remove it, as needed.
            this.tBLDERSTableAdapter.Fill(this.dbNotKayitDataSet.TBLDERS);

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut=new SqlCommand("insert into TBLDERS (OGRNUMARA,OGRAD,OGRSOYAD) values (@p1,@p2,@p3)",baglanti);
            komut.Parameters.AddWithValue("@p1", mskNumara.Text);
            komut.Parameters.AddWithValue("@p2", txtAd.Text);
            komut.Parameters.AddWithValue("@p3", txtSoyad.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Öğrenci Sisteme Eklendi");
            this.tBLDERSTableAdapter.Fill(this.dbNotKayitDataSet.TBLDERS);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            double ortalama,s1,s2,s3;
            string durum;
            s1 = Convert.ToDouble(txtSınav1.Text);
            s2 = Convert.ToDouble(txtSınav2.Text);
            s3 = Convert.ToDouble(txtSınav3.Text);
            
            ortalama = (s1 + s2 + s3) / 3;
            LblOrtalama.Text=ortalama.ToString();
            if (ortalama >= 50)
            {
                durum = "True";
            }
            else
            {
                durum = "False";
            }

            baglanti.Open();
            SqlCommand komut = new SqlCommand("update TBLDERS set OGRS1=@P1,OGRS2=@P2,OGRS3=@P3,ORTALAMA=@P4,DURUM=@P5 WHERE OGRNUMARA=@P6", baglanti);
            komut.Parameters.AddWithValue("@P1", txtSınav1.Text);
            komut.Parameters.AddWithValue("@P2", txtSınav2.Text);
            komut.Parameters.AddWithValue("@P3", txtSınav3.Text);
            komut.Parameters.AddWithValue("@P4",decimal.Parse( LblOrtalama.Text));
            komut.Parameters.AddWithValue("@P5", durum);
            komut.Parameters.AddWithValue("@P6", mskNumara.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Öğrenci Notları Güncellendi.");
            this.tBLDERSTableAdapter.Fill(this.dbNotKayitDataSet.TBLDERS);

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen= dataGridView1.SelectedCells[0].RowIndex;
            mskNumara.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtAd.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            txtSoyad.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            txtSınav1.Text= dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            txtSınav2.Text= dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            txtSınav3.Text= dataGridView1.Rows[secilen].Cells[6].Value.ToString();
        }
    }
}
