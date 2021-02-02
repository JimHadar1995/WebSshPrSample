using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Library.Common.Types.JsonConverters.Newtonsoft;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Library.Common.Kafka
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class KafkaService : IKafkaService
    {
        private readonly ILogger<KafkaService> _msLogger;
        private readonly KafkaConnection _kafkaConnectionInfo;

        private readonly IProducer<string, string>? _producer;

        /// <summary>
        /// Initializes a new instance of the <see cref="KafkaService"/> class.
        /// </summary>
        public KafkaService(
            ILogger<KafkaService> msLogger,
            IOptionsMonitor<KafkaConnection> kafkaConnectionInfo)
        {
            _msLogger = msLogger;
            _kafkaConnectionInfo = kafkaConnectionInfo.CurrentValue;
            // Отправляем данные в kafka
            var producerConfig = new ProducerConfig
            {
                BootstrapServers = _kafkaConnectionInfo.BootStrapServers
            };

            if (_kafkaConnectionInfo.Enabled)
            {
                _producer = new ProducerBuilder<string, string>(producerConfig).Build();
            }
        }


        /// <inheritdoc />
        public async Task SendInfo<T>(string topicName, string key, List<T> data)
        {
            if (!_kafkaConnectionInfo.Enabled)
                return;

            if (!data.Any())
                return;

            try
            {
                foreach (var json in data)
                {
                    var serializedData = Serialize(json);
                    using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(_kafkaConnectionInfo.KafkaProduceTimeout));
                    var dr = await _producer!.ProduceAsync(topicName,
                            new Message<string, string>
                            {
                                Value = serializedData,
                                Key = key,
                            }, cts.Token)
                        .ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                _msLogger.LogError(e, e.Message);
            }
        }


        /// <inheritdoc />
        public async Task SendInfo<T>(string topicName, Expression<Func<T, string>> keyExpr, T data)
        {
            if (!_kafkaConnectionInfo.Enabled)
                return;

            try
            {
                var serializedData = Serialize(data);
                using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(_kafkaConnectionInfo.KafkaProduceTimeout));
                await _producer!.ProduceAsync(topicName,
                        new Message<string, string>
                        {
                            Value = serializedData,
                            Key = keyExpr.Compile().Invoke(data),
                        }, cts.Token)
                    .ConfigureAwait(false);
            }
            catch (Exception e)
            {
                _msLogger.LogError(e, e.Message);
            }
        }

        /// <inheritdoc />
        public async Task SendInfo<T>(
            string topicName,
            string key,
            T data)
        {
            if (!_kafkaConnectionInfo.Enabled)
                return;

            try
            {
                var serializedData = Serialize(data);
                using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(_kafkaConnectionInfo.KafkaProduceTimeout));
                await _producer!.ProduceAsync(topicName,
                        new Message<string, string>
                        {
                            Value = serializedData,
                            Key = key,
                        }, cts.Token)
                    .ConfigureAwait(false);
            }
            catch (Exception e)
            {
                _msLogger.LogError(e, e.Message);
            }
        }

        private string Serialize(object? data)
        {
            if (data == null)
                return string.Empty;
            var jsonSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            jsonSettings.Converters.Add(new JsonDocumentConverter());
            jsonSettings.Converters.Add(new StringEnumConverter());
            return JsonConvert.SerializeObject(data, jsonSettings);
        }
    }
}
