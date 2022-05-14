<%@ Page Title="" Language="C#" MasterPageFile="~/MaterPrincipal.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="Web_CCPS_APP.Dashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
    <h1 class="text-primary display-4 text-center mt-3">CALVARY CHRISTIAN PROFESSIONAL SCHOOL (CCPS)</h1>
    <p class="display-6 text-center"> Un ministère de Calvary Chapel </p>
    <hr class="mt-1"/>
</div>

<div class="container-fluid">
    <div class="row mb-3">
        <div class="col-md-4 ">
            <a Class="btn btn-primary btn-lg form-control" runat="server"><i class="bi bi-plus-circle"></i> INSCRIRE ETUDIANTS </a>                                                       
        </div>

        <div class="col-md-4 ">
            <a  Class="btn btn-primary btn-lg form-control" asp-area="" asp-controller="Personne" asp-action="Index" >  LISTE DES ETUDIANTS </a>                                                       
        </div>

        <div class="col-md-4 ">
            <a  Class="btn btn-primary btn-lg form-control" >  ETUDIANTS PAR CLASSE(EXCEL) </a>                                                       
        </div>
    </div>

    <div class="row mb-3">
        <div class="col-md-4 ">
            <a  Class="btn btn-primary btn-lg form-control" >  MODIFIER HORAIRE </a>                                                       
        </div>

        <div class="col-md-4 ">
            <a  Class="btn btn-primary btn-lg form-control" >  AJOUTER CLASSE </a>                                                       
        </div>

        <div class="col-md-4 ">
            <a  Class="btn btn-primary btn-lg form-control" >  AJOUTER ETUDIANTS DANS CLASSE </a>                                                       
        </div>
    </div>

    <div class="row mb-3">
        <div class="col-md-4 ">
            <a  Class="btn btn-primary btn-lg form-control" >  VOIR ETUDIANTS PAR CLASSE </a>                                                       
        </div>

        <div class="col-md-4 ">
            <a Class="btn btn-primary btn-lg form-control" >  ENLEVER ETUDIANTS DANS CLASSE </a>                                                       
        </div>

        <div class="col-md-4 ">
            <a Class="btn btn-primary btn-lg form-control" asp-area="" asp-controller="EtudiantGradue" asp-action="Index"> ETUDIANTS DEJA GRADUES </a>                                                       
        </div>
    </div>

    <div class="row mb-3">
        <div class="col-md-4 ">
        </div>

        <div class="col-md-4 ">
            <a Class="btn btn-outline-primary btn-lg form-control" >  SETUP POUR ADMINISTRATEUR </a>                                                       
        </div>

        <div class="col-md-4 ">
        </div>
    </div>
</div>
</asp:Content>
