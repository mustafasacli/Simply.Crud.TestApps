using System;
namespace BookmarksStocker.Source.Util
{
    public static class ObjectUtil
    {
        public static bool IsNullOrSpace(this string str)
        {
            bool result = string.Format("{0}", str).Replace(" ", "").Length == 0;
            return result;
        }

        public static bool IsNull(this object o)
        {
            return o == null;
        }

        public static bool IsNullOrDbNull(this object obj)
        {
            return (null == obj | obj == DBNull.Value);
        }

        public static string ToStr(this object obj)
        {
            return IsNullOrDbNull(obj) == true ? string.Empty : obj.ToString();
        }

        public static bool IsNullOrEmpty(this string str)
        {
            if (str == null)
            {
                return true;
            }
            else
            {
                return str.Length == 0;
            }
        }

        public static long ToLong(this object obj)
        {
            long l = 0;

            try
            {
                long.TryParse(string.Format("{0}", obj), out l);
            }
            catch (Exception)
            {
            }

            return l;
        }

        public static DateTime ToDateTime(this object obj)
        {
            DateTime d = new DateTime();

            try
            {
                DateTime.TryParse(string.Format("{0}", obj), out d);
            }
            catch (Exception)
            {
            }

            return d;
        }

        public static bool ToBool(this object obj)
        {
            bool b = false;

            try
            {
                bool.TryParse(string.Format("{0}", obj), out b);
            }
            catch (Exception)
            {
            }

            return b;
        }

        //
        public static double ToDouble(this object obj)
        {
            double d = 0;

            try
            {
                double.TryParse(string.Format("{0}", obj), out d);
            }
            catch (Exception)
            {
            }

            return d;
        }

        //
        public static int ToInt(this object obj)
        {
            int i = 0;

            try
            {
                int.TryParse(string.Format("{0}", obj), out i);
            }
            catch (Exception)
            {
            }

            return i;
        }
    }
}