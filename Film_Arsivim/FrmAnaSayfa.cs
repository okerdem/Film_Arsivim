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

namespace Film_Arsivim
{
    public partial class FrmAnaSayfa : Form
    {
        public FrmAnaSayfa()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-Q087NT7\SQLEXPRESS01;Initial Catalog=FilmArşivim;Integrated Security=True");

        private void FrmAnaSayfa_Load(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand komut1 = new SqlCommand("select KATEGORİ from TBLFİLMLER", con);
            SqlDataReader dr1 = komut1.ExecuteReader();
            while (dr1.Read())
            {
                kategorilerToolStripMenuItem.DropDownItems.Add(dr1[0].ToString());
            }
            con.Close();
        }

        private void listBoxFilmler_SelectedIndexChanged(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand komut3 = new SqlCommand("select link from tblfilmler where ad=@ad", con);
            komut3.Parameters.AddWithValue("@ad", listBoxFilmler.SelectedItem.ToString());
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                webBrowser1.Navigate(dr3[0].ToString());
            }
            con.Close();
        }

        private void kategorilerToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            listBoxFilmler.Items.Clear();
            con.Open();
            SqlCommand komut2 = new SqlCommand("select ad from tblfilmler where kategori=@kategori", con);
            komut2.Parameters.AddWithValue("@kategori", e.ClickedItem.Text);
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                listBoxFilmler.Items.Add(dr2[0].ToString());
            }
            con.Close();
        }
    }
}
