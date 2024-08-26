namespace CoupangSub.Orders
{
    partial class AcceptOrderForm
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
            this.txtNotices = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOkay = new System.Windows.Forms.Button();
            this.cmbTimes = new System.Windows.Forms.ComboBox();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.lblMinutes = new System.Windows.Forms.Label();
            this.btnMinus = new System.Windows.Forms.Button();
            this.btnPlus = new System.Windows.Forms.Button();
            this.pnlTimes = new System.Windows.Forms.FlowLayoutPanel();
            this.btnMinus5 = new System.Windows.Forms.Button();
            this.btnPlus5 = new System.Windows.Forms.Button();
            this.txtError = new System.Windows.Forms.Label();
            this.pnlTimes.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtNotices
            // 
            this.txtNotices.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNotices.Location = new System.Drawing.Point(30, 30);
            this.txtNotices.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.txtNotices.Name = "txtNotices";
            this.txtNotices.Size = new System.Drawing.Size(520, 70);
            this.txtNotices.TabIndex = 59;
            this.txtNotices.Text = "입력한 조리시간이 고객에게 안내됩니다.";
            this.txtNotices.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(300, 250);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(160, 50);
            this.btnCancel.TabIndex = 61;
            this.btnCancel.Text = "취소(&C)";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOkay
            // 
            this.btnOkay.Location = new System.Drawing.Point(100, 250);
            this.btnOkay.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btnOkay.Name = "btnOkay";
            this.btnOkay.Size = new System.Drawing.Size(160, 50);
            this.btnOkay.TabIndex = 60;
            this.btnOkay.Text = "확인(&O)";
            this.btnOkay.UseVisualStyleBackColor = true;
            this.btnOkay.Click += new System.EventHandler(this.btnOkay_Click);
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
            this.cmbTimes.TabIndex = 62;
            this.cmbTimes.SelectedIndexChanged += new System.EventHandler(this.cmbTimes_SelectedIndexChanged);
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
            // btnMinus
            // 
            this.btnMinus.Location = new System.Drawing.Point(0, 0);
            this.btnMinus.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btnMinus.Name = "btnMinus";
            this.btnMinus.Size = new System.Drawing.Size(40, 30);
            this.btnMinus.TabIndex = 60;
            this.btnMinus.Text = "-";
            this.btnMinus.UseVisualStyleBackColor = true;
            this.btnMinus.Click += new System.EventHandler(this.btnMinus_Click);
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
            this.btnPlus.Click += new System.EventHandler(this.btnPlus_Click);
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
            this.pnlTimes.TabIndex = 64;
            // 
            // btnMinus5
            // 
            this.btnMinus5.Location = new System.Drawing.Point(120, 120);
            this.btnMinus5.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btnMinus5.Name = "btnMinus5";
            this.btnMinus5.Size = new System.Drawing.Size(40, 30);
            this.btnMinus5.TabIndex = 60;
            this.btnMinus5.Text = "-5";
            this.btnMinus5.UseVisualStyleBackColor = true;
            this.btnMinus5.Click += new System.EventHandler(this.btnMinus5_Click);
            // 
            // btnPlus5
            // 
            this.btnPlus5.Location = new System.Drawing.Point(420, 120);
            this.btnPlus5.Margin = new System.Windows.Forms.Padding(0);
            this.btnPlus5.Name = "btnPlus5";
            this.btnPlus5.Size = new System.Drawing.Size(40, 30);
            this.btnPlus5.TabIndex = 60;
            this.btnPlus5.Text = "+5";
            this.btnPlus5.UseVisualStyleBackColor = true;
            this.btnPlus5.Click += new System.EventHandler(this.btnPlus5_Click);
            // 
            // txtError
            // 
            this.txtError.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtError.Location = new System.Drawing.Point(50, 180);
            this.txtError.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.txtError.Name = "txtError";
            this.txtError.Size = new System.Drawing.Size(460, 30);
            this.txtError.TabIndex = 59;
            this.txtError.Text = "조리시간이 길면";
            this.txtError.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // AcceptOrderForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(579, 321);
            this.Controls.Add(this.btnMinus5);
            this.Controls.Add(this.pnlTimes);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnPlus5);
            this.Controls.Add(this.btnOkay);
            this.Controls.Add(this.txtError);
            this.Controls.Add(this.txtNotices);
            this.Controls.Add(this.cmbTimes);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "AcceptOrderForm";
            this.Text = "주문 접수";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AcceptOrderForm_FormClosing);
            this.Load += new System.EventHandler(this.AcceptOrderForm_Load);
            this.pnlTimes.ResumeLayout(false);
            this.pnlTimes.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label txtNotices;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOkay;
        private System.Windows.Forms.ComboBox cmbTimes;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.Label lblMinutes;
        private System.Windows.Forms.Button btnMinus;
        private System.Windows.Forms.Button btnPlus;
        private System.Windows.Forms.FlowLayoutPanel pnlTimes;
        private System.Windows.Forms.Button btnMinus5;
        private System.Windows.Forms.Button btnPlus5;
        private System.Windows.Forms.Label txtError;
    }
}