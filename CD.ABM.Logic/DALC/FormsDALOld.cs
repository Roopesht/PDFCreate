using System;
using System.Collections.Generic;
using DALC4NET;
using System.Data;
using CD.ABM.Logic.POCO;
using System.Data.OleDb;

namespace CD.ABM.Logic.DALC
{
    class FormsDALOld
    {
        String connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\CDDB.accdb";
        OleDbConnection conn = null;
        DBHelper db = null;
        public FormsDALOld ()
        {
            conn = new OleDbConnection(connString);
            db = new DBHelper("FormsConn");
        }
        public List<String> GetBlocks (String formId)
        {
            List<String> blocks = new List<string>();
            string sqlCommand = "SELECT * from TemplateBlockMap where TemplateId = " + formId;
            DataTable dt= db.ExecuteDataTable(sqlCommand);
            foreach(DataRow row in dt.Rows )
            {
                String blockName = row["BlockId"].ToString();
                blocks.Add(blockName);
            }
            return blocks;
        }

        public List<BlockItem> getItems(string formId, string blockid)
        {
            List<BlockItem> items = new List<BlockItem>();
            DataTable dt= db.ExecuteDataTable("select * from qryQuestions where id " + formId + " and blockname ='" + blockid + "'");
            foreach (DataRow row in dt.Rows)
            {
                BlockItem item = new BlockItem();
                item.FormGenIdentifer = row["FormGenId"].ToString();
                item.InputType = row["inputtype"].ToString();
                item.Question = row["question"].ToString();
                items.Add(item);
            }

            return items;


        }
    }
}
