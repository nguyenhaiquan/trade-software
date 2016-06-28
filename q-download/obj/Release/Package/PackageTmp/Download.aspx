<%@ Page Language="C#" MasterPageFile="DownloadSite.Master" AutoEventWireup="true" CodeBehind="Download.aspx.cs" Inherits="wStock2.Download" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Quantum 2012 Download</title>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">              
    Đăng ký tài khoản Quantum                       
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">  

    <form runat=server method="POST" class="user-info-from-cookie contact-form" action=Download.aspx>
         <ul style="font-size:12px;color:red;"> <%=wStock2.Globals.vContent %></ul>	    
                 
        <div class="form-item form-type-textfield form-item-subject">
	        <label for="edit-username">Tài khoản đăng nhập  <span class="form-required" title="This field is required.">*</span></label>	                
            <input type="text" id="username" name="username" value="" size="60" maxlength="255" class="form-text required" />
	    </div>	    
	    
	    <div class="form-item form-type-textfield form-item-subject">
	        <label for="edit-password">Mật khẩu<span class="form-required" title="This field is required.">*</span></label>	                
            <input type="password" id="password" name="password" value="" size="60" maxlength="255" class="form-text required" />
	    </div>	
	    
        <div class="form-item form-type-textfield form-item-subject">	         
	         <label for="edit-repassword">Nhập lại mật khẩu<span class="form-required" title="This field is required.">*</span></label>	                
            <input type="password" id="repassword" name="repassword" value="" size="60" maxlength="255" class="form-text required" />
	    </div>	
        <div class="form-item form-type-textfield form-item-subject">
	        <label for="edit-address">Email <span class="form-required" title="This field is required.">*</span></label>	                
            <input type="text" id="email" name="email" value="" size="60" maxlength="255" class="form-text required" />
        </div>	    	  
        
        <div class="form-item form-type-textfield form-item-subject">
	        <label for="edit-firstname">Họ </label>        
	        <input type="text" id="edit-firstname" name="firstname" value="" size="60" maxlength="255" class="form-text required" />
	    </div>
	    <div class="form-item form-type-textfield form-item-subject">
	        <label for="edit-lastname">Tên </label>                    
            <input type="text" id="lastname" name="lastname" value="" size="60" maxlength="255" class="form-text required" />
        </div>    
	    
	    <div class="form-item form-type-textfield form-item-subject">	         
	        <label for="edit-phone">Số điện thoại</label>	                
            <input type="text" id="phone" name="phone" value="" size="60" maxlength="255" class="form-text required" />
	    </div>	    
	    
	    <div class="button_div">
            <asp:Button ID="Button1" CssClass="buttons" runat="server" Text="Download" />
	    </div>
    </form>    	    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
     <p>
        Nếu bạn đã tạo tài khoản rồi, bấm vào
        <a href="Login.aspx">đây</a>.          
      </p>    
    
</asp:Content>
 <asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder4" runat="server"> 
     <br />
   <p>
       Những dòng bắt đầu bằng dấu <span style="color:Red;">*</span> là bắt buộc
      </p>                                           
</asp:Content>