
<!-- default file list --> 
*Files to look at*: 
* [Startup.cs](./CS/Startup.cs)
* [MultiTenantDashboardConfigurator.cs](./CS/Code/MultiTenantDashboardConfigurator.cs)
<!-- default file list end -->

# Dashboard for ASP.NET Core - How to load different data based on the current user

This example shows how to configure the Dashboard control so that it loads data in the multi-user environment. 

You can identify a user in the current session and handle the following events to select the underlying data source:

* [DashboardConfigurator.ConfigureDataConnection](https://docs.devexpress.com/Dashboard/DevExpress.DashboardWeb.DashboardConfigurator.ConfigureDataConnection)
* [DashboardConfigurator.DataLoading](https://docs.devexpress.com/Dashboard/DevExpress.DashboardWeb.DashboardConfigurator.DataLoading)
* [DashboardConfigurator.CustomFilterExpression](https://docs.devexpress.com/Dashboard/DevExpress.DashboardWeb.DashboardConfigurator.CustomFilterExpression)

## Example Structure

You can limit access to data depending on the current user's ID. This ID is stored in the `IHttpContextAccessor.HttpContext.Session.GetString("CurrentUser")` value from session state. We use the standard [IHttpContextAccessor](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/http-context?view=aspnetcore-3.0) with dependency injection to access the HTTP context in custom dashboard configurator.

When the application starts, you see the [Index](./CS/Views/Home/Index.cshtml) view with a ComboBox in which you can select a user. When you click the **Sign in** button, the ID of the selected user is passed to the `CurrentUser` session variable and you are redirected to the [Dashboard](./CS/Views/Home/Dashboard.cshtml) view. In this view, the Web Dashboard control displays the lists of dashboards and every dashboard loads data available to the selected user. Below is a table that illustrates the user IDs and their associated datasources in this example:

| Role  | Sql | Json | Excel | Object | OLAP | Extract | Entity Framework |
| --- | --- | --- | --- | --- | --- | --- | --- |
| Admin | Categories | Customers | Bikes | Sales | Customers | Sales | Categories |
| User | Categories from different source with optional filter | Customers from different source with optional filter | Bikes from different source | Sales from different source | - | - | Categories from different source |

## Documentation

- [Manage Multi-Tenancy](https://docs.devexpress.com/Dashboard/402924/web-dashboard/dashboard-backend/manage-multi-tenancy)


## More Examples

- [ASP.NET Core Dashboard - How to implement multi-tenant Dashboard architecture](https://github.com/DevExpress-Examples/DashboardUserBasedAspNetCore)
- [ASP.NET Core Dashboard - How to implement authentication](https://github.com/DevExpress-Examples/ASPNET-Core-Dashboard-Authentication)
- [ASP.NET MVC Dashboard - How to implement multi-tenant Dashboard architecture](https://github.com/DevExpress-Examples/DashboardUserBasedMVC)
- [ASP.NET MVC Dashboard - How to load and save dashboards from/to a database](https://github.com/DevExpress-Examples/mvc-dashboard-how-to-load-and-save-dashboards-from-to-a-database-t400693)
- [ASP.NET MVC Dashboard - How to load different data based on the current user](https://github.com/DevExpress-Examples/DashboardDifferentUserDataMVC)
