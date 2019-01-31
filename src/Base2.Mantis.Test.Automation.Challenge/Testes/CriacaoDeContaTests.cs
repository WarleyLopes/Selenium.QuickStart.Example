using System;
using System.Configuration;
using Mantis_Warley.Paginas;
using Selenium.QuickStart.Nucleo;
using NUnit.Framework;
using Selenium.QuickStart.Atributos;

namespace Mantis_Warley.Testes
{
    /// <summary>
    /// <para>Classe com herança da classe BaseDeTeste com rotinas comuns para realização de testes e</para>
    /// <para>com métodos de testes a serem executados para realizar de testes na tela de Criação de Conta</para>
    /// </summary>
    public class CriacaoDeContaTests : BaseDeTeste
    {
        //O padrão de instanciação do objeto POM da página a ser testada gera um warning sobre criação
        //de um novo objeto e não estar assinalando valor à ele portanto tendo um valor nulo o que nesse
        //caso não é válido de fato pois suas propriedades (elementos) são privates e já têm seus valores
        #pragma warning disable CS0649

        /// <summary>
        /// Objeto da classe onde os objetos e métodos de interação da página de criação de conta foram mapeados
        /// </summary>
        [PaginaEmPageObjectModel] CriacaoDeContaPage Pagina_De_Criacao_De_Conta;

        #pragma warning restore CS0649

        //Variáveis que podem ser utilizadas entre os testes desse contexto
        public string usuario = ConfigurationManager.AppSettings["USERNAME"];
        public string senha = ConfigurationManager.AppSettings["PASSWORD"];


        [Test]
        [Description("Valida o fluxo correto de navegação para página" +
            " de criação de conta através da página de login")]
        public void Teste_Navegacao_Para_Pagina_De_Login()
        {
            new LoginPage().Navegar_Para_Pagina_De_Criacao_De_Conta();

            Assert.That(Pagina_De_Criacao_De_Conta.Navegar_Para_Pagina_De_Login().Valida_Se_Esta_Na_Pagina());
        }

        [Test]
        [Description("Valida o correto funcionamento da validação de captcha ao tentar efetuar" +
            " uma criação de conta digitando um usuário, e-mail e captcha aleatórios")]
        public void Teste_Tentar_Criar_Nova_Conta_Sem_Resolver_Captcha()
        {
            new LoginPage().Navegar_Para_Pagina_De_Criacao_De_Conta();
            
            Pagina_De_Criacao_De_Conta.Efetuar_Criacao_De_Conta_Digitando_Com_Tab(
                new Guid().ToString(),
                new Guid() + "@" + new Guid() + ".com.br",
                new Random().Next(000000, 999999).ToString("000000")
                );

            Assert.That(Pagina_De_Criacao_De_Conta.
                Valida_Exibicao_De_Mensagem_De_Tentativa_De_Criacao_De_Conta("código de confirmação não combina"));
        }

        [Test]
        [Description("Valida o fluxo correto de navegação para página de" +
            " recuperação de senha através da página de criação de conta")]
        public void Teste_Navegacao_Para_Pagina_De_Recuperacao_De_Senha()
        {
            new LoginPage().Navegar_Para_Pagina_De_Criacao_De_Conta();

            Assert.That(Pagina_De_Criacao_De_Conta.
                Navegar_Para_Pagina_De_Recuperacao_De_Senha().Valida_Se_Esta_Na_Pagina());
        }
    }
}
