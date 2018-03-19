using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour {

   public GameObject PauseUI;
   private bool paused = false;

   void Start() {
      PauseUI.SetActive(false);
   }

   void Update() {
      if (Input.GetKeyDown(KeyCode.Space)) {
          paused = !paused;
      }
	  if (paused) {
		  PauseUI.SetActive(true);
		  Time.timeScale = 0;
      }
	  if (!paused) {
		  PauseUI.SetActive(false);
		  Time.timeScale = 1;
      }
   }

   public void Resume() {
	  paused = false;
   }

	public void Restart() {
	  Application.LoadLevel(Application.loadedLevel);
   }

}
