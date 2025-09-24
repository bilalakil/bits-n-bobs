using UnityEngine;

namespace BitsNBobs
{
    public class InheritOwner : MonoBehaviour, ITargetContextProvider
    {
        public TargetResolver.Context Context => Owner.Context;

        public ITargetContextProvider Owner;
    }
}