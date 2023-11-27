<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="DataProcess.aspx.cs" Inherits="ProtoType.DataProcess" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script srs="https://code.jquery.com/jquery-3.6.0.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplMain" runat="server">
     <div align="center">
        <asp:Button ID="btnreturn" runat="server" Text="RETURN" CssClass="btn btn-success" OnClick="btnreturn_Click"/>
    </div>
    <br />
    <br />
    <br />
    
    <div class="row clearfix" style="font-size: smaller"> 
      <asp:GridView ID="GVDprocess" runat="server" CellPadding="2"
            CssClass="table table-bordered table-striped" AutoGenerateColumns="false" OnRowEditing="GVDprocess_RowEditing" OnRowUpdating="GVDprocess_RowUpdating" OnRowCommand="GVDprocess_RowCommand" OnRowDataBound="GVDprocess_RowDataBound">
                      <Columns>
                           <asp:TemplateField HeaderText="ID" HeaderStyle-HorizontalAlign="Center" HeaderStyle-BackColor="LightBlue" HeaderStyle-BorderColor="Black" HeaderStyle-BorderWidth="1px" ItemStyle-Width="60px" ItemStyle-Wrap="true">
                              <ItemStyle HorizontalAlign="Center" />
                               <ItemTemplate>
                                   <asp:TextBox runat="server" ID="lblrid" Text='<%# Bind("rid") %>'></asp:TextBox>
                               </ItemTemplate>
                              <EditItemTemplate>
                                  <asp:TextBox runat="server" ID="lblrid2" Text='<%# Bind("rid") %>' ></asp:TextBox>
                               </ItemTemplate>
                              </EditItemTemplate>
                           </asp:TemplateField>


                           <asp:TemplateField HeaderText="EQUIPMENT" HeaderStyle-HorizontalAlign="Center" HeaderStyle-BackColor="LightBlue" HeaderStyle-BorderColor="Black" HeaderStyle-BorderWidth="1px" ItemStyle-Width="60px" ItemStyle-Wrap="true">
                               <ItemStyle HorizontalAlign="Center" />
                               <ItemTemplate>
                                   <asp:TextBox runat="server" ID="lblequip" Text='<%# Bind("equip_desc") %>'></asp:TextBox>
                               </ItemTemplate>
                               <EditItemTemplate>
                                   <asp:TextBox runat="server" ID="lblequip2" Text='<%# Bind("equip_desc") %>'></asp:TextBox>
                               </EditItemTemplate>
                           </asp:TemplateField>

                         <asp:TemplateField HeaderText="START DATE" HeaderStyle-HorizontalAlign="Center" HeaderStyle-BackColor="LightBlue" HeaderStyle-BorderColor="Black" HeaderStyle-BorderWidth="1px" ItemStyle-Width="60px" ItemStyle-Wrap="true">
                             <ItemStyle HorizontalAlign="Center" />  
                             <ItemTemplate>
                                   <asp:TextBox runat="server" ID="lblsdate" Text='<%# Bind("start_date","{0:dd-MM-yyyy}") %>'></asp:TextBox>
                               </ItemTemplate>
                             <EditItemTemplate>
                                 <asp:TextBox runat="server" ID="lblsdate2" Text='<%# Bind("start_date","{0:dd-MM-yyyy}") %>'></asp:TextBox>
                             </EditItemTemplate>
                           </asp:TemplateField>

                          <asp:TemplateField HeaderText="START TIME" HeaderStyle-HorizontalAlign="Center" HeaderStyle-BackColor="LightBlue" HeaderStyle-BorderColor="Black" HeaderStyle-BorderWidth="1px" ItemStyle-Width="60px" ItemStyle-Wrap="true">
                              <ItemStyle HorizontalAlign="Center" /> 
                              <ItemTemplate>
                                   <asp:TextBox runat="server" ID="lblstime" Text='<%# Bind("start_time") %>'></asp:TextBox>
                               </ItemTemplate>
                              <EditItemTemplate>
                                  <asp:TextBox runat="server" ID="lblstime2" Text='<%# Bind("start_time") %>'></asp:TextBox>
                              </EditItemTemplate>
                           </asp:TemplateField>

                          <asp:TemplateField HeaderText="END DATE" HeaderStyle-HorizontalAlign="Center" HeaderStyle-BackColor="LightBlue" HeaderStyle-BorderColor="Black" HeaderStyle-BorderWidth="1px" ItemStyle-Width="60px" ItemStyle-Wrap="true">
                              <ItemStyle HorizontalAlign="Center" /> 
                              <ItemTemplate>
                                   <asp:TextBox runat="server" ID="lbledate" Text='<%# Bind("end_date","{0:dd-MM-yyyy}") %>'></asp:TextBox>
                               </ItemTemplate>
                              <EditItemTemplate>
                                  <asp:TextBox runat="server" ID="lbledate2" Text='<%# Bind("end_date","{0:dd-MM-yyyy}") %>'></asp:TextBox>
                              </EditItemTemplate>
                           </asp:TemplateField>
                          
                          <asp:TemplateField HeaderText="END TIME" HeaderStyle-HorizontalAlign="Center" HeaderStyle-BackColor="LightBlue" HeaderStyle-BorderColor="Black" HeaderStyle-BorderWidth="1px" ItemStyle-Width="60px" ItemStyle-Wrap="true">
                              <ItemStyle HorizontalAlign="Center" /> 
                              <ItemTemplate>
                                   <asp:TextBox runat="server" ID="lbletime" Text='<%# Bind("end_time") %>'></asp:TextBox>
                               </ItemTemplate>
                              <EditItemTemplate>
                                  <asp:TextBox runat="server" ID="lbletime2" Text='<%# Bind("end_time") %>'></asp:TextBox>
                              </EditItemTemplate>
                           </asp:TemplateField>
                          <asp:TemplateField HeaderText="DURATION" HeaderStyle-HorizontalAlign="Center" HeaderStyle-BackColor="LightBlue" HeaderStyle-BorderColor="Black" HeaderStyle-BorderWidth="1px" ItemStyle-Width="60px" ItemStyle-Wrap="true">
                              <ItemStyle HorizontalAlign="Center" /> 
                              <ItemTemplate>
                                   <asp:TextBox runat="server" ID="lbldur" Text='<%# Bind("duration_min") %>'></asp:TextBox>
                               </ItemTemplate>
                              <EditItemTemplate>
                                   <asp:TextBox runat="server" ID="lbldur2" Text='<%# Bind("duration_min") %>'></asp:TextBox>
                              </EditItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="MAINTENANCE" HeaderStyle-HorizontalAlign="Center" HeaderStyle-BackColor="LightBlue" HeaderStyle-BorderColor="Black" HeaderStyle-BorderWidth="1px" ItemStyle-Width="120px" ItemStyle-Wrap="true">
                               <ItemStyle HorizontalAlign="Center" />
                               <ItemTemplate>
                                   <asp:DropDownList ID="ddlmaint" runat="server" CssClass="form control " AutoPostBack="true">

                                   </asp:DropDownList>
                               </ItemTemplate>
                               <EditItemTemplate>
                                   <asp:DropDownList ID="ddlmaint1" runat="server" CssClass="form control " AutoPostBack="true">

                                   </asp:DropDownList>
                               </EditItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="CAUSE" HeaderStyle-HorizontalAlign="Center" HeaderStyle-BackColor="LightBlue" HeaderStyle-BorderColor="Black" HeaderStyle-BorderWidth="1px" ItemStyle-Width="120px" ItemStyle-Wrap="true">
                               <ItemStyle HorizontalAlign="Center" />
                               <ItemTemplate>
                                   <asp:DropDownList ID="ddlcause" runat="server" CssClass="form-control " AutoPostBack="true">
                </asp:DropDownList>
                               </ItemTemplate>
                               <EditItemTemplate>
                                   <asp:DropDownList ID="ddlcause1" runat="server" CssClass="form-control " AutoPostBack="true">
                </asp:DropDownList>
                               </EditItemTemplate>
                           </asp:TemplateField>
                          <asp:TemplateField HeaderText="RESPONSIBLE" HeaderStyle-HorizontalAlign="Center" HeaderStyle-BackColor="LightBlue" HeaderStyle-BorderColor="Black" HeaderStyle-BorderWidth="1px" ItemStyle-Width="120px" ItemStyle-Wrap="true">
                              <ItemStyle HorizontalAlign="Center" /> 
                              <ItemTemplate>
                                  <asp:DropDownList ID="ddlResponsible" runat="server" CssClass="form-control " AutoPostBack="true">
                </asp:DropDownList> 
                               </ItemTemplate>
                              <EditItemTemplate>
                                  <asp:DropDownList ID="ddlResponsible1" runat="server" CssClass="form-control " AutoPostBack="true">
                </asp:DropDownList>
                              </EditItemTemplate>
                           </asp:TemplateField>
                          <asp:TemplateField HeaderText="ACTION" HeaderStyle-HorizontalAlign="Center" HeaderStyle-BackColor="LightBlue" HeaderStyle-BorderColor="Black" HeaderStyle-BorderWidth="1px" ItemStyle-Width="60px" ItemStyle-Wrap="true">
                              <ItemStyle HorizontalAlign="Center" />
                              <ItemTemplate>
                                  <asp:Button ID="btnedit" runat="server" Text="EDIT" CommandName="Edit" />
                             
                              </ItemTemplate>
                              <EditItemTemplate>
                                  <asp:Button ID="btnupdate" runat="server" Text="UPDATE" CommandName="Update" />
                                   <asp:Button ID="btnsplit" runat="server" Text="SPLIT" CommandName="Insert" OnClientClick='return confirmDelete();'/>
                              </EditItemTemplate>
                          </asp:TemplateField>
                      </Columns>
                </asp:GridView>
    </div>
    <br />
    <br />
    <div align="center">
        <asp:Button ID="btnupload" runat="server" Text="UPLOAD" CssClass="btn btn-success" OnClick="btnupload_Click"/>
    </div>
    <script type="text/javascript">
        function confirmDelete() {
            return confirm('Are you sure you want to split?');
        }
    </script>
  
</asp:Content>
