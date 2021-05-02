using Fintech.Dominio;
using Fintech.Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Fintech.Repositorios.SistemaArquivos
{
    public class MovimentoRepositorio : IMovimentoRepositorio
    {
        public MovimentoRepositorio(string caminho)
        {
            Caminho = caminho;
        }

        private const string DiretorioBase = "Dados";

        public string Caminho { get; }

        public void Inserir(Movimento movimento)
        {
            var registro = $"{movimento.Conta.Agencia.Numero};{movimento.Conta.Numero};" +
                $"{movimento.Data};{(int)movimento.Operacao};{movimento.Valor}";

            if (!Directory.Exists(DiretorioBase))
            {
                Directory.CreateDirectory(DiretorioBase);
            }

            File.AppendAllText($"{DiretorioBase}\\Movimento.txt", registro + Environment.NewLine);
        }

        public List<Movimento> Selecionar(int numeroAgencia, int numeroConta)
        {
            //Thread.Sleep(7000);

            var movimentos = new List<Movimento>();

            foreach (var linha in File.ReadAllLines(Caminho))
            {
                if (linha == string.Empty) continue;

                var propriedades = linha.Split(';');
                var propriedadeNumeroAgencia = Convert.ToInt32(propriedades[0]);
                var propriedadeNumeroConta = Convert.ToInt32(propriedades[1]);
                var data = Convert.ToDateTime(propriedades[2]);
                var operacao = (Operacao)Convert.ToInt32(propriedades[3]);
                var valor = Convert.ToDecimal(propriedades[4]);

                if (numeroAgencia == propriedadeNumeroAgencia && numeroConta == propriedadeNumeroConta)
                {
                    var movimento = new Movimento(operacao, valor);
                    movimento.Data = data;

                    movimentos.Add(movimento);
                }
            }

            return movimentos;
        }

        public async Task<List<Movimento>> SelecionarAsync(int numeroAgencia, int numeroConta)
        {
            //await Task.Delay(7000);

            var movimentos = new List<Movimento>();

            foreach (var linha in await File.ReadAllLinesAsync(Caminho))
            {
                if (linha == string.Empty) continue;

                var propriedades = linha.Split(';');
                var propriedadeNumeroAgencia = Convert.ToInt32(propriedades[0]);
                var propriedadeNumeroConta = Convert.ToInt32(propriedades[1]);
                var data = Convert.ToDateTime(propriedades[2]);
                var operacao = (Operacao)Convert.ToInt32(propriedades[3]);
                var valor = Convert.ToDecimal(propriedades[4]);

                if (numeroAgencia == propriedadeNumeroAgencia && numeroConta == propriedadeNumeroConta)
                {
                    var movimento = new Movimento(operacao, valor);
                    movimento.Data = data;

                    movimentos.Add(movimento);
                }
            }

            return movimentos;
        }
    }
}