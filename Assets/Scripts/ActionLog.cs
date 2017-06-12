using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class ActionLog : MonoBehaviour {

	public Scrollbar Scrollbar;
	public GameObject TextObject;

	private Text Text;
	private bool HasTextBeenModified = false;

	public void Start(){
		this.Text = TextObject.GetComponent<Text>() as Text;
		StartCoroutine(TrackNewLogEntries());
	}

	public void Update(){}

	private IEnumerator TrackNewLogEntries(){
		while(true){
			if(this.HasTextBeenModified){
				if(this.Scrollbar.value == 1f){
					HasTextBeenModified = false;
				} else {
					this.Scrollbar.value = 1f;
				}
			}
			yield return null;
		}
	}

	public void WriteNewLine(String NewLine){
		this.Text.text = NewLine + "\n" + this.Text.text;
		this.HasTextBeenModified = true;
	}

}
