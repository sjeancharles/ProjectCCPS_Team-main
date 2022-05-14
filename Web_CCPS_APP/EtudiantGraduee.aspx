<%@ Page Title="" Language="C#" MasterPageFile="~/MaterPrincipal.Master" AutoEventWireup="true" CodeBehind="EtudiantGraduee.aspx.cs" Inherits="Web_CCPS_APP.EtudiantGraduee" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="col-sm-12 border p-3 mt-4"> 
        <h2 class="text-primary text-center">Liste Des Etudiants Gradués</h2>
            <hr class="bg-primary"/>

        <div class="container">
            <div class="row">
               <div class="input-group">               
                   <asp:TextBox ID="Recherche" runat="server"  placeholder="Recherche par nom ou/et prénom" AutoPostBack="True" OnTextChanged="Recherche_TextChanged1" width="70%" ></asp:TextBox>            
                   <div class="input-group-btn">
                       <asp:Button ID="Button1" runat="server" class="btn" BackColor="#1C5E55" ForeColor="White" OnClick="Button1_Click" Text="Recherche" type="submit"  />                  
                   </div>
               </div>
            </div>   
        </div>  

        <div class="row pt-3">
            <div class="col-sm-12">
                <div class="form-group">                        
                    <asp:gridview runat="server" ID="gridviewId" AutoGenerateColumns = "False" CssClass="table table-responsive" 
                        AllowPaging="True" AllowSorting="True" HeaderStyle-BackColor="#0099cc" HeaderStyle-ForeColor="White" CellPadding="4" ForeColor="#333333" GridLines="None" OnPageIndexChanging="gridviewId_PageIndexChanging"   >
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField DataField="Nom" HeaderText="Nom" ReadOnly="True" />
                            <asp:BoundField DataField="Prenom" HeaderText="Prenom" />
                            <asp:BoundField DataField="DateCreee" HeaderText="Date" />
                        </Columns>
                
                        <EditRowStyle BackColor="#7C6F57" />
                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />

                        <HeaderStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True"></HeaderStyle>
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#E3EAEB" />
                        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#F8FAFA" />
                        <SortedAscendingHeaderStyle BackColor="#246B61" />
                        <SortedDescendingCellStyle BackColor="#D4DFE1" />
                        <SortedDescendingHeaderStyle BackColor="#15524A" />                
                    </asp:gridview >                
            </div>
        </div>
    </div>



    </div>
</asp:Content>
