using FastMed.Integration.Infrastructure.IntegrationUtils.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace FastMed.Integration.Infrastructure.IntegrationUtils
{
    public class DataStructureCreator
    {
        private const string TableVariableName = "table_name";
        private const int Ordinal = 0;

        public static List<DataTableContent> Initialize<T>(string connection, string tableName)
        {
            var columns = new List<string>();
            using (var source = new SqlConnection(connection))
            {
                source.Open();
                using (var cmd = new SqlCommand(SqlStoredProcedures.GetTableMetadata, source))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(TableVariableName, tableName);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            columns.Add(reader.GetString(Ordinal));
                        }
                    }
                }
                source.Close();
            }
            var result = GenerateContent<T>(columns);
            return result;
        }

        private static List<DataTableContent> GenerateContent<T>(List<string> columns)
        {
            var result = new List<DataTableContent>();
            var properties = typeof(T).GetProperties();
            foreach (var column in columns)
            {
                var item = new DataTableContent
                {
                    Name = column
                };
                var type = properties.Where(x => x.Name == column).Select(s => s.PropertyType).FirstOrDefault();
                if (type == null)
                    continue;
                item.Type = type;
                result.Add(item);
            }
            return result;
        }
    }
}
