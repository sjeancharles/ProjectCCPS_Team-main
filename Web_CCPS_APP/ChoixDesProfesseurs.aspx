<%@ Page Title="" Language="C#" MasterPageFile="~/MaterPrincipal.Master" AutoEventWireup="true" CodeBehind="ChoixDesProfesseurs.aspx.cs" Inherits="Web_CCPS_APP.ChoixDesProfesseurs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="border p-3 mt-4">
       <div class="row pb-2">
            <h2 class="text-primary text-center">Choix Des Professeurs</h2>
            <hr class="text-primary" />
           <div class="col-lg-1"></div>
         <div class="col-lg-6"> 
           <div class="card">
             <div class="card-header text-white bg-secondary text-center py-3">
                 Selectionnez un ou plusieurs professeurs
             </div>

            <div class="mb-3 p-4">
                 <asp:CheckBoxList ID="ChProfActifID" runat="server" Height="100px" AutoPostBack="true" SelectionMode="Multiple" OnSelectedIndexChanged="ChProfActifID_SelectedIndexChanged"></asp:CheckBoxList>
            </div>

            <div class="p-3">
                <asp:label id="lblMessage" runat="server" Text="" text-color="green" Font-Bold="True" Font-Size="Medium" ForeColor="red"></asp:label>
            </div>
               
         </div>           
      </div> 

      <div class="col-lg-4">
          <div class="card">
             <div class="card-header text-white bg-secondary text-center py-3">
                 Résultat de votre choix
             </div>
             <Textarea ID="Textarea1" name="txtAnnonce" width="400px" runat="server" placeholder="List des Professeurs choisir" rows="10" class="form-control" ></Textarea>
          </div>
     </div>
    <div class="col-lg-1"></div>

    <div class="p-3 text-center">
        <asp:Button ID="btnCancel" runat="server" Width="20%" Height="45px" Text="CANCEL" class="btn btn-info m-2 text-white" OnClick="btncancel_Click"/>
        <asp:Button ID="SaveProfChoisir" runat="server" Text="SAUVEGARDEZ" Width="20%" Height="45px" class="btn btn-primary m-2" OnClick="SaveProfChoisir_Click"/>
    </div> 
    </div>
  </div>
</asp:Content>
