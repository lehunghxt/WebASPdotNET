
using System.Linq;
using Web.Data;
using Web.Data.DataAccess;
using Web.Model;

namespace Web.Business
{
    public class TemplateBLL : BaseBLL
    {
        private TemplateDAL templateDAL;
        private TemplatePositionDAL templatePositionDAL;
        private TemplateSkinDAL templateSkinDAL;
        private TemplateComponentDAL templateComponentDAL;
        private TemplateComponentPositionDAL templateComponentPositionDAL;
        private CompanyDomainDAL domainDAL;
        private WebConfigDAL webConfigDAL;

        public TemplateBLL(string connectionString = "")
            : base(connectionString)
        {
            templateDAL = new TemplateDAL(this.DatabaseFactory);
            templatePositionDAL = new TemplatePositionDAL(this.DatabaseFactory);
            templateSkinDAL = new TemplateSkinDAL(this.DatabaseFactory);
            templateComponentDAL = new TemplateComponentDAL(this.DatabaseFactory);
            templateComponentPositionDAL = new TemplateComponentPositionDAL(this.DatabaseFactory);
            domainDAL = new CompanyDomainDAL(this.DatabaseFactory);
            webConfigDAL = new WebConfigDAL(this.DatabaseFactory);
        }
        
        #region Template
        public IQueryable<TemplateModel> GetAllTemplates()
        {
            var templates = templateDAL.GetAll()
                            .Select(e => new TemplateModel
                            {
                                TemplateName = e.TemplateName,
                                ImageName = e.ImageName,
                                IsPublic = e.IsPublic,
                                IsPublished = e.IsPublished,
                                Components = e.TemplateComponents.Count,
                                Positions = e.TemplatePositions.Count,
                                Skins = e.TemplateSkins.Count,
                                Usings = e.WebConfigs.Count,
                            });
            return templates;
        }

        public TemplateModel GetAllTemplate()
        {
            var templates = templateDAL.GetAll()
                            .Select(e => new TemplateModel
                            {
                                TemplateName = e.TemplateName,
                                ImageName = e.ImageName,
                                IsPublic = e.IsPublic,
                                IsPublished = e.IsPublished,
                                Components = e.TemplateComponents.Count,
                                Positions = e.TemplatePositions.Count,
                                Skins = e.TemplateSkins.Count,
                            });
            return templates.FirstOrDefault();
        }

        public void UpdateTemplate(TemplateModel model)
        {
            var template = templateDAL.GetAll().FirstOrDefault(e => e.TemplateName == model.TemplateName);
            if (template == null) throw new BusinessException("Không tồn tại giao diện");
            
            template.ImageName = model.ImageName;
            template.IsPublic = model.IsPublic;
            template.IsPublished = model.IsPublished;

            this.templateDAL.Update(template);
            this.SaveChanges();
        }

        public void AddTemplate(TemplateModel model)
        {
            var template = templateDAL.GetAll().FirstOrDefault(e => e.TemplateName == model.TemplateName);
            if (template != null) throw new BusinessException("Tên giao diện đã tồn tại");

            template = new Template();
            template.TemplateName = model.TemplateName;
            template.ImageName = model.ImageName;
            template.IsPublic = model.IsPublic;
            template.IsPublished = model.IsPublished;

            this.templateDAL.Add(template);
            this.SaveChanges();
        }

        public void RemoveTemplate(TemplateModel model)
        {
            var template = templateDAL.GetAll().FirstOrDefault(e => e.TemplateName == model.TemplateName);
            if (template == null) throw new BusinessException("Không tồn tại giao diện");
            var config = webConfigDAL.GetAll().Any(e => e.Template == model.TemplateName);
            if (config) throw new BusinessException("Giao diện đang được sử dụng");

            this.templateDAL.Delete(template);
            this.SaveChanges();
        }
        #endregion

