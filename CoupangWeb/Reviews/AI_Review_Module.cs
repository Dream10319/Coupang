using OpenAI_API.Chat;
using OpenAI_API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Data.SQLite;

namespace AIReview
{
    public class AI_Review_Module
    {
        public async void GenerateComment(Form fMain, string sAPIkey, bool rAI, bool rAI_Big, TextBox tComment, string sShop, string sRating, string sCustomerName, string sCustomerReview, string sMenu)
        {
            var openAiApiKey = sAPIkey; // Replace with your OpenAI API key

            APIAuthentication aPIAuthentication = new APIAuthentication(openAiApiKey);
            OpenAIAPI openAiApi = new OpenAIAPI(aPIAuthentication);
            string samplecomment = "";
            if (sRating.Contains("1") || sRating.Contains("2"))
            {
                samplecomment = "안녕하세여님, 안녕하세요~ 우선 고객님께 불편사항드려 너무 죄송합니다\r\n고객님 말씀 꼭 피드백하여 적극 개선하도록 하겠습니다\r\n배달이다보니 저희가 예상치못하게 발생하는 사항들이있어 너그럽게 양해 부탁드리겠습니다\r\n더 나은모습으로 찾아뵙겠습니다\r\n감사합니다~";
            }
            else if (sRating.Contains("3") || sRating.Contains("4"))
            {
                samplecomment = "시우어멍 고객님 안녕하세요.배민배달입니다.온전한 상태로 받아보지 못하셨다니, 고객님께 진심으로 죄송한 마음입니다.먼저, 서비스 이용에 불편을 드려 죄송합니다.매장에서 음식을 받은 후 라이더가 배달하는 과정에서 파손이 발생한 것으로 확인됩니다.고객님의 불편 사항은 담당 부서에 바로 전달하여 대책을 마련하고 동일한 문제가 없도록 최선을 다할 것이며, 앞으로 더 나은 배달 경험을 드리기 위해 노력하는 배민배달이 되겠습니다. 활기찬 하루 보내세요.감사합니다.";
            }
            else if (sRating.Contains("5"))
            {
                samplecomment = "배민님,😂힘이되는 리뷰 달아주셔서 감사합니다. 😊항상 좋은 음식을 제공하고자 노력하는 마음을 알아주셔서 감사합니다.❤️ 💕앞으로도 변치 않는 마음으로 최선을 다하며 더 나은 맛과 양을 제공드리고자 노력하겠습니다!! 🙇‍♂️다음번에도 꼭잊지 않고 주문해주세요🙏";
            }

            tComment.Text = "";

            if (rAI)
            {
                var chatRequest = new ChatRequest();
                if (!string.IsNullOrEmpty(sCustomerReview))
                {
                    chatRequest = new ChatRequest
                    {
                        Model = "gpt-4o",
                        MaxTokens = 500,
                        Messages = new ChatMessage[] {
                            new ChatMessage(ChatMessageRole.System, "당신은 매장 주인입니다"),
                        new ChatMessage(ChatMessageRole.User, string.Format("'{0}' 이것은 {1}고객님께서 {2}매장의 {3}에 대하여 남긴 {4}점 리뷰입니다. '{5}' 그리고 이것은 현재 별점에 대한 일반적인 댓글형식입니다. 이를 참고하여 더 정확하고 창의적인 댓글은 무엇일까요?  댓글에 이모티콘을 포함하도록 해주세요. 댓글은 날짜, 계절, 날씨, 리뷰 이벤트, 연락처 정보, 무료로 제공, 무료서비스, 찜, 현재 메뉴가 아닌 다른 음식 메뉴 등에 대해 언급하지 않도록 일반적이어야 하며 현재 매장, 현재 메뉴 및 별점과 관련되어야 합니다. 현재 매장 이름은 {2}이고 현재 메뉴는 {3}이며 별점은 {4}입니다. 그리고 현재 리뷰에 대해서도 더 자세하고 친절하게 답변해주셔야 하며 위에 언급된 댓글 양식을 참조해야 합니다.", sCustomerReview, sCustomerName, sShop, sMenu, sRating, samplecomment)),
                        new ChatMessage(ChatMessageRole.Assistant, samplecomment)
                        },
                        PresencePenalty = 0.1,
                        FrequencyPenalty = 0.1
                    };

                    try
                    {
                        fMain.Invoke(new Action(async () =>
                        {
                            await openAiApi.Chat.StreamChatAsync(chatRequest, res => tComment.Text += res.ToString());
                        }));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
                else
                {
                    chatRequest = new ChatRequest
                    {
                        Model = "gpt-4o",
                        MaxTokens = 500,
                        Messages = new ChatMessage[] {
                            new ChatMessage(ChatMessageRole.System, "당신은 매장 주인입니다"),
                        new ChatMessage(ChatMessageRole.User, string.Format("{0}고객님이 리뷰를 남기지 않았지만 {1}매장의 {2}에 별점 {3}을 주었습니다. 이에 대한 댓글은 무엇일까요?  댓글에 이모티콘을 포함하도록 해주세요. 댓글은 날짜, 계절, 날씨, 리뷰 이벤트, 연락처 정보, 무료로 제공, 무료서비스, 찜, 현재 메뉴가 아닌 다른 음식 메뉴 등에 대해 언급하지 않도록 일반적이어야 하며 현재 매장, 현재 메뉴 및 별점과 관련되어야 합니다. 현재 매장 이름은 {1}이고 현재 메뉴는 {2}이며 별점은 {3}입니다. 그리고 자세하고 친절하게 답변해주셔야 합니다.", sCustomerName, sShop, sMenu, sRating)),
                        new ChatMessage(ChatMessageRole.Assistant, null)
                        },
                        PresencePenalty = 0.1,
                        FrequencyPenalty = 0.1
                    };
                    try
                    {
                        fMain.Invoke(new Action(async () =>
                        {
                            await openAiApi.Chat.StreamChatAsync(chatRequest, res => tComment.Text += res.ToString());
                        }));

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
            else if (rAI_Big)
            {
                Thread th = new Thread(new ThreadStart(() =>
                {
                    SQLiteConnection sqlite_conn = new SQLiteConnection("Data Source=review.db; Version = 3; New = True; Compress = True; ");
                    try
                    {
                        sqlite_conn.Open();
                        SQLiteDataReader sqlite_datareader;
                        SQLiteCommand sqlite_cmd;
                        sqlite_cmd = sqlite_conn.CreateCommand();
                        sqlite_cmd.CommandText = string.Format(@"SELECT owner_comment FROM reviews WHERE rating LIKE '%{0}%' AND menu_name LIKE '%{1}%' ORDER BY RANDOM() LIMIT 1", sRating, sMenu);
                        string gencomment = "";
                        try
                        {
                            sqlite_datareader = sqlite_cmd.ExecuteReader();
                            while (sqlite_datareader.Read())
                            {
                                string myreader = sqlite_datareader.GetString(0);
                                string oldname = myreader.Substring(0, myreader.IndexOf("님"));
                                gencomment = myreader.Replace(oldname, sCustomerName);
                            }
                            if (string.IsNullOrEmpty(gencomment)) gencomment = samplecomment;
                        }
                        catch (Exception ex)
                        {
                            gencomment = samplecomment;
                        }
                        var chatRequest = new ChatRequest();
                        if (!string.IsNullOrEmpty(sCustomerReview))
                        {
                            chatRequest = new ChatRequest
                            {
                                Model = "gpt-4o",
                                MaxTokens = 500,
                                Messages = new ChatMessage[] {
                                new ChatMessage(ChatMessageRole.System, "당신은 매장 주인입니다"),
                                new ChatMessage(ChatMessageRole.User, string.Format("'{0}' 이것은 {1}고객님께서 {2}매장의 {3}에 대하여 남긴 {4}점 리뷰입니다. '{5}' 그리고 이것은 현재 별점에 대한 일반적인 댓글형식입니다. 이를 참고하여 더 정확하고 창의적인 댓글은 무엇일까요?  댓글에 이모티콘을 포함하도록 해주세요. 댓글은 날짜, 계절, 날씨, 리뷰 이벤트, 연락처 정보, 무료로 제공, 무료서비스, 찜, 현재 메뉴가 아닌 다른 음식 메뉴 등에 대해 언급하지 않도록 일반적이어야 하며 현재 매장, 현재 메뉴 및 별점과 관련되어야 합니다. 현재 매장 이름은 {2}이고 현재 메뉴는 {3}이며 별점은 {4}입니다. 그리고 현재 리뷰에 대해서도 더 자세하고 친절하게 답변해주셔야 하며 위에 언급된 댓글 양식을 참조해야 합니다.", sCustomerReview, sCustomerName, sShop, sMenu, sRating, gencomment)),
                                new ChatMessage(ChatMessageRole.Assistant, gencomment)
                                },
                                PresencePenalty = 0.1,
                                FrequencyPenalty = 0.1
                            };
                            try
                            {
                                fMain.Invoke(new Action(async () =>
                                {
                                    await openAiApi.Chat.StreamChatAsync(chatRequest, res => tComment.Text += res.ToString());
                                }));
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error: " + ex.Message);
                            }
                        }
                        else
                        {
                            chatRequest = new ChatRequest
                            {
                                Model = "gpt-4o",
                                MaxTokens = 500,
                                Messages = new ChatMessage[] {
                                new ChatMessage(ChatMessageRole.System, "당신은 매장 주인입니다"),
                                new ChatMessage(ChatMessageRole.User, string.Format("{0}고객님이 리뷰를 남기지 않았지만 {1}매장의 {2}에 별점 {3}을 주었습니다. 이에 대한 댓글은 무엇일까요? 댓글에 이모티콘을 포함하도록 해주세요. 댓글은 날짜, 계절, 날씨, 리뷰 이벤트, 연락처 정보, 무료로 제공, 무료서비스, 현재 메뉴가 아닌 다른 음식 메뉴 등에 대해 언급하지 않도록 일반적이어야 하며 현재 매장, 찜, 현재 메뉴 및 별점과 관련되어야 합니다. 현재 매장 이름은 {1}이고 현재 메뉴는 {2}이며 별점은 {3}입니다. 그리고 자세하고 친절하게 답변해주셔야 합니다.", sCustomerName, sShop, sMenu, sRating)),
                                new ChatMessage(ChatMessageRole.Assistant, null)
                                },
                                PresencePenalty = 0.1,
                                FrequencyPenalty = 0.1
                            };

                            try
                            {
                                fMain.Invoke(new Action(async () =>
                                {
                                    await openAiApi.Chat.StreamChatAsync(chatRequest, res => tComment.Text += res.ToString());
                                }));
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error: " + ex.Message);
                            }
                        }
                        sqlite_conn.Close();
                    }
                    catch (Exception ex)
                    {

                    }
                }
                ));

                th.Start();
            }
        }
    }
}
