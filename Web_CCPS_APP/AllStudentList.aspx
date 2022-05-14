<%@ Page Title="" Language="C#" MasterPageFile="~/MaterPrincipal.Master" AutoEventWireup="true" CodeBehind="AllStudentList.aspx.cs" Inherits="Web_CCPS_APP.AllStudentList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
         .cssPager td
        {
              padding-left: 4px;     
              padding-right: 4px;    
          }
    </style>
<div class="border p-3 mt-4">  

     <h2 class="text-primary text-center">Liste De tout les Etudiants de la CCPS</h2>
            <hr class="bg-primary"/>

        <div class="container">
            <div class="row pb-2" style="text-align:end">
               <div class="input-group">               
                   <asp:TextBox ID="Recherche" runat="server"  placeholder="Recherche par nom ou/et prénom" AutoPostBack="True" width="25%" ></asp:TextBox>            
                   <div class="input-group-btn">
                       <asp:Button ID="Button1" runat="server" class="btn" BackColor="#1C5E55" ForeColor="White" Text="Recherche" type="submit"  />                  
                   </div>
               </div>
            </div>   
        </div>  

    <asp:TextBox ID="txtPersonneID" runat="server" Visible="false"></asp:TextBox>
    <asp:GridView ID="GridView1" CssClass="table table-responsive"  HeaderStyle-BackColor="#0099cc" HeaderStyle-ForeColor="White" CellPadding="4" ForeColor="#333333" GridLines="None" runat="server" 
        AutoGenerateColumns="false" DataKeyNames="PersonneID" OnPageIndexChanging="GridView1_PageIndexChanging" 
        OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" PageSize="10" AllowPaging="true">  
         <AlternatingRowStyle BackColor="White" />
        <Columns>  
            <asp:BoundField DataField="PersonneID" HeaderText="S.No." />  
            <asp:BoundField DataField="Nom" HeaderText="Nom" />  
            <asp:BoundField DataField="Prenom" HeaderText="Prenom" />  
            <asp:BoundField DataField="DateCreee" HeaderText="DateCréee" />  
            <asp:BoundField DataField="Telephone1" HeaderText="Telephone" />  
<%--            <asp:CommandField ShowEditButton="true"/>  --%>
<%--            <asp:CommandField ShowDeleteButton="true"/> --%>

            <asp:TemplateField>
                <ItemTemplate>
<%--                    <i class="bi bi-trash"></i>--%>
                    <asp:Button ID="editRow" runat="server" CommandName="Edit" Text=" Edit " width="100%" Class="btn btn-info" />
                </ItemTemplate>
        </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID="deleteRow" runat="server" CommandName="Delete" Text="Delete" width="100%" Class="btn btn-danger" />
                </ItemTemplate>
        </asp:TemplateField>
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
    <asp:Label runat="server" class="text-primary text-center" Font-Bold="true" Font-Size="Larger" ID="lblPagePos"></asp:Label>
    <br /><br />
   
</div>  
<div>  
    <asp:Label ID="lblMessage" runat="server"></asp:Label>  
</div>  
</asp:Content>
