using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class frmPrincipal : Form
    {
        List<string> vCadastro = new List<string>();
        
        
        public frmPrincipal()
        {
            InitializeComponent();

            vCadastro.Add("USUARIO:admin");
            vCadastro.Add("DATA:123");
            vCadastro.Add("EMAIL:admin@admin.com");
            vCadastro.Add("USUARIO:admin2");
            vCadastro.Add("DATA:1232");
            vCadastro.Add("EMAIL:admin@admin.com2");
            vCadastro.Add("USUARIO:rafael");
            vCadastro.Add("DATA:02/07/1991");
            vCadastro.Add("EMAIL:rafael@rafael.com");

            lblMensagem.Text = string.Empty;


        }

        
        

        /// 
        /// Sair do Programa
        /// 
        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }//private void btnSair_Click(object sender, EventArgs e)

        /// 
        /// botao que faz o cadastro do cliente caso ele nao tenha e faz a checagem se ele ja e cadastrado
        /// Sugeriri um novo usuario 
        private void btnCadastrar_Click(object sender, EventArgs e)
        {

            if (ChecarUsuarioJaCadastrado(txtUsuario.Text))
            {
                string aux;
                List<string> lista = new List<string>();
                int contador = 0;

                lblMensagem.ForeColor = System.Drawing.Color.Red;
                lblMensagem.Text = "Esse usuario ja foi cadastrado \n usuarios disponiveis";

                while (contador < 3)
                {
                    aux = SugestaoNomeUsuario();

                    if (lista.Contains(aux))
                    {
                        continue;
                    }// if (lista.Contains(aux))
                    lista.Add(aux);
                    contador++;
                }//while (contador < 3)

                for (int i = 0; i < 3; i++)
                {
                    lblMensagem.Text = lblMensagem.Text + "\n"+lista[i];
                }//for (int i = 0; i < 3; i++)


            }
            else
            {
                vCadastro.Add("USUARIO:"+txtUsuario.Text);
                vCadastro.Add("DATA:"+txtNascimento.Text);
                vCadastro.Add("EMAIL:"+txtEmail.Text);
            }//if (ChecarUsuarioJaCadastrado(txtUsuario.Text))


        }//private void btnCadastrar_Click(object sender, EventArgs e)


        /// 
        /// botao de login , ele ira checar se todas as info do usuario esta correta
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (Login(txtUsuario.Text,txtNascimento.Text,txtEmail.Text))
            {
                lblMensagem.ForeColor = System.Drawing.Color.Green;
                lblMensagem.Text = "vc esta logado";
            }
        }//private void btnLogin_Click(object sender, EventArgs e)



        /// verifica se o usuario ja esta cadastrado para ser feito o login
        /// <param name="usuario">nome do usuario</param>
        /// <param name="data">data de nascimento</param>
        /// <param name="email">email do usuario</param>
        /// <returns>true / false</returns>
        public bool Login(string usuario, string data, string email)
        {
            for (int i = 0; i < vCadastro.Count; i++)
            {
                if ((vCadastro[i] == "USUARIO:" + usuario) && (vCadastro[i+1] == "DATA:" + data) && (vCadastro[i+2] == "EMAIL:"+email))
                {
                    return true;
                }

            }
            return false;
        }//public bool Login(string usuario, string data, string email)

        ///Verifica se o usuario ja existe
        /// <param name="usuario">nome do usuario que sera cadastrado</param>
        /// <returns>true / false</returns>
        public bool ChecarUsuarioJaCadastrado(string usuario)
        {
            if (vCadastro.Contains("USUARIO:" + usuario))
            {
                return true;
            }

            return false;

        }//public bool ChecarUsuarioJaCadastrado(string usuario)


        /// 
        /// Ira montar um nome usando o nome de usuario 
        /// junto do email e com um numero aleatorio entre 100 a 999
        /// ira ter um randon onde ele ira montar aleatoriamentede 0 a 3
        /// 0 = nome usuario + email + numero aletorio 100 a 999
        /// 1 = nome usuario + data 4 primeiros digitos= email
        /// 2 = nome do usuario = email = ano de nascimento
        /// 3 = nome usuario + email 3 primeiros digitos + numero de 100 a 999
        public string SugestaoNomeUsuario()
        {
            Random rnd = new Random();
            var tipoSugestao = rnd.Next(0, 3);

            if (tipoSugestao == 0)
            {
                return txtUsuario.Text + desmenbrarEmail(txtEmail.Text) + rnd.Next(100, 999);
            }
            if (tipoSugestao == 1)
            {

                return txtUsuario.Text + txtNascimento.Text.Replace("/", "").Substring(0, 4)+ desmenbrarEmail(txtEmail.Text);

            }
            if (tipoSugestao == 2)
            {
                string data = txtNascimento.Text.Replace("/", "");
                data = data.Replace(data.Substring(0, 4), "");
                return txtUsuario.Text + desmenbrarEmail(txtEmail.Text).Substring(0,3)+data;
            }
            if (tipoSugestao == 3)
            {
                return txtUsuario.Text + desmenbrarEmail(txtEmail.Text).Substring(0, 3) + rnd.Next(100, 999);
            }




            return txtUsuario.Text + desmenbrarEmail(txtEmail.Text) + rnd.Next(100,999);
        }//public string SugestaoNomeUsuario()


        /// <summary>
        /// Desmenbra o email para fazer o join depois na função SugestaoNomeUsuario()
        /// </summary>
        /// <param name="email">a variavel onde ira receber o email</param>
        /// <returns>string</returns>
        public string desmenbrarEmail(string email)
        { 
            int aux = 0;

            //controle para ver se esta Ok com o email caso contrario retorna ""
            if ((email == "") || (email == null) || (email.Contains(" ")))
            {
                return "";
            }
            else
            {
                aux = email.IndexOf("@");
                email = email.Substring(0, aux);
            }

            if (email.Contains("."))
            {
                aux = email.IndexOf(".");
                return email.Substring(0, aux);
            }

            if (email.Contains("-"))
            {
                aux = email.IndexOf(".");
                return email.Substring(0, aux);
            }

            if (email.Contains("_"))
            {
                aux = email.IndexOf(".");
                return email.Substring(0, aux);
            }

            if (email.Length >= 5)
            {
                return email.Substring(0, 5);
            }
            else
            {
                return email.Substring(0);
            }


            return "";




        }//public string desmenbrarEmail(string email)










    }//public partial class frmPrincipal : Form
}//namespace WindowsFormsApp2
