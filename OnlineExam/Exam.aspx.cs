using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OnlineExam.db;
using MongoDB.Bson;
using System.Collections;
using MySql.Data.MySqlClient;

namespace OnlineExam
{
    public partial class Exam : System.Web.UI.Page
    {
        Mongodb mongodb;
        Hashtable Question_Save = new Hashtable();
        static int count_total;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(IsPostBack)
            {

            }
            else
            {
                if (Session["sn"] != null && Session["username"] != null)
                {
                    number.Text = "学号：" + Session["sn"].ToString();
                    name.Text = "姓名：" + Session["username"].ToString();
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
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
        }

        private void ConnectMongo()
        {
            mongodb = new Mongodb();
        }

        public ArrayList GetQuestions(string type, int num, int index)
        {
            ArrayList Question = new ArrayList();
            ArrayList ansered = new ArrayList();
            ConnectMongo();
            mongodb.GetCollection(type);
            Question = mongodb.GetQuestion_random(num,index);
            return Question;
        }

        protected void Submit1_Click(object sender, EventArgs e)
        {
            if(count_total == 15)
            {
                Mysqldb mysqldb = new Mysqldb();
                MySqlConnection connection = mysqldb.getConnection();
                MySqlCommand cmd = mysqldb.getCommand(connection);
                ArrayList questions_one_chosed = null;
                ArrayList questions_more_chosed = null;
                ArrayList questions_judge_chosed = null;

                ConnectMongo();

                int score_total = 0;
                string userid = Session["sn"].ToString();
                Hashtable Question_Save = (Hashtable)Session["Question_Save"];
                ArrayList question_one = (ArrayList)Question_Save["questions_one"];
                ArrayList question_more = (ArrayList)Question_Save["questions_more"];
                ArrayList question_judge = (ArrayList)Question_Save["questions_judge"];
                Hashtable hashtable_question_one = Exam_Drector.Get_Score(DataList_Questoin_One, question_one, "questions_one");
                Hashtable hashtable_question_more = Exam_Drector.Get_Score(DataList_Questoin_More, question_more, "questions_more");
                Hashtable hashtable_question_judge = Exam_Drector.Get_Score(DataList_Questoin_Judge, question_judge, "questions_judge");
                score_total = Convert.ToInt32(hashtable_question_one["questions_one_score"]) + Convert.ToInt32(hashtable_question_more["questions_more_score"]) + Convert.ToInt32(hashtable_question_judge["questions_judge_score"]);
                Session["hashtable_question_one"] = hashtable_question_one;
                Session["hashtable_question_more"] = hashtable_question_more;
                Session["hashtable_question_judge"] = hashtable_question_judge;
                Session["score_total"] = score_total;
                count_total = 0;                            // 对进度条清0
                // 存储这次考试记录
                DateTime datetime = DateTime.Now;
                string exam_id = userid.ToString() + "_" + datetime.ToString();
                string sql = "insert into exam_record (userId, examId, score, datetime) VALUES (@userid, @exam_id, @score_total, @datetime)";
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@userid", userid);
                cmd.Parameters.AddWithValue("@exam_id", exam_id);
                cmd.Parameters.AddWithValue("@score_total", score_total.ToString());
                cmd.Parameters.AddWithValue("@datetime", datetime.ToString());
                if (mysqldb.ExceSql(cmd, connection))
                {
                    //成功插入
                    mysqldb.Close();
                }
                else
                {
                    Response.Write("mysql 插入失败");
                }
                mysqldb.ExceSql(cmd, connection);
                // 将这次考试的试题信息，以及答题信息存储到 mongodb 中
                var document = new BsonDocument
                {
                    { "examId", exam_id },
                };
                // 用户题目 选择
                question_one = ArraylistToObject(question_one);
                question_more = ArraylistToObject(question_more);
                question_judge = ArraylistToObject(question_judge);

                questions_one_chosed = (ArrayList)hashtable_question_one["questions_one_chosed"];
                questions_more_chosed = (ArrayList)hashtable_question_more["questions_more_chosed"];
                questions_judge_chosed = (ArrayList)hashtable_question_judge["questions_judge_chosed"];

                var question_one_save = new List<ArrayList> {question_one};
                var question_more_save = new List<ArrayList> { question_more };
                var question_judge_save = new List<ArrayList> { question_judge };

                var question_one_chosed_save = new List<ArrayList> { questions_one_chosed };
                var question_more_chosed_save = new List<ArrayList> { questions_more_chosed };
                var question_judge_chosed_save = new List<ArrayList> { questions_judge_chosed };

                document.Add("question_one", new BsonArray(question_one_save));
                document.Add("question_more", new BsonArray(question_more_save));
                document.Add("question_judge", new BsonArray(question_judge_save));
                document.Add("questions_one_chosed", new BsonArray(question_one_chosed_save));
                document.Add("questions_more_chosed", new BsonArray(question_more_chosed_save));
                document.Add("questions_judge_chosed", new BsonArray(question_judge_chosed_save));

                //document.Add("question_chosed", new BsonArray(question_chosed));
                mongodb.GetCollection("exam");
                mongodb.Insert(document);
                Response.Redirect("~/Result.aspx");
            }
            else
            {
#pragma warning disable CS0618 // 类型或成员已过时
                this.RegisterStartupScript("hello", "<script>alert('还有题目未完成哦!')</script>");
#pragma warning restore CS0618 // 类型或成员已过时

            }
        }

        public ArrayList ArraylistToObject(ArrayList arrayList)
        {
            ArrayList NewArrayList = new ArrayList();
            for(int i = 0; i < arrayList.Count; i++)
            {
                Exam_Question exam = (Exam_Question)arrayList[i];
                BsonDocument document = new BsonDocument
                {
                    ["Index"] = exam.Index,
                    ["id"] = exam.Id,
                    ["target"] = exam.Target,
                    ["question"] = exam.Question,
                    ["Type"] = exam.Type,
                    ["imageurl"] = exam.Imageurl,
                    ["sinaimg"] = exam.Sinaimg,
                    ["bestanswer"] = exam.Bestanswer,
                    ["bestanswerid"] = exam.Bestanswerid                   
                };
                document["options"] = new BsonDocument
                {
                    ["a"] = exam.A,
                    ["b"] = exam.B,
                    ["c"] = exam.C,
                    ["d"] = exam.D
                };
                NewArrayList.Add(document);
            }
            return NewArrayList;
        }

        protected void question_one_SelectedIndexChanged(object sender, EventArgs e)
        {
            int pre = Convert.ToInt32(ViewState["pre_question_one"]);
            int count = 0;                                               // 记录已经答题的数目
            for(int i = 0; i < DataList_Questoin_One.Items.Count; i++)
            {
                RadioButtonList radio = (RadioButtonList)DataList_Questoin_One.Items[i].FindControl("radio_options");
                if (radio.SelectedIndex != -1)
                    count++;                   
            }
            if (count - pre > 0)
                count_total += count - pre;
            ViewState["pre_question_one"] = count;
            this.progess_div.Style.Value = "width: " + count_total.ToString() + "%;";
            this.progess_value.InnerText = count_total.ToString() + "%";
        }

        protected void question_more_SelectedIndexChanged(object sender, EventArgs e)
        {
            int pre = Convert.ToInt32(ViewState["pre_question_more"]);
            int count = 0;                                              // 记录已经答题的数目
            for (int i = 0; i < DataList_Questoin_More.Items.Count; i++)
            {
                CheckBoxList checkBox = (CheckBoxList)DataList_Questoin_More.Items[i].FindControl("check_options");
                if (checkBox.SelectedIndex != -1)
                    count++;
            }
            if (count - pre > 0)
                count_total += count - pre;
            ViewState["pre_question_more"] = count;
            this.progess_div.Style.Value = "width: " + count_total.ToString() + "%;";
            this.progess_value.InnerText = count_total.ToString() + "%";
        }

        protected void question_judge_SelectedIndexChanged(object sender, EventArgs e)
        {
            int pre = Convert.ToInt32(ViewState["pre_question_judge"]);
            int count = 0;                                              // 记录已经答题的数目
            for (int i = 0; i < DataList_Questoin_Judge.Items.Count; i++)
            {
                RadioButtonList radio = (RadioButtonList)DataList_Questoin_Judge.Items[i].FindControl("radio_options");
                if (radio.SelectedIndex != -1)
                    count++;
            }
            if (count - pre > 0)
                count_total += count - pre;
            ViewState["pre_question_judge"] = count;
            this.progess_div.Style.Value = "width: " + count_total.ToString() + "%;";
            this.progess_value.InnerText = count_total.ToString() + "%";
        }
    }
}