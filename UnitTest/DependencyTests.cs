using System.Reflection;

namespace UnitTests;

public class DependencyTests
{
    private static readonly Assembly DomainAssembly = typeof(Domain.AssemblyReference).Assembly;
    private static readonly Assembly ApplicationAssembly = typeof(Application.AssemblyReference).Assembly;
    private static readonly Assembly InfrastructureAssembly = typeof(Infrastructure.AssemblyReference).Assembly;
    private static readonly Assembly PresentationAssembly = typeof(Presentation.AssemblyReference).Assembly;

    [Fact]
    public void Domain_Should_Not_Have_Any_Dependencies()
    {
        var references = DomainAssembly.GetReferencedAssemblies();
        Assert.DoesNotContain(references, r => r.Name == InfrastructureAssembly.GetName().Name);
        Assert.DoesNotContain(references, r => r.Name == ApplicationAssembly.GetName().Name);
        Assert.DoesNotContain(references, r => r.Name == PresentationAssembly.GetName().Name);
    }

    [Fact]
    public void Application_Should_Only_Depend_On_Domain()
    {
        var references = ApplicationAssembly.GetReferencedAssemblies();
        Assert.Contains(references, r => r.Name == DomainAssembly.GetName().Name);
        Assert.DoesNotContain(references, r => r.Name == InfrastructureAssembly.GetName().Name);
        Assert.DoesNotContain(references, r => r.Name == PresentationAssembly.GetName().Name);
    }

    [Fact]
    public void Infrastructure_Should_Only_Depend_On_Domain_And_Application()
    {
        var references = InfrastructureAssembly.GetReferencedAssemblies();
        Assert.Contains(references, r => r.Name == DomainAssembly.GetName().Name);
        Assert.Contains(references, r => r.Name == ApplicationAssembly.GetName().Name);
        Assert.DoesNotContain(references, r => r.Name == PresentationAssembly.GetName().Name);
    }

    [Fact]
    public void Presentation_Should_Only_Depend_On_Application()
    {
        var references = PresentationAssembly.GetReferencedAssemblies();
        Assert.Contains(references, r => r.Name == ApplicationAssembly.GetName().Name);
        Assert.DoesNotContain(references, r => r.Name == DomainAssembly.GetName().Name);
        Assert.DoesNotContain(references, r => r.Name == InfrastructureAssembly.GetName().Name);
    }
}