<%@ Page Title="" Language="C#" MasterPageFile="~/MaterPrincipal.Master" AutoEventWireup="true" CodeBehind="VoirLesSessions.aspx.cs" Inherits="Web_CCPS_APP.VoirLesSessions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
         .cssPager td
        {
              padding-left: 4px;     
              padding-right: 4px;   
              

          }
         .sizeTab{
           
             padding-right:-80px;
         }
    </style>

<div class="border p-3 mt-4">  
     <h2 class="text-primary text-center">Liste Des Etudiants Gradués</h2>
     <hr class="bg-primary"/>

        <asp:GridView ID="gvDetails" Class="table table-responsive sizeTab" HeaderStyle-BackColor="#0099cc" HeaderStyle-ForeColor="White" CellPadding="2" ForeColor="#333333"
            GridLines="None" runat="server" ShowFooter="true" AllowPaging="true" PageSize="10" AutoGenerateColumns="false" DataKeyNames="SessionDateID" OnPageIndexChanging="gvDetails_PageIndexChanging" OnRowCancelingEdit="gvDetails_RowCancelingEdit"
            OnRowEditing="gvDetails_RowEditing" OnRowUpdating="gvDetails_RowUpdating" OnRowDeleting="gvDetails_RowDeleting" OnRowCommand ="gvDetails_RowCommand">
            <Columns>
<%--                <asp:BoundField DataField="SessionDateID" HeaderText="Session ID" ReadOnly="true" />--%>
               <%-- <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                         <asp:CheckBox ID="CheckBox1" runat="server"/>
                    </ItemTemplate>
                    <%--<EditItemTemplate>
                         <asp:CheckBox ID="CheckBox1" runat="server" Checked ='<%# Eval("Actif")%>'/>
                    </EditItemTemplate>
                          
                </asp:TemplateField>--%>
       
                <asp:TemplateField HeaderText="Date Debut">
                    <ItemTemplate>
                        <asp:Label ID="lblProductname" runat="server" Text='<%# Eval("SessionDateDebut")%>'/>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Textbox ID="txtDateDebut" input-type="date" runat="server" Text='<%# Eval("SessionDateDebut")%>'/>
                    </EditItemTemplate>
                    <FooterTemplate>
                       <%-- <asp:HiddenField ID="HiddenField1" runat="server" />
                        <asp:HiddenField ID="HiddenField2" runat="server" />--%>
                        <asp:TextBox ID="txtDateDebut" runat="server" />
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField ControlStyle-CssClass="w-25" HeaderText = "Date Fin">
                    <ItemTemplate>
                        <asp:Label ID="lblPrice" runat="server" Text='<%# Eval("SessionDateFin")%>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtDateFin" Width="100%" runat="server" Text='<%# Eval("SessionDateFin")%>'/>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtDateFin" runat="server" />
                    </FooterTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText = "Remarque" ControlStyle-CssClass="w-25">
                    <ItemTemplate>
                        <asp:Label ID="lblRemarque" Width="100%" runat="server" Text='<%# Eval("Remarque")%>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtRemarque" Width="100%" runat="server" Text='<%# Eval("Remarque")%>'/>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtRemarque" Width="100%" runat="server" />
                    </FooterTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText = "Actif" ControlStyle-CssClass="w-25">
                    <ItemTemplate>
                        <asp:Label ID="lblActif" Width="100%" runat="server" Text='<%# Eval("Actif")%>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtActif" Width="100%" runat="server" Text='<%# Eval("Actif")%>'/>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtActif" Width="100%" runat="server" />
                        <asp:Button ID="btnAdd" CommandName="AddNew" runat="server" Text="Add"  width="100%" Class="btn btn-info text-white" />
                    </FooterTemplate>
                </asp:TemplateField>

               <asp:CommandField ShowEditButton="True" ShowDeleteButton="true" />

            </Columns>

            <EditRowStyle BackColor="#7C6F57" />
                                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />

                                <HeaderStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True"></HeaderStyle>
                                <PagerStyle BackColor="#666666" CssClass="cssPager" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#E3EAEB" />
                                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#F8FAFA" />
                                <SortedAscendingHeaderStyle BackColor="#246B61" />
                                <SortedDescendingCellStyle BackColor="#D4DFE1" />
                                <SortedDescendingHeaderStyle BackColor="#15524A" />  

        </asp:GridView>    
        <asp:Label ID="lblMessage" runat="server"></asp:Label>
       <br />
     <asp:Button Text="Save" runat="server" width="15%" Class="btn btn-success text-white"/>
   </div>
</asp:Content>
