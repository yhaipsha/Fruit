using UnityEngine;
using System.Collections;

public class TurnAnimate : MonoBehaviour {
	
	public UISprite sprite;
	public UISprite spriteBg;
	
	//只有精灵脚本
	private GameObject target;
	public bool autoReverse = false;

	UISlicedSprite spHead;
	// Use this for initialization
	void Start ()
	{		
		target = GameObject.FindWithTag ("Player");
		
		Globe.sameSize = new System.Collections.Generic.Dictionary<string, int> ();
		Globe.differentSize = new System.Collections.Generic.Dictionary<string, int> ();
		Globe.tempGameObject = new System.Collections.Generic.List<UnityEngine.GameObject> ();
		
		if (target != null) {
			ExampleAtlas ra = target.transform.GetComponent<ExampleAtlas> ();
			ra.EventReplace += new ExampleAtlas.replaceSprite (OnClick);
			spHead = target.transform.GetComponent<UISlicedSprite> ();	
		}
	}
	void OnClick ()
	{		
		if (sprite != null && spriteBg != null) {//		&& !isBegin			
//			transform.animation["SpriteTurn"].wrapMode=WrapMode.Clamp;
			animation.Play("TurnGo");
//			animation.Play("SpriteTurn",PlayMode.StopSameLayer);
			
//			animation.Play(AnimationPlayMode.Mix);
			switch (PlayerPrefs.GetInt ("NowMode")) {
			case 1:
				mode1 ();
				break;
			case 2:
				mode2 ();
				break;
			}
			
		}
	}

	void mode1 ()
	{//"标准模式-看3秒，找出指定水果，限错3次";
		string theNumber = RegexUtil.RemoveNotNumber (sprite.spriteName);
		string head = RegexUtil.RemoveNotNumber (spHead.spriteName);
		if (head == theNumber) {
			//signName = "Right";				
			if (Globe.sameSize.ContainsKey (spHead.spriteName)) {
				Globe.sameSize [spHead.spriteName]++;
			} else
				Globe.sameSize.Add (spHead.spriteName, 1);
		} else {
			//signName = "Wrong";				
			if (Globe.differentSize.ContainsKey (transform.name)) {
				Globe.differentSize [transform.name]++;
			} else
				Globe.differentSize.Add (transform.name, 1);
		}						
			

		ExampleAtlas ra = target.transform.GetComponent<ExampleAtlas> (); 
		if (target != null) {
			if (head == theNumber) {
				playReplace ();
				autoReverse = false;
				ra.NextSprite (spHead.spriteName);
			} else {				
//				transform.animation["SpriteTurn"].time=0;
				animation.Play("TurnBack");
//				transform.animation.Stop();
//				transform.animation.Sample();
//				gameObject.SampleAnimation(animation.clip, 0);				
				SendMessageUpwards("UpdateTime",3-Globe.differentSize.Count);		
				
				if (Globe.differentSize.Count >= 3) {
					ra.toPanelWin (0);
				}
			}

		}
	}
	void mode2 ()
	{//经典模式-看5秒找相同水果，限错N次";

		if (Globe.askatlases.Count > 0 && Globe.thisPanel != null && Globe.askatlases [0] != transform.name) {
			
			UISprite ltsp = Globe.thisPanel.FindChild ("Sprite-box").GetComponent<UISprite> ();
			UISprite tsp = transform.FindChild ("Sprite-box").GetComponent<UISprite> ();
		
			print (ltsp.spriteName + "?" + tsp.spriteName);
			if (ltsp.spriteName == tsp.spriteName) {
//				playReplace ();
				Destroy (Globe.thisPanel.gameObject);
				Destroy (gameObject);
				Globe.askatlases.Clear ();
			} else {
				;
//				StartCoroutine(show());
				Globe.thisPanel.gameObject.GetComponent<TurnRight2> ().Turn ();
//				this.OnClick();
			}
			
			Globe.thisPanel = null;
			Globe.askatlases.Clear ();
			
		} else if (!Globe.askatlases.Contains (transform.name)) {
			//记录上次精灵
			Globe.askatlases.Add (transform.name);
			Globe.thisPanel = transform;
			print (transform.name);
			autoReverse = false;
		}
		
		if (transform.parent.GetChildCount () <= 0) {
			print ("----------");
			GameWinLayer gw = Globe.getPanelOfParent (transform.parent.parent, 1, "Panel - GameWin").GetComponent<GameWinLayer> ();
			gw.init (1);
			
		}
			
	}
	void playOver(string animateName)
	{
		print (animateName);
	}
	IEnumerator playReplace ()
	{
		//		ExampleAtlas ra = target.transform.GetComponent<ExampleAtlas>();
		//		ra.init();
		//		autoReverse = false;
		yield return new WaitForSeconds(0.9f);
		//		ra.NextSprite(sp.spriteName);
	}
}
