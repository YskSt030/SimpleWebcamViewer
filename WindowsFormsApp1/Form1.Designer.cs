namespace SimpleWebcamViewer
{
    partial class F_Main
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_rec = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btn_play = new System.Windows.Forms.Button();
            this.btn_swcam = new System.Windows.Forms.Button();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel_chnum = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_rec
            // 
            this.btn_rec.Font = new System.Drawing.Font("Arial", 9F);
            this.btn_rec.Location = new System.Drawing.Point(670, 15);
            this.btn_rec.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_rec.Name = "btn_rec";
            this.btn_rec.Size = new System.Drawing.Size(101, 29);
            this.btn_rec.TabIndex = 0;
            this.btn_rec.Text = "REC";
            this.btn_rec.UseVisualStyleBackColor = true;
            this.btn_rec.Click += new System.EventHandler(this.btn_rec_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(14, 15);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(650, 516);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // btn_play
            // 
            this.btn_play.Font = new System.Drawing.Font("Arial", 9F);
            this.btn_play.Location = new System.Drawing.Point(670, 52);
            this.btn_play.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_play.Name = "btn_play";
            this.btn_play.Size = new System.Drawing.Size(101, 29);
            this.btn_play.TabIndex = 2;
            this.btn_play.Text = "PLAY";
            this.btn_play.UseVisualStyleBackColor = true;
            this.btn_play.Click += new System.EventHandler(this.btn_play_Click);
            // 
            // btn_swcam
            // 
            this.btn_swcam.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_swcam.Location = new System.Drawing.Point(670, 89);
            this.btn_swcam.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_swcam.Name = "btn_swcam";
            this.btn_swcam.Size = new System.Drawing.Size(101, 29);
            this.btn_swcam.TabIndex = 3;
            this.btn_swcam.Text = "SwitchCam";
            this.btn_swcam.UseVisualStyleBackColor = true;
            this.btn_swcam.Click += new System.EventHandler(this.btn_swcam_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel_chnum});
            this.statusStrip.Location = new System.Drawing.Point(0, 540);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(784, 22);
            this.statusStrip.TabIndex = 4;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel_chnum
            // 
            this.toolStripStatusLabel_chnum.Name = "toolStripStatusLabel_chnum";
            this.toolStripStatusLabel_chnum.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel_chnum.Text = "toolStripStatusLabel1";
            // 
            // F_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.btn_swcam);
            this.Controls.Add(this.btn_play);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btn_rec);
            this.Font = new System.Drawing.Font("Arial", 9F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "F_Main";
            this.Text = "Simple Videocam Viewer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_rec;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btn_play;
        private System.Windows.Forms.Button btn_swcam;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_chnum;
    }
}

