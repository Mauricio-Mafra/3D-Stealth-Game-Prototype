using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptPC : MonoBehaviour
{
    private float mX;
    private Vector3 mov;
    private float vel;
    private Rigidbody rbd;
    public float alturaPulo;
    // Start is called before the first frame update
    void Start()
    {
        rbd = GetComponent<Rigidbody>();
        vel = 6f;
        alturaPulo = 4f;
    }

    // Update is called once per frame
    void Update()
    {
        mX = Input.GetAxis("Mouse X");
        transform.Rotate(new Vector3(0, mX, 0));
        mov = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        transform.Translate(mov * vel * Time.deltaTime);
        if (Input.GetKey(KeyCode.LeftShift))
            vel = 10f;
        else
            vel = 6f;
        if (Input.GetKeyDown(KeyCode.Space))
            rbd.AddForce(new Vector3(0, 100f, 0) * alturaPulo);
    }
}
