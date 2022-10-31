using DevExpress.DashboardCommon;
using DevExpress.DashboardWeb;
using DevExpress.Data.Filtering;
using DevExpress.DataAccess;
using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.DataAccess.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.FileProviders;
using System;

namespace AspNetCoreDashboard {
    public class MultiTenantDashboardConfigurator : DashboardConfigurator {
        private readonly IHttpContextAccessor contextAccessor;
        private IFileProvider fileProvider { get; }

        public MultiTenantDashboardConfigurator(IWebHostEnvironment hostingEnvironment, IHttpContextAccessor contextAccessor) {
            this.contextAccessor = contextAccessor;
            this.fileProvider = hostingEnvironment.ContentRootFileProvider;

            SetDashboardStorage(new DashboardFileStorage(fileProvider.GetFileInfo("App_Data/Dashboards").PhysicalPath));
            SetDataSourceStorage(new CustomDataSourceStorage());

            CustomParameters += DashboardConfigurator_CustomParameters;
            DataLoading += DashboardConfigurator_DataLoading;
            CustomFilterExpression += DashboardConfigurator_CustomFilterExpression;
            ConfigureDataConnection += DashboardConfigurator_ConfigureDataConnection;
        }

        // Configure user-specific data caching
        private void DashboardConfigurator_CustomParameters(object sender, CustomParametersWebEventArgs e) {
            var userId = contextAccessor.HttpContext.Session.GetString("CurrentUser").GetHashCode();
            e.Parameters.Add(new Parameter("UserId", typeof(string), userId));
        }

        // Conditional data loading for ObjectDataSource
        private void DashboardConfigurator_DataLoading(object sender, DataLoadingWebEventArgs e) {
            var userName = contextAccessor.HttpContext.Session.GetString("CurrentUser");

            if (e.DataId == "odsSales") {
                if (userName == "Admin") {
                    e.Data = SalesData.GetSalesData();
                } else if (userName == "User") {
                    e.Data = SalesData.GetSalesDataLimited();
                }
            }
        }

        // Conditional data loading for other datasource types
        private void DashboardConfigurator_ConfigureDataConnection(object sender, ConfigureDataConnectionWebEventArgs e) {
            var userName = contextAccessor.HttpContext.Session.GetString("CurrentUser");

            if (e.ConnectionName == "sqlCategories") {
                var sqlConnectionParameters = e.ConnectionParameters as CustomStringConnectionParameters;
                if (userName == "Admin") {
                    sqlConnectionParameters.ConnectionString = @"XpoProvider=SQLite; Data Source=App_Data/nwind_admin.db;";
                } else if (userName == "User") {
                    sqlConnectionParameters.ConnectionString = @"XpoProvider=SQLite; Data Source=App_Data/nwind_user.db;";
                }
            } else if (e.ConnectionName == "jsonCustomers") {
                if (e.DashboardId == "JSON") {
                    string jsonFileName = "";
                    if (userName == "Admin") {
                        jsonFileName = "customers_admin.json";
                    } else if (userName == "User") {
                        jsonFileName = "customers_user.json";
                    }
                    var fileUri = new Uri(fileProvider.GetFileInfo("App_Data/").PhysicalPath + jsonFileName, UriKind.RelativeOrAbsolute);
                    ((JsonSourceConnectionParameters)e.ConnectionParameters).JsonSource = new UriJsonSource(fileUri);
                } else if (e.DashboardId == "JSONFilter") {
                    var remoteUri = new Uri(GetBaseUrl() + "Home/GetCustomers");
                    var jsonSource = new UriJsonSource(remoteUri);
                    if (userName == "User") {
                        jsonSource.QueryParameters.AddRange(new[] {
                            // "CountryPattern" is a dashboard parameter whose value is used for the "CountryStartsWith" query parameter
                            new QueryParameter("CountryStartsWith", typeof(Expression), new Expression("Parameters.CountryPattern"))
                        });
                    } else if (userName != "Admin") {
                        throw new ApplicationException("You are not authorized to access JSON data.");
                    }
                    ((JsonSourceConnectionParameters)e.ConnectionParameters).JsonSource = jsonSource;                                        
                    

                }
            } else if (e.ConnectionName == "excelSales") {
                var excelConnectionParameters = e.ConnectionParameters as ExcelDataSourceConnectionParameters;
                if (userName == "Admin") {
                    excelConnectionParameters.FileName = fileProvider.GetFileInfo("App_Data/sales_admin.xlsx").PhysicalPath;
                } else if (userName == "User") {
                    excelConnectionParameters.FileName = fileProvider.GetFileInfo("App_Data/sales_user.xlsx").PhysicalPath;
                }
            } else if (e.ConnectionName == "extractSalesPerson") {
                if (userName == "Admin") {
                    ((ExtractDataSourceConnectionParameters)e.ConnectionParameters).FileName = fileProvider.GetFileInfo("App_Data/SalesPersonExtract.dat").PhysicalPath;
                } else {
                    throw new ApplicationException("You are not authorized to access Extract data.");
                }
            }
        }

        // Custom data filtering for SqlDataSource
        private void DashboardConfigurator_CustomFilterExpression(object sender, CustomFilterExpressionWebEventArgs e) {
            var userName = contextAccessor.HttpContext.Session.GetString("CurrentUser");

            if (e.DashboardId == "SQLFilter" && e.QueryName == "Categories") {
                if (userName == "User") {
                    e.FilterExpression = CriteriaOperator.Parse("StartsWith([CategoryName], 'C')");
                }            
            }
        }

        private string GetBaseUrl() {
            var request = contextAccessor.HttpContext.Request;
            return UriHelper.BuildAbsolute(request.Scheme, request.Host);
        }
    }
}