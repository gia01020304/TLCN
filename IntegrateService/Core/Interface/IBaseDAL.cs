using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace General
{
    interface IBaseDAL
    {
        DataTable ExecuteQuery(string storeName, IDataParameter[] parameters = null);
        DataSet ExecuteQueryDataSet(string storeName, IDataParameter[] parameters = null);
        int ExecuteNonQuery(string storeName, IDataParameter[] parameters = null);
        SettingModel GetSetting(string key);
        int SaveSettingModel(SettingModel model);
    }
}
