<%@ Page Title="" Language="C#" MasterPageFile="~/MaterPrincipal.Master" AutoEventWireup="true" CodeBehind="ChangerHoraireEtudiant.aspx.cs" Inherits="Web_CCPS_APP.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <div class="border p-3 mt-4">
       <div class="row pb-2">
            <h1 class="text-primary text-capitalize text-center">Changer l'Horaire d'un Etudiant</h1>
            <hr class="bg-primary" />
         <div class="col-lg-4 pt-3"> 
             <div class="card">
                 <div class="card-header bg-secondary text-white text-center py-3 lead">
                       Choisissez un Etudiant
                  </div>
                 <div class="card-body" style="text-align:center">
                   <asp:TextBox ID="Recherche" runat="server" class="form-control" placeholder="Recherche" name="search" AutoPostBack="True" OnTextChanged="Recherche_TextChanged1"></asp:TextBox>
                     <br />
                   <asp:ListBox ID="lstTousEtudiants" width="260px" runat="server" Height="300px" AutoPostBack="True" OnSelectedIndexChanged="lstTousEtudiants_SelectedIndexChanged1"></asp:ListBox>
                  </div> 
             </div> 
        </div> 

        <div class="col-lg-4 pt-3"> 
            <div class="card">
                 <div class="card-header bg-secondary text-white text-center py-3 lead">
                  Classe à Enlever
                    </div>
                 <div class="card-body" style="text-align:center">
            <asp:ListBox ID="lstClassesEtudiant" runat="server" height="363px" width="260px"></asp:ListBox>
                     </div>
                </div>
        </div>
        
           <div class="col-lg-4 pt-3"> 
               <div class="card">
                 <div class="card-header bg-secondary text-white text-center py-3 lead">
                  Classe à Ajouter
                    </div>
                 <div class="card-body" style="text-align:center;margin-left:-10px">
                     <asp:ListBox ID="lstToutesClasses" runat="server" Height="363px" Width="270px"></asp:ListBox>
                    </div>
                   </div>
        </div>
       
           <div class="row text-center">
                <div class="col-sm-4">
                </div>
                <div class="col-sm-4">
                    <div class="form-group">
                        <asp:Label ID="lblMessage" runat="server" Font-Bold="True" ForeColor="#990000"></asp:Label>
                    </div>
                </div>
                <div class="col-sm-4 pt-3 text-center">
                    <div>
                        <asp:Button ID="Button1" runat="server" Font-Bold="True" Text="CHANGER" Width="209px" Height="40px" CausesValidation="False" UseSubmitBehavior="False" style="text-align: center" class="btn btn-success" OnClick="btnChangez_Click" />
                    </div>
                </div>
            </div>
       </div> 
  </div>
</asp:Content>

