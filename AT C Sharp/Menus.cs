using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AT_C_Sharp
{
    public static class Menus
    {
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
        //Mostra o menu de tipos de conta com as opções disponiveis.
        public static void MostrarMenuTiposDeConta()
        {
            Console.Clear();
            Console.WriteLine("Escolha uma das opções a seguir:");
            Console.WriteLine("[1] - Conta de Pessoa Fisica.");
            Console.WriteLine("[2] - Conta de Pessoa Juridica.");
        }
        //Mostra o menu de alterações com as opções disponiveis.
        public static void MostrarMenuAlteração()
        {
            Console.Clear();
            Console.WriteLine("Escolha uma das opções a seguir:");
            Console.WriteLine("[1] - Creditar valor a conta.");
            Console.WriteLine("[2] - Debitar valor a conta.");
        }
    }
}
