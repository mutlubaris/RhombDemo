﻿using UnityEngine;

public class FollowObject : MonoBehaviour
{
    bool followingObject = false;
    
    private void Update() 
    {
        //Start following the Object if got to this Turn point
        
        if (transform.position == transform.parent.GetChild(0).transform.position)
            followingObject = true;
        
        if (followingObject)
            transform.position = transform.parent.GetChild(0).transform.position;
    }
}
