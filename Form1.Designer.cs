namespace DBSCAN
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
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
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
            this.canvas = new System.Windows.Forms.PictureBox();
            this.openFileButton = new System.Windows.Forms.Button();
            this.runButton = new System.Windows.Forms.Button();
            this.epsLabel = new System.Windows.Forms.Label();
            this.minPtsLabel = new System.Windows.Forms.Label();
            this.epsTextBox = new System.Windows.Forms.TextBox();
            this.minPtsTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).BeginInit();
            this.SuspendLayout();
            // 
            // canvas
            // 
            this.canvas.BackColor = System.Drawing.Color.White;
            this.canvas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.canvas.Location = new System.Drawing.Point(13, 13);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(691, 472);
            this.canvas.TabIndex = 0;
            this.canvas.TabStop = false;
            // 
            // openFileButton
            // 
            this.openFileButton.Location = new System.Drawing.Point(714, 186);
            this.openFileButton.Name = "openFileButton";
            this.openFileButton.Size = new System.Drawing.Size(151, 60);
            this.openFileButton.TabIndex = 1;
            this.openFileButton.Text = "選取資料集";
            this.openFileButton.UseVisualStyleBackColor = true;
            // 
            // runButton
            // 
            this.runButton.Location = new System.Drawing.Point(714, 252);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(151, 60);
            this.runButton.TabIndex = 2;
            this.runButton.Text = "執行";
            this.runButton.UseVisualStyleBackColor = true;
            // 
            // epsLabel
            // 
            this.epsLabel.AutoSize = true;
            this.epsLabel.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.epsLabel.Location = new System.Drawing.Point(710, 13);
            this.epsLabel.Name = "epsLabel";
            this.epsLabel.Size = new System.Drawing.Size(130, 24);
            this.epsLabel.TabIndex = 3;
            this.epsLabel.Text = "圓心半徑(eps)";
            // 
            // minPtsLabel
            // 
            this.minPtsLabel.AutoSize = true;
            this.minPtsLabel.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.minPtsLabel.Location = new System.Drawing.Point(710, 107);
            this.minPtsLabel.Name = "minPtsLabel";
            this.minPtsLabel.Size = new System.Drawing.Size(160, 24);
            this.minPtsLabel.TabIndex = 4;
            this.minPtsLabel.Text = "範圍數量(minPts)";
            // 
            // epsTextBox
            // 
            this.epsTextBox.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.epsTextBox.Location = new System.Drawing.Point(710, 40);
            this.epsTextBox.Name = "epsTextBox";
            this.epsTextBox.Size = new System.Drawing.Size(155, 33);
            this.epsTextBox.TabIndex = 5;
            this.epsTextBox.Text = "5";
            this.epsTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // minPtsTextBox
            // 
            this.minPtsTextBox.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.minPtsTextBox.Location = new System.Drawing.Point(710, 134);
            this.minPtsTextBox.Name = "minPtsTextBox";
            this.minPtsTextBox.Size = new System.Drawing.Size(155, 33);
            this.minPtsTextBox.TabIndex = 6;
            this.minPtsTextBox.Text = "2";
            this.minPtsTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(871, 497);
            this.Controls.Add(this.minPtsTextBox);
            this.Controls.Add(this.epsTextBox);
            this.Controls.Add(this.minPtsLabel);
            this.Controls.Add(this.epsLabel);
            this.Controls.Add(this.runButton);
            this.Controls.Add(this.openFileButton);
            this.Controls.Add(this.canvas);
            this.Name = "Form1";
            this.Text = "DBSCAN";
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox canvas;
        private System.Windows.Forms.Button openFileButton;
        private System.Windows.Forms.Button runButton;
        private System.Windows.Forms.Label epsLabel;
        private System.Windows.Forms.Label minPtsLabel;
        private System.Windows.Forms.TextBox epsTextBox;
        private System.Windows.Forms.TextBox minPtsTextBox;
    }
}

