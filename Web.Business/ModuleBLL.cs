
using System;
using System.Collections.Generic;
using System.Linq;
using Web.Data;
using Web.Data.DataAccess;
using Web.Model;

namespace Web.Business
{
    public class ModuleBLL : BaseBLL
    {
        private ModuleDAL moduleDAL;
        private ModuleParamDAL moduleParamDAL;
        private ModuleConfigDAL moduleConfigDAL;
        private ModuleConfigParamDAL moduleConfigParamDAL;
        private ModuleConfigLanguageDAL moduleConfigLanguageDAL;
        private ItemDAL itemDAL;
        private IWebConfigDAL configDAL;

        public ModuleBLL(string connectionString = "")
            : base(connectionString)
        {
            moduleDAL = new ModuleDAL(this.DatabaseFactory);
            moduleParamDAL = new ModuleParamDAL(this.DatabaseFactory);
            moduleConfigDAL = new ModuleConfigDAL(this.DatabaseFactory);
            moduleConfigParamDAL = new ModuleConfigParamDAL(this.DatabaseFactory);
            moduleConfigLanguageDAL = new ModuleConfigLanguageDAL(this.DatabaseFactory);
            itemDAL = new ItemDAL(this.DatabaseFactory);
            configDAL = new WebConfigDAL(this.DatabaseFactory);
        }

        #region Module
        public IQueryable<ModuleModel> GetAllModues()
        {
            var modules = moduleDAL.GetAll()
                            .Select(e => new ModuleModel
                            {
                                ModuleName = e.ModuleName,
                                Summary = e.Summary,
                                Params = e.ModuleParams.Count
                            });
            return modules;
        }

        public void UpdateModule(ModuleModel model)
        {
            var module = moduleDAL.GetAll().FirstOrDefault(e => e.ModuleName == model.ModuleName);
            if (module == null) throw new BusinessException("Không tồn tại module");

            module.Summary = model.Summary;

            this.moduleDAL.Update(module);
            this.SaveChanges();
        }

        public void AddModule(ModuleModel model)
        {
            var module = moduleDAL.GetAll().FirstOrDefault(e => e.ModuleName == model.ModuleName);
            if (module != null) throw new BusinessException("Tên module đã tồn tại");

            module = new Module();
            module.ModuleName = model.ModuleName;
            module.Summary = model.Summary;

            this.moduleDAL.Add(module);
            this.SaveChanges();
        }

        public void RemoveModule(ModuleModel model)
        {
            var template = moduleDAL.GetAll().FirstOrDefault(e => e.ModuleName == model.ModuleName);
            if (template == null) throw new BusinessException("Không tồn tại module");

            this.moduleDAL.Delete(template);
            this.SaveChanges();
        }
        #endregion

        #region Param
        public IQueryable<ModuleParamModel> GetParamInModule(string moduleName = "")
        {
            var param = moduleParamDAL.GetAll()
                            .Select(e => new ModuleParamModel
                            {
                                ModuleName = e.ModuleName,
                                DefaultValue = e.DefaultValue,
                                ID = e.ParamName,
                                Summary = e.Summary,
                                Type = e.Type,
                                UIProperty = e.UIProperty
                            });
            if (!string.IsNullOrEmpty(moduleName)) param = param.Where(e => e.ModuleName == moduleName);
            return param;
        }

        public ModuleParamModel GetParam(string moduleName, string paramName)
        {
            var param = moduleParamDAL.GetAll()
                            .Where(e => e.ModuleName == moduleName && e.ParamName == paramName)
                            .Select(e => new ModuleParamModel
                            {
                                ModuleName = e.ModuleName,
                                DefaultValue = e.DefaultValue,
                                ID = e.ParamName,
                                Summary = e.Summary,
                                Type = e.Type,
                                UIProperty = e.UIProperty
                            });
            return param.FirstOrDefault();
        }

        public void UpdateParamModule(ModuleParamModel model)
        {
            var param = moduleParamDAL.GetAll().FirstOrDefault(e => e.ModuleName == model.ModuleName && e.ParamName == model.ID);
            if (param == null) throw new BusinessException(string.Format("Tham số [{0}] không tồn tại trên chức năng [{1}]", model.ID, model.ModuleName));
            
            param.Summary = model.Summary;
            param.Type = model.Type;
            param.DefaultValue = model.DefaultValue;
            param.UIProperty = model.UIProperty;

            this.moduleParamDAL.Update(param);
            this.SaveChanges();
        }

