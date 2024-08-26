namespace CoupangSub
{
    partial class SettingsForm
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
            this.btnApply = new System.Windows.Forms.Button();
            this.lblVersion = new System.Windows.Forms.Label();
            this.txtVersion = new System.Windows.Forms.TextBox();
            this.txtPubKey = new System.Windows.Forms.TextBox();
            this.lblPubKey = new System.Windows.Forms.Label();
            this.txtAesKey = new System.Windows.Forms.TextBox();
            this.lblAesKey = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnApply
            // 
            this.btnApply.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnApply.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApply.Location = new System.Drawing.Point(390, 105);
            this.btnApply.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(120, 40);
            this.btnApply.TabIndex = 31;
            this.btnApply.Text = "적용(&A)";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // lblVersion
            // 
            this.lblVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersion.Location = new System.Drawing.Point(30, 110);
            this.lblVersion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(60, 24);
            this.lblVersion.TabIndex = 32;
            this.lblVersion.Text = "앱 버전";
            this.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtVersion
            // 
            this.txtVersion.Location = new System.Drawing.Point(100, 110);
            this.txtVersion.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.Size = new System.Drawing.Size(180, 26);
            this.txtVersion.TabIndex = 30;
            // 
            // txtPubKey
            // 
            this.txtPubKey.Location = new System.Drawing.Point(100, 30);
            this.txtPubKey.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtPubKey.Name = "txtPubKey";
            this.txtPubKey.Size = new System.Drawing.Size(410, 26);
            this.txtPubKey.TabIndex = 30;
            // 
            // lblPubKey
            // 
            this.lblPubKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPubKey.Location = new System.Drawing.Point(30, 30);
            this.lblPubKey.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPubKey.Name = "lblPubKey";
            this.lblPubKey.Size = new System.Drawing.Size(60, 24);
            this.lblPubKey.TabIndex = 32;
            this.lblPubKey.Text = "공개키";
            this.lblPubKey.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtAesKey
            // 
            this.txtAesKey.Location = new System.Drawing.Point(100, 66);
            this.txtAesKey.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtAesKey.Name = "txtAesKey";
            this.txtAesKey.Size = new System.Drawing.Size(410, 26);
            this.txtAesKey.TabIndex = 30;
            // 
            // lblAesKey
            // 
            this.lblAesKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAesKey.Location = new System.Drawing.Point(30, 66);
            this.lblAesKey.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAesKey.Name = "lblAesKey";
            this.lblAesKey.Size = new System.Drawing.Size(60, 24);
            this.lblAesKey.TabIndex = 32;
            this.lblAesKey.Text = "비공개키";
            this.lblAesKey.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // SettingsForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(544, 411);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.lblAesKey);
            this.Controls.Add(this.lblPubKey);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.txtAesKey);
            this.Controls.Add(this.txtPubKey);
            this.Controls.Add(this.txtVersion);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "운영설정";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingsForm_FormClosing);
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.TextBox txtVersion;
        private System.Windows.Forms.TextBox txtPubKey;
        private System.Windows.Forms.Label lblPubKey;
        private System.Windows.Forms.TextBox txtAesKey;
        private System.Windows.Forms.Label lblAesKey;
    }
}