        #region Position
        public IQueryable<TemplatePositionModel> GetAllPositionTemplates(string templateName = "")
        {
            var positions = templatePositionDAL.GetAll()
                            .Select(e => new TemplatePositionModel
                            {
                                ID = e.PositionName,
                                TemplateName = e.TemplateName,
                                Summary = e.Summary
                            });
            if (!string.IsNullOrEmpty(templateName)) positions = positions.Where(e => e.TemplateName == templateName);
            return positions;
        }

        public IQueryable<TemplateComponentPositionModel> GetAllPositionComponents(string templateName = "", string componentName = "")
        {
            var positions = templateComponentPositionDAL.GetAll()
                            .Select(e => new TemplateComponentPositionModel
                            {
                                ID = e.PositionName,
                                TemplateName = e.TemplateName,
                                ComponentName = e.ComponentName,
                                Summary = e.Summary
                            });
            if (!string.IsNullOrEmpty(templateName)) positions = positions.Where(e => e.TemplateName == templateName);
            if (!string.IsNullOrEmpty(componentName)) positions = positions.Where(e => e.ComponentName == componentName);
            return positions;
        }

        public string AddPositionTemplate(TemplatePositionModel model)
        {
            var position = templatePositionDAL.GetAll().FirstOrDefault(e => e.TemplateName == model.TemplateName && e.PositionName == model.ID);
            if (position != null) throw new BusinessException(string.Format("Vị trí [{0}] đã tồn tại trên giao diện [{1}]", model.ID, model.TemplateName));

            position = new TemplatePosition();
            position.TemplateName = model.TemplateName;
            position.PositionName = model.ID;
            position.Summary = model.Summary;

            this.templatePositionDAL.Add(position);
            this.SaveChanges();
            return model.ID;
        }

        public string AddPositionComponent(TemplateComponentPositionModel model)
        {
            var position = templateComponentPositionDAL.GetAll().FirstOrDefault(e => e.TemplateName == model.TemplateName && e.ComponentName == model.ComponentName && e.PositionName == model.ID);
            if (position != null) throw new BusinessException(string.Format("Vị trí [{0}] đã tồn tại trên giao diện [{1}]-[{2}]", model.ID, model.TemplateName, model.ComponentName));

            position = new TemplateComponentPosition();
            position.TemplateName = model.TemplateName;
            position.ComponentName = model.ComponentName;
            position.PositionName = model.ID;
            position.Summary = model.Summary;

            this.templateComponentPositionDAL.Add(position);
            this.SaveChanges();
            return model.ID;
        }

        public void RemovePositionComponent(TemplateComponentPositionModel model)
        {
            var position = templateComponentPositionDAL.GetAll().FirstOrDefault(e => e.TemplateName == model.TemplateName && e.ComponentName == model.ComponentName && e.PositionName == model.ID);
            if (position == null) throw new BusinessException(string.Format("Vị trí [{0}] không tồn tại trên giao diện [{1}]-[{2}]", model.ID, model.TemplateName, model.ComponentName));

            this.templateComponentPositionDAL.Delete(position);
            this.SaveChanges();
        }

        public void RemovePositionTemplate(TemplatePositionModel model)
        {
            var position = templatePositionDAL.GetAll().FirstOrDefault(e => e.TemplateName == model.TemplateName && e.PositionName == model.ID);
            if (position == null) throw new BusinessException(string.Format("Vị trí [{0}] không tồn tại trên giao diện [{1}]", model.ID, model.TemplateName));

            this.templatePositionDAL.Delete(position);
            this.SaveChanges();
        }
        #endregion

        #region Skin
        public IQueryable<TemplateSkinModel> GetAllSkins(string templateName = "", string moduleName = "")
        {
            var skins = templateSkinDAL.GetAll()
                            .Select(e => new TemplateSkinModel
                            {
                                ID = e.SkinName,
                                TemplateName = e.TemplateName,
                                ModuleName = e.ModuleName,
                                Summary = e.Summary
                            });
            if (!string.IsNullOrEmpty(templateName)) skins = skins.Where(e => e.TemplateName == templateName);
            if (!string.IsNullOrEmpty(moduleName)) skins = skins.Where(e => e.ModuleName == moduleName);
            return skins;
        }

