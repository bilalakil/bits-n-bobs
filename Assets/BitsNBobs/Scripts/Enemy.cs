using System;
using System.Collections.Generic;
using UnityEngine;

namespace BitsNBobs
{
    public class Enemy : MonoBehaviour, IUnitProvider
    {
        public static IReadOnlyCollection<Enemy> All => s_all;
        static readonly HashSet<Enemy> s_all = new HashSet<Enemy>();

        public TargetResolver.Context Context => new(transform, isPlayer: false);
        public Stats Stats { get; } = null;
        
        HealthController _healthController;

        public void Awake()
        {
            _healthController = GetComponent<HealthController>();
        }

        public void OnEnable()
        {
            s_all.Add(this);

            if (_healthController)
                _healthController.OnDiedChanged += HandleDiedChanged;
        }

        public void OnDisable()
        {
            s_all.Remove(this);

            if (_healthController)
                _healthController.OnDiedChanged -= HandleDiedChanged;
        }

        void HandleDiedChanged()
        {
            if (_healthController.IsDead)
                Destroy(gameObject);
        }
    }
}