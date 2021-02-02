#nullable disable warnings

namespace Library.Common.Jaeger
{
    /// <summary>
    /// 
    /// </summary>
    public class JaegerOptions
    {
        /// <summary>
        /// Включен ли
        /// </summary>
        public bool Enabled { get; init; }

        /// <summary>
        /// Название сервиса
        /// </summary>
        public string ServiceName { get; init; }

        /// <summary>
        /// Хост
        /// </summary>
        public string UdpHost { get; init; }

        /// <summary>
        /// Порт
        /// </summary>
        public int UdpPort { get; set; }
        
        /// <summary>
        /// Максимальный размер пакета
        /// </summary>
        public int MaxPacketSize { get; init; }

        /// <summary>
        /// 
        /// </summary>
        public string Sampler { get; init; }
        
        /// <summary>
        /// 
        /// </summary>
        public double MaxTracesPerSecond { get; set; } = 5;
        /// <summary>
        /// 
        /// </summary>
        public double SamplingRate { get; set; } = 0.2;
    }
}
