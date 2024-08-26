namespace CoupangWeb.Reviews
{
    partial class ReviewItemCtrl
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
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.txtRating = new System.Windows.Forms.Label();
            this.txtDate = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.Label();
            this.txtOrderInfo = new System.Windows.Forms.TextBox();
            this.txtMenus = new System.Windows.Forms.TextBox();
            this.pnlImages = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAddReply = new System.Windows.Forms.Button();
            this.pnlComments = new System.Windows.Forms.FlowLayoutPanel();
            this.lblBorder = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtTitle
            // 
            this.txtTitle.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTitle.Location = new System.Drawing.Point(125, 10);
            this.txtTitle.Multiline = true;
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.ReadOnly = true;
            this.txtTitle.Size = new System.Drawing.Size(710, 40);
            this.txtTitle.TabIndex = 59;
            this.txtTitle.Text = "맛있어요!";
            // 
            // txtRating
            // 
            this.txtRating.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRating.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtRating.Location = new System.Drawing.Point(15, 50);
            this.txtRating.Name = "txtRating";
            this.txtRating.Size = new System.Drawing.Size(100, 30);
            this.txtRating.TabIndex = 56;
            this.txtRating.Text = "☆☆☆☆☆";
            this.txtRating.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtDate
            // 
            this.txtDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDate.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.txtDate.Location = new System.Drawing.Point(15, 90);
            this.txtDate.Name = "txtDate";
            this.txtDate.Size = new System.Drawing.Size(100, 30);
            this.txtDate.TabIndex = 57;
            this.txtDate.Text = "2022-11-18";
            this.txtDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtUserName
            // 
            this.txtUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserName.Location = new System.Drawing.Point(15, 10);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(100, 30);
            this.txtUserName.TabIndex = 58;
            this.txtUserName.Text = "온에어";
            this.txtUserName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtOrderInfo
            // 
            this.txtOrderInfo.Location = new System.Drawing.Point(335, 60);
            this.txtOrderInfo.Multiline = true;
            this.txtOrderInfo.Name = "txtOrderInfo";
            this.txtOrderInfo.ReadOnly = true;
            this.txtOrderInfo.Size = new System.Drawing.Size(160, 80);
            this.txtOrderInfo.TabIndex = 61;
            this.txtOrderInfo.Text = "주문일자 2022-11-03\r\n주문번호 xxx\r\n수령방식 포장";
            // 
            // txtMenus
            // 
            this.txtMenus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMenus.Location = new System.Drawing.Point(125, 60);
            this.txtMenus.Multiline = true;
            this.txtMenus.Name = "txtMenus";
            this.txtMenus.ReadOnly = true;
            this.txtMenus.Size = new System.Drawing.Size(200, 80);
            this.txtMenus.TabIndex = 60;
            this.txtMenus.Text = "주문메뉴\r\n신전치즈컵밥\r\n치즈떡볶이";
            // 
            // pnlImages
            // 
            this.pnlImages.AutoScroll = true;
            this.pnlImages.Location = new System.Drawing.Point(505, 60);
            this.pnlImages.Name = "pnlImages";
            this.pnlImages.Size = new System.Drawing.Size(330, 80);
            this.pnlImages.TabIndex = 62;
            this.pnlImages.WrapContents = false;
            // 
            // btnAddReply
            // 
            this.btnAddReply.Location = new System.Drawing.Point(15, 150);
            this.btnAddReply.Name = "btnAddReply";
            this.btnAddReply.Size = new System.Drawing.Size(100, 60);
            this.btnAddReply.TabIndex = 63;
            this.btnAddReply.Text = "사장님 댓글\r\n등록하기";
            this.btnAddReply.UseVisualStyleBackColor = true;
            this.btnAddReply.Click += new System.EventHandler(this.btnAddReply_Click);
            // 
            // pnlComments
            // 
            this.pnlComments.AutoScroll = true;
            this.pnlComments.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.pnlComments.Location = new System.Drawing.Point(125, 150);
            this.pnlComments.Name = "pnlComments";
            this.pnlComments.Size = new System.Drawing.Size(710, 110);
            this.pnlComments.TabIndex = 64;
            this.pnlComments.WrapContents = false;
            // 
            // lblBorder
            // 
            this.lblBorder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblBorder.Location = new System.Drawing.Point(15, 268);
            this.lblBorder.Name = "lblBorder";
            this.lblBorder.Size = new System.Drawing.Size(820, 2);
            this.lblBorder.TabIndex = 65;
            // 
            // ReviewItemCtrl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.lblBorder);
            this.Controls.Add(this.pnlComments);
            this.Controls.Add(this.btnAddReply);
            this.Controls.Add(this.pnlImages);
            this.Controls.Add(this.txtOrderInfo);
            this.Controls.Add(this.txtMenus);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.txtRating);
            this.Controls.Add(this.txtDate);
            this.Controls.Add(this.txtUserName);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ReviewItemCtrl";
            this.Size = new System.Drawing.Size(850, 270);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label txtRating;
        private System.Windows.Forms.Label txtDate;
        private System.Windows.Forms.Label txtUserName;
        private System.Windows.Forms.TextBox txtOrderInfo;
        private System.Windows.Forms.TextBox txtMenus;
        private System.Windows.Forms.FlowLayoutPanel pnlImages;
        private System.Windows.Forms.Button btnAddReply;
        private System.Windows.Forms.FlowLayoutPanel pnlComments;
        private System.Windows.Forms.Label lblBorder;
    }
}
