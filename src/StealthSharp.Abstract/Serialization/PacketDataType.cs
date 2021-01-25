namespace StealthSharp.Serialization
{
    public enum PacketDataType: byte
    {
        /// <summary>
        /// Metadata property or field
        /// </summary>
        MetaData,

        /// <summary>
        /// Packet id property or field
        /// </summary>
        Id,

        /// <summary>
        /// Packet length property or field
        /// </summary>
        Length,

        /// <summary>
        /// Packet body
        /// </summary>
        Body,
        
        /// <summary>
        /// Field for TypeMapper
        /// </summary>
        TypeMapper
    }
}