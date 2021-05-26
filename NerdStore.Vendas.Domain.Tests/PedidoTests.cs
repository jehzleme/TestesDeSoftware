using NerdStore.Core.DomainObjects;
using System;
using System.Linq;
using Xunit;

namespace NerdStore.Vendas.Domain.Tests
{
    public class PedidoTests
    {
        [Fact(DisplayName = "Adicionar Item Pedido Novo")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void Deve_Atualizar_Valor_Ao_Adicionar_Item_Pedido_Novo()
        {
            // Arrange
            var pedido = new Pedido();
            var pedidoItem = new PedidoItem(Guid.NewGuid(), "Produto Teste", 2, 100);

            // Act
            pedido.AdicionarItem(pedidoItem);

            // Assert
            Assert.Equal(200, pedido.ValorTotal);
        }

        [Fact(DisplayName = "Adicionar Item Pedido Existente")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void Deve_Incrementar_Unidades_E_Somar_Valores_Ao_Adicionar_Item_Pedido_Existente()
        {
            // Arrange
            var pedido = new Pedido();
            var produtoId = Guid.NewGuid();
            var pedidoItem = new PedidoItem(produtoId, "Produto Teste", 2, 100);
            pedido.AdicionarItem(pedidoItem);

            var pedidoItem2 = new PedidoItem(produtoId, "Produto Teste", 1, 100);

            // Act
            pedido.AdicionarItem(pedidoItem2);
            // Assert

            Assert.Equal(300, pedido.ValorTotal);
            Assert.Single(pedido.PedidoItens);
            Assert.Equal(3, pedido.PedidoItens.FirstOrDefault(p => p.ProdutoId == produtoId).Quantidade);
        }

        [Fact(DisplayName = "Adicionar Item Pedido Acima do Permitido")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void Deve_Retornar_Erro_Ao_Adicionar_Unidades_Acima_Permitido()
        {
            //Arrange
            var pedido = new Pedido();
            var produtoId = Guid.NewGuid();
            var pedidoItem = new PedidoItem(produtoId, "Produto Teste", Pedido.MAX_UNIDADES_ITEM + 1, 100);

            //Act & Assert
            Assert.Throws<DomainException>(() => pedido.AdicionarItem(pedidoItem));
        }

        [Fact(DisplayName = "Adicionar Item Pedido Existente Acima do Permitido")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void Deve_Retornar_Erro_Ao_Somar_Unidades_Acima_Permitido_Item_Existente()
        {
            //Arrange
            var pedido = new Pedido();
            var produtoId = Guid.NewGuid();
            var pedidoItem = new PedidoItem(produtoId, "Produto Teste", 1, 100);
            var pedidoItem2 = new PedidoItem(produtoId, "Produto Teste", Pedido.MAX_UNIDADES_ITEM, 100);
            pedido.AdicionarItem(pedidoItem);

            //Act & Assert
            Assert.Throws<DomainException>(() => pedido.AdicionarItem(pedidoItem2));
        }

        [Fact(DisplayName = "Atualizar Item Pedido Inexistente")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void Deve_Retornar_Erro_Ao_Tentar_Atualizar_Item_Pedido_Nao_Existente()
        {
            var pedido = PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var pedidoItemAtualizado = new PedidoItem(Guid.NewGuid(), "Produto Teste", 5, 100);

            Assert.Throws<DomainException>(() => pedido.AtualizarItem(pedidoItemAtualizado));
        }

        [Fact(DisplayName = "Atualizar Item Pedido Valido")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void Deve_Atualizar_Quantidade_Item_Pedido_Valido()
        {
            var pedido = PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var produtoId = Guid.NewGuid();
            var pedidoItem = new PedidoItem(produtoId, "Produto Teste", 2, 100);
            pedido.AdicionarItem(pedidoItem);
            var pedidoItemAtualizado = new PedidoItem(produtoId, "Produto Teste", 5, 100);
            var novaQuantidade = pedidoItemAtualizado.Quantidade;

            pedido.AtualizarItem(pedidoItemAtualizado);

            Assert.Equal(novaQuantidade, pedido.PedidoItens.FirstOrDefault(p => p.ProdutoId == produtoId).Quantidade);
        }

        [Fact(DisplayName = "Atualizar Item Pedido Validar Total")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void Deve_Calcular_Valor_Total_Ao_Atualizar_Item_Pedido()
        {
            var pedido = PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var produtoId = Guid.NewGuid();
            var pedidoItemExistente1 = new PedidoItem(Guid.NewGuid(), "Produto x", 2, 100);
            var pedidoItemExistente2 = new PedidoItem(produtoId, "Produto Teste", 3, 15);
            pedido.AdicionarItem(pedidoItemExistente1);
            pedido.AdicionarItem(pedidoItemExistente2);

            var pedidoItemAtualizado = new PedidoItem(produtoId, "Produto Teste", 5, 15);
            var totalPedido = pedidoItemExistente1.Quantidade * pedidoItemExistente1.ValorUnitario +
                              pedidoItemAtualizado.Quantidade * pedidoItemAtualizado.ValorUnitario;

            pedido.AtualizarItem(pedidoItemAtualizado);

            Assert.Equal(totalPedido, pedido.ValorTotal);
        }

        [Fact(DisplayName = "Atualizar Item Pedido Quantidade Acima do Permitido")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void Deve_Retornar_Erro_Ao_Atualizar_Pedido_Item_Com_Unidades_Acima_Do_Permitido()
        {
            var pedido = PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var produtoId = Guid.NewGuid();
            var pedidoItemExistente = new PedidoItem(produtoId, "Produto Teste", 3, 15);
            pedido.AdicionarItem(pedidoItemExistente);

            var pedidoItemAtualizado = new PedidoItem(produtoId, "Produto Teste", Pedido.MAX_UNIDADES_ITEM + 1, 15);

            Assert.Throws<DomainException>(() => pedido.AtualizarItem(pedidoItemAtualizado));
        }

        [Fact(DisplayName = "Remover Item Pedido Inexistente")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void Deve_Retornar_Erro_Ao_Tentar_Remover_Item_Inexistente()
        {
            var pedido = PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var pedidoItemRemover = new PedidoItem(Guid.NewGuid(), "Produto Teste", 5, 100);

            Assert.Throws<DomainException>(() => pedido.RemoverItem(pedidoItemRemover));
        }

        [Fact(DisplayName = "Remover Item Pedido Validar Total")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void Deve_Remover_Pedido_Item_E_Atualizar_Valor_Total()
        {
            var pedido = PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var produtoId = Guid.NewGuid();
            var pedidoItemExistente1 = new PedidoItem(Guid.NewGuid(), "Produto x", 2, 100);
            var pedidoItemExistente2 = new PedidoItem(produtoId, "Produto Teste", 3, 15);
            pedido.AdicionarItem(pedidoItemExistente1);
            pedido.AdicionarItem(pedidoItemExistente2);

            var totalPedido = pedidoItemExistente2.Quantidade * pedidoItemExistente2.ValorUnitario;

            pedido.RemoverItem(pedidoItemExistente1);

            Assert.Equal(totalPedido, pedido.ValorTotal);
        }

        [Fact(DisplayName = "Aplicar Voucher Válido")]
        public void Deve_Aplicar_Voucher_Valido()
        {
            var pedido = PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var voucher = new Voucher("PROMO15REAIS", null, 15, TipoDescontoVoucher.Valor, 1, DateTime.Now.AddDays(15), true, false);

            var result = pedido.AplicarVoucher(voucher);

            Assert.True(result.IsValid);
        }

        [Fact(DisplayName = "Aplicar Voucher Inválido")]
        public void Deve_Aplicar_Voucher_Invalido()
        {
            var pedido = PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var voucher = new Voucher("PROMO15REAIS", null, 15, TipoDescontoVoucher.Valor, 1, DateTime.Now.AddDays(-1), true, false);

            var result = pedido.AplicarVoucher(voucher);

            Assert.False(result.IsValid);
        }

        [Fact(DisplayName = "Aplicar Voucher Tipo Valor Desconto")]
        public void Deve_Descontar_Voucher_Valor_Total()
        {
            var pedido = PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());

            var pedidoItem1 = new PedidoItem(Guid.NewGuid(), "Produto Xpto", 2, 100);
            var pedidoItem2 = new PedidoItem(Guid.NewGuid(), "Produto Teste", 3, 15);

            pedido.AdicionarItem(pedidoItem1);
            pedido.AdicionarItem(pedidoItem2);

            var voucher = new Voucher("PROMO15REAIS", null, 15, TipoDescontoVoucher.Valor, 1, DateTime.Now.AddDays(10), true, false);

            var valorComDesconto = pedido.ValorTotal - voucher.ValorDesconto;

            pedido.AplicarVoucher(voucher);

            Assert.Equal(valorComDesconto, pedido.ValorTotal);
        }

        [Fact(DisplayName = "Aplicar Voucher Tipo Percentual Desconto")]
        public void Deve_Descontar_Voucher_Porcentagem_Valor_Total()
        {
            var pedido = PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());

            var pedidoItem1 = new PedidoItem(Guid.NewGuid(), "Produto Xpto", 2, 100);
            var pedidoItem2 = new PedidoItem(Guid.NewGuid(), "Produto Teste", 3, 15);

            pedido.AdicionarItem(pedidoItem1);
            pedido.AdicionarItem(pedidoItem2);

            var voucher = new Voucher("PROMO15OFF", 15, null, TipoDescontoVoucher.Porcentagem, 1, DateTime.Now.AddDays(10), true, false);

            var valorDesconto = (pedido.ValorTotal * voucher.PercentualDesconto) / 100;
            var valorComDesconto = pedido.ValorTotal - valorDesconto;

            pedido.AplicarVoucher(voucher);

            Assert.Equal(valorComDesconto, pedido.ValorTotal);
        }

        [Fact(DisplayName = "Aplicar Voucher Desconto Excede Valor Total")]
        public void Deve_Zerar_Valor_Total_Quando_Desconto_For_Maior()
        {
            var pedido = PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var pedidoItem1 = new PedidoItem(Guid.NewGuid(), "Produto Xpto", 2, 100);
            pedido.AdicionarItem(pedidoItem1);

            var voucher = new Voucher("PROMO15OFF", null, 300, TipoDescontoVoucher.Valor, 1, DateTime.Now.AddDays(10), true, false);
            pedido.AplicarVoucher(voucher);

            Assert.Equal(0, pedido.ValorTotal);
        }

        [Fact(DisplayName = "Aplicar Voucher Recalcular Desconto na modificação do pedido")]
        public void Deve_Calcular_Desconto_Valor_Total_Ao_Modificar_Item()
        {
            var pedido = PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var pedidoItem1 = new PedidoItem(Guid.NewGuid(), "Produto Xpto", 2, 100);
            pedido.AdicionarItem(pedidoItem1);

            var voucher = new Voucher("PROMO15OFF", null, 50, TipoDescontoVoucher.Valor, 1, DateTime.Now.AddDays(10), true, false);
            pedido.AplicarVoucher(voucher);


            var pedidoItem2 = new PedidoItem(Guid.NewGuid(), "Produto Teste", 4, 25);
            pedido.AdicionarItem(pedidoItem2);

            var totalEsperado = pedido.PedidoItens.Sum(i => i.Quantidade * i.ValorUnitario) - voucher.ValorDesconto;
            Assert.Equal(totalEsperado, pedido.ValorTotal);
        }
    }
}
