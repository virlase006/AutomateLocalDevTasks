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
            this.label_UbuntuUserName = new System.Windows.Forms.Label();
            this.txt_RedisPath = new System.Windows.Forms.TextBox();
            this.label_RedisRdbFile = new System.Windows.Forms.Label();
            this.txt_RdbPath = new System.Windows.Forms.TextBox();
            this.button_browseRedisFile = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.txt_powershellOutput = new System.Windows.Forms.RichTextBox();
            this.lbl_repoError = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label_UbuntuUserName
            // 
            this.label_UbuntuUserName.AutoSize = true;
            this.label_UbuntuUserName.Location = new System.Drawing.Point(74, 84);
            this.label_UbuntuUserName.Name = "label_UbuntuUserName";
            this.label_UbuntuUserName.Size = new System.Drawing.Size(75, 13);
            this.label_UbuntuUserName.TabIndex = 0;
            this.label_UbuntuUserName.Text = "Path To Redis";
            // 
            // txt_RedisPath
            // 
            this.txt_RedisPath.Location = new System.Drawing.Point(213, 81);
            this.txt_RedisPath.Name = "txt_RedisPath";
            this.txt_RedisPath.Size = new System.Drawing.Size(306, 20);
            this.txt_RedisPath.TabIndex = 1;
            // 
            // label_RedisRdbFile
            // 
            this.label_RedisRdbFile.AutoSize = true;
            this.label_RedisRdbFile.Location = new System.Drawing.Point(74, 157);
            this.label_RedisRdbFile.Name = "label_RedisRdbFile";
            this.label_RedisRdbFile.Size = new System.Drawing.Size(113, 13);
            this.label_RedisRdbFile.TabIndex = 2;
            this.label_RedisRdbFile.Text = "Path to Redis Rdb File";
            // 
            // txt_RdbPath
            // 
            this.txt_RdbPath.Location = new System.Drawing.Point(213, 154);
            this.txt_RdbPath.Name = "txt_RdbPath";
            this.txt_RdbPath.Size = new System.Drawing.Size(306, 20);
            this.txt_RdbPath.TabIndex = 3;
            this.txt_RdbPath.TextChanged += new System.EventHandler(this.txt_RdbPath_TextChanged);
            // 
            // button_browseRedisFile
            // 
            this.button_browseRedisFile.Location = new System.Drawing.Point(546, 154);
            this.button_browseRedisFile.Name = "button_browseRedisFile";
            this.button_browseRedisFile.Size = new System.Drawing.Size(75, 23);
            this.button_browseRedisFile.TabIndex = 4;
            this.button_browseRedisFile.Text = "Browse";
            this.button_browseRedisFile.UseVisualStyleBackColor = true;
            this.button_browseRedisFile.Click += new System.EventHandler(this.button_browseRedisFile_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(546, 202);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 8;
            this.button2.Text = "Next";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txt_powershellOutput
            // 
            this.txt_powershellOutput.Location = new System.Drawing.Point(91, 256);
            this.txt_powershellOutput.Name = "txt_powershellOutput";
            this.txt_powershellOutput.ReadOnly = true;
            this.txt_powershellOutput.Size = new System.Drawing.Size(565, 150);
            this.txt_powershellOutput.TabIndex = 9;
            this.txt_powershellOutput.Text = "";
            // 
            // lbl_repoError
            // 
            this.lbl_repoError.AutoSize = true;
            this.lbl_repoError.ForeColor = System.Drawing.Color.Red;
            this.lbl_repoError.Location = new System.Drawing.Point(213, 192);
            this.lbl_repoError.Name = "lbl_repoError";
            this.lbl_repoError.Size = new System.Drawing.Size(35, 13);
            this.lbl_repoError.TabIndex = 10;
            this.lbl_repoError.Text = "label1";
            // 
            // SetupRedis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lbl_repoError);
            this.Controls.Add(this.txt_powershellOutput);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button_browseRedisFile);
            this.Controls.Add(this.txt_RdbPath);
            this.Controls.Add(this.label_RedisRdbFile);
            this.Controls.Add(this.txt_RedisPath);
            this.Controls.Add(this.label_UbuntuUserName);
            this.Name = "SetupRedis";
            this.Text = "SetupRedis";
            this.Load += new System.EventHandler(this.SetupRedis_Load);
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
    }
}