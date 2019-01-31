using NUnit.Framework;
using System.Collections;
using Selenium.QuickStart.Utilidades;
using System.Resources;

namespace Base2.Mantis.Test.Automation.Challenge.Resources
{
    /// <summary>
    /// Classe para implementar a forma que DataDriven é fornecido aos metodos de testes
    /// </summary>
    public class RepositorioDataDriven
    {
        /// <summary>
        /// <para>Método para ser utilizado em métodos de teste que retorna um IEnumerable com dados em</para>
        /// <para>array de string de um CSV nomeado MyDataSource.csv no Properties.Resources do projeto</para>
        /// </summary>
        public static IEnumerable TarefasParaReportarDoCsvMyDataSource
        {
            //Para cada linha do CSV, ignorando o header se existir, retorna um novo TestCaseData com string[] para cada linha
            get
            {
                foreach (string[] data in LeitorCsv.GetDados(Properties.Resources.TarefasParaReportar))
                    yield return new TestCaseData(data);
            }
        }
    }
}
