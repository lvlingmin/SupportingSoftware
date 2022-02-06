using System;
namespace EBarv0._2.Model
{
	/// <summary>
	/// tbMainScalCurve:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class tbMainScalCurve
	{
		public tbMainScalCurve()
		{ }
		#region Model
		private int _maincurveid;
		private string _itemname;
		private string _regentbatch;
		private string _points;
		private DateTime? _activedate;
		private DateTime? _validperiod;
		/// <summary>
		/// 索引
		/// </summary>
		public int MainCurveID
		{
			set { _maincurveid = value; }
			get { return _maincurveid; }
		}
		/// <summary>
		/// 项目名称
		/// </summary>
		public string ItemName
		{
			set { _itemname = value; }
			get { return _itemname; }
		}
		/// <summary>
		/// 试剂批号
		/// </summary>
		public string RegentBatch
		{
			set { _regentbatch = value; }
			get { return _regentbatch; }
		}
		/// <summary>
		/// 定标点
		/// </summary>
		public string Points
		{
			set { _points = value; }
			get { return _points; }
		}
		/// <summary>
		/// 激活日期
		/// </summary>
		public DateTime? ActiveDate
		{
			set { _activedate = value; }
			get { return _activedate; }
		}
		/// <summary>
		/// 有效期限
		/// </summary>
		public DateTime? ValidPeriod
		{
			set { _validperiod = value; }
			get { return _validperiod; }
		}
		#endregion Model

	}
}

