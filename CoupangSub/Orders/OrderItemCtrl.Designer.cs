namespace CoupangSub.Orders
{
    partial class OrderItemCtrl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnAction3 = new System.Windows.Forms.Button();
            this.btnAction1 = new System.Windows.Forms.Button();
            this.txtAddress = new System.Windows.Forms.Label();
            this.txtPayment = new System.Windows.Forms.Label();
            this.txtMenuName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.Label();
            this.txtTime = new System.Windows.Forms.Label();
            this.btnAction2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnAction3
            // 
            this.btnAction3.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAction3.Location = new System.Drawing.Point(840, 95);
            this.btnAction3.Name = "btnAction3";
            this.btnAction3.Size = new System.Drawing.Size(160, 40);
            this.btnAction3.TabIndex = 44;
            this.btnAction3.Text = "거부";
            this.btnAction3.UseVisualStyleBackColor = true;
            this.btnAction3.Click += new System.EventHandler(this.btnAction_Click);
            // 
            // btnAction1
            // 
            this.btnAction1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAction1.Location = new System.Drawing.Point(494, 95);
            this.btnAction1.Name = "btnAction1";
            this.btnAction1.Size = new System.Drawing.Size(160, 40);
            this.btnAction1.TabIndex = 45;
            this.btnAction1.Text = "완료알림 발송(&N)";
            this.btnAction1.UseVisualStyleBackColor = true;
            this.btnAction1.Click += new System.EventHandler(this.btnAction_Click);
            // 
            // txtAddress
            // 
            this.txtAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddress.Location = new System.Drawing.Point(20, 55);
            this.txtAddress.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(980, 30);
            this.txtAddress.TabIndex = 39;
            this.txtAddress.Text = "대구 달서구 성당동 55-1 12";
            this.txtAddress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPayment
            // 
            this.txtPayment.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPayment.Location = new System.Drawing.Point(20, 100);
            this.txtPayment.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.txtPayment.Name = "txtPayment";
            this.txtPayment.Size = new System.Drawing.Size(400, 30);
            this.txtPayment.TabIndex = 40;
            this.txtPayment.Text = "결제완료 20,500원";
            this.txtPayment.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtMenuName
            // 
            this.txtMenuName.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMenuName.Location = new System.Drawing.Point(380, 10);
            this.txtMenuName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.txtMenuName.Name = "txtMenuName";
            this.txtMenuName.Size = new System.Drawing.Size(620, 30);
            this.txtMenuName.TabIndex = 41;
            this.txtMenuName.Text = "단비 순대 1개";
            this.txtMenuName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtName
            // 
            this.txtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.Location = new System.Drawing.Point(20, 10);
            this.txtName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(140, 30);
            this.txtName.TabIndex = 42;
            this.txtName.Text = "배달 PONR";
            this.txtName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtName.Click += new System.EventHandler(this.txtName_Click);
            // 
            // txtTime
            // 
            this.txtTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTime.Location = new System.Drawing.Point(180, 10);
            this.txtTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.txtTime.Name = "txtTime";
            this.txtTime.Size = new System.Drawing.Size(180, 30);
            this.txtTime.TabIndex = 43;
            this.txtTime.Text = "2023-05-22 15:58:58";
            this.txtTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnAction2
            // 
            this.btnAction2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAction2.Location = new System.Drawing.Point(670, 95);
            this.btnAction2.Name = "btnAction2";
            this.btnAction2.Size = new System.Drawing.Size(160, 40);
            this.btnAction2.TabIndex = 45;
            this.btnAction2.Text = "완료알림 발송(&N)";
            this.btnAction2.UseVisualStyleBackColor = true;
            this.btnAction2.Click += new System.EventHandler(this.btnAction_Click);
            // 
            // OrderItemCtrl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.btnAction3);
            this.Controls.Add(this.btnAction2);
            this.Controls.Add(this.btnAction1);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.txtPayment);
            this.Controls.Add(this.txtMenuName);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtTime);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "OrderItemCtrl";
            this.Size = new System.Drawing.Size(1018, 148);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAction3;
        private System.Windows.Forms.Button btnAction1;
        private System.Windows.Forms.Label txtAddress;
        private System.Windows.Forms.Label txtPayment;
        private System.Windows.Forms.Label txtMenuName;
        private System.Windows.Forms.Label txtName;
        private System.Windows.Forms.Label txtTime;
        private System.Windows.Forms.Button btnAction2;
    }
}
