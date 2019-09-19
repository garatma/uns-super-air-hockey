using System.Collections;
using UnityEngine;

public class control_sombrero : MonoBehaviour
{
	float speed = 10.0F;
	float rotationSpeed = 100.0F;

    // Update is called once per frame
    void Update()
    {
    	float translationX = Input.GetAxis("Horizontal") * speed;
		float translationZ = Input.GetAxis("Vertical") * speed;

		translationX *= Time.deltaTime;
		translationZ *= Time.deltaTime;
		transform.Translate(translationX,0,translationZ);
    }
}
