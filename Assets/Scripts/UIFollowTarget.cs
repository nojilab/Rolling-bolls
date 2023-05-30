// using System.Diagnostics;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class UIFollowTarget : MonoBehaviour 
{
	private RectTransform rectTransform;
	private TextMeshProUGUI playerName;
	public GameObject target;
	[SerializeField] public Camera camera;

	void Awake()
	{
		if(target == null){
            target = GameObject.Find("Stage");                               //ターゲットが存在しな時はステージに追従
        }

		rectTransform = GetComponent<RectTransform>();
		playerName = GetComponent<TextMeshProUGUI>();
		// UnityEngine.Debug.Log("UI_Awake:" + target.name);
		playerName.text = target.name;
	}

	void LateUpdate()
	{
		if(target == null){
            target = GameObject.Find("Stage");                               //ターゲットが存在しな時はステージに追従
        }
		playerName.text = target.name;
		// UnityEngine.Debug.Log("UI_Update:" + target.name);

		var targetScreenPos = camera.WorldToScreenPoint (target.transform.position);

		rectTransform.position = targetScreenPos;
	}
}
