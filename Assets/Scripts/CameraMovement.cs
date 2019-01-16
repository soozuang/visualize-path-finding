
using System;
using UnityEngine;
namespace Zombies
{
	public class CameraMovement : MonoBehaviour{
        readonly int sensitivity = 200;
        readonly int scrollSensitivity = 500;
        readonly int scrollMaxDistance = 9999;
        readonly int scrollMinDistance = 5;
	
		void Start() {
		}
				
		// Update is called once per frame
		void Update () {

			float moveRate = sensitivity * Time.deltaTime;
			float scrollRate = scrollSensitivity * Time.deltaTime;

			float deltaX = 0;
			float deltaY = 0;
			float deltaZ = 0;

			// Move the camera with the arrow keys or with the mouse.
			if ( Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)){
					deltaY = moveRate;
			}
			if ( Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)){
					deltaY = -moveRate;
			}
			if ( Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)){
					deltaX = moveRate;
			}
			if ( Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)){
					deltaX = -moveRate;
			}

			// Zoom in/out with the scroll wheel.
			if (Input.GetAxis("Mouse ScrollWheel") > 0){
				if (GetComponent<Camera>().orthographicSize - scrollRate > scrollMinDistance)
					GetComponent<Camera>().orthographicSize = GetComponent<Camera>().orthographicSize - scrollRate;
				else
					GetComponent<Camera>().orthographicSize = scrollMinDistance;
			}
			if (Input.GetAxis("Mouse ScrollWheel") < 0){
				if (GetComponent<Camera>().orthographicSize + scrollRate < scrollMaxDistance)
					GetComponent<Camera>().orthographicSize = GetComponent<Camera>().orthographicSize + scrollRate;
				else
					GetComponent<Camera>().orthographicSize = scrollMaxDistance;

			}

            MoveCamera(deltaX, deltaY, deltaZ);
		}

		private void MoveCamera(float x, float y, float z){
			// Different coordinate standards.
			transform.position = new Vector3(transform.position.x + x, transform.position.y + z, transform.position.z + y);
		}

		public void SetStartingPosition(Vector3 position){
			transform.position = new Vector3(position.x, transform.position.y, position.z);
		}

		public float GetDistanceRatio(){
			return GetComponent<Camera>().orthographicSize / scrollMaxDistance;
		}
	
	}
}

