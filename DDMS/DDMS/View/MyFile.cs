using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DDMS.Controller;
using DDMS.Model;

namespace DDMS.View
{
    public partial class MyFile : Form
    {
        FileController fileController = new FileController();
        public MyFile()
        {
            InitializeComponent();
        }

        private void load_data()
        {
           
            //Files files =new Files();
            DataTable dt = fileController.Myfile(User.userId);
            dataGridView1.DataSource = dt;
            delete();
            download();
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
           // dataGridView1.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView1.BackgroundColor = Color.White;

            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].ReadOnly = true;
          
            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[1].Width = 100;
            dataGridView1.Columns[2].Width = 80;
            dataGridView1.Columns[3].Width = 80;
           
        }

        private void delete()
        {
            DataGridViewLinkColumn delete = new DataGridViewLinkColumn();
            delete.Text = "Delete";
            delete.UseColumnTextForLinkValue = true;
            dataGridView1.Columns.Add(delete);
        }

        private void download()
        {
            DataGridViewLinkColumn download = new DataGridViewLinkColumn();
            download.Text = "Download";
            download.UseColumnTextForLinkValue = true;
            dataGridView1.Columns.Add(download);
        }
        private void btnProfile_Click(object sender, EventArgs e)
        {
            Profile profile = new Profile();
            this.Hide();
            profile.Show();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
         //   User.userId=null;
            Login login = new Login();
            this.Hide();
            login.Show();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MyFile_Load(object sender, EventArgs e)
        {
            load_data();
        }

        private void btnLogout_Click_1(object sender, EventArgs e)
        {
            User.userId = null;
            Login login = new Login();
            this.Hide();
            login.Show();
        }

        

        private void btnOtherFile_Click(object sender, EventArgs e)
        {
            this.Hide();
            OthersFile othersFile = new OthersFile();
            othersFile.Show();
        }

        private void btnMyFile_Click(object sender, EventArgs e)
        {

        }

        private void btnViewFile_Click(object sender, EventArgs e)
        {
            if (paneldropdown.Visible == false)
            {
                paneldropdown.Visible = true;
            }

            else
            {
                paneldropdown.Visible = false;
            }
        }

        private void btn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Upload upload = new Upload();
            upload.Show();
        }

    

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Minimize_Click(object sender, EventArgs e)
        {

        }

       

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = String.Format("name LIKE '{0}%'", txtSearch.Text);
        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                String id = dataGridView1.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString();

                if (MessageBox.Show("Do you want to delete this file?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    fileController.delete(Convert.ToInt32(id));
                    dataGridView1.Rows.RemoveAt(e.RowIndex);

                }
            }

            if (e.ColumnIndex == 3)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                //saveFileDialog.InitialDirectory = @"D:\";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    String source = @"D:\Image\" + dataGridView1.Rows[e.RowIndex].Cells["name"].FormattedValue.ToString(); ;
                    String destination = Path.GetFullPath(saveFileDialog.FileName);
                    File.Copy(source, destination);
                    MessageBox.Show("Download successfully");
                }
            }
        }
    }
}
