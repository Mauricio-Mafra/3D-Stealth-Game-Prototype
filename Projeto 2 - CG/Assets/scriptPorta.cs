using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptPorta : MonoBehaviour
{
    public LayerMask playerMask;
    public LayerMask enemyMask;
    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider col)
    {
        //if (col.gameObject.layer == playerMask || col.gameObject.layer == enemyMask)
            anim.SetBool("openDoor", true);
    }

    private void OnTriggerExit(Collider col)
    {
        anim.SetBool("openDoor", false);
    }
}
