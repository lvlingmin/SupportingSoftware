using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace EBarv0._2.BLL
{
    class tbUser
    {
		private readonly DAL.tbUser dal = new DAL.tbUser();
		public tbUser()
		{ }
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
		public bool Exists(int UserID)
		{
			return dal.Exists(UserID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(Model.tbUser model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Model.tbUser model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int UserID)
		{

			return dal.Delete(UserID);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		//public bool DeleteList(string UserIDlist )
		//{
		//    return dal.DeleteList(Maticsoft.Common.PageValidate.SafeLongFilter(UserIDlist,0) );
		//}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Model.tbUser GetModel(int UserID)
		{

			return dal.GetModel(UserID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public Model.tbUser GetModelByCache(int UserID)
		{

			string CacheKey = "tbUserModel-" + UserID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(UserID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch { }
			}
			return (Model.tbUser)objModel;
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
		public List<Model.tbUser> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Model.tbUser> DataTableToList(DataTable dt)
		{
			List<Model.tbUser> modelList = new List<Model.tbUser>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Model.tbUser model;
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
