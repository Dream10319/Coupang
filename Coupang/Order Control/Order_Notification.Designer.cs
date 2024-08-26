namespace Coupang
{
    partial class Order_Notification
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
            this.timer1 = new System.Windows.Forms.Timer();
            this.label1 = new System.Windows.Forms.Label();
            this.abbrOrderId = new System.Windows.Forms.Label();
            this.orderStoreId = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.orderServiceType = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 2;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label1.Location = new System.Drawing.Point(355, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(18, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "X";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // abbrOrderId
            // 
            this.abbrOrderId.Location = new System.Drawing.Point(77, 28);
            this.abbrOrderId.Name = "abbrOrderId";
            this.abbrOrderId.Size = new System.Drawing.Size(233, 18);
            this.abbrOrderId.TabIndex = 1;
            this.abbrOrderId.Text = "...";
            this.abbrOrderId.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // orderStoreId
            // 
            this.orderStoreId.AutoSize = true;
            this.orderStoreId.Location = new System.Drawing.Point(148, 104);
            this.orderStoreId.Name = "orderStoreId";
            this.orderStoreId.Size = new System.Drawing.Size(23, 18);
            this.orderStoreId.TabIndex = 2;
            this.orderStoreId.Text = "...";
            this.orderStoreId.Click += new System.EventHandler(this.orderStoreId_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 18);
            this.label2.TabIndex = 3;
            this.label2.Text = "Order Store ID";
            // 
            // orderServiceType
            // 
            this.orderServiceType.AutoSize = true;
            this.orderServiceType.Location = new System.Drawing.Point(187, 76);
            this.orderServiceType.Name = "orderServiceType";
            this.orderServiceType.Size = new System.Drawing.Size(23, 18);
            this.orderServiceType.TabIndex = 4;
            this.orderServiceType.Text = "...";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(153, 18);
            this.label3.TabIndex = 5;
            this.label3.Text = "Order Service Type";
            // 
            // Order_Notification
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 144);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.orderServiceType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.orderStoreId);
            this.Controls.Add(this.abbrOrderId);
            this.Controls.Add(this.label1);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(386, 144);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(0, 144);
            this.Name = "Order_Notification";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Order_Notification";
            this.Load += new System.EventHandler(this.Order_Notification_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Label orderStoreId;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Label orderServiceType;
        private System.Windows.Forms.Label label3;
        internal System.Windows.Forms.Label abbrOrderId;
    }
}