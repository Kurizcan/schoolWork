<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Result.aspx.cs" Inherits="OnlineExam.Result" %>

<%@ Register Src="Templates/header.ascx" TagPrefix="user" TagName="head"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Exam Result</title>
        <!-- Bootstrap core CSS -->
    <link href="Content/bootstrap.min.css" rel="stylesheet" type="text/css">
    <link href="Content/css/scrolling-nav.css" rel="stylesheet" type="text/css">

</head>
<body id="page-top">

    <!-- Navigation -->
    <user:head ID ="header" runat="server"></user:head>


    <header class="bg-primary text-white">
      <div class="container text-center">
        <h1>祝贺你，已完成考试</h1>
        <p class="lead"></p>
      </div>
    </header>
   
    <form id="form2" runat="server">
        <section id="about">
                  <div class="container">
                    <div class="row">
                      <div class="col-lg-8 mx-auto">
                        <h2>About your grade</h2>
                              <table style="width: 100%;">
                                  <tr>
                                      <td>学号：</td>
                                      <td><asp:Label ID="number" runat="server"></asp:Label></td>
                                  </tr>
                                  <tr>
                                      <td>姓名：</td>
                                      <td><asp:Label ID="name" runat="server"></asp:Label></td>
                                  </tr>
                                  <tr>
                                      <td>成绩：</td>
                                      <td><asp:Label ID="score" runat="server"></asp:Label></td>
                                  </tr>
                              </table>
                          <div>
                             <h2>继续努力，追求卓越</h2>
                          </div>
                      </div>
                    </div>
                    <div align="center" style="margin:5px;border:5px;padding:0">
                         <asp:Button ID="Back" runat="server" Text="返回首页" CssClass="btn btn-primary" PostBackUrl="~/Default.aspx"/>
                         <asp:Button ID="Datails" runat="server" Text="查看详情" CssClass="btn btn-primary" PostBackUrl="~/Detail.aspx"/>
                    </div>
                  </div>
        </section>

    </form>
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
