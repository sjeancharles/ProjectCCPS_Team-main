<%@ Page Title="" Language="C#" MasterPageFile="~/MaterPrincipal.Master" AutoEventWireup="true" CodeBehind="VoirEtudiantParClasseExcel.aspx.cs" Inherits="Web_CCPS_APP.VoirEtudiantParClasseExcel" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-lg-8 p-3">
        <asp:Button ID="btnVoirExcelPage" ImageUrl="Images/msexcel.png" runat="server" Text="Voir Dans une page Excel" Height="50px" Width="50%" CssClass="btn btn-primary" OnClick="btnVoirExcelPage_Click" />
        <asp:Label ID="lblError" runat ="server"></asp:Label>        
    </div>  
</asp:Content>
