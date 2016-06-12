using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	private float moveSpeed = .058f;
	private Animator ani;

	public int xStart = 2;
	public int yStart = 2;
	private GridLocation location;
	private WalkDirection walkingDir = WalkDirection.None;
	// Use this for initialization
	void Start () 
	{
		ani = this.GetComponent<Animator> ();
		location = new GridLocation (xStart, yStart);
	}

	void SetMovement(string animationString)
	{
		if(animationString != "None")
		{
			isMoving = true;
			justMoved = false;
			ani.SetBool (animationString, true);
			switch(animationString)
			{
			case "WalkRight":
				ani.SetBool("WalkLeft", false);
				ani.SetBool("WalkUp", false);
				ani.SetBool("WalkDown", false);
				walkingDir = WalkDirection.Right;
				break;
			case "WalkLeft":
				ani.SetBool("WalkRight", false);
				ani.SetBool("WalkUp", false);
				ani.SetBool("WalkDown", false);
				walkingDir = WalkDirection.Left;
				break;
			case "WalkUp":
				ani.SetBool("WalkLeft", false);
				ani.SetBool("WalkRight", false);
				ani.SetBool("WalkDown", false);
				walkingDir = WalkDirection.Up;
				break;
			case "WalkDown":
				ani.SetBool("WalkLeft", false);
				ani.SetBool("WalkUp", false);
				ani.SetBool("WalkRight", false);
				walkingDir = WalkDirection.Down;
				break;
			}
		}
		else
		{
			ani.SetBool("WalkLeft", false);
			ani.SetBool("WalkUp", false);
			ani.SetBool("WalkRight", false);
			ani.SetBool("WalkDown", false);
			walkingDir = WalkDirection.None;
		}
	}

	bool isMoving = false;
	bool justMoved = true;
	GridLocation moveTo;
	GameObject goMoveTo;
	Vector3 nextGridLoc;
	WalkDirection nextDirection = WalkDirection.None;

	// Update is called once per frame
	void FixedUpdate () 
	{
		DoMovement ();
	}

	void OnGUI()
	{
		GUI.Label (new Rect (10, 10, 100, 30), "X: " + location.x.ToString() + ", Y: " + location.y.ToString());
	}

	void Update()
	{
		if(justMoved)
		{
			if(nextDirection == WalkDirection.Right)
			{
				//if(!ani.GetBool("WalkRight"))
				//{
				SetMovement("WalkRight");
				moveTo = this.location;
				
				if(World.LayerRight(moveTo) != LayerMask.GetMask("Block"))
				{
					goMoveTo = World.GameobjectFromLocation(moveTo);
					nextGridLoc = this.transform.position + Vector3.right;
				}else 
				{
					Debug.Log("FUC");
				}
				//}
			}
			else if(nextDirection == WalkDirection.Left)
			{
				//if(!ani.GetBool("WalkLeft"))
				//{
				
				if(World.LayerLeft(moveTo) != LayerMask.GetMask("Block"))
				{
					SetMovement("WalkLeft");
					moveTo = this.location;
					goMoveTo = World.GameobjectFromLocation(moveTo);
					nextGridLoc = this.transform.position - Vector3.right;
				}
				else 
				{
					Debug.Log("FUC");
				}
				//}
			}
			else if(nextDirection == WalkDirection.Up)
			{
				//if(!ani.GetBool("WalkUp"))
				//{
				SetMovement("WalkUp");
				moveTo = this.location;
				
				if(World.LayerAbove(moveTo) != LayerMask.GetMask("Block"))
				{
					goMoveTo = World.GameobjectFromLocation(moveTo);
					nextGridLoc = this.transform.position + Vector3.up;
				}else 
				{
					Debug.Log("FUC");
				}
				//}
			}
			else if(nextDirection == WalkDirection.Down)
			{
				//if(!ani.GetBool("WalkDown"))
				//{
				SetMovement("WalkDown");
				moveTo = this.location;
				
				if(World.LayerBelow(moveTo) != LayerMask.GetMask("Block"))
				{
					goMoveTo = World.GameobjectFromLocation(moveTo);
					nextGridLoc = this.transform.position - Vector3.up;
				}else 
				{
					Debug.Log("FUC");
				}
				//}
			}
			else 
			{
				justMoved = true;
				SetMovement("None");
			}
		}


		if(Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0)
		{
			nextDirection = WalkDirection.None;
		}
		if(Input.GetKeyDown(KeyCode.D) ||Input.GetKeyDown(KeyCode.RightArrow))
		{
			if(World.LayerRight(location) != LayerMask.GetMask("Block"))
			{	
				nextDirection = WalkDirection.Right;
			}
			else
			{
				
				Debug.Log ("FUCK");
			}
		}
		if(Input.GetKeyDown(KeyCode.A) ||Input.GetKeyDown(KeyCode.LeftArrow))
		{
			nextDirection = WalkDirection.Left;
		}
		if(Input.GetKeyDown(KeyCode.W) ||Input.GetKeyDown(KeyCode.UpArrow))
		{
			nextDirection = WalkDirection.Up;
		}
		if(Input.GetKeyDown(KeyCode.S) ||Input.GetKeyDown(KeyCode.DownArrow))
		{
			nextDirection = WalkDirection.Down;
		}
	}

	void DoMovement()
	{
		if(isMoving)
		{
			switch(walkingDir)
			{
			case WalkDirection.Right:
				if(World.LayerRight(moveTo) != 8)
				{
					this.transform.position += Vector3.right * moveSpeed;
				}
				else
				{
					SetMovement("None");
					nextGridLoc -= Vector3.right;
				}
				break;
			case WalkDirection.Left:
				if(World.LayerLeft(moveTo) != 8)
				{
					this.transform.position -= Vector3.right * moveSpeed;
				}
				else
				{
					SetMovement("None");
					nextGridLoc += Vector3.right;
				}
				break;
			case WalkDirection.Up:
				if(World.LayerAbove(moveTo) != 8)
				{
					this.transform.position += Vector3.up * moveSpeed;
				}
				else
				{
					SetMovement("None");
					nextGridLoc -= Vector3.up;
				}
				break;
			case WalkDirection.Down:
				if(World.LayerBelow(moveTo) != 8)
				{
					this.transform.position -= Vector3.up * moveSpeed;
				}
				else
				{
					SetMovement("None");
					nextGridLoc += Vector3.up;
				}
				break;
			case WalkDirection.None:
				//do nothing
				break;
			}

			if(Vector3.Distance(nextGridLoc, this.transform.position) < .1f)
			{
				this.transform.position = nextGridLoc;
				isMoving = false;
				justMoved = true;

				//GameObject.Destroy(World.GameobjectFromLocation(location));

				switch(walkingDir)
				{
					case WalkDirection.Right:
					location += GridLocation.right;
						break;
					case WalkDirection.Left:
					location -= GridLocation.right;
						break;
					case WalkDirection.Up:
					location -= GridLocation.up;
						break;
					case WalkDirection.Down:
					location += GridLocation.up;
						break;
					case WalkDirection.None:
					//do nothing
						break;
				}
			}
		}
	}
}

public enum WalkDirection
{
	Up,
	Right,
	Left,
	Down,
	None
}
