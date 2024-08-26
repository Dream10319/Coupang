using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using AIReview;
using CoupangWeb.Models;

namespace CoupangWeb.Reviews
{
    public partial class ReviewCommentForm : Form
    {
        public AI_Review_Module aI_Review_Module = new AI_Review_Module();
        public string Comment { get; set; }
        public bool ModifyFlags { get; set; }

        public CPWReviewContent review_content = new CPWReviewContent();
        public ReviewListForm main_form = new ReviewListForm();
        public ReviewCommentForm(CPWReviewContent review, ReviewListForm form)
        {
            InitializeComponent();
            ModifyFlags = false;
            review_content = review;
            main_form = form;
        }

        private void ReviewCommentForm_Load(object sender, EventArgs e)
        {
            txtComment.Text = Comment;
            btnConfirm.Text = ModifyFlags ? "수정 (&U)" : "등록 (&R)";
            Text = ModifyFlags ? "코멘트 수정" : "코멘트 추가";
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            Comment = txtComment.Text;
            int len = Comment.Length;
            if (len < 1)
            {
                MessageBox.Show((ModifyFlags ? "수정" : "등록") + "하실 댓글을 입력하세요.");
            }
            /*else if (len > 1000)
            {
                MessageBox.Show("댓글은 1000글자미만이어야 합니다.");
            }*/
            else
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string shopname = Regex.Match(main_form.cmbStores.Text, @"^[^\(]+").Value;
            
            string menuname = review_content.orderInfo[0].dishName;
            if (review_content.orderInfo[0].dishName.Contains("리뷰 이벤트 참여")) menuname = review_content.orderInfo[1].dishName;
            string cleanedMenu = Regex.Replace(menuname, @"\[프리미엄\]\s*|\s*\d+[a-zA-Z가-힣]*", "").Trim();

            aI_Review_Module.GenerateComment(this, "sk-p2KqRqv8sJhi0uHZL4RtT3BlbkFJ6" +
                "UfAOZS9kHH0DIIh0Bl4", radioButton1.Checked, radioButton2.Checked, txtComment, shopname, review_content.rating.ToString(), review_content.customerName, review_content.comment, cleanedMenu);

        }
    }
}
