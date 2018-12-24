<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="OnlineExam.Default" %>

<%@ Register Src="Templates/header.ascx" TagPrefix="user" TagName="head"%>


<!DOCTYPE html>
<html lang="en">

  <head>

    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta name="description" content="">
    <meta name="author" content="">

    <title>Online Exam</title>

    <!-- Bootstrap core CSS -->
    <link href="Content/bootstrap.min.css" rel="stylesheet">

    <!-- Custom styles for this template -->
    <link href="Content/css/portfolio-item.css" rel="stylesheet">

      <!-- Favicon -->
      <link href="~/Assets/img/brand/favicon.png" rel="icon" type="image/png">
      <!-- Fonts -->
      <link href="https://fonts.googleapis.com/css?family=Open+Sans:300,400,600,700" rel="stylesheet">
      <!-- Icons -->
      <link href="~/Assets/vendor/nucleo/css/nucleo.css" rel="stylesheet">
      <link href="~/Assets/vendor/@fortawesome/fontawesome-free/css/all.min.css" rel="stylesheet">
      <!-- Argon CSS -->
      <link type="text/css" href="~/Assets/css/argon.css?v=1.0.0" rel="stylesheet">

  </head>

  <body>

    <!-- Navigation -->
    <user:head ID ="header" runat="server"></user:head>

    <!-- Page Content -->
    <div class="container">

      <!-- Portfolio Item Heading -->
      <h1 class="my-4">Exam System
        <small> </small>
      </h1>

      <!-- Portfolio Item Row -->
      <div class="row">

        <div class="col-md-8">
          <asp:Image ID="Image1" runat="server" ImageUrl="Images/welcome.png" BackColor="#33ccff" Width="750" Height="500"/>
        </div>

        <div class="col-md-4">
          <h3 class="my-3">Exam System Feature</h3>
          <p>在这里可以进行考试，学习，复习等一切你想要的功能</p>
          <h3 class="my-3">Feature Details</h3>
          <ul>
            <li>在线考试</li>
            <li>在线练习</li>
            <li>历史成绩</li>
          </ul>
        </div>

      </div>
      <!-- /.row -->

      <!-- Related Projects Row -->
      <h3 class="my-4">Personalized option</h3>

     <form id="form1" runat="server">


      <div class="row">

        <div class="col-md-3 col-sm-6 mb-4" style="margin:auto">
            <asp:ImageButton ID="OnlineExam" CssClass="img-fluid" runat="server" ImageUrl="Images/Exam.png" PostBackUrl="~/Exam.aspx" BackColor="#33ccff" Width="500" Height="300"/>
        </div>

        <div class="col-md-3 col-sm-6 mb-4" style="margin:auto">
            <asp:ImageButton ID="OnlineExersice" CssClass="img-fluid" runat="server" ImageUrl="Images/exersice.png" PostBackUrl="~/OnlineExersice.aspx" BackColor="#33ccff" Width="500" Height="300" />
        </div>

        <div class="col-md-3 col-sm-6 mb-4" style="margin:auto">
            <asp:ImageButton ID="History" CssClass="img-fluid" runat="server" ImageUrl="Images/history.png" PostBackUrl="~/History.aspx" BackColor="#33ccff" Width="500" Height="300"/>
        </div>

      </div>

    </form>
      <!-- /.row -->

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

