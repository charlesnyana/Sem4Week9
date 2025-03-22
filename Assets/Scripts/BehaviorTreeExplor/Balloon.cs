using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    public GameObject billy;
    Rigidbody rb;
    public float arrivalDistance;
    // Start is called before the first frame update
    void Start()
    {
        rb= GetComponent<Rigidbody>();
        rb.AddForce(Vector3.up * Random.Range(40, 100));
    }

    // Update is called once per frame
    void Update()
    {
        float distanceFromBalloon = Vector3.Distance(billy.transform.position, gameObject.transform.position);
        if (distanceFromBalloon <= arrivalDistance)
        {
            rb.AddForce(Vector3.up*Random.Range(200,400));
        }
    }
}
