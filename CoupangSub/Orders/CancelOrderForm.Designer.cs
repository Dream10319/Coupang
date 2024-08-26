namespace CoupangSub.Orders
{
    partial class CancelOrderForm
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
            this.clbReasons = new System.Windows.Forms.CheckedListBox();
            this.pnlMenus = new System.Windows.Forms.FlowLayoutPanel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOkay = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // clbReasons
            // 
            this.clbReasons.CheckOnClick = true;
            this.clbReasons.FormattingEnabled = true;
            this.clbReasons.Location = new System.Drawing.Point(30, 30);
            this.clbReasons.Name = "clbReasons";
            this.clbReasons.Size = new System.Drawing.Size(300, 382);
            this.clbReasons.TabIndex = 38;
            this.clbReasons.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbReasons_ItemCheck);
            // 
            // pnlMenus
            // 
            this.pnlMenus.AutoScroll = true;
            this.pnlMenus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMenus.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.pnlMenus.Location = new System.Drawing.Point(350, 30);
            this.pnlMenus.Name = "pnlMenus";
            this.pnlMenus.Padding = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.pnlMenus.Size = new System.Drawing.Size(280, 382);
            this.pnlMenus.TabIndex = 37;
            this.pnlMenus.WrapContents = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(450, 430);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(160, 50);
            this.btnCancel.TabIndex = 36;
            this.btnCancel.Text = "취소(&C)";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOkay
            // 
            this.btnOkay.Location = new System.Drawing.Point(50, 430);
            this.btnOkay.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btnOkay.Name = "btnOkay";
            this.btnOkay.Size = new System.Drawing.Size(160, 50);
            this.btnOkay.TabIndex = 35;
            this.btnOkay.Text = "확인(&O)";
            this.btnOkay.UseVisualStyleBackColor = true;
            this.btnOkay.Click += new System.EventHandler(this.btnOkay_Click);
            // 
            // CancelOrderForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(659, 501);
            this.Controls.Add(this.clbReasons);
            this.Controls.Add(this.pnlMenus);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOkay);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "CancelOrderForm";
            this.Text = "주문 취소";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CancelOrderForm_FormClosing);
            this.Load += new System.EventHandler(this.CancelOrderForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox clbReasons;
        private System.Windows.Forms.FlowLayoutPanel pnlMenus;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOkay;
    }
}