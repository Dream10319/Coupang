namespace CoupangSub.Orders
{
    partial class DelayOrderForm
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOkay = new System.Windows.Forms.Button();
            this.txtNotices = new System.Windows.Forms.Label();
            this.cmbTimes = new System.Windows.Forms.ComboBox();
            this.btnMinus = new System.Windows.Forms.Button();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.lblMinutes = new System.Windows.Forms.Label();
            this.btnPlus = new System.Windows.Forms.Button();
            this.btnMinus5 = new System.Windows.Forms.Button();
            this.pnlTimes = new System.Windows.Forms.FlowLayoutPanel();
            this.btnPlus5 = new System.Windows.Forms.Button();
            this.txtError = new System.Windows.Forms.Label();
            this.pnlBusy1 = new System.Windows.Forms.FlowLayoutPanel();
            this.lblBusyTitle = new System.Windows.Forms.Label();
            this.chkNormal = new System.Windows.Forms.CheckBox();
            this.chkBusy = new System.Windows.Forms.CheckBox();
            this.pnlBusy2 = new System.Windows.Forms.FlowLayoutPanel();
            this.lblBusyTime = new System.Windows.Forms.Label();
            this.chkDuration1 = new System.Windows.Forms.CheckBox();
            this.chkDuration2 = new System.Windows.Forms.CheckBox();
            this.chkDuration3 = new System.Windows.Forms.CheckBox();
            this.pnlTimes.SuspendLayout();
            this.pnlBusy1.SuspendLayout();
            this.pnlBusy2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(310, 290);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(160, 50);
            this.btnCancel.TabIndex = 70;
            this.btnCancel.Text = "취소(&C)";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOkay
            // 
            this.btnOkay.Location = new System.Drawing.Point(110, 290);
            this.btnOkay.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btnOkay.Name = "btnOkay";
            this.btnOkay.Size = new System.Drawing.Size(160, 50);
            this.btnOkay.TabIndex = 67;
            this.btnOkay.Text = "확인(&O)";
            this.btnOkay.UseVisualStyleBackColor = true;
            // 
            // txtNotices
            // 
            this.txtNotices.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNotices.Location = new System.Drawing.Point(30, 30);
            this.txtNotices.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.txtNotices.Name = "txtNotices";
            this.txtNotices.Size = new System.Drawing.Size(520, 70);
            this.txtNotices.TabIndex = 65;
            this.txtNotices.Text = "입력한 조리시간이 고객에게 안내됩니다.";
            this.txtNotices.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbTimes
            // 
            this.cmbTimes.DropDownHeight = 120;
            this.cmbTimes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTimes.FormattingEnabled = true;
            this.cmbTimes.IntegralHeight = false;
            this.cmbTimes.Location = new System.Drawing.Point(170, 120);
            this.cmbTimes.Name = "cmbTimes";
            this.cmbTimes.Size = new System.Drawing.Size(240, 28);
            this.cmbTimes.TabIndex = 71;
            this.cmbTimes.SelectedIndexChanged += new System.EventHandler(this.cmbTimes_SelectedIndexChanged);
            // 
            // btnMinus
            // 
            this.btnMinus.Location = new System.Drawing.Point(0, 0);
            this.btnMinus.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btnMinus.Name = "btnMinus";
            this.btnMinus.Size = new System.Drawing.Size(40, 30);
            this.btnMinus.TabIndex = 60;
            this.btnMinus.Text = "-";
            this.btnMinus.UseVisualStyleBackColor = true;
            // 
            // txtValue
            // 
            this.txtValue.Location = new System.Drawing.Point(50, 2);
            this.txtValue.Margin = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.txtValue.Name = "txtValue";
            this.txtValue.ReadOnly = true;
            this.txtValue.Size = new System.Drawing.Size(110, 26);
            this.txtValue.TabIndex = 63;
            // 
            // lblMinutes
            // 
            this.lblMinutes.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMinutes.Location = new System.Drawing.Point(165, 0);
            this.lblMinutes.Margin = new System.Windows.Forms.Padding(5, 0, 10, 0);
            this.lblMinutes.Name = "lblMinutes";
            this.lblMinutes.Size = new System.Drawing.Size(25, 30);
            this.lblMinutes.TabIndex = 59;
            this.lblMinutes.Text = "분";
            this.lblMinutes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnPlus
            // 
            this.btnPlus.Location = new System.Drawing.Point(200, 0);
            this.btnPlus.Margin = new System.Windows.Forms.Padding(0);
            this.btnPlus.Name = "btnPlus";
            this.btnPlus.Size = new System.Drawing.Size(40, 30);
            this.btnPlus.TabIndex = 60;
            this.btnPlus.Text = "+";
            this.btnPlus.UseVisualStyleBackColor = true;
            // 
            // btnMinus5
            // 
            this.btnMinus5.Location = new System.Drawing.Point(120, 120);
            this.btnMinus5.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btnMinus5.Name = "btnMinus5";
            this.btnMinus5.Size = new System.Drawing.Size(40, 30);
            this.btnMinus5.TabIndex = 68;
            this.btnMinus5.Text = "-5";
            this.btnMinus5.UseVisualStyleBackColor = true;
            // 
            // pnlTimes
            // 
            this.pnlTimes.Controls.Add(this.btnMinus);
            this.pnlTimes.Controls.Add(this.txtValue);
            this.pnlTimes.Controls.Add(this.lblMinutes);
            this.pnlTimes.Controls.Add(this.btnPlus);
            this.pnlTimes.Location = new System.Drawing.Point(170, 120);
            this.pnlTimes.Name = "pnlTimes";
            this.pnlTimes.Size = new System.Drawing.Size(240, 30);
            this.pnlTimes.TabIndex = 72;
            // 
            // btnPlus5
            // 
            this.btnPlus5.Location = new System.Drawing.Point(420, 120);
            this.btnPlus5.Margin = new System.Windows.Forms.Padding(0);
            this.btnPlus5.Name = "btnPlus5";
            this.btnPlus5.Size = new System.Drawing.Size(40, 30);
            this.btnPlus5.TabIndex = 69;
            this.btnPlus5.Text = "+5";
            this.btnPlus5.UseVisualStyleBackColor = true;
            // 
            // txtError
            // 
            this.txtError.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtError.Location = new System.Drawing.Point(60, 240);
            this.txtError.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.txtError.Name = "txtError";
            this.txtError.Size = new System.Drawing.Size(460, 30);
            this.txtError.TabIndex = 66;
            this.txtError.Text = "조리시간이 길면";
            this.txtError.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlBusy1
            // 
            this.pnlBusy1.Controls.Add(this.lblBusyTitle);
            this.pnlBusy1.Controls.Add(this.chkNormal);
            this.pnlBusy1.Controls.Add(this.chkBusy);
            this.pnlBusy1.Location = new System.Drawing.Point(110, 160);
            this.pnlBusy1.Name = "pnlBusy1";
            this.pnlBusy1.Size = new System.Drawing.Size(360, 30);
            this.pnlBusy1.TabIndex = 73;
            // 
            // lblBusyTitle
            // 
            this.lblBusyTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBusyTitle.Location = new System.Drawing.Point(0, 0);
            this.lblBusyTitle.Margin = new System.Windows.Forms.Padding(0);
            this.lblBusyTitle.Name = "lblBusyTitle";
            this.lblBusyTitle.Size = new System.Drawing.Size(190, 30);
            this.lblBusyTitle.TabIndex = 74;
            this.lblBusyTitle.Text = "매장이 평소보다 바쁜가요?";
            this.lblBusyTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkNormal
            // 
            this.chkNormal.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkNormal.Location = new System.Drawing.Point(200, 0);
            this.chkNormal.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.chkNormal.Name = "chkNormal";
            this.chkNormal.Size = new System.Drawing.Size(60, 30);
            this.chkNormal.TabIndex = 75;
            this.chkNormal.Tag = "NORMAL";
            this.chkNormal.Text = "보통";
            this.chkNormal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkNormal.UseVisualStyleBackColor = true;
            this.chkNormal.Click += new System.EventHandler(this.chkStatus_Click);
            // 
            // chkBusy
            // 
            this.chkBusy.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkBusy.Location = new System.Drawing.Point(270, 0);
            this.chkBusy.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.chkBusy.Name = "chkBusy";
            this.chkBusy.Size = new System.Drawing.Size(60, 30);
            this.chkBusy.TabIndex = 76;
            this.chkBusy.Tag = "BUSY";
            this.chkBusy.Text = "바쁨";
            this.chkBusy.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkBusy.UseVisualStyleBackColor = true;
            this.chkBusy.Click += new System.EventHandler(this.chkStatus_Click);
            // 
            // pnlBusy2
            // 
            this.pnlBusy2.Controls.Add(this.lblBusyTime);
            this.pnlBusy2.Controls.Add(this.chkDuration1);
            this.pnlBusy2.Controls.Add(this.chkDuration2);
            this.pnlBusy2.Controls.Add(this.chkDuration3);
            this.pnlBusy2.Location = new System.Drawing.Point(110, 195);
            this.pnlBusy2.Name = "pnlBusy2";
            this.pnlBusy2.Size = new System.Drawing.Size(360, 30);
            this.pnlBusy2.TabIndex = 77;
            // 
            // lblBusyTime
            // 
            this.lblBusyTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBusyTime.Location = new System.Drawing.Point(0, 0);
            this.lblBusyTime.Margin = new System.Windows.Forms.Padding(0);
            this.lblBusyTime.Name = "lblBusyTime";
            this.lblBusyTime.Size = new System.Drawing.Size(190, 30);
            this.lblBusyTime.TabIndex = 78;
            this.lblBusyTime.Text = "다음 주문부터 더 필요한 시간:";
            this.lblBusyTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkDuration1
            // 
            this.chkDuration1.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkDuration1.Location = new System.Drawing.Point(200, 0);
            this.chkDuration1.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.chkDuration1.Name = "chkDuration1";
            this.chkDuration1.Size = new System.Drawing.Size(50, 30);
            this.chkDuration1.TabIndex = 80;
            this.chkDuration1.Tag = "10";
            this.chkDuration1.Text = "10분";
            this.chkDuration1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkDuration1.UseVisualStyleBackColor = true;
            this.chkDuration1.Click += new System.EventHandler(this.chkDuration_Click);
            // 
            // chkDuration2
            // 
            this.chkDuration2.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkDuration2.Location = new System.Drawing.Point(255, 0);
            this.chkDuration2.Margin = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.chkDuration2.Name = "chkDuration2";
            this.chkDuration2.Size = new System.Drawing.Size(50, 30);
            this.chkDuration2.TabIndex = 81;
            this.chkDuration2.Tag = "20";
            this.chkDuration2.Text = "20분";
            this.chkDuration2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkDuration2.UseVisualStyleBackColor = true;
            this.chkDuration2.Click += new System.EventHandler(this.chkDuration_Click);
            // 
            // chkDuration3
            // 
            this.chkDuration3.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkDuration3.Location = new System.Drawing.Point(310, 0);
            this.chkDuration3.Margin = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.chkDuration3.Name = "chkDuration3";
            this.chkDuration3.Size = new System.Drawing.Size(50, 30);
            this.chkDuration3.TabIndex = 82;
            this.chkDuration3.Tag = "30";
            this.chkDuration3.Text = "30분";
            this.chkDuration3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkDuration3.UseVisualStyleBackColor = true;
            this.chkDuration3.Click += new System.EventHandler(this.chkDuration_Click);
            // 
            // DelayOrderForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(579, 361);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOkay);
            this.Controls.Add(this.txtNotices);
            this.Controls.Add(this.cmbTimes);
            this.Controls.Add(this.btnMinus5);
            this.Controls.Add(this.pnlTimes);
            this.Controls.Add(this.btnPlus5);
            this.Controls.Add(this.txtError);
            this.Controls.Add(this.pnlBusy1);
            this.Controls.Add(this.pnlBusy2);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "DelayOrderForm";
            this.Text = "준비 지연";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DelayOrderForm_FormClosing);
            this.Load += new System.EventHandler(this.DelayOrderForm_Load);
            this.pnlTimes.ResumeLayout(false);
            this.pnlTimes.PerformLayout();
            this.pnlBusy1.ResumeLayout(false);
            this.pnlBusy2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOkay;
        private System.Windows.Forms.Label txtNotices;
        private System.Windows.Forms.ComboBox cmbTimes;
        private System.Windows.Forms.Button btnMinus;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.Label lblMinutes;
        private System.Windows.Forms.Button btnPlus;
        private System.Windows.Forms.Button btnMinus5;
        private System.Windows.Forms.FlowLayoutPanel pnlTimes;
        private System.Windows.Forms.Button btnPlus5;
        private System.Windows.Forms.Label txtError;
        private System.Windows.Forms.FlowLayoutPanel pnlBusy1;
        private System.Windows.Forms.FlowLayoutPanel pnlBusy2;
        private System.Windows.Forms.Label lblBusyTitle;
        private System.Windows.Forms.Label lblBusyTime;
        private System.Windows.Forms.CheckBox chkNormal;
        private System.Windows.Forms.CheckBox chkBusy;
        private System.Windows.Forms.CheckBox chkDuration1;
        private System.Windows.Forms.CheckBox chkDuration2;
        private System.Windows.Forms.CheckBox chkDuration3;
    }
}