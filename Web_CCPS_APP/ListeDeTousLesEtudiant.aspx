<%@ Page Title="" Language="C#" MasterPageFile="~/MaterPrincipal.Master" AutoEventWireup="true" CodeBehind="ListeDeTousLesEtudiant.aspx.cs" Inherits="Web_CCPS_APP.ListeDeTousLesEtudiant" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <div class="border p-3 mt-4">
       <div class="row pb-2">
            <h2 class="text-primary text-center text-capitalize ">Liste De Tous Les Etudiants</h2>
            <hr class="bg-primary"/>       
                       
            <div class="col-lg-6">
                <asp:DropDownList  ID="DropDownList1" runat="server" width="300px" Height="27" AutoPostBack="True">
                    <asp:ListItem>Selectionnez Etudiants Par</asp:ListItem>
                    <asp:ListItem>Etudiants dans la session courante</asp:ListItem>
                    <asp:ListItem>Tous les Etudiants</asp:ListItem>                                
                </asp:DropDownList>
            </div>

            <div class="col-lg-6"> 
                <asp:TextBox runat="server" width="300px" ID="txtSearch" PlaceHolder="SEARCH"></asp:TextBox>
            </div>

          
                        
            <div class="pt-3">
                 <asp:ListBox ID ="lstTousEtudiants"  runat ="server" Height ="300px" Width ="80%"></asp:ListBox>
            </div>               

            <label runat="server" id="lblError" font-color="red"></label>

           <div class="text-center">               
                  <asp:button runat="server" width="150px" CssClass="btn btn-primary" ID="btnEditez" text="EDITEZ" OnClick="btnEditez_Click"/>
           </div> 
      
       </div> 
  </div>
</asp:Content>
