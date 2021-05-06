using Fintech.Dominio;
using Fintech.Dominio.Interfaces;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using System.Threading.Tasks;
using System;

namespace Fintech.Repositorios.SqlServer
{
    public class MovimentoRepositorio : IMovimentoRepositorio
    {
        private readonly string stringConexao;

        public MovimentoRepositorio(string stringConexao)
        {
            this.stringConexao = stringConexao;
        }

        public void Atualizar(Movimento cliente)
        {
            throw new System.NotImplementedException();
        }

        public void Excluir(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Inserir(Movimento movimento)
        {
            var instrucao = @$"Insert Movimento(IdConta, Data, Valor, Operacao)
                                        values({movimento.Conta.Numero}, @Data, @Valor, @Operacao)";

            using (var conexao = new SqlConnection(stringConexao))
            {
                conexao.Execute(instrucao, movimento);
            }
        }

        public List<Movimento> Selecionar(int numeroAgencia, int numeroConta)
        {
            var instrucao = @$"Select Data, Operacao, Valor from Movimento where IdConta=@numeroConta";

            using (var conexao = new SqlConnection(stringConexao))
            {
                return conexao.Query<Movimento>(instrucao, new { numeroConta }).AsList();
            }
        }

        public List<Movimento> Selecionar()
        {
            throw new System.NotImplementedException();
        }

        public Movimento Selecionar(int id)
        {
            throw new System.NotImplementedException();
        }

        public List<Movimento> Selecionar(Predicate<Movimento> consulta)
        {

            using (var conexao = new SqlConnection(stringConexao))
            {
                return conexao.Query<Movimento>("").AsList();
            }
        }

        public Task<List<Movimento>> SelecionarAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}