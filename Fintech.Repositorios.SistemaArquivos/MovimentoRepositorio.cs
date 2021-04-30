using Fintech.Dominio;
using System;
using System.Collections.Generic;
using System.IO;

namespace Fintech.Repositorios.SistemaArquivos
{
    public class MovimentoRepositorio : IMovimentoRepositorio
    {
        private const string DiretorioBase = "Dados";

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

        public List<Movimento> Selecionar(Conta conta)
        {
            throw new NotImplementedException();
        }
    }
}