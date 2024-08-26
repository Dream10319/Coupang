namespace CoupangWeb.Reviews
{
    partial class ReviewListForm
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
            this.cmbStores = new System.Windows.Forms.ComboBox();
            this.txtAvgRating = new System.Windows.Forms.Label();
            this.txtTotalCount = new System.Windows.Forms.Label();
            this.txtReplyCount = new System.Windows.Forms.Label();
            this.txtNoReplyCount = new System.Windows.Forms.Label();
            this.dtcStart = new System.Windows.Forms.DateTimePicker();
            this.dtcEnds = new System.Windows.Forms.DateTimePicker();
            this.btnInquiry = new System.Windows.Forms.Button();
            this.chkBlock = new System.Windows.Forms.CheckBox();
            this.chkNoRes = new System.Windows.Forms.CheckBox();
            this.chkTotal = new System.Windows.Forms.CheckBox();
            this.btnLastPage = new System.Windows.Forms.Button();
            this.btnNextPage = new System.Windows.Forms.Button();
            this.btnFirstPage = new System.Windows.Forms.Button();
            this.btnPrevPage = new System.Windows.Forms.Button();
            this.txtPageNo = new System.Windows.Forms.Label();
            this.pnlReviews = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // cmbStores
            // 
            this.cmbStores.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStores.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbStores.FormattingEnabled = true;
            this.cmbStores.Location = new System.Drawing.Point(30, 20);
            this.cmbStores.Name = "cmbStores";
            this.cmbStores.Size = new System.Drawing.Size(880, 32);
            this.cmbStores.TabIndex = 63;
            this.cmbStores.SelectedIndexChanged += new System.EventHandler(this.cmbStores_SelectedIndexChanged);
            // 
            // txtAvgRating
            // 
            this.txtAvgRating.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAvgRating.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtAvgRating.Location = new System.Drawing.Point(50, 70);
            this.txtAvgRating.Name = "txtAvgRating";
            this.txtAvgRating.Size = new System.Drawing.Size(204, 50);
            this.txtAvgRating.TabIndex = 64;
            this.txtAvgRating.Text = "평균별점 (최근 12주 기준)\r\n4.85";
            this.txtAvgRating.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtTotalCount
            // 
            this.txtTotalCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalCount.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtTotalCount.Location = new System.Drawing.Point(350, 70);
            this.txtTotalCount.Name = "txtTotalCount";
            this.txtTotalCount.Size = new System.Drawing.Size(140, 50);
            this.txtTotalCount.TabIndex = 64;
            this.txtTotalCount.Text = "전체\r\n4개";
            this.txtTotalCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtReplyCount
            // 
            this.txtReplyCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReplyCount.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtReplyCount.Location = new System.Drawing.Point(550, 70);
            this.txtReplyCount.Name = "txtReplyCount";
            this.txtReplyCount.Size = new System.Drawing.Size(140, 50);
            this.txtReplyCount.TabIndex = 64;
            this.txtReplyCount.Text = "답변\r\n1개";
            this.txtReplyCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtNoReplyCount
            // 
            this.txtNoReplyCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNoReplyCount.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtNoReplyCount.Location = new System.Drawing.Point(750, 70);
            this.txtNoReplyCount.Name = "txtNoReplyCount";
            this.txtNoReplyCount.Size = new System.Drawing.Size(140, 50);
            this.txtNoReplyCount.TabIndex = 64;
            this.txtNoReplyCount.Text = "미답변\r\n3개";
            this.txtNoReplyCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtcStart
            // 
            this.dtcStart.CustomFormat = "yyyy-MM-dd";
            this.dtcStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtcStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtcStart.Location = new System.Drawing.Point(30, 145);
            this.dtcStart.MaxDate = new System.DateTime(2099, 12, 31, 0, 0, 0, 0);
            this.dtcStart.MinDate = new System.DateTime(2010, 1, 1, 0, 0, 0, 0);
            this.dtcStart.Name = "dtcStart";
            this.dtcStart.Size = new System.Drawing.Size(140, 29);
            this.dtcStart.TabIndex = 65;
            this.dtcStart.Value = new System.DateTime(2022, 12, 8, 1, 23, 33, 0);
            this.dtcStart.ValueChanged += new System.EventHandler(this.dtcStart_ValueChanged);
            // 
            // dtcEnds
            // 
            this.dtcEnds.CustomFormat = "yyyy-MM-dd";
            this.dtcEnds.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtcEnds.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtcEnds.Location = new System.Drawing.Point(180, 145);
            this.dtcEnds.MaxDate = new System.DateTime(2099, 12, 31, 0, 0, 0, 0);
            this.dtcEnds.MinDate = new System.DateTime(2010, 1, 1, 0, 0, 0, 0);
            this.dtcEnds.Name = "dtcEnds";
            this.dtcEnds.Size = new System.Drawing.Size(140, 29);
            this.dtcEnds.TabIndex = 66;
            this.dtcEnds.Value = new System.DateTime(2022, 12, 8, 1, 23, 33, 0);
            this.dtcEnds.ValueChanged += new System.EventHandler(this.dtcEnds_ValueChanged);
            // 
            // btnInquiry
            // 
            this.btnInquiry.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInquiry.Location = new System.Drawing.Point(330, 140);
            this.btnInquiry.Name = "btnInquiry";
            this.btnInquiry.Size = new System.Drawing.Size(142, 40);
            this.btnInquiry.TabIndex = 67;
            this.btnInquiry.Text = "조회 (&I)";
            this.btnInquiry.UseVisualStyleBackColor = true;
            this.btnInquiry.Click += new System.EventHandler(this.btnInquiry_Click);
            // 
            // chkBlock
            // 
            this.chkBlock.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkBlock.Location = new System.Drawing.Point(530, 190);
            this.chkBlock.Name = "chkBlock";
            this.chkBlock.Size = new System.Drawing.Size(240, 40);
            this.chkBlock.TabIndex = 75;
            this.chkBlock.Tag = "SUSPEND";
            this.chkBlock.Text = "(&S) 게시 중단 리뷰";
            this.chkBlock.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkBlock.UseVisualStyleBackColor = true;
            this.chkBlock.Click += new System.EventHandler(this.chkType_Click);
            // 
            // chkNoRes
            // 
            this.chkNoRes.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkNoRes.Location = new System.Drawing.Point(280, 190);
            this.chkNoRes.Name = "chkNoRes";
            this.chkNoRes.Size = new System.Drawing.Size(240, 40);
            this.chkNoRes.TabIndex = 76;
            this.chkNoRes.Tag = "BLIND";
            this.chkNoRes.Text = "(&B) 차단 리뷰";
            this.chkNoRes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkNoRes.UseVisualStyleBackColor = true;
            this.chkNoRes.Click += new System.EventHandler(this.chkType_Click);
            // 
            // chkTotal
            // 
            this.chkTotal.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkTotal.Checked = true;
            this.chkTotal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTotal.Location = new System.Drawing.Point(30, 190);
            this.chkTotal.Name = "chkTotal";
            this.chkTotal.Size = new System.Drawing.Size(240, 40);
            this.chkTotal.TabIndex = 74;
            this.chkTotal.Tag = "EXPOSE";
            this.chkTotal.Text = "(&A) 노출 리뷰";
            this.chkTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkTotal.UseVisualStyleBackColor = true;
            this.chkTotal.Click += new System.EventHandler(this.chkType_Click);
            // 
            // btnLastPage
            // 
            this.btnLastPage.Location = new System.Drawing.Point(620, 665);
            this.btnLastPage.Name = "btnLastPage";
            this.btnLastPage.Size = new System.Drawing.Size(60, 40);
            this.btnLastPage.TabIndex = 72;
            this.btnLastPage.Text = ">| (&L)";
            this.btnLastPage.UseVisualStyleBackColor = true;
            this.btnLastPage.Click += new System.EventHandler(this.btnLastPage_Click);
            // 
            // btnNextPage
            // 
            this.btnNextPage.Location = new System.Drawing.Point(540, 665);
            this.btnNextPage.Name = "btnNextPage";
            this.btnNextPage.Size = new System.Drawing.Size(60, 40);
            this.btnNextPage.TabIndex = 71;
            this.btnNextPage.Text = "> (&N)";
            this.btnNextPage.UseVisualStyleBackColor = true;
            this.btnNextPage.Click += new System.EventHandler(this.btnNextPage_Click);
            // 
            // btnFirstPage
            // 
            this.btnFirstPage.Location = new System.Drawing.Point(260, 665);
            this.btnFirstPage.Name = "btnFirstPage";
            this.btnFirstPage.Size = new System.Drawing.Size(60, 40);
            this.btnFirstPage.TabIndex = 69;
            this.btnFirstPage.Text = "|< (&F)";
            this.btnFirstPage.UseVisualStyleBackColor = true;
            this.btnFirstPage.Click += new System.EventHandler(this.btnFirstPage_Click);
            // 
            // btnPrevPage
            // 
            this.btnPrevPage.Location = new System.Drawing.Point(340, 665);
            this.btnPrevPage.Name = "btnPrevPage";
            this.btnPrevPage.Size = new System.Drawing.Size(60, 40);
            this.btnPrevPage.TabIndex = 70;
            this.btnPrevPage.Text = "< (&P)";
            this.btnPrevPage.UseVisualStyleBackColor = true;
            this.btnPrevPage.Click += new System.EventHandler(this.btnPrevPage_Click);
            // 
            // txtPageNo
            // 
            this.txtPageNo.Location = new System.Drawing.Point(430, 670);
            this.txtPageNo.Name = "txtPageNo";
            this.txtPageNo.Size = new System.Drawing.Size(80, 30);
            this.txtPageNo.TabIndex = 73;
            this.txtPageNo.Text = "1";
            this.txtPageNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlReviews
            // 
            this.pnlReviews.AutoScroll = true;
            this.pnlReviews.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlReviews.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.pnlReviews.Location = new System.Drawing.Point(30, 235);
            this.pnlReviews.Name = "pnlReviews";
            this.pnlReviews.Padding = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.pnlReviews.Size = new System.Drawing.Size(880, 420);
            this.pnlReviews.TabIndex = 68;
            this.pnlReviews.WrapContents = false;
            // 
            // ReviewListForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(939, 721);
            this.Controls.Add(this.chkBlock);
            this.Controls.Add(this.chkNoRes);
            this.Controls.Add(this.chkTotal);
            this.Controls.Add(this.btnLastPage);
            this.Controls.Add(this.btnNextPage);
            this.Controls.Add(this.btnFirstPage);
            this.Controls.Add(this.btnPrevPage);
            this.Controls.Add(this.txtPageNo);
            this.Controls.Add(this.pnlReviews);
            this.Controls.Add(this.dtcStart);
            this.Controls.Add(this.dtcEnds);
            this.Controls.Add(this.btnInquiry);
            this.Controls.Add(this.txtNoReplyCount);
            this.Controls.Add(this.txtReplyCount);
            this.Controls.Add(this.txtTotalCount);
            this.Controls.Add(this.txtAvgRating);
            this.Controls.Add(this.cmbStores);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ReviewListForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "리뷰관리";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ReviewListForm_FormClosing);
            this.Load += new System.EventHandler(this.ReviewListForm_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label txtAvgRating;
        private System.Windows.Forms.Label txtTotalCount;
        private System.Windows.Forms.Label txtReplyCount;
        private System.Windows.Forms.Label txtNoReplyCount;
        private System.Windows.Forms.DateTimePicker dtcStart;
        private System.Windows.Forms.DateTimePicker dtcEnds;
        private System.Windows.Forms.Button btnInquiry;
        private System.Windows.Forms.CheckBox chkBlock;
        private System.Windows.Forms.CheckBox chkNoRes;
        private System.Windows.Forms.CheckBox chkTotal;
        private System.Windows.Forms.Button btnLastPage;
        private System.Windows.Forms.Button btnNextPage;
        private System.Windows.Forms.Button btnFirstPage;
        private System.Windows.Forms.Button btnPrevPage;
        private System.Windows.Forms.Label txtPageNo;
        private System.Windows.Forms.FlowLayoutPanel pnlReviews;
        public System.Windows.Forms.ComboBox cmbStores;
    }
}