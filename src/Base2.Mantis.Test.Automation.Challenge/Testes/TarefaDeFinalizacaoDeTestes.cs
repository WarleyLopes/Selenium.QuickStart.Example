﻿using NUnit.Framework;
using Selenium.QuickStart.Nucleo;

namespace Mantis_Warley.Testes
{
    /// <summary>
    /// <para>Classe para implementar um teste vazio com nome fixo e específico com</para>
    /// <para>a intenção de que seja o último a ser executado para rotinas internas</para>
    /// <para>da biblioteca Selenium.QuickStart conseguir afirmar que é o último teste</para>
    /// <para>pois o NUnit executa o OneTimeTearDown a cada final de classe de testes</para>
    /// </summary>
    public class TarefaDeFinalizacaoDeTestes : BaseDeTeste
    {
        /// <summary>
        /// <para>Método de teste vazio com o simples propósito de servir para verificar seu nome,</para>
        /// <para>ao ser o último a ser executado pela ordenação alfabética padrão do NUNIT</para>
        /// <para>permitindo que rotinas de finalização de relatório, email, executem só 1 vez</para>
        /// <para>devido o NUnit executar o OneTimeTearDown a cada final de classe de testes</para>
        /// </summary>
        [Test]
        public void Z99999_Selenium_QuickStart_Teste_Para_Finalizar_Rotinas_De_Log_De_Testes() { }
    }
}
