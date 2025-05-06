namespace LibrarySystem.Abstractions;

public record Error(string Code,string Description,int? StatusCode)
{
    public static readonly Error none=new(string.Empty,string.Empty,null);

    
}
