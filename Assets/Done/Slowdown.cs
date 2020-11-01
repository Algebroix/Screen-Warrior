using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slowdown : MonoBehaviour
{
    [Range(0.01f, 2.0f)]
    [SerializeField]
    private float slowdownRate = 0.01f;

    private Rigidbody2D rigidBody;

    //Using Update because velocity can change through physics interactions, not only manually.
    private void Update()
    {
        //If velocity is non zero, slow down.
        if (rigidBody.velocity.magnitude > 0.0f)
        {
            rigidBody.velocity = rigidBody.velocity.normalized * Mathf.Max(rigidBody.velocity.magnitude - slowdownRate, 0.0f);
        }
    }

    private void Awake()
    {
        //Cache
        rigidBody = GetComponent<Rigidbody2D>();
    }
}