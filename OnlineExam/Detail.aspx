<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Detail.aspx.cs" Inherits="OnlineExam.Detail"%>

<%@ Register Src="Templates/header.ascx" TagPrefix="user" TagName="head"%>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Online Exam</title>
        <!-- Bootstrap core CSS -->
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
    .image_result  {
          height:50px;
          width:50px;
    }
</style>
<script type="text/javascript">

</script>
</head>
<body>
    <!-- Navigation -->
    <user:head ID ="header" runat="server"></user:head>

    <!-- Page Content -->
    <div class="container">

      <!-- Page Heading -->
      <h1 class="my-4">Exam System</h1>
      <div align="right">
          学号：<asp:Label ID="number" runat="server" Font-Size="Medium" ForeColor="#ff0066"></asp:Label>
          姓名：<asp:Label ID="name" runat="server"  Font-Size="Medium" ForeColor="#ff0066"></asp:Label>
          得分：<asp:Label ID="score" runat="server" Font-Size="Medium" ForeColor="#ff0066"></asp:Label>
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
