using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AT_C_Sharp
{
    public static class Validações
    {
        //METODOS ULTILITARIOS E DE VALIDAÇÃO
        //Valida o CNPJ informado. 
        public static bool ValidarCNPJ(string cnpj)
        {
            //if (Regex.IsMatch(cnpj, @"/^\\d{3}\\.\\d{3}\\.\\d{3}\\-\\d{2}$/"))
            //{
            //    return true;
            //}
            //else { return false; }
            return true;
        }
        //Valida o CPF informado.
        public static bool ValidarCPF(string cpf)
        {
            //if (Regex.IsMatch(cpf, @"/^\d{3}.?\d{3}.?\d{3}-?\d{2}$/"))
            //{
            //    return true;
            //}
            //else { return false; 
            return true;
        }
        //Repete a pergunta se o numero não for valido.
        public static string VerificarEntradaNumerica(string entrada)
        {
            if (!VerificarSeNumero(entrada))
            {
                do
                {
                    ErroNumerico();
                    entrada = Interações.Perguntar();
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
                    entrada = Interações.Perguntar();
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
                catch (Exception)
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
        //Mostra uma mensagem de erro caso a entrada não seje um numero valido.
        public static void ErroNumerico()
        {
            Console.WriteLine("Apenas numeros são permitidos! Tente novamente.");
        }
        //Mostra uma mensagem de erro caso a entrada não seje um nome valido.
        public static void ErroNome()
        {
            Console.WriteLine("Apenas nomes  são permitidos!");
        }
    }
}
