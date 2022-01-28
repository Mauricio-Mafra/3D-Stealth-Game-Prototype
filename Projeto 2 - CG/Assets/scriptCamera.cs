using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptCamera : MonoBehaviour
{
    private float mY;
    // Start is called before the first frame update
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        mY = Input.GetAxis("Mouse Y");
        transform.Rotate(new Vector3(-mY, 0, 0));
    }
}
