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
    public partial class Upload : Form
    {
        String des;
        String source,fileName;
        BackgroundWorker worker = new BackgroundWorker();
        //Files files = new Files();
        FileController fileController = new FileController();
        public Upload()
        {
            InitializeComponent();
            customizeSideBar();
            worker.WorkerSupportsCancellation = true;
            worker.WorkerReportsProgress = true;

            worker.ProgressChanged += new ProgressChangedEventHandler( Worker_ProgressChanged);
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
        }

        private void customizeSideBar()
        {
            paneldropdown.Visible = false;
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("File uploaded");
        }

        void FileUpload(String source, String des)
        {
            FileStream fsOut = new FileStream(des, FileMode.Create);
            FileStream fsIn = new FileStream(source, FileMode.Open);
            byte[] bt = new byte[1048756];
            int ReadByte;

            while((ReadByte=fsIn.Read(bt,0,bt.Length))>0)
            {
                fsOut.Write(bt, 0, ReadByte);
                worker.ReportProgress((int)(fsIn.Position * 100 / fsIn.Length));
            }
            fsIn.Close();
            fsOut.Close();
        }
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            FileUpload(source, des); 
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
            lblProgress.Text = progressBar.Value.ToString() + "%";
        }

        private void Upload_Load(object sender, EventArgs e)
        {

        }

        private void btnFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if(openFileDialog.ShowDialog()==DialogResult.OK)
            {
                source = openFileDialog.FileName;
                lblFile.Text = Path.GetFileName(openFileDialog.FileName);
                des = Path.Combine("D:\\File",Path.GetFileName(txtFileName.Text+Path.GetExtension(openFileDialog.FileName)));
                fileName = txtFileName.Text + Path.GetExtension(openFileDialog.FileName);
            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                worker.RunWorkerAsync();
                

                fileController.upload(new Files(fileName,Convert.ToInt32 (User.userId)));
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void txtFileName_Leave(object sender, EventArgs e)
        {
            
           if( fileController.checkFileName(this.txtFileName.Text)==false)
            {
                errorProvider1.SetError(this.txtFileName, "Filename already exist");
            }
            else 
            {
                errorProvider1.Clear();
            }
           
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
        private void showDropDown()
        {
            paneldropdown.Visible = true;
        }
        private void button3_Click(object sender, EventArgs e)
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

        private void btnLogout_Click(object sender, EventArgs e)
        {
            User.userId = null;
            Login login = new Login();
            this.Hide();
            login.Show();
        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            Profile profile = new Profile();
            this.Hide();
            profile.Show();
        }

        private void btnMyFile_Click(object sender, EventArgs e)
        {
            MyFile myFile = new MyFile();
            this.Hide();
            myFile.Show();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
