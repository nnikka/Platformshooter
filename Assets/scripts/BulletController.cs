using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    public Sprite animatedBoolet;
    public AudioClip hitSound;
    public GameObject Shooter;

    private float yPoint;
    private float xPoint;

	// Use this for initialization
	void Start () {
	    yPoint = this.transform.position.y;
	    xPoint = this.transform.position.x;
		addVelocity();
	}

	void addVelocity() {
	    float dir = this.transform.eulerAngles.z;
	    float dirY = this.transform.eulerAngles.y;
	    this.GetComponent<SpriteRenderer>().sprite = animatedBoolet;
		if (Mathf.Approximately(dir, 0) && Mathf.Approximately(dirY, 0)) {
			this.GetComponent<Rigidbody2D>().velocity = new Vector2(4.25f, 0);
		} else if (Mathf.Approximately(dir, 45f) && Mathf.Approximately(dirY, 0)) {
			this.GetComponent<Rigidbody2D>().velocity = new Vector2(3f, 3f);
		} else if (Mathf.Approximately(dir, 90f)) {
			this.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 4.25f);
		} else if (Mathf.Approximately(dir, 270f)) {
			this.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -4.25f);
		} else if (Mathf.Approximately(dir, 315f) && Mathf.Approximately(dirY, 0)) {
			this.GetComponent<Rigidbody2D>().velocity = new Vector2(3f, -3f);
		} else if (Mathf.Approximately(dir, 0) && Mathf.Approximately(dirY, 180f)) {
			this.GetComponent<Rigidbody2D>().velocity = new Vector2(-4.25f, 0);
		} else if (Mathf.Approximately(dir, 45f) && Mathf.Approximately(dirY, 180f)) {
			this.GetComponent<Rigidbody2D>().velocity = new Vector2(-3f, 3f);
		} else if (Mathf.Approximately(dir, 315f) && Mathf.Approximately(dirY, 180f)) {
			this.GetComponent<Rigidbody2D>().velocity = new Vector2(-3f, -3f);
		}
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.tag == "Warrior") {
			AudioSource.PlayClipAtPoint(hitSound, this.transform.position);
			float currXPos = this.transform.position.x;
	        float currYPos = this.transform.position.y;
			float distance = Mathf.Sqrt(Mathf.Pow(Mathf.Abs(currXPos - xPoint), 2) + Mathf.Pow(Mathf.Abs(currYPos - yPoint), 2));
			if (distance <= 1) {
				collision.gameObject.GetComponent<PersonCont>().HP -= 100f;
			} else if (distance <= 3) {
				collision.gameObject.GetComponent<PersonCont>().HP -= 50f;
			} else {
				collision.gameObject.GetComponent<PersonCont>().HP -= 25f;
			}
			if (collision.gameObject.GetComponent<PersonCont>().HP <= 0) {
				Shooter.GetComponent<PersonCont>().Point += 1;
		    }
		} else if (collision.gameObject.tag == "Border") {
		   
	    }
		Destroy(this.gameObject, 0f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
