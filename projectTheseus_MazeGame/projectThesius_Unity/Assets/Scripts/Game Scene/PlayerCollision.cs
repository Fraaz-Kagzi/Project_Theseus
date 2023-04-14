using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public Player Movement; 
    void OnCollisionEnter(UnityEngine.Collision collisioninfo)
    {
        if(collisioninfo.collider.tag == "Wall")
        {
            //Debug.Log("");
        }
        
    }
}
