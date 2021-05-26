using System;
using System.Linq;
using Xunit;

namespace NerdStore.Vendas.Domain.Tests
{
    public class VoucherTests
    {
        [Fact(DisplayName = "Validar Voucher Tipo Valor")]
        [Trait("Categoria", "Vendas - Voucher")]
        public void Deve_Validar_Voucher_Tipo_Valor()
        {
            var voucher = new Voucher("PROMO15REAIS", null, 15, TipoDescontoVoucher.Valor, 1, DateTime.Now.AddDays(15), true, false);

            var result = voucher.ValidarSeAplicavel();

            Assert.True(result.IsValid);
        }

        [Fact(DisplayName = "Validar Voucher Tipo Valor Inválido")]
        [Trait("Categoria", "Vendas - Voucher")]
        public void Deve_Validar_Voucher_Tipo_Valor_Invalido()
        {
            var voucher = new Voucher("", null, null, TipoDescontoVoucher.Valor, 0, DateTime.Now.AddDays(-1), false, true);

            var result = voucher.ValidarSeAplicavel();

            Assert.False(result.IsValid);
            Assert.Equal(6, result.Errors.Count);
            Assert.Contains(VoucherValidation.AtivoErroMsg, result.Errors.Select(x=>x.ErrorMessage));
            Assert.Contains(VoucherValidation.CodigoErroMsg, result.Errors.Select(x=>x.ErrorMessage));
            Assert.Contains(VoucherValidation.DataValidadeErroMsg, result.Errors.Select(x=>x.ErrorMessage));
            Assert.Contains(VoucherValidation.QuantidadeErroMsg, result.Errors.Select(x=>x.ErrorMessage));
            Assert.Contains(VoucherValidation.UtilizadoErroMsg, result.Errors.Select(x=>x.ErrorMessage));
            Assert.Contains(VoucherValidation.ValorDescontoErroMsg, result.Errors.Select(x=>x.ErrorMessage));
        }

        [Fact(DisplayName = "Validar Voucher Tipo Porcentagem")]
        [Trait("Categoria", "Vendas - Voucher")]
        public void Deve_Validar_Voucher_Tipo_Porcentagem()
        {
            var voucher = new Voucher("PROMO15OFF", 15, null, TipoDescontoVoucher.Porcentagem, 1, DateTime.Now.AddDays(15), true, false);

            var result = voucher.ValidarSeAplicavel();

            Assert.True(result.IsValid);
        }

        [Fact(DisplayName = "Validar Voucher Tipo Percentual Inválido")]
        [Trait("Categoria", "Vendas - Voucher")]
        public void Deve_Validar_Voucher_Tipo_Percentual_Invalido()
        {
            var voucher = new Voucher("", null, null, TipoDescontoVoucher.Porcentagem, 0, DateTime.Now.AddDays(-1), false, true);

            var result = voucher.ValidarSeAplicavel();

            Assert.False(result.IsValid);
            Assert.Equal(6, result.Errors.Count);
            Assert.Contains(VoucherValidation.AtivoErroMsg, result.Errors.Select(x => x.ErrorMessage));
            Assert.Contains(VoucherValidation.CodigoErroMsg, result.Errors.Select(x => x.ErrorMessage));
            Assert.Contains(VoucherValidation.DataValidadeErroMsg, result.Errors.Select(x => x.ErrorMessage));
            Assert.Contains(VoucherValidation.QuantidadeErroMsg, result.Errors.Select(x => x.ErrorMessage));
            Assert.Contains(VoucherValidation.UtilizadoErroMsg, result.Errors.Select(x => x.ErrorMessage));
            Assert.Contains(VoucherValidation.PercentualDescontoErroMsg, result.Errors.Select(x => x.ErrorMessage));
        }
    }
}
