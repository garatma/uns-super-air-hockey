using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckDrag : MonoBehaviour {

	public Transform initialPosition;
	private float targetWidth,targetHeight;
	float width,height;

	// Use this for initialization
	void Start () {
		width = Screen.width;
		height = Screen.height;
		Debug.Log("w: "+width + " - h: " +height);

		targetWidth = initialPosition.localScale.x*  1.0f;
		targetHeight= initialPosition.localScale.z* 1.0f;
	}
	
	// Update is called once per frame
	void Update () {
		//Vector3 pos= (Input.mousePosition);
		//pos.z = pos.y;
		//pos.y = 0;

		Vector3 targetPos = Vector3.one;
		targetPos.x =( targetWidth / width ) *Input.mousePosition.x;
		targetPos.z = ( targetHeight /height ) *Input.mousePosition.y;

		targetPos.x += initialPosition.position.x - (targetWidth / 2.0f) ;
		targetPos.z += initialPosition.position.z - (targetHeight / 2.0f) ;

		transform.position = targetPos;
	}


}
