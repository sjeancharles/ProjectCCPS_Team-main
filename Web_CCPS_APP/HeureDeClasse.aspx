<%@ Page Title="" Language="C#" MasterPageFile="~/MaterPrincipal.Master" AutoEventWireup="true" CodeBehind="HeureDeClasse.aspx.cs" Inherits="Web_CCPS_APP.HeureDeClasse" %>

<asp:Content ID="Content2" class="head" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <div class="row">
    <!--col 1-->
    <div class="col-lg-2">

    </div>
    <!--col 2-->
    <div class="col-lg-8">
        <div class="border p-4 mt-4">  
            <h2 class="text-primary text-center">Heure de Classe</h2>
            <hr class="bg-primary"/>
             <div class="form-group pt-3">                                  
                <label>*Separez les heures avec un tirait (-)</label> 
             </div>

            <div class="form-group pt-4">                                  
                <asp:TextBox ID="txtHeure" runat="server" Height="40"  class="form-control" placeholder="2hPM-4hPM" MaxLength="40"></asp:TextBox>
            </div>

            <div class="form-group pt-4">
                <asp:DropDownList runat="server" CssClass="form-control" ID="DroClasseCat" />
            </div>

            <div class="form-group pt-4" style="text-align:center">
                <asp:Button ID="btnCancel" Height="40" runat="server" Width="200" Text="CANCEL" class="btn btn-info text-white" OnClick="btnCancel_Click"/>                               
                <asp:Button ID="AddHeure" runat="server" Height="40" Width="200" Text="SAUVEGARDEZ" class="btn btn-success" OnClick="AddHeure_Click"/>                            
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
