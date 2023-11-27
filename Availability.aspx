<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Availability.aspx.cs" Inherits="ProtoType.Availability" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
            .report-container {
  max-width: 800px;
  margin: 0 auto;
  padding: 20px;
  background-color: #f0f0f0;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  font-family: Arial, sans-serif;
  text-align: center;
}

.company-info {
  display: flex;
  align-items: center;
  justify-content: center;
  margin-bottom: 20px;
}

.company-logo {
  width: 50px; /* Adjust the width as per your logo's dimensions */
  height: 50px; /* Adjust the height as per your logo's dimensions */
  margin-right: 10px;
}

.company-name {
  font-size: 28px;
  margin-bottom: 0;
}

.report-title {
  font-size: 24px;
  margin-bottom: 10px;
}

.horizontal-line {
  border: none;
  border-top: 2px solid #ccc;
  margin: 20px auto;
}

.gridview-content {
  /* Add any necessary styling for the GridView content */
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplMain" runat="server">
    <div align="center">
        <asp:Button runat="server" ID="btnpdf" Text="PDF" OnClick="btnpdf_Click" />
    </div>
  <div class="report-container">
    <div class="company-info">
      <img src="Logo.png" alt="Company Logo" class="company-logo">
      <h1 class="company-name">ReportCraft</h1>
    </div>
        <br />
        <br />
    <h2 class="report-title">DEPARTMENT AVAILABILITY</h2>
        <br />
        <br />
    <hr class="horizontal-line">
  
    <div class="gridview-content">
        <!-- Add your GridView here -->
            <asp:GridView ID="GVDreport" runat="server" CellPadding="2" CssClass="table table-bordered table-striped" AutoGenerateColumns="false" HeaderStyle-BackColor="Black" HeaderStyle-ForeColor="White">
                  <Columns>
                      <asp:BoundField HeaderText="DEPARTMENT" DataField="function_desc" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" />
                      <asp:TemplateField HeaderText="AVAILABILTY (%)" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                            <ItemTemplate>
                                <asp:Label ID="lbl_HT250" runat="server" Text='<%#Eval("AVAILABLE") %>'></asp:Label>
                            </ItemTemplate>
                      </asp:TemplateField>
                  </Columns>
            </asp:GridView>
     </div>
    </div>
</asp:Content>
