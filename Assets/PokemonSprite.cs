using UnityEngine;
using System.Collections;

public class PokemonSprite : MonoBehaviour {

	int currentIndex = 0;
	public bool isFontFacing = true;
	public Sprite[] frontFrames;
	public Sprite[] backFrames;
	public int playtime = 20;
	private int playtimeReset = 20;
	SpriteRenderer rend;
	// Use this for initialization
	void Start () 
	{
		playtimeReset = playtime;
		rend = this.gameObject.GetComponent<SpriteRenderer> ();
		if (isFontFacing)
			rend.sprite = frontFrames [0];
		else
			rend.sprite = backFrames [0];
	}	
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		playtime--;

		if(playtime == 0)
		{
			if(isFontFacing)
			{
				if(currentIndex == frontFrames.Length - 1)
					currentIndex = 0;
				else 
					currentIndex++;

				rend.sprite = frontFrames[currentIndex];
			}
			else
			{
				if(currentIndex == backFrames.Length - 1)
					currentIndex = 0;
				else 
					currentIndex++;
				
				rend.sprite = backFrames[currentIndex];
			}
			playtime  = playtimeReset;
		}
	}
}
