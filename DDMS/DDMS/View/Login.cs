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
using DDMS.View;

namespace DDMS
{
    public partial class Login : Form
    {
        int TogMove;
        int MValX, MValY;
       // User user = new User();
        UserController userController = new UserController();
        public Login()
        {
            InitializeComponent();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            TogMove = 1;
            MValX = e.X;
            MValY = e.Y;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if(TogMove==1)
            {
                this.SetDesktopLocation(MousePosition.X - MValX, MousePosition.Y - MValY);
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


        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                String email = txtEmail.Text;
                String password = txtPassowrd.Text;
                
                String hashedPassword = EasyEncryption.MD5.ComputeMD5Hash(password);
                bool check = userController.Login(email, hashedPassword);
                //  MessageBox.Show(user.email);
                if (check == true)
                {
                    MessageBox.Show("Login successful");
                    this.Hide();
                    Profile profile = new Profile();
                    profile.Show();
                  
                }
                else
                {
                    MessageBox.Show("login failed");
                }
            }
            catch(Exception ex)
            { MessageBox.Show(ex.Message); }
          
        }

        private void linkRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Register register = new Register();
            register.Show();
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            TogMove = 0;
        }
    }
}
