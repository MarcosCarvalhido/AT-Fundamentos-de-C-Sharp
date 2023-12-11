namespace AT_C_Sharp
{
    public static class Interações
    {
        //METODOS DE INTERAÇÃO COM O USUARIOS

        //Executa uma das opções de relatorio escolhidas pelo usuario.
        public static void MostrarRelatorios()
        {
            bool SairDoRealtorio = false;
            do
            {
                Menus.MostrarMenuRelatorios();

                Enums.eRelatorio opção = (Enums.eRelatorio)int.Parse(Validações.VerificarEntradaNumerica(Perguntar()));
                switch (opção)
                {
                    case Enums.eRelatorio.Sair:
                        {
                            SairDoRealtorio = true;
                            break;
                        }
                    case Enums.eRelatorio.ContasExpecificas:
                        {
                            ListarContasExpecificas();
                            break;
                        }
                    case Enums.eRelatorio.ContasNegativas:
                        {
                            ListarContasNegativas();
                            break;
                        }
                    case Enums.eRelatorio.ListarTodos:
                        {
                            ListarTodos();
                            break;
                        }
                    default:
                        {
                            Validações.ErroNumerico();
                            break;
                        }

                }
            }
            while (!SairDoRealtorio);
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
        //Recebe os dados do usuario e valida se uma conta nova pode ser criada.
        public static void ValidarNovaConta()
        {
            int id = int.Parse(Validações.VerificarEntradaNumerica(Perguntar("Informe um novo ID para criar a nova conta: ")));
            if (GerenciadorDeConta.EncontrarConta(id) == null)
            {
                Menus.MostrarMenuTiposDeConta();
                int tipo = int.Parse(Validações.VerificarEntradaNumerica(Perguntar()));
                if (tipo == 1)
                {
                    CriarContaPF(id);
                }
                else if (tipo == 2)
                {
                    CriarContaPJ(id);
                }
                else
                {
                    Console.WriteLine("Opção invalida!");
                }
            }
            else
            {
                Console.WriteLine($"Já existe uma conta com o id {id}!");
            }
            ConfirmarContinuar();
        }
        //Recolhe as informações para criar uma conta de tipo Pessoa fisica.
        public static void CriarContaPF(int id)
        {
            string nome = Validações.VerificarEntradaNomes(Perguntar("Informe o nome e sobrenome do titular da conta: "));
            if (Validações.VerificaNomeSobrenome(nome))
            {
                string cpf = Validações.VerificarEntradaNomes(Perguntar("Informe o CPF do titular da conta, ultilize o formato: (111.111.111-11). "));
                if (Validações.ValidarCPF(cpf))
                {
                    decimal saldo = decimal.Parse(Validações.VerificarEntradaNumerica(Perguntar("Informe o saldo inicial da conta: ")));
                    if (saldo > 0)
                    {
                        GerarContaPF(cpf, id, nome, saldo);
                        ExibirConta(GerenciadorDeConta.EncontrarConta(id));
                        Console.WriteLine("Conta pessoa fisica criada com sucesso!");
                    }
                    else
                    {
                        Console.WriteLine("Uma conta precisa ter um saldo inicial maior do que zero!");
                    }
                }
                else
                {
                    Console.WriteLine("CPF invalido! Ultilize o formato: (111.111.111-11).");
                }
            }
            else
            {
                Console.WriteLine("Uma conta precisa ter pelo menos um nome e um sobrenome!");
            }
        }
        //Recolhe as informações para criar uma conta de tipo Pessoa Juridica.
        public static void CriarContaPJ(int id)
        {
            string nome = Validações.VerificarEntradaNomes(Perguntar("Informe o nome da empresa titular da conta: "));
            string cnpj = Validações.VerificarEntradaNomes(Perguntar("Informe o CNPJ do titular da conta, ultilize o formato: (11.111.111/1111-11). "));
            if (Validações.ValidarCNPJ(cnpj))
            {
                decimal saldo = decimal.Parse(Validações.VerificarEntradaNumerica(Perguntar("Informe o saldo inicial da conta: ")));
                if (saldo > 0)
                {
                    GerarContaPJ(cnpj, id, nome, saldo);
                    ExibirConta(GerenciadorDeConta.EncontrarConta(id));
                    Console.WriteLine("Conta pessoa Juridica criada com sucesso!");
                }
                else
                {
                    Console.WriteLine("Uma conta precisa ter um saldo inicial maior do que zero!");
                }
            }
            else
            {
                Console.WriteLine("CNPJ invalido! ultilize o formato: (11.111.111/1111-11).");
            }
        }
        //Cria uma conta pessoa fisica e adciona ao sistema.
        public static void GerarContaPF(string cpf, int id, string nome, decimal saldo)
        {
            PessoaFisica conta = new()
            {
                ID = id,
                Tipo = "pf",
                Nome = nome,
                CPF = cpf,
                Saldo = saldo
            };
            GerenciadorDeConta.AdcionarConta(conta);
        }
        //Cria uma conta pessoa juridica e adciona ao sistema.
        public static void GerarContaPJ(string cnpj, int id, string nome, decimal saldo)
        {
            PessoaJuridica conta = new()
            {
                ID = id,
                Tipo = "pj",
                Nome = nome,
                CNPJ = cnpj,
                Saldo = saldo
            };
            GerenciadorDeConta.AdcionarConta(conta);
        }

        public static void ValidarAlteraçãoDeConta()
        {
            int id = int.Parse(Validações.VerificarEntradaNumerica(Perguntar("Informe o ID da conta que deseja alteral:")));
            Conta conta = GerenciadorDeConta.EncontrarConta(id);
            if (conta != null)
            {
                Menus.MostrarMenuAlteração();
                Enums.eOperações operação = (Enums.eOperações)int.Parse(Validações.VerificarEntradaNumerica(Perguntar()));
                decimal valor = decimal.Parse(Validações.VerificarEntradaNumerica(Perguntar("Informe o valor que deseja alteral:")));
                if (valor >= 0)
                {
                    switch (operação)
                    {
                        case Enums.eOperações.Creditar:
                            {
                                AlterarConta(id, conta, operação, valor);
                                break;
                            }
                        case Enums.eOperações.Debitar:
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
        public static void AlterarConta(int id, Conta conta, Enums.eOperações operação, decimal valor)
        {
            GerenciadorDeConta.AlterarConta(conta, valor, operação);
            Console.WriteLine($"R$: {valor} alterados com sucesso na conta de ID: {id}! ");
            Console.WriteLine($"O saldo atual é de: R$ {conta.Saldo}");
            ConfirmarContinuar();
        }
        //Exclui uma conta determinada.
        public static void RemoverConta()
        {
            int id = int.Parse(Validações.VerificarEntradaNumerica(Perguntar("Informe o ID da conta que deseja excluir:")));
            Conta conta = GerenciadorDeConta.EncontrarConta(id);
            if (conta != null)
            {
                if (conta.Saldo <= 0)
                {
                    GerenciadorDeConta.RemoverConta(id);
                    Console.WriteLine($"Conta de ID {id} excluida com sucesso!");
                }
                else if (ConfirmarExclusão(id))
                {
                    GerenciadorDeConta.RemoverConta(id);
                    Console.WriteLine($"Conta de ID {id} excluida com sucesso!");
                }
            }
            else
            {
                Console.WriteLine($"Conta de ID {id} não foi encontrada");
            }
            ConfirmarContinuar();
        }
        //Verifica a exclusão de uma conta com saldo positivo.
        private static bool ConfirmarExclusão(int id)
        {
            Console.WriteLine($"Conta de ID {id} possui saldo postivo, tem certeza que deseja remove-la?");
            Console.WriteLine("[1] - Remover mesmo assim");
            Console.WriteLine("[0] - Cancelar");
            int entrada = int.Parse(Validações.VerificarEntradaNumerica(Perguntar()));
            if (entrada > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
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
            int numero = int.Parse(Validações.VerificarEntradaNumerica(Perguntar("Insira um valor para filtrar as contas com saldo igual ou superior")));
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
        //Exibe as informações de uma conta.
        public static void ExibirConta(Conta item)
        {
            if (item.Tipo == "pf")
            //{
            {
                Console.WriteLine($"ID: {item.ID}, Nome: {item.Nome}, Tipo: {item.Tipo}, CPF: {((PessoaFisica)item).CPF}, Saldo: R$:{item.Saldo}");
            }
            else
            {
                Console.WriteLine($"ID: {item.ID}, Nome: {item.Nome}, Tipo: {item.Tipo}, CNPJ: {((PessoaJuridica)item).CNPJ}, Saldo: R$:{item.Saldo}");
            }
        }
            //Mostra uma mensagem no final do programa.
        public static void MensagemFinal()
        {
            Console.Clear();
            Console.WriteLine("Alterações salvas com sucesso!");
            Console.WriteLine("Você pode fechar esta janela.");
        }
        //Espera um input do usuario para continuar o programa.
        public static void ConfirmarContinuar()
        {
            Console.WriteLine("Digite qualquer tecla para continuar...");
            Console.ReadLine();
        }
    }
}
