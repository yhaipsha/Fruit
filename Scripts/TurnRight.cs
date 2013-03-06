using UnityEngine;
using System.Collections;

public class TurnRight : MonoBehaviour
{
	/// <summary>
	/// 绕sprite中心左右旋转
	/// </summary>
	
//	public delegate void deleEnemyDie();
//	public event deleEnemyDie EvenEnemyDie;
	public delegate void clicked(string name) ;
	public event clicked eventClicked;
	
	public UISprite sprite;
	public UISprite spriteBg;

	private GameObject target;
	private bool isBegin = false;

	public bool autoReverse = false;
	int clickCount = 0;
	Transform trans;
	UISprite sp;
	// Use this for initialization
	void Start ()
	{		
		sprite.enabled = false;
		target = GameObject.FindWithTag ("Player");
		
		if(target != null){
			RondamAtlas ra = target.transform.GetComponent<RondamAtlas>();
			ra.EventReplace +=	new RondamAtlas.replaceSprite(OnClick);
			//sp=target.transform.GetComponentInChildren<UISprite>();
			trans = target.transform.FindChild("UISprite");
			sp = trans.GetComponent<UISprite>();	
		}
	}
	
	void OnClick ()
	{
		++clickCount;
		if (sprite != null && spriteBg != null 	) {//		&& !isBegin
						
			isBegin = true;			
			qua = Quaternion.Euler (0f, 180f, 0f) * sprite.transform.localRotation;	
			quaBg = Quaternion.Euler (0f, 180f, 0f) * spriteBg.transform.localRotation;
			if(target !=null){
				if(sp.spriteName == sprite.spriteName && sp.atlas.name =="AtlasAnimate2"){
					StartCoroutine(playReplace());
				}
			}
			//UISprite sp = target.transform.GetComponent<RondamAtlas>();
			//this.EventReversed+=
			

			//ra.EventReversed+=new reversed("");
		}		
	}
	IEnumerator playReplace()
	{
		RondamAtlas ra = target.transform.GetComponent<RondamAtlas>();
		autoReverse = false;
		yield return new WaitForSeconds(0.9f);
		ra.NextSprite(sp.spriteName);
	}
	
	Quaternion qua;
	Quaternion quaBg;
	void Update ()
	{
			
		if (isBegin) {
						
			spriteBg.transform.rotation = Quaternion.Slerp (spriteBg.transform.rotation, quaBg, Time.deltaTime * 3);   
			sprite.transform.rotation = Quaternion.Slerp (sprite.transform.rotation, qua, Time.deltaTime * 3);     		

			float vEuler = Mathf.Round (spriteBg.transform.rotation.eulerAngles.y);
			//反面
			IsNotCorrect (vEuler);
			//正面
			IsCorrect (vEuler);
			
		}
			
	}

	UIItemStorageMy item;

	

	void IsNotCorrect (float euler)
	{
		if (0 < euler && euler < 180) {
			if (0 < euler && euler < 90) {
				//isBegin=false;
				spriteBg.enabled = true;
				sprite.enabled = false;
			}
			if (euler > 90) {					
				if(autoReverse && clickCount!=0)
				{
					this.OnClick();
					
				}else{
					//spriteBg.alpha=1f;			
					spriteBg.enabled = false;
					sprite.enabled = true;
				}
				isBegin	=true;
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
				if(autoReverse&& clickCount!=0)
				{
					this.OnClick();
					
				}else{
					spriteBg.enabled = true;
					sprite.enabled = false;
				}
				//isBegin=false;
					


			}

		}
	}
}
