using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {
	
	public AssetManager assetManager;
	public Font activeFont;
	public Font inactiveFont;
	public Font highlightFont;
	
	public string inputData = "0";
	int outputData;
	
	public Button[] buttons;
	
	bool isActive = false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseEnter(){
	GetComponent<GUIText>().font = highlightFont;
	}
	void OnMouseExit(){
		if(!isActive){
			GetComponent<GUIText>().font = inactiveFont;
		}
		else{
			GetComponent<GUIText>().font = activeFont;
		}
	}
	
	
	void OnMouseDown(){
		
		
		//If currently not active, changes asset/animation
		if(!isActive){
			

			//If convertible to an int, it's a asset
			if (int.TryParse(inputData, out outputData)){
				buttons = GameObject.FindObjectsOfType(typeof(Button)) as Button[];
				//Disable all other asset buttons
				for(int i=0; i<buttons.Length; i++){
					if(int.TryParse(buttons[i].inputData, out buttons[i].outputData)){
						buttons[i].Deactivate();
					}
				}
				
				//Change asset
				assetManager.ChangeAsset(outputData);
			}
			//Otherwise, it's an animation
			else{
				buttons = GameObject.FindObjectsOfType(typeof(Button)) as Button[];
				//Disable all other animation buttons
				for(int i=0; i<buttons.Length; i++){
					if(int.TryParse(buttons[i].inputData, out outputData) == false){
						buttons[i].Deactivate();
					}
				}
				
				//Change animation
				assetManager.ChangeAnimation(inputData);
				
				//Change current animation (saves when switching between assets)
				assetManager.SetCurrentAnimation(inputData);
			}
			
			Activate();
		}
		else{
			//Deactivate();
		}
	}
	
	public void Deactivate(){
		isActive = false;
	GetComponent<GUIText>().font = inactiveFont;
	}
	
	public void Activate(){
		isActive = true;
	GetComponent<GUIText>().font = activeFont;
	}
	
	public void SetData(string buttonTitle, string newInputData, AssetManager newAssetManager){
		GetComponent<GUIText>().text = buttonTitle;
		inputData = newInputData;
		assetManager = newAssetManager;
	}
	
}
