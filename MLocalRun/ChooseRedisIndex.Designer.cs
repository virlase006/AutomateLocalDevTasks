namespace MLocalRun
{
    partial class ChooseRedisIndex
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChooseRedisIndex));
      this.comboBox1 = new System.Windows.Forms.ComboBox();
      this.label1 = new System.Windows.Forms.Label();
      this.button1 = new System.Windows.Forms.Button();
      this.txt_powershellOutput = new System.Windows.Forms.RichTextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.txt_PathToElasticSearch = new System.Windows.Forms.TextBox();
      this.button2 = new System.Windows.Forms.Button();
      this.panel1 = new System.Windows.Forms.Panel();
      this.pictureBox1 = new System.Windows.Forms.PictureBox();
      this.label3 = new System.Windows.Forms.Label();
      this.panel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      this.SuspendLayout();
      // 
      // comboBox1
      // 
      this.comboBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.comboBox1.FormattingEnabled = true;
      this.comboBox1.Location = new System.Drawing.Point(211, 44);
      this.comboBox1.Margin = new System.Windows.Forms.Padding(4);
      this.comboBox1.Name = "comboBox1";
      this.comboBox1.Size = new System.Drawing.Size(397, 26);
      this.comboBox1.TabIndex = 0;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(19, 44);
      this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(141, 18);
      this.label1.TabIndex = 1;
      this.label1.Text = "Choose Redis Index";
      // 
      // button1
      // 
      this.button1.BackColor = System.Drawing.Color.White;
      this.button1.Location = new System.Drawing.Point(737, 127);
      this.button1.Margin = new System.Windows.Forms.Padding(4);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(100, 28);
      this.button1.TabIndex = 2;
      this.button1.Text = "Submit";
      this.button1.UseVisualStyleBackColor = false;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // txt_powershellOutput
      // 
      this.txt_powershellOutput.Location = new System.Drawing.Point(22, 178);
      this.txt_powershellOutput.Margin = new System.Windows.Forms.Padding(4);
      this.txt_powershellOutput.Name = "txt_powershellOutput";
      this.txt_powershellOutput.ReadOnly = true;
      this.txt_powershellOutput.Size = new System.Drawing.Size(892, 275);
      this.txt_powershellOutput.TabIndex = 3;
      this.txt_powershellOutput.Text = "";
      this.txt_powershellOutput.TextChanged += new System.EventHandler(this.txt_powershellOutput_TextChanged);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label2.Location = new System.Drawing.Point(19, 95);
      this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(148, 18);
      this.label2.TabIndex = 4;
      this.label2.Text = "Path to Elasticsearch";
      // 
      // txt_PathToElasticSearch
      // 
      this.txt_PathToElasticSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txt_PathToElasticSearch.Location = new System.Drawing.Point(211, 93);
      this.txt_PathToElasticSearch.Margin = new System.Windows.Forms.Padding(4);
      this.txt_PathToElasticSearch.Name = "txt_PathToElasticSearch";
      this.txt_PathToElasticSearch.Size = new System.Drawing.Size(496, 24);
      this.txt_PathToElasticSearch.TabIndex = 5;
      // 
      // button2
      // 
      this.button2.BackColor = System.Drawing.Color.White;
      this.button2.Location = new System.Drawing.Point(737, 91);
      this.button2.Margin = new System.Windows.Forms.Padding(4);
      this.button2.Name = "button2";
      this.button2.Size = new System.Drawing.Size(100, 28);
      this.button2.TabIndex = 6;
      this.button2.Text = "Browse";
      this.button2.UseVisualStyleBackColor = false;
      this.button2.Click += new System.EventHandler(this.button2_Click);
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.txt_powershellOutput);
      this.panel1.Controls.Add(this.button2);
      this.panel1.Controls.Add(this.button1);
      this.panel1.Controls.Add(this.label1);
      this.panel1.Controls.Add(this.txt_PathToElasticSearch);
      this.panel1.Controls.Add(this.comboBox1);
      this.panel1.Controls.Add(this.label2);
      this.panel1.Location = new System.Drawing.Point(28, 30);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(938, 477);
      this.panel1.TabIndex = 7;
      // 
      // pictureBox1
      // 
      this.pictureBox1.BackgroundImage = global::MLocalRun.Properties.Resources.f01a5591f8484bfc1862d58e5aaee70c145d0e91;
      this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
      this.pictureBox1.Location = new System.Drawing.Point(799, 523);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new System.Drawing.Size(167, 45);
      this.pictureBox1.TabIndex = 19;
      this.pictureBox1.TabStop = false;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.BackColor = System.Drawing.Color.Transparent;
      this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label3.Location = new System.Drawing.Point(36, 17);
      this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(211, 24);
      this.label3.TabIndex = 7;
      this.label3.Text = "Redis and Elasticsearch";
      // 
      // ChooseRedisIndex
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.White;
      this.ClientSize = new System.Drawing.Size(994, 582);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.pictureBox1);
      this.Controls.Add(this.panel1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Margin = new System.Windows.Forms.Padding(4);
      this.MaximizeBox = false;
      this.Name = "ChooseRedisIndex";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Automated Local Dev Tasks Tool";
      this.Load += new System.EventHandler(this.ChooseRedisIndex_Load);
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox txt_powershellOutput;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_PathToElasticSearch;
        private System.Windows.Forms.Button button2;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.PictureBox pictureBox1;
    private System.Windows.Forms.Label label3;
  }
}