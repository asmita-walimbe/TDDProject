namespace TDDProject
{
    public static class Constants
    {
        public const string HealthCheckApi = "/health";
        public static string GetById = "api/user/{0}";
        public const string UserApi = "api/user";

        public static class UserValidationMessage
        {
            public const string Name = "Name is required";
            public const string Address = "Address is required";
        }
    }
}
