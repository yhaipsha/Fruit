using UnityEngine;
using System.Collections;

public class TurnManager : MonoBehaviour
{
	public bool autoReverse = false;
	public Transform transExample;
	public Transform transTimes;
	int initialized = 0;
	
	int clickCount = 0;
	private bool isBegin = false;
	Quaternion qua;
	Quaternion quaBg;
	UISprite sprite;
	UISprite spriteBg;
	bool beginTurn = false;

	void examPlay ()
	{
		if (transExample != null) {
			transExample.GetComponent<UISlicedSprite> ().enabled = true;
			transExample.animation.Play ();
//			transExample.animation.Play(AnimationPlayMode.Mix);
//			animation["name"].time = 0;
//			ExampleAtlas ea = transExample.GetComponent<ExampleAtlas>();
//			if (ea != null) {
//				print (ea.GetInstanceID());
//				playWait ();
//			}
//				init (2);
//			PlayerPrefs.DeleteKey ("cardReady");
		}
	}
	
	void UpdateTime (int score)
	{
		if (transTimes != null) {
			transTimes.GetComponent<UILabel> ().text = score.ToString ();
		}
	}
	// Update is called once per frame
	void Update ()
	{
		if (PlayerPrefs.GetInt ("cardReady") == 1 && initialized != 0) {
			StartCoroutine (show ());
		}
	}

	IEnumerator show ()
	{				
		if (initialized == 1) {
			yield return  new WaitForSeconds(3.5f);
			init (-1);			
		} 
		if(initialized == 2){
			yield return null;
			this.examPlay ();
			init (2);
			PlayerPrefs.DeleteKey ("cardReady");
			initialized=0;
		}		
	}

	public void init (int state)
	{		
		for (int i = 0; i < transform.GetChildCount(); i++) {
			Transform _trans = transform.GetChild (i);
//			Animation animate = _trans.animation;
			if (state==1) {
				_trans.animation.Play ();
			} else if(state == 2) {
				_trans.animation.Stop();
			}else if(state == -1){
				_trans.animation ["TurnGo"].time = 0;
				_trans.animation.Play ();
			}
		}
		if (state == 1) {
			initialized = 1;
		} else if(state == -1){
			initialized = 2;
		}
		
	}

	bool IsNotCorrect2 (float euler)
	{
//		print (euler);
//		if (euler ==0) {
//			return false;
//		}
		if (270 < euler && euler < 360) {
			spriteBg.enabled = false;
			sprite.enabled = true;
		} else if (195 < euler && euler < 270) {
			spriteBg.enabled = true;
			sprite.enabled = false;
			
		} else if (180 < euler && euler < 195) {
			return true;
		}		
		return false;
		
	}	
	bool IsCorrect2 (float euler)
	{
		//做减法运算,默认已经旋转180
//		print (euler);
		if (euler > 300) {
			return true;
		}
		if (90 < euler && euler < 180) {			
			spriteBg.enabled = true;
			sprite.enabled = false;
		} else if (5 < euler && euler < 90) {
			spriteBg.enabled = false;
			sprite.enabled = true;
		} else if (0 < euler && euler < 5) {
			return isBegin = true;
		}
		return false;		
	}
	
	void OnTurn ()
	{
		isBegin = true;
		qua = Quaternion.Euler (0f, 180f, 0f) * sprite.transform.localRotation;
		quaBg = Quaternion.Euler (0f, 180f, 0f) * spriteBg.transform.localRotation;
		
//		spriteBg.transform.rotation = Quaternion.Slerp (spriteBg.transform.rotation, quaBg, Time.deltaTime);   
//		sprite.transform.rotation = Quaternion.Slerp (sprite.transform.rotation, qua, Time.deltaTime);    
	}		
	
	void IsNotCorrect (float euler)
	{
		if (0 < euler && euler < 180) {
			if (0 < euler && euler < 90) {
				//isBegin=false;
				spriteBg.enabled = true;
				sprite.enabled = false;
			}
			if (euler > 90) {					
				if (autoReverse) {
					this.OnTurn ();
					
				} else {
					//spriteBg.alpha=1f;			
					spriteBg.enabled = false;
					sprite.enabled = true;
				}
				isBegin = true;
			}
			
			clickCount = 0;	
			//isCorrect=false;
		}
	}	
	void IsCorrect (float euler)
	{
		if (180 < euler && euler < 360) {				
			if (180 < euler && euler < 270) {
				//转过270度角
				//spriteBg.alpha=1f;			
				spriteBg.enabled = false;
				sprite.enabled = true;
			}
			if (euler > 270) {
				if (autoReverse) {
					this.OnTurn ();
					
				} else {
					spriteBg.enabled = true;
					sprite.enabled = false;
				}
				//isBegin=false;
			}

		}
	}
	
}
