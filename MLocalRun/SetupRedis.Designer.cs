namespace MLocalRun
{
    partial class SetupRedis
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetupRedis));
      this.label_UbuntuUserName = new System.Windows.Forms.Label();
      this.txt_RedisPath = new System.Windows.Forms.TextBox();
      this.label_RedisRdbFile = new System.Windows.Forms.Label();
      this.txt_RdbPath = new System.Windows.Forms.TextBox();
      this.button_browseRedisFile = new System.Windows.Forms.Button();
      this.button2 = new System.Windows.Forms.Button();
      this.txt_powershellOutput = new System.Windows.Forms.RichTextBox();
      this.lbl_repoError = new System.Windows.Forms.Label();
      this.panel1 = new System.Windows.Forms.Panel();
      this.button1 = new System.Windows.Forms.Button();
      this.label2 = new System.Windows.Forms.Label();
      this.pictureBox1 = new System.Windows.Forms.PictureBox();
      this.panel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      this.SuspendLayout();
      // 
      // label_UbuntuUserName
      // 
      this.label_UbuntuUserName.AutoSize = true;
      this.label_UbuntuUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label_UbuntuUserName.Location = new System.Drawing.Point(15, 48);
      this.label_UbuntuUserName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.label_UbuntuUserName.Name = "label_UbuntuUserName";
      this.label_UbuntuUserName.Size = new System.Drawing.Size(102, 18);
      this.label_UbuntuUserName.TabIndex = 0;
      this.label_UbuntuUserName.Text = "Path To Redis";
      // 
      // txt_RedisPath
      // 
      this.txt_RedisPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txt_RedisPath.Location = new System.Drawing.Point(191, 45);
      this.txt_RedisPath.Margin = new System.Windows.Forms.Padding(4);
      this.txt_RedisPath.Name = "txt_RedisPath";
      this.txt_RedisPath.Size = new System.Drawing.Size(509, 24);
      this.txt_RedisPath.TabIndex = 1;
      // 
      // label_RedisRdbFile
      // 
      this.label_RedisRdbFile.AutoSize = true;
      this.label_RedisRdbFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label_RedisRdbFile.Location = new System.Drawing.Point(15, 125);
      this.label_RedisRdbFile.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.label_RedisRdbFile.Name = "label_RedisRdbFile";
      this.label_RedisRdbFile.Size = new System.Drawing.Size(155, 18);
      this.label_RedisRdbFile.TabIndex = 2;
      this.label_RedisRdbFile.Text = "Path to Redis Rdb File";
      // 
      // txt_RdbPath
      // 
      this.txt_RdbPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txt_RdbPath.Location = new System.Drawing.Point(191, 122);
      this.txt_RdbPath.Margin = new System.Windows.Forms.Padding(4);
      this.txt_RdbPath.Name = "txt_RdbPath";
      this.txt_RdbPath.Size = new System.Drawing.Size(509, 24);
      this.txt_RdbPath.TabIndex = 3;
      this.txt_RdbPath.TextChanged += new System.EventHandler(this.txt_RdbPath_TextChanged);
      // 
      // button_browseRedisFile
      // 
      this.button_browseRedisFile.BackColor = System.Drawing.Color.White;
      this.button_browseRedisFile.Location = new System.Drawing.Point(723, 44);
      this.button_browseRedisFile.Margin = new System.Windows.Forms.Padding(4);
      this.button_browseRedisFile.Name = "button_browseRedisFile";
      this.button_browseRedisFile.Size = new System.Drawing.Size(100, 28);
      this.button_browseRedisFile.TabIndex = 4;
      this.button_browseRedisFile.Text = "Browse";
      this.button_browseRedisFile.UseVisualStyleBackColor = false;
      this.button_browseRedisFile.Click += new System.EventHandler(this.button_browseRedisFile_Click);
      // 
      // button2
      // 
      this.button2.BackColor = System.Drawing.Color.White;
      this.button2.Location = new System.Drawing.Point(723, 162);
      this.button2.Margin = new System.Windows.Forms.Padding(4);
      this.button2.Name = "button2";
      this.button2.Size = new System.Drawing.Size(100, 28);
      this.button2.TabIndex = 8;
      this.button2.Text = "Next";
      this.button2.UseVisualStyleBackColor = false;
      this.button2.Click += new System.EventHandler(this.button2_Click);
      // 
      // txt_powershellOutput
      // 
      this.txt_powershellOutput.BackColor = System.Drawing.Color.White;
      this.txt_powershellOutput.Location = new System.Drawing.Point(26, 207);
      this.txt_powershellOutput.Margin = new System.Windows.Forms.Padding(4);
      this.txt_powershellOutput.Name = "txt_powershellOutput";
      this.txt_powershellOutput.ReadOnly = true;
      this.txt_powershellOutput.Size = new System.Drawing.Size(797, 213);
      this.txt_powershellOutput.TabIndex = 9;
      this.txt_powershellOutput.Text = "";
      this.txt_powershellOutput.TextChanged += new System.EventHandler(this.txt_powershellOutput_TextChanged);
      // 
      // lbl_repoError
      // 
      this.lbl_repoError.AutoSize = true;
      this.lbl_repoError.ForeColor = System.Drawing.Color.Red;
      this.lbl_repoError.Location = new System.Drawing.Point(188, 167);
      this.lbl_repoError.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.lbl_repoError.Name = "lbl_repoError";
      this.lbl_repoError.Size = new System.Drawing.Size(46, 18);
      this.lbl_repoError.TabIndex = 10;
      this.lbl_repoError.Text = "label1";
      // 
      // panel1
      // 
      this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.panel1.Controls.Add(this.button1);
      this.panel1.Controls.Add(this.txt_powershellOutput);
      this.panel1.Controls.Add(this.button2);
      this.panel1.Controls.Add(this.lbl_repoError);
      this.panel1.Controls.Add(this.button_browseRedisFile);
      this.panel1.Controls.Add(this.label_UbuntuUserName);
      this.panel1.Controls.Add(this.label_RedisRdbFile);
      this.panel1.Controls.Add(this.txt_RedisPath);
      this.panel1.Controls.Add(this.txt_RdbPath);
      this.panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.panel1.Location = new System.Drawing.Point(23, 34);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(850, 457);
      this.panel1.TabIndex = 11;
      // 
      // button1
      // 
      this.button1.BackColor = System.Drawing.Color.White;
      this.button1.Location = new System.Drawing.Point(723, 120);
      this.button1.Margin = new System.Windows.Forms.Padding(4);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(100, 28);
      this.button1.TabIndex = 11;
      this.button1.Text = "Browse";
      this.button1.UseVisualStyleBackColor = false;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label2.Location = new System.Drawing.Point(30, 22);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(112, 24);
      this.label2.TabIndex = 13;
      this.label2.Text = "Redis Setup";
      // 
      // pictureBox1
      // 
      this.pictureBox1.BackgroundImage = global::MLocalRun.Properties.Resources.f01a5591f8484bfc1862d58e5aaee70c145d0e91;
      this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
      this.pictureBox1.Location = new System.Drawing.Point(706, 511);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new System.Drawing.Size(167, 45);
      this.pictureBox1.TabIndex = 19;
      this.pictureBox1.TabStop = false;
      // 
      // SetupRedis
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.White;
      this.ClientSize = new System.Drawing.Size(898, 584);
      this.Controls.Add(this.pictureBox1);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.panel1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Margin = new System.Windows.Forms.Padding(4);
      this.MaximizeBox = false;
      this.Name = "SetupRedis";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Automated Local Dev Tasks Tool";
      this.Load += new System.EventHandler(this.SetupRedis_Load);
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_UbuntuUserName;
        private System.Windows.Forms.TextBox txt_RedisPath;
        private System.Windows.Forms.Label label_RedisRdbFile;
        private System.Windows.Forms.TextBox txt_RdbPath;
        private System.Windows.Forms.Button button_browseRedisFile;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.RichTextBox txt_powershellOutput;
        private System.Windows.Forms.Label lbl_repoError;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.PictureBox pictureBox1;
    private System.Windows.Forms.Button button1;
  }
}