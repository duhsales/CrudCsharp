using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Regra;
using Util;
using Banco;

namespace Crud
{
    public partial class FrmCadastro : Form
    {
        RegraCadastro Regra;

        public FrmCadastro()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
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

        private void btIncluir_Click(object sender, EventArgs e)
        {
            FrmAddCadastro frm = new FrmAddCadastro();
            frm.isUpdate = false;
            frm.ShowDialog();
            GetCadastros();
            GetCidades();
        }

        private void btAlterar_Click(object sender, EventArgs e)
        {
            if (dgvCadastro.SelectedRows.Count > 0)
            {
                FrmAddCadastro frm = new FrmAddCadastro();
                frm.isUpdate = true;
                int iCodigo = dgvCadastro.Rows[dgvCadastro.SelectedRows[0].Index].Cells[0].Value.ToInt();
                frm.iCodigo = iCodigo;
                frm.ShowDialog();
                GetCadastros();
                GetCidades();
            }
            else
            {
                MessageBox.Show("Cadastro não selecionado!", Util.Mensagem.Alerta);
            }
        }

        private void GetCadastros()
        {
            Regra = new RegraCadastro();
            Regra.setPesquisa(txtPesquisa.Text);
            Regra.setPesquisaCidade(cbCidade.Text);
            dgvCadastro.DataSource = Regra.getCadastros();
        }

        private void GetCidades()
        {
            Regra = new RegraCadastro();
            cbCidade.DataSource = Regra.getCidades();
        }

        private void btExcluir_Click(object sender, EventArgs e)
        {
            if (dgvCadastro.SelectedRows.Count > 0)
            {
                try
                {
                    DialogResult dr = MessageBox.Show("Deseja excluir o cadastro?", Util.Mensagem.Alerta, MessageBoxButtons.YesNo,MessageBoxIcon.Information);

                    if (dr == DialogResult.Yes)
                    {
                        int iCodigo = dgvCadastro.Rows[dgvCadastro.SelectedRows[0].Index].Cells[0].Value.ToInt();

                        Regra = new RegraCadastro(iCodigo);
                        Regra.Delete();
                        MessageBox.Show("Cadastro excluído com sucesso!", Util.Mensagem.Alerta);
                        GetCadastros();
                        GetCidades();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Util.Mensagem.Erro);
                }
            }
            else
            {
                MessageBox.Show("Cadastro não selecionado!", Util.Mensagem.Alerta);
            }
        }

        private void btPesquisa_Click(object sender, EventArgs e)
        {
            GetCadastros();
        }
    }
}
