using System.Diagnostics.CodeAnalysis;

namespace BitsNBobs
{
    public interface IUnitProvider
    {
        public TargetResolver.Context Context { get; }
        [AllowNull] public Stats Stats { get; }
    }
}