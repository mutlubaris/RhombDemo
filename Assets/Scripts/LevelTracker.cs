using System.Collections;
using UnityEngine;

public class LevelTracker : MonoBehaviour
{
    [SerializeField] float levelChangeDelay = 2f;
    [SerializeField] GameObject[] levelPrefabs;
    
    private int levelIndex;
    private bool youWon;

    public int targetsReached;
    public int totalTargets;
    public bool levelFailed;
    
    private void Update() 
    {
        if (targetsReached == totalTargets) //If all targets are reached
        {
            if (levelIndex == levelPrefabs.Length - 1) //If the level was the last one
            {
                if(!youWon)
                {
                    print("You won!");
                    youWon = true;
                }
            }

            else //Load next level
            {
                targetsReached = 0;
                levelIndex += 1;
                StartCoroutine(StartLevel());
            }
        } 

        if (levelFailed) //Reload this level
        {
            print("Level failed");
            targetsReached = 0;
            levelFailed = false;
            StartCoroutine(StartLevel());
        }
    }

    IEnumerator StartLevel()
    {
        //Destroy the current level and instantiate new level
        
        print("Ending level");
        yield return new WaitForSeconds(levelChangeDelay);
        Destroy(FindObjectOfType<Level>().gameObject);
        Instantiate(levelPrefabs[levelIndex], Vector3.zero, Quaternion.identity);
    }
}
