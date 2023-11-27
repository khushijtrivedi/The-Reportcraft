using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Xml;

namespace ProtoType
{
    public partial class AllRecords : System.Web.UI.Page
    {
        public static string scon = ConfigurationManager.ConnectionStrings["LTprojectdb"].ConnectionString;
        SqlConnection con = new SqlConnection(scon);
        DataSet ds = new DataSet();
        DataSet ds_cause = new DataSet();
        DataSet ds_dept = new DataSet();
        DataSet ds_maint = new DataSet();
        SqlDataAdapter adp = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Closed || con.State == ConnectionState.Broken)
            {
                con.Open();
                BindGrid();
            }

        }
        protected void BindGrid()
        {
            adp = new SqlDataAdapter("select * from MAIN_DATA", con);
            ds = new DataSet();
            adp.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                GVDrecords.DataSource = ds.Tables[0];
                GVDrecords.DataBind();
            }
        }

        protected void btnexport_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/ms-excel";
            Response.AddHeader("content-disposition", "attachment; filename=DataIfo.xls");
            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GVDrecords.RenderControl(htw);
            Response.Output.Write(sw.ToString());
            Response.End();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            
        }

        protected void btnxml_Click(object sender, EventArgs e)
        {
            // Create a new XML document
            XmlDocument xmlDoc = new XmlDocument();

            // Create the root element
            XmlElement rootElement = xmlDoc.CreateElement("Records");
            xmlDoc.AppendChild(rootElement);

            // Loop through each row in the GridView
            foreach (GridViewRow row in GVDrecords.Rows)
            {
                // Create a new XML element for each row
                XmlElement rowElement = xmlDoc.CreateElement("Record");
                rootElement.AppendChild(rowElement);

                // Loop through each cell in the row
                for (int i = 0; i < row.Cells.Count; i++)
                {
                    // Get the header text and cell value
                    string headerText = GVDrecords.Columns[i].HeaderText;
                    string cellValue = row.Cells[i].Text;

                    // Create a new XML element for each cell
                    XmlElement cellElement = xmlDoc.CreateElement(headerText);
                    cellElement.InnerText = cellValue;
                    rowElement.AppendChild(cellElement);
                }
            }

            // Save the XML document to a file
            string filePath = Server.MapPath("~/App_Data/gridview_data.xml");
            xmlDoc.Save(filePath);

            // Optionally, provide the file as a download to the user
            Response.Clear();
            Response.ContentType = "text/xml";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + filePath);
            Response.TransmitFile(filePath);
            Response.End();
        }

        protected void btnpdf_Click(object sender, EventArgs e)
        {

        }
    }
}