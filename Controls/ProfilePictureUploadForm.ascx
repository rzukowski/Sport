<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProfilePictureUploadForm.ascx.cs" Inherits="Controls_ProfilePictureUploadForm" %>

<div id="wrapperAjax" />
<<a class="popupBoxClose" onclick="unloadPopupBox(uploadProfilePicture)">X</a>
<form id="formToUploadProfilePic">
        <div>
        <input id="fileToUpload" type="file" name="fileToUpload" class="input">
        <button id="buttonUpload" onclick="return ajaxFileUpload('userName','userid')">Upload</button>
            
<div id="popUpMessage">
    
    <a class="popupBoxClose" onclick="return HideDiv('#popUpMessage')">X</a>
   
    <br />  <span id="errorOnProfilePictureUploadForm">text</span>
    
</div>
          
        </div>
    </form>

