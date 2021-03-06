﻿using System.Linq;
using System.Text;
using Trl.IntegerMapper.ByteEnumerableIntegerMapper;

namespace Trl.IntegerMapper.StringIntegerMapper
{
    /// <summary>
    /// Maps equal strings to equal integers.
    /// Null and empty string is mapped to 0.
    /// </summary>
    public class StringMapper : IIntegerMapper<string>
    {
        private readonly ByteEnumerableMapper _integerMapper = new ByteEnumerableMapper();

        public ulong Map(string inputValue)
        {
            if (string.IsNullOrEmpty(inputValue))
            {
                return MapConstants.NullOrEmpty;
            }
            else
            {
                byte[] byteString = Encoding.UTF8.GetBytes(inputValue);
                return _integerMapper.Map(byteString);
            }
        }

        public string ReverseMap(ulong mappedValue)
        {
            if (mappedValue == MapConstants.NullOrEmpty)
            {
                return string.Empty;
            }
            else
            {
                byte[] stringAsByteArray = _integerMapper.ReverseMap(mappedValue).ToArray();
                return Encoding.UTF8.GetString(stringAsByteArray);
            }
        }

        public void Clear() => _integerMapper.Clear();

        public bool TryGetMappedValue(string inputValue, out ulong? mappedValue)
        {
            byte[] byteString = Encoding.UTF8.GetBytes(inputValue);
            return _integerMapper.TryGetMappedValue(byteString, out mappedValue);
        }

        public ulong MappedObjectsCount => _integerMapper.MappedObjectsCount;
    }
}
