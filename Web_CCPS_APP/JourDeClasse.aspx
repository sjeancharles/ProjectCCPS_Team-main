<%@ Page Title="" Language="C#" MasterPageFile="~/MaterPrincipal.Master" AutoEventWireup="true" CodeBehind="JourDeClasse.aspx.cs" Inherits="Web_CCPS_APP.JourDeClasse" %>
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
            <h2 class="text-primary text-center">Jours de Classe</h2>
            <hr class="bg-primary"/>
             <div class="form-group">
                 <label>*separez les jours avec un tirait (-)</label>
            </div>

            <div class="form-group pt-4">                                  
                  <asp:TextBox  runat="server"  class="form-control" ID="txtJour" placeholder = "Jour Ou Jour1-Jour2" MaxLength="40"> </asp:TextBox>
            </div>

            <div class="form-group pt-4">
                 <asp:TextBox ID="txtRemarque" runat="server" Height="40" rows="2" placeholder="Description" class="form-control" ></asp:TextBox>
            </div>

            <div class="form-group pt-4" style="text-align:center">
                <asp:Button ID="btnCancel" Height="40" runat="server" Width="200" Text="CANCEL" class="btn btn-info text-white" OnClick="btnCancel_Click"/>                               
                <asp:Button ID="AddJour" runat="server" Height="40" Width="200" Text="SAUVEGARDEZ" class="btn btn-success" OnClick="btnAddJour_Click"/>               
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

