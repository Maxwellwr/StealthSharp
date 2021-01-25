namespace StealthSharp.Network
{
    public interface ISizable<out T> where T:unmanaged
    {
        T Length { get; }
    }
}