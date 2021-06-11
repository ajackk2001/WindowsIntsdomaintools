﻿namespace DomainTool
{
    partial class Domain
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.butEnableWin = new System.Windows.Forms.Button();
            this.butUpdata = new System.Windows.Forms.Button();
            this.butApplication = new System.Windows.Forms.Button();
            this.butSelFile = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // butEnableWin
            // 
            this.butEnableWin.Location = new System.Drawing.Point(838, 134);
            this.butEnableWin.Name = "butEnableWin";
            this.butEnableWin.Size = new System.Drawing.Size(212, 69);
            this.butEnableWin.TabIndex = 14;
            this.butEnableWin.Text = "啟用";
            this.butEnableWin.UseVisualStyleBackColor = true;
            this.butEnableWin.Click += new System.EventHandler(this.ButEnableWin_Click);
            // 
            // butUpdata
            // 
            this.butUpdata.Location = new System.Drawing.Point(423, 134);
            this.butUpdata.Name = "butUpdata";
            this.butUpdata.Size = new System.Drawing.Size(212, 69);
            this.butUpdata.TabIndex = 13;
            this.butUpdata.Text = "安裝";
            this.butUpdata.UseVisualStyleBackColor = true;
            this.butUpdata.Click += new System.EventHandler(this.ButUpdata_Click);
            // 
            // butApplication
            // 
            this.butApplication.Location = new System.Drawing.Point(111, 134);
            this.butApplication.Name = "butApplication";
            this.butApplication.Size = new System.Drawing.Size(212, 69);
            this.butApplication.TabIndex = 0;
            this.butApplication.Text = "申請";
            this.butApplication.UseVisualStyleBackColor = true;
            this.butApplication.Click += new System.EventHandler(this.ButApplication_Click);
            // 
            // butSelFile
            // 
            this.butSelFile.Location = new System.Drawing.Point(890, 44);
            this.butSelFile.Name = "butSelFile";
            this.butSelFile.Size = new System.Drawing.Size(160, 44);
            this.butSelFile.TabIndex = 3;
            this.butSelFile.Text = "選擇檔案";
            this.butSelFile.UseVisualStyleBackColor = true;
            this.butSelFile.Click += new System.EventHandler(this.ButSelFile_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 18);
            this.label1.TabIndex = 2;
            this.label1.Text = "檔案路徑";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(111, 44);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(740, 34);
            this.textBox1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.butEnableWin);
            this.panel1.Controls.Add(this.butUpdata);
            this.panel1.Controls.Add(this.butApplication);
            this.panel1.Controls.Add(this.butSelFile);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1080, 227);
            this.panel1.TabIndex = 4;
            // 
            // Domain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1080, 227);
            this.Controls.Add(this.panel1);
            this.Name = "Domain";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button butEnableWin;
        private System.Windows.Forms.Button butUpdata;
        private System.Windows.Forms.Button butApplication;
        private System.Windows.Forms.Button butSelFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Panel panel1;
    }
}

