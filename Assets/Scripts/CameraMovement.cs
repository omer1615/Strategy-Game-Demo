using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public int camSpeed = 10;
		
	void Update ()
    {
        if (Input.GetButton("Horizontal"))
        {
            transform.position += new Vector3(Input.GetAxis("Horizontal") / camSpeed, 0, 0);
        }

        if (Input.GetButton("Vertical"))
        {
            transform.position += new Vector3(0, Input.GetAxis("Vertical") / camSpeed, 0);
        }
	}
}
