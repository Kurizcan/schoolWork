<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="History.aspx.cs" Inherits="OnlineExam.History" %>

<%@ Register Src="Templates/header.ascx" TagPrefix="user" TagName="head"%>


<!DOCTYPE html>

<html lang="en">

  <head>

    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta name="description" content="">
    <meta name="author" content="">

    <title>Online History</title>

    <!-- Bootstrap core CSS -->
    <link href="Content/bootstrap.min.css" rel="stylesheet" type="text/css">
    <link href="Content/css/scrolling-nav.css" rel="stylesheet" type="text/css">
  </head>



  <body id="page-top">
    <!-- Navigation -->
    <user:head ID ="header" runat="server"></user:head>

    <header class="bg-primary text-white">
      <div class="container text-center">
        <h1>历史考试成绩</h1>
        <p class="lead"></p>
      </div>
    </header>

        
    <!-- Page Content -->
    <div class="container">

    <form id="form2" runat="server">
        <div align="center">
            <asp:GridView ID="GridView_Exam_History" runat="server" OnPageIndexChanging="GridView_Exam_History_PageIndexChanging" AutoGenerateColumns="False" CssClass="table" style="margin-bottom:10%;margin-top:15%;" AllowPaging="true" PageSize="5">
                <Columns>
                    <asp:BoundField DataField="id" HeaderText="ID"  ReadOnly="True" SortExpression="id"/>
                    <asp:BoundField DataField="examId" HeaderText="examId" SortExpression="examId" />
                    <asp:BoundField DataField="score" HeaderText="score" SortExpression="score" />
                    <asp:BoundField DataField="datetime" HeaderText="datetime" SortExpression="datetime" />
                    <asp:TemplateField HeaderText="view">
                        <ItemTemplate>
                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%#string.Format("~/Detail.aspx?examId={0}&score={1}",Eval("examId").ToString(),Eval("score").ToString())%>' Text="查看详情"></asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
      </form>

    </div>


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

    <!-- Plugin JavaScript -->
    <script src="Scripts/jquery-easing/jquery.easing.min.js"></script>

    <!-- Custom JavaScript for this theme -->
    <script src="Scripts/js/scrolling-nav.js"></script>

  </body>

</html>

