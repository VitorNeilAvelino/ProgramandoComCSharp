using System;
using static System.Console;

//namespace CSharp.Capitulo01.ValeTransporte
//{
    //class Program
    //{
    //    static void Main(string[] args)
    //    {
            Inicio:

            Write("Informe o nome do funcionário: ");
            var nome = Console.ReadLine();

            Console.Write("Informe o salário do funcionário: ");
            var salario = Convert.ToDecimal(Console.ReadLine());

            Console.Write("Informe o valor gasto com transporte: ");
            var gastoComTransporte = Convert.ToDecimal(Console.ReadLine());

            //cálculo de 6% sobre o salário
            var descontoMaximo = salario * 6 / 100;

            //verificação do valor real do vale-transporte
            var descontoVT = (gastoComTransporte > descontoMaximo ? descontoMaximo : gastoComTransporte);

            var resultado = $"\nFuncionário: {nome}\n" +
                $"Salário: {salario:c}" +
                Environment.NewLine + 
                $"Desconto VT: {descontoVT:c}";

            Console.WriteLine(resultado);

            Console.WriteLine("\nPressione Enter para novo cálculo ou Esc para sair.");

            var comando = Console.ReadKey();

            if (comando.Key == ConsoleKey.Escape)
            {
                Environment.Exit(0);
            }

            Console.Clear();

            goto Inicio;            
    //    }
    //}
//}