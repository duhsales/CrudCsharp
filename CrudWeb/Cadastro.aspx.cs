using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Util;
using Banco;
using Regra;
using System.Drawing;

namespace CrudWeb
{


    public partial class Cadastro : System.Web.UI.Page
    {
        public AtualizaTela AtualizaTela { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Criar o diretório do banco de dados
                Diretorios.CriarDiretorios();

                //Cria o banco de dados
                CriarBanco Banco = new CriarBanco();
                Banco.Create();

                //Busca os registros no banco
                GetCadastros();
                GetCidades();
            }

            if (ucCadastro.Visible)
            {
                DelegateAtualizaTela();
            }
        }

        private void GetCadastros()
        {
            RegraCadastro Regra = new RegraCadastro();
            Regra.setPesquisa(txtPesquisa.Text);
            Regra.setPesquisaCidade(ddlCidade.Text);
            gvCadastro.DataSource = Regra.getCadastros();
            gvCadastro.DataBind();
        }

        private void GetCidades()
        {
            RegraCadastro Regra = new RegraCadastro();
            ddlCidade.DataSource = Regra.getCidades();
            ddlCidade.DataBind();
        }

        protected void btIncluir_Click(object sender, EventArgs e)
        {
            ucCadastro.Inicializa(string.Empty);
            ucCadastro.Visible = true;
            dvCadastro.Visible = true;
        }

        protected void btFechar_Click(object sender, EventArgs e)
        {
            ucCadastro.Visible = false;
            dvCadastro.Visible = false;
        }

        protected void bPesquisar_Click(object sender, EventArgs e)
        {
            GetCadastros();
        }

        private void DelegateAtualizaTela()
        {
            ucCadastro.AtualizaTela = delegate()
            {
                GetCidades();
                GetCadastros();
                ucCadastro.Visible = false;
                dvCadastro.Visible = false;
            };
        }

        protected void gvCadastro_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvCadastro, "Select$" + e.Row.RowIndex);
            }
        }

        protected void gvCadastro_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvCadastro.Rows)
            {
                if (row.RowIndex == gvCadastro.SelectedIndex)
                {
                    row.BackColor = ColorTranslator.FromHtml("#A1DCF2");
                }
                else
                {
                    row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                }
            }
        }

        protected void gvCadastro_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCadastro.PageIndex = e.NewPageIndex;
            GetCadastros();
        }

        protected void btAlterar_Click(object sender, EventArgs e)
        {
            if (gvCadastro.SelectedIndex > -1)
            {
                RegraCadastro Regra = new RegraCadastro(gvCadastro.SelectedRow.Cells[1].Text.ToInt());
                ucCadastro.Inicializa(Regra.getCodigo().ToString());
                ucCadastro.Visible = true;
                dvCadastro.Visible = true;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Util.Mensagem.Alerta, "alert('Selecione um cadastro!')", true);
            }
        }

        protected void btExcluir_Click(object sender, EventArgs e)
        {
            if (gvCadastro.SelectedIndex > -1)
            {
                RegraCadastro Regra = new RegraCadastro(gvCadastro.SelectedRow.Cells[1].Text.ToInt());
                Regra.Delete();
                GetCidades();
                GetCadastros();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Util.Mensagem.Alerta, "alert('Selecione um cadastro!')", true);
            }
        }
    }
}