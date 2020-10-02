using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DDMS.Controller;
using DDMS.Model;

namespace DDMS.View
{
    public partial class Profile : Form
    {
        int TogMove;
        int MValX, MValY;
        public Profile()
        {
            InitializeComponent();
        }

        private void loadForm()
        {
            UserController userController = new UserController();
            DataTable dt = userController.Profile(User.userId);
            lblName.Text = "Name : "+(String)dt.Rows[0][1];
            lblEmail.Text="Email : "+ (String)dt.Rows[0][2];
            lblDepartment.Text = "Department : " + (String)dt.Rows[0][4];
            lblRole.Text = "Role : " + (String)dt.Rows[0][5];
            pictureBox1.ImageLocation = @"D:\Image\"+dt.Rows[0][6];
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnOtherFile_Click(object sender, EventArgs e)
        {

        }

        private void Minimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void paneldropdown_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            User.userId = null;
            Login login = new Login();
            this.Hide();
            login.Show();
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

        private void btnProfile_Click(object sender, EventArgs e)
        {

        }

        private void Profile_Load(object sender, EventArgs e)
        {
            loadForm();
        }

        private void panel1_Move(object sender, EventArgs e)
        {
            if (TogMove == 1)
            {
                this.SetDesktopLocation(MousePosition.X - MValX, MousePosition.Y - MValY);
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            TogMove = 1;
            MValX = e.X;
            MValY = e.Y;
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            TogMove = 0;
        }

        private void btnMyFile_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            MyFile myFile = new MyFile();
            myFile.Show();

        }

        private void btnOtherFile_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            OthersFile othersFile = new OthersFile();
            othersFile.Show();
        }

     

        private void btnUpload_Click(object sender, EventArgs e)
        {
            this.Hide();
            Upload upload = new Upload();
            upload.Show();
        }
    }
}
