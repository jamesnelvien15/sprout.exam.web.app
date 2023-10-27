using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Sprout.Exam.DataAccess.Helper
{
    public static class DapperHelperExt
    {
        public static async Task<T> QuerySingleOrDefaultAsync<T>(this IDbConnection connection, string sql, object? parameters = null)
        {
            return await connection.QuerySingleOrDefaultAsync<T>(sql, parameters);
        }

        public static async Task<List<T>> QueryAsync<T>(this IDbConnection connection, string sql, object? parameters = null)
        {
            var result = await connection.QueryAsync<T>(sql, parameters);
            return result.AsList();
        }

        public static Task<int> ExecuteAsync(this IDbConnection connection, string sql, object? parameters = null)
        {
            return connection.ExecuteAsync(sql, parameters);
        }

        public static async Task<T> ExecuteScalarAsync<T>(this IDbConnection connection, string sql, object? parameters = null)
        {
            return await connection.ExecuteScalarAsync<T>(sql, parameters, commandTimeout: 120);
        }
        public static async Task<T> ExecuteStoredProcedureSingleOrDefaultAsync<T>(this IDbConnection connection, string storedProcedureName, object? parameters = null, IDbTransaction? transaction = null)
        {
            return await connection.QuerySingleOrDefaultAsync<T>(storedProcedureName, parameters, commandType: CommandType.StoredProcedure, transaction: transaction, commandTimeout: 120);
        }
        public static async Task<List<T>> ExecuteStoredProcedureAsync<T>(this IDbConnection connection, string storedProcedureName, object? parameters = null, IDbTransaction? transaction = null)
        {
            var result = await connection.QueryAsync<T>(storedProcedureName, parameters, commandType: CommandType.StoredProcedure, transaction: transaction, commandTimeout: 120);
            return result.AsList();
        }
        public static Task<int> ExecuteStoredProcedureAsync(this IDbConnection connection, string storedProcedureName, object? parameters = null, IDbTransaction? transaction = null)
        {
            return connection.ExecuteAsync(storedProcedureName, parameters, commandType: CommandType.StoredProcedure, transaction: transaction, commandTimeout: 120);
        }
        public static async Task<GridReader> ExecuteQueryMultipleAsync(this IDbConnection connection, string storedProcedureName, object parameters = null, IDbTransaction transaction = null)
        {
            return await connection.QueryMultipleAsync(storedProcedureName, parameters, commandType: CommandType.StoredProcedure, transaction: transaction, commandTimeout: 120);
        }
    }
}
