namespace ViewAppUp
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.butEnable = new System.Windows.Forms.Button();
            this.bntPrevious = new System.Windows.Forms.Button();
            this.butNext = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buFalse = new System.Windows.Forms.Button();
            this.butFirst = new System.Windows.Forms.Button();
            this.butLast = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(297, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(124, 36);
            this.button1.TabIndex = 0;
            this.button1.Text = "查詢";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(29, 152);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 31;
            this.dataGridView1.Size = new System.Drawing.Size(740, 270);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseClick);
            // 
            // butEnable
            // 
            this.butEnable.Location = new System.Drawing.Point(473, 12);
            this.butEnable.Name = "butEnable";
            this.butEnable.Size = new System.Drawing.Size(145, 60);
            this.butEnable.TabIndex = 2;
            this.butEnable.Text = "啟用";
            this.butEnable.UseVisualStyleBackColor = true;
            this.butEnable.Click += new System.EventHandler(this.butEnable_Click);
            // 
            // bntPrevious
            // 
            this.bntPrevious.Location = new System.Drawing.Point(180, 94);
            this.bntPrevious.Name = "bntPrevious";
            this.bntPrevious.Size = new System.Drawing.Size(145, 41);
            this.bntPrevious.TabIndex = 3;
            this.bntPrevious.Text = "上一個";
            this.bntPrevious.UseVisualStyleBackColor = true;
            this.bntPrevious.Click += new System.EventHandler(this.bntPrevious_Click);
            // 
            // butNext
            // 
            this.butNext.Location = new System.Drawing.Point(473, 94);
            this.butNext.Name = "butNext";
            this.butNext.Size = new System.Drawing.Size(145, 41);
            this.butNext.TabIndex = 4;
            this.butNext.Text = "下一個";
            this.butNext.UseVisualStyleBackColor = true;
            this.butNext.Click += new System.EventHandler(this.butNext_Click);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("新細明體", 12F);
            this.textBox1.Location = new System.Drawing.Point(89, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(187, 36);
            this.textBox1.TabIndex = 5;
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("新細明體", 12F);
            this.label1.Location = new System.Drawing.Point(25, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 24);
            this.label1.TabIndex = 6;
            this.label1.Text = "名字";
            // 
            // buFalse
            // 
            this.buFalse.Location = new System.Drawing.Point(624, 12);
            this.buFalse.Name = "buFalse";
            this.buFalse.Size = new System.Drawing.Size(145, 60);
            this.buFalse.TabIndex = 7;
            this.buFalse.Text = "停用";
            this.buFalse.UseVisualStyleBackColor = true;
            this.buFalse.Click += new System.EventHandler(this.buFalse_Click);
            // 
            // butFirst
            // 
            this.butFirst.Location = new System.Drawing.Point(29, 94);
            this.butFirst.Name = "butFirst";
            this.butFirst.Size = new System.Drawing.Size(145, 41);
            this.butFirst.TabIndex = 8;
            this.butFirst.Text = "第一個";
            this.butFirst.UseVisualStyleBackColor = true;
            this.butFirst.Click += new System.EventHandler(this.butFirst_Click);
            // 
            // butLast
            // 
            this.butLast.Location = new System.Drawing.Point(624, 94);
            this.butLast.Name = "butLast";
            this.butLast.Size = new System.Drawing.Size(145, 41);
            this.butLast.TabIndex = 9;
            this.butLast.Text = "最後一個";
            this.butLast.UseVisualStyleBackColor = true;
            this.butLast.Click += new System.EventHandler(this.butLast_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(348, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 18);
            this.label2.TabIndex = 10;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(793, 441);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.butLast);
            this.Controls.Add(this.butFirst);
            this.Controls.Add(this.buFalse);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.butNext);
            this.Controls.Add(this.bntPrevious);
            this.Controls.Add(this.butEnable);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "AdminTools";
            this.MinimumSizeChanged += new System.EventHandler(this.Form1_MinimumSizeChanged);
            this.Load += new System.EventHandler(this.Form1_Load);            
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button butEnable;
        private System.Windows.Forms.Button bntPrevious;
        private System.Windows.Forms.Button butNext;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buFalse;
        private System.Windows.Forms.Button butFirst;
        private System.Windows.Forms.Button butLast;
        private System.Windows.Forms.Label label2;
    }
}

