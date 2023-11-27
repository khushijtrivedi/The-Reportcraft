using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using MimeKit;
using MimeKit.Text;
using System.Net.Mail;
using System.IO;
using SendGrid.Helpers.Mail;
using SendGrid;
using System.Threading.Tasks;
using System.Web.Hosting;
using Org.BouncyCastle.Math.Field;

namespace ProtoType
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        private static string scon = ConfigurationManager.ConnectionStrings["LTprojectdb"].ToString();
        SqlConnection con = new SqlConnection(scon);
        SqlDataAdapter adp = new SqlDataAdapter();
        DataSet ds = new DataSet();

        
        private async Task SendEmailWithSendGrid(string email,string userName,string pass)
        {
            
            var apiKey = "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";
            var client = new SendGridClient(apiKey);

            var from = new EmailAddress("khushi.trivedi.j@gmail.com", "REPORTCRAFT");
            var subject = "Forgot Password";
            var to = new EmailAddress(email, userName);
            var plainTextContent = "Your password : "+pass+" ";
            var htmlContent = "<strong>USERNAME : "+userName+"<BR> PASSWORD: "+pass+".</strong>";

            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }

        protected void cmdLogin_Click(object sender, EventArgs e)
        {
            string email = txtUser.Text;
            string pass = " ";
            string user = " ";
            adp = new SqlDataAdapter("select * from employeetb where email = '"+txtUser.Text+"' ",con);
            ds = new DataSet();
            adp.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0) 
            {
                user = ds.Tables[0].Rows[0]["name"].ToString();
                pass = ds.Tables[0].Rows[0]["credentials"].ToString();
                SendEmailWithSendGrid(email, user, pass);
                Response.Redirect("Login.aspx");
            }
            else
            {
                string script = "setTimeout(function(){ alert('ENTER REGISTERED EMAIL ADDRESS'); }, 100);";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
            }
        }
    }
}