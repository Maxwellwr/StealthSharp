using System.Collections.Generic;

namespace StealthSharp.Serialization
{
    public class ReflectionMetadata
    {
        public IReadOnlyList<TcpProperty> Properties { get; }
        public int MetaLength { get; }
        public TcpProperty BodyProperty { get; }
        public TcpProperty LengthProperty { get; }
        public TcpProperty ComposeProperty { get; }
    }
}