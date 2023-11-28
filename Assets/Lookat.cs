using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lookat : MonoBehaviour
{
    public GameObject Cube;
    public GameObject Cannon;


    void Start()
   {
     
   }

    void FixedUpdate()
    {
        Debug.DrawLine(Cube.transform.position, Cannon.transform.position, Color.red);
        Cannon.transform.rotation = Quaternion.LookRotation(new Vector3 (Cube.transform.position.x,Cannon.transform.position.y,Cube.transform.position.z) - Cannon.transform.position);

        
    }
}
