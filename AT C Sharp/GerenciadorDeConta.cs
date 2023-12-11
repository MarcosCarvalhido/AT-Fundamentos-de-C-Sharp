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

                        Conta conta = new Conta()
                        {
                            ID = int.Parse(dados[0]),
                            Nome = dados[1],
                            Saldo = decimal.Parse(dados[2])
                        };
                        Contas.Add(conta.ID, conta);
                    }
                }
            }
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
        //Cria uma conta nova apartir dos dados apresentados.
        public static void CriarConta(Conta conta)
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
        //Remove uma conta apartir de um ID. Retorna uma opção se deletada, não encontrada ou com saldo.
        public static Resposta RemoverConta(int id)
        {
            Conta conta = EncontrarConta(id);
            if (conta != null)
            {
                if (conta.Saldo <= 0)
                {
                    Contas.Remove(conta.ID);
                    return Resposta.Excluida;
                }
                else
                {
                    return Resposta.PossuiSaldo;
                }
            }
            else
            {
                return Resposta.NãoEcontrado;
            }
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
                foreach (var Conta in Contas )
                    outputFile.WriteLine($"{Conta.Value.ID};{Conta.Value.Nome};{Conta.Value.Saldo}");
            }
        }

        //Lista de opções para a ação de apagar.
        public enum Resposta
        {
            NãoEcontrado = 0,
            PossuiSaldo = 1,
            Excluida = 2,
        }
    }
}
