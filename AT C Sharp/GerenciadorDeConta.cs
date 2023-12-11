using System.Text;
using static AT_C_Sharp.Program;

namespace AT_C_Sharp
{
    internal static class GerenciadorDeConta
    {
        //Lista de contas carregadas no sistema
        public static Dictionary<int, Conta> Contas { get; set; }
        //Le um arquivo CSV e adciona a lista de contas.
        internal static void InicializarContas()
        {
            string CaminhoarquivoCSV = Directory.GetParent(Environment.ProcessPath).Parent.Parent.Parent.ToString() +"\\Contas.csv";
            Contas = new Dictionary<int, Conta>();

            using (FileStream fs = new FileStream(CaminhoarquivoCSV, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                var reader = new StreamReader(fs, Encoding.Default, true);
                string line;
                while ((reader.Peek() != -1))
                {
                    line = reader.ReadLine();
                    if (!string.IsNullOrEmpty(line))
                    {
                        string[] dados = line.Split(";");
                        if(dados[1] == "pf")
                        {
                            CriarContaPF(dados);
                        }
                        else
                        {
                            CriarContaPj(dados);
                        }
                    }
                }
            }
        }
        //Cria uma conta de tipo pessoa juridica.
        private static void CriarContaPj(string[] dados)
        {
            PessoaJuridica conta = new()
            {
                ID = int.Parse(dados[0]),
                Tipo = dados[1],
                Nome = dados[2],
                CNPJ = dados[3],
                Saldo = decimal.Parse(dados[4])
            };
            Contas.Add(conta.ID, conta);
        }
        //Cria uma conta de tipo pessoa fisica
        private static void CriarContaPF(string[] dados)
        {
            PessoaFisica conta = new()
            {
                ID = int.Parse(dados[0]),
                Tipo = dados[1],
                Nome = dados[2],
                CPF = dados[3],
                Saldo = decimal.Parse(dados[4])
            };
            Contas.Add(conta.ID, conta);
        }

        //Procura uma conta apartir de um ID. Retorna nulo se não achar nenhuma correspondencia.
        public static Conta EncontrarConta(int id)
        {
            if (Contas.ContainsKey(id))
            {
                return Contas[id];
            }
            else
            {
                return null;
            }
        }
        //Adciona uma nova conta a lista de contas.
        public static void AdcionarConta(Conta conta)
        {
            Contas.Add(conta.ID, conta);
        }
        //Altera o valor da conta apartir de um valor informado.
        public static void AlterarConta(Conta conta, decimal valor, eOperações operação)
        {
            switch (operação)
            {
                case eOperações.Creditar:
                    {
                        conta.Saldo += valor;
                        break;
                    }
                case eOperações.Debitar:
                    {
                        conta.Saldo -= valor;
                        break;
                    }
            }
        }
        //Remove uma conta apartir de um ID.
        public static void RemoverConta(int id)
        {
            Contas.Remove(id);
        }
        //Lista todas as contas que possuem um saldo maior que uma quantidade determinada. Ordena resultados de forma crescente.
        public static List<Conta> ListarContasNegativas()
        {
            List<Conta> lista = (from conta in Contas where conta.Value.Saldo < 0 orderby conta.Value.Saldo select conta.Value).ToList();
            return lista;
        }
        //Lista todas as contas que possuem saldo acima de uma quantia determinada. Ordena resultados de forma crescente.
        public static List<Conta> ListarContasExpecificas(int numero)
        {
            List<Conta> lista = (from conta in Contas where conta.Value.Saldo >= numero orderby conta.Value.Saldo  select conta.Value).ToList();
            return lista;
        }
        //Lista todas as contas adcionadas no sistema em ordem de ID. Ordena resultados de forma crescente.
        public static List<Conta> ListarTodasContas()
        {
            List<Conta> lista = (from conta in Contas orderby conta.Value.ID select conta.Value).ToList();
            return lista;
        }

        //Salva as alterações em um arquivo CSV.
        internal static void Salvar()
        {
            string CaminhoarquivoCSV = Directory.GetParent(Environment.ProcessPath).Parent.Parent.Parent.ToString() + "\\Contas.csv";

            File.Delete(CaminhoarquivoCSV);
            using (StreamWriter outputFile = new StreamWriter(CaminhoarquivoCSV))
            {
                foreach (var Conta in Contas)
                {
                    if (Conta.Value.Tipo=="pf")
                    {
                        outputFile.WriteLine($"{Conta.Value.ID};{Conta.Value.Tipo};{((PessoaFisica)Conta.Value).CPF};{Conta.Value.Nome};{Conta.Value.Saldo}");
                    }
                    else
                    {
                        outputFile.WriteLine($"{Conta.Value.ID};{Conta.Value.Tipo};{((PessoaJuridica)Conta.Value).CNPJ};{Conta.Value.Nome};{Conta.Value.Saldo}");
                    }
                }
              
            }
        }
    }
}
