using System.Globalization;
using UnityEditor;
using UnityEngine;

namespace BitsNBobs.Editor
{
    [CustomEditor(typeof(HealthController))]
    public class HealthControllerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            
            if (!Application.isPlaying)
                return;

            var healthController = (HealthController)target;
            EditorGUILayout.LabelField("Max Health", healthController.MaxHealth.ToString());
            EditorGUILayout.LabelField("Current Health", healthController.CurrentHealth.ToString());
            EditorGUILayout.LabelField("Health Regen Per Second",
                healthController.HealthRegenerationPerSecond.ToString(CultureInfo.CurrentCulture));
            EditorGUILayout.LabelField("Is Dead", healthController.IsDead.ToString());
        }
    }
}