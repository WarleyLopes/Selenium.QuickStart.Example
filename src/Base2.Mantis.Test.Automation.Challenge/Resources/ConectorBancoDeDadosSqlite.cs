using System;
using System.Data.SQLite;

namespace Base2.Mantis.Test.Automation.Challenge.Resources
{
    /// <summary>
    /// Classe implementada pela necessidade de se preparar um arquivo de banco de dados do Sqlite na primeira execução do projeto
    /// </summary>
    public static class ConectorBancoDeDadosSQLite
    {
        /// <summary>
        /// Verifica se o arquivo de banco de dados (.sqlite) existe, se não, cria ele com uma certa estrutura de tabela
        /// </summary>
        public static void PreparaArquivoDoBancoDeDadosSeNecessario()
        {
            //Valida se arquivo de banco de dados .sqlite existe, ignorando sua criação ou então cria sua estrutura de tabelas e inserção inicial
            if (!System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + "MyDatabase.sqlite"))
            {
                //Cria o arquivo de banco de dados .sqlite
                SQLiteConnection.CreateFile(AppDomain.CurrentDomain.BaseDirectory+"MyDatabase.sqlite");

                //Prepara string para criação da tabela
                string criacaoDeTabelaBugs = "CREATE TABLE bugs (id UNIQUEIDENTIFIER,name VARCHAR(20), steps VARCHAR(200))";

                //Prepara a string para preenchimento aleatório de dados à tabela acima
                string insercaoDeDadosNaTabelaBugs = "INSERT INTO bugs VALUES (\"" + Guid.NewGuid() + "\",\"Mantis - BUG " + new Random().Next(1, 1000) + "\",\"1 - Abrir o site do mantis \n 2-Testar igual louco\")," +
                                                               "(\"" + Guid.NewGuid() + "\",\"Mantis - BUG " + new Random().Next(1, 1000) + "\",\"1 - Abrir o site do mantis \n 2-Testar igual louco\")";

                //Prepara o objeto para a conexão para acessar o SQLite
                SQLiteConnection conexaoBDSQLite = new SQLiteConnection("Data Source="+ AppDomain.CurrentDomain.BaseDirectory + "MyDatabase.sqlite;Version=3;");

                //Abre a conexão
                conexaoBDSQLite.Open();

                //Executa os comandos SQL criados nas strings acima com a conexão aberta do SQLite acima
                new SQLiteCommand(criacaoDeTabelaBugs, conexaoBDSQLite).ExecuteNonQuery();
                new SQLiteCommand(insercaoDeDadosNaTabelaBugs, conexaoBDSQLite).ExecuteNonQuery();

                //Após execução dos comandos acima, fecha a conexão com o banco de dados
                conexaoBDSQLite.Close();
            }
        }
    }
}
