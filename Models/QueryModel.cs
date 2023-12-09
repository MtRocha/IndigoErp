namespace IndigoErp.Models
{
    public class QueryModel
    {
        public QueryModel(string table, string column)
        {
            Table = table;
            Column = column;
        }

        public QueryModel(string table, string column, string id)
        {
            Table = table;
            Column = column;
            Id = id;
        }

        public QueryModel(string table, string column, string filter, string order)
        {
            Table = table;
            Column = column;
            Filter = filter;
            Order = order;
        }

        public QueryModel(string table, string column, string filter, string order, string id)
        {
            Table = table;
            Column = column;
            Id = id;
            Filter = filter;
            Order = order;
        }

        public string Table { get; set; }
        public string Column { get; set; }
        public string Id { get; set; }
        public string Filter { get; set; }
        public string Order { get; set; }
    }
}