<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OnlineExersice.aspx.cs" Inherits="OnlineExam.OnlineExersice" %>

<%@ Register Src="Templates/header.ascx" TagPrefix="user" TagName="head"%>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" type="text/css">

    <link href="Content/css/1-col-portfolio.css" rel="stylesheet" type="text/css">
    <style>
        .center {
	        width: 960px;
	        margin-left: auto;
	        margin-right: auto;
        }
        .div1 {
            margin:5px;
	        background-color: whitesmoke;
	        border-radius: 10px; /*所有角都使用半径为10px的圆角*/
            width: 1100px;
        }
        .radio_options {
            margin:2px;
            border:5px;
            padding:0;
        }
        .an_right {
            background: #cce7c9;
            border: 1px solid #b3d7af;
            color: #53bb48;
        }
        .an_result {
            border-radius: 3px;
            line-height: 42px;
            height: 42px;
            display: inline-block;
            float: left;
            padding: 0 12px;
        }
        .an_wrong {
            background: #fcbabc;
            border: 1px solid #fba4a7;
            color: #fd0000;
        }
        .image_result  {
              height:50px;
              width:50px;
        }
       .progress{
              height: 25px;
              background: #262626;
              padding: 5px;
              overflow: visible;
              border-radius: 20px;
              border-top: 1px solid #000;
              border-bottom: 1px solid #7992a8;
              margin-top: 50px;
              width: 1000px;
        }
        .progress .progress-bar{
              border-radius: 20px;
              position: relative;
              animation: animate-positive 2s;
        }
        .progress .progress-value{
              display: block;
              padding: 3px 7px;
              font-size: 13px;
              color: #fff;
              border-radius: 4px;
              background: #191919;
              border: 1px solid #000;
              position: absolute;
              top: -40px;
              right: -10px;
         }
        .progress .progress-value:after{
            content: "";
            border-top: 10px solid #191919;
            border-left: 10px solid transparent;
            border-right: 10px solid transparent;
            position: absolute;
            bottom: -6px;
            left: 26%;
        }
        .progress-bar.active{
           animation: reverse progress-bar-stripes 0.40s linear infinite, animate-positive 2s;
        }
        @-webkit-keyframes animate-positive{
           0% { width: 0; }
        }
        @keyframes animate-positive{
           0% { width: 0; }
        }
      #gg{margin:0 auto; width:100%; position:fixed; z-index:99; background:#343a40; color:#FFF} 

    </style>
</head>

