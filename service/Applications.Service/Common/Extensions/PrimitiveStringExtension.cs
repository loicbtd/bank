using Libraries.Web.Common.Exceptions;

namespace Applications.Service.Common.Helpers;

public static class PrimitiveStringExtension
{
    public static Guid ToGuidOrFail(this string guidAsString)
    {
        Guid guid;
        
        try
        {
            guid = Guid.Parse(guidAsString);
        }
        catch (Exception exception)
        {
            throw new UserException($"the id must be a UUID: {exception.Message}");
        }

        return guid;
    } 
}