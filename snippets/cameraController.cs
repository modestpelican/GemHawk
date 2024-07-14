using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    [SerializeField] private Transform player;  // editable in unity

    private void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);   // keeping y constant

    }
}
