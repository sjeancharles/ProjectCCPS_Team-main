<%@ Page Title="" Language="C#" MasterPageFile="~/MaterPrincipal.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Web_CCPS_APP.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <h1 class="text-uppercase text-center fw-bold " style="color:seagreen; margin-top:4%" >annonces importantes</h1>
        <hr style="color:#10346b;" size="5" />
        <h3>
            <!-- Le literal affiche les annonces sur l'ecran-->
            <asp:Literal ID="Literal1" runat="server"></asp:Literal>
        </h3>
    </div>
</asp:Content>
