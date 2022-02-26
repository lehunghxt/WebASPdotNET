namespace Web.Business
{
    using System;
    using System.Linq;
    using log4net;
    using Data.DataAccess;
    using Data;
    using Library;
    using Web.Model;
    using System.Collections.Generic;
    using System.IO;

    public class CustomerBLL : BaseBLL
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(CustomerBLL));

        private readonly ICustomerDAL _customerDAL;
        private readonly ICustomerPointDAL _customerPointDAL;
        private readonly IWebConfigDAL _webconfigDAL;
        private readonly ICompanyUserDAL _userDAL;
        private readonly IConfigMemberPointDAL configPointDAL;
        private readonly IOrderDAL orderDAL;
        private readonly IOrderProductDAL orderProductDAL;
        private readonly IArticleLanguageDAL articleDAL;

        #region Constructor

        public CustomerBLL()
            : this(null)
        {
        }

        public CustomerBLL(string connectionString)
            : base(connectionString)
        {
            this._customerDAL = new CustomerDAL(DatabaseFactory);
            _customerPointDAL = new CustomerPointDAL(DatabaseFactory);
            this._webconfigDAL = new WebConfigDAL(DatabaseFactory);
            _userDAL = new CompanyUserDAL(DatabaseFactory);
            configPointDAL = new ConfigMemberPointDAL(DatabaseFactory);
            orderDAL = new OrderDAL(DatabaseFactory);
            orderProductDAL = new OrderProductDAL(DatabaseFactory);
            articleDAL = new ArticleLanguageDAL(DatabaseFactory);
        }

        #endregion

        #region Function
        public int ValidateUser(int companyId, string phone_mail, string password)
        {
            var pass = this._customerDAL.GetAll().Where(e => (e.Phone == phone_mail || e.Email == phone_mail) && e.CompanyId == companyId)
                            .Select(e => e.Password).FirstOrDefault();
            if (string.IsNullOrEmpty(pass)) return -1;
            if (pass != password) return 0;
            return 1;
        }

        public void RegisCustomer(int companyId, string name, string address, string email, string phone, DateTime birthday, string password, string title, string content, Stream stream, string fileName, bool isSendMail = false)
        {
            if (this._customerDAL.GetAll().Any(e => e.CompanyId == companyId && e.Email == email)) throw new BusinessException("Email đã có người khác sử dụng");
            if (this._customerDAL.GetAll().Any(e => e.CompanyId == companyId && e.Phone == phone)) throw new BusinessException("Số điện thoại đã có người khác sử dụng");

            var customer = new Customer();
            customer.Email = email;
            customer.Phone = phone;
            customer.Address = address;
            customer.Name = name;
            customer.CompanyId = companyId;
            customer.Birthday = birthday;
            customer.CreateDate = DateTime.Now;
            customer.Password = password;
            this._customerDAL.Add(customer);
            this.SaveChanges();

            if (isSendMail)
            {
                var config = this._webconfigDAL.GetAll().FirstOrDefault(e => e.Id == companyId);
                if (config != null)
                {
                    this.SendEmail(config.MailServer, config.MailEnableSSL ?? false, config.MailAccount, config.MailAccount, config.MailPassword, config.MailPort ?? 25, title, content, stream, fileName);
                }
            }
        }

        public void UpdateCustomer(int companyId, int customerId, string name, string address, string email, string phone, DateTime birthday, string oldPass, string newPass, string title, string content, Stream stream, string fileName, bool isSendMail = false)
        {
            var customer = this._customerDAL.GetAll().FirstOrDefault(e => e.Id == customerId && e.CompanyId == companyId);
            if (customer == null) throw new BusinessException("Người dùng không tồn tại");
            if (this._customerDAL.GetAll().Any(e => e.Id != customer.Id && e.CompanyId == companyId && e.Email == email)) throw new BusinessException("Email đã có người khác sử dụng");
            if (this._customerDAL.GetAll().Any(e => e.Id != customer.Id && e.CompanyId == companyId && e.Phone == phone)) throw new BusinessException("Số điện thoại đã có người khác sử dụng");

            if (customer.Email != email) customer.Email = email;
            if (customer.Phone != phone) customer.Phone = phone;
            if (customer.Address != address) customer.Address = address;
            if (customer.Name != name) customer.Name = name;
            if (customer.Birthday != birthday) customer.Birthday = birthday;
            customer.CreateDate = DateTime.Now;

            if (customer.Password == oldPass && !string.IsNullOrEmpty(newPass)) customer.Password = newPass;

            this._customerDAL.Update(customer);
            this.SaveChanges();

            if (isSendMail)
            {
                var config = this._webconfigDAL.GetAll().FirstOrDefault(e => e.Id == companyId);
                if (config != null)
                {
                    this.SendEmail(config.MailServer, config.MailEnableSSL ?? false, config.MailAccount, config.MailAccount, config.MailPassword, config.MailPort ?? 25, title, content, stream, fileName);
                }
            }
        }

        public IQueryable<CustomerModel> GetCustomers(int companyId)
        {
            var cuss = this._customerDAL.GetAll().Where(e => e.CompanyId == companyId)
                            .Select(e => new CustomerModel
                            {
                                Id = e.Id,
                                Address = e.Address,
                                Email = e.Email,
                                Name = e.Name,
                                Phone = e.Phone,
                                Point = e.Point,
                                BirthDay = e.Birthday,
                                CreateDate = e.CreateDate
                            });

            return cuss;
        }

        public CustomerModel GetCustomer(int id)
        {
            var cus = this._customerDAL.GetAll().Where(e => e.Id == id)
                            .Select(e => new CustomerModel
                            {
                                Id = e.Id,
                                Address = e.Address,
                                Email = e.Email,
                                Name = e.Name,
                                Phone = e.Phone,
                                Point = e.Point,
                                BirthDay = e.Birthday,
                                CreateDate = e.CreateDate
                            }).FirstOrDefault();
            if (cus != null)
            {
                var config = configPointDAL.GetById(cus.CompanyId);
                if (config != null)
                {
                    cus.TranferPrice = config.TranferPrice;
                }
            }

            return cus;
        }
        public CustomerModel GetCustomer(int companyId, string phone_mail)
        {
            var cus = this._customerDAL.GetAll().Where(e => (e.Phone == phone_mail || e.Email == phone_mail) && e.CompanyId == companyId)
                            .Select(e => new CustomerModel
                            {
                                Id = e.Id,
                                Address = e.Address,
                                Email = e.Email,
                                Name = e.Name,
                                Phone = e.Phone,
                                Point = e.Point ?? 0,
                                BirthDay = e.Birthday,
                                CreateDate = e.CreateDate
                            }).FirstOrDefault();
            var config = configPointDAL.GetById(companyId);
            if (config != null && cus != null)
            {
                cus.TranferPrice = config.TranferPrice;
            }

            return cus;
        }

        public IQueryable<CustomerOrderModel> GetCustomerOrders(int companyId, DateTime? fromDate, DateTime? toDate)
        {
            var query = this.orderDAL.GetAll().Where(e => e.CompanyId == companyId && e.Status == 3);

            var customerQuery = query.GroupBy(e => e.CustomerPhone)
                .Select(e => new CustomerOrderModel
                {
                    CustomerName = e.FirstOrDefault().CustomerName,
                    CustomerPhone = e.FirstOrDefault().CustomerPhone,
                    CustomerAddress = e.FirstOrDefault().CustomerAddress,
                    TotalDue = e.Sum(o => o.Due),
                    CountProducts = e.Sum(o => o.OrderProducts.Sum(p => p.Quantity)),
                    LastBuyDate = e.Max(o => o.LastUpdate)
                });

            if (fromDate != null) customerQuery = customerQuery.Where(e => fromDate <= e.LastBuyDate);
            if (toDate != null) customerQuery = customerQuery.Where(e => e.LastBuyDate <= toDate);

            customerQuery = customerQuery.OrderByDescending(o => o.TotalDue).ThenByDescending(o => o.LastBuyDate);

            return customerQuery;
        }

        public IList<CustomerProductModel> GetCustomerProducts(int companyId, string phone, string language)
        {
            var orderIds = this.orderDAL.GetAll()
                .Where(e => e.CompanyId == companyId && e.Status == 3)
                .Where(e => e.CustomerPhone == phone)
                .Select(e => e.Id)
                .ToList();

            var query = this.orderProductDAL.GetAll()
                .Where(e => orderIds.Contains(e.OrderId))
                .GroupBy(e => e.ProductId)
                .Select(e => new CustomerProductModel
                {
                    Id = e.FirstOrDefault().ProductId,
                    CountProducts = e.Sum(o => o.Quantity),
                });

            var products = query.ToList();
            var productIds = products.Select(e => e.Id).ToList();

            var langs = articleDAL.GetAll()
                                .Where(e => productIds.Contains(e.ArticleId) && e.LanguageId == language)
                                .Select(e => new
                                {
                                    Id = e.ArticleId,
                                    e.Title,
                                    e.Article.Image,
                                }).ToList();

            foreach (var product in products)
            {
                var lang = langs.FirstOrDefault(e => e.Id == product.Id);
                if (lang != null)
                {
                    product.ProductName = lang.Title;
                    product.ProductImage = lang.Image;
                }
            }

            return products;
        }

        public string ForgetPassword(string email)
        {
            var account = this._customerDAL.GetAll()
                        .FirstOrDefault(e => e.Email == email);
            if (account == null) return string.Empty;

            var newPass = GenerateRandomCode.RandomCode(6);

            account.Password = newPass.Trim().EnCodeMD5();
            this._customerDAL.Update(account);
            this.SaveChanges();
            return newPass;
        }
        #endregion

        #region Point
        public IQueryable<OrderPointModel> GetPointByFinishOrders(int customerId)
        {
            var query = this._customerPointDAL.GetAll().Where(e => e.CustomerId == customerId);

            var orders = query.Select(e => new OrderPointModel
            {
                Id = e.OrderId,
                Addition = e.Addition,
                Subtraction = e.Subtraction,
                Due = e.Order.Due,
                Status = e.Order.Status,
                Date = e.Order.CreateDate
            });

            return orders;
        }
        public IList<OrderPointModel> GetPointByNewOrders(int customerId)
        {
            var query = this.orderDAL.GetAll().Where(e => e.CustomerId == customerId && e.Status != 3 && e.Status != 4);

            var orders = query.Select(e => new OrderPointModel
            {
                Id = e.Id,
                Subtraction = e.Point ?? 0,
                Addition = 0,
                Due = e.Due,
                Status = e.Status,
                Date = e.CreateDate
            }).ToList();

            return orders;
        }
        #endregion

        #region private method

        private void SendEmail(string host, bool enableSSL, string SendFrom, string SendTo, string Password, int port, string Subject, string Body, Stream stream, string fileName)
        {
            try
            {
                MailManager mail = new MailManager();

                mail.FileStream = stream;
                mail.FileName = fileName;
                mail.EnableSSL = enableSSL;
                mail.Host = host;
                mail.Port = port;
                mail.From = SendFrom;
                mail.Password = Password;
                mail.To = SendTo;
                mail.Title = Subject;
                mail.Content = Body;
                mail.SendEmail();
            }
            catch (Exception ex)
            {
                throw new BusinessException("Lỗi Email: " + ex.Message);
            }
        }

        #endregion
    }
}