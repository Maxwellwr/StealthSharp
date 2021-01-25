namespace StealthSharp.Network
{
    public interface ICorrelation<out T> where T: unmanaged
    {
        T CorrelationId { get; }
    }
}