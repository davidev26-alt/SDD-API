using Oracle.ManagedDataAccess.Client;
using SDD_Api.Contracts;
using SDD_Api.Infrastructure.DBAccess;
using SDD_Api.Models;

namespace SDD_Api.Infrastructure.Procedures
{
    public class chradm_001_pkg
    {
        private const string PKG = "USERADM.CHRADM_001_PKG";

        private readonly ConnectionDB _db;

        public chradm_001_pkg(ConnectionDB db) => _db = db ?? throw new ArgumentNullException(nameof(db));

        public async Task<ApiResponse<List<chradm_001_model.Consulta_Produto>>> Consulta_ProdutoAsync(string? pCodProduto)
        {
            var parameters = new[]
            {
                OracleDbHelper.Input ("P_Cod_Produto", OracleDbType.Varchar2, pCodProduto),
                OracleDbHelper.Cursor("P_Cursor"),
                OracleDbHelper.Output("P_Erro_Num",    OracleDbType.Decimal),
                OracleDbHelper.Output("P_Erro_Des",    OracleDbType.Varchar2, 500)
            };

            var lista   = new List<chradm_001_model.Consulta_Produto>();
            var erroNum = 0;
            var erroDes = "";

            await _db.ExecuteProcWithCursorAndOutputsAsync($"{PKG}.CONSULTA_PRODUTO", parameters,
                reader =>
                {
                    int colCodigo    = reader.GetOrdinal("CODIGO");
                    int colDescricao = reader.GetOrdinal("DESCRICAO");

                    while (reader.Read())
                    {
                        chradm_001_model.Consulta_Produto lt_produto = new chradm_001_model.Consulta_Produto();
                        {
                            lt_produto.Codigo = reader.IsDBNull(colCodigo) ? "" : reader.GetString(colCodigo);
                            lt_produto.Descricao = reader.IsDBNull(colDescricao) ? "" : reader.GetString(colDescricao);

                            lista.Add(lt_produto);
                        }
                    }
                },
                outputs =>
                {
                    erroNum = OracleDbHelper.GetInt   (outputs[2]);
                    erroDes = OracleDbHelper.GetString(outputs[3]);
                });

            if (erroNum != 0)
                return ApiResponse<List<chradm_001_model.Consulta_Produto>>.FromError(erroNum, erroDes);

            return ApiResponse<List<chradm_001_model.Consulta_Produto>>.Ok(lista);
        }

        public async Task<ApiResponse<chradm_001_model.Valida_Rastreabilidade>> Valida_RastreabilidadeAsync(string? pCodProduto, string? pNrRastrea)
        {
            var parameters = new[]
            {
                OracleDbHelper.Input ("P_Cod_Produto", OracleDbType.Varchar2, pCodProduto),
                OracleDbHelper.Input ("P_Nr_Rastrea",  OracleDbType.Varchar2, pNrRastrea),
                OracleDbHelper.Output("P_Data",        OracleDbType.Date),
                OracleDbHelper.Output("P_Erro_Num",    OracleDbType.Decimal),
                OracleDbHelper.Output("P_Erro_Des",    OracleDbType.Varchar2, 500)
            };

            var erroNum = 0;
            var erroDes = "";
            var result  = new chradm_001_model.Valida_Rastreabilidade();

            await _db.ExecuteProcWithOutputsAsync($"{PKG}.VALIDA_RASTREABILIDADE", parameters, outputs =>
            {
                result.Data = OracleDbHelper.GetDate(outputs[2]);
                erroNum     = OracleDbHelper.GetInt   (outputs[3]);
                erroDes     = OracleDbHelper.GetString(outputs[4]);
            });

            if (erroNum != 0)
                return ApiResponse<chradm_001_model.Valida_Rastreabilidade>.FromError(erroNum, erroDes);

            return ApiResponse<chradm_001_model.Valida_Rastreabilidade>.Ok(result);
        }

        public async Task<ApiResponse<List<chradm_001_model.Consulta_Rastreabilidade>>> Consulta_RastreabilidadeAsync(
            string? pCodProduto,
            string? pDataInicial,
            string? pDataFinal)
        {
            var parameters = new[]
            {
                OracleDbHelper.Input ("P_Cod_Produto",  OracleDbType.Varchar2, pCodProduto),
                OracleDbHelper.Input ("P_Data_Inicial", OracleDbType.Varchar2, pDataInicial),
                OracleDbHelper.Input ("P_Data_Final",   OracleDbType.Varchar2, pDataFinal),
                OracleDbHelper.Cursor("P_Cursor"),
                OracleDbHelper.Output("P_Erro_Num",     OracleDbType.Decimal),
                OracleDbHelper.Output("P_Erro_Des",     OracleDbType.Varchar2, 500)
            };

            var lista   = new List<chradm_001_model.Consulta_Rastreabilidade>();
            var erroNum = 0;
            var erroDes = "";

            await _db.ExecuteProcWithCursorAndOutputsAsync($"{PKG}.CONSULTA_RASTREABILIDADE", parameters,
                reader =>
                {
                    int colLote      = reader.GetOrdinal("LOTE");
                    int colDhRastrea = reader.GetOrdinal("DH_RASTREA");

                    while (reader.Read())
                    {
                        chradm_001_model.Consulta_Rastreabilidade lt_rastrea = new chradm_001_model.Consulta_Rastreabilidade();
                        {
                            lt_rastrea.Lote = reader.IsDBNull(colLote) ? "" : reader.GetString(colLote);
                            lt_rastrea.Dh_Rastrea = reader.IsDBNull(colDhRastrea) ? null : reader.GetDateTime(colDhRastrea);

                             lista.Add(lt_rastrea);
                        }
                    }
                },
                outputs =>
                {
                    erroNum = OracleDbHelper.GetInt   (outputs[4]);
                    erroDes = OracleDbHelper.GetString(outputs[5]);
                });

            if (erroNum != 0)
                return ApiResponse<List<chradm_001_model.Consulta_Rastreabilidade>>.FromError(erroNum, erroDes);

            return ApiResponse<List<chradm_001_model.Consulta_Rastreabilidade>>.Ok(lista);
        }
    }
}
