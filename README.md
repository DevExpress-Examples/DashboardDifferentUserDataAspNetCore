<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/353010289/20.2.6%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T986376)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
[![](https://img.shields.io/badge/💬_Leave_Feedback-feecdd?style=flat-square)](#does-this-example-address-your-development-requirementsobjectives)
<!-- default badges end -->

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
* [DashboardConfigurator.CustomParameters](https://docs.devexpress.com/Dashboard/DevExpress.DashboardWeb.DashboardConfigurator.CustomParameters)

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

- [Dashboard for ASP.NET Core - How to implement multi-tenant Dashboard architecture](https://github.com/DevExpress-Examples/DashboardUserBasedAspNetCore)
- [Dashboard for ASP.NET Core - How to implement authentication](https://github.com/DevExpress-Examples/ASPNET-Core-Dashboard-Authentication)
- [Dashboard for MVC - How to implement multi-tenant Dashboard architecture](https://github.com/DevExpress-Examples/DashboardUserBasedMVC)
- [Dashboard for MVC - How to load and save dashboards from/to a database](https://github.com/DevExpress-Examples/mvc-dashboard-how-to-load-and-save-dashboards-from-to-a-database-t400693)
- [Dashboard for MVC - How to load different data based on the current user](https://github.com/DevExpress-Examples/DashboardDifferentUserDataMVC)
<!-- feedback -->
## Does this example address your development requirements/objectives?

[<img src="https://www.devexpress.com/support/examples/i/yes-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=DashboardDifferentUserDataAspNetCore&~~~was_helpful=yes) [<img src="https://www.devexpress.com/support/examples/i/no-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=DashboardDifferentUserDataAspNetCore&~~~was_helpful=no)

(you will be redirected to DevExpress.com to submit your response)
<!-- feedback end -->
