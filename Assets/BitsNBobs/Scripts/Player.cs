using UnityEngine;

namespace BitsNBobs
{
    public class Player : MonoBehaviour, IUnitProvider
    {
        public static Player I { get; private set; }

        public TargetResolver.Context Context => new(transform, isPlayer: true);
        public Stats Stats { get; } = new();

        public void OnEnable()
        {
            I = this;
        }

        public void OnDisable()
        {
            I = null;
        }
    }
}