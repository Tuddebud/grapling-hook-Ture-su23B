using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class GrapplingGun : MonoBehaviour
{
    // visar en linje när man skuta
    private LineRenderer lr;
    // var man träffar
    private Vector3 GrapplePoint;
    //var man kan grappla 
    public LayerMask WhatIsGrappleable;
    //viktiga positioner
    public Transform Guntip,camera, player;
    //hur långt man kan skjuta
    private float maxDistance = 100;
    //sätta ihop två rigidbodys men distanserna kan ändras typ
    private SpringJoint joint;
    void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            StartGrapple();
        }else if (Input.GetMouseButtonUp(0))
        {
            StopGrapple();
        }
        void StartGrapple(){
            RaycastHit hitt;
            //skickar ut en raycast som känner av var den träffar och om den träffar.
            if (Physics.Raycast(origin:camera.position,direction:camera.forward, out hitt, maxDistance))
            {
                //var den träffar
                GrapplePoint = hitt.point;
                //sätter typ ihop var den träffa och en själv så man kan svinga sig
                joint = player.gameObject.AddComponent<SpringJoint>();
                joint.autoConfigureConnectedAnchor = false;
                joint.connectedAnchor = GrapplePoint;

                float DistanceFromPoint = Vector3.Distance(a: player.position, b: GrapplePoint);


                joint.maxDistance = DistanceFromPoint * 0.3f;
                joint.minDistance = DistanceFromPoint * 0.25f;
                //världen som du kan ändra så det känns som ni vill
                joint.spring = 4.5f;
                joint.damper = 7;
                joint.massScale = 4.5f;

                lr.positionCount = 2;
            }
        }
        void StopGrapple()
        {
            //gör så det slutar
            lr.positionCount = 0;
            Destroy(joint);
        }
       
    }
    //gör rep. det görs lite senare en update annars ser det skit ut av någon anledning
    private void LateUpdate()
    {
        DrawPope();
    }
    void DrawPope()
    {
        // gör en line renderer
        if (!joint) return;
        lr.SetPosition(index: 0, Guntip.position);
        lr.SetPosition(index: 1, GrapplePoint);
    }
    public bool isGrappling()
    {
        return joint == null;
    }
    public Vector3 GetGrapplePoint() {
        return GrapplePoint;
    }
}
