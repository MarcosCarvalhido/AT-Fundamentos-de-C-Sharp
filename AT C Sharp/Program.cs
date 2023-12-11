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
                MostrarMenuInicial();
                int entrada = int.Parse(VerificarEntradaNumerica(Perguntar()));

                //Executa uma das ações escolida pelo usuario.
                eOpção opção = (eOpção)entrada;
                switch (opção)
                {
                    case eOpção.Sair:
                        {
                            SairDoPrograma = true;
                            break;
                        }
                    case eOpção.CriarConta:
                        {
                            ValidarNovaConta();
                            break;
                        }
                    case eOpção.AlterarConta:
                        {
                            ValidarAlteraçãoDeConta();
                            break;
                        }
                    case eOpção.ExcluirConta:
                        {
                            RemoverConta();
                            break;
                        }
                    case eOpção.MostrarRelatorio:
                        {
                            MostrarRelatorios();
                            break;
                        }
                    default:
                        {
                            ErroNumerico();
                            break;
                        }
                }
            }
            while (!SairDoPrograma);
            //Salva as alterações e fecha o programa.
            GerenciadorDeConta.Salvar();
            MensagemFinal();
        }
        // ----------------------------------------
        // MENUS DE OPÇÕES

        //Mostra o menu Principal com as opções disponiveis.
        public static void MostrarMenuInicial()
        {
            Console.Clear();
            Console.WriteLine("\nPESQUISA DE CONTAS");
            Console.WriteLine("Escolha uma das opções a seguir:");
            Console.WriteLine("[1] - Incluir uma conta nova.");
            Console.WriteLine("[2] - Alterar saldo de uma conta.");
            Console.WriteLine("[3] - Excluir uma conta.");
            Console.WriteLine("[4] - Gerar relatorios gerenciais.");
            Console.WriteLine("[0] - Sair e Salvar.");
        }
        //Mostra o menu de Relatorios com as opções disponiveis.
        public static void MostrarMenuRelatorios()
        {
            Console.Clear();
            Console.WriteLine("\nRELATORIO DE CONTAS");
            Console.WriteLine("Escolha uma das opções a seguir:");
            Console.WriteLine("[1] - Mostrar clientes com saldo negativos..");
            Console.WriteLine("[2] - Mostrar clientes com saldo expecifico.");
            Console.WriteLine("[3] - Listar Todas as contas.");
            Console.WriteLine("[0] - Voltar para o menu anterior.");
        }
        //Mostra o menu de alterações com as opções disponiveis.
        public static void MostrarMenuAlteração()
        {
            Console.Clear();
            Console.WriteLine("Escolha uma das opções a seguir:");
            Console.WriteLine("[1] - Creditar valor a conta.");
            Console.WriteLine("[2] - Debitar valor a conta.");
        }
        // ----------------------------------------
        //METODOS DE INTERAÇÃO COM O USUARIOS

        //Executa uma das opções de relatorio escolhidas pelo usuario.
        public static void MostrarRelatorios()
        {
            bool SairDoRealtorio = false;
            do
            {
                MostrarMenuRelatorios();

                eRelatorio opção = (eRelatorio)int.Parse(VerificarEntradaNumerica(Perguntar()));
                switch (opção)
                {
                    case eRelatorio.Sair:
                        {
                            SairDoRealtorio = true;
                            break;
                        }
                    case eRelatorio.ContasExpecificas:
                        {
                            ListarContasExpecificas();
                            break;
                        }
                    case eRelatorio.ContasNegativas:
                        {
                            ListarContasNegativas();
                            break;
                        }
                    case eRelatorio.ListarTodos:
                        {
                            ListarTodos();
                            break;
                        }
                    default:
                        {
                            ErroNumerico();
                            break;
                        }

                }
            }
            while (!SairDoRealtorio);
        }

        //Recebe os dados do usuario e valida se uma conta nova pode ser criada.
        public static void ValidarNovaConta()
        {
            int id = int.Parse(VerificarEntradaNumerica(Perguntar("Informe um novo ID para criar a nova conta: ")));
            if (GerenciadorDeConta.EncontrarConta(id) == null)
            {
                string nome = VerificarEntradaNomes(Perguntar("Informe o nome e sobrenome do titular da conta: "));
                if (VerificaNomeSobrenome(nome))
                {
                    decimal saldo = decimal.Parse(VerificarEntradaNumerica(Perguntar("Informe o saldo inicial da conta: ")));
                    if (saldo > 0)
                    {
                        CriarConta(id, nome, saldo);
                        ExibirConta(GerenciadorDeConta.EncontrarConta(id));
                        Console.WriteLine("Conta Criada com sucesso!");
                    }
                    else
                    {
                        Console.WriteLine("Uma conta precisa ter um saldo inicial maior do que zero!");
                    }
                }
                else
                {
                    Console.WriteLine("Uma conta precisa ter pelo menos um nome e um sobrenome!");
                }
            }
            else
            {
                Console.WriteLine($"Já existe uma conta com o id {id}!");
            }
            ConfirmarContinuar();
        }
        //Cria uma conta e adciona ao sistema.
        public static void CriarConta(int id, string nome, decimal saldo)
        {
            Conta conta = new()
            {
                ID = id,
                Nome = nome,
                Saldo = saldo
            };
            GerenciadorDeConta.CriarConta(conta);
        }

        public static void ValidarAlteraçãoDeConta()
        {
            int id = int.Parse(VerificarEntradaNumerica(Perguntar("Informe o ID da conta que deseja alteral:")));
            Conta conta = GerenciadorDeConta.EncontrarConta(id);
            if (conta != null)
            {
                MostrarMenuAlteração();
                eOperações operação = (eOperações)int.Parse(VerificarEntradaNumerica(Perguntar()));
                decimal valor = decimal.Parse(VerificarEntradaNumerica(Perguntar("Informe o valor que deseja alteral:")));
                if (valor >= 0)
                {
                    switch (operação)
                    {
                        case eOperações.Creditar:
                            {
                                AlterarConta(id, conta, operação, valor);
                                break;
                            }
                        case eOperações.Debitar:
                            {
                                AlterarConta(id, conta, operação, valor);
                                break;
                            }
                    }
                }
                else
                {
                    Console.WriteLine("O valor não pode ser menor do que zero!");
                }
            }
            else
            {
                Console.WriteLine($"Conta de ID {id} não foi encontrada");
            }
        }
        //Altera os dados cadastrais de uma conta.
        private static void AlterarConta(int id, Conta conta, eOperações operação, decimal valor)
        {
            GerenciadorDeConta.AlterarConta(conta, valor, operação);
            Console.WriteLine($"R$: {valor} alterados com sucesso na conta de ID: {id}! ");
            Console.WriteLine($"O saldo atual é de: R$ {conta.Saldo}");
            ConfirmarContinuar();
        }

        //Exclui uma conta determinada.
        public static void RemoverConta()
        {
            int id = int.Parse(VerificarEntradaNumerica(Perguntar("Informe o ID da conta que deseja excluir:")));
            switch (GerenciadorDeConta.RemoverConta(id))
            {
                case GerenciadorDeConta.Resposta.Excluida:
                    {
                        Console.WriteLine($"Conta de ID {id} excluida com sucesso!");
                        break;
                    }
                case GerenciadorDeConta.Resposta.PossuiSaldo:
                    {
                        Console.WriteLine($"Conta de ID {id} possui saldo postivo e não pode ser excluida");
                        break;
                    }
                case GerenciadorDeConta.Resposta.NãoEcontrado:
                    {
                        Console.WriteLine($"Conta de ID {id} não foi encontrada");
                        break;
                    }
            }
            ConfirmarContinuar();
        }
        //Mostra no console as contas com saldos negativos.
        private static void ListarContasNegativas()
        {
            bool contemNegativados = false;
            Console.WriteLine("Clientes Negativados:");
            foreach (var item in GerenciadorDeConta.ListarContasNegativas())
            {
                if (item.Saldo < 0)
                {
                    ExibirConta(item);
                    contemNegativados = true;
                }
            }
            if (!contemNegativados)
            {
                Console.WriteLine("Não existe nenhum cliente negativado!");
            }
            ConfirmarContinuar();
        }
        //Mostra no console os usuarios com saldo acima de um determinado valor.
        private static void ListarContasExpecificas()
        {
            bool contemMaior = false;
            int numero = int.Parse(VerificarEntradaNumerica(Perguntar("Insira um valor para filtrar as contas com saldo igual ou superior")));
            foreach (var item in GerenciadorDeConta.ListarContasExpecificas(numero))
            {
                ExibirConta(item);
                contemMaior = true;
            }
            if (!contemMaior)
            {
                Console.WriteLine("Não existe nenhum cliente maior que o valor procurado!");
            }
            ConfirmarContinuar();
        }
        //Lista no console todas as contas no sistema.
        private static void ListarTodos()
        {
            Console.WriteLine("Todas as Contas:");
            foreach (var item in GerenciadorDeConta.ListarTodasContas())
            {
                ExibirConta(item);
            }
            ConfirmarContinuar();
        }
        // ----------------------------------------
        //METODOS ULTILITARIOS E DE VALIDAÇÃO

        //Exibe as informações de uma conta.
        private static void ExibirConta(Conta item)
        {
            Console.WriteLine($"ID: {item.ID}, Nome: {item.Nome}, Saldo: R$:{item.Saldo}");
        }
        //Mostra a pergunta e recebe a entrada do usuario.
        public static string Perguntar(string pergunta = "")
        {
            string entrada;
            if (!string.IsNullOrEmpty(pergunta))
            {
                Console.WriteLine(pergunta);
            }
            entrada = Console.ReadLine();

            return entrada;
        }
        //Repete a pergunta se o numero não for valido.
        public static string VerificarEntradaNumerica(string entrada)
        {
            if (!VerificarSeNumero(entrada))
            {
                do
                {
                    ErroNumerico();
                    entrada = Perguntar();
                } while (!VerificarSeNumero(entrada));
            }
            Console.Clear();
            return entrada;
        }
        //Verifica se a entrada é numerica ou não nula.
        public static bool VerificarSeNumero(string entrada)
        {
            if (entrada != null)
            {
                try
                {
                    decimal numero = decimal.Parse(entrada);
                }
                catch (Exception)
                {
                    return false;
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        //Repete a pergunta se o nome não for valido.
        public static string VerificarEntradaNomes(string entrada)
        {
            if (!VerificarSeNome(entrada))
            {
                do
                {
                    ErroNome();
                    entrada = Perguntar();
                } while (!VerificarSeNome(entrada));
            }
            Console.Clear();    
            return entrada;
        }
        //Verifica se a entrada é numerica ou não nula.
        public static bool VerificarSeNome(string entrada)
        {
            if (entrada != null)
            {
                try
                {
                    decimal numero = decimal.Parse(entrada);
                }
                catch (Exception )
                {
                    return true;
                }
                return false;
            }
            else
            {
                return false;
            }
        }
        //Verifica se uma entrada de nome possui pelo menos um nome e sobrenome.
        public static bool VerificaNomeSobrenome(String nome)
        {
            String[] nomes = nome.Split(" ");
            if (nomes.Length > 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //Espera um input do usuario para continuar o programa.
        public static void ConfirmarContinuar()
        {
            Console.WriteLine("Digite qualquer tecla para continuar...");
            Console.ReadLine();
        }
        //Mostra uma mensagem de erro caso a entrada não seje um numero valido.
        public static void ErroNumerico()
        {
            Console.WriteLine("Apenas numeros são permitidos! Tente novamente.");
        }
        //Mostra uma mensagem no final do programa.
        public static void MensagemFinal()
        {
            Console.Clear();
            Console.WriteLine("Alterações salvas com sucesso!");
            Console.WriteLine("Você pode fechar esta janela.");
        }
        //Mostra uma mensagem de erro caso a entrada não seje um nome valido.
        public static void ErroNome()
        {
            Console.WriteLine("Apenas nomes  são permitidos!");
        }
        // ----------------------------------------
        //ENUMS DE OPÇÕES

        //Lista de opções para o menu principal.
        public enum eOpção
        {
            Sair = 0,
            CriarConta = 1,
            AlterarConta = 2,
            ExcluirConta = 3,
            MostrarRelatorio = 4,
        }
        //Lista de opções para o menu de relatorios.
        public enum eRelatorio
        {
            Sair = 0,
            ContasNegativas = 1,
            ContasExpecificas = 2,
            ListarTodos = 3,
        }
        //Lista de opções para alterar as contas.
        public enum eOperações
        {
            Creditar = 1,
            Debitar = 2,
        }
        //Lista de opções de tipos de contas.
        public enum eTipos
        {
            PessoaFisica = 1,
            PessoaJuridica = 2,
        }
    }
}
