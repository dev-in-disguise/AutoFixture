using System.Diagnostics.CodeAnalysis;

namespace AutoFixture.TUnit;

/// <summary>
/// Provides auto-generated data specimens generated by AutoFixture as an extension to
/// xUnit.net's Theory attribute.
/// </summary>

// [DataDiscoverer(
//     typeName: "AutoFixture.Xunit2.NoPreDiscoveryDataDiscoverer",
//     assemblyName: "AutoFixture.Xunit2")] 
[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
[CLSCompliant(false)]
[SuppressMessage("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes",
    Justification = "This attribute is the root of a potential attribute hierarchy.")]
[SuppressMessage("Usage", "TUnit0028:AttributeUsage Overridden")]
public class AutoDataAttribute<T> : DataSourceGeneratorAttribute<T>
{
    private readonly Lazy<IFixture> fixtureLazy;

    /// <summary>
    /// Initializes a new instance of the <see cref="AutoDataAttribute{T}"/> class.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This constructor overload initializes the <see cref="Fixture"/> to an instance of
    /// <see cref="Fixture"/>.
    /// </para>
    /// </remarks>
    public AutoDataAttribute()
        : this(() => new Fixture())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AutoDataAttribute{T}"/> class
    /// with the supplied <paramref name="fixtureFactory"/>. Fixture will be created
    /// on demand using the provided factory.
    /// </summary>
    /// <param name="fixtureFactory">The fixture factory used to construct the fixture.</param>
    protected AutoDataAttribute(Func<IFixture> fixtureFactory)
    {
        if (fixtureFactory == null) throw new ArgumentNullException(nameof(fixtureFactory));

        this.fixtureLazy = new Lazy<IFixture>(fixtureFactory, LazyThreadSafetyMode.PublicationOnly);
    }
    
    public override IEnumerable<T> GenerateDataSources(DataGeneratorMetadata metadata)
    {
        ArgumentNullException.ThrowIfNull(metadata.ParameterInfos);
        if (metadata.ParameterInfos.Length == 0)
        {
            throw new ArgumentException($"{nameof(metadata.ParameterInfos)} cannot be empty.");
        }

        if (metadata.ParameterInfos.Length != 1)
        {
            throw new ArgumentException($"{nameof(metadata.ParameterInfos)} length can not differ to amount of generic type parameters.");
        }

        // todo: validate parameterinfos against generic types

        return DataSourceGenerator
            .GenerateDataSources(
                metadata.ParameterInfos.Select(p => new GeneratedParameterMetadata(p)).ToArray<ParameterMetadata>(),
                this.fixtureLazy.Value).Cast<T>();
    }
}

/// <summary>
/// Provides auto-generated data specimens generated by AutoFixture as an extension to
/// xUnit.net's Theory attribute.
/// </summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
[CLSCompliant(false)]
[SuppressMessage("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes",
    Justification = "This attribute is the root of a potential attribute hierarchy.")]
[SuppressMessage("Usage", "TUnit0028:AttributeUsage Overridden")]
public class AutoDataAttribute<T1, T2> : DataSourceGeneratorAttribute<T1, T2>
{
    private readonly Lazy<IFixture> fixtureLazy;

    /// <summary>
    /// Initializes a new instance of the <see cref="AutoDataAttribute{T}"/> class.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This constructor overload initializes the <see cref="Fixture"/> to an instance of
    /// <see cref="Fixture"/>.
    /// </para>
    /// </remarks>
    public AutoDataAttribute()
        : this(() => new Fixture())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AutoDataAttribute{T}"/> class
    /// with the supplied <paramref name="fixtureFactory"/>. Fixture will be created
    /// on demand using the provided factory.
    /// </summary>
    /// <param name="fixtureFactory">The fixture factory used to construct the fixture.</param>
    protected AutoDataAttribute(Func<IFixture> fixtureFactory)
    {
        if (fixtureFactory == null) throw new ArgumentNullException(nameof(fixtureFactory));

        this.fixtureLazy = new Lazy<IFixture>(fixtureFactory, LazyThreadSafetyMode.PublicationOnly);
    }
    
    public override IEnumerable<(T1, T2)> GenerateDataSources(DataGeneratorMetadata metadata)
    {
        ArgumentNullException.ThrowIfNull(metadata.ParameterInfos);
        if (metadata.ParameterInfos.Length == 0)
        {
            return Enumerable.Empty<(T1, T2)>();
        }
        // todo: validate parameterinfos against generic types

        object[] dataSources = DataSourceGenerator.GenerateDataSources(
            metadata.ParameterInfos.Select(p => new GeneratedParameterMetadata(p)).ToArray<ParameterMetadata>(),
            this.fixtureLazy.Value);

        return [((T1)dataSources[0], (T2)dataSources[1])];
    }
}

/// <summary>
/// Provides auto-generated data specimens generated by AutoFixture as an extension to
/// xUnit.net's Theory attribute.
/// </summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
[CLSCompliant(false)]
[SuppressMessage("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes",
    Justification = "This attribute is the root of a potential attribute hierarchy.")]
[SuppressMessage("Usage", "TUnit0028:AttributeUsage Overridden")]
public class AutoDataAttribute<T1, T2, T3> : DataSourceGeneratorAttribute<T1, T2, T3>
{
    private readonly Lazy<IFixture> fixtureLazy;

    /// <summary>
    /// Initializes a new instance of the <see cref="AutoDataAttribute{T}"/> class.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This constructor overload initializes the <see cref="Fixture"/> to an instance of
    /// <see cref="Fixture"/>.
    /// </para>
    /// </remarks>
    public AutoDataAttribute()
        : this(() => new Fixture())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AutoDataAttribute{T}"/> class
    /// with the supplied <paramref name="fixtureFactory"/>. Fixture will be created
    /// on demand using the provided factory.
    /// </summary>
    /// <param name="fixtureFactory">The fixture factory used to construct the fixture.</param>
    protected AutoDataAttribute(Func<IFixture> fixtureFactory)
    {
        if (fixtureFactory == null) throw new ArgumentNullException(nameof(fixtureFactory));

        this.fixtureLazy = new Lazy<IFixture>(fixtureFactory, LazyThreadSafetyMode.PublicationOnly);
    }
    
    public override IEnumerable<(T1, T2, T3)> GenerateDataSources(DataGeneratorMetadata metadata)
    {
        ArgumentNullException.ThrowIfNull(metadata.ParameterInfos);
        if (metadata.ParameterInfos.Length == 0)
        {
            return Enumerable.Empty<(T1, T2, T3)>();
        }
        // todo: validate parameterinfos against generic types

        object[] dataSources = DataSourceGenerator.GenerateDataSources(
            metadata.ParameterInfos.Select(p => new GeneratedParameterMetadata(p)).ToArray<ParameterMetadata>(),
            this.fixtureLazy.Value);

        return [((T1)dataSources[0], (T2)dataSources[1], (T3)dataSources[2])];
    }
}

/// <summary>
/// Provides auto-generated data specimens generated by AutoFixture as an extension to
/// xUnit.net's Theory attribute.
/// </summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
[CLSCompliant(false)]
[SuppressMessage("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes",
    Justification = "This attribute is the root of a potential attribute hierarchy.")]
[SuppressMessage("Usage", "TUnit0028:AttributeUsage Overridden")]
public class AutoDataAttribute<T1, T2, T3, T4> : DataSourceGeneratorAttribute<T1, T2, T3, T4>
{
    private readonly Lazy<IFixture> fixtureLazy;

    /// <summary>
    /// Initializes a new instance of the <see cref="AutoDataAttribute{T}"/> class.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This constructor overload initializes the <see cref="Fixture"/> to an instance of
    /// <see cref="Fixture"/>.
    /// </para>
    /// </remarks>
    public AutoDataAttribute()
        : this(() => new Fixture())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AutoDataAttribute{T}"/> class
    /// with the supplied <paramref name="fixtureFactory"/>. Fixture will be created
    /// on demand using the provided factory.
    /// </summary>
    /// <param name="fixtureFactory">The fixture factory used to construct the fixture.</param>
    protected AutoDataAttribute(Func<IFixture> fixtureFactory)
    {
        if (fixtureFactory == null) throw new ArgumentNullException(nameof(fixtureFactory));

        this.fixtureLazy = new Lazy<IFixture>(fixtureFactory, LazyThreadSafetyMode.PublicationOnly);
    }
    
    public override IEnumerable<(T1, T2, T3, T4)> GenerateDataSources(DataGeneratorMetadata metadata)
    {
        ArgumentNullException.ThrowIfNull(metadata.ParameterInfos);
        if (metadata.ParameterInfos.Length == 0)
        {
            return Enumerable.Empty<(T1, T2, T3, T4)>();
        }
        // todo: validate parameterinfos against generic types

        object[] dataSources = DataSourceGenerator.GenerateDataSources(
            metadata.ParameterInfos.Select(p => new GeneratedParameterMetadata(p)).ToArray<ParameterMetadata>(),
            this.fixtureLazy.Value);

        return [((T1)dataSources[0], (T2)dataSources[1], (T3)dataSources[2], (T4)dataSources[3])];
    }
}

/// <summary>
/// Provides auto-generated data specimens generated by AutoFixture as an extension to
/// xUnit.net's Theory attribute.
/// </summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
[CLSCompliant(false)]
[SuppressMessage("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes",
    Justification = "This attribute is the root of a potential attribute hierarchy.")]
[SuppressMessage("Usage", "TUnit0028:AttributeUsage Overridden")]
public class AutoDataAttribute<T1, T2, T3, T4, T5> : DataSourceGeneratorAttribute<T1, T2, T3, T4, T5>
{
    private readonly Lazy<IFixture> fixtureLazy;

    /// <summary>
    /// Initializes a new instance of the <see cref="AutoDataAttribute{T}"/> class.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This constructor overload initializes the <see cref="Fixture"/> to an instance of
    /// <see cref="Fixture"/>.
    /// </para>
    /// </remarks>
    public AutoDataAttribute()
        : this(() => new Fixture())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AutoDataAttribute{T}"/> class
    /// with the supplied <paramref name="fixtureFactory"/>. Fixture will be created
    /// on demand using the provided factory.
    /// </summary>
    /// <param name="fixtureFactory">The fixture factory used to construct the fixture.</param>
    protected AutoDataAttribute(Func<IFixture> fixtureFactory)
    {
        if (fixtureFactory == null) throw new ArgumentNullException(nameof(fixtureFactory));

        this.fixtureLazy = new Lazy<IFixture>(fixtureFactory, LazyThreadSafetyMode.PublicationOnly);
    }
    
    public override IEnumerable<(T1, T2, T3, T4, T5)> GenerateDataSources(DataGeneratorMetadata metadata)
    {
        ArgumentNullException.ThrowIfNull(metadata.ParameterInfos);
        if (metadata.ParameterInfos.Length == 0)
        {
            return Enumerable.Empty<(T1, T2, T3, T4, T5)>();
        }
        // todo: validate parameterinfos against generic types

        object[] dataSources = DataSourceGenerator.GenerateDataSources(
            metadata.ParameterInfos.Select(p => new GeneratedParameterMetadata(p)).ToArray<ParameterMetadata>(),
            this.fixtureLazy.Value);

        return [((T1)dataSources[0], (T2)dataSources[1], (T3)dataSources[2], (T4)dataSources[3], (T5)dataSources[4])];
    }
}