        public string AddParamModule(ModuleParamModel model)
        {
            var param = moduleParamDAL.GetAll().FirstOrDefault(e => e.ModuleName == model.ModuleName && e.ParamName == model.ID);
            if (param != null) throw new BusinessException(string.Format("Tham số [{0}] đã tồn tại trên chức năng [{1}]-[{2}]", model.ID, model.ModuleName));

            param = new ModuleParam();
            param.ModuleName = model.ModuleName;
            param.ParamName = model.ID;
            param.Type = model.Type;
            param.DefaultValue = model.DefaultValue;
            param.Summary = model.Summary;
            param.UIProperty = model.UIProperty;

            this.moduleParamDAL.Add(param);
            this.SaveChanges();
            return model.ID;
        }

        public void RemoveParamModule(ModuleParamModel model)
        {
            var position = moduleParamDAL.GetAll().FirstOrDefault(e => e.ModuleName == model.ModuleName && e.ParamName == model.ID);
            if (position == null) throw new BusinessException(string.Format("Tham số [{0}] không tồn tại trên chức năng [{1}]", model.ID, model.ModuleName));

            this.moduleParamDAL.Delete(position);
            this.SaveChanges();
        }
        #endregion

        #region Module Config
        public IQueryable<ModuleConfigModel> GetAllModuleConfigs(int companyId, string languageId = "vi-VN", string templateName = "", string componentName = "", string position = "", bool? inTemplate = null)
        {
            var query = moduleConfigDAL.GetAll()
                            .Where(e => e.Item.CompanyId == companyId)
                            .OrderBy(e => e.Item.Orders)
                            .Select(e => new ModuleConfigModel
                            {
                                Id = e.Id,
                                SkinName = e.SkinName,
                                TemplateName = e.TemplateName,
                                ComponentName = e.ComponentName,
                                InTemplate = e.InTemplate,
                                Position = e.Position,
                                ModuleName = e.ModuleName,
                                Orders = e.Item.Orders,
                                Publish = e.Item.IsPublished,
                                Params = e.ModuleConfigParams.Count
                            });

            if (!string.IsNullOrEmpty(templateName)) query = query.Where(e => e.TemplateName == templateName);
            if (!string.IsNullOrEmpty(componentName)) query = query.Where(e => e.ComponentName == componentName || e.ComponentName == "" || e.ComponentName == null);
            if (!string.IsNullOrEmpty(position)) query = query.Where(e => e.Position == position);
            if (inTemplate != null) query = query.Where(e => e.InTemplate == inTemplate);

            var data = query.ToList();

            var defaultLanguage = configDAL.GetAll().Where(e => e.Id == companyId)
                                                .Select(e => new { e.DefaultLanguage, e.DefaultLanguageIfNotSet })
                                                .FirstOrDefault();

            var langs = moduleConfigLanguageDAL.GetAll()
                            .Where(e => e.ModuleConfig.Item.CompanyId == companyId)
                            .Select(e => new ModuleConfigModel
                            {
                                LanguageId = e.LanguageId,
                                Id = e.ModuleId,
                                Title = e.Title,
                            }).ToList();


            foreach(var config in data)
            {
                var lang = langs.FirstOrDefault(e => e.Id == config.Id && e.LanguageId == languageId);
                if(lang != null)
                {
                    config.LanguageId = lang.LanguageId;
                    config.Id = lang.Id;
                    config.Title = lang.Title;
                }
                else if (defaultLanguage.DefaultLanguageIfNotSet)
                {
                    lang = langs.FirstOrDefault(e => e.Id == config.Id && e.LanguageId == defaultLanguage.DefaultLanguage);
                    if (lang == null) lang = langs.FirstOrDefault(e => e.Id == config.Id);
                    if (lang != null)
                    {
                        config.LanguageId = lang.LanguageId;
                        config.Id = lang.Id;
                        config.Title = lang.Title;
                    }
                }
                else
                {
                    config.LanguageId = languageId;
                    config.Title = "Chua co noi dung voi ngon ngu '" + languageId + "'";
                }

                config.Languages = string.Join(", ", langs.Where(e => e.Id == config.Id).Select(e => e.LanguageId));
            }
            
            return data.AsQueryable();
        }

        public ModuleConfigModel GetModuleConfig(int companyId, string language, int moduleId)
        {
            var module = moduleConfigDAL.GetAll()
                            .Where(e => e.Item.CompanyId == companyId && e.Id == moduleId)
                            .Select(e => new ModuleConfigModel
                            {
                                Id = e.Id,
                                SkinName = e.SkinName,
                                TemplateName = e.TemplateName,
                                ComponentName = e.ComponentName,
                                InTemplate = e.InTemplate,
                                Position = e.Position,
                                ModuleName = e.ModuleName,
                                Orders = e.Item.Orders,
                                Publish = e.Item.IsPublished
                            })
                            .FirstOrDefault();
            if (module == null) return null;
            var lang = moduleConfigLanguageDAL.GetAll().Where(e => e.ModuleId == moduleId && e.ModuleConfig.Item.CompanyId == companyId && e.LanguageId == language).FirstOrDefault();
            if (lang != null)
            {
                module.Title = lang.Title;
                module.LanguageId = lang.LanguageId;
            }
            else module.LanguageId = language;
            return module;
        }

