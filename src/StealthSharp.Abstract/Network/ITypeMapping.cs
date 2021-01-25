namespace StealthSharp.Network
{
    public interface ITypeMapping<out T>
        where T: unmanaged
    {
        T TypeId { get; }
    }
}