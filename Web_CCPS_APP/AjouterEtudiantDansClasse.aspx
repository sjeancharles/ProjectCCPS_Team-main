<%@ Page Title="" Language="C#" MasterPageFile="~/MaterPrincipal.Master" AutoEventWireup="true" CodeBehind="AjouterEtudiantDansClasse.aspx.cs" Inherits="Web_CCPS_APP.AjouterEtudiantDansClasse" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <div class="border p-3 mt-4">
        <asp:TextBox ID="txtPersonneID" runat="server" Visible="False"></asp:TextBox>
       <div class="row pb-2">
            <h2 class="text-primary text-center">Ajouter Un Etudiant dans une Classe</h2>
            <hr class="bg-primary"/>
         <div class="col-lg-6 p-3"> 
           <div class="card">
             <div class="card-header text-white bg-secondary text-center py-3">
                 Paramètre de séléction 
             </div>
               <div class="card-body">
                    <asp:DropDownList ID="ListeCours" runat="server" AutoPostBack="True" Height="37px" Width="100%" OnSelectedIndexChanged="ListeCours_SelectedIndexChanged" BackColor="#D2D2D2" Font-Bold="True" Font-Size="Medium" ForeColor="Black" ToolTip="Cliquez pour choisir une option"></asp:DropDownList>   
              </div>
                <div class="card-body">
                    <asp:DropDownList ID="ListeNiveau" runat="server" AutoPostBack="True" Height="37px" Width="100%" OnSelectedIndexChanged="ListeNiveau_SelectedIndexChanged" BackColor="#D2D2D2" Font-Bold="True" Font-Size="Medium" ForeColor="Black" ToolTip="Cliquez pour choisir  une classe"> </asp:DropDownList> 
                </div>
               <div class="card-body">
                    <asp:DropDownList ID="ListeHoraire" runat="server" AutoPostBack="True" Height="37px" Width="100%" OnSelectedIndexChanged="ListeHoraire_SelectedIndexChanged" BackColor="#D2D2D2" Font-Bold="True" Font-Size="Medium" ForeColor="Black" ToolTip="Cliquer pour choisir l'horaire"> </asp:DropDownList>
            </div>
         </div>
             <br />
             <asp:Literal ID="progressbarId" runat="server"></asp:Literal>  
        </div> 
        <div class="col-lg-6 p-3"> 
           <div class="card">
             <div class="card-header text-white bg-secondary text-center">
                 <div class="row">
                 <div class="col-4 p-2">
                      Choisir un Étudiant
                 </div> 
               
               <div class="col-8 text-end">
                    <form class="d-flex">
                          <asp:TextBox ID="Seach" TextMode="Search" placeholder="Chercher un étudiant" runat="server" AutoPostBack="True" Height="27px" OnTextChanged="Seach_TextChanged" Width="197px" BackColor="#D2D2D2" Font-Bold="True" ToolTip="Recherchez par nom ou prenom"></asp:TextBox>
                         <button class="btn btn-secondary my-2 my-sm-0" type="submit"><i class="bi bi-search"></i></button>
                    </form>
                </div>
                </div>
             </div>
            <div class="card-body">
               <asp:ListBox ID="lstClasse" class="navbar navbar-light bg-light flex-column align-items-stretch p-3" height="152px" Width="100%" runat="server" AutoPostBack="True" BackColor="#D2D2D2" Font-Bold="True" Font-Size="Medium" ForeColor="Black" OnSelectedIndexChanged="lstClasse_SelectedIndexChanged"></asp:ListBox>
            </div>
         </div>
        </div>
       <div class="text-center">        
            <asp:Button ID="btnCancel" runat="server" Width="20%" Height="40px" Text="CANCEL" class="btn btn-info mx-2 text-white"/>
            <asp:Button ID="btnTerminer" runat="server" Width="20%" Height="40px" Text="TERMINEZ" class="btn btn-success mx-2" OnClick="btnTerminer_Click"/>
       </div>
        <div>
            <asp:Label ID="lblMessage" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="GREEN"></asp:Label> 
        </div>
       </div> 
  </div>
</asp:Content>
