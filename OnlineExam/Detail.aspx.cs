using MongoDB.Bson;
using OnlineExam.db;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineExam
{
    public partial class Detail : System.Web.UI.Page
    {
        ArrayList question_one;
        ArrayList question_more;
        ArrayList question_judge;
        Hashtable hashtable_question_one;
        Hashtable hashtable_question_more;
        Hashtable hashtable_question_judge;
        ArrayList questions_one_chosed;
        ArrayList questions_more_chosed;
        ArrayList questions_judge_chosed;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(Server.UrlDecode(Request.QueryString["examId"]) != null || Session["Question_Save"] != null)
            {
                if (Server.UrlDecode(Request.QueryString["examId"]) != null)
                {
                    string examId = Server.UrlDecode(Request.QueryString["examId"]);
                    Mongodb mongodb = new Mongodb();
                    mongodb.GetCollection("exam");
                    BsonDocument document = mongodb.Query("examId", examId);
                    question_one = Exam_Drector.GetQuestionsOrChoosed(document, "question_one");
                    question_more = Exam_Drector.GetQuestionsOrChoosed(document, "question_more");
                    question_judge = Exam_Drector.GetQuestionsOrChoosed(document, "question_judge");
                    questions_one_chosed = Exam_Drector.GetQuestionsOrChoosed(document, "questions_one_chosed");
                    questions_more_chosed = Exam_Drector.GetQuestionsOrChoosed(document, "questions_more_chosed");
                    questions_judge_chosed = Exam_Drector.GetQuestionsOrChoosed(document, "questions_judge_chosed");
                    score.Text = Request.QueryString["score"].ToString();
                }
                else if (Session["Question_Save"] != null)
                {
                    Hashtable Question_Save = (Hashtable)Session["Question_Save"];
                    question_one = (ArrayList)Question_Save["questions_one"];
                    question_more = (ArrayList)Question_Save["questions_more"];
                    question_judge = (ArrayList)Question_Save["questions_judge"];
                    // 获取存储用户选择的项
                    hashtable_question_one = (Hashtable)Session["hashtable_question_one"];
                    hashtable_question_more = (Hashtable)Session["hashtable_question_more"];
                    hashtable_question_judge = (Hashtable)Session["hashtable_question_judge"];

                    questions_one_chosed = (ArrayList)hashtable_question_one["questions_one_chosed"];
                    questions_more_chosed = (ArrayList)hashtable_question_more["questions_more_chosed"];
                    questions_judge_chosed = (ArrayList)hashtable_question_judge["questions_judge_chosed"];
                    score.Text = Session["score_total"].ToString();
                }
                DataList_Questoin_One.DataSource = question_one;
                DataList_Questoin_One.DataBind();
                DataList_Questoin_More.DataSource = question_more;
                DataList_Questoin_More.DataBind();
                DataList_Questoin_Judge.DataSource = question_judge;
                DataList_Questoin_Judge.DataBind();

                Exam_Drector.Loading_Question(DataList_Questoin_One, question_one, "questions_one");
                Exam_Drector.Loading_Question(DataList_Questoin_More, question_more, "questions_more");
                Exam_Drector.Loading_Result(DataList_Questoin_One, question_one, "questions_one", questions_one_chosed);
                Exam_Drector.Loading_Result(DataList_Questoin_More, question_more, "questions_more", questions_more_chosed);
                Exam_Drector.Loading_Result(DataList_Questoin_Judge, question_judge, "questions_judge", questions_judge_chosed);

                number.Text = Session["sn"].ToString();
                name.Text = Session["username"].ToString();

            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }
    }
}