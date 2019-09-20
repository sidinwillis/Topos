﻿using Confluent.Kafka;
using Topos.Internals;

namespace Topos.Config
{
    public class KafkaProducerConfigurationBuilder
    {
        internal ProducerConfig Apply(ProducerConfig config)
        {
            var bootstrapServers = config.BootstrapServers;

            AzureEventHubsHelper.TrySetConnectionInfo(bootstrapServers, info =>
            {
                config.BootstrapServers = info.BootstrapServers;
                config.SaslUsername = info.SaslUsername;
                config.SaslPassword = info.SaslPassword;
            });
            
            return config;
        }
    }
}