using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
	public int dir = -1;
    [SerializeField]
    private float rotationSpeed = 200f;
	[SerializeField]
	private float linearSpeed = 3f;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed);
		var position = transform.position;
		position.x += Time.deltaTime * dir * linearSpeed;
		transform.position = position;
    }
}