        public string UpdateSkinTemplate(TemplateSkinModel model)
        {
            var skin = templateSkinDAL.GetAll().FirstOrDefault(e => e.TemplateName == model.TemplateName && e.SkinName == model.ID);
            if (skin == null) throw new BusinessException(string.Format("Skin [{0}] không tồn tại trên giao diện [{1}]", model.ID, model.TemplateName));
            
            skin.TemplateName = model.TemplateName;
            skin.SkinName = model.ID;
            skin.ModuleName = model.ModuleName;
            skin.Summary = model.Summary;

            this.templateSkinDAL.Update(skin);
            this.SaveChanges();
            return model.ID;
        }

        public string AddSkinTemplate(TemplateSkinModel model)
        {
            var skin = templateSkinDAL.GetAll().FirstOrDefault(e => e.TemplateName == model.TemplateName && e.SkinName == model.ID);
            if (skin != null) throw new BusinessException(string.Format("Skin [{0}] đã tồn tại trên giao diện [{1}]", model.ID, model.TemplateName));

            skin = new TemplateSkin();
            skin.TemplateName = model.TemplateName;
            skin.SkinName = model.ID;
            skin.ModuleName = model.ModuleName;
            skin.Summary = model.Summary;

            this.templateSkinDAL.Add(skin);
            this.SaveChanges();
            return model.ID;
        }

        public void RemoveSkinTemplate(TemplateSkinModel model)
        {
            var skin = templateSkinDAL.GetAll().FirstOrDefault(e => e.TemplateName == model.TemplateName && e.SkinName == model.ID);
            if (skin == null) throw new BusinessException(string.Format("Skin [{0}] không tồn tại trên giao diện [{1}]", model.ID, model.TemplateName));

            this.templateSkinDAL.Delete(skin);
            this.SaveChanges();
        }
        #endregion

        #region Component
        public IQueryable<TemplateComponentModel> GetAllComponents(string templateName = "")
        {
            var skins = templateComponentDAL.GetAll()
                            .Select(e => new TemplateComponentModel
                            {
                                ComponentName = e.ComponentName,
                                TemplateName = e.TemplateName,
                                Summary = e.Summary,
                                Positions = e.TemplateComponentPositions.Count
                            });
            if (!string.IsNullOrEmpty(templateName)) skins = skins.Where(e => e.TemplateName == templateName);
            return skins;
        }

        public string AddComponentemplate(TemplateComponentModel model)
        {
            var component = templateComponentDAL.GetAll().FirstOrDefault(e => e.TemplateName == model.TemplateName && e.ComponentName == model.ComponentName);
            if (component != null) throw new BusinessException(string.Format("Component [{0}] đã tồn tại trên giao diện [{1}]", model.ComponentName, model.TemplateName));

            component = new TemplateComponent();
            component.TemplateName = model.TemplateName;
            component.ComponentName = model.ComponentName;
            component.Summary = model.Summary;

            this.templateComponentDAL.Add(component);
            this.SaveChanges();
            return model.ComponentName;
        }

        public void RemoveComponentTemplate(TemplateComponentModel model)
        {
            var component = templateComponentDAL.GetAll().FirstOrDefault(e => e.TemplateName == model.TemplateName && e.ComponentName == model.ComponentName);
            if (component == null) throw new BusinessException(string.Format("Component [{0}] không tồn tại trên giao diện [{1}]", model.ComponentName, model.TemplateName));

            this.templateComponentDAL.Delete(component);
            this.SaveChanges();
        }
        #endregion

        #region Extension
        public string GetDomainByTemplatePrivate(string TemplateName)
        {
            var companyId = this.webConfigDAL.GetAll()
                .Where(e => e.Template == TemplateName)
                .Select(e => e.Id)
                .FirstOrDefault();
            if (companyId > 0)
            {
                var domain = this.domainDAL.GetAll()
                    .Where(e => e.CompanyId == companyId)
                    .Select(e => e.Domain)
                    .FirstOrDefault();

                return domain;
            }

            return null;
        }
        #endregion
    }
}
