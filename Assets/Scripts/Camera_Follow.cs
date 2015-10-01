using UnityEngine;
using System.Collections;

public class Camera_Follow : MonoBehaviour {

	public enum followType {X_Axis_Only, X_And_Y_Axis};
	public followType FollowType;
	public float Smooth;

	public Vector2 Offset;
	
	//Non Public Variable
	Transform Target;

	Vector3 startPos;
	Vector3 endPos;

	// Use this for initialization
	void Start () 
	{
		Target = GameObject.FindGameObjectWithTag ("Player").transform;

	}
	
	// Update is called once per frame
	void Update () 
	{
		startPos = transform.position;

		if (FollowType == followType.X_And_Y_Axis) {
			endPos = new Vector3 (Target.position.x + Offset.x, Target.position.y + Offset.y, transform.position.z);
		}

		if (FollowType == followType.X_Axis_Only) {
			endPos = new Vector3(Target.position.x, 0, transform.position.z);
		}

		//Bergerak Lembut Menuju Posisi Akhir Camera
		transform.position = Vector3.Lerp (startPos, endPos, Smooth * Time.deltaTime);
	}
}
