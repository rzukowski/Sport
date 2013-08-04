<%@ WebHandler Language="C#" Class="AjaxFileUpload" %>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using UsrCode;
   /// <summary>
   /// Summary description for AjaxFileUploader
   /// </summary>
   public class AjaxFileUpload : IHttpHandler
   {

       public void ProcessRequest(HttpContext context)
       {
            
           if (context.Request.Files.Count > 0 && context.Request["name"]!=null)
           {

               string username = context.Request["name"].ToString().Replace("'","");
               string userid = context.Request["id"].ToString().Replace("'", "");
               
               string strPath = "gallery\\";
               string path = context.Server.MapPath("./") + strPath + "\\" + username;

               
               
               if (!Directory.Exists(path))
                   Directory.CreateDirectory(path);

               var file = context.Request.Files[0];

              

               string fileName;

               if (HttpContext.Current.Request.Browser.Browser.ToUpper() == "IE")
               {
                   string[] files = file.FileName.Split(new char[] { '\\' });
                   fileName = files[files.Length - 1];
               }
               else
               {
                   fileName = file.FileName;
               }
               string strFileName=fileName ;
               string msg = null;
               if (Usr.extensions.Any(strFileName.Contains))
               {

                   System.Drawing.Image img = System.Drawing.Image.FromStream(file.InputStream);
                   int fileHeight = img.Height;
                   int fileWidth = img.Width;
                   if (fileHeight > Usr.maxImgHeight || fileWidth > Usr.maxImgWidth || file.ContentLength > Usr.maxRozmiar)
                   {
                       msg = "{";
                       msg += string.Format("error:'{0}',\n", "Zły rozmiar pliku. Maksymalna rozdzielczość: " + Usr.maxImgHeight + "px/" + Usr.maxImgWidth + "px. Maksymalny rozmiar: " + Usr.maxRozmiar / 1048576 + " MB");
                       msg += string.Format("msg:'{0}'\n", strFileName);
                       msg += "}";
                       context.Response.Write(msg);
                       return;
                   }

               }

               fileName = Path.Combine(path, fileName);
               file.SaveAs(fileName);
               
               
               //saves profile picture to database
               
                   
              
               msg = "{";
               msg += string.Format("error:'{0}',\n", string.Empty);
               msg += string.Format("msg:'{0}'\n", strFileName);
               msg += "}";

               if (!Usr.SaveFileToDatabase(userid, "gallery\\"+strFileName, true))
               {
                   msg = "{";
                   msg += string.Format("error:'{0}',\n", "Zdjęcie znajduje się już w bazie");
                   msg += string.Format("msg:'{0}'\n", strFileName);
                   msg += "}";
               }
               context.Response.Write(msg); 

              
           }
       }

       public bool IsReusable
       {
           get
           {
               return true;
           }
       }
   }


