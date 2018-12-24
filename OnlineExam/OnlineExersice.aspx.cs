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
    public partial class OnlineExersice : System.Web.UI.Page
    {
        Mongodb mongodb;
        Hashtable Question_Save = new Hashtable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Loading();
            }
        }

        public void Loading()
        {
            ArrayList question_one;
            ArrayList question_more;
            ArrayList question_judge;
            question_one = Exam_Drector.GetQuestions("questions_one", 5, 1);
            question_more = Exam_Drector.GetQuestions("questions_more", 5, 6);
            question_judge = Exam_Drector.GetQuestions("questions_judge", 5, 11);
            Question_Save.Add("questions_one", question_one);                  // 保存题目用来比对答案，存储在哈希表中
            Question_Save.Add("questions_more", question_more);
            Question_Save.Add("questions_judge", question_judge);
            DataList_Questoin_One.DataSource = question_one;
            DataList_Questoin_One.DataBind();
            DataList_Questoin_More.DataSource = question_more;
            DataList_Questoin_More.DataBind();
            DataList_Questoin_Judge.DataSource = question_judge;
            DataList_Questoin_Judge.DataBind();
            Exam_Drector.Loading_Question(DataList_Questoin_One, question_one, "questions_one");
            Exam_Drector.Loading_Question(DataList_Questoin_More, question_more, "questions_more");
            Session["Question_Save"] = Question_Save;
            ViewState["pre_question_one"] = 0;
            ViewState["pre_question_more"] = 0;
            ViewState["pre_question_judge"] = 0;
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            int score_total = 0;
            Hashtable Question_Save = (Hashtable)Session["Question_Save"];
            ArrayList question_one = (ArrayList)Question_Save["questions_one"];
            ArrayList question_more = (ArrayList)Question_Save["questions_more"];
            ArrayList question_judge = (ArrayList)Question_Save["questions_judge"];
            Hashtable hashtable_question_one = Exam_Drector.Get_Score(DataList_Questoin_One, question_one, "questions_one");
            Hashtable hashtable_question_more = Exam_Drector.Get_Score(DataList_Questoin_More, question_more, "questions_more");
            Hashtable hashtable_question_judge = Exam_Drector.Get_Score(DataList_Questoin_Judge, question_judge, "questions_judge");
            ArrayList questions_one_chosed = (ArrayList)hashtable_question_one["questions_one_chosed"];
            ArrayList questions_more_chosed = (ArrayList)hashtable_question_more["questions_more_chosed"];
            ArrayList questions_judge_chosed = (ArrayList)hashtable_question_judge["questions_judge_chosed"];

            Boolean one_finshed = Exam_Drector.IsFinshed(DataList_Questoin_One, "questions_one");
            Boolean more_finshed = Exam_Drector.IsFinshed(DataList_Questoin_More, "questions_more");
            Boolean judge_finshed = Exam_Drector.IsFinshed(DataList_Questoin_Judge, "questions_judge");

            if(one_finshed && more_finshed && judge_finshed)
            {
                score_total = Convert.ToInt32(hashtable_question_one["questions_one_score"]) + Convert.ToInt32(hashtable_question_more["questions_more_score"]) + Convert.ToInt32(hashtable_question_judge["questions_judge_score"]);
                Exam_Drector.Loading_Result(DataList_Questoin_One, question_one, "questions_one", questions_one_chosed);
                Exam_Drector.Loading_Result(DataList_Questoin_More, question_more, "questions_more", questions_more_chosed);
                Exam_Drector.Loading_Result(DataList_Questoin_Judge, question_judge, "questions_judge", questions_judge_chosed);
                score.Text = score_total.ToString();
            }
            else
            {
#pragma warning disable CS0618 // 类型或成员已过时
                this.RegisterStartupScript("hello", "<script>alert('还有题目未完成哦!')</script>");
#pragma warning restore CS0618 // 类型或成员已过时
            }


        }

        protected void Again_Click(object sender, EventArgs e)
        {
            Loading();
        }
    }
}