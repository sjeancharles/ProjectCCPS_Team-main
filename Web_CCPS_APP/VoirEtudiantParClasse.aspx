<%@ Page Title="" Language="C#" MasterPageFile="~/MaterPrincipal.Master" AutoEventWireup="true" CodeBehind="VoirEtudiantParClasse.aspx.cs" Inherits="Web_CCPS_APP.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <div class="border p-3 mt-4">
       <div class="row pb-2">
            <h2 class="text-primary text-center text-capitalize ">Voir etudiant par classe</h2>
            <hr class="bg-primary"/>
         <div class="col-lg-6 p-3"> 
           <div class="card">
             <div class="card-header text-white bg-secondary text-center py-3 lead">
                 Parametre de selection 
             </div>
               <div class="card-body">
                  <asp:DropDownList runat="server"   ID="drowpListOption" OnSelectedIndexChanged="drowpListOption_SelectedIndexChanged" AutoPostBack="true" Width="100%" Height="37px" BackColor="#D2D2D2" Font-Bold="True" Font-Size="Medium" ForeColor="Black" ToolTip="Cliquez pour choisir une option"/>
              </div>
                <div class="card-body">
                   <asp:DropDownList runat="server"  ID="DropDownListClasse" OnSelectedIndexChanged="DropDownListClasse_SelectedIndexChanged" AutoPostBack="true" Width="100%" Height="37px" BackColor="#D2D2D2" Font-Bold="True" Font-Size="Medium" ForeColor="Black" ToolTip="Cliquez pour choisir une classe"/>
                </div>
               <div class="card-body">
                    <asp:DropDownList runat="server"  ID="DropDownListHoraire"  AutoPostBack="true" OnSelectedIndexChanged="DropDownListHoraire_SelectedIndexChanged" Width="100%" Height="37px" BackColor="#D2D2D2" Font-Bold="True" Font-Size="Medium" ForeColor="Black" ToolTip="Cliquez pour choisir une horaire"/>         
            </div>
         </div>
        </div> 
        <div class="col-lg-6 p-3"> 
           <div class="card">
             <div class="card-header text-white bg-secondary text-center">
                 <div class="row">
                 <div class="col-5 p-2 text-center lead">
                      Tous les Etudiants
                 </div> 
               
               
                </div>
             </div>
            <div class="card-body">
                <asp:listbox CssClass="lstCenter" runat="server" id="ListeEtudiants" width="430px" height="175px"></asp:listbox>
                <asp:label Width="300px" runat="server" text="" ID ="lblCount"  Font-Bold="True" Font-Size="Medium" ForeColor="GREEN"></asp:label>
            </div>
         </div>
        </div>
       <div class="text-center">        
           <asp:label runat="server" text="" id="lblError" ForeColor="Red"></asp:label>
        </div>
           <div>
              <asp:Label ID="lblMessage" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label> 
           </div>
       </div> 
  </div>
</asp:Content>
