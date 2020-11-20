using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace EBarv0._2
{
    /// <summary>
    /// 自建类，存储原始编码、中间编码、密文，主要用于建造泛型链表list
    /// </summary>
    //class Number//原始编码、中间编码、密文
    //{
        ///// <summary>
        ///// 原始编码
        ///// </summary>
        //readonly string oriNum;
        //public string OriNum { get { return oriNum; } }

        ///// <summary>
        ///// 中间编码
        ///// </summary>
        //readonly string midNum;
        //public string MidNum { get { return midNum; } }

        ///// <summary>
        ///// 密文
        ///// </summary>
        //readonly string encryNum;
        //public string EncryNum { get { return encryNum; } }

        ///// <summary>
        ///// 全属性构造方法
        ///// </summary>
        ///// <param name="oriNum">原始编码</param>
        ///// <param name="midNum">中间编码</param>
        ///// <param name="encryNum">密文</param>
        //public Number(string oriNum,string midNum,string encryNum)
        //{
        //    this.oriNum = oriNum;
        //    this.midNum = midNum;
        //    this.encryNum = encryNum;
        //}

        ///// <summary>
        ///// 全属性构造方法
        ///// </summary>
        ///// <param name="oriNum">原始编码</param>
        ///// <param name="encryNum">密文</param>
        //public Number(string oriNum, string encryNum)
        //{
        //    this.oriNum = oriNum;
        //    this.encryNum = encryNum;
        //}
    //}

    ///// <summary>
    ///// 一维码图片存储类，可存储原始编码、码制、一维码图片
    ///// </summary>
    //class BitPicture
    //{
    //    /// <summary>
    //    /// 原始编码
    //    /// </summary>
    //    readonly string oriNum;
    //    public string OriNum { get { return oriNum; } }

    //    /// <summary>
    //    /// 码制
    //    /// </summary>
    //    readonly string encodeStandard;
    //    public string EncodeStandard { get { return encodeStandard; } }

    //    /// <summary>
    //    /// 一维码图片
    //    /// </summary>
    //    readonly Bitmap bit;
    //    public Bitmap Bit{ get { return bit; } }

    //    /// <summary>
    //    /// 全属性构造方法
    //    /// </summary>
    //    /// <param name="oriNum">原始编码</param>
    //    /// <param name="encodeStandard">码制</param>
    //    /// <param name="bit">一维码图片</param>
    //    public BitPicture(string oriNum,string encodeStandard, Bitmap bit)
    //    {
    //        this.oriNum = oriNum;
    //        this.encodeStandard = encodeStandard;
    //        this.bit = bit;
    //    }
    //}

    class message   //试剂的信息
    {
        readonly string time;
        public string Time { get { return time; } }

        readonly string name;
        public string Nmae { get { return name; } }

        readonly string standard;
        public string Standard { get { return standard; } }

        readonly string batch;
        public string Batch { get { return batch; } }

        readonly string num;
        public string Num { get { return num; } }

        public message(string time, string name, string standard,string batch,string num)
        {
            this.time = time;
            this.name = name;
            this.standard = standard;
            this.batch = batch;
            this.num = num;
        }
    }
}
