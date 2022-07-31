namespace TommyAPI.Contracts
{
    public static class ApiRoutes
    {
        public const string Base = "api";
        
        
        public static class Identity
        {
            public const string Login = Base + "/Identity/login";

            public const string Register = Base + "/Identity/register";
        }
    }
}
