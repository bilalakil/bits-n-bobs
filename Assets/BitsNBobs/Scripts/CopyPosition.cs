using UnityEngine;

namespace BitsNBobs
{
    public class CopyPosition : MonoBehaviour
    {
        public Transform target;

        public void Update()
        {
            transform.position = target.position;
        }
        
#if UNITY_EDITOR
        [ContextMenu("Execute now")]
        public void ExecuteNow()
        {
            Update();
        }
#endif
    }
}