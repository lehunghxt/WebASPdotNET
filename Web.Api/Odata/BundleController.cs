namespace Web.Api.Odata
{
    using System.Linq;
    using System.Web.Http.OData;
    using Web.Model;
    using Web.Business;
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class BundleController : OdataBaseController<string, string>
    {
        private ModuleBLL bll;
        public BundleController()
        {
            this.bll = new ModuleBLL();
        }
        [EnableQuery]
        public override IQueryable<string> Get()
        {
            var param = this.GetParameter();
            var template = this.Web.Template;
            if (param.ContainsKey("template")) template = param["template"];

            var root = AppDomain.CurrentDomain.BaseDirectory.Replace("\\", "/");
            var folder = root + "Templates/" + template; 
            var dir = new DirectoryInfo(folder);
            var files = dir.GetFiles().Select(e => "Templates/" + template + "/" + e.Name).ToList();
            var folders = dir.GetDirectories();
            foreach(var fol in folders)
            {
                var fileInFolder = fol.GetFiles().Select(e => "Templates/" + template + "/" + fol.Name + "/" + e.Name).ToList();
                files.AddRange(fileInFolder);
            }
            return files.AsQueryable();
        }
    }
}
