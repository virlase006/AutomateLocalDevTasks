namespace MLocalRun
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
            this.components = new System.ComponentModel.Container();
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
            this.lbl_repoError = new System.Windows.Forms.Label();
            this.txt_powershellOutput = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.panel_GitLogin.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Git connection";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(40, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Git version";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(40, 58);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Username";
            // 
            // txt_gitVersion
            // 
            this.txt_gitVersion.Location = new System.Drawing.Point(113, 22);
            this.txt_gitVersion.Name = "txt_gitVersion";
            this.txt_gitVersion.Size = new System.Drawing.Size(100, 20);
            this.txt_gitVersion.TabIndex = 8;
            // 
            // txt_gitUsername
            // 
            this.txt_gitUsername.Location = new System.Drawing.Point(113, 51);
            this.txt_gitUsername.Name = "txt_gitUsername";
            this.txt_gitUsername.Size = new System.Drawing.Size(100, 20);
            this.txt_gitUsername.TabIndex = 9;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(246, 58);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 10;
            this.button2.Text = "Next";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // check_DoYouHaveGit
            // 
            this.check_DoYouHaveGit.AutoSize = true;
            this.check_DoYouHaveGit.Location = new System.Drawing.Point(27, 57);
            this.check_DoYouHaveGit.Name = "check_DoYouHaveGit";
            this.check_DoYouHaveGit.Size = new System.Drawing.Size(146, 17);
            this.check_DoYouHaveGit.TabIndex = 11;
            this.check_DoYouHaveGit.Text = "Do you have a git repo ? ";
            this.check_DoYouHaveGit.UseVisualStyleBackColor = true;
            this.check_DoYouHaveGit.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // lbl_chooseGit
            // 
            this.lbl_chooseGit.AutoSize = true;
            this.lbl_chooseGit.Location = new System.Drawing.Point(27, 97);
            this.lbl_chooseGit.Name = "lbl_chooseGit";
            this.lbl_chooseGit.Size = new System.Drawing.Size(110, 13);
            this.lbl_chooseGit.TabIndex = 12;
            this.lbl_chooseGit.Text = "Choose git repo folder";
            // 
            // btn_GitRepoBrowse
            // 
            this.btn_GitRepoBrowse.Location = new System.Drawing.Point(357, 87);
            this.btn_GitRepoBrowse.Name = "btn_GitRepoBrowse";
            this.btn_GitRepoBrowse.Size = new System.Drawing.Size(75, 23);
            this.btn_GitRepoBrowse.TabIndex = 13;
            this.btn_GitRepoBrowse.Text = "Browse";
            this.btn_GitRepoBrowse.UseVisualStyleBackColor = true;
            this.btn_GitRepoBrowse.Click += new System.EventHandler(this.btn_GitRepoBrowse_Click);
            // 
            // txt_gitRepoPath
            // 
            this.txt_gitRepoPath.Location = new System.Drawing.Point(180, 90);
            this.txt_gitRepoPath.Name = "txt_gitRepoPath";
            this.txt_gitRepoPath.Size = new System.Drawing.Size(154, 20);
            this.txt_gitRepoPath.TabIndex = 14;
            this.txt_gitRepoPath.TextChanged += new System.EventHandler(this.txt_gitRepoPath_TextChanged);
            // 
            // panel_GitLogin
            // 
            this.panel_GitLogin.Controls.Add(this.txt_gitVersion);
            this.panel_GitLogin.Controls.Add(this.label4);
            this.panel_GitLogin.Controls.Add(this.label5);
            this.panel_GitLogin.Controls.Add(this.txt_gitUsername);
            this.panel_GitLogin.Controls.Add(this.button2);
            this.panel_GitLogin.Location = new System.Drawing.Point(30, 147);
            this.panel_GitLogin.Name = "panel_GitLogin";
            this.panel_GitLogin.Size = new System.Drawing.Size(578, 100);
            this.panel_GitLogin.TabIndex = 15;
            // 
            // lbl_repoError
            // 
            this.lbl_repoError.AutoSize = true;
            this.lbl_repoError.ForeColor = System.Drawing.Color.Red;
            this.lbl_repoError.Location = new System.Drawing.Point(180, 117);
            this.lbl_repoError.Name = "lbl_repoError";
            this.lbl_repoError.Size = new System.Drawing.Size(35, 13);
            this.lbl_repoError.TabIndex = 16;
            this.lbl_repoError.Text = "label1";
            // 
            // txt_powershellOutput
            // 
            this.txt_powershellOutput.Enabled = false;
            this.txt_powershellOutput.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.txt_powershellOutput.Location = new System.Drawing.Point(27, 280);
            this.txt_powershellOutput.Name = "txt_powershellOutput";
            this.txt_powershellOutput.Size = new System.Drawing.Size(596, 96);
            this.txt_powershellOutput.TabIndex = 17;
            this.txt_powershellOutput.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(669, 427);
            this.Controls.Add(this.txt_powershellOutput);
            this.Controls.Add(this.lbl_repoError);
            this.Controls.Add(this.panel_GitLogin);
            this.Controls.Add(this.txt_gitRepoPath);
            this.Controls.Add(this.btn_GitRepoBrowse);
            this.Controls.Add(this.lbl_chooseGit);
            this.Controls.Add(this.check_DoYouHaveGit);
            this.Controls.Add(this.label3);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.panel_GitLogin.ResumeLayout(false);
            this.panel_GitLogin.PerformLayout();
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
    }
}

