using System.Text.RegularExpressions;

namespace AT_C_Sharp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool SairDoPrograma = false;
            //le o arquivo e carrega os dados.
            GerenciadorDeConta.InicializarContas();
            do
            {
                //Mostra o menu e recebe a escolha do usuario.
                Menus.MostrarMenuInicial();
                int entrada = int.Parse(Validações.VerificarEntradaNumerica(Interações.Perguntar()));

                //Executa uma das ações escolida pelo usuario.
                Enums.eOpção opção = (Enums.eOpção)entrada;
                switch (opção)
                {
                    case Enums.eOpção.Sair:
                        {
                            SairDoPrograma = true;
                            break;
                        }
                    case Enums.eOpção.CriarConta:
                        {
                            Interações.ValidarNovaConta();
                            break;
                        }
                    case Enums.eOpção.AlterarConta:
                        {
                            Interações.ValidarAlteraçãoDeConta();
                            break;
                        }
                    case Enums.eOpção.ExcluirConta:
                        {
                            Interações.RemoverConta();
                            break;
                        }
                    case Enums.eOpção.MostrarRelatorio:
                        {
                            Interações.MostrarRelatorios();
                            break;
                        }
                    default:
                        {
                            Validações.ErroNumerico();
                            break;
                        }
                }
            }
            while (!SairDoPrograma);
            //Salva as alterações e fecha o programa.
            GerenciadorDeConta.Salvar();
            Interações.MensagemFinal();
        }
    }
}
