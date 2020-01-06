namespace MLocalRun
{
    partial class GetGitRepo
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GetGitRepo));
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_gitVersion = new System.Windows.Forms.TextBox();
            this.txt_gitUsername = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.check_DoYouHaveGit = new System.Windows.Forms.CheckBox();
            this.lbl_chooseGit = new System.Windows.Forms.Label();
            this.btn_GitRepoBrowse = new System.Windows.Forms.Button();
            this.txt_gitRepoPath = new System.Windows.Forms.TextBox();
            this.panel_GitLogin = new System.Windows.Forms.Panel();
            this.cmbReleaseVersions = new System.Windows.Forms.ComboBox();
            this.lbl_repoError = new System.Windows.Forms.Label();
            this.txt_powershellOutput = new System.Windows.Forms.RichTextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.radioButton_No = new System.Windows.Forms.RadioButton();
            this.radioButton_Yes = new System.Windows.Forms.RadioButton();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox_Changeset = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.button_Changeset = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label_GitPleaseWait = new System.Windows.Forms.Label();
            this.progressBar_Changeset = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.panel_GitLogin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(45, 35);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(131, 24);
            this.label3.TabIndex = 5;
            this.label3.Text = "Git connection";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(53, 27);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 23);
            this.label4.TabIndex = 6;
            this.label4.Text = "Git branch:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(405, 29);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 18);
            this.label5.TabIndex = 7;
            this.label5.Text = "Username:";
            // 
            // txt_gitVersion
            // 
            this.txt_gitVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_gitVersion.Location = new System.Drawing.Point(49, 879);
            this.txt_gitVersion.Margin = new System.Windows.Forms.Padding(4);
            this.txt_gitVersion.Name = "txt_gitVersion";
            this.txt_gitVersion.Size = new System.Drawing.Size(165, 24);
            this.txt_gitVersion.TabIndex = 8;
            // 
            // txt_gitUsername
            // 
            this.txt_gitUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_gitUsername.Location = new System.Drawing.Point(503, 27);
            this.txt_gitUsername.Margin = new System.Windows.Forms.Padding(4);
            this.txt_gitUsername.Name = "txt_gitUsername";
            this.txt_gitUsername.Size = new System.Drawing.Size(177, 24);
            this.txt_gitUsername.TabIndex = 9;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.White;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(699, 22);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(90, 32);
            this.button2.TabIndex = 10;
            this.button2.Text = "Checkout";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // check_DoYouHaveGit
            // 
            this.check_DoYouHaveGit.AutoSize = true;
            this.check_DoYouHaveGit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.check_DoYouHaveGit.Location = new System.Drawing.Point(25, 37);
            this.check_DoYouHaveGit.Margin = new System.Windows.Forms.Padding(4);
            this.check_DoYouHaveGit.Name = "check_DoYouHaveGit";
            this.check_DoYouHaveGit.Size = new System.Drawing.Size(194, 22);
            this.check_DoYouHaveGit.TabIndex = 11;
            this.check_DoYouHaveGit.Text = "Do you have a git repo ? ";
            this.check_DoYouHaveGit.UseVisualStyleBackColor = true;
            this.check_DoYouHaveGit.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // lbl_chooseGit
            // 
            this.lbl_chooseGit.AutoSize = true;
            this.lbl_chooseGit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_chooseGit.Location = new System.Drawing.Point(25, 76);
            this.lbl_chooseGit.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_chooseGit.Name = "lbl_chooseGit";
            this.lbl_chooseGit.Size = new System.Drawing.Size(155, 18);
            this.lbl_chooseGit.TabIndex = 12;
            this.lbl_chooseGit.Text = "Choose git repo folder";
            // 
            // btn_GitRepoBrowse
            // 
            this.btn_GitRepoBrowse.BackColor = System.Drawing.Color.White;
            this.btn_GitRepoBrowse.Location = new System.Drawing.Point(589, 115);
            this.btn_GitRepoBrowse.Margin = new System.Windows.Forms.Padding(4);
            this.btn_GitRepoBrowse.Name = "btn_GitRepoBrowse";
            this.btn_GitRepoBrowse.Size = new System.Drawing.Size(100, 28);
            this.btn_GitRepoBrowse.TabIndex = 13;
            this.btn_GitRepoBrowse.Text = "Browse";
            this.btn_GitRepoBrowse.UseVisualStyleBackColor = false;
            this.btn_GitRepoBrowse.Click += new System.EventHandler(this.btn_GitRepoBrowse_Click);
            // 
            // txt_gitRepoPath
            // 
            this.txt_gitRepoPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_gitRepoPath.Location = new System.Drawing.Point(151, 116);
            this.txt_gitRepoPath.Margin = new System.Windows.Forms.Padding(4);
            this.txt_gitRepoPath.Name = "txt_gitRepoPath";
            this.txt_gitRepoPath.Size = new System.Drawing.Size(403, 24);
            this.txt_gitRepoPath.TabIndex = 14;
            this.txt_gitRepoPath.TextChanged += new System.EventHandler(this.txt_gitRepoPath_TextChanged);
            // 
            // panel_GitLogin
            // 
            this.panel_GitLogin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_GitLogin.Controls.Add(this.cmbReleaseVersions);
            this.panel_GitLogin.Controls.Add(this.label4);
            this.panel_GitLogin.Controls.Add(this.label5);
            this.panel_GitLogin.Controls.Add(this.txt_gitUsername);
            this.panel_GitLogin.Controls.Add(this.button2);
            this.panel_GitLogin.Location = new System.Drawing.Point(36, 259);
            this.panel_GitLogin.Margin = new System.Windows.Forms.Padding(4);
            this.panel_GitLogin.Name = "panel_GitLogin";
            this.panel_GitLogin.Size = new System.Drawing.Size(829, 72);
            this.panel_GitLogin.TabIndex = 15;
            // 
            // cmbReleaseVersions
            // 
            this.cmbReleaseVersions.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmbReleaseVersions.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbReleaseVersions.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbReleaseVersions.FormattingEnabled = true;
            this.cmbReleaseVersions.Location = new System.Drawing.Point(151, 25);
            this.cmbReleaseVersions.Name = "cmbReleaseVersions";
            this.cmbReleaseVersions.Size = new System.Drawing.Size(221, 26);
            this.cmbReleaseVersions.TabIndex = 18;
            this.cmbReleaseVersions.SelectedIndexChanged += new System.EventHandler(this.cmbReleaseVersions_SelectedIndexChanged);
            // 
            // lbl_repoError
            // 
            this.lbl_repoError.AutoSize = true;
            this.lbl_repoError.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_repoError.ForeColor = System.Drawing.Color.Red;
            this.lbl_repoError.Location = new System.Drawing.Point(213, 155);
            this.lbl_repoError.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_repoError.Name = "lbl_repoError";
            this.lbl_repoError.Size = new System.Drawing.Size(46, 18);
            this.lbl_repoError.TabIndex = 16;
            this.lbl_repoError.Text = "label1";
            // 
            // txt_powershellOutput
            // 
            this.txt_powershellOutput.BackColor = System.Drawing.Color.White;
            this.txt_powershellOutput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_powershellOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_powershellOutput.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.txt_powershellOutput.Location = new System.Drawing.Point(36, 349);
            this.txt_powershellOutput.Margin = new System.Windows.Forms.Padding(4);
            this.txt_powershellOutput.Name = "txt_powershellOutput";
            this.txt_powershellOutput.ReadOnly = true;
            this.txt_powershellOutput.Size = new System.Drawing.Size(829, 377);
            this.txt_powershellOutput.TabIndex = 17;
            this.txt_powershellOutput.Text = "";
            this.txt_powershellOutput.TextChanged += new System.EventHandler(this.txt_powershellOutput_TextChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::MLocalRun.Properties.Resources.f01a5591f8484bfc1862d58e5aaee70c145d0e91;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(700, 873);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(167, 45);
            this.pictureBox1.TabIndex = 18;
            this.pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btn_GitRepoBrowse);
            this.panel1.Controls.Add(this.txt_gitRepoPath);
            this.panel1.Controls.Add(this.lbl_repoError);
            this.panel1.Controls.Add(this.check_DoYouHaveGit);
            this.panel1.Controls.Add(this.lbl_chooseGit);
            this.panel1.Location = new System.Drawing.Point(36, 47);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(829, 190);
            this.panel1.TabIndex = 19;
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(64, 24);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.White;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(902, 28);
            this.menuStrip1.TabIndex = 20;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(45, 246);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(310, 24);
            this.label1.TabIndex = 21;
            this.label1.Text = "Git branch and Bitbucket credentials";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(47, 339);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 24);
            this.label2.TabIndex = 22;
            this.label2.Text = "Output";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.radioButton_No);
            this.panel2.Controls.Add(this.radioButton_Yes);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.textBox_Changeset);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.button_Changeset);
            this.panel2.Location = new System.Drawing.Point(34, 746);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(831, 110);
            this.panel2.TabIndex = 21;
            // 
            // radioButton_No
            // 
            this.radioButton_No.AutoSize = true;
            this.radioButton_No.Location = new System.Drawing.Point(498, 23);
            this.radioButton_No.Name = "radioButton_No";
            this.radioButton_No.Size = new System.Drawing.Size(47, 21);
            this.radioButton_No.TabIndex = 26;
            this.radioButton_No.TabStop = true;
            this.radioButton_No.Text = "No";
            this.radioButton_No.UseVisualStyleBackColor = true;
            this.radioButton_No.CheckedChanged += new System.EventHandler(this.radioButton_No_CheckedChanged);
            // 
            // radioButton_Yes
            // 
            this.radioButton_Yes.AutoSize = true;
            this.radioButton_Yes.Location = new System.Drawing.Point(413, 23);
            this.radioButton_Yes.Name = "radioButton_Yes";
            this.radioButton_Yes.Size = new System.Drawing.Size(53, 21);
            this.radioButton_Yes.TabIndex = 25;
            this.radioButton_Yes.TabStop = true;
            this.radioButton_Yes.Text = "Yes";
            this.radioButton_Yes.UseVisualStyleBackColor = true;
            this.radioButton_Yes.CheckedChanged += new System.EventHandler(this.radioButton_Yes_CheckedChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(55, 23);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(319, 18);
            this.label7.TabIndex = 25;
            this.label7.Text = "Do you want to checkout a specific changeset?";
            // 
            // textBox_Changeset
            // 
            this.textBox_Changeset.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_Changeset.Location = new System.Drawing.Point(221, 61);
            this.textBox_Changeset.Name = "textBox_Changeset";
            this.textBox_Changeset.Size = new System.Drawing.Size(359, 24);
            this.textBox_Changeset.TabIndex = 19;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(124, 62);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 18);
            this.label6.TabIndex = 20;
            this.label6.Text = "Changeset:";
            // 
            // button_Changeset
            // 
            this.button_Changeset.BackColor = System.Drawing.Color.White;
            this.button_Changeset.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Changeset.Location = new System.Drawing.Point(595, 56);
            this.button_Changeset.Margin = new System.Windows.Forms.Padding(4);
            this.button_Changeset.Name = "button_Changeset";
            this.button_Changeset.Size = new System.Drawing.Size(96, 35);
            this.button_Changeset.TabIndex = 10;
            this.button_Changeset.Text = "Next";
            this.button_Changeset.UseVisualStyleBackColor = false;
            this.button_Changeset.Click += new System.EventHandler(this.button_Changeset_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(45, 732);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(183, 24);
            this.label8.TabIndex = 24;
            this.label8.Text = "Checkout changeset";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // label_GitPleaseWait
            // 
            this.label_GitPleaseWait.AutoSize = true;
            this.label_GitPleaseWait.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_GitPleaseWait.Location = new System.Drawing.Point(31, 874);
            this.label_GitPleaseWait.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_GitPleaseWait.Name = "label_GitPleaseWait";
            this.label_GitPleaseWait.Size = new System.Drawing.Size(261, 18);
            this.label_GitPleaseWait.TabIndex = 19;
            this.label_GitPleaseWait.Text = "Connecting to Git server, please wait...";
            // 
            // progressBar_Changeset
            // 
            this.progressBar_Changeset.Location = new System.Drawing.Point(34, 896);
            this.progressBar_Changeset.Name = "progressBar_Changeset";
            this.progressBar_Changeset.Size = new System.Drawing.Size(258, 23);
            this.progressBar_Changeset.TabIndex = 25;
            // 
            // GetGitRepo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(902, 938);
            this.Controls.Add(this.progressBar_Changeset);
            this.Controls.Add(this.label_GitPleaseWait);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txt_gitVersion);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.txt_powershellOutput);
            this.Controls.Add(this.panel_GitLogin);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "GetGitRepo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Automated Local Dev Tasks Tool";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.panel_GitLogin.ResumeLayout(false);
            this.panel_GitLogin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_gitVersion;
        private System.Windows.Forms.TextBox txt_gitUsername;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.CheckBox check_DoYouHaveGit;
        private System.Windows.Forms.Label lbl_chooseGit;
        private System.Windows.Forms.TextBox txt_gitRepoPath;
        private System.Windows.Forms.Button btn_GitRepoBrowse;
        private System.Windows.Forms.Panel panel_GitLogin;
        private System.Windows.Forms.Label lbl_repoError;
        private System.Windows.Forms.RichTextBox txt_powershellOutput;
    private System.Windows.Forms.ComboBox cmbReleaseVersions;
    private System.Windows.Forms.PictureBox pictureBox1;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.MenuStrip menuStrip1;
    private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.TextBox textBox_Changeset;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Button button_Changeset;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.Label label_GitPleaseWait;
    private System.Windows.Forms.RadioButton radioButton_No;
    private System.Windows.Forms.RadioButton radioButton_Yes;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.ProgressBar progressBar_Changeset;
  }
}

