using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SamlEmulator
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        
        }
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            string xml = "&lt;?xml version='1.0' encoding='UTF-8'?><request><header><userId>*GUEST</userId><password>guest</password></header></request></request>";
            /*  string xml = "&lt;?xml version='1.0' encoding='UTF-8'?><request><header><userId>*GUEST</userId><password>guest</password></header><body><command>searchJobs</command><parms><parm>100</parm><parm>Open</parm><parm></parm><parm></parm><parm></parm><parm></parm><parm></parm><parm></parm></parms></body></request>";
              string url = "http://localhost:2064/SamlRequest.aspx";
              HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);


              //string s = "id="+Server.UrlEncode(xml);
              byte[] requestBytes = System.Text.Encoding.ASCII.GetBytes(xml);
              req.Method = "POST";
              req.ContentType = "text/xml;charset=utf-8";
              req.ContentLength = requestBytes.Length;            
              Stream requestStream = req.GetRequestStream();
              requestStream.Write(requestBytes, 0, requestBytes.Length);
              requestStream.Close();

              HttpWebResponse res = (HttpWebResponse)req.GetResponse();


              StreamReader sr = new StreamReader(res.GetResponseStream(), System.Text.Encoding.Default);
              string backstr = sr.ReadToEnd();
              TextBox1.Text = backstr;
              sr.Close();
              res.Close();*/
            Response.Clear();
            string xmlBase64 = Base64Encode(xml);
            string UrlCallBack= ConfigurationManager.AppSettings["postbackUrl"];
            /*   StringBuilder sb = new StringBuilder();
               sb.Append("<html>");
               sb.AppendFormat(@"<body onload='document.forms[""form""].submit()'>");
               sb.AppendFormat("<form name='form' action='{0}' method='post'>", "http://localhost:2064/SamlRequest.aspx");
               sb.AppendFormat("<input type='hidden' name='ID' value='{0}'>", "1");
               sb.AppendFormat("<input type='hidden' name='IsValid' value='{0}'>", "true");
               sb.AppendFormat("<input type='hidden' name='Name' value='{0}'>", TextBox1.Text);
               sb.AppendFormat("<input type='hidden' name='Email' value='{0}'>", TextBox1.Text);
               // Other params go here
               sb.Append("</form>");
               sb.Append("</body>");
               sb.Append("</html>");*/
            StringBuilder sb = new StringBuilder();
            sb.Append("<html>");
            sb.AppendFormat(@"<body onload='document.forms[""form""].submit()'>");
            sb.AppendFormat("<form name='form' action='{0}' method='post'>", "http://localhost:2064/SamlRequest.aspx");
            sb.AppendFormat("<input type='hidden' name='SAMLResponse' value='{0}'>", xmlBase64);      
            // Other params go here
            sb.Append("</form>");
            sb.Append("</body>");
            sb.Append("</html>");

            Response.Write(sb.ToString());

            Response.End();
        }
    }
}