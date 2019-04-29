using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour {

	Rigidbody2D rigb;
	BoxCollider2D col;
	Vector2 pos, boxSize, boxOffset;
	public LayerMask groundLayers;
	public bool grounded, head, left, right, wall;
	public float timeSinceLastGrounded = 0.0f;

	void Start () {
		rigb = GetComponent<Rigidbody2D> ();
		col = GetComponent<BoxCollider2D> ();
		boxSize = (col.size + (Vector2.one * 2 * col.edgeRadius)) * 0.9f;
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
		grounded = Physics2D.OverlapBox (pos + boxOffset + (Vector2.down * 0.06f), boxSize,0,groundLayers.value);
		head = Physics2D.OverlapBox (pos + boxOffset + (Vector2.up * 0.06f), boxSize,0,groundLayers.value);
		left = Physics2D.OverlapBox (pos + boxOffset + (Vector2.left * 0.06f), boxSize,0,groundLayers.value);
		right = Physics2D.OverlapBox (pos + boxOffset + (Vector2.right * 0.06f), boxSize,0,groundLayers.value);
		wall = left || right;
		if(grounded){
			timeSinceLastGrounded = 0.0f;
		}else{
			timeSinceLastGrounded += Time.deltaTime;
		}
	}
}