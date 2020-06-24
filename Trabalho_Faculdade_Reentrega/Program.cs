using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
// Trabalho feito por JOÃO VICTOR FAGUNDES DE OLIVEIRA, DE ADS
namespace Trabalho_Faculdade_Reentrega
{
    class Program
    {
        static void Main(string[] args)
        {
            MenuPrincipal();
        }
        static List<Pessoas> pessoasInfo = new List<Pessoas>();

        public static void MenuPrincipal()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Gerenciador de Aniversários");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            DiaHoje();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            AniversarianteDia();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("1:Cadastro de Pessoas");
            Console.WriteLine("2:Listar Pessoas");
            Console.WriteLine("3:Pesquisar Pessoas");
            Console.WriteLine("4:Editar Pessoa");
            Console.WriteLine("5:Excluir Pessoa");
            //Diretorio do arquivo: Trabalho_Faculdade_Reentrega\bin\Debug\netcoreapp3.1\dir
            Console.WriteLine("6:Fazer CSV");
            Console.WriteLine("7:Sair");
            char option = Console.ReadLine().ToCharArray()[0];
            if (option == '1')
            {
                CadastrarPessoa();
                VoltarMenu();
            }
            else if (option == '2')
            {
                ListarPessoas();
                VoltarMenu();
            }
            else if (option == '3')
            {
                PesquisarPessoa();
                VoltarMenu();
            }
            else if (option == '4')
            {
                EditarPessoa();
                VoltarMenu();
            }
            else if (option == '5')
            {
                ExcluirPessoa();
                VoltarMenu();
            }
            else if (option == '6')
            {
                FazerCSV();
                VoltarMenu();
            }
            else if (option == '7')
            {
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("Opção inválida. Volte ao menu principal");
                Thread.Sleep(2000);
                MenuPrincipal();
            }
        }
        public static void CadastrarPessoa()
        {
            Console.Clear();
            Console.Write("Digite o nome da pessoa: ");
            string nome = Console.ReadLine();
            Console.Write("Digite o sobrenome da pessoa: ");
            string sobrenome = Console.ReadLine();
            Console.Write("Digite a data de nascimento da pessoa: ");
            DateTime datanascimento = DateTime.Parse(Console.ReadLine());
            var pessoa = new Pessoas();
            pessoa.Nome = nome.ToUpper();
            pessoa.Sobrenome = sobrenome.ToUpper();
            pessoa.DataNascimento = datanascimento;
            DateTime agora = DateTime.Today;
            var idade = agora.Year - pessoa.DataNascimento.Year;
            if (pessoa.DataNascimento.Date > agora.AddYears(-idade)) idade--;
            pessoa.Idade = idade;
            pessoasInfo.Add(pessoa);
            Console.WriteLine("Pessoa cadastrada.");
        }
        public static void PesquisarPessoa()
        {
            Console.Clear();
            Console.WriteLine("Como deseja fazer sua pesquisa:");
            Console.WriteLine("1:Por nome");
            Console.WriteLine("2:Por sobrenome");
            char option2 = Console.ReadLine().ToCharArray()[0];
            if (option2 == '1')
            {
                Console.Write("Escreva o nome: ");
                string pesquisanome = Console.ReadLine();
                pesquisanome = pesquisanome.ToUpper();
                var pessoasPesquisadas = pessoasInfo.Where(pessoa => pessoa.Nome.Contains(pesquisanome));
                var pessoasEncontradas = new List<Pessoas>();

                foreach (var pessoa in pessoasPesquisadas)
                {
                    pessoasEncontradas.Add(pessoa);
                }
                Console.Clear();
                foreach (var pessoa in pessoasEncontradas)
                {
                    Console.WriteLine("Nome: " + pessoa.Nome);
                    Console.WriteLine("Nome: " + pessoa.Sobrenome);
                    Console.WriteLine("Data de Nascimento: " + pessoa.DataNascimento);
                    Console.WriteLine("Idade: " + pessoa.Idade);
                    TempoAniversario(pessoa.DataNascimento, pessoa.Nome);
                }
                if (pessoasEncontradas.Count == 0)
                {
                    Console.WriteLine("Não houve resultados");
                }
            }
            else if (option2 == '2')
            {
                Console.Write("Escreva o sobrenome: ");
                string pesquisasobrenome = Console.ReadLine();
                pesquisasobrenome = pesquisasobrenome.ToUpper();
                var pessoasPesquisadas = pessoasInfo.Where(pessoa => pessoa.Sobrenome.Contains(pesquisasobrenome));
                var pessoasEncontradas = new List<Pessoas>();

                foreach (var pessoa in pessoasPesquisadas)
                {
                    pessoasEncontradas.Add(pessoa);
                }
                Console.Clear();
                foreach (var pessoa in pessoasEncontradas)
                {
                    Console.WriteLine("Nome: " + pessoa.Nome);
                    Console.WriteLine("Nome: " + pessoa.Sobrenome);
                    Console.WriteLine("Data de Nascimento: " + pessoa.DataNascimento);
                    Console.WriteLine("Idade: " + pessoa.Idade);
                    TempoAniversario(pessoa.DataNascimento, pessoa.Nome);
                }
                if (pessoasEncontradas.Count == 0)
                {
                    Console.WriteLine("Não houve resultados");
                }
            }    
            else
            {
                Console.WriteLine("Opção inválida. Volte ao menu principal");
                Thread.Sleep(2000);
                MenuPrincipal();
            }
        }
        static void TempoAniversario(DateTime pessoa, string nome)
        {
            DateTime agora = DateTime.Today;
            DateTime prox = new DateTime(agora.Year, pessoa.Month, pessoa.Day);
            if (prox < agora)
            {
                prox = prox.AddYears(1);
            }
            int diasparaaniversario = (prox - agora).Days;
            Console.WriteLine("Faltam " + diasparaaniversario + " dias para o aniversário da " + nome);
        }
        static void ListarPessoas()
        {
            Console.Clear();
            foreach (var pessoa in pessoasInfo)
            {
                int contador = pessoasInfo.IndexOf(pessoa) + 1;
                Console.WriteLine(contador + "º pessoa:");
                Console.WriteLine("NOME: " + pessoa.Nome);
                Console.WriteLine("SOBRENOME: " + pessoa.Sobrenome);
                string datanascimento = pessoa.DataNascimento.ToString("dd/MM/yyyy");
                Console.WriteLine("DATA DE NASCIMENTO: " + datanascimento);
                Console.WriteLine("IDADE: "+ pessoa.Idade);
                Console.WriteLine("-------------------");
            }
        }
        static void ExcluirPessoa()
        {
            Console.Clear();
            ListarPessoas();
            Console.Write("Escreva o número referente a pessao que deseja excluir: ");
            int contador = int.Parse(Console.ReadLine());
            int contador2 = contador - 1;
            pessoasInfo.RemoveAt(contador2);
            Console.WriteLine("Pessoa excluída com sucesso");
        }
        static void VoltarMenu()
        {
            Thread.Sleep(2000);
            MenuPrincipal();
        }
        static void EditarPessoa()
        {
            Console.Clear();
            ListarPessoas();
            Console.Write("Escreva o número referente a pessoa que deseja editar: ");
            int contador = int.Parse(Console.ReadLine());
            int contador2 = contador - 1;
            Console.Write("Escreva um novo nome: ");
            string nome = Console.ReadLine();
            Console.Write("Escreva um novo sobrenome: ");
            string sobrenome = Console.ReadLine();
            Console.Write("Escreva uma nova data de nascimento: ");
            DateTime datanascimento = DateTime.Parse(Console.ReadLine());
            pessoasInfo.RemoveAt(contador2);
            pessoasInfo.Insert(contador2, new Pessoas(){Nome = nome.ToUpper(), Sobrenome = sobrenome.ToUpper(), DataNascimento = datanascimento});
            CalcularIdade();
            Console.Write("Pessoa editada com sucesso");
            VoltarMenu();
        }
        static void FazerCSV()
        {
            var diretorio = "dir";
            Directory.CreateDirectory(diretorio);
            var nomeArquivo = "Texto.csv";
            var caminhoArquivo = Path.Combine(diretorio, nomeArquivo);
            var csv = new StringBuilder();
            csv.Clear();
            csv.AppendLine("Nome;Data de Nascimento;Idade");
            foreach (var pessoa in pessoasInfo)
            {
                csv.Append(pessoa.Nome);
                csv.Append(";");
                string datanascimento = pessoa.DataNascimento.ToString("dd/MM/yyyy");
                csv.Append(datanascimento);
                csv.Append(";");
                csv.Append(pessoa.Idade);
                csv.Append(";");
                csv.AppendLine();
            }
            File.WriteAllText(caminhoArquivo, csv.ToString());
            Console.Clear();
            Console.WriteLine("O CSV foi atualizado com sucesso");
            VoltarMenu();
        }
        static void CalcularIdade()
        {
            foreach (var pessoa in pessoasInfo)
            {
                DateTime agora = DateTime.Today;
                var idade = agora.Year - pessoa.DataNascimento.Year;
                if (pessoa.DataNascimento.Date > agora.AddYears(-idade)) idade--;
                pessoa.Idade = idade;
            }
        }
        static void AniversarianteDia()
        {
            DateTime agora = DateTime.Today;
            string nome = null;
            foreach (var pessoa in pessoasInfo)
            {
                DateTime agoraaniversariante = new DateTime( agora.Year, pessoa.DataNascimento.Month, pessoa.DataNascimento.Day);
                if (agoraaniversariante == agora)
                {
                    nome = pessoa.Nome;
                    Console.WriteLine("O aniversariante do dia é " + nome + " e está fazendo " + pessoa.Idade + " anos"); ;
                } 
            }
            if (nome == null)
            {
                Console.WriteLine("Nenhuma pessoa faz aniversário hoje");
            }
        }
        static void DiaHoje()
        {
            DateTime agora = DateTime.Today;
            string agoraaniversario = agora.ToString("dd/MM/yyyy");
            Console.WriteLine("Dia de hoje " + agoraaniversario);
        }
    }
    public class Pessoas : IdadePessoa
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public DateTime DataNascimento { get; set; }
    }

    public class IdadePessoa
    {
        public int Idade { get; set; }
    }
}


