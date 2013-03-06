using UnityEngine;
using System.Collections;

public class GamePauseAftermath : MonoBehaviour
{

	//清理卡牌
	//清理关卡
	// Use this for initialization
	void Start ()
	{
		
	}
	
	public Transform transLevelPanel;
	public Transform transPausePanel;
	public bool resetPause = true;
	public bool resetPlay = true;
//	public bool resetLevel = true;
	public bool removeCard = false;

	void OnClick ()
	{
		GamePlayLayer gp =Globe.getPanelOfParent (transLevelPanel, 1, "Panel - GamePlay").GetComponent<GamePlayLayer> ();
		if (resetPlay)
			gp.resetCards ();		
		if (removeCard)
			gp.removeCards();
		
//		if (resetLevel)
//			cleanLevel (transLevelPanel);		
		if (resetPause && transPausePanel != null)
			resetPanelPause ();
	}
		
	void resetPanelPause ()
	{
		if (transPausePanel != null) {
			
			TweenPosition tp = transPausePanel.GetComponent<TweenPosition> ();	
			tp.from = new Vector3 (0f, 0f, Globe.gamePause.z);
			tp.to = Globe.gamePause;
			tp.Play(true);

		}
		
	}

		
	/// <summary>
	/// 清理关卡
	/// </summary>
	/// <param name='trans'>
	/// Trans.
	/// </param>
	void cleanLevel (Transform trans)
	{
		Transform transOff = trans.FindChild ("Panel-sprite").FindChild ("Offset");
		//重置关卡
        NowMode now = Globe.getPanelOfParent(trans, 1, "Panel - Main").FindChild("ButtonsRight").FindChild("BtnBotton").GetComponent<NowMode>();
        print(now+"<>"+transOff);
        now.levelOffsetGo = transOff.gameObject;
        now.itemCardLayer();

		for (int i = 0; i < transOff.GetChildCount(); i++) {
			Transform transf = transOff.GetChild (i);
			Destroy (transf.gameObject, 1.0f);
		}
        transOff.GetComponent<UIItemCard>().initLevel();
	}
	
	
}
