public static class EnvironmentConstant
{
    public static readonly bool SwaggerEnabled = string.Equals(Environment.GetEnvironmentVariable("SWAGGER_ENABLED"), "true", StringComparison.OrdinalIgnoreCase);
}




