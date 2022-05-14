<%@ Page Title="" Language="C#" MasterPageFile="~/MaterPrincipal.Master" AutoEventWireup="true" CodeBehind="EditSession.aspx.cs" Inherits="Web_CCPS_APP.EditSession" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="border p-3 mt-4">
       <div class="row pb-2">
            <h2 class="text-primary text-center">Editer une Classe</h2>
            <hr />
           <br />
         <div class="form-group px-3" style="text-align:center">
            <asp:DropDownList runat="server" CausesValidation="True" AutoPostBack="true" Width="49%" CssClass="form-select" ID="cbo_A_Modifier" OnSelectedIndexChanged="cbo_A_Modifier_SelectedIndexChanged"/>
         </div>
         <div class="col-lg-6 p-3"> 
           <div class="card">
             <div class="card-header text-white bg-secondary text-center py-3">
                 Parametre de séléction 
             </div>

            <div class="p-3">
                <asp:DropDownList runat="server" CausesValidation="True" AutoPostBack="true" CssClass="form-select" ID="NomClasse"/>
            </div>

            <div class="p-3">
                <asp:DropDownList runat="server" CssClass="form-select" ID="dJourDeClasse" />
            </div>

            <div class="p-3">
                <asp:TextBox ID="txtMaxEtudiant" runat="server" rows="2" placeholder="Max Etudiant" class="form-control" ></asp:TextBox>
            </div>

            <div class="p-3">
                <asp:Label ID="lblDateDebut" runat="server">Debut de La Session: </asp:Label>
            </div>

            <div class="p-3">
                <asp:label id="lblMessage" runat="server" Text="" Font-Bold="True" Font-Size="Medium"></asp:label>
            </div>
               
         </div>         
      </div> 
     <!--Fin colonne 1 -->

    <div class="col-lg-6 p-3"> 
        <div class="card">
            <div class="card-header text-white bg-secondary text-center py-3">
                 Parametre de séléction        
            </div>
            <div class="card">
                <div class="p-3">
                    <asp:DropDownList runat="server" CssClass="form-select" ID="DrpProfesseurName" />
                </div>  
                
                <div class="text-center p-3">        
                    <asp:DropDownList runat="server"  CssClass="form-select" ID="DropHeureDeClasse"/>
                </div>

                <div class="p-3">
                    <asp:TextBox ID="txtMontant" runat="server" width="100%" placeholder="Montant En Gourde (HTG)" class="form-control" ></asp:TextBox> 
                </div>

                <div class="p-3">
                    <asp:Label ID="lblDateFin" runat="server">Fin de La Session: </asp:Label> 
                </div> 
             </div>   
           </div>           
         </div>
        <div class="text-center p-3">
            <asp:Button ID="btnCancel" runat="server" Width="20%" Text="CANCEL" class="btn text-white btn-info" OnClick="btncancel_Click"/>
            <asp:Button ID="BtnAddClasse" runat="server" Width="20%" Text="SAUVEGARDEZ" class="btn btn-success" OnClick="BtnAddClasse_Click"/> 
        </div>
     </div>
  </div>
</asp:Content>
