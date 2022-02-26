
using Library.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using Web.Data;
using Web.Data.DataAccess;
using Web.Model;

namespace Web.Business
{
    public class CompanyBLL : BaseBLL
    {
        private CompanyUserDAL userDAL;
        private CompanyDAL companyDAL;
        private CompanyLanguageDAL companyLanguageDAL;
        private CompanyDomainDAL domainDAL;
        private WebConfigDAL configDAL;
        private ConfigOnePayDAL configOnePayDAL;
        private ConfigGHNDAL configGHNDAL;
        private ConfigGHTKDAL configGHTKDAL;
        private ConfigMemberPointDAL configMemberPointDAL;
        private ConfigAccessTradeDAL configAccessTradeDAL;
        private LanguageDAL languageDAL;
        private ThirdPartyDAL thirdPartyDAL;
        private ItemDAL itemDAL;
        private AttributeDAL attributeDAL;
        private ModuleConfigDAL moduleConfigDAL;
        private MenuShortcutDAL shorcutDAL;
        private CategoryDAL categoryDAL;

        public CompanyBLL(string connectionString = "")
            : base(connectionString)
        {
            userDAL = new CompanyUserDAL(this.DatabaseFactory);
            companyDAL = new CompanyDAL(this.DatabaseFactory);
            companyLanguageDAL = new CompanyLanguageDAL(this.DatabaseFactory);
            configDAL = new WebConfigDAL(this.DatabaseFactory);
            configOnePayDAL = new ConfigOnePayDAL(this.DatabaseFactory);
            configGHNDAL = new ConfigGHNDAL(this.DatabaseFactory);
            configGHTKDAL = new ConfigGHTKDAL(this.DatabaseFactory);
            configMemberPointDAL = new ConfigMemberPointDAL(this.DatabaseFactory);
            configAccessTradeDAL = new ConfigAccessTradeDAL(this.DatabaseFactory);
            languageDAL = new LanguageDAL(this.DatabaseFactory);
            domainDAL = new CompanyDomainDAL(this.DatabaseFactory);
            thirdPartyDAL = new ThirdPartyDAL(this.DatabaseFactory);
            itemDAL = new ItemDAL(this.DatabaseFactory);
            attributeDAL = new AttributeDAL(this.DatabaseFactory);
            moduleConfigDAL = new ModuleConfigDAL(this.DatabaseFactory);
            shorcutDAL = new MenuShortcutDAL(this.DatabaseFactory);
            categoryDAL = new CategoryDAL(this.DatabaseFactory);
        }

        #region User
        public int GetCompanyByUserId(int userId)
        {
            var companyId = userDAL.GetAll()
                            .Where(e => e.UserId == userId)
                            .Select(e => e.CompanyId)
                            .FirstOrDefault();
            return companyId;
        }
        public UserCompanyModel UserCompanyModel(int userId)
        {
            var user = userDAL.GetAll()
                            .Where(e => e.UserId == userId)
                            .Select(e => new UserCompanyModel { CompanyId = e.CompanyId, Balance = e.Balance, ID = e.UserId })
                            .FirstOrDefault();
            return user;
        }
        
        #endregion

        #region Company
        public DataSimpleModel GetDataSingle(int companyId, string languageId)
        {
            var query = this.companyLanguageDAL.GetAll()
                .Where(e => e.CompanyId == companyId && e.LanguageId == languageId);

            var selected = query.Select(a => new DataSimpleModel
            {
                ID = a.CompanyId,
                Title = a.DisplayName,
                Description = a.Description,
                ImagePath = a.Company.Image,
                TargetTag = a.Slogan,
            });

            var data = selected.FirstOrDefault();

            return data;
        }

        public COMPANYLANGUAGEModel GetCompanyInfo(int companyId, string language)
        {
            var company = companyDAL.GetAll().Where(e => e.Id == companyId)
                .Select(e => new COMPANYLANGUAGEModel
                {
                    ID = e.Id,
                    IMAGE = e.Image,
                    EMAIL = e.Email,
                    PHONE = e.Phone,
                    FAX = e.Fax,
                    LANGUAGEID = language
                }).FirstOrDefault();

            if (company == null) return null;
            
            var comLang = companyLanguageDAL.GetAll().Where(e => e.CompanyId == companyId && e.LanguageId == language).FirstOrDefault();
            if(comLang != null)
            {
                company.ABOUTUS = comLang.AboutUs;
                company.ADDRESS = comLang.Address;
                company.DESCRIPTION = comLang.Description;
                company.DISPLAYNAME = comLang.DisplayName;
                company.FULLNAME = comLang.FullName;
                company.SLOGAN = comLang.Slogan;
                company.Certificate = comLang.Certificate;
            }
            else
            {
                var defaultLanguage = configDAL.GetAll().Where(e => e.Id == companyId).FirstOrDefault();
                if (defaultLanguage.DefaultLanguageIfNotSet)
                {
                    comLang = companyLanguageDAL.GetAll().Where(e => e.CompanyId == companyId && e.LanguageId == defaultLanguage.DefaultLanguage).FirstOrDefault();
                    if (comLang == null) comLang = companyLanguageDAL.GetAll().Where(e => e.CompanyId == companyId).FirstOrDefault();
                    if (comLang != null)
                    {
                        company.ABOUTUS = comLang.AboutUs;
                        company.ADDRESS = comLang.Address;
                        company.DESCRIPTION = comLang.Description;
                        company.DISPLAYNAME = comLang.DisplayName;
                        company.FULLNAME = comLang.FullName;
                        company.SLOGAN = comLang.Slogan;
                        company.Certificate = comLang.Certificate;
                    }
                }
            }

            return company;
        }

