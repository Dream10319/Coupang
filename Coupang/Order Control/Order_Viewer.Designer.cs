namespace Coupang
{
    partial class Order_Viewer
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
            this.label1 = new System.Windows.Forms.Label();
            this.Order_No = new System.Windows.Forms.Label();
            this.customerOrderCount = new System.Windows.Forms.Label();
            this.Order_Items = new System.Windows.Forms.FlowLayoutPanel();
            this.Total = new System.Windows.Forms.Label();
            this.totalAmount = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.Order_Time = new System.Windows.Forms.Label();
            this.O_status = new System.Windows.Forms.Label();
            this.Order_Control = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "주문번호";
            // 
            // Order_No
            // 
            this.Order_No.AutoSize = true;
            this.Order_No.Location = new System.Drawing.Point(96, 30);
            this.Order_No.Name = "Order_No";
            this.Order_No.Size = new System.Drawing.Size(23, 18);
            this.Order_No.TabIndex = 1;
            this.Order_No.Text = "...";
            // 
            // customerOrderCount
            // 
            this.customerOrderCount.AutoSize = true;
            this.customerOrderCount.Location = new System.Drawing.Point(247, 65);
            this.customerOrderCount.Name = "customerOrderCount";
            this.customerOrderCount.Size = new System.Drawing.Size(23, 18);
            this.customerOrderCount.TabIndex = 2;
            this.customerOrderCount.Text = "...";
            // 
            // Order_Items
            // 
            this.Order_Items.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Order_Items.Location = new System.Drawing.Point(17, 99);
            this.Order_Items.Name = "Order_Items";
            this.Order_Items.Size = new System.Drawing.Size(589, 287);
            this.Order_Items.TabIndex = 9;
            // 
            // Total
            // 
            this.Total.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Total.Location = new System.Drawing.Point(401, 389);
            this.Total.Name = "Total";
            this.Total.Size = new System.Drawing.Size(205, 18);
            this.Total.TabIndex = 11;
            this.Total.Text = "...";
            this.Total.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // totalAmount
            // 
            this.totalAmount.AutoSize = true;
            this.totalAmount.Location = new System.Drawing.Point(17, 389);
            this.totalAmount.Name = "totalAmount";
            this.totalAmount.Size = new System.Drawing.Size(60, 18);
            this.totalAmount.TabIndex = 10;
            this.totalAmount.Text = "최종금액";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 18);
            this.label4.TabIndex = 14;
            this.label4.Text = "고객 주문 횟수:";
            // 
            // Order_Time
            // 
            this.Order_Time.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Order_Time.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.Order_Time.Location = new System.Drawing.Point(425, 65);
            this.Order_Time.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Order_Time.Name = "Order_Time";
            this.Order_Time.Size = new System.Drawing.Size(181, 19);
            this.Order_Time.TabIndex = 15;
            this.Order_Time.Text = "10:09";
            this.Order_Time.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // O_status
            // 
            this.O_status.BackColor = System.Drawing.Color.Transparent;
            this.O_status.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.O_status.ForeColor = System.Drawing.SystemColors.ControlText;
            this.O_status.Location = new System.Drawing.Point(361, 18);
            this.O_status.Name = "O_status";
            this.O_status.Size = new System.Drawing.Size(245, 21);
            this.O_status.TabIndex = 16;
            this.O_status.Text = "...";
            this.O_status.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Order_Control
            // 
            this.Order_Control.Location = new System.Drawing.Point(127, 423);
            this.Order_Control.Name = "Order_Control";
            this.Order_Control.Size = new System.Drawing.Size(369, 62);
            this.Order_Control.TabIndex = 17;
            // 
            // Order_Viewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(623, 497);
            this.Controls.Add(this.Order_Control);
            this.Controls.Add(this.O_status);
            this.Controls.Add(this.Order_Time);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Total);
            this.Controls.Add(this.totalAmount);
            this.Controls.Add(this.Order_Items);
            this.Controls.Add(this.customerOrderCount);
            this.Controls.Add(this.Order_No);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "Order_Viewer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "주문정보";
            this.Load += new System.EventHandler(this.Order_Viewer_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Label Order_No;
        internal System.Windows.Forms.Label customerOrderCount;
        internal System.Windows.Forms.FlowLayoutPanel Order_Items;
        internal System.Windows.Forms.Label Total;
        private System.Windows.Forms.Label totalAmount;
        private System.Windows.Forms.Label label4;
        internal System.Windows.Forms.Label Order_Time;
        internal System.Windows.Forms.Label O_status;
        private System.Windows.Forms.FlowLayoutPanel Order_Control;
    }
}