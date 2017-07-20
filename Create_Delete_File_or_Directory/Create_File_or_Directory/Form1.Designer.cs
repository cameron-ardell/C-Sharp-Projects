namespace Create_File_or_Directory
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonFile = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.buttonDirectory = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button_rm_file = new System.Windows.Forms.Button();
            this.richTextBox_rm_directory = new System.Windows.Forms.RichTextBox();
            this.button_rm_directory = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonFile
            // 
            this.buttonFile.Location = new System.Drawing.Point(64, 137);
            this.buttonFile.Name = "buttonFile";
            this.buttonFile.Size = new System.Drawing.Size(262, 150);
            this.buttonFile.TabIndex = 0;
            this.buttonFile.Text = "Create File";
            this.buttonFile.UseVisualStyleBackColor = true;
            this.buttonFile.Click += new System.EventHandler(this.buttonFile_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(64, 344);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(413, 50);
            this.richTextBox1.TabIndex = 3;
            this.richTextBox1.Text = "File path...";
            this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // buttonDirectory
            // 
            this.buttonDirectory.Location = new System.Drawing.Point(462, 137);
            this.buttonDirectory.Name = "buttonDirectory";
            this.buttonDirectory.Size = new System.Drawing.Size(243, 150);
            this.buttonDirectory.TabIndex = 4;
            this.buttonDirectory.Text = "Create Directory";
            this.buttonDirectory.UseVisualStyleBackColor = true;
            this.buttonDirectory.Click += new System.EventHandler(this.buttonDirectory_Click_1);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(64, 69);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(641, 35);
            this.textBox1.TabIndex = 5;
            // 
            // button_rm_file
            // 
            this.button_rm_file.Location = new System.Drawing.Point(493, 330);
            this.button_rm_file.Name = "button_rm_file";
            this.button_rm_file.Size = new System.Drawing.Size(212, 62);
            this.button_rm_file.TabIndex = 6;
            this.button_rm_file.Text = "rm file";
            this.button_rm_file.UseVisualStyleBackColor = true;
            this.button_rm_file.Click += new System.EventHandler(this.button_rm_file_Click);
            // 
            // richTextBox_rm_directory
            // 
            this.richTextBox_rm_directory.Location = new System.Drawing.Point(71, 450);
            this.richTextBox_rm_directory.Name = "richTextBox_rm_directory";
            this.richTextBox_rm_directory.Size = new System.Drawing.Size(405, 56);
            this.richTextBox_rm_directory.TabIndex = 7;
            this.richTextBox_rm_directory.Text = "";
            // 
            // button_rm_directory
            // 
            this.button_rm_directory.Location = new System.Drawing.Point(493, 454);
            this.button_rm_directory.Name = "button_rm_directory";
            this.button_rm_directory.Size = new System.Drawing.Size(211, 61);
            this.button_rm_directory.TabIndex = 8;
            this.button_rm_directory.Text = "rm directory";
            this.button_rm_directory.UseVisualStyleBackColor = true;
            this.button_rm_directory.Click += new System.EventHandler(this.button_rm_directory_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 666);
            this.Controls.Add(this.button_rm_directory);
            this.Controls.Add(this.richTextBox_rm_directory);
            this.Controls.Add(this.button_rm_file);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.buttonDirectory);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.buttonFile);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonFile;
        private System.Windows.Forms.Button buttonDirectory;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button_rm_file;
        private System.Windows.Forms.RichTextBox richTextBox_rm_directory;
        private System.Windows.Forms.Button button_rm_directory;
    }
}

