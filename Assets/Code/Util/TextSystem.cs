using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextSystem : MonoBehaviour {

	public string[] lines;
	public bool[] takeBreak;
	public bool[] clear;
	public float textSpeed;
	public float fastTextMultiplier = 10;
	public Text textbox;
	public AudioSource sound;
	public float minPitch, maxPitch;
	public int textboxHeight;
	public bool finished;

	float timer;
	int line, letter, cline = 0;
	bool lineFull,progLine;
	string currentText;

	void Start () {
		timer = textSpeed;
		Time.timeScale = 0;
	}

	void OnEnable(){
		timer = textSpeed;
		Time.timeScale = 0;
		textbox.text = "";
		line = 0;
		letter = 0;
		lineFull = false;
		progLine = false;
		currentText = "";
		finished = false;
	}

	void Update () {
		if (progLine && !Input.anyKey) {
			progLine = false;
		}
		if (lineFull) {
			if (!takeBreak[line] || Input.anyKeyDown) {
				if (line >= lines.Length - 1) {
					finished = true;
				} else {
					sound.pitch = minPitch;
					sound.Play ();
					if (takeBreak [line]){
						progLine = true;
					}
					lineFull = false;
					line++;
					if (clear [line]) {
						cline = 0;
						currentText = "";
					} else {
						cline++;
					}
					currentText += "\n";
					letter = 0;
				}
			}
		} else {
			if (timer > 0) {
				if (Input.anyKey && !progLine) {
					timer -= Time.unscaledDeltaTime * fastTextMultiplier;
				} else {
					timer -= Time.unscaledDeltaTime;
				}
			} else if (Input.anyKey && !progLine) {
				currentText += lines [line][letter];
				letter++;
				timer = textSpeed;
				if (!sound.isPlaying) {
					sound.pitch = maxPitch;
					sound.Play ();
				}
				if (letter >= lines [line].Length) {
					lineFull = true;
				}
			} else {
				currentText += lines [line][letter];
				letter++;
				timer = textSpeed;
				if (!sound.isPlaying) {
					sound.pitch = Random.Range (minPitch, maxPitch);
					sound.Play ();
				}
				if (letter >= lines [line].Length) {
					lineFull = true;
				}
			}
		}
		if (cline < textboxHeight) {
			string toAdd = "";
			for (int i = 0; i < textboxHeight - cline; i++) {
				toAdd += "\n";
			}
			textbox.text = currentText + toAdd;
		} else {
			textbox.text = currentText;
		}
	}
}
