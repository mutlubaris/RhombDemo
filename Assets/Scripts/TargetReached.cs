using UnityEngine;

public class TargetReached : MonoBehaviour
{
    bool jobDone;

    private void Update() 
    {
        if (transform.parent.GetChild(0).transform.position == transform.position && !jobDone)
        {
            //Let the LevelTracker know when a target is reached
            
            FindObjectOfType<LevelTracker>().targetsReached += 1;
            print("Targets reached: " + FindObjectOfType<LevelTracker>().targetsReached);
            jobDone = true;
        }
    }
}
