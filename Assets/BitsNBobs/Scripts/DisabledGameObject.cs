using UnityEngine;

namespace BitsNBobs
{
    public static class DisabledGameObject
    {
        static GameObject _i;
        public static GameObject I
        {
            get
            {
                if (!_i)
                {
                    _i = new GameObject("DisabledGameObject"); 
                    _i.SetActive(false);
                }

                return _i;
            }
        }
    }
}