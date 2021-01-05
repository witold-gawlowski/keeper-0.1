using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TestScript : MonoBehaviour
{
    public GameObject linkerA;
    public GameObject linkerB;
    
    public void OnTest()
    {
        BlockScript ABlockkScript = linkerA.GetComponentInParent<BlockScript>();
        bool collidiing = ABlockkScript.IsColliding(linkerA, linkerB);
        if (collidiing)
        {
            print("COLLIDING!");
        }
        else
        {
            print("NOT COLLIDING!");
        }
    }
}
