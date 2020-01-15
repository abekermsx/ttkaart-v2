namespace ttkaart.Models
{

    public class SortOptions
    {
        public string DisplayField;
        public string SortField;
        public string DefaultSortOrder;
        public bool IsDefaultSortColumn;
        public string Attributes;

        public SortOptions(string _displayField, string _sortField = "", string _defaultSortOrder = "Oplopend", bool _isDefaultSortColumn = false, string _attributes = "")
        {
            DisplayField = _displayField;
            SortField = _sortField;
            DefaultSortOrder = _defaultSortOrder;
            IsDefaultSortColumn = _isDefaultSortColumn;
            Attributes = _attributes;
        }
    }

}