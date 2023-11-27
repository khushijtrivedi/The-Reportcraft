using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;

namespace ProtoType
{
    public partial class StdReportHalfYear : System.Web.UI.Page
    {
        public static string scon = ConfigurationManager.ConnectionStrings["LTprojectdb"].ConnectionString;
        SqlConnection con = new SqlConnection(scon);
        DataSet ds = new DataSet();
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
            adp = new SqlDataAdapter("SELECT (1 - (SUM(duration_min)/DATEDIFF(MINUTE, DATEADD(MONTH, -6, GETDATE()), GETDATE())))*100 as AVAILABLE,function_desc FROM MAIN_DATA_R WHERE start_date >= DATEADD(MONTH,-6,GETDATE()) AND start_date <= GETDATE() GROUP BY function_desc", con);
            ds = new DataSet();
            adp.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                GVDreport.DataSource = ds.Tables[0];
                GVDreport.DataBind();
            }
        }

        protected void btncolumn_Click(object sender, EventArgs e)
        {
            Response.Redirect("HalfYearlyColumn.aspx");
        }

        protected void btnpie_Click(object sender, EventArgs e)
        {
            Response.Redirect("HalfYearlyPie.aspx");
        }
        protected void btnpdf_Click(object sender, EventArgs e)
        {

            // Create a new PDF document
            iTextSharp.text.Document document = new iTextSharp.text.Document();

            // Create a new PDF writer and bind it to a memory stream
            MemoryStream memoryStream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);

            // Open the document for writing
            document.Open();

            // Set up the styles
            iTextSharp.text.Font companyNameFont = iTextSharp.text.FontFactory.GetFont(iTextSharp.text.FontFactory.HELVETICA_BOLD, 28, iTextSharp.text.BaseColor.BLACK);
            iTextSharp.text.Font reportTitleFont = iTextSharp.text.FontFactory.GetFont(iTextSharp.text.FontFactory.HELVETICA_BOLD, 24, iTextSharp.text.BaseColor.BLACK);
            iTextSharp.text.Font columnHeaderFont = iTextSharp.text.FontFactory.GetFont(iTextSharp.text.FontFactory.HELVETICA_BOLD, 12, iTextSharp.text.BaseColor.BLACK);
            iTextSharp.text.Font cellDataFont = iTextSharp.text.FontFactory.GetFont(iTextSharp.text.FontFactory.HELVETICA, 12, iTextSharp.text.BaseColor.BLACK);

            // Add the company logo, company name, and report title to the PDF document
            PdfPTable headerTable = new PdfPTable(2);
            headerTable.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            headerTable.WidthPercentage = 100;

            // Add the company logo cell
            PdfPCell logoCell = new PdfPCell();
            logoCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            logoCell.HorizontalAlignment = Element.ALIGN_LEFT;
            logoCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            iTextSharp.text.Image logoImage = iTextSharp.text.Image.GetInstance(Server.MapPath("Logo.png")); // Provide the correct path to your logo image
            logoImage.ScaleToFit(50, 50); // Adjust the logo size as needed
            logoCell.AddElement(logoImage);
            headerTable.AddCell(logoCell);

            // Add the company name cell
            PdfPCell companyNameCell = new PdfPCell(new Phrase("ReportCraft", companyNameFont));
            companyNameCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            companyNameCell.HorizontalAlignment = Element.ALIGN_LEFT;
            companyNameCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            headerTable.AddCell(companyNameCell);

            // Add the header table to the document
            document.Add(headerTable);

            // Add spacing between the header and report title
            document.Add(new Paragraph("\n\n"));

            // Add the report title
            Paragraph reportTitle = new Paragraph("HALF YEARLY REPORT", reportTitleFont);
            reportTitle.Alignment = Element.ALIGN_CENTER;
            document.Add(reportTitle);

            // Add spacing between the report title and GridView data
            document.Add(new Paragraph("\n\n"));
            // Add the horizontal line
            PdfPTable horizontalLineTable = new PdfPTable(1);
            horizontalLineTable.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            horizontalLineTable.WidthPercentage = 100;
            PdfPCell horizontalLineCell = new PdfPCell(new Phrase(""));
            horizontalLineCell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
            horizontalLineCell.BorderColorBottom = BaseColor.BLACK;
            horizontalLineTable.AddCell(horizontalLineCell);
            document.Add(horizontalLineTable);

            // Add spacing between the horizontal line and the GridView data
            document.Add(new Paragraph("\n\n"));

            // Create the table for GridView data
            PdfPTable gridViewTable = new PdfPTable(GVDreport.Columns.Count);
            gridViewTable.DefaultCell.Border = iTextSharp.text.Rectangle.BOX; // Add a border to the table
            gridViewTable.WidthPercentage = 100;

            // Add the column headers
            foreach (DataControlFieldHeaderCell headerCell in GVDreport.HeaderRow.Cells)
            {
                PdfPCell columnHeader = new PdfPCell(new Phrase(headerCell.Text, columnHeaderFont));
                columnHeader.HorizontalAlignment = Element.ALIGN_CENTER;
                columnHeader.VerticalAlignment = Element.ALIGN_MIDDLE;
                gridViewTable.AddCell(columnHeader);
            }

            // Add the data rows
            foreach (GridViewRow row in GVDreport.Rows)
            {
                string department = row.Cells[0].Text;
                string availability = ((Label)row.FindControl("lbl_HT250")).Text;

                PdfPCell departmentCell = new PdfPCell(new Phrase(department, cellDataFont));
                departmentCell.HorizontalAlignment = Element.ALIGN_CENTER;
                departmentCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                gridViewTable.AddCell(departmentCell);

                PdfPCell availabilityCell;
                if (string.IsNullOrEmpty(availability))
                {
                    availabilityCell = new PdfPCell(new Phrase("N/A", cellDataFont));
                }
                else
                {
                    availabilityCell = new PdfPCell(new Phrase(availability, cellDataFont));
                }
                availabilityCell.HorizontalAlignment = Element.ALIGN_CENTER;
                availabilityCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                gridViewTable.AddCell(availabilityCell);
            }

            // Add the GridView table to the document
            document.Add(gridViewTable);

            // Close the document
            document.Close();

            // Clear the response
            Response.Clear();

            // Set the content type and headers for the response
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "attachment;filename=HalfYearReport.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            // Write the PDF document bytes to the response stream
            Response.BinaryWrite(memoryStream.ToArray());
            Response.End();
        }
        protected void refreshbtn_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
    }
}