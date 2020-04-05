﻿using Xunit;

using Trs.IntegerMapper.StringIntegerMapper;
using Trs.IntegerMapper.Tests.Fixtures;

namespace Trs.IntegerMapper.Tests
{
    [Collection("StringMapper tests for mapping strings to integers")]
    public class StringMapperTests
    {
        [Fact]
        public void DefaultContainerShouldContainEmptyCase()
        {
            // Act
            var mapper = new StringMapper();

            // Assert
            Assert.Equal(1u, mapper.MappedObjectsCount);
            Assert.Equal(string.Empty, mapper.ReverseMap(0));
        }

        [Fact]
        public void ShouldMapNullAndEmptyToZero()
        {
            // Arrange
            var mapper = new StringMapper();

            // Act
            var r1 = mapper.Map(null);
            var r2 = mapper.Map(string.Empty);

            // Assert
            Assert.Equal(MapConstants.NullOrEmpty, r1);
            Assert.Equal(MapConstants.NullOrEmpty, r2);
        }

        [Fact]
        public void ShouldNotMapWhitespaceToZero()
        {
            // Arrange
            var mapper = new StringMapper();

            // Act
            var r = mapper.Map("\t");

            // Assert
            Assert.NotEqual(MapConstants.NullOrEmpty, r);
        }

        [Fact]
        public void ShouldMapValueToFirstAssignableInteger()
        {
            // Arrange
            var IntegerMapper = new StringMapper();

            // Act
            var r = IntegerMapper.Map("a");

            // Assert
            Assert.Equal(MapConstants.FirstMappableInteger, r);
        }

        [Fact]
        public void ShouldRepeatedlyMapSameValuesToSameInputs()
        {
            // Arrange
            var mapper = new StringMapper();
            var testCases = TestFixtures.GetTestDataForString();

            foreach (var testCase in testCases)
            {
                for (int i = 0; i < 3; i++)
                {
                    // Act
                    var r = mapper.Map(testCase.Input);

                    // Assert
                    Assert.Equal(testCase.ExpectedOutput, r);
                }
            }
            Assert.Equal(testCases.Length + 1, (int)mapper.MappedObjectsCount);
        }

        [Fact]
        public void ShouldDoInverseMapForZero()
        {
            // Arrange
            var mapper = new StringMapper();

            // Act            
            var rInverse = mapper.ReverseMap(MapConstants.NullOrEmpty);

            // Assert
            Assert.Equal(rInverse, string.Empty);
        }

        [Fact]
        public void ShouldDoInverseMapForValues()
        {
            // Arrange
            var mapper = new StringMapper();
            var testData = TestFixtures.GetTestDataForString();

            foreach (var test in testData)
            {
                // Act
                var r = mapper.Map(test.Input);
                var rInverse = mapper.ReverseMap(r);

                // Assert
                Assert.Equal(test.Input, rInverse);
            }
        }
    }
}
