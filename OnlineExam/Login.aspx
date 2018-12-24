<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="OnlineExam.Login" %>

<!DOCTYPE html>
<html>
<head>
<title>Welcome to OnlineExam</title>
<meta name="viewport" content="width=device-width, initial-scale=1">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="keywords" content="" />
<script type="application/x-javascript"> addEventListener("load", function() { setTimeout(hideURLbar, 0); }, false); function hideURLbar(){ window.scrollTo(0,1); } </script>
<!-- Custom Theme files -->
<link href="Content/css/login.css" rel="stylesheet" type="text/css" media="all" />
<link href="Areas/layer/theme/default/layer.css" rel="stylesheet" type="text/css" media="all" />
<!-- //Custom Theme files -->
<!-- js -->
<script src="Scripts/jquery-3.3.1.min.js"></script>
<script src="Scripts/js/easyResponsiveTabs.js"></script>
<script src="Areas/layer/layer.js"></script>
<!-- //js -->
<style>
    .error { color: red; }
</style>
</head>
<body>
	<!-- main -->
	<div class="main">
		<h1>Online Exam</h1>
		<div class="login-form">
			<div class="sap_tabs w3ls-tabs">
				<div id="horizontalTab" style="display: block; width: 100%; margin: 0px;">
					<ul class="resp-tabs-list">
						<li class="resp-tab-item" aria-controls="tab_item-0" role="tab"><span>Login</span></li> 
						<li class="resp-tab-item" aria-controls="tab_item-1" role="tab"><label>/</label><span>Sign up</span></li>
					</ul>	
					<div class="clear"> </div>
                    <form id="form1" runat="server">
					    <div class="resp-tabs-container">
						    <div class="tab-1 resp-tab-content" aria-labelledby="tab_item-0">
							    <div class="login-agileits-top"> 
									    <p>User ID </p>
                                        <asp:TextBox ID="userid" runat="server"></asp:TextBox>
                                        <asp:CustomValidator ID="useridValidator" runat="server" Text="<a class='error'>学号要求必须输入10位整数</a>" ErrorMessage="学号要求必须输入10位整数" ControlToValidate="userid" OnServerValidate="useridValidator1_ServerValidate" EnableClientScript="false" Enabled="true"></asp:CustomValidator>
									    <p>User Name </p>
                                        <asp:TextBox ID="username" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="usernameRequiredFieldValidator3" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="username" Text="<a class='error'>姓名不能为空</a>"></asp:RequiredFieldValidator>
									    <p>Password</p>
                                        <asp:TextBox ID="password" runat="server" TextMode="Password"></asp:TextBox>
                                        <asp:CustomValidator ID="passwordCustomValidator" runat="server" Text="<a class='error'>密码必须在 6 - 10 位数字或者字母或者下划线</a>" ErrorMessage="密码必须在 6 - 10 位数字或者字母或者下划线" ControlToValidate="password" OnServerValidate="passwordValidator1_ServerValidate" EnableClientScript="false" Enabled="true"></asp:CustomValidator>
									    <input type="checkbox" id="brand" value="">
									    <label for="brand"><span></span> Remember me ?</label><br /> 
                                        <asp:Label ID="login_result" runat="server" Text=""></asp:Label>
                                        <asp:Button ID="login" runat="server" Text="LOGIN" OnClick="login_Click" CausesValidation="false"/>
							    </div>
							    <div class="login-agileits-bottom"> 
								    <p><a href="ForgetPassword.aspx">Forgot password?</a></p>
							    </div> 
						    </div> 
						    <div class="tab-1 resp-tab-content" aria-labelledby="tab_item-1">
							    <div class="login-agileits-top sign-up"> 
									    <p>User ID </p>
                                        <asp:TextBox ID="userid_sign" runat="server"></asp:TextBox>
                                        <asp:CustomValidator ID="userid_signCustomValidator2" runat="server" Text="<a class='error'>学号要求必须输入10位整数1</a>" ErrorMessage="学号要求必须输入10位整数" ControlToValidate="userid_sign" OnServerValidate="useridValidator1_ServerValidate" EnableClientScript="false" Enabled="true"></asp:CustomValidator>
									    <p>User Name </p>
                                        <asp:TextBox ID="username_sign" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="username_signRequiredFieldValidator1" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="username_sign" Text="<a class='error'>姓名不能为空</a>"></asp:RequiredFieldValidator>
									    <p>Your Email </p>
                                        <asp:TextBox ID="email_sign" runat="server"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="email_signValidator1" runat="server" ErrorMessage="RegularExpressionValidator" ControlToValidate="email_sign" Text="<a class='error'>邮箱不能为空或者格式不对</a>" ValidationExpression="[\w!#$%&'*+/=?^_`{|}~-]+(?:\.[\w!#$%&'*+/=?^_`{|}~-]+)*@(?:[\w](?:[\w-]*[\w])?\.)+[\w](?:[\w-]*[\w])?"></asp:RegularExpressionValidator>
									    <p>Password</p>
                                        <asp:TextBox ID="password_sign" runat="server" TextMode="Password"></asp:TextBox>
                                        <asp:CustomValidator ID="passwordValidator" runat="server" Text="<a class='error'>密码必须在 6 - 10 位数字或者字母或者下划线</a>" ErrorMessage="密码必须在 6 - 10 位数字或者字母或者下划线" ControlToValidate="password_sign" OnServerValidate="passwordValidator1_ServerValidate" EnableClientScript="false" Enabled="true"></asp:CustomValidator>
                                    	<p>Password Again</p>
                                        <asp:TextBox ID="password_again" runat="server" TextMode="Password"></asp:TextBox>
                                        <asp:CompareValidator ID="PasswordCompareValidator" runat="server" ErrorMessage="CompareValidator" ControlToCompare="password_sign" ControlToValidate="password_again" Text="<a class='error'>密码确认不一致</a>"></asp:CompareValidator>
                                        <input type="checkbox" id="brand1" value="">
									    <label for="brand1"><span></span>I accept the terms Use</label> 
                                        <asp:Button ID="signup" runat="server" Text="Sign up" OnClick="signup_Click" CausesValidation="false"/>
							    </div>
						    </div>
					    </div>	
                    </form>
				</div>	 
			</div> 
			<!-- ResponsiveTabs js -->
			<script type="text/javascript">
				$(document).ready(function () {
					$('#horizontalTab').easyResponsiveTabs({
						type: 'default', //Types: default, vertical, accordion           
						width: 'auto', //auto or any width like 600px
						fit: true   // 100% fit in a container
					});
				});
			</script>
			<!-- //ResponsiveTabs js -->
		</div>	
	</div>	
	<!-- //main -->
	<!-- copyright -->
	<div class="copyright">
		<p> © 2018 All rights reserved by Lcanboom </p>
	</div>
	<!-- //copyright --> 
</body>
</html>
