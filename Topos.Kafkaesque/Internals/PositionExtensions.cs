﻿using Topos.Consumer;

namespace Topos.Internals
{
    static class PositionExtensions
    {
        static readonly KafkaesquePosition DefaultKafkaesquePosition = new KafkaesquePosition(-1, -1);

        public static KafkaesquePosition ToKafkaesquePosition(this Position positionOrNull) => ToKafkaesquePosition((Position?)positionOrNull);

        public static KafkaesquePosition ToKafkaesquePosition(this Position? positionOrNull)
        {
            if (positionOrNull == null) return DefaultKafkaesquePosition;

            var position = positionOrNull.Value;

            if (position.IsDefault) return DefaultKafkaesquePosition;

            var offset = (ulong)position.Offset;

            return new KafkaesquePosition((int)(offset >> 32), (int)(offset & 0xffffffff));
        }

        public static Position ToPosition(this KafkaesquePosition position, string topic, int partition)
        {
            var fileNumber = (ulong)position.FileNumber;
            var bytePosition = (ulong)position.BytePosition;

            return new Position(topic, partition, (long)(fileNumber<<32 | bytePosition));
        }
    }
}