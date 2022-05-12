// using System.Diagnostics;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIFollowTarget : MonoBehaviour 
{
	RectTransform rectTransform;
	[SerializeField] Transform target;
	[SerializeField] Camera camera;

	void Awake()
	{
		rectTransform = GetComponent<RectTransform> ();

	}

	void LateUpdate()
	{
		var targetScreenPos = camera.WorldToScreenPoint (target.position);
		rectTransform.position = targetScreenPos;
	}
}
