using System.Collections;
using System.Collections.Generic;
using UnityEngine;  

public static class HelperFunctions {
    
    /*
     * Makes the front of plane "sourcePlane" rotate to point at target.
     * 
     * Useful when using planes with images to represent objects in your game.
     * This function will make the front of the image face target.
     */
    public static void PlaneLookAt(Transform sourcePlane, Transform target) {
        sourcePlane.LookAt(target);
        sourcePlane.Rotate(new Vector3(90, 0, 0));
    }

    public static GameObject GetPlayerObject() {
        return GameObject.Find("Main Camera");
    }
}
