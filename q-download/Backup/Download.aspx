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
	        <label for="edit-firstname">Họ <span class="form-required" title="This field is required.">*</span></label>        
	        <input type="text" id="edit-firstname" name="firstname" value="" size="60" maxlength="255" class="form-text required" />
	    </div>
	    <div class="form-item form-type-textfield form-item-subject">
	        <label for="edit-lastname">Tên <span class="form-required" title="This field is required.">*</span></label>                    
            <input type="text" id="lastname" name="lastname" value="" size="60" maxlength="255" class="form-text required" />
        </div>
        
        <div class="form-item form-type-textfield form-item-subject">
	        <label for="edit-address">Email <span class="form-required" title="This field is required.">*</span></label>	                
            <input type="text" id="email" name="email" value="" size="60" maxlength="255" class="form-text required" />
        </div>	    	  
        
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
	        <label for="edit-phone">Số điện thoại</label>	                
            <input type="text" id="phone" name="phone" value="" size="60" maxlength="255" class="form-text required" />
	    </div>	    
	    
	     <div class="form-item form-type-textfield form-item-subject">	       
	        <label for="edit-company">Công ty</label>	               
            <input type="text" id="company" name="company" value="" size="60" maxlength="255" class="form-text required" />
	    </div>	    
	    
	    <div class="form-item form-type-textfield form-item-subject">	 
	        <label for="edit-stockcompany">Công ty chứng khoán đã mở tài khoản<span class="form-required" title="This field is required.">*</span></label>	                            
            <select id="stockcompany" name="stockcompany" class="form-text required" >
                <option value="Công ty Cổ phần Chứng khoán Bảo Việt">Công ty Cổ phần Chứng khoán Bảo Việt</option>
                <option value="Công ty TNHH Chứng khoán Ngân hàng Đầu tư và Phát triển Việt Nam">Công ty TNHH Chứng khoán Ngân hàng Đầu tư và Phát triển Việt Nam</option>
                <option value="Công ty Cổ phần Chứng khoán Sài Gòn">Công ty Cổ phần Chứng khoán Sài Gòn</option>
                <option value="Công ty Cổ phần Chứng khoán Đệ Nhất">Công ty Cổ phần Chứng khoán Đệ Nhất</option>
                <option value="Công ty TNHH Chứng khoán Thăng Long">Công ty TNHH Chứng khoán Thăng Long</option>
                <option value="Công ty TNHH Chứng khoán Ngân hàng Á Châu">Công ty TNHH Chứng khoán Ngân hàng Á Châu</option>
                <option value="Công ty TNHH Chứng khoán Ngân hàng Công thương Việt Nam">Công ty TNHH Chứng khoán Ngân hàng Công thương Việt Nam</option>
                <option value="Công ty TNHH Chứng khoán Ngân hàng Nông nghiệp và Phát triển nông thôn Việt Nam">Công ty TNHH Chứng khoán Ngân hàng Nông nghiệp và Phát triển nông thôn Việt Nam</option>
                <option value="Công ty TNHH Chứng khoán Ngân hàng Ngoại thương Việt Nam">Công ty TNHH Chứng khoán Ngân hàng Ngoại thương Việt Nam</option>
                <option value="Công ty Cổ phần Chứng khoán Mê Kông">Công ty Cổ phần Chứng khoán Mê Kông</option>
                <option value="Công ty Cổ phần Chứng khoán Thành phố Hồ Chí Minh">Công ty Cổ phần Chứng khoán Thành phố Hồ Chí Minh</option>
                <option value="Công ty TNHH Một thành viên Chứng khoán Ngân hàng Đông Á">Công ty TNHH Một thành viên Chứng khoán Ngân hàng Đông Á</option>
                <option value="Công ty cổ phần chứng khoán Hải Phòng">Công ty cổ phần chứng khoán Hải Phòng</option>
                <option value="Công ty TNHH Chứng khoán Ngân hàng Thương mại Cổ phần Nhà Hà Nội">Công ty TNHH Chứng khoán Ngân hàng Thương mại Cổ phần Nhà Hà Nội</option>
                <option value="Công ty Cổ phần Chứng khoán Đại Việt">Công ty Cổ phần Chứng khoán Đại Việt</option>
                <option value="Công ty Cổ phần Chứng khoán An Bình">Công ty Cổ phần Chứng khoán An Bình</option>
                <option value="Công ty TNHH Chứng khoán Ngân hàng Sài gòn Thương Tín">Công ty TNHH Chứng khoán Ngân hàng Sài gòn Thương Tín</option>
                <option value="Công ty Cổ phần Chứng khoán Kim Long">Công ty Cổ phần Chứng khoán Kim Long</option>
                <option value="Công ty Cổ phần Chứng khoán Việt">Công ty Cổ phần Chứng khoán Việt</option>
                <option value="Công ty Cổ phần Chứng khoán Quốc tế Việt Nam">Công ty Cổ phần Chứng khoán Quốc tế Việt Nam</option>
                <option value="Công ty Cổ phần Chứng khoán VNDIRECT">Công ty Cổ phần Chứng khoán VNDIRECT</option>
                <option value="Công ty Cổ phần Chứng khoán Âu Lạc">Công ty Cổ phần Chứng khoán Âu Lạc</option>
                <option value="Công ty Cổ phần Chứng khoán Việt Nam">Công ty Cổ phần Chứng khoán Việt Nam</option>
                <option value="Công ty Cổ phần Chứng khoán Việt Tín">Công ty Cổ phần Chứng khoán Việt Tín</option>
                <option value="Công ty Cổ phần Chứng khoán Hà Thành">Công ty Cổ phần Chứng khoán Hà Thành</option>
                <option value="Công ty Cổ phần Chứng khoán Dầu Khí">Công ty Cổ phần Chứng khoán Dầu Khí</option>
                <option value="Công ty Cổ phần Chứng khoán Quốc Gia">Công ty Cổ phần Chứng khoán Quốc Gia</option>
                <option value="Công ty Cổ phần Chứng khoán Hà Nội">Công ty Cổ phần Chứng khoán Hà Nội</option>
                <option value="Công ty TNHH Chứng khoán Ngân hàng TMCP các doanh nghiệp ngoài quốc doanh Việt Nam">Công ty TNHH Chứng khoán Ngân hàng TMCP các doanh nghiệp ngoài quốc doanh Việt Nam</option>
                <option value="Công ty Cổ phần Chứng khoán Thủ Đô">Công ty Cổ phần Chứng khoán Thủ Đô</option>
                <option value="Công ty Cổ phần Chứng khoán Rồng Việt">Công ty Cổ phần Chứng khoán Rồng Việt</option>
                <option value="Công ty Cổ phần Chứng khoán Sao Việt">Công ty Cổ phần Chứng khoán Sao Việt</option>
                <option value="Công ty Cổ phần Chứng khoán Ngân hàng Thương mại Cổ phần Đông Nam Á">Công ty Cổ phần Chứng khoán Ngân hàng Thương mại Cổ phần Đông Nam Á</option>
                <option value="Công ty Cổ phần Chứng khoán Doanh nghiệp nhỏ và vừa Việt Nam">Công ty Cổ phần Chứng khoán Doanh nghiệp nhỏ và vừa Việt Nam</option>
                <option value="Công ty Cổ phần Chứng khoán Thiên Việt">Công ty Cổ phần Chứng khoán Thiên Việt</option>
                <option value="Công ty Cổ phần Chứng khoán Châu Á Thái Bình Dương">Công ty Cổ phần Chứng khoán Châu Á Thái Bình Dương</option>
                <option value="Công ty Cổ phần Chứng khoán Gia Anh">Công ty Cổ phần Chứng khoán Gia Anh</option>
                <option value="Công ty Cổ phần Chứng khoán Chợ Lớn">Công ty Cổ phần Chứng khoán Chợ Lớn</option>
                <option value="Công ty Cổ phần Chứng khoán Tân Việt">Công ty Cổ phần Chứng khoán Tân Việt</option>
                <option value="Công ty Cổ phần Chứng khoán Tràng An">Công ty Cổ phần Chứng khoán Tràng An</option>
                <option value="Công ty Cổ phần Chứng khoán Tầm Nhìn">Công ty Cổ phần Chứng khoán Tầm Nhìn</option>
                <option value="Công ty Cổ phần Chứng khoán Biển Việt">Công ty Cổ phần Chứng khoán Biển Việt</option>
                <option value="Công ty Cổ phần Chứng khoán Alpha">Công ty Cổ phần Chứng khoán Alpha</option>                
                <option value="Công ty Cổ phần Chứng khoán Ngân hàng Phát triển Nhà Đồng bằng Sông Cửu Long">Công ty Cổ phần Chứng khoán Ngân hàng Phát triển Nhà Đồng bằng Sông Cửu Long</option>
                <option value="Công ty Cổ phần Chứng khoán Thái Bình Dương">Công ty Cổ phần Chứng khoán Thái Bình Dương</option>
                <option value="Công ty Cổ phần Chứng khoán Phú Gia">Công ty Cổ phần Chứng khoán Phú Gia</option>
                <option value="Công ty Cổ phần Chứng khoán Đại Dương">Công ty Cổ phần Chứng khoán Đại Dương</option>
                <option value="Công ty Cổ phần Chứng khoán Phương Đông">Công ty Cổ phần Chứng khoán Phương Đông</option>
                <option value="Công ty Cổ phần Chứng khoán VINA">Công ty Cổ phần Chứng khoán VINA</option>
                <option value="Công ty Cổ phần Chứng khoán Hoàng Gia">Công ty Cổ phần Chứng khoán Hoàng Gia</option>
                <option value="Công ty Cổ phần Chứng khoán Hướng Việt">Công ty Cổ phần Chứng khoán Hướng Việt</option>
                <option value="Công ty Cổ phần Chứng khoán Cao Su">Công ty Cổ phần Chứng khoán Cao Su</option>
                <option value="Công ty Cổ phần Chứng khoán Nam Việt">Công ty Cổ phần Chứng khoán Nam Việt</option>
                <option value="Công ty Cổ phần Chứng khoán Việt Quốc">Công ty Cổ phần Chứng khoán Việt Quốc</option>
                <option value="Công ty Cổ phần Chứng khoán Gia Quyền">Công ty Cổ phần Chứng khoán Gia Quyền</option>
                <option value="Công ty Cổ phần Chứng khoán Âu Việt">Công ty Cổ phần Chứng khoán Âu Việt</option>
                <option value="Công ty Cổ phần Chứng khoán Quốc tế Hoàng Gia">Công ty Cổ phần Chứng khoán Quốc tế Hoàng Gia</option>
                <option value="Công ty Cổ phần Chứng khoán FPT">Công ty Cổ phần Chứng khoán FPT</option>
                <option value="Công ty Cổ phần Chứng khoán VNS">Công ty Cổ phần Chứng khoán VNS</option>
                <option value="Công ty Cổ phần Chứng khoán Đông Dương">Công ty Cổ phần Chứng khoán Đông Dương</option>
                <option value="Công ty Cổ phần Chứng khoán Đại Nam">Công ty Cổ phần Chứng khoán Đại Nam</option>
                <option value="Công ty cổ phần Chứng khoán Bản Việt">Công ty cổ phần Chứng khoán Bản Việt</option>
                <option value="Công ty Cổ phần Chứng khoán An Phát">Công ty Cổ phần Chứng khoán An Phát</option>
                <option value="Công ty Cổ phần Chứng khoán Sài Gòn – Hà Nội">Công ty Cổ phần Chứng khoán Sài Gòn – Hà Nội</option>
                <option value="Công ty Cổ phần Chứng khoán An Thành">Công ty Cổ phần Chứng khoán An Thành</option>
                <option value="Công ty Cổ phần Chứng khoán Gia Phát">Công ty Cổ phần Chứng khoán Gia Phát</option>
                <option value="Trung tâm giao dịch chứng khoán Hà nội Hanoi Stock Exchange">Trung tâm giao dịch chứng khoán Hà nội Hanoi Stock Exchange</option>                
                <option value="Uỷ Ban chứng khoán nhà nước Vietnam Securities Committee">Uỷ Ban chứng khoán nhà nước Vietnam Securities Committee</option>      
                <option value="Trung tâm giao dịch chứng khoán Tp Hồ Chí Minh HCMC Stock Exchange ">Trung tâm giao dịch chứng khoán Tp Hồ Chí Minh HCMC Stock Exchange</option>      
                <option value="VIPC Capital Management">VIPC Capital Management </option>      
            </select>
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