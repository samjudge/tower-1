using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class ActionLog : MonoBehaviour {

    [SerializeField]
	private Scrollbar Scrollbar;
    [SerializeField]
    private GameObject TextObject;
    [SerializeField]
    private Text Text;
    [SerializeField]
    private bool HasTextBeenModified = false;

	void Start(){
		this.Text = TextObject.GetComponent<Text>() as Text;
		StartCoroutine(TrackNewLogEntries());
	}

	void Update(){}

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

    /**
     * WriteNewLine()
     * @param String NewLine 
     * Write a line to the action log
     */
    public void WriteNewLine(String NewLine){
        Debug.Log(this.name);
		this.Text.text = NewLine + "\n" + this.Text.text;
		this.HasTextBeenModified = true;
	}

}
