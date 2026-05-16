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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PersonelTakip
{
    public partial class Form1 : Form
    {
        SqlConnection baglanti = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='C:\Users\beyza\source\repos\PersonelTakip\PersonelTakip\Database1.mdf';Integrated Security=True");
        public Form1()
        {

            InitializeComponent();

        }
        void DepartmanGetir()
        {
            if (baglanti.State == ConnectionState.Closed) baglanti.Open();

            SqlCommand komut = new SqlCommand("SELECT * FROM Departmanlar", baglanti);
            SqlDataReader dr = komut.ExecuteReader();

            while (dr.Read())
            {
               
                cmbDepartman.Items.Add(dr["DepAd"].ToString());
            }

            baglanti.Close();
        }
        void Listele()
        {
            try
            {
             
                if (baglanti.State == ConnectionState.Closed)
                    baglanti.Open();

               
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Personeller", baglanti);

           
                DataTable dt = new DataTable();
                da.Fill(dt); 

               
                dataGridView1.DataSource = dt;

                baglanti.Close(); 
            }
            catch (Exception hata)
            {
                MessageBox.Show("Listeleme sırasında bir hata oluştu: " + hata.Message);
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtTC.Text.Length != 11 || !long.TryParse(txtTC.Text, out _))
                {
                    MessageBox.Show("TC Kimlik Numarası tam olarak 11 haneli ve sadece rakamlardan oluşmalıdır!", "Hatalı Giriş", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; 
                }

                if (baglanti.State == ConnectionState.Closed) baglanti.Open();

               
                SqlCommand komut = new SqlCommand("INSERT INTO Personeller (Ad, Soyad, TCNo, Maas, Departman) VALUES (@p1, @p2, @p3, @p4, @p5)", baglanti);

                komut.Parameters.AddWithValue("@p1", txtAd.Text);
                komut.Parameters.AddWithValue("@p2", txtSoyad.Text);
                komut.Parameters.AddWithValue("@p3", txtTC.Text);
                komut.Parameters.AddWithValue("@p4", txtMaas.Text);

               
                komut.Parameters.AddWithValue("@p5", cmbDepartman.Text);

                komut.ExecuteNonQuery();
                baglanti.Close();

                MessageBox.Show("eklendi");
                Listele(); 
            }
            catch (Exception hata)
            {
                MessageBox.Show("Ekleme hatası: " + hata.Message);
                if (baglanti.State == ConnectionState.Open) baglanti.Close();
            }
        }
        private void btnListele_Click(object sender, EventArgs e)
        {
            Listele();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {

                if (dataGridView1.CurrentRow == null)
                {
                    MessageBox.Show("Lütfen önce tablodan güncellenecek personeli seçin!");
                    return;
                }


                int seciliId = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);

                if (baglanti.State == ConnectionState.Closed) baglanti.Open();


                SqlCommand komut = new SqlCommand("UPDATE Personeller SET Ad=@p1, Soyad=@p2, TCNo=@p3, Maas=@p4, Departman=@p5 WHERE Id=@p6", baglanti);


                komut.Parameters.AddWithValue("@p1", txtAd.Text);
                komut.Parameters.AddWithValue("@p2", txtSoyad.Text);
                komut.Parameters.AddWithValue("@p3", txtTC.Text);


                decimal maas = 0;
                if (!string.IsNullOrEmpty(txtMaas.Text)) maas = Convert.ToDecimal(txtMaas.Text.Replace(".", ","));
                komut.Parameters.AddWithValue("@p4", maas);


                komut.Parameters.AddWithValue("@p5", cmbDepartman.Text);


                komut.Parameters.AddWithValue("@p6", seciliId);


                komut.ExecuteNonQuery();
                baglanti.Close();

                MessageBox.Show("Personel ve departman bilgisi başarıyla güncellendi! 🚀");

                Listele();
            }
            catch (Exception hata)
            {
                MessageBox.Show("Sistemsel bir hata oluştu: " + hata.Message);
                if (baglanti.State == ConnectionState.Open) baglanti.Close();
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            DepartmanGetir();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow == null)
                {
                    MessageBox.Show("Lütfen silmek istediğiniz personeli tablodan seçin!");
                    return;
                }

              
                int seciliId = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                string personelAd = dataGridView1.CurrentRow.Cells[1].Value.ToString();

                
                DialogResult onay = MessageBox.Show(personelAd + " isimli personeli silmek istediğinize emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (onay == DialogResult.Yes)
                {
                    if (baglanti.State == ConnectionState.Closed) baglanti.Open();

                    
                    SqlCommand komut = new SqlCommand("DELETE FROM Personeller WHERE Id=@p1", baglanti);
                    komut.Parameters.AddWithValue("@p1", seciliId);

                    komut.ExecuteNonQuery();
                    baglanti.Close();

                    MessageBox.Show("Kayıt başarıyla silindi! 🗑️");
                    Listele();
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show("Hata: " + hata.Message);
                if (baglanti.State == ConnectionState.Open) baglanti.Close();
            }
         
        }
    }
}