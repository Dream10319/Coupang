using CoupangWeb.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoupangWeb.Reviews
{
    public partial class ReviewItemCtrl : UserControl, ReplyItemCtrl.IEventCallback
    {
        public ReviewListForm main_form = new ReviewListForm();
        public CPWReviewContent mReview { get; set; }
        public long mShopId { get; set; }
        private IEventCallback mCallback { get; set; }
        private ReplyItemCtrl mSelReply = null;


        public ReviewItemCtrl()
        {
            InitializeComponent();
            mReview = null;
            mCallback = null;

            txtUserName.Text = "";
            txtTitle.Text = "";
            txtRating.Text = "";
            txtDate.Text = "";
            txtMenus.Text = "";
            txtOrderInfo.Text = "";
        }

        public void SetData(CPWReviewContent review, long shopId, IEventCallback callback)
        {
            mReview = review;
            mShopId = shopId;
            mCallback = callback;

            txtUserName.Text = review.customerName ?? "";
            txtTitle.Text = review.comment ?? "";
            txtRating.Text = review.GetRatingText();
            txtDate.Text = review.GetDateText();
            txtMenus.Text = review.GetMenusText();
            txtOrderInfo.Text = review.GetOrderInfoText();

            ShowImages();
            ShowComments();
        }

        private void ShowImages()
        {
            if ((mReview == null) || (mReview.images == null))
                return;
            pnlImages.Controls.Clear();
            int count = 0;
            foreach (var image in mReview.images)
            {
                if (!string.IsNullOrWhiteSpace(image) && image.StartsWith("http"))
                {
                    PictureBox pic = new PictureBox();
                    pic.SizeMode = PictureBoxSizeMode.StretchImage;
                    pic.ImageLocation = image;
                    pic.Size = new Size(80, 80);
                    pic.Margin = new Padding((count > 0) ? 10 : 0, 0, 0, 0);
                    pnlImages.Controls.Add(pic);
                    count++;
                }
            }
        }

        private void ShowComments()
        {
            int count = ((mReview != null) && (mReview.replies != null)) ? mReview.replies.Count : 0;
            bool writeable = false;
            var dtCreate = mReview.createdAt;
            if (dtCreate != null)
                writeable = (DateTime.Today - dtCreate).TotalDays < 14;
            btnAddReply.Visible = writeable;
            btnAddReply.Text = (count > 0) ? "사장님 댓글 추가하기" : "사장님 댓글 등록하기";
            if (count > 0)
            {
                pnlComments.Controls.Clear();
                foreach (var reply in mReview.replies)
                {
                    if ((reply != null) && (reply.orderReviewReplyStatusType == "EXPOSE"))
                    {
                        var ctrl = new ReplyItemCtrl();
                        ctrl.SetData(reply, this);
                        pnlComments.Controls.Add(ctrl);
                    }
                }
            }
        }

        private void btnAddReply_Click(object sender, EventArgs e)
        {
            ReviewCommentForm dlg = new ReviewCommentForm(mReview, main_form);
            string userName = mReview.customerName ?? "";
            if (!string.IsNullOrWhiteSpace(userName))
                dlg.Comment = userName + "님,";
            dlg.ModifyFlags = false;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string comment = dlg.Comment;
                Thread th = new Thread(new ParameterizedThreadStart((object f) =>
                {
                    AddReplyApi(comment);
                }));
                th.Start();
            }
        }

        public void OnDeleteReply(ReplyItemCtrl source)
        {
            if ((mShopId < 1) || (mReview.orderReviewId < 1) || (source == null))
                return;
            var reply = source.mReplyInfo;
            if ((reply == null) || (reply.orderReviewId < 1))
                return;
            if (MessageBox.Show("정말 삭제하시겠습니까?", "", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;
            mSelReply = source;
            Thread th = new Thread(new ParameterizedThreadStart((object f) =>
            {
                DeleteReplyApi(reply.orderReviewReplyId);
            }));
            th.Start();
        }

        public void OnUpdateReply(ReplyItemCtrl source)
        {
            if ((mShopId < 1) || (mReview.orderReviewId < 1) || (source == null))
                return;
            var info = source.mReplyInfo;
            if ((info == null) || (info.orderReviewReplyId < 1))
                return;
            /*if (MessageBox.Show("댓글을 수정하시겠습니까?", "댓글 수정", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;*/
            ReviewCommentForm dlg = new ReviewCommentForm(mReview, main_form);
            dlg.Comment = source.mReplyInfo.content;
            dlg.ModifyFlags = true;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                mSelReply = source;
                string reply = dlg.Comment;
                Thread th = new Thread(new ParameterizedThreadStart((object f) =>
                {
                    UpdateCommentApi(info, reply);
                }));
                th.Start();
            }
        }

        private async void AddReplyApi(string replyText)
        {
            if ((mReview == null) || string.IsNullOrWhiteSpace(replyText))
                return;

            var client = new RestClient("https://store.coupangeats.com/api/v1/merchant/reviews/reply")
            {
                CookieContainer = Global.cookies,
                Timeout = -1
            };
            client.UserAgent = Global.userAgent;

            var request = new RestRequest(Method.POST);
            request.AddHeader("Accept", "*/*");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Origin", "https://store.coupangeats.com");
            request.AddHeader("Referer", "https://store.coupangeats.com/merchant/management/reviews");

            request.AddJsonBody(new
            {
                comment = replyText,
                orderReviewId = mReview.orderReviewId,
                storeId = mReview.storeId
            });

            IRestResponse _result = await Task.Run(() => client.ExecuteAsync(request));
            Invoke(new Action(() => {
                Global.SaveCookies(_result.Cookies);

                string msg = "댓글 등록 실패했습니다.";
                if (_result.StatusCode == HttpStatusCode.OK)
                {
                    try
                    {
                        var jsonresponse = JsonConvert.DeserializeObject<dynamic>(_result.Content);
                        if (jsonresponse.data == null)
                            msg = jsonresponse.error.message;
                        else
                        {
                            CPWReviewReply replyitem = JsonConvert.DeserializeObject<CPWReviewReply>(jsonresponse.data.ToString());
                            mReview.replies.Add(replyitem);
                            ShowComments();
                            if (mCallback != null)
                                mCallback.OnChangedReply(this);
                            msg = "";
                        }
                    }
                    catch { }
                }
                if (!string.IsNullOrWhiteSpace(msg))
                    MessageBox.Show(msg);
                else
                    MessageBox.Show("댓글 등록되었습니다.");
            }));
        }

        private async void UpdateCommentApi(CPWReviewReply replyInfo, string replyText)
        {
            if ((mReview == null) || (replyInfo == null))
                return;

            var client = new RestClient("https://store.coupangeats.com/api/v1/merchant/reviews/reply/modify")
            {
                CookieContainer = Global.cookies,
                Timeout = -1
            };
            client.UserAgent = Global.userAgent;

            var request = new RestRequest(Method.POST);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json;charset=UTF-8");
            request.AddHeader("Origin", "https://store.coupangeats.com");
            request.AddHeader("Referer", "https://store.coupangeats.com/merchant/management/reviews");

            request.AddJsonBody(new
            {
                comment = replyText,
                orderReviewReplyId = replyInfo.orderReviewReplyId,
                storeId = mReview.storeId
            });

            IRestResponse _result = await Task.Run(() => client.ExecuteAsync(request));
            Invoke(new Action(() => {
                Global.SaveCookies(_result.Cookies);

                if (_result.StatusCode == HttpStatusCode.OK)
                {
                    replyInfo.content = replyText;
                    if (mSelReply != null)
                        mSelReply.UpdateReply(replyText);
                    mSelReply = null;
                    if (mCallback != null)
                        mCallback.OnChangedReply(this);

                    MessageBox.Show("댓글 수정되었습니다.");
                }
                else
                    MessageBox.Show("댓글 수정 실패했습니다!");
            }));
        }

        private async void DeleteReplyApi(long commentId)
        {
            if (commentId < 1)
                return;

            var client = new RestClient("https://store.coupangeats.com/api/v1/merchant/reviews/reply/delete")
            {
                CookieContainer = Global.cookies,
                Timeout = -1
            };
            client.UserAgent = Global.userAgent;

            var request = new RestRequest(Method.POST);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json;charset=UTF-8");
            request.AddHeader("Origin", "https://store.coupangeats.com");
            request.AddHeader("Referer", "https://store.coupangeats.com/merchant/management/reviews");

            request.AddJsonBody(new
            {
                orderReviewReplyId = commentId,
                storeId = mReview.storeId
            });

            IRestResponse _result = await Task.Run(() => client.ExecuteAsync(request));
            Invoke(new Action(() => {
                Global.SaveCookies(_result.Cookies);

                if (_result.StatusCode == HttpStatusCode.OK)
                {
                    MessageBox.Show("댓글 삭제되었습니다.");
                    pnlComments.Controls.Remove(mSelReply);
                    mSelReply = null;
                }
                else
                    MessageBox.Show("댓글 삭제하는데 실패했습니다!");
            }));
        }





        public interface IEventCallback
        {
            void OnChangedReply(ReviewItemCtrl source);
        }

        public void SetForm(ReviewListForm form)
        {
            main_form = form;
        }
    }
}
