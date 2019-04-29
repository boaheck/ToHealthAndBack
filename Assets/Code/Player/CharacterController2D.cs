using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour {

	Rigidbody2D rigb;
	BoxCollider2D col;
	RaycastHit2D hit;
	Vector2 pos, boxSize, boxOffset;
	public LayerMask groundLayers;
	public bool grounded, head, left, right, wall;
	public float timeSinceLastGrounded = 0.0f;
	public float detectOffset = 0.02f;
	public float detectionBoxScale = 0.95f;

	void Start () {
		rigb = GetComponent<Rigidbody2D> ();
		col = GetComponent<BoxCollider2D> ();
		boxSize = (col.size + (Vector2.one * 2 * col.edgeRadius)) * detectionBoxScale;
		boxOffset = col.offset;
		pos = transform.position;
	}

	public void setPos(Vector3 nPos){
		transform.position = new Vector3 (nPos.x, nPos.y);
		pos = nPos;
	}

	public void Move (Vector2 move) {
		rigb.MovePosition (pos + move);
	}

	public int WallDir() {
		if(left){
			return -1;
		}else if(right){
			return 1;
		}
		return 0;
	}

	void Update () {
		pos = new Vector2 (transform.position.x, transform.position.y);
		Vector2 groundBoxPos = pos + boxOffset + (Vector2.down * detectOffset);
		Vector2 headBoxPos = pos + boxOffset + (Vector2.up * detectOffset);
		Vector2 leftBoxPos= pos + boxOffset + (Vector2.left * detectOffset);
		Vector2 rightBoxPos= pos + boxOffset + (Vector2.right * detectOffset);
		grounded = Physics2D.OverlapBox (groundBoxPos, boxSize,0,groundLayers.value);
		if(grounded){
			Debug.DrawRay(groundBoxPos,Vector3.down *(boxSize.y/2),Color.green);
		}else{
			Debug.DrawRay(groundBoxPos,Vector3.down *(boxSize.y/2),Color.red);
		}
		head = Physics2D.OverlapBox (headBoxPos, boxSize,0,groundLayers.value);
		if(head){
			Debug.DrawRay(headBoxPos,Vector3.up *(boxSize.y/2),Color.green);
		}else{
			Debug.DrawRay(headBoxPos,Vector3.up *(boxSize.y/2),Color.red);
		}
		left = Physics2D.OverlapBox (leftBoxPos, boxSize,0,groundLayers.value);
		if(left){
			Debug.DrawRay(leftBoxPos,Vector3.left *(boxSize.x/2),Color.green);
		}else{
			Debug.DrawRay(leftBoxPos,Vector3.left *(boxSize.x/2),Color.red);
		}
		right = Physics2D.OverlapBox (rightBoxPos, boxSize,0,groundLayers.value);
		if(right){
			Debug.DrawRay(rightBoxPos,Vector3.right *(boxSize.x/2),Color.green);
		}else{
			Debug.DrawRay(rightBoxPos,Vector3.right *(boxSize.x/2),Color.red);
		}
		wall = left || right;
		if(grounded){
			timeSinceLastGrounded = 0.0f;
			

		}else{
			timeSinceLastGrounded += Time.deltaTime;
		}
	}
}