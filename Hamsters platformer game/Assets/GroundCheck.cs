using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{

    [SerializeField] private Transform rayCastOrigin;
    [SerializeField] private Transform playerFeet;
    [SerializeField] private LayerMask layerMask;
    private RaycastHit2D hit2D;

    // Update is called once per frame
    void Update()
    {
        GroundCheckMethod();
    }

    private void GroundCheckMethod()
    {
        hit2D = Physics2D.Raycast(rayCastOrigin.position, -Vector2.up, 100f, layerMask);
        if(hit2D != false)
        {
            Vector2 temp = playerFeet.position;
            temp.y = hit2D.point.y;
            playerFeet.position = temp;
        }
    }
}
