using UnityEngine;

namespace DefaultNamespace
{
    public class ReturnButton : MonoBehaviour
    {
        public void OnClickReturn()
        {
            //DO Return
        }

        public bool IsActive()
        {
            return gameObject.activeInHierarchy;
        }
    }
}