        public int AddModuleConfig(int companyId, int user, ModuleConfigModel model, Dictionary<string, string> listParam)
        {
            var module = moduleConfigLanguageDAL.GetAll().FirstOrDefault(e => e.ModuleConfig.Item.CompanyId == companyId && e.ModuleId == model.Id);
            if (module != null) throw new BusinessException(string.Format("Module [{0}] đã tồn tại", model.Id));

            module = new ModuleConfigLanguage();
            module.LanguageId = model.LanguageId;
            module.Title = model.Title;
            module.ModuleConfig = new ModuleConfig();
            module.ModuleConfig.TemplateName = model.TemplateName;
            module.ModuleConfig.ComponentName = model.ComponentName;
            module.ModuleConfig.InTemplate = model.InTemplate;
            module.ModuleConfig.Position = model.Position;
            module.ModuleConfig.Item = new Item();
            module.ModuleConfig.Item.Orders = model.Orders;
            module.ModuleConfig.Item.IsPublished = model.Publish;
            module.ModuleConfig.Item.ModifyDate = DateTime.Now;
            module.ModuleConfig.Item.ModifyByUser = user;
            module.ModuleConfig.ModuleConfigParams = new List<ModuleConfigParam>();
            
            foreach (var p in listParam)
            {
                var pc = new ModuleConfigParam();
                pc.ParamName = p.Key;
                pc.Value = p.Value;

                module.ModuleConfig.ModuleConfigParams.Add(pc);
            }

            this.moduleConfigLanguageDAL.Add(module);
            this.SaveChanges();
            model.Id = module.ModuleId;
            return model.Id;
        }

        public void UpdateModuleConfig(int companyId, int user, ModuleConfigModel model, Dictionary<string, string> listParam)
        {
            var moduleLaguage = moduleConfigLanguageDAL.AllIncludes(e => e.ModuleConfig, e => e.ModuleConfig.Item, e => e.ModuleConfig.ModuleConfigParams).FirstOrDefault(e => e.ModuleConfig.Item.CompanyId == companyId && e.LanguageId == model.LanguageId && e.ModuleId == model.Id);
            if (moduleLaguage == null)
            {
                var module = this.moduleConfigDAL.GetAll().FirstOrDefault(e => e.Id == model.Id && e.Item.CompanyId == companyId);
                if (module == null) throw new BusinessException("Không tồn tại Module");
                else
                {
                    moduleLaguage = new ModuleConfigLanguage();
                    moduleLaguage.ModuleConfig = module;
                    moduleLaguage.LanguageId = model.LanguageId;
                    moduleConfigLanguageDAL.Add(moduleLaguage);
                }
            }
            else moduleConfigLanguageDAL.Update(moduleLaguage);
            
            moduleLaguage.Title = model.Title;
            moduleLaguage.ModuleConfig.TemplateName = model.TemplateName;
            moduleLaguage.ModuleConfig.ComponentName = model.ComponentName;
            moduleLaguage.ModuleConfig.InTemplate = model.InTemplate;
            moduleLaguage.ModuleConfig.Position = model.Position;
            moduleLaguage.ModuleConfig.Item.Orders = model.Orders;
            moduleLaguage.ModuleConfig.Item.IsPublished = model.Publish;
            moduleLaguage.ModuleConfig.Item.ModifyDate = DateTime.Now;
            moduleLaguage.ModuleConfig.Item.ModifyByUser = user;
            moduleLaguage.ModuleConfig.ModuleConfigParams.Clear();

            foreach (var p in listParam)
            {
                var pc = new ModuleConfigParam();
                pc.ParamName = p.Key;
                pc.Value = p.Value;

                moduleLaguage.ModuleConfig.ModuleConfigParams.Add(pc);
            }
            
            this.SaveChanges();
        }

