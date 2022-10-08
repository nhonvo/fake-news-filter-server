using System.Collections.Generic;
namespace FakeNewsFilter.Utilities.Constants
{
    public class SystemConstants
    {
        public const string MainConnectionString = "DefaultConnection";
        
        public static Dictionary<string, int> MapSysTopics = new Dictionary<string, int>()
        {
            {"Technology", 23},
            {"Entertainment", 24},
            {"Business", 25},
            {"Lifestyle", 26},
            {"Travel", 27}
        };

        public static Dictionary<string, int> MapOigetitTopic = new Dictionary<string, int>()
        {
            {"Technology", 3},
            {"Entertainment", 7},
            {"Business", 4},
            {"Lifestyle", 5},
            {"Travel", 6}
        };
        
        public class AppSettings
        {
            public const string DefaultLanguageId = "DefaultLanguageId";
            public const string Token = "Token";
            public const string BaseAddress = "BaseAddress";
        }
    }
}
