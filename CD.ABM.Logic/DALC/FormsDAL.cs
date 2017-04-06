using System;
using System.Collections.Generic;
using CD.ABM.Logic.POCO;
using System.Data.OleDb;
using System.Configuration;

namespace CD.ABM.Logic.DALC
{
    class FormsDAL: IDisposable
    {
        String connString = ConfigurationManager.ConnectionStrings["FormsConn"].ToString();
        OleDbConnection conn = null;
        public FormsDAL ()
        {
            conn = new OleDbConnection(connString);
            conn.Open();
        }
        public List<String> GetBlocks (String formId)
        {
            string sqlCommand = "SELECT Id from TemplateBlockMap where TemplateId = " + formId;
            OleDbDataReader reader = getReader(sqlCommand);
            List<String> blocks = new List<string>();
            while (reader.Read())
            {
                String blockName = reader["Id"].ToString();
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

        public List<ItemRef> getItems(string formId, string blockid)
        {

            List<ItemRef> items = new List<ItemRef>();
            OleDbDataReader reader = getReader("select * from qryQuestions where id =" + formId + " and templateblockMapid =" + blockid );
            while (reader.Read())
            {
                ItemRef item = new ItemRef(
                    formsGenId: reader["FormGenId"].ToString(),
                    inputType: reader["inputtype"].ToString(),
                    question: reader["question"].ToString(),
                    uniqueId: reader["QUESTION_CODE"].ToString()
                )
                {
                    IsMandatory = (bool)reader["IS_MANDATORY_YN"]
                };
                items.Add(item);
            }

            return items;


        }

        public void Dispose()
        {
            if (conn != null) conn.Dispose();
        }
    }
}
