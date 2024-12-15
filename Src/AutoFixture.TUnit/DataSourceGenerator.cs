using System.Reflection;
using AutoFixture.Kernel;

namespace AutoFixture.TUnit;

internal static class DataSourceGenerator
{
    internal static void SetMetadata<T>(ParameterInfo parameterInfo, ref ParameterMetadata parameter, T? inlineData)
    {
        if (EqualityComparer<T>.Default.Equals(inlineData, default))
        {
            parameter = new GeneratedParameterMetadata(parameterInfo);
        }
        else
        {
            parameter = new InlinedParameterMetadata(inlineData!);
        }
    }
    
    internal static object[] GenerateDataSources(ParameterMetadata[] metadata, IFixture fixture)
    {
        var specimens = new object[metadata.Length];
        for (int i = 0; i < metadata.Length; i++)
        {
            ParameterMetadata current = metadata[i];
            if (current is InlinedParameterMetadata inlinedMetadata)
            {
                specimens[i] = inlinedMetadata.Value; 
                continue;
            }

            if (current is not GeneratedParameterMetadata generated)
            {
                // todo: define proper message
                throw new ArgumentOutOfRangeException();
            }

            CustomizeFixture(generated.ParameterInfo, fixture);

            specimens[i] = Resolve(generated.ParameterInfo, fixture);
        }
        
        return specimens;
    }
    
    private static void CustomizeFixture(ParameterInfo p, IFixture fixture)
    {
        var customizeAttributes = p.GetCustomAttributes()
            .OfType<IParameterCustomizationSource>()
            .OrderBy(x => x, new CustomizeAttributeComparer());

        foreach (var ca in customizeAttributes)
        {
            var c = ca.GetCustomization(p);
            fixture.Customize(c);
        }
    }
    
    private static object Resolve(ParameterInfo p, IFixture fixture)
    {
        var context = new SpecimenContext(fixture);

        return context.Resolve(p);
    }
}

internal abstract record ParameterMetadata();
internal sealed record GeneratedParameterMetadata(ParameterInfo ParameterInfo) : ParameterMetadata();
internal sealed record InlinedParameterMetadata(object Value) : ParameterMetadata();