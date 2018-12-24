using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace OnlineExam.db
{
    public class Exam_Question
    {
        [BsonId]
        public string Index { get; set; }
        public string Id { get; set; }
        public string Type { get; set; }
        public string Target { get; set; }
        public string Question { get; set; }
        public string Bestanswer { get; set; }
        public string Bestanswerid { get; set; }
        public string Imageurl { get; set; }
        public string Sinaimg { get; set; }
        public string A { get; set; }
        public string B { get; set; }
        public string C { get; set; }
        public string D { get; set; }

    }

    public class Exam_Drector
    {
        static Mongodb mongodb;

        public static void ConnectMongo()
        {
            mongodb = new Mongodb();
        }

        public static ArrayList GetQuestions(string type, int num, int index)
        {
            ArrayList Question = new ArrayList();
            ArrayList ansered = new ArrayList();
            ConnectMongo();
            mongodb.GetCollection(type);
            Question = mongodb.GetQuestion_random(num, index);
            return Question;
        }

        public static ArrayList GetQuestionsOrChoosed(BsonDocument document, string type)
        {
            ArrayList arrayList = new ArrayList();
            var questions = document[type];
            BsonArray array = (BsonArray)document[type][0];
            if(type == "question_one" || type == "question_more" || type == "question_judge")
            {
                foreach (var item in array)
                {
                    Exam_Question exam = new Exam_Question
                    {
                        Target = item["target"].ToString(),
                        Id = item["id"].ToString(),
                        Question = item["question"].ToString(),
                        Sinaimg = item["sinaimg"].ToString(),
                        Imageurl = item["imageurl"].ToString(),
                        Bestanswer = item["bestanswer"].ToString(),
                        Bestanswerid = item["bestanswerid"].ToString(),
                        A = item["options"]["a"].ToString(),
                        B = item["options"]["b"].ToString(),
                        C = item["options"]["c"].ToString(),
                        D = item["options"]["d"].ToString(),
                        Index = item["Index"].ToString(),
                        Type = item["Type"].ToString(),
                    };
                    arrayList.Add(exam);
                }
            }
            else if(type == "questions_more_chosed")
            {
                foreach(var item in array)
                {
                    string choosed = item.ToString();
                    arrayList.Add(choosed);
                }
            }
            else
            {
                foreach(var item in array)
                {
                    int choosed = item.AsInt32;
                    arrayList.Add(choosed);
                }
            }
            return arrayList;
        }

        public static void Loading_Question(DataList dataList, ArrayList question, string type)
        {
            if(type == "questions_one")
            {
                for (int i = 0; i < dataList.Items.Count; i++)
                {
#pragma warning disable CS0436 // 类型与导入类型冲突
                    Exam_Question exam = (Exam_Question)question[i];
#pragma warning restore CS0436 // 类型与导入类型冲突
                    RadioButtonList radio = (RadioButtonList)dataList.Items[i].FindControl("radio_options");
                    if (exam.A != "")
                        radio.Items.Add(exam.A);
                    if (exam.B != "")
                        radio.Items.Add(exam.B);
                    if (exam.C != "")
                        radio.Items.Add(exam.C);
                    if (exam.D != "")
                        radio.Items.Add(exam.D);
                }
            }
            else if(type == "questions_more")
            {
                for (int i = 0; i < dataList.Items.Count; i++)
                {
#pragma warning disable CS0436 // 类型与导入类型冲突
                    Exam_Question exam = (Exam_Question)question[i];
#pragma warning restore CS0436 // 类型与导入类型冲突
                    CheckBoxList check = (CheckBoxList)dataList.Items[i].FindControl("check_options");
                    if (exam.A != "")
                        check.Items.Add(exam.A);
                    if (exam.B != "")
                        check.Items.Add(exam.B);
                    if (exam.C != "")
                        check.Items.Add(exam.C);
                    if (exam.D != "")
                        check.Items.Add(exam.D);
                }
            }
        }

        public static void Loading_Result(DataList dataList, ArrayList question, string type, ArrayList choosed)
        {
            if(type == "questions_more")
            {
                for(int i = 0; i < dataList.Items.Count; i++)
                {
#pragma warning disable CS0436 // 类型与导入类型冲突
                    Exam_Question exam = (Exam_Question)question[i];
#pragma warning restore CS0436 // 类型与导入类型冲突
                    string checkboxchoosed = choosed[i].ToString();                     // 用户选择的项
                    CheckBoxList checkBoxList = (CheckBoxList)dataList.Items[i].FindControl("check_options");
                    for(int j = 0; j < checkboxchoosed.Length; j++)
                    {
                        int num = Int32.Parse(checkboxchoosed[j].ToString()) - 1;
                        checkBoxList.Items[num].Selected = true;
                    }
                    if(checkboxchoosed == exam.Target)
                    {
                        Panel Panel_result_image = (Panel)dataList.Items[i].FindControl("Panel_result_image");
                        Image image = new Image
                        {
                            CssClass = "image_result",
                            ImageUrl = "~/Images/选中.png"
                        };
                        Panel_result_image.Controls.Add(image);
                    }
                    else
                    {
                        Panel Panel_Answer = (Panel)dataList.Items[i].FindControl("Panel_Answer");
                        Panel Panel_result_image = (Panel)dataList.Items[i].FindControl("Panel_result_image");
                        Image image = new Image
                        {
                            CssClass = "image_result",
                            ImageUrl = "~/Images/关闭.png"
                        };
                        Panel_result_image.Controls.Add(image);
                        Label bestanswer = new Label
                        {
                            Text = "本题解析：</br>" + exam.Bestanswer,
                        };
                        Panel_Answer.Controls.Add(bestanswer);
                    }
                    checkBoxList.Enabled = false;
                }
            }
            else
            {
                for (int i = 0; i < dataList.Items.Count; i++)
                {
#pragma warning disable CS0436 // 类型与导入类型冲突
                    Exam_Question exam = (Exam_Question)question[i];
#pragma warning restore CS0436 // 类型与导入类型冲突
                    int target = Convert.ToInt32(choosed[i]) + 1;
                    RadioButtonList radio = (RadioButtonList)dataList.Items[i].FindControl("radio_options");
                    radio.Items[target - 1].Selected = true;
                    if(target.ToString() == exam.Target)
                    {
                        Panel Panel_result_image = (Panel)dataList.Items[i].FindControl("Panel_result_image");
                        Image image = new Image
                        {
                            CssClass = "image_result",
                            ImageUrl = "~/Images/选中.png"
                        };
                        Panel_result_image.Controls.Add(image);
                    }
                    else
                    {
                        Panel Panel_Answer = (Panel)dataList.Items[i].FindControl("Panel_Answer");
                        Panel Panel_result_image = (Panel)dataList.Items[i].FindControl("Panel_result_image");
                        Image image = new Image
                        {
                            CssClass = "image_result",
                            ImageUrl = "~/Images/关闭.png"
                        };
                        Panel_result_image.Controls.Add(image);
                        Label bestanswer = new Label
                        {
                            Text = "本题解析：</br>" + exam.Bestanswer,
                        };
                        Panel_Answer.Controls.Add(bestanswer);
                    }
                    radio.Enabled = false;
                }
            }
        }

        public static Boolean IsFinshed(DataList dataList, string type)
        {
            if(type == "questions_more")
            {
                for (int i = 0; i < dataList.Items.Count; i++)
                {
                    CheckBoxList radio = (CheckBoxList)dataList.Items[i].FindControl("check_options");
                    if (radio.SelectedIndex == -1)
                        return false;
                }
            }
            else
            {
                for (int i = 0; i < dataList.Items.Count; i++)
                {
                    RadioButtonList radio = (RadioButtonList)dataList.Items[i].FindControl("radio_options");
                    if (radio.SelectedIndex == -1)
                        return false;
                }
            }
            return true;

        }

        public static Hashtable Get_Score(DataList dataList, ArrayList question, string type)
        {
            Hashtable hashtable = new Hashtable();
            ArrayList question_choosed = new ArrayList();
            int score = 0;
            if(type == "questions_more")
            {
                for (int i = 0; i < dataList.Items.Count; i++)
                {
#pragma warning disable CS0436 // 类型与导入类型冲突
                    Exam_Question exam = (Exam_Question)question[i];
#pragma warning restore CS0436 // 类型与导入类型冲突
                    string checkboxchoosed = null;
                    CheckBoxList checkBoxList = (CheckBoxList)dataList.Items[i].FindControl("check_options");
                    // 获取选择项
                    for (int j = 0; j < checkBoxList.Items.Count; j++)
                    {
                        if (checkBoxList.Items[j].Selected)
                        {
                            checkboxchoosed = checkboxchoosed + (j + 1).ToString();
                        }
                    }
                    question_choosed.Add(checkboxchoosed);

                    // 比对答案
                    if (checkboxchoosed == exam.Target)
                    {
                        score++;
                    }
                }
            }
            else
            {
                for (int i = 0; i < dataList.Items.Count; i++)
                {
#pragma warning disable CS0436 // 类型与导入类型冲突
                    Exam_Question exam = (Exam_Question)question[i];
#pragma warning restore CS0436 // 类型与导入类型冲突
                    RadioButtonList radio = (RadioButtonList)dataList.Items[i].FindControl("radio_options");
                    // 获取选择项
                    for (int j = 0; j < radio.Items.Count; j++)
                    {
                        if (radio.Items[j].Selected)
                        {
                            question_choosed.Add(j);
                            // 比对答案
                            if ((j + 1).ToString() == exam.Target)
                                score++;
                            break;
                        }
                    }
                }
            }
            hashtable.Add(type + "_score", score);
            hashtable.Add(type + "_chosed", question_choosed);
            return hashtable;
        }
    }
}


