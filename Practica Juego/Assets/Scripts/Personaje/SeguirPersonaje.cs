using UnityEngine;

public class SeguirPersonaje : MonoBehaviour
{
   public Transform target;
   public Vector3 z = new Vector3(0, 0, -10); 

    void LateUpdate()
    {
        if (target != null)
        {
            transform.position = target.position + z;
        }
    }
}
