using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Create_File_or_Directory
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonFile_Click(object sender, EventArgs e)
        {
            string path = textBox1.Text;

            if (!File.Exists(path))
            {
                File.Create(path);
                MessageBox.Show("File Created");
            } else
            {
                MessageBox.Show("File Already Exists");
            }
        }

        private void buttonDirectory_Click_1(object sender, EventArgs e)
        {
            string path = textBox1.Text;
            if(!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                MessageBox.Show("Directory Created");
            }
            else
            {
                MessageBox.Show("Directory Already Exists");
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button_rm_file_Click(object sender, EventArgs e)
        {
            string filePath = richTextBox1.Text;
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                MessageBox.Show("File Deleted");
            } else
            {
                MessageBox.Show("File Does Not Exist");
            }
        }

        private void button_rm_directory_Click(object sender, EventArgs e)
        {
            string filePath = richTextBox_rm_directory.Text;
            if (Directory.Exists(filePath))
            {
                Directory.Delete(filePath);
                MessageBox.Show("Directory Deleted");
            }
            else
            {
                MessageBox.Show("Directory Does Not Exist");
            }
        }
    }
}
