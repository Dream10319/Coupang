namespace CoupangSub
{
    partial class MainForm
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
            this.txtGreetings = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.pnlOrders = new System.Windows.Forms.FlowLayoutPanel();
            this.chkCompleted = new System.Windows.Forms.CheckBox();
            this.chkProgress = new System.Windows.Forms.CheckBox();
            this.chkPending = new System.Windows.Forms.CheckBox();
            this.btnSettings = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtGreetings
            // 
            this.txtGreetings.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGreetings.Location = new System.Drawing.Point(300, 40);
            this.txtGreetings.Name = "txtGreetings";
            this.txtGreetings.Size = new System.Drawing.Size(510, 50);
            this.txtGreetings.TabIndex = 65;
            this.txtGreetings.Text = "안녕하세요, 쿠팡사장님";
            this.txtGreetings.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.Location = new System.Drawing.Point(700, 115);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(180, 50);
            this.btnRefresh.TabIndex = 63;
            this.btnRefresh.Text = "새로고침 (&R)";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // pnlOrders
            // 
            this.pnlOrders.AutoScroll = true;
            this.pnlOrders.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlOrders.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.pnlOrders.Location = new System.Drawing.Point(30, 170);
            this.pnlOrders.Name = "pnlOrders";
            this.pnlOrders.Padding = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.pnlOrders.Size = new System.Drawing.Size(1050, 630);
            this.pnlOrders.TabIndex = 64;
            this.pnlOrders.WrapContents = false;
            // 
            // chkCompleted
            // 
            this.chkCompleted.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkCompleted.Location = new System.Drawing.Point(290, 125);
            this.chkCompleted.Name = "chkCompleted";
            this.chkCompleted.Size = new System.Drawing.Size(120, 40);
            this.chkCompleted.TabIndex = 61;
            this.chkCompleted.Tag = "COMPLETED";
            this.chkCompleted.Text = "(&3) 완료, 취소";
            this.chkCompleted.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkCompleted.UseVisualStyleBackColor = true;
            this.chkCompleted.Click += new System.EventHandler(this.chkType_Click);
            // 
            // chkProgress
            // 
            this.chkProgress.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkProgress.Location = new System.Drawing.Point(160, 125);
            this.chkProgress.Name = "chkProgress";
            this.chkProgress.Size = new System.Drawing.Size(120, 40);
            this.chkProgress.TabIndex = 60;
            this.chkProgress.Tag = "PROCESSING";
            this.chkProgress.Text = "(&2) 진행";
            this.chkProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkProgress.UseVisualStyleBackColor = true;
            this.chkProgress.Click += new System.EventHandler(this.chkType_Click);
            // 
            // chkPending
            // 
            this.chkPending.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkPending.Checked = true;
            this.chkPending.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPending.Location = new System.Drawing.Point(30, 125);
            this.chkPending.Name = "chkPending";
            this.chkPending.Size = new System.Drawing.Size(120, 40);
            this.chkPending.TabIndex = 59;
            this.chkPending.Tag = "PENDING";
            this.chkPending.Text = "(&1) 접수대기";
            this.chkPending.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkPending.UseVisualStyleBackColor = true;
            this.chkPending.Click += new System.EventHandler(this.chkType_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSettings.Location = new System.Drawing.Point(900, 115);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(180, 50);
            this.btnSettings.TabIndex = 58;
            this.btnSettings.Text = "운영설정 (&T)";
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogin.Location = new System.Drawing.Point(40, 30);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(240, 70);
            this.btnLogin.TabIndex = 57;
            this.btnLogin.Text = "로그인(&L)";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(1109, 740);
            this.Controls.Add(this.txtGreetings);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.pnlOrders);
            this.Controls.Add(this.chkCompleted);
            this.Controls.Add(this.chkProgress);
            this.Controls.Add(this.chkPending);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.btnLogin);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "쿠팡이츠 주문 접수";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label txtGreetings;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.FlowLayoutPanel pnlOrders;
        private System.Windows.Forms.CheckBox chkCompleted;
        private System.Windows.Forms.CheckBox chkProgress;
        private System.Windows.Forms.CheckBox chkPending;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Button btnLogin;
    }
}

