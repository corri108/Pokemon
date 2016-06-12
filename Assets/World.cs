using UnityEngine;
using System.Collections;
using System;

public class World : MonoBehaviour {

	public static Tile[,] levelWorld;
	public GameObject topLeftTile;
	static GameObject[] tot = null;
	// Use this for initialization
	void Start () 
	{
		//store the world objects linearly
		tot = GameObject.FindGameObjectsWithTag ("Tile");
		Debug.Log ("Total Tiles: " + tot.Length);

		int totalX = GrabXBounds ();
		int totalY = GrabYBounds ();
		levelWorld = new Tile[totalX, totalY];
		CreateWorld (totalX, totalY);

		//GameObject.Destroy (levelWorld [3, 3].gameObject);
	}

	private void CreateWorld(int tx, int ty)
	{
		Debug.Log ("Creating World...");

		for(int i = 0; i < tx; ++i)
		{
			for(int j = 0; j < ty; ++j)
			{
				//Debug.Log ("X: " + i + ", Y: " + j);
				GameObject tile = FindByPosition(
					topLeftTile.transform.position.x + i,
					topLeftTile.transform.position.y - j);
				levelWorld[i, j] = new Tile(tile.GetComponent<SpriteRenderer>().sprite,
				                            new GridLocation(i,j), tile.layer, tile.gameObject);
			}
		}

		Debug.Log ("World Created.");
	}

	/// <summary>
	/// Gets the Gameobjects from the associated location.
	/// </summary>
	/// <returns>The from location.</returns>
	/// <param name="gl">Gl.</param>
	public static GameObject GameobjectFromLocation(GridLocation gl)
	{
		return levelWorld [gl.x, gl.y].gameObject;
	}

	/// <summary>
	/// Finds a gameobject by a given position.
	/// </summary>
	/// <returns>The by position.</returns>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
	public static GameObject FindByPosition(float xx, float yy)
	{
		float x = (float)xx;
		float y = (float)yy;

		for(int i = 0; i < tot.Length; ++i)
		{
			float compX = (float)(tot[i].transform.position.x);
			//Debug.Log ("X: " + compX);
			if(compX.Equals(x))
			{
				float compY = (float)(tot[i].transform.position.y);
				//Debug.Log ("Y: " + compY);
				if(compY.Equals(y))
				{
					return tot[i];
				}
			}
		}

		return null;
	}

	/// <summary>
	/// Gets the layer above the location.
	/// </summary>
	/// <returns>The above.</returns>
	/// <param name="gl">Gl.</param>
	public static int LayerAbove(GridLocation gl)
	{
		GameObject placeholder = levelWorld[gl.x, gl.y - 1].gameObject;
		return placeholder != null ? placeholder.layer : -1;
	}

	/// <summary>
	/// Gets the layer below the location.
	/// </summary>
	/// <returns>The above.</returns>
	/// <param name="gl">Gl.</param>
	public static int LayerBelow(GridLocation gl)
	{
		GameObject placeholder = levelWorld [gl.x, gl.y + 1].gameObject;
		return placeholder != null ? placeholder.layer : -1;
	}

	/// <summary>
	/// Gets the layer to the right of the location.
	/// </summary>
	/// <returns>The above.</returns>
	/// <param name="gl">Gl.</param>
	public static int LayerRight(GridLocation gl)
	{
		GameObject placeholder = levelWorld [gl.x + 1, gl.y].gameObject;
		//placeholder.GetComponent<SpriteRenderer> ().sprite = null;
		Debug.Log ("X: " + (gl.x + 1).ToString () + "Y: " + gl.y.ToString ());
		return placeholder != null ? placeholder.layer : -1;
	}

	/// <summary>
	/// Gets the layer to the left of the location.
	/// </summary>
	/// <returns>The above.</returns>
	/// <param name="gl">Gl.</param>
	public static int LayerLeft(GridLocation gl)
	{
			GameObject placeholder = levelWorld[gl.x - 1, gl.y].gameObject;
			Debug.Log ("X: " + (gl.x - 1).ToString () + "Y: " + gl.y.ToString ());
			return placeholder != null ? placeholder.layer : -1;
	}

	/// <summary>
	/// Grabs the X bounds.
	/// </summary>
	/// <returns>The X bounds.</returns>
	public int GrabXBounds()
	{
		GameObject next = topLeftTile;
		int i = 0;
		while(next != null)
		{
			next = FindByPosition(next.transform.position.x + 1, next.transform.position.y);
			++i;
		}

		return i;
	}

	/// <summary>
	/// Grabs the Y bounds.
	/// </summary>
	/// <returns>The Y bounds.</returns>
	public int GrabYBounds()
	{
		GameObject next = topLeftTile;
		int i = 0;
		while(next != null)
		{
			next = FindByPosition(next.transform.position.x, next.transform.position.y - 1);
			++i;
		}
		
		return i;
	}

	// Update is called once per frame
	void Update () 
	{
		
	}
}
