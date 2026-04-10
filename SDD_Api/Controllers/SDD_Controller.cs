using Microsoft.AspNetCore.Mvc;
using SDD_Api.Contracts;
using SDD_Api.Service;

namespace SDD_Api.Controllers
{
    [ApiController]
    [Route("sdd")]
    public class SDD_Controller : Controller
    {
        private readonly chradm_001_service _chradmservice;

        public SDD_Controller(chradm_001_service chradmservice)
        {
            _chradmservice = chradmservice ?? throw new ArgumentNullException(nameof(chradmservice));
        }

        [HttpGet("consultaproduto")]
        public async Task<IActionResult> GetConsultaProduto([FromQuery] string? codProduto)
        {
            try
            {
                var resp = await _chradmservice.Consulta_ProdutoAsync(codProduto);

                if (resp.Sucesso)
                    return Ok(resp.Dados);

                return BadRequest(resp.Erro ?? new ApiErrorInfo(-1, "Erro ao consultar produto."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message, detail = ex.InnerException?.Message });
            }
        }
        [HttpGet("validarastreabilidade")]
        public async Task<IActionResult> GetValidaRastreabilidade(
            [FromQuery] string? codProduto,
            [FromQuery] string? nrRastrea)
        {
            try
            {
                var resp = await _chradmservice.Valida_RastreabilidadeAsync(codProduto, nrRastrea);

                if (resp.Sucesso)
                    return Ok(resp.Dados);

                return BadRequest(resp.Erro ?? new ApiErrorInfo(-1, "Erro ao validar rastreabilidade."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message, detail = ex.InnerException?.Message });
            }
        }
        [HttpGet("consultarastreabilidade")]
        public async Task<IActionResult> GetConsultaRastreabilidade(
            [FromQuery] string? codProduto,
            [FromQuery] string? dataInicial,
            [FromQuery] string? dataFinal)
        {
            try
            {
                var resp = await _chradmservice.Consulta_RastreabilidadeAsync(codProduto, dataInicial, dataFinal);

                if (resp.Sucesso)
                    return Ok(resp.Dados);

                return BadRequest(resp.Erro ?? new ApiErrorInfo(-1, "Erro ao consultar rastreabilidade."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message, detail = ex.InnerException?.Message });
            }
        }
    }
}
