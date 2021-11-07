using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalScroll : MonoBehaviour
{
    [SerializeField] float speed = .2f;
    private void Update()
    {
        transform.Translate(Vector3.up*speed *Time.deltaTime);
    }
}
