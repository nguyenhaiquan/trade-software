<%@ Page Language="C#" MasterPageFile="~/DownloadSite.Master" AutoEventWireup="true"
    CodeBehind="Login.aspx.cs" Inherits="wStock2.Register" Title="Untitled Page" %>
   
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Login</title>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">    
            Đăng nhập   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">            
        <form  method="POST" class="form" runat=server>
         <ul style="font-size:12px;color:red;"> <%=wStock2.Globals.vContent %></ul>        	    
	     <div class="form-item form-type-textfield form-item-subject">	    
        <label for="edit-company">Tài khoản<span class="form-required" title="This field is required."></span></label>	                                 
             <input type="text" id="username" name="username" value="" size="60" maxlength="255" class="form-text required" />
        </div>
        
        <div class="form-item form-type-textfield form-item-subject">	    
        <label for="password">
            <label for="edit-company">Mật khẩu<span class="form-required" title="This field is required."></span></label>	                                                     
            <input type="password" id="password" name="password" value="" size="60" maxlength="255" class="form-text required" />
        </div>
        <div class="button_div">
            <asp:Button ID="Button1" runat="server" Text="Download" CssClass="buttons" onclick="Button1_Click1" 
               />          
        </div>
        </form>
        <div class="clear">
        </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <p>Xin vui lòng đăng nhập để tải về.
    Hoặc có thể <a href="Download.aspx">ĐĂNG KÝ</a></p>
</asp:Content>
