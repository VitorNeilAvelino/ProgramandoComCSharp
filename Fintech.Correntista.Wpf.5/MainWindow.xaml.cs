using Fintech.Dominio;
using Fintech.Repositorios.SistemaArquivos;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Fintech.Correntista.Wpf._5
{
    public partial class MainWindow : Window
    {
        readonly MovimentoRepositorio movimentoRepositorio = new(Properties.Settings.Default.CaminhoArquivoMovimento);
        public List<Cliente> Clientes { get; set; } = new List<Cliente>();
        public Cliente ClienteSelecionado { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            PopularControles();
        }

        private void PopularControles()
        {
            sexoComboBox.Items.Add(Sexo.Masculino);
            sexoComboBox.Items.Add(Sexo.Feminino);

            tipoContaComboBox.Items.Add(TipoConta.ContaCorrente);
            tipoContaComboBox.Items.Add(TipoConta.ContaEspecial);
            tipoContaComboBox.Items.Add(TipoConta.Poupanca);

            bancoComboBox.Items.Add(new Banco { Nome = "Banco 1", Numero = 177 });
            bancoComboBox.Items.Add(new Banco { Nome = "Banco 2", Numero = 178 });

            operacaoComboBox.Items.Add(Operacao.Deposito);
            operacaoComboBox.Items.Add(Operacao.Saque);

            clienteDataGrid.ItemsSource = Clientes;
        }

        private void IncluirClienteButtonClick(object sender, RoutedEventArgs e)
        {
            var endereco = new Endereco();
            endereco.Logradouro = logradouroTextBox.Text;
            endereco.Numero = numeroLogradouroTextBox.Text;
            endereco.Cidade = cidadeTextBox.Text;
            endereco.Cep = cepTextBox.Text;

            Cliente cliente = new();
            cliente.Cpf = cpfTextBox.Text;
            cliente.Nome = nomeTextBox.Text;
            cliente.DataNascimento = Convert.ToDateTime(dataNascimentoTextBox.Text, new CultureInfo("pt-BR"));
            cliente.Sexo = (Sexo)sexoComboBox.SelectedItem;
            cliente.EnderecoResidencial = endereco;

            Clientes.Add(cliente);

            MessageBox.Show("Cliente cadastrado com sucesso");
            LimparControlesCliente();
            clienteDataGrid.Items.Refresh();
            pesquisaClienteTabItem.Focus();
        }

        private void LimparControlesCliente()
        {
            cpfTextBox.Clear();
            nomeTextBox.Text = "";
            dataNascimentoTextBox.Text = null;
            sexoComboBox.SelectedIndex = -1;
            logradouroTextBox.Text = string.Empty;
            numeroLogradouroTextBox.Clear();
            cidadeTextBox.Clear();
            cepTextBox.Clear();
        }

        private void SelecionarClienteButtonClick(object sender, RoutedEventArgs e)
        {
            SelecionarCliente(sender);

            clienteTextBox.Text = $"{ClienteSelecionado.Nome} - {ClienteSelecionado.Cpf}";

            contasTabItem.Focus();
        }

        private void SelecionarCliente(object sender)
        {
            var botaoClicado = (Button)sender;
            var clienteSelecionado = botaoClicado.DataContext;

            ClienteSelecionado = (Cliente)clienteSelecionado;
        }

        private void principalTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var abaSelecionada = (TabItem)principalTabControl.SelectedItem;

            if (abaSelecionada.Name == "contasTabItem" && ClienteSelecionado == null)
            {
                MessageBox.Show("Selecione o Cliente na aba Clientes.");
                clientesTabItem.Focus();
            }
        }

        private void tipoContaComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tipoContaComboBox.SelectedIndex == -1) return;

            var tipoConta = (TipoConta)tipoContaComboBox.SelectedItem;

            if (tipoConta == TipoConta.ContaEspecial)
            {
                limiteDockPanel.Visibility = Visibility.Visible;
                limiteTextBox.Focus();
            }
            else
            {
                limiteDockPanel.Visibility = Visibility.Collapsed;
            }
        }

        private void incluirContaButton_Click(object sender, RoutedEventArgs e)
        {
            Conta conta = null;

            var agencia = new Agencia();
            agencia.Banco = (Banco)bancoComboBox.SelectedItem;
            agencia.Numero = Convert.ToInt32(numeroAgenciaTextBox.Text);
            agencia.DigitoVerificador = Convert.ToInt32(dvAgenciaTextBox.Text);

            var numero = Convert.ToInt32(numeroContaTextBox.Text);
            var digitoVerificador = dvContaTextBox.Text;

            switch ((TipoConta)tipoContaComboBox.SelectedItem)
            {
                case TipoConta.ContaCorrente:
                    conta = new ContaCorrente(agencia, numero, digitoVerificador);
                    break;
                case TipoConta.ContaEspecial:
                    var limite = Convert.ToDecimal(limiteTextBox.Text);
                    conta = new ContaEspecial(agencia, numero, digitoVerificador, limite);
                    break;
                case TipoConta.Poupanca:
                    conta = new Poupanca(agencia, numero, digitoVerificador);
                    break;
            }

            ClienteSelecionado.Contas.Add(conta);

            MessageBox.Show("Conta adicionada com sucesso.");
            LimparControlesConta();
            clienteDataGrid.Items.Refresh();
            clientesTabItem.Focus();
            pesquisaClienteTabItem.Focus();
        }

        private void LimparControlesConta()
        {
            clienteTextBox.Clear();
            bancoComboBox.SelectedIndex = -1;
            numeroAgenciaTextBox.Clear();
            dvAgenciaTextBox.Clear();
            numeroContaTextBox.Clear();
            dvContaTextBox.Clear();
            tipoContaComboBox.SelectedIndex = -1;
            limiteTextBox.Clear();
        }

        private void incluirOperacaoButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var conta = (Conta)contaComboBox.SelectedItem;
                var operacao = (Operacao)operacaoComboBox.SelectedItem;
                var valor = Convert.ToDecimal(valorTextBox.Text);

                var movimento = conta.EfetuarOperacao(valor, operacao);

                movimentoRepositorio.Inserir(movimento);

                movimentacaoDataGrid.ItemsSource = conta.Movimentos;
                movimentacaoDataGrid.Items.Refresh();

                saldoTextBox.Text = conta.Saldo.ToString("c");
            }
            catch (FileNotFoundException excecao)
            {
                MessageBox.Show($"O arquivo {excecao.FileName} não foi encontrado.");
            }
            catch (DirectoryNotFoundException)
            {
                MessageBox.Show($"O diretório {Properties.Settings.Default.CaminhoArquivoMovimento} não foi encontrado.");
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox
                    .Show($"O arquivo {Properties.Settings.Default.CaminhoArquivoMovimento} " +
                    $"está com o atributo Somente Leitura.");
            }
            catch (SaldoInsuficienteException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eita! Algo deu errado e em breve teremos uma solução."/* + ex.Message*/);
                //Logar(ex); - log4Net
            }
            finally
            {
                // É executado sempre! Mesmo que haja algum return no código.
            }
        }

        private void SelecionarContaButtonClick(object sender, RoutedEventArgs e)
        {
            SelecionarCliente(sender);

            contaTextBox.Text = $"{ClienteSelecionado.Nome} - {ClienteSelecionado.Cpf}";

            contaComboBox.ItemsSource = ClienteSelecionado.Contas;
            contaComboBox.Items.Refresh();

            LimparControlesOperacoes();

            operacoesTabItem.Focus();
        }

        private void LimparControlesOperacoes()
        {
            contaComboBox.SelectedIndex = -1;
            operacaoComboBox.SelectedIndex = -1;
            valorTextBox.Clear();
            movimentacaoDataGrid.ItemsSource = null;
            saldoTextBox.Clear();
        }

        private void contaComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (contaComboBox.SelectedIndex == -1)
            {
                return;
            }

            var conta = (Conta)contaComboBox.SelectedItem;

            conta.Movimentos = movimentoRepositorio.Selecionar(conta.Agencia.Numero, conta.Numero);

            movimentacaoDataGrid.ItemsSource = conta.Movimentos;
            saldoTextBox.Text = conta.Saldo.ToString();
        }
    }
}