<%@ Page Title="" Language="C#" MasterPageFile="~/MaterPrincipal.Master" AutoEventWireup="true" CodeBehind="CreerUneSession.aspx.cs" Inherits="Web_CCPS_APP.CreerUneSession" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="border p-3 mt-4">
       <div class="row pb-2">
            <h2 class="text-primary text-center">Creer Une Nouvelle Session</h2>
            <hr />

           <div class="col-lg-2 pt-5">
                 <asp:Label id="OldDateDebut" Font-Bold="false" runat ="server"></asp:Label> 
                 <asp:Label id="OldDateFin" runat ="server"></asp:Label>  
              
           </div>

         <div class="col-lg-8 p-3"> 
           <div class="card">
             <div class="card-header text-primary text-center">
                 Parametre de séléction 
                 <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="true" Text="Editer" OnCheckedChanged="CheckBox1_CheckedChanged" />
             </div>

            <div class="mb-3 row p-3">
                <div class="input-group">
                     <asp:Label id="debuSessionCourante" runat ="server" class="input-group-text w-25"><i class="bi bi-calendar-plus"></i> &nbsp;&nbsp; Date Debut</asp:Label>             
                          <asp:TextBox ID="DateDebut" runat="server" class="form-control" placeholder="Entrer la Date de Naissance" TextMode="Date" ToolTip="mm/jj/aaaa"></asp:TextBox>
                     <span asp-validation-for="Ddn" class="alert-danger"></span>
                </div>
            </div>

            <div class="mb-3 row p-3">
                <div class="input-group">
                     <asp:Label id="FinSessionCourante" runat ="server" class="input-group-text w-25"><i class="bi bi-calendar-plus"></i> &nbsp;&nbsp; Date Fin</asp:Label>             
                     <asp:TextBox ID="DateFin" runat="server" class="form-control" TextMode="Date" ToolTip="mm/jj/aaaa"></asp:TextBox>
                     <span asp-validation-for="Ddn" class="alert-danger"></span>
                </div>
            </div>

            <div class="mb-3 row p-3">
                <div class="input-group"> 
                     <asp:Label runate="server" class="input-group-text w-25"><i class="bi bi-receipt"></i>&nbsp;&nbsp; Remarque</asp:Label>             
<%--                     <asp:textarea ID="SessionRemarque" runat="server" rows="2" class="form-control" ></asp:textarea>--%>
                     <textarea id="SessionRemarque" runat="server" cols="150" rows="3"></textarea>
                     <span asp-validation-for="Nom" class="alert-danger"></span>
                </div>
            </div>

            <div class="p-3">
                <asp:label id="lblMessage" runat="server" Text="" text-color="green" Font-Bold="True" Font-Size="Medium" ForeColor="red"></asp:label>
            </div>
               
         </div>   
        <div class="p-3 text-center">
            <asp:Button ID="btnCancel" runat="server" Width="30%" Text="CANCEL" class="btn btn-outline btn-info" OnClick="btncancel_Click"/>
            <asp:Button ID="btnAddSession" runat="server" Width="30%" Text="SAUVEGARDEZ" Class="btn btn-success" OnClick="btnAddSession_Click"/>
        </div> 
      </div> 

      <div class="col-lg-2"></div>

    </div>
  </div>
</asp:Content>
