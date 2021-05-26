using FluentValidation;
using NerdStore.Vendas.Domain;
using System;

namespace NerdStore.Vendas.Application.Commands
{
    public class AdicionarItemPedidoCommandValidation : AbstractValidator<AdicionarItemPedidoCommand>
    {
        public static string IdClienteErroMsg => "Id do cliente inválido";
        public static string IdProdutoErroMsg => "Id do produto inválido";
        public static string NomeErroMsg => "O nome do produto não foi informado";
        public static string QuantidadeMaxErroMsg => $"A quantidade máxima de um item é {Pedido.MAX_UNIDADES_ITEM}";
        public static string QuantidadeMinErroMsg => "A quanitdade mínima de um item é 1";
        public static string ValorErroMsg => "O valor do item precisa ser maior que 0";

        public AdicionarItemPedidoCommandValidation()
        {
            RuleFor(c => c.ClienteId)
                .NotEqual(Guid.Empty)
                .WithMessage(IdClienteErroMsg);

            RuleFor(c => c.ProdutoId)
                .NotEqual(Guid.Empty)
                .WithMessage(IdProdutoErroMsg);

            RuleFor(c => c.Nome)
                .NotEmpty()
                .WithMessage(NomeErroMsg);

            RuleFor(c => c.Quantidade)
                .GreaterThan(0)
                .WithMessage(QuantidadeMinErroMsg)
                .LessThanOrEqualTo(Pedido.MAX_UNIDADES_ITEM)
                .WithMessage(QuantidadeMaxErroMsg);

            RuleFor(c => c.ValorUnitario)
                .GreaterThan(0)
                .WithMessage(ValorErroMsg);
        }
    }
}
