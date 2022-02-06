using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EBarv0._2.Model
{
    class tbUser
    {
		public tbUser()
		{ }
		#region Model
		private int _userid;
		private string _username;
		private string _userpassword;
		private int? _roletype;
		private int? _defaultvalue;
		/// <summary>
		/// 用户ID
		/// </summary>
		public int UserID
		{
			set { _userid = value; }
			get { return _userid; }
		}
		/// <summary>
		/// 用户姓名
		/// </summary>
		public string UserName
		{
			set { _username = value; }
			get { return _username; }
		}
		/// <summary>
		/// 密码
		/// </summary>
		public string UserPassword
		{
			set { _userpassword = value; }
			get { return _userpassword; }
		}
		/// <summary>
		/// 用户类型
		/// </summary>
		public int? RoleType
		{
			set { _roletype = value; }
			get { return _roletype; }
		}
		/// <summary>
		/// 默认值
		/// </summary>
		public int? defaultValue
		{
			set { _defaultvalue = value; }
			get { return _defaultvalue; }
		}
		#endregion Model
	}
}
