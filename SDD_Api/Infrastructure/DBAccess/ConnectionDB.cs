using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Data;

namespace SDD_Api.Infrastructure.DBAccess
{
    public class ConnectionDB
    {
        private readonly string _connectionString;

        public ConnectionDB(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("CHRIS_ORCL")
                ?? throw new InvalidOperationException("Connection string 'CHRIS_ORCL' não configurada em appsettings.json.");
        }

        public async Task ExecuteProcAsync(string procedureName, OracleParameter[] parameters)

        {

            using var conn = new OracleConnection(_connectionString);

            await conn.OpenAsync();

            using var initCmd = conn.CreateCommand();

            initCmd.CommandText = "ALTER SESSION SET NLS_DATE_FORMAT = 'DD/MM/YYYY' NLS_LANGUAGE = 'BRAZILIAN PORTUGUESE'";

            await initCmd.ExecuteNonQueryAsync();

            using var cmd = conn.CreateCommand();

            cmd.CommandText = procedureName;

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddRange(parameters);

            await cmd.ExecuteNonQueryAsync();

        }

        public async Task ExecuteProcWithOutputsAsync(

            string procedureName,

            OracleParameter[] parameters,

            Action<OracleParameter[]> processOutputs
            
            )

        {

            using var conn = new OracleConnection(_connectionString);

            await conn.OpenAsync();

            using var initCmd = conn.CreateCommand();

            initCmd.CommandText = "ALTER SESSION SET NLS_DATE_FORMAT = 'DD/MM/YYYY' NLS_LANGUAGE = 'BRAZILIAN PORTUGUESE'";

            await initCmd.ExecuteNonQueryAsync();

            using var cmd = conn.CreateCommand();

            cmd.CommandText = procedureName;

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddRange(parameters);

            await cmd.ExecuteNonQueryAsync();

            processOutputs(parameters);

        }
        public async Task ExecuteProcWithCursorAsync(

            string procedureName,

            OracleParameter[] parameters,

            Action<OracleDataReader> readAction
            
            )

        {

            using var conn = new OracleConnection(_connectionString);

            await conn.OpenAsync();

            using var initCmd = conn.CreateCommand();

            initCmd.CommandText = "ALTER SESSION SET NLS_DATE_FORMAT = 'DD/MM/YYYY' NLS_LANGUAGE = 'BRAZILIAN PORTUGUESE'";

            await initCmd.ExecuteNonQueryAsync();

            using var cmd = conn.CreateCommand();

            cmd.CommandText = procedureName;

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddRange(parameters);

            await cmd.ExecuteNonQueryAsync();


            var cursorParam = parameters.First(p => p.OracleDbType == OracleDbType.RefCursor);

            if (cursorParam.Value is OracleRefCursor cursor)

                using (var reader = cursor.GetDataReader())

                    readAction(reader);

        }

        public async Task ExecuteProcWithCursorAndOutputsAsync(
            string procedureName,
            OracleParameter[] parameters,
            Action<OracleDataReader> readAction,
            Action<OracleParameter[]> processOutputs)
        {
            using var conn = new OracleConnection(_connectionString);

            await conn.OpenAsync();

            using var initCmd = conn.CreateCommand();

            initCmd.CommandText = "ALTER SESSION SET NLS_DATE_FORMAT = 'DD/MM/YYYY' NLS_LANGUAGE = 'BRAZILIAN PORTUGUESE'";

            await initCmd.ExecuteNonQueryAsync();

            using var cmd = conn.CreateCommand();

            cmd.CommandText = procedureName;

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddRange(parameters);

            await cmd.ExecuteNonQueryAsync();

            processOutputs(parameters);

            var cursorParam = parameters.First(p => p.OracleDbType == OracleDbType.RefCursor);

            if (cursorParam.Value is OracleRefCursor cursor)

                using (var reader = cursor.GetDataReader())

                    readAction(reader);
        }

    }
    public static class OracleDbHelper
    {

        public static OracleParameter Input(string name, OracleDbType type, object? value) =>
            new(name, type) { Direction = ParameterDirection.Input, Value = value ?? DBNull.Value };


        public static OracleParameter Output(string name, OracleDbType type, int size = 0) =>
            size > 0

                ? new(name, type, size) { Direction = ParameterDirection.Output }

                : new(name, type) { Direction = ParameterDirection.Output };


        public static OracleParameter Cursor(string name) =>
            new(name, OracleDbType.RefCursor) { Direction = ParameterDirection.Output };


        public static int GetInt(OracleParameter p) =>
            p.Value is OracleDecimal d && !d.IsNull ? (int)d.Value : 0;


        public static decimal GetDecimal(OracleParameter p) =>
            p.Value is OracleDecimal d && !d.IsNull ? d.Value : 0m;


        public static string GetString(OracleParameter p) =>
            p.Value is OracleString s && !s.IsNull ? s.Value : "";


        public static DateTime? GetDate(OracleParameter p) =>
            p.Value is OracleDate d && !d.IsNull ? d.Value : null;
    }
}