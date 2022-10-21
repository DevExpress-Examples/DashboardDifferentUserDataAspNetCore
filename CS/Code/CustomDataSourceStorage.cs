using DevExpress.DashboardCommon;
using DevExpress.DashboardWeb;
using DevExpress.DataAccess.Excel;
using DevExpress.DataAccess.Sql;
using System.Collections.Generic;
using System.Xml.Linq;

public class CustomDataSourceStorage : IDataSourceStorage {
    private Dictionary<string, XDocument> documents = new Dictionary<string, XDocument>();

    private const string sqlDataSourceId = "SQL Data Source";
    private const string jsonDataSourceId = "JSON Data Source";
    private const string odsDataSourceId = "Object Data Source";
    private const string excelDataSourceId = "Excel Data Source";
    private const string extractDataSourceId = "Extract Data Source";

    public CustomDataSourceStorage() {
        DashboardSqlDataSource sqlDataSource = new DashboardSqlDataSource(sqlDataSourceId, "sqlCategories");
        SelectQuery query = SelectQueryFluentBuilder
            .AddTable("Categories")
            .SelectAllColumnsFromTable()
            .Build("Categories");
        sqlDataSource.Queries.Add(query);

        DashboardJsonDataSource jsonDataSource = new DashboardJsonDataSource(jsonDataSourceId) {
            RootElement = "Customers",
            ConnectionName = "jsonCustomers"
        };

        DashboardObjectDataSource objDataSource = new DashboardObjectDataSource(odsDataSourceId) {
            DataId = "odsSales"
        };

        DashboardExtractDataSource extractDataSource = new DashboardExtractDataSource(extractDataSourceId) {
            ConnectionName = "extractSalesPerson"
        };

        DashboardExcelDataSource excelDataSource = new DashboardExcelDataSource(excelDataSourceId) {
            ConnectionName = "excelSales",
            SourceOptions = new ExcelSourceOptions(new ExcelWorksheetSettings("Sheet1"))
        };

        documents[sqlDataSourceId] = new XDocument(sqlDataSource.SaveToXml());
        documents[jsonDataSourceId] = new XDocument(jsonDataSource.SaveToXml());
        documents[odsDataSourceId] = new XDocument(objDataSource.SaveToXml());
        documents[extractDataSourceId] = new XDocument(extractDataSource.SaveToXml());
        documents[excelDataSourceId] = new XDocument(excelDataSource.SaveToXml());
    }

    public XDocument GetDataSource(string dataSourceID) {
        return documents[dataSourceID];
    }

    public IEnumerable<string> GetDataSourcesID() {
        return documents.Keys;
    }
}