namespace TodoList.Api.Configurations;

public class AllowedOriginsSettings
{
    public const string SectionName = "AllowedOrigins";

    public string Frontend { get; set; } = string.Empty;
}
