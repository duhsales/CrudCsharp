<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucCadastro.ascx.cs"
    Inherits="CrudWeb.ucCadastro" %>
<link rel="stylesheet" href="Styles/Cadastro.css" />
<div class="w100perc fleft">
    <asp:HiddenField ID="hdnFld" runat="server" />
    <div class="w32perc fleft mleft3perc mright3perc">
        <asp:Label ID="lblNome" runat="server" Text="Nome" CssClass="font_padrao"></asp:Label>
        <asp:TextBox ID="txtNome" runat="server" CssClass="w98perc fleft font_padrao" MaxLength="500"></asp:TextBox>
    </div>
    <div class="w32perc fleft mright3perc">
        <asp:Label ID="lblCidade" runat="server" Text="Cidade" CssClass="font_padrao"></asp:Label>
        <asp:TextBox ID="txtCidade" runat="server" CssClass="w98perc fleft font_padrao" MaxLength="100"></asp:TextBox>
    </div>
    <div class="w32perc fleft mright3perc">
        <asp:Label ID="lblTelefone" runat="server" Text="Telefone" CssClass="font_padrao"></asp:Label>
        <asp:TextBox ID="txtTelefone" runat="server" CssClass="w98perc fleft font_padrao" MaxLength="10"></asp:TextBox>
        
    </div>
    <div class="fright">
       <asp:Button ID="btSalvar" Text="Salvar" runat="server" 
            CssClass="font_padrao mtop15px" style="margin-right:5px" 
            onclick="btSalvar_Click" />
    </div>
</div>
