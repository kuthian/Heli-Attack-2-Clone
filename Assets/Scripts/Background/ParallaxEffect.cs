using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public Camera cam;
    public Transform subject;

    Vector2 startPosition;

    float startZ;

    Vector2 travel => (Vector2)cam.transform.position - startPosition;

    float distaceFromSubject => transform.position.z - subject.position.z;
    float clippingPlane => (cam.transform.position.z + (distaceFromSubject > 0 ? cam.farClipPlane : cam.nearClipPlane));
    float parallaxFactor => Mathf.Abs(distaceFromSubject) / clippingPlane;

    

    private void Start()
    {
        startPosition = transform.position;
        startZ = transform.position.z;
    }

    private void Update()
    {
        Vector2 newPos = startPosition + travel * parallaxFactor;
        transform.position = new Vector3(newPos.x, newPos.y, startZ);
        
    }


}
