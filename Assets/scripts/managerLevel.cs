using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class managerLevel : MonoBehaviour {

   public static bool threePayer = false;

   public void LoadLevel(string name) {
        SceneManager.LoadScene(name);
   }

	public void QuitRequest() {
       Application.Quit();
   }

   public void playerCount(int num) {
		if (num == 2) {
			threePayer = false;
        }
		if (num == 3) {
			threePayer = true;
        }
   }

}
