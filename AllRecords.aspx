<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="AllRecords.aspx.cs" Inherits="ProtoType.AllRecords" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .button-container {
  display: flex;
  justify-content: center;
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplMain" runat="server">
   
    <div class="button-container">
       <asp:Button runat="server" ID="btnexport" Text="EXCEL" CssClass="btn btn-success" OnClick="btnexport_Click"/>
           &nbsp;
       <asp:Button runat="server" ID="btnxml" Text="XML" CssClass="btn btn-success" OnClick="btnxml_Click" />
        &nbsp;
        <asp:Button runat="server" ID="btnpdf" Text="PDF" CssClass="btn btn-success" OnClick="btnpdf_Click" />
    </div>
    <br />
    <br />
    <div class="row clearfix" style="font-size: smaller"> 
          
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="GVDrecords" runat="server" CellPadding="0" AllowPaging="false"
                            CssClass="table table-bordered table-striped"
                            ShowFooter="False"
                            EmptyDataText="No Data Available" 
                            AutoGenerateColumns="False"  EditRowStyle-BackColor="LightGray">
                            <HeaderStyle HorizontalAlign="Center" BackColor="LightGray"/>
                            <RowStyle HorizontalAlign="Center" />
                            
                            <Columns>
                                
                                <asp:BoundField HeaderText="RECORD_ID" DataField="record_id" ItemStyle-Width="8%" HeaderStyle-Width="8%"/>
                                <asp:BoundField HeaderText="DEPARTMNET_CODE" DataField="function_code" ItemStyle-Width="8%" HeaderStyle-Width="8%" />
                                <asp:BoundField HeaderText="DEPARTMENT_DESCRIPTION" DataField="function_desc" ItemStyle-Width="8%" HeaderStyle-Width="8%" />
                                <asp:BoundField HeaderText="EQUIPMENT_CODE" DataField="equip_code" ItemStyle-Width="8%" HeaderStyle-Width="8%" />
                                <asp:BoundField HeaderText="EQUIPMENT_DESCRIPTION" DataField="equip_desc" ItemStyle-Width="8%" HeaderStyle-Width="8%" />
                                <asp:BoundField HeaderText="EMPLOYEE_ID" DataField="Id" ItemStyle-Width="8%" HeaderStyle-Width="8%" />
                                <asp:BoundField HeaderText="START_DATE" DataField="start_date" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-Width="8%" HeaderStyle-Width="8%" />
                                <asp:BoundField HeaderText="START_TIME" DataField="start_time" ItemStyle-Width="8%" HeaderStyle-Width="8%" />
                                <asp:BoundField HeaderText="END_DATE" DataField="end_date" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-Width="8%" HeaderStyle-Width="8%"/>
                                <asp:BoundField HeaderText="END_TIME" DataField="end_time" ItemStyle-Width="8%" HeaderStyle-Width="8%" />
                                <asp:BoundField HeaderText="MAINTENANCE_CODE" DataField="maint_code" ItemStyle-Width="8%" HeaderStyle-Width="8%" />
                                <asp:BoundField HeaderText="RESPONSIBLE_DEPARTMENT" DataField="dept_code" ItemStyle-Width="8%" HeaderStyle-Width="8%" />
                                <asp:BoundField HeaderText="CAUSE_CODE" DataField="cause_code" ItemStyle-Width="8%" HeaderStyle-Width="8%"/>
                                <asp:BoundField HeaderText="UPDATED_BY" DataField="update_by" ItemStyle-Width="8%" HeaderStyle-Width="8%"/>
                                <asp:BoundField HeaderText="UPDATE_TIME" DataField="update_time" ItemStyle-Width="8%" HeaderStyle-Width="8%"/>
                                <asp:BoundField HeaderText="DURATION" DataField="duration_mins" ItemStyle-Width="8%" HeaderStyle-Width="8%"/>
                                
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
</asp:Content>