<body>
    <!-- Navigation -->
    <user:head ID ="header" runat="server"></user:head>

    <!-- Page Content -->
    <div class="container">

      <!-- Page Heading -->
      <h1 class="my-4">Exam System</h1>

      <div align="right">
          得分：<asp:Label ID="score" runat="server" Font-Size="X-Large"></asp:Label>
      </div>
        
      <form id="form1" runat="server">
          <!-- Questoin One -->
          <asp:DataList ID="DataList_Questoin_One" runat="server">
              <HeaderTemplate>
                    <asp:Label ID="type" runat="server">一. 单选题 （1 × 40）</asp:Label>
              </HeaderTemplate>
              <ItemTemplate>
                    <div class="div1">
                        <asp:Label ID="num" runat="server"><%# Eval("Index")%></asp:Label>
                        <asp:Label ID="question" runat="server"><%# Eval("Question")%></asp:Label>
                        <div class="row">
                            <div class="col-md-5">
                                <asp:RadioButtonList ID="radio_options" runat="server" style="margin:2px;border:5px;padding:0"></asp:RadioButtonList>
                            </div>
                            <div class="col-md-7">
                                <img src="<%# Eval("Sinaimg")%>" class="img-fluid rounded mb-3 mb-md-0" style="margin:2px;border:5px;padding:0" />
                            </div>
                        </div>
                        <div>
                            <asp:Panel ID="Panel_result_image" runat="server"></asp:Panel>
                        </div>
                        <div style="background-color:lawngreen">                           
                            <asp:Panel ID="Panel_Answer" runat="server"></asp:Panel>                           
                        </div>
                    </div>
              </ItemTemplate>
          </asp:DataList>
 
          <!-- Questoin More -->
          <asp:DataList ID="DataList_Questoin_More" runat="server">
              <HeaderTemplate>
                    <asp:Label ID="type" runat="server">二. 多选题 （1 × 30）</asp:Label>
              </HeaderTemplate>
              <ItemTemplate>
                    <div class="div1">
                        <asp:Label ID="num" runat="server"><%# Eval("Index")%></asp:Label>
                        <asp:Label ID="question" runat="server"><%# Eval("Question")%></asp:Label>
                        <div class="row">
                            <div class="col-md-5">
                                <asp:CheckBoxList ID="check_options" runat="server" style="margin:2px;border:5px;padding:0"></asp:CheckBoxList>
                            </div>
                            <div class="col-md-7">
                                <img src="<%# Eval("Sinaimg")%>" class="img-fluid rounded mb-3 mb-md-0" style="margin:2px;border:5px;padding:0" />
                            </div>
                        </div>
                        <div>
                            <asp:Panel ID="Panel_result_image" runat="server"></asp:Panel>
                        </div>
                        <div style="background-color:lawngreen">                           
                            <asp:Panel ID="Panel_Answer" runat="server"></asp:Panel>                           
                        </div>
                    </div>
              </ItemTemplate>
          </asp:DataList>

          <!-- Questoin Judge -->
          <asp:DataList ID="DataList_Questoin_Judge" runat="server">
              <HeaderTemplate>
                    <asp:Label ID="type" runat="server">三. 判断题 （1 × 30）</asp:Label>
              </HeaderTemplate>
              <ItemTemplate>
                    <div class="div1">
                        <asp:Label ID="num" runat="server"><%# Eval("Index")%></asp:Label>
                        <asp:Label ID="question" runat="server"><%# Eval("Question")%></asp:Label>
                        <div class="row">
                            <div class="col-md-5">
                                <asp:RadioButtonList ID="radio_options" runat="server" style="margin:2px;border:5px;padding:0">
                                    <asp:ListItem>对</asp:ListItem>
                                    <asp:ListItem>错</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                            <div class="col-md-7">
                                <img src="<%# Eval("Sinaimg")%>" class="img-fluid rounded mb-3 mb-md-0" style="margin:2px;border:5px;padding:0" />
                            </div>
                        </div>
                        <div>
                            <asp:Panel ID="Panel_result_image" runat="server"></asp:Panel>
                        </div>
                        <div style="background-color:lawngreen">                           
                            <asp:Panel ID="Panel_Answer" runat="server"></asp:Panel>                           
                        </div>
                    </div>
              </ItemTemplate>
          </asp:DataList>

          <div align="center">
                <asp:Button ID="Button_back" runat="server" Text="返回首页" CssClass="btn btn-primary" PostBackUrl="~/Default.aspx"/>
                <asp:Button ID="Submit" runat="server" Text="提交试卷" CssClass="btn btn-primary" OnClick="Submit_Click"/>
                <asp:Button ID="Again" runat="server" Text="重新出题" CssClass="btn btn-primary" OnClick="Again_Click"/>
          </div>   
      </form>
      <!-- /.row -->

      <!-- post your answer -->

      <!-- /.row -->

      <hr>
    </div>

    <!-- /.container -->

    <!-- Footer -->
    <footer class="py-5 bg-dark">
      <div class="container">
        <p class="m-0 text-center text-white">Copyright &copy; Lcanboom 2018</p>
      </div>
      <!-- /.container -->
    </footer>

    <!-- Bootstrap core JavaScript -->
    <script src="Scripts/jquery-3.3.1.min.js"></script>
    <script src="Scripts/bootstrap.bundle.min.js"></script>

</body>
</html>
