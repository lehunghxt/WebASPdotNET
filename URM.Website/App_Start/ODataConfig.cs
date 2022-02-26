using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web.Http;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Extensions;
using URM.Website.Odata;

namespace URM.Website
{
    public static class ODataConfig
    {
        private const string namespaceModel = "URM.Website.Odata";
        /// <sURMmary>
        ///     The controller name convention.
        /// </sURMmary>
        private static readonly Regex controllerNameConvention = new Regex(@"^(\w+)Controller$", RegexOptions.Compiled);

        /// <sURMmary>
        ///     The model builder cache.
        /// </sURMmary>
        private static IDictionary<string, ODataConventionModelBuilder> modelBuilderCache = new Dictionary<string, ODataConventionModelBuilder>();

        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes(); //This has to be called before the following OData mapping, so also before WebApi mapping
            
            Type[] entitySetControllerTypes = GetEntitySetControllerTypes().ToArray();
            foreach (Type entitySetControllerType in entitySetControllerTypes)
            {
                RegisterEntitySet(entitySetControllerType, ResolveModelBuilder(entitySetControllerType));
                //RegisterNavigationEntitySet(entitySetControllerType, entitySetControllerTypes);
            }

            foreach (string keyNamespace in modelBuilderCache.Keys)
            {
                ODataConventionModelBuilder modelBuilder = modelBuilderCache[keyNamespace];
                if (keyNamespace == "odata")
                {
                    config.Routes.MapODataRoute(
                        "OData",
                        "odata",
                        modelBuilder.GetEdmModel());
                }
                else
                {
                    config.Routes.MapODataRoute(
                        "OData" + keyNamespace,
                        "odata/" + keyNamespace.Replace(".", "/"),
                        modelBuilder.GetEdmModel());
                }
            }

            ////ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            ////config.Routes.MapODataServiceRoute("ODataRoute", "odata/VienPhi", GetEdmModel(), new DefaultODataBatchHandler(GlobalConfiguration.DefaultServer));

            config.AddODataQueryFilter();
        }

        //
        //private static IEdmModel GetEdmModel()
        //{
        //    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
        //    builder.Namespace = "DataTransferObject";
        //    builder.ContainerName = "HISEntities";
        //    builder.EntitySet<LoaiVienPhiDto>("LoaiVienPhi");
        //    var edmModel = builder.GetEdmModel();
        //    return edmModel;
        //}

        /// <sURMmary>
        ///     The get entity set controller types.
        /// </sURMmary>
        /// <returns>
        ///     The <see cref="IEnumerable{Type}" />.
        /// </returns>
        private static IEnumerable<Type> GetEntitySetControllerTypes()
        {
            Assembly controllerAssembly = typeof(ODataConfig).Assembly;
            Type entitySetControllerType = typeof(OdataBaseController<,>);
            return
                controllerAssembly.GetTypes()
                    .Where(
                        x =>
                        x.BaseType != null && x.BaseType.IsGenericType
                        && entitySetControllerType == x.BaseType.GetGenericTypeDefinition() && x.Namespace != null
                        && x.Namespace.StartsWith(namespaceModel))
                    .ToList();
        }

        private static void RegisterEntitySet(
            Type entitySetControllerType,
            ODataModelBuilder modelBuilder,
            string containerName = null)
        {
            Type entityType = entitySetControllerType.BaseType.GetGenericArguments()[0];

            MethodInfo methodEntitySet = typeof(ODataConventionModelBuilder).GetMethod("EntitySet");
            MethodInfo genericMethodEntitySet = methodEntitySet.MakeGenericMethod(entityType);
            if (containerName == null)
            {
                Match match = controllerNameConvention.Match(entitySetControllerType.Name);
                if (match.Success)
                {
                    containerName = match.Groups[1].Value;
                }
                else
                {
                    throw new InvalidOperationException(
                        string.Format(
                            "EntitySet {0} does not match naming convention",
                            entitySetControllerType.FullName));
                }
            }

            genericMethodEntitySet.Invoke(modelBuilder, new object[] { containerName });
        }

        private static ODataConventionModelBuilder ResolveModelBuilder(Type entitySetControllerType)
        {
            string packageKey = "odata";
            if(entitySetControllerType.Namespace != namespaceModel) entitySetControllerType.Namespace.Substring(namespaceModel.Length + 1);

            ODataConventionModelBuilder modelBuilder;

            if (!modelBuilderCache.TryGetValue(packageKey, out modelBuilder))
            {
                modelBuilder = new ODataConventionModelBuilder();
                modelBuilderCache.Add(packageKey, modelBuilder);
            }

            return modelBuilder;
        }

        private static void RegisterNavigationEntitySet(Type entitySetControllerType, Type[] entitySetControllerTypes)
        {
            if (entitySetControllerType.BaseType != null && entitySetControllerType.BaseType.IsGenericType)
            {
                Type resourceType = entitySetControllerType.BaseType.GetGenericArguments()[0];
                IEnumerable<PropertyInfo> navigationProperties =
                    resourceType.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                        .Where(x => IsComplexType(x.PropertyType));
                ODataConventionModelBuilder modelBuilder = ResolveModelBuilder(entitySetControllerType);

                foreach (PropertyInfo navigationProperty in navigationProperties)
                {
                    Type controllerOfNavigation =
                        entitySetControllerTypes.FirstOrDefault(
                            x =>
                            x.BaseType != null && x.BaseType.IsGenericType
                            && x.BaseType.GetGenericArguments()[0] == navigationProperty);
                    if (controllerOfNavigation != null)
                    {
                        RegisterEntitySet(controllerOfNavigation, modelBuilder, navigationProperty.Name);
                    }
                }
            }
        }

        private static bool IsComplexType(Type type)
        {
            return !(
                (type.IsGenericType && !IsComplexType(type.GetGenericArguments()[0])) ||
                type.IsPrimitive ||
                type == typeof(DateTime) ||
                type == typeof(string));
        }
    }
}
