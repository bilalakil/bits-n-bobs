using UnityEngine;

namespace BitsNBobs
{
    public class InheritOwner : MonoBehaviour, IUnitProvider
    {
        public TargetResolver.Context Context => Owner.Context;
        public Stats Stats => Owner.Stats;

        public IUnitProvider Owner;
    }
}