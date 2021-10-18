namespace Tracer.Core.Services
{
    public interface ISerializer<in T>
    {
        string Serialize(T data);
    }
}