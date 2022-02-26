namespace Web.Asp.Provider.Cache
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    using Library.VirtualMemory;

    public class CacheProvider
    {
        public enum Keys
        {
            Com, // Company
            Cat, // Category
            CatType, // Type of Category
            Art, // Article
            ArtCount, // Count Article
            Pro, // Product
            ProType, // Type
            ProCount, // count product
            Fil, // File
            Doc, // Doculent
            Vid, // Video
            Aud, // Audio
            Col, // Color
            Sty, // Style
            ArtType, // Type
            Mem, // Member
            Man, // Manufacturer
            Mod, // Model
            Lin, // Link - logo - Image
            Onl, // Support online
            Obj, // Item
            ObjCount, // Count(Item)   
            Web, // webconfig
            War, // Ward
            Lan, // Language
            Spo, // Support Online
        }

        public static T GetCache<T>(Keys key, int companyId, params object[] listParam)
        {
            if (SettingsManager.AppSettings.IsTestEnviroment) return default(T);

            var cacheMap = HttpContext.Current.Application[SettingsManager.Constants.AllMapCache] as Dictionary<int, int>;
            if (cacheMap != null && cacheMap.ContainsKey(companyId))
            {
                var cacheIndex = cacheMap[companyId];
                var cacheData = HttpRuntime.Cache.Get(SettingsManager.Constants.AllDataCache) as List<CacheDictionary<string, object>>;
                if (cacheData != null && cacheData.Count > cacheIndex)
                {
                    var cache = cacheData[cacheIndex];
                    if (cache != null)
                    {
                        var param = string.Join(string.Empty, listParam);
                        var k = key + "|" + param;
                        if (cache.ContainsKey(k))
                        {
                            if (cache[k] is T)
                            {
                                return (T)cache[k];
                            }

                            try
                            {
                                return (T)Convert.ChangeType(cache[k], typeof(T));
                            }
                            catch (InvalidCastException)
                            {
                                return default(T);
                            }
                        }
                    }
                }
            }

            return default(T);
        }
        
        public static void SetCache<T>(T value, Keys key, int companyId, params object[] listParam)
        {
            var cacheMap = HttpContext.Current.Application[SettingsManager.Constants.AllMapCache] as Dictionary<int, int>;
            if (cacheMap == null)
            {
                cacheMap = new Dictionary<int, int>();
                HttpContext.Current.Application[SettingsManager.Constants.AllMapCache] = cacheMap;
            }

            var cacheData = HttpRuntime.Cache.Get(SettingsManager.Constants.AllDataCache) as List<CacheDictionary<string, object>>;
            if (cacheData == null)
            {
                cacheData = new List<CacheDictionary<string, object>>();
                HttpContext.Current.Cache.Insert(SettingsManager.Constants.AllDataCache, cacheData);
            }

            if (cacheMap.ContainsKey(companyId))
            {
                var cacheIndex = cacheMap[companyId];
                if (cacheData.Count > cacheIndex)
                {
                    var cache = cacheData[cacheIndex];
                    SetValueForCache(cache, value, key, listParam);
                }
                else
                {
                    // truong hop nay ko the xay ra
                    var cache = new CacheDictionary<string, object>();
                    SetValueForCache(cache, value, key, listParam);
                    cacheData.Add(cache);
                    cacheMap[companyId] = cacheData.Count - 1;
                }
            }
            else
            {
                var cache = new CacheDictionary<string, object>();
                SetValueForCache(cache, value, key, listParam);
                cacheData.Add(cache);
                cacheMap[companyId] = cacheData.Count - 1;
            }
        }

        public static void ClearCache(int companyId)
        {
            var cacheMap = HttpContext.Current.Application[SettingsManager.Constants.AllMapCache] as Dictionary<int, int>;
            if (cacheMap != null)
            {
                if (cacheMap.ContainsKey(companyId))
                {
                    var index = cacheMap[companyId];
                    var cacheData = HttpRuntime.Cache.Get(SettingsManager.Constants.AllDataCache) as List<CacheDictionary<string, object>>;
                    if (cacheData != null && cacheData.Count > index)
                    {
                        cacheData.RemoveAt(index);
                        cacheMap.Remove(companyId);

                        HttpContext.Current.Cache.Remove(SettingsManager.Constants.AllDataCache);

                        HttpContext.Current.Cache.Insert(SettingsManager.Constants.AllDataCache, cacheData);
                        HttpContext.Current.Application[SettingsManager.Constants.AllMapCache] = cacheMap;
                    }
                    else
                    {
                        cacheMap.Remove(companyId);
                        HttpContext.Current.Application[SettingsManager.Constants.AllMapCache] = cacheMap;
                    }
                }
            }
        }

        private static void SetValueForCache<T>(CacheDictionary<string, object> cache, T value, Keys key, params object[] listParam)
        {
            if (cache == null) cache = new CacheDictionary<string, object>();

            var k = key + "|";
            foreach (var o in listParam)
            {
                if (o != null)
                {
                    k += o.ToString();
                }
            }

            cache[k] = value;
        }
    }
}