        public int SaveModuleConfig(int companyId, int user, ModuleConfigModel model, IList<ModuleConfigParamModel> listParam)
        {
            var moduleLaguage = moduleConfigLanguageDAL.AllIncludes(e => e.ModuleConfig, e => e.ModuleConfig.Item, e => e.ModuleConfig.ModuleConfigParams).FirstOrDefault(e => e.ModuleConfig.Item.CompanyId == companyId && e.LanguageId == model.LanguageId && e.ModuleId == model.Id);
            try
            {
                if (moduleLaguage == null)
                {
                    var module = this.moduleConfigDAL.AllIncludes(e => e.Item, e => e.ModuleConfigParams).FirstOrDefault(e => e.Id == model.Id && e.Item.CompanyId == companyId);
                    if (module == null)
                    {
                        module = new ModuleConfig();
                        module.ModuleName = model.ModuleName;
                        module.Item = new Item();
                        module.Item.CompanyId = companyId;
                    }

                    moduleLaguage = new ModuleConfigLanguage();
                    moduleLaguage.LanguageId = model.LanguageId;
                    moduleLaguage.ModuleConfig = module;
                    moduleConfigLanguageDAL.Add(moduleLaguage);
                }
                else moduleConfigLanguageDAL.Update(moduleLaguage);

                moduleLaguage.Title = model.Title;
                moduleLaguage.ModuleConfig.TemplateName = model.TemplateName;
                moduleLaguage.ModuleConfig.ComponentName = model.ComponentName;
                moduleLaguage.ModuleConfig.InTemplate = model.InTemplate;
                moduleLaguage.ModuleConfig.Position = model.Position;
                moduleLaguage.ModuleConfig.SkinName = model.SkinName;
                moduleLaguage.ModuleConfig.Item.Orders = model.Orders;
                moduleLaguage.ModuleConfig.Item.IsPublished = model.Publish;
                moduleLaguage.ModuleConfig.Item.ModifyDate = DateTime.Now;
                moduleLaguage.ModuleConfig.Item.ModifyByUser = user;
                moduleLaguage.ModuleConfig.ModuleConfigParams.Clear();

                foreach (var p in listParam)
                {
                    var pc = new ModuleConfigParam();
                    pc.ParamName = p.ID;
                    pc.Value = p.Value;

                    moduleLaguage.ModuleConfig.ModuleConfigParams.Add(pc);
                }

                this.SaveChanges();
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
            }

            return moduleLaguage.ModuleId;
        }

        public void RemoveModuleConfig(int companyId, int moduleId)
        {
            var module = moduleConfigDAL.GetAll().FirstOrDefault(e => e.Item.CompanyId == companyId && e.Id == moduleId);
            if (module == null) throw new BusinessException(string.Format("Module [{0}] không tồn tại", moduleId));

            this.moduleConfigParamDAL.Delete(e => e.ModuleId == moduleId);
            this.moduleConfigDAL.Delete(module);
            this.itemDAL.Delete(e => e.Id == moduleId);
            this.SaveChanges();
        }
        #endregion

        #region Param Config
        public IQueryable<ModuleConfigParamModel> GetParamConfigs(int companyId, IList<int> moduleIds)
        {
            var paramConfig = moduleConfigParamDAL.GetAll()
                            .Where(e => e.ModuleConfig.Item.CompanyId == companyId)
                            .Select(e => new ModuleConfigParamModel
                            {
                                ModuleId = e.ModuleId,
                                Value = e.Value,
                                ID = e.ParamName,
                                Summary = e.ModuleConfig.ModuleName
                            });
            if (moduleIds.Count > 0) paramConfig = paramConfig.Where(e => moduleIds.Contains(e.ModuleId));
            var data = paramConfig.ToList();
            moduleIds = data.Select(e => e.ModuleId).ToList();
            var moduleNames = moduleConfigDAL.GetAll().Where(e => moduleIds.Contains(e.Id)).Select(e => e.ModuleName).ToList();
            var param = moduleParamDAL.GetAll()
                            .Where(e => moduleNames.Contains(e.ModuleName))
                            .Select(e => new { e.ModuleName, e.ParamName, e.UIProperty })
                            .ToList();

            
            foreach(var config in data)
            {
                var p = param.FirstOrDefault(e => e.ModuleName == config.Summary && e.ParamName == config.ID);
                if(p != null)
                {
                    config.UIProperty = p.UIProperty;
                }
            }
            
            return data.AsQueryable();
        }

        public IQueryable<ModuleConfigParamModel> GetParamConfig(string moduleName, int moduleId = 0)
        {
            var param = moduleParamDAL.GetAll()
                            .Where(e => e.ModuleName == moduleName)
                            .Select(e => new ModuleConfigParamModel
                            {
                                Value = e.DefaultValue,
                                ID = e.ParamName,
                                Type = e.Type,
                                Summary = e.Summary,
                                UIProperty = e.UIProperty,
                            }).ToList();

            if (moduleId > 0)
            {
                var paramConfig = moduleConfigParamDAL.GetAll()
                                    .Where(e => e.ModuleId == moduleId)
                                    .Select(e => new { e.ParamName, e.Value })
                                    .ToList();
                foreach(var p in param)
                {
                    var pc = paramConfig.FirstOrDefault(e => e.ParamName == p.ID);
                    if (pc != null) p.Value = pc.Value;
                }
            }
            
            return param.AsQueryable();
        }
        #endregion
    }
}
