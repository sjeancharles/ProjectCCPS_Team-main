<%@ Page Title="" Language="C#" MasterPageFile="~/MaterPrincipal.Master" AutoEventWireup="true" CodeBehind="EnleverEtudiant.aspx.cs" Inherits="Web_CCPS_APP.EnleverEtudiant" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <div class="border p-3 mt-4">
       <div class="row pb-2">
            <h2 class="text-primary text-center text-capitalize ">Enlever Un Etudiant Dans Une Classe</h2>
            <hr class="bg-primary"/>
         <div class="col-lg-6 p-3"> 
           <div class="card">
             <div class="card-header bg-secondary text-white text-center py-3 lead">
                 Parametre de séléction 
             </div>
               <div class="card-body">
                    <asp:dropdownlist runat="server" ID="drowpListOption" AutoPostBack="True" OnSelectedIndexChanged="drowpListOption_SelectedIndexChanged"  Width="100%" Height="37px" BackColor="#D2D2D2" Font-Bold="True" Font-Size="Medium" ForeColor="Black" ToolTip="Cliquez pour choisir une option"></asp:dropdownlist> 
              </div>
                <div class="card-body">
                     <asp:DropDownList ID="DropDownListClasse"  runat="server"  AutoPostBack="True" OnSelectedIndexChanged="DropDownListClasse_SelectedIndexChanged" Width="100%" Height="37px" BackColor="#D2D2D2" Font-Bold="True" Font-Size="Medium" ForeColor="Black" ToolTip="Cliquez pour choisir une classe"></asp:DropDownList>                                                            
                </div>
               <div class="card-body">
                    <asp:DropDownList ID="DropDownListHoraire"  runat="server"  AutoPostBack="True" OnSelectedIndexChanged="DropDownListHoraire_SelectedIndexChanged" Width="100%" Height="37px" BackColor="#D2D2D2" Font-Bold="True" Font-Size="Medium" ForeColor="Black" ToolTip="Cliquez pour choisir une horaire"></asp:DropDownList>                                                            
            </div>
         </div>
        </div> 
        <div class="col-lg-6 p-3"> 
           <div class="card">
             <div class="card-header bg-secondary text-white text-center">
                 <div class="row">
                 <div class="col-5 p-2 text-center lead">
                      Choisir un Etudiant
                 </div> 
               
               <div class="col-7 text-end">
                  
                </div>
                </div>
             </div>
            <div class="card-body">
                <asp:listbox runat="server" id="ListeEtudiants" height="175px" width="400px"></asp:listbox>
            </div>
         </div>
        </div>
       <div class="text-center">
             <asp:Button ID="btnCancel" runat="server" Width="200" Text="CANCEL" class="btn btn-info text-white" Height="40px" OnClick="btnCancel_Click"/> 
             <asp:Button ID="btnEnlever" runat="server" Width="200" Text="ENLEVER L'ETUDIANT"  OnClick="btnEnlever_Click" class="btn btn-success" Height="40px"/> 
        </div>
           <div>
              <asp:Label ID="lblMessage" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label> 
           </div>
       </div> 
  </div>
</asp:Content>