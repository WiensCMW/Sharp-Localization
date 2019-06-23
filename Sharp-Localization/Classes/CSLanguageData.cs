namespace Sharp_Localization
{
    public class CSLanguageData
    {
        // https://docs.microsoft.com/en-us/bingmaps/rest-services/common-parameters-and-types/supported-culture-codes
        public string CultureCode;
        public string Value;

        public CSLanguageData(string cultureCode, string value)
        {
            CultureCode = cultureCode;
            Value = value;
        }
    }
}