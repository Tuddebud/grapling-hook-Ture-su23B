using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class gun_rotate : MonoBehaviour
{
    // Start is called before the first frame update
    public GrapplingGun grappling;
    private Quaternion desiredrotation;
    private float rotationSpeed= 5f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (grappling.isGrappling())
        {
            desiredrotation = transform.parent.rotation;
        }
        else
        {
            desiredrotation = Quaternion.LookRotation(grappling.GetGrapplePoint() - transform.position);
        }
        transform.rotation = Quaternion.Lerp(a: transform.rotation, b: desiredrotation, t: Time.deltaTime * rotationSpeed);
    }

    
    
}
