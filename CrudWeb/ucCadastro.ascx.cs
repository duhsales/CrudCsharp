using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Regra;
using Util;

namespace CrudWeb
{
    public delegate void AtualizaTela();

    public partial class ucCadastro : System.Web.UI.UserControl
    {
        public AtualizaTela AtualizaTela { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void Inicializa(string strCodigo)
        {
            if (!string.IsNullOrEmpty(strCodigo))
            {
                hdnFld.Value = strCodigo;
                Popular();
            }
            else
            {
                LimpaCampo();
            }

            txtNome.Focus();
        }

        protected void btSalvar_Click(object sender, EventArgs e)
        {
            Salvar();
            AtualizaTela();
        }

        private void LimpaCampo()
        {
            txtNome.Text = string.Empty;
            txtCidade.Text = string.Empty;
            txtTelefone.Text = string.Empty;
        }

        private void Popular()
        {
            if (!string.IsNullOrEmpty(hdnFld.Value))
            {
                RegraCadastro Regra = new RegraCadastro(hdnFld.Value.ToInt());
                txtNome.Text = Regra.getNome();
                txtCidade.Text = Regra.getCidade();
                txtTelefone.Text = Regra.getTelefone();
            }
        }

        private void Salvar()
        {
            RegraCadastro Regra;

            if (string.IsNullOrEmpty(hdnFld.Value))
            {
                Regra = new RegraCadastro();
            }
            else
            {
                Regra = new RegraCadastro(hdnFld.Value.ToInt());
            }

            Regra.setNome(txtNome.Text);
            Regra.setCidade(txtCidade.Text);
            Regra.setTelefone(txtTelefone.Text);
            Regra.Save();
        }
    }
}