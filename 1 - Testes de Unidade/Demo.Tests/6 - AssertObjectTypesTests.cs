using Xunit;

namespace Demo.Tests
{
    public class AssertObjectTypesTests
    {
        [Fact]
        public void FuncionarioFactory_Criar_DeveRetornarTipoFuncionario()
        {
            var funcionario = FuncionarioFactory.Criar("Eduardo", 10000);

            Assert.IsType<Funcionario>(funcionario);
        }
        
        [Fact]
        public void FuncionarioFactory_Criar_DeveRetornarTipoDerivadoPessoa()
        {
            var funcionario = FuncionarioFactory.Criar("Eduardo", 10000);

            Assert.IsAssignableFrom<Pessoa>(funcionario);
        }
    }
}
