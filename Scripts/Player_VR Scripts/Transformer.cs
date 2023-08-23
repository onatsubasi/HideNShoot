using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Transformer : MonoBehaviour
{
    [SerializeField] Transform other;
    Vector3 offset;

    private void Start()
    {
        offset = transform.position - other.position;
    }
    private void LateUpdate()
    {
        transform.position = other.position + offset;
        //transform.Rotate(Vector3.up, other.rotation.y - last);
        transform.localRotation = Quaternion.Euler(other.localRotation.eulerAngles.y * Vector3.up);
    }
}