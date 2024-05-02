using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class GrapplingGun : MonoBehaviour
{
    // visar en linje n�r man skuta
    private LineRenderer lr;
    // var man tr�ffar
    private Vector3 GrapplePoint;
    //var man kan grappla 
    public LayerMask WhatIsGrappleable;
    //viktiga positioner
    public Transform Guntip,camera, player;
    //hur l�ngt man kan skjuta
    private float maxDistance = 100;
    //s�tta ihop tv� rigidbodys men distanserna kan �ndras typ
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
            //skickar ut en raycast som k�nner av var den tr�ffar och om den tr�ffar.
            if (Physics.Raycast(origin:camera.position,direction:camera.forward, out hitt, maxDistance))
            {
                //var den tr�ffar
                GrapplePoint = hitt.point;
                //s�tter typ ihop var den tr�ffa och en sj�lv s� man kan svinga sig
                joint = player.gameObject.AddComponent<SpringJoint>();
                joint.autoConfigureConnectedAnchor = false;
                joint.connectedAnchor = GrapplePoint;

                float DistanceFromPoint = Vector3.Distance(a: player.position, b: GrapplePoint);


                joint.maxDistance = DistanceFromPoint * 0.3f;
                joint.minDistance = DistanceFromPoint * 0.25f;
                //v�rlden som du kan �ndra s� det k�nns som ni vill
                joint.spring = 4.5f;
                joint.damper = 7;
                joint.massScale = 4.5f;

                lr.positionCount = 2;
            }
        }
        void StopGrapple()
        {
            //g�r s� det slutar
            lr.positionCount = 0;
            Destroy(joint);
        }
       
    }
    //g�r rep. det g�rs lite senare en update annars ser det skit ut av n�gon anledning
    private void LateUpdate()
    {
        DrawPope();
    }
    void DrawPope()
    {
        // g�r en line renderer
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
