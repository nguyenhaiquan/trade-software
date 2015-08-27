<%@ Page Language="C#" MasterPageFile="~/DownloadSite.Master" AutoEventWireup="true" CodeBehind="DownloadSuccess.aspx.cs" Inherits="wStock2.DownloadSuccess" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"> 
 <script language=javascript>
jQuery(document).ready(function() {  
   window.open('DownloadHandler.ashx?File=Quantum_0202beta.zip');
});
 </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">

                Tải về thành công                
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server"> 
<a id="tempID" style="display:none"></a>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
 <p>Bạn đã tải về thành công, đồng thời bạn đã tạo tài khoản trong hệ thống Quantum. </p>
 <p>Với tài khoản này bạn có thể sử dụng trên phần mềm của chúng tôi.</p>
 
 
 <p>
 Trân trọng,</p>
 <p>
 <b>Quantum Invest Team</b>
 </p>
</asp:Content>
