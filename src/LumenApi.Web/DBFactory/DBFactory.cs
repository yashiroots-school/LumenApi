using System.Data;
using System.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

public class DBFactory
{
  private static string? _connectionString;

  // Static constructor to initialize _connectionString
  static DBFactory()
  {
    var configuration = new ConfigurationBuilder()
        .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
        .AddJsonFile("appsettings.json")
        .Build();

    _connectionString = configuration.GetConnectionString("DefaultConnection");
  }

  public static IDbConnection CreateConnection()
  {
    return new SqlConnection(_connectionString);
  }

  // Your existing methods remain unchanged
  public async static Task<List<T>> SelectCommand_SP<T>(string SPName)
  {
    using (IDbConnection con = CreateConnection())
    {
      con.Open();
      IEnumerable<T> result = await con.QueryAsync<T>(SPName, commandType: CommandType.StoredProcedure);
      return result.ToList();
    }
  }

  public async static Task<List<T>> SelectCommand_SP<T>(List<T> objlst,string SPName, DynamicParameters ObjParameters)
  {
    using (IDbConnection con = CreateConnection())
    {
     con.Open();
      IEnumerable<T> result = await con.QueryAsync<T>(SPName, ObjParameters, commandType: CommandType.StoredProcedure);
      return result.ToList();
    }
  }

  public async static Task<T?> SelectCommand_SP<T>(T objmodel,string SPName, DynamicParameters ObjParameters)
  {
    using (IDbConnection con = CreateConnection())
    {
      con.Open();
      T? objModel = await con.QueryFirstOrDefaultAsync<T>(SPName, ObjParameters, commandType: CommandType.StoredProcedure);
      return objModel;
    }
  }

  public async static Task<string> Execute_SP(string SPName, DynamicParameters ObjParameters)
  {
    using (IDbConnection con = CreateConnection())
    {
     con.Open();
      int result = await con.ExecuteAsync(SPName, ObjParameters, commandType: CommandType.StoredProcedure);
      return Convert.ToString(result);
    }
  }

  public async static Task<string?> ExecuteScaler_SP(string SPName, DynamicParameters ObjParameters)
  {
    using (IDbConnection con = CreateConnection())
    {
     con.Open();
      string? result = await con.ExecuteScalarAsync<string>(SPName, ObjParameters, commandType: CommandType.StoredProcedure);
      return result;
    }
  }

  public async static Task<string?> ExecuteScaler_SP(string Query)
  {
    using (IDbConnection con = CreateConnection())
    {
     con.Open();
      string? result = await con.ExecuteScalarAsync<string>(Query, commandType: CommandType.Text);
      return result;
    }
  }
}
