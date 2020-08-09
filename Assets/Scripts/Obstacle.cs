using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) 
    {
        //Destroy the obstacle after the Object comes to a stop
        
        if (other.tag == "Object")
        {
            Destroy(gameObject, 0.01f);
        }
    }
}
