using System;
using System.Collections.Generic;
using CD.ABM.Logic.POCO;
using System.Data.OleDb;

namespace CD.ABM.Logic.DALC
{
    class FormsDAL
    {
        String connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\CDDB.accdb";
        OleDbConnection conn = null;
        public FormsDAL ()
        {
            conn = new OleDbConnection(connString);
            conn.Open();
        }
        public List<String> GetBlocks (String formId)
        {
            string sqlCommand = "SELECT BlockId from TemplateBlockMap where TemplateId = " + formId;
            OleDbDataReader reader = getReader(sqlCommand);
            List<String> blocks = new List<string>();
            while (reader.Read())
            {
                String blockName = reader["BlockId"].ToString();
                blocks.Add(blockName);
            }
            return blocks;
        }

        private OleDbDataReader getReader(String sql)
        {
            OleDbCommand cmd = new OleDbCommand(sql, conn);
            return cmd.ExecuteReader();
        }

        private String getScalar(String sql)
        {
            OleDbCommand cmd = new OleDbCommand(sql, conn);
            return cmd.ExecuteScalar().ToString();
        }

        public List<BlockItem> getItems(string formId, string blockid)
        {

            List<BlockItem> items = new List<BlockItem>();
            OleDbDataReader reader = getReader("select * from qryQuestions where id =" + formId + " and blockid =" + blockid );
            while (reader.Read())
            {
                BlockItem item = new BlockItem();
                item.FormGenIdentifer = reader["FormGenId"].ToString();
                item.InputType = reader["inputtype"].ToString();
                item.Question = reader["question"].ToString();
                items.Add(item);
            }

            return items;


        }
    }
}