        public CompanyInfoModel GetCompany(int companyId, string language)
        {
            var company = companyDAL.GetAll().Where(e => e.Id == companyId)
                .Select(e => new CompanyInfoModel
                {
                    ID = e.Id,
                    IMAGE = e.Image,
                    EMAIL = e.Email,
                    PHONE = e.Phone,
                    FAX = e.Fax,
                    CreateDate = e.WebConfig.CreateDate,
                    ExperDate = e.WebConfig.ExperDate,
                    FacebookAppId = e.WebConfig.FacebookAppId,
                    FacebookFanpage = e.WebConfig.FacebookFanpage,
                    GoogleAnalytics = e.WebConfig.GoogleAnalytics,
                    GoogleApiKey = e.WebConfig.GoogleApiKey,
                    GoogleMapAddress = e.WebConfig.GoogleMapAddress,
                    GooglePlus = e.WebConfig.GooglePlus,
                    Instagram = e.WebConfig.Instagram,
                    Keys = e.WebConfig.Keys,
                    MailAccount = e.WebConfig.MailAccount,
                    MailEnableSSL = e.WebConfig.MailEnableSSL ?? false,
                    MailPort = e.WebConfig.MailPort,
                    MailServer = e.WebConfig.MailServer,
                    RegisDate = e.WebConfig.RegisDate,
                    Twitter = e.WebConfig.Twitter,
                    WebIcon = e.WebConfig.WebIcon,
                    Youtube = e.WebConfig.Youtube,
                    Zalo = e.WebConfig.Zalo,
                    Whatsapp = e.WebConfig.Whatsapp,
                    DefaultLanguage = e.WebConfig.DefaultLanguage,
                    DefaultTemplate = e.WebConfig.Template,
                    TemplateConfigBy = e.WebConfig.TemplateConfigBy,
                    Background = e.WebConfig.Background,
                    BackgroundPosition = e.WebConfig.BackgroundPosition,
                    BackgroundRepeat = e.WebConfig.BackgroundRepeat,
                    FacebookPersonalId = e.WebConfig.FacebookPersonalId,
                    FooterBackground = e.WebConfig.FooterBackground,
                    FooterFontColor = e.WebConfig.FooterFontColor,
                    FooterFontSize = e.WebConfig.FooterFontSize,
                    GHNFromDistrict = e.WebConfig.ConfigGHN.GHNFromDistrict,
                    GHNUserName = e.WebConfig.ConfigGHN.GHNUserName,
                    GHTKAddressId = e.WebConfig.ConfigGHTK.GHTKAddressId,
                    HeaderBackground = e.WebConfig.HeaderBackground,
                    HeaderFontColor = e.WebConfig.HeaderFontColor,
                    HeaderFontSize = e.WebConfig.HeaderFontSize,
                    IsRightClick = e.WebConfig.IsRightClick,
                    IsSelectCoppy = e.WebConfig.IsSelectCoppy,
                    Linkedin = e.WebConfig.Linkedin,
                    LinkGoPublic = e.WebConfig.LinkGoPublic,
                    ModifyByUser = e.WebConfig.ModifyByUser,
                    ModifyDate = e.WebConfig.ModifyDate,
                    Pinterest = e.WebConfig.Pinterest,
                    Hierarchy = e.WebConfig.Hierarchy,
                    VerifyOutside = e.WebConfig.VerifyOutside
                }).FirstOrDefault();

            if (company == null) return null;

            var comLang = companyLanguageDAL.GetAll().Where(e => e.CompanyId == companyId && e.LanguageId == language).FirstOrDefault();
            if (comLang != null)
            {
                company.ABOUTUS = comLang.AboutUs;
                company.ADDRESS = comLang.Address;
                company.DESCRIPTION = comLang.Description;
                company.DISPLAYNAME = comLang.DisplayName;
                company.FULLNAME = comLang.FullName;
                company.SLOGAN = comLang.Slogan;
                company.Certificate = comLang.Certificate;
            }
            else
            {
                var defaultLanguage = configDAL.GetAll().Where(e => e.Id == companyId).FirstOrDefault();
                if(defaultLanguage.DefaultLanguageIfNotSet)
                {
                    comLang = companyLanguageDAL.GetAll().Where(e => e.CompanyId == companyId && e.LanguageId == defaultLanguage.DefaultLanguage).FirstOrDefault();
                    if (comLang == null)  comLang = companyLanguageDAL.GetAll().Where(e => e.CompanyId == companyId).FirstOrDefault();
                    if (comLang != null)
                    {
                        company.ABOUTUS = comLang.AboutUs;
                        company.ADDRESS = comLang.Address;
                        company.DESCRIPTION = comLang.Description;
                        company.DISPLAYNAME = comLang.DisplayName;
                        company.FULLNAME = comLang.FullName;
                        company.SLOGAN = comLang.Slogan;
                        company.Certificate = comLang.Certificate;
                    }
                }
            }

            return company;
        }

        public void UpdateInfo(COMPANYLANGUAGEModel model)
        {
            var companyLanguage = companyLanguageDAL.AllIncludes(e => e.Company).FirstOrDefault(e => e.CompanyId == model.ID && e.LanguageId == model.LANGUAGEID);
            if (companyLanguage == null)
            {
                var company = companyDAL.GetAll().FirstOrDefault(e => e.Id == model.ID);
                if(company == null) throw new BusinessException("Không tồn tại thông tin cty");
                else
                {
                    companyLanguage = new CompanyLanguage();
                    companyLanguage.LanguageId = model.LANGUAGEID;
                    company.CompanyLanguages.Add(companyLanguage);
                    this.companyDAL.Update(company);
                }
            }
            else this.companyLanguageDAL.Update(companyLanguage);

            companyLanguage.AboutUs = model.ABOUTUS;
            companyLanguage.Address = model.ADDRESS;
            companyLanguage.Description = model.DESCRIPTION;
            companyLanguage.DisplayName = model.DISPLAYNAME;
            companyLanguage.FullName = model.FULLNAME;
            companyLanguage.Certificate = model.Certificate;
            companyLanguage.Slogan = model.SLOGAN;
            companyLanguage.Company.Image = model.IMAGE;
            companyLanguage.Company.Email = model.EMAIL;
            companyLanguage.Company.Phone = model.PHONE;
            companyLanguage.Company.Fax = model.FAX;
            
            this.SaveChanges();
        }

