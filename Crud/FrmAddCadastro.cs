using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Regra;

namespace Crud
{
    public partial class FrmAddCadastro : Form
    {
        RegraCadastro Regra;
        public bool isUpdate { get; set; }
        public int iCodigo { get; set; }

        public FrmAddCadastro()
        {
            InitializeComponent();
        }

        private void btSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                Regra = isUpdate ? new RegraCadastro(iCodigo) : new RegraCadastro();
                Regra.setNome(txtNome.Text);
                Regra.setCidade(txtCidade.Text);
                txtTelefone.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                Regra.setTelefone(txtTelefone.Text);
                Regra.Save();

                if (isUpdate)
                {
                    MessageBox.Show("Cadastro alterado com sucesso!", Util.Mensagem.Alerta);
                }
                else
                {
                    MessageBox.Show("Cadastro salvo com sucesso!", Util.Mensagem.Alerta);
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Util.Mensagem.Erro);
            }
        }

        private void FrmAddCadastro_Load(object sender, EventArgs e)
        {
            setInfo();
            txtNome.Select();
        }

        private void setInfo()
        {
            if (isUpdate)
            {
                Regra = new RegraCadastro(iCodigo);
                txtNome.Text = Regra.getNome();
                txtCidade.Text = Regra.getCidade();
                txtTelefone.Text = Regra.getTelefone();
            }
        }
    }
}
