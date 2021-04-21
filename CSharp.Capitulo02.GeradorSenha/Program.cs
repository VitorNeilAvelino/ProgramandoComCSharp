using System;

namespace CSharp.Capitulo02.GeradorSenha
{
    class Program
    {
        static void Main(string[] args)
        {
            //var quantidadeDigitos = 0;

            //Console.Write("Informe a quantidade de dígitos da senha (entre 4 e 10): ");

            //while (quantidadeDigitos == 0)
            //{
            //    quantidadeDigitos = ObterQuantidadeDigitos();
            //}

            int quantidadeDigitos;

            do
            {
                Console.Write("Informe a quantidade de dígitos da senha (entre 4 e 10): ");
                quantidadeDigitos = ObterQuantidadeDigitos();
            }
            while (quantidadeDigitos == 0);

            //var senha = "";
            //var randomico = new Random();

            //for (int i = 0; i < quantidadeDigitos; i++)
            //{
            //    var numero = randomico.Next(0, 10);

            //    senha += numero; //rnd.Next(0, 9);
            //}

            Console.WriteLine($"Senha gerada: {new Senha(quantidadeDigitos).Valor}");
            Environment.Exit(0);
        }

        private static int ObterQuantidadeDigitos()
        {
            //var quantidadeDigitos = 0;

            int.TryParse(Console.ReadLine(), out int quantidadeDigitos);

            //if (digitos < 4 || digitos > 10 || digitos % 2 != 0)
            if (quantidadeDigitos is (< 4 or > 10) || quantidadeDigitos % 2 != 0)
            {
                Console.WriteLine($"O valor {quantidadeDigitos} é inválido de acordo com as regras.");
                quantidadeDigitos = 0;
            }

            return quantidadeDigitos;
        }
    }
}