        //urmService = SettingsManager.AppSettings.URMService + "/odata/User"
        public int RegisWeb(string language,
            string template,
            List<string> domains,
            string companyName,
            string email,
            string phone,
            string fullName,
            string refId,
            int copyFromCompany,
            int companyId,
            string host, bool enableSSL, string sendFrom, string emailPassword, int port,
            string urmService)
        {
            // validate domain
            var checkDomain = this.domainDAL.GetAll()
                .Where(e => domains.Contains(e.Domain.ToLower()) && e.CompanyId != companyId)
                .Select(e => e.Domain).FirstOrDefault();
            if (checkDomain != null) throw new BusinessException("Tên miền '" + checkDomain + "' đã có người sử dụng");

            var api = new ApiHelper();
            foreach (var domain in domains)
            {
                try
                {
                    if (!domain.Contains("."))
                        throw new BusinessException("Tên miền '" + domain + "' không đúng định dạng, phải chứa ký tự '.'");

                    var d = domain.Split('.');
                    if (string.IsNullOrEmpty(d[0]))
                        throw new BusinessException("Tên miền '" + domain + "' không đúng định dạng, tên miền không được rỗng");

                    if (string.IsNullOrEmpty(d[1]))
                        throw new BusinessException("Tên miền '" + domain + "' không đúng định dạng, đuôi tên miền không được rỗng");
                }
                catch
                {
                    throw new BusinessException("Tên miền '" + domain + "' không hợp lệ, không đúng định dạng");
                }
            }

            // validate email
            if (!string.IsNullOrEmpty(email))
            {
                var regex = new System.Text.RegularExpressions.Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
                var result = regex.IsMatch(email);
                if (result == false)
                {
                    throw new BusinessException("Địa chỉ email không hợp lệ.");
                }
            }

            // validate RefId
            if (!string.IsNullOrEmpty(refId))
            {
                try
                {
                    var refs = refId.Split('u');
                    var refUserId = Convert.ToInt32(refs[1]);
                    var refCompanyId = Convert.ToInt32(refs[0].Substring(1));
                    //var existUsser = this._userAccountDAL.GetAll().Any(e => e.UserId == refUserId && e.UserInfo.CompanyId == refCompanyId);
                    //if(!existUsser) throw new BusinessLogicLayerException("Mã người giới thiệu '" + refId + "' không tồn tại");
                }
                catch
                {
                    throw new BusinessException("Mã người giới thiệu '" + refId + "' không hợp lệ, không đúng định dạng");
                }
            }

            log.Info(domains[0] + " - Step 0: Get default data.");
            #region B0: Get du lieu can thiet de tao du lieu mac dinh
            var items = this.itemDAL.AllIncludes(e => e.Category, e => e.Category.CategoryLanguages, 
                                                    e => e.Article, e => e.Article.ArticleLanguages, 
                                                    e => e.Article.Product, e => e.Article.Product.ProductGroupon, e => e.Article.Product.ProductPrices, e => e.Article.Product.ProductColors, e => e.Article.Product.ProductAttributes, 
                                                    e => e.Article.ArticleLink, 
                                                    e => e.Article.File, e => e.Article.File.FileDocument)
            .Where(e => e.CompanyId == copyFromCompany)
            .ToList();

            // tat ca danh muc cua company default 
            var listAllCategories = items.Where(e => e.Category != null).ToList();

            // get cac thong tin can thiet cho B1, B2, B3
            var companyInfo = this.companyDAL.GetAll().Where(e => e.Id == copyFromCompany).FirstOrDefault();
            var companyInfos = this.companyLanguageDAL.GetAll().Where(e => e.CompanyId == copyFromCompany).ToList();

            var tempWebconfig = this.configDAL.Get(e => e.Id == copyFromCompany);

            // du lieu mac dinh cua thuộc tính
            var attributes = this.attributeDAL.GetAll().Where(e => e.CompanyId == copyFromCompany).ToList();
            #endregion

            // mo transaction bat dau xu ly
            var userId = 0;
            var conn = this.DatabaseFactory.Get().Database.Connection;
            conn.Open();
            var trans = conn.BeginTransaction();
            try
            {
                log.Info(domains[0] + " - Step 1: Create table (Company, CompanyLanguage, CompanyDomain).");
                #region B1: Tao company: table Company, CompanyLanguage, CompanyDomain, CompanyTemplate
                Company company;
                if (companyId > 0)
                {
                    company = this.companyDAL.GetAll().FirstOrDefault(e => e.Id == companyId);
                    company.Email = email;
                    company.Phone = phone;
                    company.Fax = phone;
                    company.RefId = refId;
                    company.IsConfirm = true;

                    var comLang = company.CompanyLanguages.FirstOrDefault(e => e.LanguageId == language);
                    if (comLang != null)
                    {
                        comLang.FullName = companyName;
                        comLang.DisplayName = companyName;
                        this.companyLanguageDAL.Update(comLang);
                    }
                    else
                    {
                        comLang = new CompanyLanguage();
                        comLang.FullName = companyName;
                        comLang.DisplayName = companyName;

                        comLang.LanguageId = language;
                        company.CompanyLanguages.Add(comLang);
                    }

                    this.companyDAL.Update(company);
                }
                else
                {
                    company = new Company();
                    company.Email = email;
                    company.Phone = phone;
                    company.Fax = phone;
                    company.IsConfirm = true;
                    company.RefId = refId;
                    company.CreateByUser = companyInfo.CreateByUser;
                    company.CreateDate = DateTime.Now;
                    if (companyInfo != null) company.Image = companyInfo.Image;

                    foreach (var info in companyInfos)
                    {
                        var companyLanguage = new CompanyLanguage();
                        companyLanguage.FullName = companyName;
                        companyLanguage.DisplayName = companyName;

                        companyLanguage.LanguageId = info.LanguageId;
                        companyLanguage.Slogan = info.Slogan;
                        companyLanguage.Address = info.Address;
                        companyLanguage.AboutUs = info.AboutUs;
                        company.CompanyLanguages.Add(companyLanguage);
                    }

                    this.companyDAL.Add(company);
                }
                
                foreach (var domain in domains)
                {
                    if (!this.domainDAL.GetAll().Any(e => e.Domain == domain.Trim() && e.CompanyId == companyId))
                    {
                        var companyDomain = new CompanyDomain { Domain = domain.Trim() };
                        company.CompanyDomains.Add(companyDomain);
                    }
                }
                #endregion

                log.Info(domains[0] + " - Step 2: Create user (CompanyUser).");
                #region B2: Tao user: table UserInfo, UserAccount, UserAdmin (chu y roles default)

                var user = new UserModel();
                user.Password = Library.GenerateRandomCode.RandomCode(6).ToLower();
                user.Phone = phone;
                user.Email = email;
                user.FullName = fullName;
                user.GroupId = 12;

                var createsuccess = false;
                do
                {
                    try
                    {
                        user.UserName = "web" + Library.GenerateRandomCode.RandomNumber(5);
                        var newuser = api.Insert<UserModel>(urmService, user);
                        user.ID = userId = newuser.ID;
                        createsuccess = true;
                    }
                    catch { }
                }
                while (createsuccess == false);

                var companyUser = new CompanyUser();
                companyUser.UserId = user.ID;
                company.CreateByUser = user.UserName;
                company.CompanyUsers.Add(companyUser);
                #endregion

                log.Info(domains[0] + " - Step 3: Create config (WebConfig, language).");
                #region B3: cau hinh cho web: table WebConfig, language
                company.WebConfig = new WebConfig();
                company.WebConfig.CreateDate = DateTime.Now;
                company.WebConfig.Template = template;
                company.WebConfig.Keys = string.Empty;

                company.WebConfig.Hierarchy = false;
                company.WebConfig.IsSelectCoppy = true;
                company.WebConfig.IsRightClick = true;
                company.WebConfig.WebIcon = tempWebconfig.WebIcon;
                #endregion

                this.SaveChanges();

                log.Info(domains[0] + " - Step 4: Create sample data.");
                // B4: Tao du lieu mau: copy du lieu theo template cua company o B1
                var mapItems = this.CreateDefaultDataForNewCompany(items, listAllCategories, attributes, company.Id, user.ID, copyFromCompany);

                log.Info(domains[0] + "- Step 5: Create default module.");
                // B5: Tao module defaule: copy du lieu theo template cua company o B1
                this.CreateModuleDefaultForNewCompany(items, template, company.Id, user.ID, mapItems);

                this.SaveChanges();
                trans.Commit();
                return company.Id;

                //log.Info(domains[0] + " - Step 6: Copy sample files.");
                //// B6: copy file tu company o B1 sang company moi
                //var source = string.Format(SettingsManager.AppSettings.FolderUpload, copyFromCompany);
                //var dest = string.Format(SettingsManager.AppSettings.FolderUpload, company.Id);
                //this.CopyFolder(source, dest);

                //log.Info(domains[0] + " - Step7: Bind new domain to IIS.");
                //// B7: Add domain binding for website                
                //BindingWeb.Add(domains.ToList());

                //log.Info(domains[0] + " - Step 8: Send mail to customer.");
                //// B8: gửi mail thông báo
                //var content = new System.Text.StringBuilder();
                //content.AppendFormat("<p>Chào <b>{0}</b>,</p>", fullName);
                //content.Append("<p>Chúc mừng bạn đã đăng ký website thành công, thông tin đăng ký:<br />");
                //content.AppendFormat("Tên website: {0} <br />", companyName);
                //content.AppendFormat("Tên miền truy cập: {0} <br />", string.Join(", ", domains));
                //content.AppendFormat("Email: {0} <br />", email);
                //content.AppendFormat("Số điện thoại: {0} <br />", phone);
                //content.AppendFormat("Hệ thống quản trị nội dung: {0} <br />", SettingsManager.AppSettings.DomainStore);
                //content.AppendFormat("Tài khoản: {0} <br />", user.UserName);
                //content.AppendFormat("Mật khẩu: {0} <br />", user.Password);
                //content.Append("<br />");
                ////themsub
                //content.AppendFormat("Hiện tại Quý khách đã có thể truy cập website tại địa chỉ <a href='http://{1}'>{1}</a>. <br /> Để sử dụng tên miền chính thức <b>{0}</b> Quý khách vui lòng trỏ tên miền về địa chỉ IP: {2}<br />", string.Join(", ", domains.Where(e => e != domainRegisted).ToList()), domainRegisted, SettingsManager.AppSettings.IpPublic);
                //content.Append("<br />");
                //content.Append("<br />");
                //content.Append("Nếu Quý khách gặp khó khăn trong quá trình vận hành vui lòng xem hướng dẫn <a href='http://vdoni.com/thong-tin-can-biet'>tại đây</a> hoặc gọi ngay 0868 456 400 để được hỗ trợ kịp thời.");
                //content.Append("<br />");
                //content.Append("</p><p>Cảm ơn bạn đã sử dụng dịch vụ!<br/><b>TAVISOL.,JSC</b></p>");
                //if (!string.IsNullOrEmpty(email)) this.SendEmail(host, enableSSL, sendFrom, email, emailPassword, port, "TAVISOL.,JSC - Đăng ký thành công website " + domains[0], content.ToString());
                //this.SendEmail(host, enableSSL, sendFrom, sendFrom, emailPassword, port, email + ": " + " đã đăng ký thành công " + domains[0], content.ToString());
            }
            catch (BusinessException)
            {
                trans.Rollback();
                api.Delete(urmService, userId);
                throw;
            }
            catch (Exception ex)
            {
                log.Info(domains[0] + "Exception: " + ex.Message);
                trans.Rollback();
                api.Delete(urmService, userId);
                throw new BusinessException(string.Format("Đăng ký không thành công. Vì có quá nhiều người đang đăng ký, vui lòng quay lại sau hoặc liên hệ bộ phận kỹ thuật nhờ giúp đỡ: {0}.", ex.Message));
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Ham tao du lieu mac dinh cho web moi dang ky
        /// </summary>
        /// <param name="items">
        /// Danh sach tat ca du lieu mac dinh can copy qua
        /// </param>
        /// <param name="newCompanyId">
        /// Ma cong ty moi vua khoi tao
        /// </param>
        /// <param name="userName">
        /// Ma nguoi dang ky
        /// </param>
        /// <returns>
        /// Tra ve bang Map giu Id cu va Id moi vua copy qua
        /// </returns>
        private IDictionary<int, int> CreateDefaultDataForNewCompany(IList<Item> items, IList<Item> categories, IList<Web.Data.Attribute> attributes, int newCompanyId, int userId, int copyFromCompany)
        {
            // danh muc duoc su dung trong template
            var listCategoryId = categories.Select(e => e.Id).ToList();

            // copy tat ca du lieu cua company 2 sang company moi
            // su dung cac bien map de luu Id cu va moi su dung lai cho module
            var mapItems = new Dictionary<int, int>();

            #region du lieu mac dinh cua danh muc
            this.CopyCategories(mapItems, categories, null, userId, newCompanyId);
            #endregion

            #region du lieu mac dinh attribute
            var mapAttributes = new Dictionary<int, int>();
            foreach(var attribute in attributes)
            {
                var newAttribute = new Web.Data.Attribute();
                if(attribute.CategoryId > 0)
                    newAttribute.CategoryId = mapItems[attribute.CategoryId ?? 0];
                newAttribute.CompanyId = newCompanyId;
                newAttribute.Name = attribute.Name;
                newAttribute.Type = attribute.Type;
                this.attributeDAL.Add(newAttribute);
                this.DatabaseFactory.Get().SaveChanges();
                mapAttributes[attribute.Id] = newAttribute.Id;
            }
            #endregion

            #region du lieu mac dinh cua bai viet
            var articles = items.Where(e => e.Article != null && listCategoryId.Contains(e.Article.CategoryId)).ToList();
            foreach (var item in articles)
            {
                var newItem = new Item();
                newItem.ModifyByUser = userId;
                newItem.ModifyDate = DateTime.Now;
                newItem.CompanyId = newCompanyId;
                newItem.IsPublished = true;
                newItem.Orders = item.Orders;

                var obj = new Article();
                obj.Image = item.Article.Image;
                obj.CategoryId = mapItems[item.Article.CategoryId];
                obj.Tag = item.Article.Tag;
                obj.HasComment = item.Article.HasComment;
                obj.DisplayDate = DateTime.Now;
                newItem.Article = obj;

                foreach (var cl in item.Article.ArticleLanguages)
                {
                    var lang = new ArticleLanguage();
                    lang.Contents = cl.Contents;
                    lang.Title = cl.Title;
                    lang.Brief = cl.Brief;
                    lang.Contents = cl.Contents;
                    lang.LanguageId = cl.LanguageId;
                    newItem.Article.ArticleLanguages.Add(lang);
                }

                // link
                if (item.Article.ArticleLink != null)
                {
                    newItem.Article.ArticleLink = new ArticleLink();
                    newItem.Article.ArticleLink.Target = item.Article.ArticleLink.Target;
                    newItem.Article.ArticleLink.URL = item.Article.ArticleLink.URL;
                }

                // file
                if (item.Article.File != null)
                {
                    newItem.Article.File = new File();
                    newItem.Article.File.FileName = item.Article.File.FileName;
                    newItem.Article.File.FileUrl = item.Article.File.FileUrl;
                    newItem.Article.File.Embed = item.Article.File.Embed;
                    newItem.Article.File.Type = item.Article.File.Type;
                    newItem.Article.File.Size = item.Article.File.Size;

                    if (item.Article.File.FileDocument != null)
                    {
                        newItem.Article.File.FileDocument.Pages = item.Article.File.FileDocument.Pages;
                        newItem.Article.File.FileDocument.Author = item.Article.File.FileDocument.Author;
                    }
                }

                //product
                if (item.Article.Product != null)
                {
                    newItem.Article.Product = new Product();
                    newItem.Article.Product.Code = item.Article.Product.Code;
                    newItem.Article.Product.Price = item.Article.Product.Price;
                    newItem.Article.Product.Quantity = item.Article.Product.Quantity;
                    newItem.Article.Product.Sale = item.Article.Product.Sale;

                    // Groupon
                    if (item.Article.Product.ProductGroupon != null)
                    {
                        newItem.Article.Product.ProductGroupon = new ProductGroupon();
                        newItem.Article.Product.ProductGroupon.EndDate = item.Article.Product.ProductGroupon.EndDate;
                        newItem.Article.Product.ProductGroupon.Quantity = item.Article.Product.ProductGroupon.Quantity;
                        newItem.Article.Product.ProductGroupon.StartDate = item.Article.Product.ProductGroupon.StartDate;
                    }

                    // colors
                    foreach (var o in item.Article.Product.ProductColors)
                    {
                        var color = new ProductColor();
                        color.ImageName = o.ImageName;
                        color.Name = o.Name;
                        color.Price = o.Price;
                        color.Value = o.Value;
                        newItem.Article.Product.ProductColors.Add(color);
                    }

                    // prices
                    foreach (var o in item.Article.Product.ProductPrices)
                    {
                        var price = new ProductPrice();
                        price.Price = o.Price;
                        price.Quantity = o.Quantity;
                        newItem.Article.Product.ProductPrices.Add(price);
                    }

                    // prices
                    foreach (var o in item.Article.Product.ProductAttributes)
                    {
                        var atr = new ProductAttribute();
                        atr.Value = o.Value;
                        atr.AttributeId = mapAttributes[o.AttributeId];
                        newItem.Article.Product.ProductAttributes.Add(atr);
                    }
                }

                this.itemDAL.Add(newItem);
                this.DatabaseFactory.Get().SaveChanges();
                mapItems[item.Id] = newItem.Id;
            }
            #endregion

            #region du lieu mac dinh cua support-online
            var supports = items.Where(e => e.SupportOnline != null && listCategoryId.Contains(e.SupportOnline.CategoryId)).ToList();
            foreach (var item in supports)
            {
                var newItem = new Item();
                newItem.ModifyByUser = userId;
                newItem.ModifyDate = DateTime.Now;
                newItem.CompanyId = newCompanyId;
                newItem.IsPublished = true;
                newItem.Orders = item.Orders;

                var obj = new SupportOnline();
                obj.FullName = item.SupportOnline.FullName;
                obj.CategoryId = mapItems[item.SupportOnline.CategoryId];
                obj.NickName = item.SupportOnline.NickName;
                obj.Phone = item.SupportOnline.Phone;
                obj.TypeId = item.SupportOnline.TypeId;
                newItem.SupportOnline = obj;

                this.itemDAL.Add(newItem);
                this.DatabaseFactory.Get().SaveChanges();
                mapItems[item.Id] = newItem.Id;
            }
            #endregion

            return mapItems;
        }

        /// <summary>
        /// Ham tao module mau cho web moi dang ky.
        /// </summary>
        /// <param name="items">
        /// Danh sach du lieu mau
        /// </param>
        /// <param name="templateName">
        /// The template name.
        /// </param>
        /// <param name="companyId">
        /// Ma cong ty dang ky
        /// </param>
        /// <param name="user">
        /// Nguoi dang ky
        /// </param>
        /// <param name="mapIds">
        /// Tra ve bang Map giu Id cu va Id moi vua copy qua
        /// </param>
        private void CreateModuleDefaultForNewCompany(IList<Item> items, string templateName, int companyId, int user, IDictionary<int, int> mapIds)
        {
            var modules = items.Where(e => e.ModuleConfig != null && e.ModuleConfig.TemplateName.ToLower() == templateName.ToLower()).ToList();

            foreach (var item in modules)
            {
                var languageList = item.ModuleConfig.ModuleConfigLanguages;
                var paramList = item.ModuleConfig.ModuleConfigParams;

                var obj = new ModuleConfig();
                obj.ComponentName = item.ModuleConfig.ComponentName;
                obj.Position = item.ModuleConfig.Position;
                obj.ModuleName = item.ModuleConfig.ModuleName;
                obj.SkinName = item.ModuleConfig.SkinName;
                obj.TemplateName = item.ModuleConfig.TemplateName;

                obj.Item = new Item();
                obj.Item.ModifyByUser = user;
                obj.Item.ModifyDate = DateTime.Now;
                obj.Item.IsPublished = true;
                obj.Item.Orders = item.Orders;

                // neu la template admin thi ko luu company
                if (item.ModuleConfig.TemplateName.Trim().ToLower() != "admin") obj.Item.CompanyId = companyId;

                if (languageList != null)
                {
                    foreach (var language in languageList)
                    {
                        obj.ModuleConfigLanguages.Add(new ModuleConfigLanguage
                        {
                            Title = language.Title,
                            LanguageId = language.LanguageId
                        });
                    }
                }

                if (paramList != null)
                {
                    foreach (var param in paramList)
                    {
                        var moduleParam = new ModuleConfigParam();
                        moduleParam.ParamName = param.ParamName;
                        if (param.ParamName.ToLower() == "categoryids"
                            || param.ParamName.ToLower() == "categoryid"
                            || param.ParamName.ToLower() == "productid"
                            || param.ParamName.ToLower() == "articleid"
                            || param.ParamName.ToLower() == "itemid")
                        {
                            if (param.ParamName == "CategoryIds")
                            {
                                var cateoryIds = param.Value.Split(',').ToList();
                                var newCateoryIds = new List<int>();
                                foreach (var oldId in cateoryIds)
                                {
                                    int id, newId = 0;
                                    int.TryParse(oldId, out id);

                                    if (mapIds.ContainsKey(id))
                                    {
                                        newId = mapIds[id];
                                        newCateoryIds.Add(newId);
                                    }
                                }

                                moduleParam.Value = string.Join(",", newCateoryIds);
                            }
                            else
                            {
                                int id;
                                int.TryParse(param.Value, out id);

                                if (mapIds.ContainsKey(id)) moduleParam.Value = mapIds[id].ToString();
                                else moduleParam.Value = "0";
                            }
                        }
                        else moduleParam.Value = param.Value;

                        obj.ModuleConfigParams.Add(moduleParam);
                    }
                }

                this.moduleConfigDAL.Add(obj);
            }
        }


        /// <summary>
        /// Copy ra danh muc moi theo danh muc mau, ghi lai Id cu va Id moi tao ra
        /// </summary>
        /// <param name="mapIds">
        /// Mang Map Id cu va Id moi tao ra
        /// </param>
        /// <param name="categories">
        /// Danh sach danh muc cu.
        /// </param>
        /// <param name="parentId">
        /// Ma danh muc Root.
        /// </param>
        /// <param name="userId">
        /// Nguoi tao
        /// </param>
        /// <param name="companyId">
        /// Ma cong ty moi tao.
        /// </param>
        private void CopyCategories(IDictionary<int, int> mapIds, IList<Item> categories, int? parentId, int userId, int companyId)
        {
            var subcats = categories.Where(o => o.Category.ParentId == parentId).ToList();
            if (subcats.Count != 0)
            {
                foreach (var item in subcats)
                {
                    var newItem = new Item();
                    newItem.ModifyByUser = userId;
                    newItem.ModifyDate = DateTime.Now;
                    newItem.CompanyId = companyId;
                    newItem.IsPublished = true;
                    newItem.Orders = item.Orders;

                    var category = new Category();
                    category.Image = item.Category.Image;
                    category.ParentId = item.Category.ParentId == null ? (int?)null : mapIds[parentId ?? 0];
                    category.TypeId = item.Category.TypeId;
                    newItem.Category = category;

                    foreach (var cl in item.Category.CategoryLanguages)
                    {
                        var catLang = new CategoryLanguage();
                        catLang.Description = cl.Description;
                        catLang.Title = cl.Title;
                        catLang.LanguageId = cl.LanguageId;
                        newItem.Category.CategoryLanguages.Add(catLang);
                    }

                    this.itemDAL.Add(newItem);
                    this.DatabaseFactory.Get().SaveChanges();
                    mapIds[item.Id] = newItem.Id;

                    this.CopyCategories(mapIds, categories, item.Id, userId, companyId);
                }
            }
        }

        #endregion

        #region Config

        public string GetLanguageDefault(int companyId)
        {
            var query = configDAL.GetAll().Where(e => e.Id == companyId)
                            .Select(e => e.DefaultLanguage);

            return query.FirstOrDefault();
        }
        public WEBCONFIGModel GetConfig(int companyId)
        {
            var query = configDAL.GetAll().Where(e => e.Id == companyId)
                            .Select(e => new WEBCONFIGModel
            {
                Id = companyId,
                CreateDate = e.CreateDate,
                ExperDate = e.ExperDate,
                FacebookAppId = e.FacebookAppId,
                FacebookFanpage = e.FacebookFanpage,
                GoogleAnalytics = e.GoogleAnalytics,
                GoogleApiKey = e.GoogleApiKey,
                GoogleMapAddress = e.GoogleMapAddress,
                GooglePlus = e.GooglePlus,
                Instagram = e.Instagram,
                Keys = e.Keys,
                MailAccount = e.MailAccount,
                MailEnableSSL = e.MailEnableSSL ?? false,
                MailPassword = e.MailPassword,
                MailPort = e.MailPort,
                MailServer = e.MailServer,
                RegisDate = e.RegisDate,
                Twitter = e.Twitter,
                WebIcon = e.WebIcon,
                Youtube = e.Youtube,
                                Zalo = e.Zalo,
                                Whatsapp = e.Whatsapp,
                                DefaultLanguage = e.DefaultLanguage,
                DefaultTemplate = e.Template,
                TemplateConfigBy = e.TemplateConfigBy,
                Background = e.Background,
                BackgroundPosition = e.BackgroundPosition,
                BackgroundRepeat = e.BackgroundRepeat,
                FacebookPersonalId = e.FacebookPersonalId,
                FooterBackground = e.FooterBackground,
                FooterFontColor = e.FooterFontColor,
                FooterFontSize = e.FooterFontSize,
                HeaderBackground = e.HeaderBackground,
                HeaderFontColor = e.HeaderFontColor,
               HeaderFontSize = e.HeaderFontSize,
               IsRightClick = e.IsRightClick,
               IsSelectCoppy = e.IsSelectCoppy,
               Linkedin = e.Linkedin,
               LinkGoPublic = e.LinkGoPublic,
               ModifyByUser = e.ModifyByUser,
               ModifyDate = e.ModifyDate,
              Pinterest = e.Pinterest,
                                Hierarchy = e.Hierarchy,
              VerifyOutside = e.VerifyOutside,
                                GHNFromDistrict = e.ConfigGHN.GHNFromDistrict,
                                GHNPassword = e.ConfigGHN.GHNPassword,
                                GHNUserName = e.ConfigGHN.GHNUserName,
                                GHNToken = e.ConfigGHN.GHNToken,
                                GHTKAddressId = e.ConfigGHTK.GHTKAddressId,
                                GHTKToken = e.ConfigGHTK.GHTKToken,
                                OnePayAccessKey = e.ConfigOnePay.AccessKey,
                                OnePaySecret = e.ConfigOnePay.Secret,
                                AccessTradeSecretKey = e.ConfigAccessTrade.SecretKey,
                                AccessTradeAccessKey = e.ConfigAccessTrade.AccessKey,
                                AccessTradeDeepLink = e.ConfigAccessTrade.DeepLink,
                                AccessTradeSourceId = e.ConfigAccessTrade.SourceId,
                                OrderPercent = e.ConfigMemberPoint.OrderPercent,
                                ProductAttribute = e.ConfigMemberPoint.ProductAttribute,
                                TranferPrice = e.ConfigMemberPoint.TranferPrice,
                                DefaultLanguageIfNotSet = e.DefaultLanguageIfNotSet 
                            });

            return query.FirstOrDefault();
        }

        public CompanyConfigModel GetCompanyById(int companyId)
        {
            var config = configDAL.GetAll()
                            .Where(e => e.Id == companyId)
                            .Select(e => new CompanyConfigModel
                            {
                                ID = e.Id,
                                Language = e.DefaultLanguage,
                                Template = e.Template
                            }).FirstOrDefault();

            return config;
        }

        public ConfigGHNModel GetConfigGHN(int companyId)
        {
            var query = configGHNDAL.GetAll().Where(e => e.Id == companyId)
                            .Select(e => new ConfigGHNModel
                            {
                                GHNFromDistrict = e.GHNFromDistrict,
                                GHNPassword = e.GHNPassword,
                                GHNUserName = e.GHNUserName,GHNToken = e.GHNToken
                            });

            return query.FirstOrDefault();
        }

        public ConfigGHTKModel GetConfigGHTK(int companyId)
        {
            var query = configGHTKDAL.GetAll().Where(e => e.Id == companyId)
                            .Select(e => new ConfigGHTKModel
                            {
                                GHTKAddressId = e.GHTKAddressId,
                                GHTKToken = e.GHTKToken
                            });

            return query.FirstOrDefault();
        }

        public ConfigOnePayModel GetConfigOnePay(int companyId)
        {
            var query = configOnePayDAL.GetAll().Where(e => e.Id == companyId)
                            .Select(e => new ConfigOnePayModel
                            {
                                AccessKey = e.AccessKey,
                                Secret = e.Secret
                            });

            return query.FirstOrDefault();
        }

        public ConfigAccessTradeModel GetConfigAccessTrade(int companyId)
        {
            var query = configAccessTradeDAL.GetAll().Where(e => e.Id == companyId)
                            .Select(e => new ConfigAccessTradeModel
                            {
                                AccessKey = e.AccessKey,
                                SecretKey = e.SecretKey,
                                DeepLink = e.DeepLink,
                                SourceId = e.SourceId,
                                ID = e.Id
                            });

            return query.FirstOrDefault();
        }

        public ConfigMemberPointModel GetConfigMemberPoint(int companyId)
        {
            var query = configMemberPointDAL.GetAll().Where(e => e.Id == companyId)
                            .Select(e => new ConfigMemberPointModel
                            {
                                Id = e.Id,
                                OrderPercent = e.OrderPercent,
                                TranferPrice = e.TranferPrice,
                                ProductAttribute = e.ProductAttribute
                            });

            return query.FirstOrDefault();
        }
        public void UpdateConfig(WEBCONFIGModel model)
        {
            var config = configDAL.AllIncludes(e => e.ConfigGHN, e => e.ConfigGHTK, e => e.ConfigOnePay, e => e.ConfigAccessTrade, e => e.ConfigMemberPoint)
                                .FirstOrDefault(e => e.Id == model.Id);
            if (config == null) throw new BusinessException("Không tồn tại cấu hình");

            config.FacebookAppId = model.FacebookAppId;
            config.FacebookFanpage = model.FacebookFanpage;
            config.GoogleAnalytics = model.GoogleAnalytics;
            config.GoogleApiKey = model.GoogleApiKey;
            config.GoogleMapAddress = model.GoogleMapAddress;
            config.GooglePlus = model.GooglePlus;
            config.Instagram = model.Instagram;
            config.MailAccount = model.MailAccount;
            config.MailEnableSSL = model.MailEnableSSL;
            config.MailPassword = model.MailPassword;
            config.MailPort = model.MailPort;
            config.MailServer = model.MailServer;
            config.RegisDate = model.RegisDate;
            config.Twitter = model.Twitter;
            config.WebIcon = model.WebIcon;
            config.Youtube = model.Youtube;
            config.Zalo = model.Zalo;
            config.Whatsapp = model.Whatsapp;
            config.Background = model.Background;
            config.IsRightClick = model.IsRightClick;
            config.IsSelectCoppy = model.IsSelectCoppy;
            config.Hierarchy = model.Hierarchy;
            config.DefaultLanguage = model.DefaultLanguage;
            config.DefaultLanguageIfNotSet = model.DefaultLanguageIfNotSet;

            configDAL.Update(config);

            if (config.ConfigGHN != null || !string.IsNullOrEmpty(model.GHNToken) || !string.IsNullOrEmpty(model.GHNPassword) || !string.IsNullOrEmpty(model.GHNUserName) || model.GHNFromDistrict != null)
            {
                if (config.ConfigGHN == null)
                {
                    config.ConfigGHN = new ConfigGHN();
                    configGHNDAL.Add(config.ConfigGHN);
                }
                else configGHNDAL.Update(config.ConfigGHN);

                config.ConfigGHN.GHNFromDistrict = model.GHNFromDistrict;
                config.ConfigGHN.GHNPassword = model.GHNPassword;
                config.ConfigGHN.GHNUserName = model.GHNUserName;
                config.ConfigGHN.GHNToken = model.GHNToken;
            }
            if (config.ConfigGHTK != null || !string.IsNullOrEmpty(model.GHTKAddressId) || !string.IsNullOrEmpty(model.GHTKToken))
            {
                if (config.ConfigGHTK == null)
                {
                    config.ConfigGHTK = new ConfigGHTK();
                    configGHTKDAL.Add(config.ConfigGHTK);
                }
                else configGHTKDAL.Update(config.ConfigGHTK);

                config.ConfigGHTK.GHTKAddressId = model.GHTKAddressId;
                config.ConfigGHTK.GHTKToken = model.GHTKToken;
            }
            if (config.ConfigOnePay != null || !string.IsNullOrEmpty(model.OnePayAccessKey) || !string.IsNullOrEmpty(model.OnePaySecret))
            {
                if (config.ConfigOnePay == null)
                {
                    config.ConfigOnePay = new ConfigOnePay();
                    configOnePayDAL.Add(config.ConfigOnePay);
                }
                else configOnePayDAL.Update(config.ConfigOnePay);

                config.ConfigOnePay.AccessKey = model.OnePayAccessKey;
                config.ConfigOnePay.Secret = model.OnePaySecret;
            }
            if (config.ConfigAccessTrade != null || !string.IsNullOrEmpty(model.AccessTradeAccessKey) || !string.IsNullOrEmpty(model.AccessTradeSecretKey))
            {
                if (config.ConfigAccessTrade == null)
                {
                    config.ConfigAccessTrade = new ConfigAccessTrade();
                    configAccessTradeDAL.Add(config.ConfigAccessTrade);
                }
                else configAccessTradeDAL.Update(config.ConfigAccessTrade);

                config.ConfigAccessTrade.AccessKey = model.AccessTradeAccessKey;
                config.ConfigAccessTrade.SecretKey = model.AccessTradeSecretKey;
                config.ConfigAccessTrade.DeepLink = model.AccessTradeDeepLink;
                config.ConfigAccessTrade.SourceId = model.AccessTradeSourceId;
            }
            if (config.ConfigMemberPoint != null || model.ProductAttribute > 0 || model.TranferPrice > 0 || model.OrderPercent > 0)
            {
                if (config.ConfigMemberPoint == null)
                {
                    config.ConfigMemberPoint = new ConfigMemberPoint();
                    configMemberPointDAL.Add(config.ConfigMemberPoint);
                }
                else configMemberPointDAL.Update(config.ConfigMemberPoint);

                config.ConfigMemberPoint.OrderPercent = model.OrderPercent;
                config.ConfigMemberPoint.ProductAttribute = model.ProductAttribute;
                config.ConfigMemberPoint.TranferPrice = model.TranferPrice;
            }

            this.SaveChanges();
        }
        #endregion

        #region Language
        public IList<LANGUAGEModel> GetLanguage(int companyId = 0)
        {
            var query = languageDAL.GetAll()
                            .Select(e => new LANGUAGEModel
                            {
                                ID = e.Id,
                                NAME = e.Name,
                                ISDEFAULT = false
                            });

            var data = query.ToList();
            if (companyId == 0) return data;
            else
            {
                var queryCompany = companyLanguageDAL.GetAll()
                                .Where(e => e.CompanyId == companyId)
                                .Select(e => new LANGUAGEModel
                                {
                                    ID = e.LanguageId,
                                    ISDEFAULT = false
                                });
                var temp = queryCompany.ToList();
                foreach(var lc in temp)
                {
                    var lang = data.FirstOrDefault(e => e.ID == lc.ID);
                    if (lang != null) lc.NAME = lang.NAME;
                    else lc.NAME = lc.ID;
                }

                return temp ;
            }            
        }
        #endregion

        #region Domain
        public IQueryable<string> GetDomains(int companyId)
        {
            var query = domainDAL.GetAll()
                                .Where(e => e.CompanyId == companyId).
                                Select(e => e.Domain);
            return query;
        }

        public CompanyConfigModel GetCompanyByDomain(string domain)
        {
            var config = domainDAL.GetAll()
                            .Where(e => e.Domain == domain)
                            .Select(e => new CompanyConfigModel
                            {
                                ID = e.CompanyId,
                                Language = e.LanguageId == null ? e.Company.WebConfig.DefaultLanguage : e.LanguageId,
                                Template = e.Company.WebConfig.Template
                            }).FirstOrDefault();

            return config;
        }
        #endregion

        #region third party
        public IQueryable<ThirdPartyModel> GetThirdPartyByWebConfigId(int companyId)
        {
            var query = this.thirdPartyDAL.GetAll().Where(e => e.Item.CompanyId == companyId)
                .Select(e => new ThirdPartyModel()
                {
                    Id = e.Id,
                    ContentHTML = e.ContentHTML,
                    PositionName = e.PositionName,
                    TemplateName = e.TemplateName,
                    ThirdPartyName = e.ThirdPartyName,
                    IsPublished = e.Item.IsPublished
                });

            return query;
        }

        public void SaveThirdParty(ThirdPartyModel dto, int companyId, int userId)
        {
            //existed
            var obj = this.thirdPartyDAL.Get(c => c.Id == dto.Id);
            var item = this.itemDAL.Get(e => e.Id == dto.Id && e.CompanyId == companyId);

            if (obj != null && item != null) //create a new 
            {
                obj.Item = item;
                obj.Item.ModifyByUser = userId;
                obj.Item.ModifyDate = DateTime.Now;
                this.thirdPartyDAL.Update(obj);
            }
            else
            {
                //insert
                obj = new ThirdParty();
                obj.Item = new Item();
                obj.Item.CompanyId = companyId;
                obj.Item.ModifyByUser = userId;
                obj.Item.ModifyDate = DateTime.Now;

                this.thirdPartyDAL.Add(obj);
            }
            
            obj.Item.IsPublished = dto.IsPublished;
            obj.TemplateName = dto.TemplateName;
            obj.PositionName = dto.PositionName;
            obj.ThirdPartyName = dto.ThirdPartyName;
            obj.ContentHTML = dto.ContentHTML;

            this.SaveChanges();
        }

        public bool RemoveThirdParty(int id, int companyId)
        {
            try
            {
                    var grp = this.thirdPartyDAL.Get(o => o.Id == id && o.Item.CompanyId == companyId);
                    if (grp != null)
                    {
                        this.thirdPartyDAL.Delete(grp);
                    }

                this.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region MenuShortcut
        public IQueryable<MenuShortcutModel> GetMenuShortcut(int companyId)
        {
            var query = this.shorcutDAL.GetAll().Where(e => e.CompanyId == companyId)
                .OrderBy(e => e.No)
                .Select(e => new MenuShortcutModel()
                {
                    Id = e.Id,
                    CategoryId = e.CategoryId,
                    CategoryType = e.CategoryType,
                    CompanyId = e.CompanyId,
                    IsCategories = e.IsCategories,
                    Name = e.Name,
                    No = e.No,
                    ParentId = e.ParentId
                });

            return query;
        }

        public void SaveMenuShortcut(MenuShortcutModel dto, int companyId)
        {
            //existed
            var obj = this.shorcutDAL.Get(c => c.Id == dto.Id && c.CompanyId == companyId);

            if (obj == null)
            {
                //insert
                obj = new MenuShortcut();
                obj.CompanyId = companyId;

                this.shorcutDAL.Add(obj);
            }
            else this.shorcutDAL.Update(obj);

            if (dto.CategoryId > 0)
            {
                var cat = categoryDAL.GetAll().FirstOrDefault(e => e.Id == dto.CategoryId);
                if (cat != null)
                {
                    obj.CategoryId = cat.Id;
                    obj.CategoryType = cat.TypeId;
                }
            }

            obj.No = dto.No;
            obj.Name = dto.Name;
            obj.IsCategories = dto.IsCategories;
            obj.ParentId = dto.ParentId;
            
            this.SaveChanges();
        }

        public bool RemoveMenuShortcut(int id, int companyId)
        {
            try
            {
                var grp = this.shorcutDAL.Get(o => o.Id == id && o.CompanyId == companyId);
                if (grp != null)
                {
                    this.shorcutDAL.Delete(grp);
                }

                this.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }
}
