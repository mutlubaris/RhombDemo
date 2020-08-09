using UnityEngine;

public class Level : MonoBehaviour
{
    void Start()
    {
        //Determine the number of targets on this level
        
        FindObjectOfType<LevelTracker>().totalTargets = transform.childCount - 1;
        print("Number of routes: " + FindObjectOfType<LevelTracker>().totalTargets);
    }
}
