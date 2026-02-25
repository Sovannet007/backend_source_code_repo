using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.Common;

namespace asp.net_api_teaching.Data
{
    public static class DataManager
    {
        // method for extract data set
        public static DataSet ExtractDataSet(DbContext db, string storeProcedure, params SqlParameter[] sqlPram)
        {
            DataSet dataSet = new DataSet();
            DbConnection connection = db.Database.GetDbConnection();
            DbProviderFactory dbFacetory = DbProviderFactories.GetFactory(connection)!;
            // create command
            using (DbCommand cmd = dbFacetory.CreateCommand()!)
            {
                cmd.Connection = connection;
                cmd.CommandText = storeProcedure;
                cmd.CommandType = CommandType.StoredProcedure;

                if (sqlPram != null && sqlPram.Length > 0)
                {
                    foreach (var item in sqlPram)
                    {
                        cmd.Parameters.Add(item);
                    }
                }

                using (DbDataAdapter adapter = dbFacetory.CreateDataAdapter()!)
                {
                    adapter.SelectCommand = cmd;
                    adapter.Fill(dataSet);
                }
            }
            return dataSet;
        }


        // method for extract data table to object list
        public static object ExtractDataTableToObjectList(DataTable dt)
        {
            var list = new List<Dictionary<string, object>>();
            foreach (DataRow dr in dt.Rows)
            {
                var dict = new Dictionary<string, object>();
                foreach (DataColumn cl in dt.Columns)
                {
                    dict[cl.ColumnName] = dr[cl.ColumnName];
                }
                // add dictionary to list
                list.Add(dict);
            }
            return list;
        }

        // method for call store procedure return datatable
        public static DataTable ExecuteSPReturnDt(DbContext db, string storeProcedure, params SqlParameter[] sqlPram)
        {
            DataTable dt = new DataTable();
            DbConnection connection = db.Database.GetDbConnection();
            DbProviderFactory dbFacetory = DbProviderFactories.GetFactory(connection)!;
            // create command
            using(DbCommand cmd = dbFacetory.CreateCommand()!)
            {
                cmd.Connection = connection;
                cmd.CommandText = storeProcedure;
                cmd.CommandType = CommandType.StoredProcedure;
                if (sqlPram != null && sqlPram.Length > 0)
                {
                    foreach (var item in sqlPram)
                    {
                        cmd.Parameters.Add(item);
                    }
                }
                using (DbDataAdapter adapter = dbFacetory.CreateDataAdapter()!)
                {
                    adapter.SelectCommand = cmd;
                    adapter.Fill(dt);
                }
            }
            return dt;
        }
    }
}

// table 
// id  | name     | remark
// 12  | dara     | Hello
// 13  | sok      | none

// ======================== extract data table to object list  ========================

// first loop 
// row 0 => 12  | dara | Hello
//  column 0 => id = 12
//  column 1 => name = dara
//  column 2 => remark = Hello

// list[0] => { id = 12, name = dara, remark = Hello }

// second loop
// row 1 => 13  | sok | none
//  column 0 => id = 13
//  column 1 => name = sok
//  column 2 => remark = none

// list[1] => { id = 13, name = sok, remark = none }