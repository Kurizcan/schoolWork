<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="header.ascx.cs" Inherits="OnlineExam.Templates.header" %>
    <nav class="navbar navbar-expand-lg navbar-dark bg-dark fixed-top">
      <div class="container">
        <a class="navbar-brand" href="Default.aspx">Online Exam</a>
        <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarResponsive" aria-controls="navbarResponsive" aria-expanded="false" aria-label="Toggle navigation">
          <span class="navbar-toggler-icon"></span>
        </button>
        <div class="collapse navbar-collapse" id="navbarResponsive">
          <ul class="navbar-nav ml-auto">
            <li class="nav-item active">
              <a class="nav-link" href="Default.aspx">Home
                <span class="sr-only">(current)</span>
              </a>s
            </li>
            <li class="nav-item">
              <a class="nav-link" href="Exam.aspx">Exam</a>
            </li>
            <li class="nav-item">
              <a class="nav-link" href="OnlineExersice.aspx">Exersice</a>
            </li>
            <li class="nav-item">
              <a class="nav-link" href="History.aspx">History</a>
            </li>
            <li class="nav-item dropdown">
                <a class="nav-link pr-0" href="#" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                  <div class="media align-items-center">
                    <span class="avatar avatar-sm rounded-circle">
                      <img alt="Image placeholder" src="../Images/people.png" width="30">
                    </span>
                    <div class="media-body ml-2 d-none d-lg-block">
                      <span class="mb-0 text-sm  font-weight-bold"><%= Session["username"] %></span>
                    </div>
                  </div>
                </a>
                  <div class="dropdown-menu dropdown-menu-arrow dropdown-menu-right">
                    <div class=" dropdown-header noti-title">
                      <h6 class="text-overflow m-0">Welcome!</h6>
                    </div>
                    <a href="../OnlineExersice.aspx" class="dropdown-item">
                      <i class="ni ni-single-02"></i>
                      <span>OnlineExersice</span>
                    </a>
                    <a href="../History.aspx" class="dropdown-item">
                      <i class="ni ni-support-16"></i>
                      <span>History</span>
                    </a>
                    <div class="dropdown-divider"></div>
                    <a id="SingOrLogout" runat="server" class="dropdown-item">
                      <i class="ni ni-user-run"></i>
                      <span id="spanTosay" runat="server"></span>
                    </a>
                  </div>
            </li>
          </ul>
        </div>
      </div>
    </nav>