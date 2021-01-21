namespace Library.Common.Kafka
{
    /// <summary>
    /// Manages connections to the Kafka
    /// </summary>
    public class KafkaConnection
    {
        /// <summary>
        /// BootStrapServers.
        /// </summary>
        public string BootStrapServers { get; set; } = string.Empty;

        /// <summary>
        /// Таймаут, после которого идет отмена отправки данных в kafka.
        /// В секундах
        /// </summary>
        public int KafkaProduceTimeout { get; set; } = 5;

        /// <summary>
        /// Включена ли отправка данных в kafka
        /// </summary>
        public bool Enabled { get; set; } = false;
    }
}
