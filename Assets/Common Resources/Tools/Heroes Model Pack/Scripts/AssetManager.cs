using UnityEngine;
using System.Collections;

public class AssetManager : MonoBehaviour {
	
	public GameObject[] assets;
	
	public GUIText assetListTitle;
	public GUIText animationListTitle;
	
	public GameObject buttonTemplate;
	
	public string assetListName = "Assets";
	public string animationListName = "";
			
	GameObject currentAsset;
	
	public GameObject editorPrompt;
	
			
	string currentAnimation = "Idle";
	
	float assetButtonSpacing;
	
	
	// Use this for initialization
	void Start () {
		
		assetButtonSpacing = 0.9f/(assets.Length+1);
		
		//Switch to the default asset
		ChangeAsset (0);
		editorPrompt.SetActive(false);
		
		//Set asset list titles
		assetListTitle.text = assetListName;
		animationListTitle.text = animationListName;
		
		//Create a button for each asset in the list
		GenerateAssetButtons();
		
		

	}
	
	// Update is called once per frame
	void Update () {
	}
	
	
	//Generates asset buttons
	public void GenerateAssetButtons(){
		
		for( int i=0; i<assets.Length; i++)
		{
			GameObject newButton = Instantiate (buttonTemplate) as GameObject;
			newButton.transform.position = new Vector3(0.05f, 0.9f, 0f);
			newButton.GetComponent<GUIText>().pixelOffset = new Vector2(0, -(30f*i));
			newButton.GetComponent<GUIText>().fontSize = 16;
			newButton.GetComponent<Button>().SetData(assets[i].GetComponent<Asset>().assetName, i.ToString(), this);
			newButton.name = "Asset Button "+i;
		}
		
		GameObject.Find("Asset Button 0").GetComponent<Button>().Activate();;
	}
	
	
	//Generates animation buttons
	public void GenerateAnimationButtons(){
		
		Button[] oldAnimationButtons = GameObject.FindSceneObjectsOfType(typeof(Button)) as Button[];
		foreach (Button animationButton in oldAnimationButtons)
		{
			if(animationButton.name.Contains("Animation Button"))
			Destroy (animationButton.gameObject);
		}
		
		
		//Precalculates length
		int animationCount = 0;
		foreach (AnimationState state in currentAsset.GetComponent<Animation>()) {
    	animationCount++;
		}
		
		float animationButtonSpacing = 1f/(animationCount);
		
		int currentAnimationID = 0;
		
		//Goes through each animation
		foreach (AnimationState state in currentAsset.GetComponent<Animation>()) {
			
    		GameObject newButton = Instantiate (buttonTemplate) as GameObject;
			//newButton.transform.position = new Vector3(0.95f, animationButtonSpacing*(animationCount-currentAnimationID), 0f);
			newButton.transform.position = new Vector3(0.95f, 0.9f, 0f);
			newButton.GetComponent<GUIText>().pixelOffset = new Vector2(0, -(15f*currentAnimationID));
			newButton.GetComponent<Button>().SetData(state.name, state.name, this);
			newButton.GetComponent<GUIText>().anchor = TextAnchor.UpperRight;
			newButton.GetComponent<GUIText>().alignment = TextAlignment.Right;
			newButton.name = "Animation Button "+state.name;
			currentAnimationID++;
			
			if(state.name == currentAnimation)
				newButton.GetComponent<Button>().Activate();
		}
		
		//GameObject.Find("Animation Button "+currentAnimation).GetComponent<Button>().Activate();

	}
	
	//Changes current asset
	public void ChangeAsset(int assetID){
		if(currentAsset != null)
		{
			Destroy (currentAsset.gameObject);
		}
			
		currentAsset = Instantiate(assets[assetID], new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
		ChangeAnimation(currentAnimation);
		
		GenerateAnimationButtons ();
	}
	
	
		
		
	public void ChangeAnimation(string animationName){
		currentAsset.GetComponent<Animation>().Stop();
		currentAsset.GetComponent<Animation>().Play (animationName);
		//foreach (AnimationState state in currentAsset.GetComponent<Animation>()) {
    	//state.speed = 0.7F;
		//}
		
		
		
	}
	
	public void SetCurrentAnimation(string animationName){
	currentAnimation = animationName;
	}
}
