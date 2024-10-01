using CoupangWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoupangWeb.Reviews
{
    public partial class ReplyItemCtrl : UserControl
    {
        public CPWReviewReply mReplyInfo;
        private IEventCallback mCallback;

        public ReplyItemCtrl()
        {
            InitializeComponent();

            mReplyInfo = null;
            mCallback = null;

            txtName.Text = "";
            txtDate.Text = "";
            txtReply.Text = "";
        }

        public void SetData(CPWReviewReply replyInfo, IEventCallback callback)
        {
            txtName.Text = replyInfo.orderReviewReplyRespondentType == "STORE" ? "사장님" : "";
            var dtCreate = replyInfo.createdAt;
            txtDate.Text = (dtCreate != null) ? dtCreate.ToString("yyyy-MM-dd") : "";
            txtReply.Text = replyInfo.content ?? "";
            btnUpdateComment.Visible = false;
            if (dtCreate != null)
                btnUpdateComment.Visible = (DateTime.Today - dtCreate).TotalDays < 31;

            mReplyInfo = replyInfo;
            mCallback = callback;
        }

        public void UpdateReply(string text)
        {
            txtReply.Text = text;
            mReplyInfo.content = text;
        }


        private void btnDeleteComment_Click(object sender, EventArgs e)
        {
            if (mCallback != null)
                mCallback.OnDeleteReply(this);
        }

        private void btnUpdateComment_Click(object sender, EventArgs e)
        {
            if (mCallback != null)
                mCallback.OnUpdateReply(this);
        }

        public interface IEventCallback
        {
            void OnDeleteReply(ReplyItemCtrl source);
            void OnUpdateReply(ReplyItemCtrl source);
        }
    }
}
