namespace Coupang.Controls
{
    partial class Order_Item
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
            this.abbrOrderId = new System.Windows.Forms.Label();
            this.O_Amount = new System.Windows.Forms.Label();
            this.O_Price = new System.Windows.Forms.Label();
            this.note = new System.Windows.Forms.Label();
            this.Order_Time = new System.Windows.Forms.Label();
            this.O_modifiers = new System.Windows.Forms.FlowLayoutPanel();
            this.O_status = new System.Windows.Forms.Label();
            this.O_orderServiceType = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // abbrOrderId
            // 
            this.abbrOrderId.AutoSize = true;
            this.abbrOrderId.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.abbrOrderId.Location = new System.Drawing.Point(20, 22);
            this.abbrOrderId.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.abbrOrderId.Name = "abbrOrderId";
            this.abbrOrderId.Size = new System.Drawing.Size(84, 23);
            this.abbrOrderId.TabIndex = 0;
            this.abbrOrderId.Text = "16G800";
            // 
            // O_Amount
            // 
            this.O_Amount.AutoSize = true;
            this.O_Amount.Location = new System.Drawing.Point(249, 26);
            this.O_Amount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.O_Amount.Name = "O_Amount";
            this.O_Amount.Size = new System.Drawing.Size(55, 18);
            this.O_Amount.TabIndex = 1;
            this.O_Amount.Text = "label2";
            this.O_Amount.Visible = false;
            // 
            // O_Price
            // 
            this.O_Price.AutoSize = true;
            this.O_Price.Location = new System.Drawing.Point(368, 26);
            this.O_Price.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.O_Price.Name = "O_Price";
            this.O_Price.Size = new System.Drawing.Size(55, 18);
            this.O_Price.TabIndex = 2;
            this.O_Price.Text = "label4";
            this.O_Price.Visible = false;
            // 
            // note
            // 
            this.note.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.note.BackColor = System.Drawing.Color.MistyRose;
            this.note.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.note.ForeColor = System.Drawing.Color.Red;
            this.note.Location = new System.Drawing.Point(140, 85);
            this.note.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.note.Name = "note";
            this.note.Size = new System.Drawing.Size(346, 31);
            this.note.TabIndex = 3;
            this.note.Text = "[수저포크X]";
            this.note.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Order_Time
            // 
            this.Order_Time.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Order_Time.AutoSize = true;
            this.Order_Time.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Order_Time.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.Order_Time.Location = new System.Drawing.Point(20, 94);
            this.Order_Time.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Order_Time.Name = "Order_Time";
            this.Order_Time.Size = new System.Drawing.Size(55, 19);
            this.Order_Time.TabIndex = 4;
            this.Order_Time.Text = "10:09";
            // 
            // O_modifiers
            // 
            this.O_modifiers.AutoSize = true;
            this.O_modifiers.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.O_modifiers.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.O_modifiers.Location = new System.Drawing.Point(19, 84);
            this.O_modifiers.Name = "O_modifiers";
            this.O_modifiers.Size = new System.Drawing.Size(0, 0);
            this.O_modifiers.TabIndex = 5;
            // 
            // O_status
            // 
            this.O_status.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.O_status.BackColor = System.Drawing.Color.Transparent;
            this.O_status.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.O_status.ForeColor = System.Drawing.SystemColors.ControlText;
            this.O_status.Location = new System.Drawing.Point(317, 55);
            this.O_status.Name = "O_status";
            this.O_status.Size = new System.Drawing.Size(169, 29);
            this.O_status.TabIndex = 6;
            this.O_status.Text = "label1";
            this.O_status.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // O_orderServiceType
            // 
            this.O_orderServiceType.AutoSize = true;
            this.O_orderServiceType.Location = new System.Drawing.Point(21, 55);
            this.O_orderServiceType.Name = "O_orderServiceType";
            this.O_orderServiceType.Size = new System.Drawing.Size(55, 18);
            this.O_orderServiceType.TabIndex = 7;
            this.O_orderServiceType.Text = "label1";
            // 
            // Order_Item
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.Controls.Add(this.O_orderServiceType);
            this.Controls.Add(this.O_status);
            this.Controls.Add(this.note);
            this.Controls.Add(this.O_modifiers);
            this.Controls.Add(this.Order_Time);
            this.Controls.Add(this.O_Price);
            this.Controls.Add(this.O_Amount);
            this.Controls.Add(this.abbrOrderId);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Order_Item";
            this.Size = new System.Drawing.Size(517, 151);
  
       
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        internal System.Windows.Forms.FlowLayoutPanel O_modifiers;
        internal System.Windows.Forms.Label abbrOrderId;
        internal System.Windows.Forms.Label O_Amount;
        internal System.Windows.Forms.Label O_Price;
        internal System.Windows.Forms.Label note;
        internal System.Windows.Forms.Label Order_Time;
        internal System.Windows.Forms.Label O_status;
        internal System.Windows.Forms.Label O_orderServiceType;
    }
}
