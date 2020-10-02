using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using DDMS.Controller;
using DDMS.Model;
using EasyEncrypt2;

namespace DDMS.View
{
    public partial class Register : Form
    {
        UserController userController= new UserController();
        int TogMove;
        int MValX, MValY;

        String ext; //to get extension of image
        public Register()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
           if (String.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("Name is required", " Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           else if(String.IsNullOrEmpty(txtEmail.Text))
            {
                MessageBox.Show("Email is required", " Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           else if(String.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Password is required", " Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           else if(string.IsNullOrEmpty(comboBox.Text))
            {
                MessageBox.Show("Please select a department", " Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (profile.Image == null)
            {
                MessageBox.Show("Please select image for profile", " Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                //Upload image
                String fileName = $@"{DateTime.Now.Ticks}" + ext;
                String location = "D:\\Image";
                String pathString = System.IO.Path.Combine(location, fileName);
                Image image = profile.Image;
                image.Save(pathString);
                String name = txtName.Text;
                String email = txtEmail.Text;
                String password = txtPassword.Text;
                String hashedPassword = EasyEncryption.MD5.ComputeMD5Hash(password);           
                //string hashedPassword = ASCIIEncoding.GetString(md5data);
              String department = comboBox.SelectedItem.ToString();
                String role = "";
                bool ischecked = radioBtnOfficer.Checked;
                if(ischecked)
                {
                  role = radioBtnOfficer.Text;
                }
                else
                {
                    role = radioBtnStaff.Text;
                }
                User user = new User(name,hashedPassword,department,role,fileName,email);
                userController.Register(user);
                MessageBox.Show("Registered Successfully");
            
             
                Reload();
            }
            
          
        }

        private void Reload()
        {
            profile.Image=null;
            txtName.ResetText();
            txtEmail.ResetText();
            txtPassword.ResetText();
            comboBox.SelectedIndex=-1;

        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            TogMove = 1;
            MValX = e.X;
            MValY = e.Y;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (TogMove == 1)
            {
                this.SetDesktopLocation(MousePosition.X - MValX, MousePosition.Y - MValY);
            }
        }

        private void profile_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            profile = sender as PictureBox;
            if(profile!=null)
            {
                op.Filter = "(*.jpg; *.jpeg; *.png)| *.jpg; *.jpeg; *.png";
                if(op.ShowDialog()==DialogResult.OK)
                {
                    profile.Image = Image.FromFile(op.FileName);
                    profile.SizeMode = PictureBoxSizeMode.StretchImage;
                    ext = Path.GetExtension(op.FileName);
                }
            }
        }

    
     

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Minimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.Show();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtEmail_Leave(object sender, EventArgs e)
        {
           bool res = UserController.checkEmail(txtEmail.Text);
            var regex = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
            bool isValid = Regex.IsMatch(txtEmail.Text, regex, RegexOptions.IgnoreCase);

             if (isValid == false)
            {
                errorProvider1.SetError(txtEmail, "Invalid email");
            }
            else if (res == true)
            {
                errorProvider1.SetError(txtEmail, "Email already used");
            }
           
            else
            {
                errorProvider1.Clear();
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            TogMove = 0;
        }
    }
}
