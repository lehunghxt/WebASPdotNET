using System.Collections.Generic;

namespace Web.Asp.ObjectData
{
    using System;

    [Serializable]
    public class ModuleData
    {
        public ModuleData()
        {
            PathSkins = new Dictionary<string, string[]>();
            ListParams = new Dictionary<string, IList<IList<ModuleParamData>>>();
            Titles = new Dictionary<string, string[]>();
            Skins = new Dictionary<string, string[]>();
            Ids = new Dictionary<string, int[]>();
        }

        public IList<string> Positions { get; set; }
        public IDictionary<string, string[]> PathSkins { get; private set; }
        public IDictionary<string, IList<IList<ModuleParamData>>> ListParams { get; private set; }
        public IDictionary<string, string[]> Titles { get; private set; }
        public IDictionary<string, string[]> Skins { get; private set; }
        public IDictionary<string, int[]> Ids { get; private set; }
    }
}
