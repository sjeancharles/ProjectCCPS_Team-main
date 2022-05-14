<%@ Page Title="" Language="C#" MasterPageFile="~/MaterPrincipal.Master" AutoEventWireup="true" CodeBehind="SalleDeClasse.aspx.cs" Inherits="Web_CCPS_APP.SalleDeClasse" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
    <!--col 1-->
    <div class="col-lg-2">

    </div>
    <!--col 2-->
    <div class="col-lg-8">
        <div class="border p-4 mt-4">  
            <h2 class="text-primary text-center">Salle De Classe</h2>
            <hr class="bg-primary"/>
             <div class="form-group pt-3">                                  
                 <asp:TextBox ID="txtSallDeClasse" runat="server" Height="40" rows="2" placeholder="Entrez le nom d'une salle *" class="form-control" ></asp:TextBox>
             </div>

            <div class="form-group pt-4">                                  
                <asp:TextBox ID="txtNbres" runat="server" Height="40" rows="2" placeholder="Nombre de Personne*" class="form-control" ></asp:TextBox>
            </div>

            <div class="form-group pt-4">
                 <asp:TextBox ID="txtDescriptionSalle" runat="server" Height="40" rows="2" placeholder="Description" class="form-control" ></asp:TextBox>
            </div>

            <div class="form-group pt-4" style="text-align:center">
                <asp:Button ID="btnCancel" Height="40" runat="server" Width="200" Text="CANCEL" class="btn btn-info text-white" OnClick="btnCancel_Click"/>                               
                <asp:Button ID="btnSalleName" Height="40" runat="server" Width="200" Text="SAUVEGARDEZ" class="btn btn-success" OnClick="btnSalleName_Click"/>                               
            </div>
            <div class="form-group pt-4">
                <asp:Label runat="server" ID="lblMessage"></asp:Label>
            </div>
         </div>
     </div>
    <!--col 3-->
    <div class="col-lg-2">

    </div>
  </div>
</asp:Content>

