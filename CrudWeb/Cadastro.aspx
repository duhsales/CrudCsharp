<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="CrudWeb.Cadastro" %>

<%@ Register Src="~/ucCadastro.ascx" TagName="ucCadastro" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="Styles/Cadastro.css" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="Scpt" runat="server" AsyncPostBackTimeout="0">
    </asp:ScriptManager>
    <div>
        <div class="fleft w46perc mright1perc">
            <asp:Label ID="lblPesquisar" Text="Pesquisar" runat="server" CssClass="font_padrao" />
            <asp:TextBox runat="server" ID="txtPesquisa" CssClass="w100perc font_padrao"></asp:TextBox>
        </div>
        <div class="fleft w46perc mright1perc">
            <asp:Label ID="Label1" Text="Cidade" runat="server" CssClass="font_padrao" />
            <asp:DropDownList ID="ddlCidade" CssClass="w100perc font_padrao" runat="server" DataTextField="CIDADE"
                DataValueField="CIDADE">
            </asp:DropDownList>
        </div>
        <div class="fleft">
            <asp:Button ID="bPesquisar" Text="Pesquisar" runat="server" 
                CssClass="font_padrao w100perc mtop15px" onclick="bPesquisar_Click" />
        </div>
    </div>
    <div>
        <asp:GridView ID="gvCadastro" runat="server" AutoGenerateColumns="False" Width="100%"
             ShowSelectButton="false" CssClass="w100perc" PageSize="3" AllowPaging="true"
            PagerSettings-Mode="NumericFirstLast" DataKeyNames="CODIGO"
            onpageindexchanging="gvCadastro_PageIndexChanging" 
            onrowdatabound="gvCadastro_RowDataBound" 
            onselectedindexchanged="gvCadastro_SelectedIndexChanged">
            <Columns>
            <asp:CommandField ShowSelectButton="True" SelectText="" ItemStyle-Width="0px" />
                <asp:BoundField HeaderText="Código" DataField="CODIGO" />
                <asp:BoundField HeaderText="Nome" DataField="NOME" />
                <asp:BoundField HeaderText="Cidade" DataField="CIDADE" />
                <asp:BoundField HeaderText="Telefone" DataField="TELEFONE" />
            </Columns>
            <RowStyle Font-Size="Smaller" />
            <HeaderStyle BackColor="#e8e8e8" Font-Size="Smaller" />
            <PagerStyle Font-Size="Smaller" />
        </asp:GridView>
    </div>
    <div>
        <div class="fright">
            <asp:Button ID="btExcluir" Text="Excluir" runat="server" 
                CssClass="font_padrao w100perc mtop15px" 
                OnClientClick="return confirm('Deseja excluír o cadastro?')" 
                onclick="btExcluir_Click" />
        </div>
        <div class="fright mright1perc">
            <asp:Button ID="btAlterar" Text="Alterar" runat="server" 
                CssClass="font_padrao w100perc mtop15px" onclick="btAlterar_Click" />
        </div>
        <div class="fright mright1perc">
            <asp:Button ID="btIncluir" Text="Incluir" runat="server" CssClass="font_padrao w100perc mtop15px"
                OnClick="btIncluir_Click" />
        </div>
    </div>
    <div class="w100perc fnone fundoTransp pabsolute" style="height: 100%!important; margin-top:-3%;" runat="server" id="dvCadastro"
        visible="false">
        <div class="w400px m0auto mtop2perc">
            <div class="w100perc mright5perc backgroundCinza h30px borderRadius">
                <asp:Label ID="lblTipoMotivo" runat="server" Text="Cadastro" CssClass="font_padrao" style="font-weight:bold;margin-left:2px;"></asp:Label>
                <span class="fright w5perc mtop1perc">
                    <asp:LinkButton ID="btFechar" runat="server" CssClass="closeButtom" 
                    onclick="btFechar_Click">X</asp:LinkButton></span>
            </div>
            <div class="fleft w100perc h300px overflowAuto ptop1perc backgroundBranco">
                <uc1:ucCadastro ID="ucCadastro" runat="server" Visible="false" />
            </div>
        </div>
    </div>
    </form>
</body>
</html>
