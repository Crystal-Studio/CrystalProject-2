using UnityEngine;
using System.Collections;

public class SetAnimations : MonoBehaviour {
	
	public GameObject[] characters;
	public string animationName;
	

	// Use this for initialization
	void Start () {
		foreach(GameObject currentCharacter in characters){
	currentCharacter.GetComponent<Animation>().Stop();
	currentCharacter.GetComponent<Animation>().Play (animationName);
		foreach (AnimationState state in currentCharacter.GetComponent<Animation>()) {
    state.speed = 0F;
			}
	}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
