using System.Reflection;
using TUnit.Core.Enums;

namespace AutoFixture.TUnit.UnitTest
{
    public class AutoDataAttributeTest
    {
        [Test]
        [Arguments(typeof(AutoDataAttribute<>), typeof(DataSourceGeneratorAttribute<>))]
        [Arguments(typeof(AutoDataAttribute<,>), typeof(DataSourceGeneratorAttribute<,>))]
        [Arguments(typeof(AutoDataAttribute<,,>), typeof(DataSourceGeneratorAttribute<,,>))]
        [Arguments(typeof(AutoDataAttribute<,,,>), typeof(DataSourceGeneratorAttribute<,,,>))]
        [Arguments(typeof(AutoDataAttribute<,,,,>), typeof(DataSourceGeneratorAttribute<,,,,>))]
        public async Task SutIsDataSourceGeneratorAttribute(Type autoDataAttributeType, Type expectedBaseType)
        {
            await Assert.That(autoDataAttributeType.BaseType).IsNotNull();
            await Assert.That(autoDataAttributeType.BaseType!.IsGenericType).IsTrue();
            await Assert.That(autoDataAttributeType.BaseType.GetGenericTypeDefinition() == expectedBaseType).IsTrue();
        }

        [Test]
        public void InitializedWithNullFixtureThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new TestAutoDataAttribute<int>(null));
            Assert.Throws<ArgumentNullException>(() => new TestAutoDataAttribute<int, int>(null));
            Assert.Throws<ArgumentNullException>(() => new TestAutoDataAttribute<int, int, int>(null));
            Assert.Throws<ArgumentNullException>(() => new TestAutoDataAttribute<int, int, int, int>(null));
            Assert.Throws<ArgumentNullException>(() => new TestAutoDataAttribute<int, int, int, int, int>(null));
        }

        [Test]
        public void AutoDataAttributeOfTGenerateDataSourcesDoesNotReceiveParameterInfos()
        {
            AutoDataAttribute<int> sut = new();
            DataGeneratorMetadata metadata = new()
            {
                TestClassType = this.GetType(),
                TestObjectBag = new Dictionary<string, object?>(),
                ParameterInfos = null,
                PropertyInfo = null,
                Type = DataGeneratorType.Parameters,
                TestSessionId = "TestSessionId"
            };
            
            Assert.Throws<ArgumentNullException>(() => sut.GenerateDataSources(metadata));
        }
        
        [Test]
        [MethodDataSource(nameof(AutoDataAttributeOfTIncorrectLengthInParametersDataSource))]
        public void AutoDataAttributeOfTGenerateDataSourcesAmountOfParameterInfosDiffersToAmountOfT(ParameterInfo[] parameterInfos)
        {
            AutoDataAttribute<int> sut = new();
            DataGeneratorMetadata metadata = new()
            {
                TestClassType = this.GetType(),
                TestObjectBag = new Dictionary<string, object?>(),
                ParameterInfos = parameterInfos,
                PropertyInfo = null,
                Type = DataGeneratorType.Parameters,
                TestSessionId = "TestSessionId"
            };

            Assert.Throws<ArgumentException>(() => sut.GenerateDataSources(metadata));
        }
        
        [Test]
        [MethodDataSource(nameof(AutoDataAttributeOfTIncorrectTypesInParametersDataSource))]
        public void AutoDataAttributeOfTGenerateDataSourcesParameterInfosTypesMustBeTypeOfT(ParameterInfo[] parameterInfos)
        {
            AutoDataAttribute<int> sut = new();
            DataGeneratorMetadata metadata = new()
            {
                TestClassType = this.GetType(),
                TestObjectBag = new Dictionary<string, object?>(),
                ParameterInfos = parameterInfos,
                PropertyInfo = null,
                Type = DataGeneratorType.Parameters,
                TestSessionId = "TestSessionId"
            };

            Assert.Throws<ArgumentException>(() => sut.GenerateDataSources(metadata));
        }

        public static IEnumerable<ParameterInfo[]> AutoDataAttributeOfTIncorrectTypesInParametersDataSource()
        {
            var test = typeof(AutoDataAttributeTest)
                .GetMethod("MethodString", BindingFlags.NonPublic | BindingFlags.Instance).GetParameters();
            yield return new ParameterInfo[1]
            {
                new TestParameterInfo(typeof(string))
            };
            
            yield return new ParameterInfo[1]
            {
                new TestParameterInfo(typeof(bool))
            };
            
            yield return new ParameterInfo[1]
            {
                new TestParameterInfo(typeof(decimal))
            };

            void MethodString(string argument)
            {
            }
        }
        public static IEnumerable<ParameterInfo[]> AutoDataAttributeOfTIncorrectLengthInParametersDataSource()
        {
            yield return new ParameterInfo[0];
            yield return new ParameterInfo[2]
            {
                new TestParameterInfo(),
                new TestParameterInfo()
            };
        }

        private class TestAutoDataAttribute<T> : AutoDataAttribute<T>
        {
            public TestAutoDataAttribute(Func<IFixture> fixtureFactory) : base(fixtureFactory)
            {
            }
        }
        
        private class TestAutoDataAttribute<T1, T2> : AutoDataAttribute<T1, T2>
        {
            public TestAutoDataAttribute(Func<IFixture> fixtureFactory) : base(fixtureFactory)
            {
            }
        }
        
        private class TestAutoDataAttribute<T1, T2, T3> : AutoDataAttribute<T1, T2, T3>
        {
            public TestAutoDataAttribute(Func<IFixture> fixtureFactory) : base(fixtureFactory)
            {
            }
        }
        
        private class TestAutoDataAttribute<T1, T2, T3, T4> : AutoDataAttribute<T1, T2, T3, T4>
        {
            public TestAutoDataAttribute(Func<IFixture> fixtureFactory) : base(fixtureFactory)
            {
            }
        }
        
        private class TestAutoDataAttribute<T1, T2, T3, T4, T5> : AutoDataAttribute<T1, T2, T3, T4, T5>
        {
            public TestAutoDataAttribute(Func<IFixture> fixtureFactory) : base(fixtureFactory)
            {
            }
        }

        private class TestParameterInfo : ParameterInfo
        {
            private readonly Type? _parameterType;

            public TestParameterInfo()
            {
            }

            public TestParameterInfo(Type parameterType)
            {
                _parameterType = parameterType;
            }
            public override Type ParameterType => _parameterType ?? base.ParameterType;
        }
    }
}