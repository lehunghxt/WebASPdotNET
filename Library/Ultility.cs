namespace Library
{
    using System;
    using System.Text;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Data.SqlClient;
    using System.Globalization;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    /// <summary>
    /// Class chua cac Extention method
    /// </summary>
    public static class Ultility
    {
        #region Table

        /// <summary>
        /// Convert a List{T} to a DataTable.
        /// </summary>
        public static DataTable ToDataTable<T>(this List<T> items)
        {
            var table = new DataTable(typeof(T).Name);

            var props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in props)
            {
                Type t = GetCoreType(prop.PropertyType);
                table.Columns.Add(prop.Name, t);
            }

            foreach (T item in items)
            {
                var values = new object[props.Length];

                for (var i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }

                table.Rows.Add(values);
            }

            return table;
        }

        public static string GetChildId(this DataTable table, string parentID, string columnParentName = "ParentId", string columnIdName = "")
        {
            var rows = table.Select(string.Format("{0} = '{1}'", columnParentName, parentID));    // lay tat ca con cua parentID
            if (rows.Length == 0) return "'" + parentID + "'"; // neu ko con -> tra ve id
            var result = new List<string>();
            foreach (DataRow row in rows)
            {
                var temp = GetChildId(table, columnIdName != "" ? row[columnIdName].ToString() : row[0].ToString(), columnParentName, columnIdName);
                //if (!string.IsNullOrEmpty(temp))
                result.Add(temp);
            }
            return string.Join(",", result.ToArray());
        }

        public static string GetAllChildId(this DataTable table, string parentID, string columnParentName = "ParentId", string columnIdName = "Id")
        {
            DataRow[] rows = table.Select(string.Format("{0} = '{1}'", columnParentName, parentID));    // lay tat ca con cua parentID
            if (rows.Length == 0) return string.Empty;
            var result = new List<string>();
            foreach (DataRow row in rows)
            {
                var temp = GetAllChildId(table, columnIdName != "" ? row[columnIdName].ToString() : row[0].ToString(), columnParentName, columnIdName);

                result.Add(row[columnIdName].ToString());
                if (temp.Length > 0)
                    result.Add(temp);
            }
            return string.Join(",", result.ToArray());
        }

        public static DataTable SortTable(this DataTable table, string parentId, string columnIdName = "", string columnChange = "", string columParentName = "ParentId", string columnOrder = "", string space = "", string distance = "....")
        {
            DataRow[] rows = table.Select(columParentName + " = '" + parentId + "'", (columnOrder != "") ? columnOrder + "," + columnChange : columnChange);
            if (rows.Length == 0) return null;
            DataTable sortData = table.Clone();
            foreach (DataRow row in rows)
            {
                var spaceNext = space + distance;
                DataTable dt = SortTable(table, columnIdName != "" ? row[columnIdName].ToString() : row[0].ToString(), columnIdName, columnChange, columParentName, columnOrder, spaceNext, distance);
                DataRow dr = sortData.NewRow();
                if (columnChange != "")
                {
                    dr.ItemArray = row.ItemArray;
                    dr[columnChange] = space + row[columnChange];
                }
                sortData.Rows.Add(dr);
                if (dt != null)
                    foreach (DataRow row2 in dt.Rows)
                        sortData.Rows.Add(row2.ItemArray);
            }
            return sortData;
        }
        #endregion

        #region string
        // EndCode
        public static string EnCode(this string text, int key = 0)
        {
            return Encrypt.EnCode(text, key);
        }
        public static string EnCodeMD5(this string text)
        {
            return Encrypt.EnCodeMD5(text);
        }
        public static string EnCodeBase64(this string text)
        {
            return Encrypt.StringToBase64(text);
        }

        // DeEnCode
        public static string DeEnCode(this string text, int key = 0)
        {
            return Encrypt.DeEnCode(text, key);
        }
        public static string DeEnCodeBase64(this string text)
        {
            return Encrypt.Base64ToString(text);
        }

        public static string ConvertToTitleCase(this string text)
        {
            if (string.IsNullOrEmpty(text)) return text;
            return new CultureInfo("en-US", false).TextInfo.ToTitleCase(text.ToLower());
        }
        #endregion

        #region String
        /// <summary>
        /// Hàm chuyển chuỗi định dạng ISO (&#7843;, ã &#7841;, ...) thành chuỗi Unicode
        /// </summary>
        /// <param name="strSource">Chuỗi cần lọc</param>
        /// <returns>Chuỗi mới đã lọc</returns>
        public static String ConvertISOToUnicode(this String strSource)
        {
            String strUni = "á à ả ã ạ Á À Ả Ã Ạ ă ắ ằ ẳ ẵ ặ Ă Ắ Ằ Ẳ Ẵ Ặ â ấ ầ ẩ ẫ ậ Â Ấ Ầ Ẩ Ẫ Ậ đ Đ é è ẻ ẽ ẹ É È Ẻ Ẽ Ẹ ê ế ề ể ễ ệ Ê Ế Ề Ể Ễ Ệ í ì ỉ ĩ ị Í Ì Ỉ Ĩ Ị ó ò ỏ õ ọ Ó Ò Ỏ Õ Ọ ô ố ồ ổ ỗ ộ Ô Ố Ồ Ổ Ỗ Ộ ơ ớ ờ ở ỡ ợ Ơ Ớ Ờ Ở Ỡ Ợ ú ù ủ ũ ụ Ú Ù Ủ Ũ Ụ ư ứ ừ ử ữ ự Ư Ứ Ừ Ử Ữ Ự ý ỳ ỷ ỹ ỵ Ý Ỳ Ỷ Ỹ Ỵ";
            String strISO = "á à &#7843; ã &#7841; Á À &#7842; Ã &#7840; &#259; &#7855; &#7857; &#7859; &#7861; &#7863; &#258; &#7854; &#7856; &#7858; &#7860; &#7862; â &#7845; &#7847; &#7849; &#7851; &#7853; Â &#7844; &#7846; &#7848; &#7850; &#7852; &#273; &#272; é è &#7867; "
                            + "&#7869; &#7865; É È &#7866; &#7868; &#7864; ê &#7871; &#7873; &#7875; &#7877; &#7879; Ê &#7870; &#7872; &#7874; &#7876; &#7878; í ì &#7881; &#297; &#7883; Í Ì &#7880; &#296; &#7882; ó ò &#7887; õ &#7885; Ó Ò &#7886; Õ &#7884; ô "
                            + "&#7889; &#7891; &#7893; &#7895; &#7897; Ô &#7888; &#7890; &#7892; &#7894; &#7896; &#417; &#7899; &#7901; &#7903; &#7905; &#7907; &#416; &#7898; &#7900; &#7902; &#7904; &#7906; ú ù &#7911; &#361; &#7909; Ú Ù &#7910; &#360; &#7908; &#432; &#7913; &#7915; &#7917; &#7919; &#7921; &#431; "
                            + "&#7912; &#7914; &#7916; &#7918; &#7920; ý &#7923; &#7927; &#7929; &#7925; Ý &#7922; &#7926; &#7928; &#7924;";

            String[] arrCharUni = strUni.Split(" ".ToCharArray());
            String[] arrCharISO = strISO.Split(" ".ToCharArray());

            String strResult = strSource;
            for (int i = 0; i < arrCharUni.Length; i++)
                strResult = strResult.Replace(arrCharISO[i], arrCharUni[i]);

            strUni = "À Á Â Ã Ä Å Æ Ç È É Ê Ë Ì Í Î Ï Ð Ñ Ò Ó Ô Õ Ö Ø Ù Ú Û Ü Ý Þ ß à á â ã ä å æ ç è é ê ë ì í î ï ð ñ ò ó ô õ ö ø ù ú û ü ý þ ÿ";
            strISO = "&#192; &#193; &#194; &#195; &#196; &#197; &#198; &#199; &#200; &#201; &#202; &#203; &#204; &#205; &#206; "
                + "&#207; &#208; &#209; &#210; &#211; &#212; &#213; &#214; &#216; &#217; &#218; &#219; &#220; &#221; &#222; "
                + "&#223; &#224; &#225; &#226; &#227; &#228; &#229; &#230; &#231; &#232; &#233; &#234; &#235; &#236; &#237; &#238; &#239; "
                + "&#240; &#241; &#242; &#243; &#244; &#245; &#246; &#248; &#249; &#250; &#251; &#252; &#253; &#254; &#255;";

            String[] arrCharUni1 = strUni.Split(" ".ToCharArray());
            String[] arrCharISO1 = strISO.Split(" ".ToCharArray());

            for (int i = 0; i < arrCharUni1.Length; i++)
                strResult = strResult.Replace(arrCharISO1[i], arrCharUni1[i]);

            strResult = strResult.Replace("\0", "");
            return strResult;
        }

        /// <summary>
        /// Hàm bỏ dấu tiếng việt của chuỗi định dạng Unicode 
        /// </summary>
        /// <param name="strSource">Chuỗi cần lọc</param>
        /// <returns>Chuỗi mới đã lọc</returns>
        public static String FilterVietkey(this String strSource)
        {
            strSource = ConvertISOToUnicode(strSource);
            if (strSource.Trim().Length == 0)
                return "";

            String strUni = "á à ả ã ạ Á À Ả Ã Ạ ă ắ ằ ẳ ẵ ặ Ă Ắ Ằ Ẳ Ẵ Ặ â ấ ầ ẩ ẫ ậ Â Ấ Ầ Ẩ Ẫ Ậ đ Đ é è ẻ ẽ ẹ É È Ẻ Ẽ Ẹ ê ế ề ể ễ ệ Ê Ế Ề Ể Ễ Ệ í ì ỉ ĩ ị Í Ì Ỉ Ĩ Ị ó ò ỏ õ ọ Ó Ò Ỏ Õ Ọ ô ố ồ ổ ỗ ộ Ô Ố Ồ Ổ Ỗ Ộ ơ ớ ờ ở ỡ ợ Ơ Ớ Ờ Ở Ỡ Ợ ú ù ủ ũ ụ Ú Ù Ủ Ũ Ụ ư ứ ừ ử ữ ự Ư Ứ Ừ Ử Ữ Ự ý ỳ ỷ ỹ ỵ Ý Ỳ Ỷ Ỹ Ỵ";
            String strASCI = "a a a a a A A A A A a a a a a a A A A A A A a a a a a a A A A A A A d d e e e e e E E E E E e e e e e e E E E E E E i i i i i I I I I I o o o o o O O O O O o o o o o o O O O O O O o o o o o o O O O O O O u u u u u U U U U U u u u u u u U U U U U U y y y y y Y Y Y Y Y";

            String[] arrCharUni = strUni.Split(" ".ToCharArray());
            String[] arrCharASCI = strASCI.Split(" ".ToCharArray());

            String strResult = strSource;
            for (int i = 0; i < arrCharUni.Length; i++)
                strResult = strResult.Replace(arrCharUni[i], arrCharASCI[i]);

            strUni = "À Á Â Ã Ä Å Æ Ç È É Ê Ë Ì Í Î Ï Ð Ñ Ò Ó Ô Õ Ö Ø Ù Ú Û Ü Ý Þ ß à á â ã ä å æ ç è é ê ë ì í î ï ð ñ ò ó ô õ ö ø ù ú û ü ý þ ÿ";
            strASCI = "A A A A A A Æ Ç E E E E I I I I D N O O O O O Ø U U U U Y Þ ß a a a a a a æ ç e e e e i i i i ð n o o o o o ø u u u u y þ y";

            String[] arrCharUni1 = strUni.Split(" ".ToCharArray());
            String[] arrCharASCI1 = strASCI.Split(" ".ToCharArray());

            for (int i = 0; i < arrCharUni1.Length; i++)
                strResult = strResult.Replace(arrCharUni1[i], arrCharASCI1[i]);

            strResult = strResult.Replace("\0", "");
            return strResult;
        }

        /// <summary>
        /// Hàm chuyển chuỗi VNI sang UNICODE
        /// </summary>
        /// <param name="vni">Chuỗi VNI</param>
        /// <returns></returns>
        public static String ConvertVNIToUnicodeLower(this String vni)
        {
            var text = vni.Replace("aù", "á").Replace("aø", "à").Replace("aû", "ả").Replace("aõ", "ã").Replace("aï", "ạ").Replace("aê", "ă").Replace("aé", "ắ").Replace("aè", "ằ").Replace("aú", "ẳ").Replace("aü", "ẵ").Replace("aë", "ặ").Replace("aâ", "â").Replace("aá", "ấ").Replace("aà", "ầ").Replace("aå", "ẩ").Replace("aã", "ẫ").Replace("aä", "ậ").Replace("où", "ó").Replace("oø", "ò").Replace("oû", "ỏ").Replace("oõ", "õ").Replace("oï", "ọ").Replace("oâ", "ô").Replace("oá", "ố").Replace("oà", "ồ").Replace("oå", "ổ").Replace("oã", "ỗ").Replace("oä", "ộ").Replace("ô", "ơ").Replace("ôù", "ớ").Replace("ôø", "ờ").Replace("ôû", "ở").Replace("ôõ", "ỡ").Replace("ôï", "ợ").Replace("uù", "ú").Replace("uø", "ù").Replace("uû", "ủ").Replace("uõ", "ũ").Replace("uï", "ụ").Replace("ö", "ư").Replace("öù", "ứ").Replace("öø", "ừ").Replace("öû", "ử").Replace("öõ", "ữ").Replace("öï", "ự").Replace("eù", "é").Replace("eø", "è").Replace("eû", "ẻ").Replace("eõ", "ẽ").Replace("eï", "ẹ").Replace("eâ", "ê").Replace("eá", "ế").Replace("eà", "ề").Replace("eå", "ể").Replace("eã", "ễ").Replace("eä", "ệ").Replace("yù", "ý").Replace("yø", "ỳ").Replace("yû", "ỷ").Replace("yõ", "ỹ").Replace("î", "ỵ").Replace("ñ", "đ").Replace("í", "í").Replace("ì", "ì").Replace("æ", "ỉ").Replace("ò", "ị").Replace("ưù", "ứ").Replace("ơø", "ơ").Replace("õu", "ũ");
            return text;
        }

        /// <summary>
        /// Hàm chuyển chuỗi VNI sang UNICODE
        /// </summary>
        /// <param name="vni">Chuỗi VNI</param>
        /// <returns></returns>
        public static String ConvertVNIToUnicode(this String vni)
        {
            var sUnicode = "";
            if (vni.Length == 0) return "";
            var iChieuDai = vni.Length - 1;
            var i = 0;
            while (i <= iChieuDai)
            {
                switch ((byte)vni[i])
                {
                    case 97: //, 101, 105, 111, 117 'a,e,i,o,u
                        if (i < iChieuDai)
                        {
                            switch ((byte)vni[i + 1])
                            {
                                case 249: sUnicode += "á"; i += 2; //dấu sắc
                                    break;
                                case 248: sUnicode += "à"; i += 2; //dấu huyền
                                    break;
                                case 251: sUnicode += "ả"; i += 2; //dấu hỏi
                                    break;
                                case 245: sUnicode += "ã"; i += 2; //dấu ngã
                                    break;
                                case 239: sUnicode += "ạ"; i += 2; //dấu nặng
                                    break;
                                case 226: sUnicode += "â"; i += 2; //^
                                    break;
                                case 225: sUnicode += "ấ"; i += 2; //ấ
                                    break;
                                case 224: sUnicode += "ầ"; i += 2; //ầ
                                    break;
                                case 229: sUnicode += "ẩ"; i += 2; //ẩ
                                    break;
                                case 227: sUnicode += "ẫ"; i += 2; //ẫ
                                    break;
                                case 228: sUnicode += "ậ"; i += 2; //ậ
                                    break;
                                case 234: sUnicode += "ă"; i += 2; //ă
                                    break;
                                case 233: sUnicode += "ắ"; i += 2; //ắ
                                    break;
                                case 232: sUnicode += "ằ"; i += 2; //ằ
                                    break;
                                case 250: sUnicode += "ẳ"; i += 2; //ẳ
                                    break;
                                case 252: sUnicode += "ẵ"; i += 2; //ẵ
                                    break;
                                case 235: sUnicode += "ặ"; i += 2; //ặ
                                    break;
                                default: sUnicode += "a"; i += 1;
                                    break;
                            }
                        }
                        else
                        {
                            sUnicode += "a"; i += 1;
                        }
                        break;
                    case 65: //A
                        if (i < iChieuDai)
                        {
                            switch ((byte)vni[i + 1])
                            {
                                case 217: sUnicode += "Á"; i += 2; //dấu sắc
                                    break;
                                case 216: sUnicode += "À"; i += 2; //dấu huyền
                                    break;
                                case 219: sUnicode += "Ả"; i += 2; //dấu hỏi
                                    break;
                                case 213: sUnicode += "Ã"; i += 2; //dấu ngã
                                    break;
                                case 207: sUnicode += "Ạ"; i += 2; //dấu nặng
                                    break;
                                case 194: sUnicode += "Â"; i += 2; //^
                                    break;
                                case 193: sUnicode += "Ấ"; i += 2; //ấ
                                    break;
                                case 192: sUnicode += "Ầ"; i += 2; //ầ
                                    break;
                                case 197: sUnicode += "Ẩ"; i += 2; //ẩ
                                    break;
                                case 195: sUnicode += "Ẫ"; i += 2; //ẫ
                                    break;
                                case 196: sUnicode += "Ậ"; i += 2; //ậ
                                    break;
                                case 202: sUnicode += "Ă"; i += 2; //ă
                                    break;
                                case 201: sUnicode += "Ắ"; i += 2; //ắ
                                    break;
                                case 200: sUnicode += "Ằ"; i += 2; //ằ
                                    break;
                                case 218: sUnicode += "Ẳ"; i += 2; //ẳ
                                    break;
                                case 220: sUnicode += "Ẵ"; i += 2; //ẵ
                                    break;
                                case 203: sUnicode += "Ặ"; i += 2; //ặ
                                    break;
                                //Trường hợp bị lỗi
                                case 249: sUnicode += "Á"; i += 2; //dấu sắc
                                    break;
                                case 248: sUnicode += "À"; i += 2; //dấu huyền
                                    break;
                                case 251: sUnicode += "Ả"; i += 2; //dấu hỏi
                                    break;
                                case 245: sUnicode += "Ã"; i += 2;  //dấu ngã
                                    break;
                                case 239: sUnicode += "Ạ"; i += 2; //dấu nặng
                                    break;
                                case 226: sUnicode += "Â"; i += 2; //^
                                    break;
                                case 225: sUnicode += "Ấ"; i += 2; //ấ
                                    break;
                                case 224: sUnicode += "Ầ"; i += 2; //ầ
                                    break;
                                case 229: sUnicode += "Ẩ"; i += 2; //ẩ
                                    break;
                                case 227: sUnicode += "Ẫ"; i += 2; //ẫ
                                    break;
                                case 228: sUnicode += "Ậ"; i += 2; //ậ
                                    break;
                                case 234: sUnicode += "Ă"; i += 2; //ă
                                    break;
                                case 233: sUnicode += "Ắ"; i += 2; //ắ
                                    break;
                                case 232: sUnicode += "Ằ"; i += 2; //ằ
                                    break;
                                case 250: sUnicode += "Ẳ"; i += 2; //ẳ
                                    break;
                                case 252: sUnicode += "Ẵ"; i += 2; //ẵ
                                    break;
                                case 235: sUnicode += "Ặ"; i += 2; //ặ
                                    break;
                                default: sUnicode += "A"; i += 1; break;
                            }
                        }
                        else { sUnicode += "A"; i += 1; } break;
                    case 101: //e
                        if (i < iChieuDai)
                        {
                            switch ((byte)vni[i + 1])
                            {
                                case 249: sUnicode += "é"; i += 2; //dấu sắc
                                    break;
                                case 248: sUnicode += "è"; i += 2; //dấu huyền
                                    break;
                                case 251: sUnicode += "ẻ"; i += 2; //dấu hỏi
                                    break;
                                case 245: sUnicode += "ẽ"; i += 2; //dấu ngã
                                    break;
                                case 239: sUnicode += "ẹ"; i += 2; //dấu nặng
                                    break;
                                case 226: sUnicode += "ê"; i += 2; //^
                                    break;
                                case 225: sUnicode += "ế"; i += 2; //ấ
                                    break;
                                case 224: sUnicode += "ề"; i += 2; //ầ
                                    break;
                                case 229: sUnicode += "ể"; i += 2; //ẩ
                                    break;
                                case 227: sUnicode += "ễ"; i += 2; //ẫ
                                    break;
                                case 228: sUnicode += "ệ"; i += 2; //ậ
                                    break;
                                default: sUnicode += "e"; i += 1;
                                    break;
                            }
                        }
                        else { sUnicode += "e"; i += 1; } break;
                    case 69:
                        //E
                        if (i < iChieuDai)
                        {
                            switch ((byte)vni[i + 1])
                            {
                                case 217: sUnicode += "É"; i += 2; //dấu sắc
                                    break;
                                case 216: sUnicode += "È"; i += 2; //dấu huyền
                                    break;
                                case 219: sUnicode += "Ẻ"; i += 2; //dấu hỏi
                                    break;
                                case 213: sUnicode += "Ẽ"; i += 2; //dấu ngã
                                    break;
                                case 207: sUnicode += "Ẹ"; i += 2; //dấu nặng
                                    break;
                                case 194: sUnicode += "Ê"; i += 2; //^
                                    break;
                                case 193: sUnicode += "Ế"; i += 2; //ấ
                                    break;
                                case 192: sUnicode += "Ề"; i += 2; //ầ
                                    break;
                                case 197: sUnicode += "Ể"; i += 2; //ẩ
                                    break;
                                case 195: sUnicode += "Ễ"; i += 2; //ẫ
                                    break;
                                case 196: sUnicode += "Ệ"; i += 2; //ậ
                                    break;
                                default: sUnicode += "E"; i += 1;
                                    break;
                            }
                        }
                        else { sUnicode += "E"; i += 1; } break;
                    case 111:
                        //o
                        if (i < iChieuDai)
                        {
                            switch ((byte)vni[i + 1])
                            {
                                case 249: sUnicode += "ó"; i += 2; //dấu sắc
                                    break;
                                case 248: sUnicode += "ò"; i += 2; //dấu huyền
                                    break;
                                case 251: sUnicode += "ỏ"; i += 2; //dấu hỏi
                                    break;
                                case 245: sUnicode += "õ"; i += 2; //dấu ngã
                                    break;
                                case 239: sUnicode += "ọ"; i += 2; //dấu nặng
                                    break;
                                case 226: sUnicode += "ô"; i += 2; //^
                                    break;
                                case 225: sUnicode += "ố"; i += 2; //ấ
                                    break;
                                case 224: sUnicode += "ồ"; i += 2; //ầ
                                    break;
                                case 229: sUnicode += "ổ"; i += 2; //ẩ
                                    break;
                                case 227: sUnicode += "ỗ"; i += 2; //ẫ
                                    break;
                                case 228: sUnicode += "ộ"; i += 2; //ậ
                                    break;
                                default: sUnicode += "o"; i += 1;
                                    break;
                            }
                        }
                        else { sUnicode += "o"; i += 1; } break;
                    case 79: //O
                        if (i < iChieuDai)
                        {
                            switch ((byte)vni[i + 1])
                            {
                                case 217: sUnicode += "Ó"; i += 2; //dấu sắc
                                    break;
                                case 216: sUnicode += "Ò"; i += 2; //dấu huyền
                                    break;
                                case 219: sUnicode += "Ỏ"; i += 2; //dấu hỏi
                                    break;
                                case 213: sUnicode += "Õ"; i += 2; //dấu ngã
                                    break;
                                case 207: sUnicode += "Ọ"; i += 2; //dấu nặng
                                    break;
                                case 194: sUnicode += "Ô"; i += 2; //^
                                    break;
                                case 193: sUnicode += "Ố"; i += 2; //ấ
                                    break;
                                case 192: sUnicode += "Ồ"; i += 2; //ầ
                                    break;
                                case 197: sUnicode += "Ổ"; i += 2; //ẩ
                                    break;
                                case 195: sUnicode += "Ỗ"; i += 2; //ẫ
                                    break;
                                case 196: sUnicode += "Ộ"; i += 2; //ậ
                                    break;
                                default: sUnicode += "O"; i += 1; break;
                            }
                        }
                        else { sUnicode += "O"; i += 1; } break;
                    case 117: //u
                        if (i < iChieuDai)
                        {
                            switch ((byte)vni[i + 1])
                            {
                                case 249: sUnicode += "ú"; i += 2; //dấu sắc
                                    break;
                                case 248: sUnicode += "ù"; i += 2; //dấu huyền
                                    break;
                                case 251: sUnicode += "ủ"; i += 2; //dấu hỏi
                                    break;
                                case 245: sUnicode += "ũ"; i += 2; //dấu ngã
                                    break;
                                case 239: sUnicode += "ụ"; i += 2; //dấu nặng
                                    break;
                                default: sUnicode += "u"; i += 1; break;
                            }
                        }
                        else { sUnicode += "u"; i += 1; } break;
                    case 85: //U
                        if (i < iChieuDai)
                        {
                            switch ((byte)vni[i + 1])
                            {
                                case 217: sUnicode += "Ú"; i += 2; //dấu sắc
                                    break;
                                case 216: sUnicode += "Ù"; i += 2; //dấu huyền
                                    break;
                                case 219: sUnicode += "Ủ"; i += 2; //dấu hỏi
                                    break;
                                case 213: sUnicode += "Ũ"; i += 2; //dấu ngã
                                    break;
                                case 207: sUnicode += "Ụ"; i += 2; //dấu nặng
                                    break;
                                default: sUnicode += "U"; i += 1; break;
                            }
                        }
                        else { sUnicode += "U"; i += 1; } break;
                    case 244: //ơ
                        if (i < iChieuDai)
                        {
                            switch ((byte)vni[i + 1])
                            {
                                case 249: sUnicode += "ớ"; i += 2; //dấu sắc
                                    break;
                                case 248: sUnicode += "ờ"; i += 2; //dấu huyền
                                    break;
                                case 251: sUnicode += "ở"; i += 2; //dấu hỏi
                                    break;
                                case 245: sUnicode += "ỡ"; i += 2; //dấu ngã
                                    break;
                                case 239: sUnicode += "ợ"; i += 2; //dấu nặng
                                    break;
                                default: sUnicode += "ơ"; i += 1; break;
                            }
                        }
                        else
                        {
                            sUnicode += "ơ";
                            i += 1;
                        }
                        break;
                    case 212: //Ơ
                        if (i < iChieuDai)
                        {
                            switch ((byte)vni[i + 1])
                            {
                                case 217: sUnicode += "Ớ"; i += 2; //dấu sắc
                                    break;
                                case 216: sUnicode += "Ờ"; i += 2; //dấu huyền
                                    break;
                                case 219: sUnicode += "Ở"; i += 2; //dấu hỏi
                                    break;
                                case 213: sUnicode += "Ỡ"; i += 2; //dấu ngã
                                    break;
                                case 207: sUnicode += "Ợ"; i += 2; //dấu nặng
                                    break;
                                default: sUnicode += "Ơ"; i += 1; break;
                            }
                        }
                        else { sUnicode += "Ơ"; i += 1; } break;
                    case 246: //ư
                        if (i < iChieuDai)
                        {
                            switch ((byte)vni[i + 1])
                            {
                                case 249: sUnicode += "ứ"; i += 2; //dấu sắc
                                    break;
                                case 248: sUnicode += "ừ"; i += 2; //dấu huyền
                                    break;
                                case 251: sUnicode += "ử"; i += 2; //dấu hỏi
                                    break;
                                case 245: sUnicode += "ữ"; i += 2; //dấu ngã
                                    break;
                                case 239: sUnicode += "ự"; i += 2; //dấu nặng
                                    break;
                                default: sUnicode += "ư"; i += 1;
                                    break;
                            }
                        }
                        else { sUnicode += "ư"; i += 1; } break;
                    case 214: //Ư
                        if (i < iChieuDai)
                        {
                            switch ((byte)vni[i + 1])
                            {
                                case 217: sUnicode += "Ứ"; i += 2; //dấu sắc
                                    break;
                                case 216: sUnicode += "Ừ"; i += 2; //dấu huyền
                                    break;
                                case 219: sUnicode += "Ử"; i += 2; //dấu hỏi
                                    break;
                                case 213: sUnicode += "Ữ"; i += 2; //dấu ngã
                                    break;
                                case 207: sUnicode += "Ự"; i += 2; //dấu nặng
                                    break;
                                default: sUnicode += "Ư"; i += 1; break;
                            }
                        }
                        else { sUnicode += "Ư"; i += 1; } break;
                    case 121: //y
                        if (i < iChieuDai)
                        {
                            switch ((byte)vni[i + 1])
                            {
                                case 249: sUnicode += "ý"; i += 2; //dấu sắc
                                    break;
                                case 248: sUnicode += "ỳ"; i += 2; //dấu huyền
                                    break;
                                case 251: sUnicode += "ỷ"; i += 2; //dấu hỏi
                                    break;
                                case 245: sUnicode += "ỹ"; i += 2; //dấu ngã
                                    break;
                                case 239: sUnicode += "ỵ"; i += 2; //dấu nặng
                                    break;
                                default: sUnicode += "y"; i += 1; break;
                            }
                        }
                        else { sUnicode += "y"; i += 1; }
                        break;
                    case 89: //Y
                        if (i < iChieuDai)
                        {
                            switch ((byte)vni[i + 1])
                            {
                                case 217: sUnicode += "Ý"; i += 2; //dấu sắc
                                    break;
                                case 216: sUnicode += "Ỳ"; i += 2; //dấu huyền
                                    break;
                                case 219: sUnicode += "Ỷ"; i += 2; //dấu hỏi
                                    break;
                                case 213: sUnicode += "Ỹ"; i += 2; //dấu ngã
                                    break;
                                case 207: sUnicode += "Ỵ"; i += 2; //dấu nặng
                                    break;
                                default: sUnicode += "Y"; i += 1; break;
                            }
                        }
                        else { sUnicode += "Y"; i += 1; } break;
                    case 237: sUnicode += "í"; i += 1; break;
                    case 236: sUnicode += "ì"; i += 1; break;
                    case 230: sUnicode += "ỉ"; i += 1; break;
                    case 243: sUnicode += "ĩ"; i += 1; break;
                    case 242: sUnicode += "ị"; i += 1; break;
                    case 205: sUnicode += "Í"; i += 1; break;
                    case 204: sUnicode += "Ì"; i += 1; break;
                    case 198: sUnicode += "Ỉ"; i += 1; break;
                    case 211: sUnicode += "Ĩ"; i += 1; break;
                    case 210: sUnicode += "Ị"; i += 1; break;
                    case 241: sUnicode += "đ"; i += 1; break;
                    case 209: sUnicode += "Đ"; i += 1; break;
                    case 238:
                    case 255: sUnicode += "ỵ"; i += 1; break;
                    case 159:
                    case 206: sUnicode += "Ỵ"; i += 1; break;
                    default:
                        sUnicode += vni[i]; i += 1; break;
                }
            }
            return sUnicode;
        }

        /// <summary>
        /// Hàm chuyển chuỗi có dấu thành không dấu
        /// </summary>
        /// <param name="text"> chuỗi cần chuyển</param>
        /// <returns>chuỗi không dấu</returns>
        public static String ConvertToUnSign(this String text)
        {
            if (text == null) return string.Empty;

            for (int i = 33; i < 48; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }
            for (int i = 58; i < 65; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }
            for (int i = 91; i < 97; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }
            for (int i = 123; i < 127; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }
            text = text.Replace(" ", "-");
            var regex = new System.Text.RegularExpressions.Regex(@"\p{IsCombiningDiacriticalMarks}+");
            string strFormD = text.Normalize(System.Text.NormalizationForm.FormD);
            return regex.Replace(strFormD, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }

        /// <summary>
        /// Hàm thực hiện lọc mã độc trong tham số SQL
        /// </summary>
        /// <param name="strParam">Chuỗi cần lọc</param>
        /// <returns>Chuỗi sau khi lọc</returns>
        public static String FilterSQLParamater(String strParam)
        {
            if (strParam == null)
                return "";

            String strTemp = strParam.Trim();

            try
            {
                strTemp = strTemp.Replace("'", "''");
                strTemp = strTemp.Replace(";", "");
                strTemp = strTemp.Replace("--", "");
                strTemp = strTemp.Replace("delete ", "");
                strTemp = strTemp.Replace("del ", "");
                strTemp = strTemp.Replace("set ", "");
                strTemp = strTemp.Replace("drop ", "");
                strTemp = strTemp.Replace("update ", "");
                strTemp = strTemp.Replace("select ", "");
                strTemp = strTemp.Replace("exec ", "");
                strTemp = strTemp.Replace("execute ", "");
                strTemp = strTemp.Replace("delete%", "");
                strTemp = strTemp.Replace("del%", "");
                strTemp = strTemp.Replace("set%", "");
                strTemp = strTemp.Replace("drop%", "");
                strTemp = strTemp.Replace("update%", "");
                strTemp = strTemp.Replace("select%", "");
                strTemp = strTemp.Replace("exec%", "");
                strTemp = strTemp.Replace("execute%", "");
            }
            catch (Exception) { }

            return strTemp;
        }
        #endregion

        #region DateTime
        public static DateTime AddDateWorking(this DateTime dateTime, int day)
        {
            int now = Convert.ToInt16(dateTime.DayOfWeek);
            var songuyen = day / 7;
            var sodu = day % 7;

            switch (sodu)
            {
                case 4: if (now > 0) sodu += 2; break;
                case 3: if (now > 1) sodu += 2; break;
                case 2: if (now > 2) sodu += 2; break;
                case 1: if (now > 3) sodu += 2; break;
                default: sodu += 2; break;
            }
            var ngay = songuyen * 7 + songuyen * 2 + sodu;
            return dateTime.AddDays(ngay);
        }
        #endregion

        #region IQueryable
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string property)
        {
            return ApplyOrder(source, property, "OrderBy");
        }
        public static IQueryable<T> OrderByDescending<T>(this IQueryable<T> source, string property)
        {
            return ApplyOrder(source, property, "OrderByDescending");
        }
        public static IQueryable<T> ThenBy<T>(this IOrderedQueryable<T> source, string property)
        {
            return ApplyOrder(source, property, "ThenBy");
        }
        public static IQueryable<T> ThenByDescending<T>(this IOrderedQueryable<T> source, string property)
        {
            return ApplyOrder(source, property, "ThenByDescending");
        }
        public static IQueryable<T> ApplyOrder<T>(IQueryable<T> source, string property, string methodName)
        {
            var props = property.Split('.');
            var type = typeof(T);
            var arg = Expression.Parameter(type, "x");
            Expression expr = arg;
            foreach (var prop in props)
            {
                // use reflection (not ComponentModel) to mirror LINQ
                PropertyInfo pi = type.GetProperty(prop);
                expr = Expression.Property(expr, pi);
                type = pi.PropertyType;
            }

            var delegateType = typeof(Func<,>).MakeGenericType(typeof(T), type);
            var lambda = Expression.Lambda(delegateType, expr, arg);

            var result = typeof(Queryable).GetMethods().Single(
                    method => method.Name == methodName
                            && method.IsGenericMethodDefinition
                            && method.GetGenericArguments().Length == 2
                            && method.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(T), type)
                    .Invoke(null, new object[] { source, lambda });
            return (IQueryable<T>)result;
        }
        #endregion

        #region IEnumerable
        public static DataTable ToDataTable<T>(this IEnumerable<T> items)
        {
            // Create the result table, and gather all properties of a T        
            var table = new DataTable(typeof(T).Name);
            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            // Add the properties as columns to the datatable
            foreach (var prop in props)
            {
                var propType = prop.PropertyType;

                // Is it a nullable type? Get the underlying type 
                if (propType.IsGenericType && propType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    propType = new NullableConverter(propType).UnderlyingType;

                table.Columns.Add(prop.Name, propType);
            }

            // Add the property values per T as rows to the datatable
            foreach (var item in items)
            {
                var values = new object[props.Length];
                for (var i = 0; i < props.Length; i++)
                    values[i] = props[i].GetValue(item, null);

                table.Rows.Add(values);
            }

            return table;
        }

        public static DataTable ToDataTable<T>(this IEnumerable items)
        {
            // Create the result table, and gather all properties of a T        
            var table = new DataTable(typeof(T).Name);
            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            // Add the properties as columns to the datatable
            foreach (var prop in props)
            {
                Type propType = prop.PropertyType;

                // Is it a nullable type? Get the underlying type 
                if (propType.IsGenericType && propType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    propType = new NullableConverter(propType).UnderlyingType;

                table.Columns.Add(prop.Name, propType);
            }

            // Add the property values per T as rows to the datatable
            foreach (var item in items)
            {
                var values = new object[props.Length];
                for (var i = 0; i < props.Length; i++)
                    values[i] = props[i].GetValue(item, null);

                table.Rows.Add(values);
            }

            return table;
        } 
        #endregion

        #region IList

        /// <summary>
        /// Sort List
        /// </summary>
        /// <param name="list">
        /// The list.
        /// </param>
        /// <param name="fieldName">
        /// The field name.
        /// </param>
        /// <param name="sortOrder">
        /// The sort order.
        /// </param>
        /// <typeparam name="T">
        /// The Object
        /// </typeparam>
        /// <returns>
        /// this List
        /// </returns>
        public static IList<T> Sort<T>(this IList<T> list, string fieldName = "", SortOrder sortOrder = SortOrder.Ascending)
        {
            if (!string.IsNullOrEmpty(fieldName))
                list = sortOrder == SortOrder.Ascending ? list.AsQueryable().OrderBy(fieldName).ToList() : list.AsQueryable().OrderByDescending(fieldName).ToList();

            return list;
        }

        public static List<T> GetRandomFromList<T>(this List<T> passedList, int numberToChoose)
        {
            System.Random rnd = new System.Random();
            List<T> chosenItems = new List<T>();

            for (int i = 1; i <= numberToChoose; i++)
            {
                int index = rnd.Next(passedList.Count);
                chosenItems.Add(passedList[index]);
            }

            //Debug.Log(chosenItems.Count);

            return chosenItems;
        }

        #endregion

        #region Exception
        /// <summary>
        /// Line separator.
        /// </summary>
        private const string Line = "==============================================================================";

        /// <summary>
        /// Returns the inner exception which is wrapped by the <see cref="TargetInvocationException"/> while preserving the stack trace.
        /// </summary>
        /// <param name="ex">The TargetInvocationException which is thrown.</param>
        /// <returns>The inner exception with correct stack trace.</returns>
        public static Exception Unwrap(this Exception ex)
        {
            while (ex is TargetInvocationException)
            {
                FieldInfo remoteStackTraceString =
                    typeof(Exception).GetField("_remoteStackTraceString", BindingFlags.Instance | BindingFlags.NonPublic);

                System.Diagnostics.Debug.Assert(remoteStackTraceString != null, "remoteStackTraceString != null");
                remoteStackTraceString.SetValue(ex.InnerException, ex.InnerException.StackTrace + Environment.NewLine);

                ex = ex.InnerException;
            }

            return ex;
        }

        /// <summary>
        /// Returns the first nested exception of the specified type, if any.
        /// </summary>
        /// <typeparam name="TException">The type of the nested exception to find.</typeparam>
        /// <param name="exception">The exception to inspect.</param>
        /// <returns>
        /// The nested exception, or <c>null</c> when not found.
        /// </returns>
        public static Exception FindNestedOfType<TException>(this Exception exception)
            where TException : Exception
        {
            Exception innerException = exception;
            while (innerException != null)
            {
                innerException = innerException.InnerException;
                if (innerException != null && innerException is TException)
                {
                    return innerException;
                }
            }

            return null;
        }

        /// <summary>
        /// Format all inner exceptions of <paramref name="exception"/>
        /// and return as single exception string.
        /// </summary>
        /// <param name="exception">
        /// The exception.
        /// </param>
        /// <returns>
        /// The formated exception string.
        /// </returns>
        public static string TraceInformation(this Exception exception)
        {
            if (exception == null)
            {
                return string.Empty;
            }

            var exceptionInformation = new StringBuilder();
            exceptionInformation.Append(BuildMessage(exception));
            var inner = exception.InnerException;

            while (inner != null)
            {
                exceptionInformation.Append(Environment.NewLine);
                exceptionInformation.Append(Environment.NewLine);
                exceptionInformation.Append(BuildMessage(inner));
                inner = inner.InnerException;
            }

            return exceptionInformation.ToString();
        }

        /// <summary>
        /// Build message from exception StackTrace and Message.
        /// </summary>
        /// <param name="exception">
        /// The exception.
        /// </param>
        /// <returns>
        /// The string that represent the exception.
        /// </returns>
        private static string BuildMessage(Exception exception)
        {
            return string.Format(
                System.Globalization.CultureInfo.InvariantCulture,
                "{0}{1}{2}:{3}{4}{5}{6}{7}",
                Line,
                Environment.NewLine,
                exception.GetType().Name,
                exception.Message,
                Environment.NewLine,
                exception.StackTrace,
                Environment.NewLine,
                Line);
        }
        #endregion


        #region
        /// <summary>
        /// Ham get value tu Dictionary, neu key khong ton tai thi tra ve default.
        /// </summary>
        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> data, TKey key)
        {
            TValue result;
            data.TryGetValue(key, out result);

            return result;
        }
        #endregion

        #region private method
        /// <summary>
        /// Determine of specified type is nullable
        /// </summary>
        /// <param name="t">
        /// The type
        /// </param>
        /// <returns>
        /// The is nullable.
        /// </returns>
        private static bool IsNullable(Type t)
        {
            return !t.IsValueType || (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>));
        }

        /// <summary>
        /// Return underlying type if type is Nullable otherwise return the type
        /// </summary>
        /// <param name="t">
        /// The type
        /// </param>
        /// <returns>
        /// The get core type.
        /// </returns>
        private static Type GetCoreType(Type t)
        {
            if (t != null && IsNullable(t))
            {
                return !t.IsValueType ? t : Nullable.GetUnderlyingType(t);
            }

            return t;
        }
        #endregion
    }
}
