using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour {

	//Animator of Text
	[SerializeField] Animator anim;
	//Text component
	Text damageText;

	// Use this for initialization
	void OnEnable () {
		//Destroy Popup text after end animation
		AnimatorClipInfo [] clipInfo = anim.GetCurrentAnimatorClipInfo (0);
		Destroy(gameObject, clipInfo[0].clip.length);

		//Get reference for Text
		damageText = anim.GetComponent<Text> ();
	}

	//Set text with string and color
	public void SetText(string text, Color c, int s){
		damageText.text = text;
		damageText.color = c;
		damageText.fontSize = s;
	}



}
