using UnityEngine;
using System.Collections;

public class Tile
{
	private Sprite sprite;

	public Sprite GetSprite()
	{
		return sprite;
	}

	private GridLocation position;

	public GridLocation GetPosition()
	{
		return position;
	}

	public int layer;
	public GameObject gameObject;

	//public GameObject gameObject = null;

	public Tile(Sprite s, GridLocation pos, int layer, GameObject go)
	{
		this.sprite = s;
		this.position = pos;
		this.layer = layer;
		this.gameObject = go;
	}
}

