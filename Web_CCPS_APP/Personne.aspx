<%@ Page Title="" Language="C#" MasterPageFile="~/MaterPrincipal.Master" AutoEventWireup="true" CodeBehind="Personne.aspx.cs" Inherits="Web_CCPS_APP.Personne" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <div class="row mx-5">
    <div class="border p-3 mt-4">
        <div class="row pb-2">
            <asp:TextBox ID="txtPersonneID" runat="server" Visible="False"></asp:TextBox>
            <h2 class="text-success text-uppercase text-center fw-bold">Enregistrer une personne</h2>
            <hr />
        
        <div class="col-6">
            <div class="mb-3 row">
                <div class="input-group mb-1">
                     <span for="Nom" class="input-group-text w-25"><i class="bi bi-file-person-fill"></i>&nbsp;&nbsp; Nom</span>             
                     <asp:TextBox runat="server" class="form-control" ID="txtNom" AutoPostBack="true" OnTextChanged="txtNom_TextChanged1"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="col-6">
             <div class="input-group mb-1">
                     <span for="Prenom" class="input-group-text w-25"><i class="bi bi-file-person-fill"></i> &nbsp;&nbsp; Prénom</span>             
                     <asp:TextBox runat="server" class="form-control" id="txtPrenom" AutoPostBack="true" OnTextChanged="txtPrenom_TextChanged1"></asp:TextBox>                    
                </div>
        </div>

        <div class="col-6">
            <div class="mb-3 row">
                <div class="input-group mb-1">
                     <span for="Ddn" class="input-group-text w-25"><i class="bi bi-calendar-plus"></i>&nbsp;&nbsp; DDN</span>             
                     <asp:TextBox ID="txtdate" runat="server" class="form-control" TextMode="date" Tooltip="Date de Naissance" ></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="col-6">
            <div class="mb-3 row">
                <div class="input-group mb-1">
                    <span for="Sexe" class="input-group-text w-25"><i class="bi bi-gender-ambiguous"></i>&nbsp;&nbsp; Sexe</span>             
                <asp:DropDownList ID="DrpSexe" class="form-control" runat="server">
                    <asp:ListItem Enabled="true" Text="Choisir Sexe" Value="-1"></asp:ListItem>
                    <asp:ListItem Text="M" Value="1"></asp:ListItem>
                    <asp:ListItem Text="F" Value="2"></asp:ListItem>
                </asp:DropDownList>
                </div>
            </div>

        </div>
        <div class="col-6">
             <div class="mb-3 row">
                <div class="input-group mb-1">
                     <span for="Telephone1" class="input-group-text w-25"><i class="bi bi-telephone"></i>&nbsp;&nbsp; Tel1</span>             
                     <asp:TextBox runat="server" class="form-control" id="txttelephone"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="col-6">
             <div class="mb-3 row">
                <div class="input-group mb-1">
                     <span for="Telephone2" class="input-group-text w-25"><i class="bi bi-telephone"></i>&nbsp;&nbsp; Tel Urgence </span>
                     <asp:TextBox runat="server" class="form-control" id="Txturgence"></asp:TextBox>            
                </div>
            </div>
        </div>
        <div class="col-6">
             <div class="mb-3 row">
                <div class="input-group mb-1">
                     <span for="Email" class="input-group-text w-25"><i class="bi bi-envelope"></i>&nbsp;&nbsp; Email</span>             
                     <asp:TextBox runat="server" class="form-control" id="txtemail"></asp:TextBox>
                </div>
            </div>

        </div>
        <div class="col-6">
            <div class="mb-3 row">
                <div class="input-group mb-1">
                     <span class="input-group-text w-25"><i class="bi bi-house"></i>&nbsp;&nbsp; # Maison</span> 
                     <asp:TextBox runat="server" class="form-control" id="txtnumeromaison"></asp:TextBox>
                    
                </div>
            </div>
        </div>
        <div class="col-6">
             <div class="mb-3 row">
                <div class="input-group mb-1">
                     <span class="input-group-text w-25"><i class="bi bi-house"></i>&nbsp;&nbsp; Adresse Rue</span> 
                     <asp:TextBox runat="server" class="form-control" id="txtrue"></asp:TextBox>                
                </div>
            </div>
        </div>
        <div class="col-6">
             <div class="mb-3 row">
                <div class="input-group mb-1">
                     <span class="input-group-text w-25"><i class="bi bi-house"></i>&nbsp;&nbsp; # Ville</span>             
                     <asp:TextBox runat="server" class="form-control" id="txtville"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="col-6">
            <div class="mb-3 row">
                <div class="input-group mb-1">
                     <span for="NumeroRecu" class="input-group-text w-25"><i class="bi bi-receipt"></i>&nbsp;&nbsp; # Reçu</span>             
                     <asp:TextBox runat="server" class="form-control" id="txtRecu"> </asp:TextBox>
                </div>
            </div>
        </div>
        <div class="col-6">
          <div class="mb-3 row">
            <div class="input-group mb-1">
                    <span for="Nom" class="input-group-text w-25"><i class="bi bi-file-person-fill"></i>&nbsp;&nbsp; Niveau Etude</span>             
                    <asp:TextBox runat="server" class="form-control" id="txtNivEtude"></asp:TextBox>
            </div>
        </div>
    </div>       
    <div class="col-6">           
        <div class="mb-3 row">
            <div class="input-group mb-1">
                 <span for="NumeroRecu" class="input-group-text w-25"><i class="bi bi-receipt"></i>&nbsp;&nbsp; Departement</span>             
                     <asp:DropDownList ID="DropDownDepartement" class="form-control" runat="server">
                        <asp:ListItem Enabled="true" Text="Choisir Departement" Value="-1"></asp:ListItem>
                            <asp:ListItem Text="Artibonite" Value="1"></asp:ListItem>                               
                            <asp:ListItem Text="Centre" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Grande-Anse" Value="3"></asp:ListItem>
                            <asp:ListItem Text="Nippes" Value="4"></asp:ListItem>
                            <asp:ListItem Text="Nord" Value="5"></asp:ListItem>
                            <asp:ListItem Text="Nord-Est" Value="6"></asp:ListItem>
                            <asp:ListItem Text="Nord-Ouest" Value="7"></asp:ListItem>
                            <asp:ListItem Text="Ouest" Value="8"></asp:ListItem>
                            <asp:ListItem Text="Sud" Value="9"></asp:ListItem>
                            <asp:ListItem Text="Sud-Est" Value="10">                                     
                            </asp:ListItem>                                      
                        </asp:DropDownList>              
            </div>
        </div>
    </div>
      <div class="col-6">
            <div class="mb-3 row">
                <div class="input-group mb-1">
                     <span class="input-group-text"><i class="bi bi-receipt"></i>&nbsp;&nbsp; Photo</span>
                     <input class="form-control" id="formFile" type="file">                    
                </div>
            </div>
        </div>
        <div class="col-6">
            <div class="mb-3 row">
                <div class="input-group mb-1">
                     <span class="input-group-text w-25"><i class="bi bi-receipt"></i>&nbsp;&nbsp; Remarque</span>             
                     <Textarea runat="server" class="form-control"  id="txtRemarque" cols="55" rows="3"></Textarea>
                </div>
            </div>
        </div>
    
        <div class="col-6"> 
           <div class="card text-center">
             <div class="card-header">
                 Statut De la Personne
             </div>
            <div class="card-body">
                <div class="custom-control custom-radio custom-control-inline">
                   <label class="px-2">| Etudiant  </label>
                  <asp:RadioButton type="radio" id="rdbStu" Checked="True" runat="server" GroupName="customRadioInline1" class="custom-control-input" OnCheckedChanged="RadioButton_CheckedChanged" AutoPostBack="true"/>

                  <label class="px-2">| Professeur </label>
                  <asp:RadioButton type="radio" id="rdbProf" runat="server" GroupName="customRadioInline1" class="custom-control-input" OnCheckedChanged="RadioButton_CheckedChanged" AutoPostBack="true"/>
                 

                  <label class="px-2">| Staff </label>
                  <asp:CheckBox ID="chbadStaff" runat="server" AutoPostBack="true" OnCheckedChanged="chbSatff_CheckedChanged"/>
                </div>               
            </div>
         </div>                       
        </div>            
         <div class="col-6">
           <div class="row">
            <div class="input-group">
                <asp:Label ID="lblMessage" runat="server" Text="" Font-Bold="True" Font-Size="Medium"></asp:Label>
<%--                <asp:Label ID="lblSucces" runat="server" Text="" text-color="green" Font-Bold="True" Font-Size="Medium" ForeColor="#CC0000"></asp:Label>--%>
                <asp:Label ID="LblCount" runat="server" Text="" text-color="green" Font-Bold="True" Font-Size="Medium" ForeColor="Green"></asp:Label>
            </div>
          </div>
        </div>
     </div>
   </div>
       <div class="p-2" style="text-align:center">
            <asp:button  runat="server" class="btn btn-success text-center m-2" Text="SAUVEGARDER" Width="15%" id="btnsauvegarder" OnClick="btnsauvegarder_Click1" />
            <asp:button  runat="server" class="btn btn-danger text-white m-2" Text="CANCEL" Width="15%" id="btnCancel" OnClick="btnCancel_Click" />
        </div> 
</div>
</asp:Content>
