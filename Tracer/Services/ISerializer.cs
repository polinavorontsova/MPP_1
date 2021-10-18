namespace Tracer.Services
{
    public interface ISerializer<in T>
    {
        string Serialize(T data);
    }
}