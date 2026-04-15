using SDD_Api.Contracts;
using SDD_Api.Infrastructure.Procedures;
using SDD_Api.Models;

namespace SDD_Api.Service
{
    public class chradm_001_service
    {
        private readonly chradm_001_pkg _processopkg;

        public chradm_001_service(chradm_001_pkg processopkg) => _processopkg = processopkg ?? throw new ArgumentNullException(nameof(processopkg));

        public async Task<ApiResponse<List<chradm_001_model.Consulta_Produto>>> Consulta_ProdutoAsync(string? pCodProduto)
        {
            return await _processopkg.Consulta_ProdutoAsync(pCodProduto);
        }

        public async Task<ApiResponse<chradm_001_model.Valida_Rastreabilidade>> Valida_RastreabilidadeAsync(string? pCodProduto, string? pNrRastrea)
        {
            return await _processopkg.Valida_RastreabilidadeAsync(pCodProduto, pNrRastrea);
        }

        public async Task<ApiResponse<List<chradm_001_model.Consulta_Rastreabilidade>>> Consulta_RastreabilidadeAsync(
            string? pCodProduto,
            string? pDataInicial,
            string? pDataFinal)
        {
            return await _processopkg.Consulta_RastreabilidadeAsync(pCodProduto, pDataInicial, pDataFinal);
        }
    }
}
