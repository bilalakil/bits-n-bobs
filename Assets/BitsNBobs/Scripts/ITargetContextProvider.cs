namespace BitsNBobs
{
    public interface ITargetContextProvider
    {
        public TargetResolver.Context Context { get; }
    }
}