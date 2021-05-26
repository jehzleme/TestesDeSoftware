using Xunit;

namespace Demo.Tests
{
    public class AssertCollectionsTests
    {
        [Fact]
        public void Funcionario_Habilidades_NaoDevePossuirHabilidadesVazias()
        {
            var funcionario = FuncionarioFactory.Criar("Eduardo", 10000);

            Assert.All(funcionario.Habilidades, habilidade => Assert.False(string.IsNullOrWhiteSpace(habilidade)));
        }
        
        [Fact]
        public void Funcionario_Habilidades_JuniorDevePossuirHabilidadeBasica()
        {
            var funcionario = FuncionarioFactory.Criar("Eduardo", 1000);

            Assert.Contains("OOP", funcionario.Habilidades);
        }
        
        [Fact]
        public void Funcionario_Habilidades_JuniorNaoDevePossuirHabilidadeAvancada()
        {
            var funcionario = FuncionarioFactory.Criar("Eduardo", 1000);

            Assert.DoesNotContain("Microsservices", funcionario.Habilidades);
        }
        
        [Fact]
        public void Funcionario_Habilidades_SeniorDevePossuirTodasHabilidades()
        {
            var funcionario = FuncionarioFactory.Criar("Eduardo", 15000);

            var habilidades = new[]
            {
                "Lógica de Programação",
                "OOP",
                "Testes",
                "Microsservices"
            };


            Assert.Equal(habilidades, funcionario.Habilidades);
        }

    }
}
