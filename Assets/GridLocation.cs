using UnityEngine;
using System.Collections;

public class GridLocation
{
	public int x = -1;
	public int y = -1;

	public GridLocation()
	{
		x = 0;
		y = 0;
	}

	public GridLocation(int _x, int _y)
	{
		x = _x;
		y = _y;
	}

	public static GridLocation right = new GridLocation (1, 0);
	public static GridLocation up = new GridLocation (0, 1);

	public static GridLocation operator +(GridLocation c1, GridLocation c2) 
	{
		return new GridLocation(c1.x + c2.x, c1.y + c2.y);
	}

	public static GridLocation operator -(GridLocation c1, GridLocation c2) 
	{
		return new GridLocation(c1.x - c2.x, c1.y - c2.y);
	}
}
