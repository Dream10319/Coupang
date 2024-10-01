namespace CoupangWeb.Reviews
{
    partial class ReplyItemCtrl
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
            this.txtName = new System.Windows.Forms.Label();
            this.btnUpdateComment = new System.Windows.Forms.Button();
            this.btnDeleteComment = new System.Windows.Forms.Button();
            this.txtReply = new System.Windows.Forms.TextBox();
            this.txtDate = new System.Windows.Forms.Label();
            this.pnlButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtName
            // 
            this.txtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.Location = new System.Drawing.Point(10, 10);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(100, 30);
            this.txtName.TabIndex = 64;
            this.txtName.Text = "사장님";
            this.txtName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnUpdateComment
            // 
            this.btnUpdateComment.Location = new System.Drawing.Point(70, 0);
            this.btnUpdateComment.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnUpdateComment.Name = "btnUpdateComment";
            this.btnUpdateComment.Size = new System.Drawing.Size(60, 30);
            this.btnUpdateComment.TabIndex = 55;
            this.btnUpdateComment.Text = "수정";
            this.btnUpdateComment.UseVisualStyleBackColor = true;
            this.btnUpdateComment.Visible = false;
            this.btnUpdateComment.Click += btnUpdateComment_Click;
            // 
            // btnDeleteComment
            // 
            this.btnDeleteComment.Location = new System.Drawing.Point(0, 0);
            this.btnDeleteComment.Margin = new System.Windows.Forms.Padding(0);
            this.btnDeleteComment.Name = "btnDeleteComment";
            this.btnDeleteComment.Size = new System.Drawing.Size(60, 30);
            this.btnDeleteComment.TabIndex = 56;
            this.btnDeleteComment.Text = "삭제";
            this.btnDeleteComment.UseVisualStyleBackColor = true;
            this.btnDeleteComment.Click += btnDeleteComment_Click;
            // 
            // txtReply
            // 
            this.txtReply.Location = new System.Drawing.Point(20, 45);
            this.txtReply.Multiline = true;
            this.txtReply.Name = "txtReply";
            this.txtReply.ReadOnly = true;
            this.txtReply.Size = new System.Drawing.Size(650, 55);
            this.txtReply.TabIndex = 65;
            this.txtReply.Text = "감사해요.\r\n또 와주세요";
            // 
            // txtDate
            // 
            this.txtDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDate.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.txtDate.Location = new System.Drawing.Point(130, 11);
            this.txtDate.Name = "txtDate";
            this.txtDate.Size = new System.Drawing.Size(100, 30);
            this.txtDate.TabIndex = 63;
            this.txtDate.Text = "2022-11-18";
            this.txtDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlButtons
            // 
            this.pnlButtons.AutoSize = true;
            this.pnlButtons.Controls.Add(this.btnUpdateComment);
            this.pnlButtons.Controls.Add(this.btnDeleteComment);
            this.pnlButtons.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.pnlButtons.Location = new System.Drawing.Point(540, 10);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(130, 30);
            this.pnlButtons.TabIndex = 62;
            this.pnlButtons.WrapContents = false;
            // 
            // ReplyItemCtrl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtReply);
            this.Controls.Add(this.txtDate);
            this.Controls.Add(this.pnlButtons);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ReplyItemCtrl";
            this.Size = new System.Drawing.Size(680, 105);
            this.pnlButtons.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void BtnDeleteComment_Click(object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        private System.Windows.Forms.Label txtName;
        private System.Windows.Forms.Button btnUpdateComment;
        private System.Windows.Forms.Button btnDeleteComment;
        private System.Windows.Forms.TextBox txtReply;
        private System.Windows.Forms.Label txtDate;
        private System.Windows.Forms.FlowLayoutPanel pnlButtons;
    }
}
