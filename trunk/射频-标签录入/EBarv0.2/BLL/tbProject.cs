using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using LTP.Common;
using EBarv0._2.Model;
using EBarv0._2.DBUtility;
using System.Collections;
namespace EBarv0._2.BLL
{
    /// <summary>
    /// tbProject
    /// </summary>
    public partial class tbProject
    {
        private readonly DAL.tbProject dal = new DAL.tbProject();
        public tbProject()
        {
        }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return dal.GetMaxId();
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ProjectID)
        {
            return dal.Exists(ProjectID);
        }

        /// <summary>
        /// 是否存在该记录（ShrotName）
        /// </summary>
        public bool Exists_(string ShrotName)
        {
            return dal.Exists_(ShrotName);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Model.tbProject model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.tbProject model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int ProjectID)
        {

            return dal.Delete(ProjectID);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete_(string ShortName)
        {

            return dal.Delete_(ShortName);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        //public bool DeleteList(string ProjectIDlist )
        //{
        //    return dal.DeleteList(Maticsoft.Common.PageValidate.SafeLongFilter(ProjectIDlist,0) );
        //}

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.tbProject GetModel(int ProjectID)
        {

            return dal.GetModel(ProjectID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Model.tbProject GetModelByCache(int ProjectID)
        {

            string CacheKey = "tbProjectModel-" + ProjectID;
            object objModel = LTP.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(ProjectID);
                    if (objModel != null)
                    {
                        int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
                        LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Model.tbProject)objModel;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.tbProject> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.tbProject> DataTableToList(DataTable dt)
        {
            List<Model.tbProject> modelList = new List<Model.tbProject>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Model.tbProject model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = dal.DataRowToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            return dal.GetRecordCount(strWhere);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        #endregion  BasicMethod
        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}
