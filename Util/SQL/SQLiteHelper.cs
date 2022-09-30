using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util.SQL
{
    /// <summary>
    /// SQLite 帮助类
    /// </summary>
    public class SQLiteHelper
    {
        public SQLiteHelper() { }
        /// <summary>
        /// 实例化
        /// </summary>
        /// <param name="file">sql文件</param>
        public SQLiteHelper(string file)
        {
            Link(file);
        }


        #region 内部对象
        public bool Linked
        { 
            get => SQLiteConnection != null; 
        }
        public System.Data.SQLite.SQLiteConnection SQLiteConnection;
        #endregion

        #region 操作
        /// <summary>
        /// 执行输入的方法
        /// </summary>
        /// <param name="action"></param>
        public void Execute(Action<System.Data.SQLite.SQLiteConnection> action)
        {
            if (!Linked) return;
            action?.Invoke(SQLiteConnection);
        }
        /// <summary>
        /// 执行输入的查询命令, 返回结果的DataSet
        /// </summary>
        /// <param name="commond"></param>
        /// <returns></returns>
        public System.Data.DataSet SelectAsDataSet(string commond)
        {
            if (!Linked) return null;
            System.Data.DataSet ds = new System.Data.DataSet();
            System.Data.SQLite.SQLiteDataAdapter adapter
                = new System.Data.SQLite.SQLiteDataAdapter(commond, SQLiteConnection);
            adapter.Fill(ds);
            return ds;
        }
        /// <summary>
        /// 执行输入的查询命令, 返回结果的DataSet的第一个DataTable
        /// </summary>
        /// <param name="commond"></param>
        /// <returns></returns>
        public System.Data.DataTable SelectAsDataTable(string commond)
        {
            if (!Linked) return null;
            System.Data.DataSet ds = new System.Data.DataSet();
            System.Data.SQLite.SQLiteDataAdapter adapter
                = new System.Data.SQLite.SQLiteDataAdapter(commond, SQLiteConnection);
            adapter.Fill(ds);
            if (ds.Tables.Count == 0) return null;
            return ds.Tables[0];
        }
        #endregion

        #region 建立连接
        /// <summary>
        /// 建立连接
        /// </summary>
        /// <param name="file"></param>
        public void Link(string file)
        {
            Close();
            try
            {
                SQLiteConnection = new System.Data.SQLite.SQLiteConnection("Data Source=" + file);
                SQLiteConnection.Open();
            }
            catch
            {
                SQLiteConnection = null;
            }
        }

        #endregion

        #region 结束
        public void Close()
        {
            SQLiteConnection?.Close();
        }
        #endregion

    }
}
