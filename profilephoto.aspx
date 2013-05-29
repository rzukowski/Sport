<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="profilephoto.aspx.cs" Inherits="profilephoto" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxControlToolkit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <script type="text/javascript">
         function uploadComplete(sender, args) {
             alert("uploadCompleteBegin");
             var imgDisplay = $get("photo");
            // var img = new Image();
            // img.src = "/photos/" + args.get_fileName();
            // imgDisplay.src = img.src;
         }
</script>
     <div id="Div1"></div>
    <h3>Zmień zdjęcie</h3>
Wybierz plik : 
    <asp:ScriptManager   
            ID="ScriptManager1"  
            runat="server"  
            >  
        </asp:ScriptManager>  
    <cc1:asyncfileupload ID="AsyncFileUpload1" runat="server" UploadingBackColor="Yellow" OnClientUploadComplete="uploadComplete"
            OnUploadedComplete="ProcessUpload" ThrobberID="spanUploading" />
<span id="spanUploading" runat="server">Uploading...</span>
    
    <asp:Label ID="lblMsg" runat="server"></asp:Label>
   <asp:Label runat="server" Text=" " ID="uploadResult" />
<br />
    
</asp:Content>

