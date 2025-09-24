using System;
using UnityEngine;

namespace BitsNBobs
{
    public class Player : MonoBehaviour, ITargetContextProvider
    {
        public static Player I { get; private set; }

        public TargetResolver.Context Context => new(transform, isPlayer: true);

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