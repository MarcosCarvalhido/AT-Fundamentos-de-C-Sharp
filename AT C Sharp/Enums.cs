using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AT_C_Sharp
{
    public static class Enums
    {
        //ENUMS DE OPÇÕES

        //Lista de opções para o menu principal.
        public  enum eOpção
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
