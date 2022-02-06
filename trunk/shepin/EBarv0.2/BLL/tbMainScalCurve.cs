using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
namespace EBarv0._2.BLL
{
	/// <summary>
	/// tbMainScalCurve
	/// </summary>
	public partial class tbMainScalCurve
	{
		private readonly DAL.tbMainScalCurve dal = new DAL.tbMainScalCurve();
		public tbMainScalCurve()
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
		public bool Exists(int MainCurveID)
		{
			return dal.Exists(MainCurveID);
		}

		/// <summary>
		/// 是否存在主曲线
		/// </summary>
		public bool ExistsCurve(string ItemName, string RegentBatch)
		{
			return dal.ExistsCurve(ItemName, RegentBatch);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(Model.tbMainScalCurve model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 根据批号返回id lyq add 20190815
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		public int SelectIdAsRegentBatch(string rBatch)
		{
			return dal.SelectIdAsRegentBatch(rBatch);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Model.tbMainScalCurve model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int MainCurveID)
		{

			return dal.Delete(MainCurveID);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		//public bool DeleteList(string MainCurveIDlist )
		//{
		//    return dal.DeleteList(LTP.Common.PageValidate.SafeLongFilter(MainCurveIDlist, 0));
		//}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Model.tbMainScalCurve GetModel(int MainCurveID)
		{

			return dal.GetModel(MainCurveID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public Model.tbMainScalCurve GetModelByCache(int MainCurveID)
		{

			string CacheKey = "tbMainScalCurveModel-" + MainCurveID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(MainCurveID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch { }
			}
			return (Model.tbMainScalCurve)objModel;
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
		public List<Model.tbMainScalCurve> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Model.tbMainScalCurve> DataTableToList(DataTable dt)
		{
			List<Model.tbMainScalCurve> modelList = new List<Model.tbMainScalCurve>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Model.tbMainScalCurve model;
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

