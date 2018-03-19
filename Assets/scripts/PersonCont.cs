using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class PersonCont : MonoBehaviour {

    public float HP = 100f;
    public int Point = 0;
    public Transform[] spawnPoints;
	public Transform upPoint;

	private Animator anim;
    private string jumpAnimationTrigger = "jump";
	private string shootingAnimationTrigger = "shooting";
	private string runAnimationTrigger = "run";
	private string walkAnimationTrigger = "walk";
	private string stopAnimationTrigger = "stop";
	private string stopRunningAnimationTrigger = "stopRunning";
	private string stopJumpingAnimationTrigger = "stopJumping";
	private string upAnimationTrigger = "up";
	private string stopUpAnimationTrigger = "stopUp";
	private string downAnimationTrigger = "down";
	private string stopDownAnimationTrigger = "stopDown";
	private string diagonalyUpAnimationTrigger = "diagonalUp";
	private string stopDiagonalyUpAnimationTrigger = "stopDiagonalUp";
	private string diagonalDownAnimationTrigger = "diagonalDown";
	private string stopDiagonalDownAnimationTrigger = "stopDiagonalDown";

	public Sprite diagonalUp;
	public Sprite diagonalDown;
	public Sprite Up;
	public Sprite Down;

	public bool facingLeft;
	public bool facingRight;
	public bool clickLeft = false;
	public bool clickRight = false;
	public bool clickUp = false;
	public bool clickDown = false;
    public bool isRuning = false;
    public bool isWalking = false;
    public bool isShooting = false;
    public bool isJumping = false;

    private Vector3 currPos;
    private float walkVelocity = 0.03f;
    private float runVelocity = 0.05f;
    private bool grounded = true;

    private Transform bulletPoint;
	public GameObject bullet;
	public AudioClip jumpSound;

    private float lastshoot;
   

	// Use this for initialization
	void Start () {
		anim = this.GetComponent<Animator>();
		bulletPoint = this.transform.Find("lazerForward").transform;
		lastshoot = Time.time;
	}


	
	// Update is called once per frame
	void Update () {
	   if (facingLeft) {
		   transform.rotation = Quaternion.Euler(0, 180f, 0);
	   } else {
			transform.rotation = Quaternion.Euler(0, 0, 0);
	   }
	   this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, this.GetComponent<Rigidbody2D>().velocity.y);
	    if (clickLeft || clickRight) {
	        isWalking = true;
	    } else {
	        isWalking = false;
	    }
		handleAnimation();
	}

	void FixedUpdate() {
		ifDead();
		bulletPoint = this.transform.Find("lazerForward").transform;
		if (HP > 1) {
			if (clickLeft) {
				if (facingRight) {
			       this.transform.Rotate(Vector3.up, 180f);
			       facingRight = false;
			       facingLeft = true;
			    }
		    }
		    if (clickRight) {
				if (facingLeft) {
					this.transform.Rotate(Vector3.up, 180f);
					facingRight = true;
					facingLeft = false;
			    }
		    }
			if ((clickRight && !clickUp && !clickDown && grounded) || (clickRight && !grounded)) {
				goRight();
			}
			if ((clickLeft && !clickUp && !clickDown && grounded) || (clickLeft && !grounded)) {
			    goLeft();
			}
			if (clickUp && (!clickRight && !clickLeft)) {
				handleUp();
			} else if (clickDown && (!clickRight && !clickLeft)) {
				handleDown();
			} else if (clickUp && (clickLeft || clickRight)) {
			    handlediagonalUp();
			} else if (clickDown && (clickLeft || clickRight)) {
			    handlediagonalDown();
			} else {
			    enableAnimation();
			}
			if (isJumping) {
				jump();
			}
			if (isShooting) {
			    shoot();
			}
		}
	}


	void handleAnimation() {
		if (!isWalking) {
			anim.SetTrigger(stopAnimationTrigger);
			anim.SetTrigger(stopRunningAnimationTrigger);
		} else if (!isRuning && isWalking) {
			anim.SetTrigger(stopRunningAnimationTrigger);
			anim.SetTrigger(walkAnimationTrigger);
		} else if (isRuning && isWalking) {
			anim.SetTrigger(stopAnimationTrigger);
			anim.SetTrigger(runAnimationTrigger);
		} else if (!isRuning) {
			anim.SetTrigger(stopRunningAnimationTrigger);
		}
	}

	public void goLeft() {
		currPos = this.transform.position;
		if (isRuning) {
			currPos.x -= runVelocity;
		    this.transform.position = currPos;
		} else {
			currPos.x -= walkVelocity;
		    this.transform.position = currPos;
		}
	}

	public void goRight() {
		currPos = this.transform.position;
		if (isRuning) {
			currPos.x += runVelocity;
		    this.transform.position = currPos;
		} else {
			currPos.x += walkVelocity;
		    this.transform.position = currPos;
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (!grounded && coll.gameObject.tag == "Ground") {
			grounded = true;
		    anim.SetTrigger(stopJumpingAnimationTrigger);
	    }
    }

    void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "StimPack") {
		   HP = 100f;
		   Destroy(coll.gameObject);
        }
		if (coll.gameObject.tag == "LoosingLine") {
			Debug.Log(HP);
		   if (HP <= 1) {
		        HP = 100f;
				this.GetComponent<BoxCollider2D>().isTrigger = false;
			    this.GetComponent<Animator>().enabled = true;
				int posNum = Random.Range(0, spawnPoints.Length);
		        this.transform.position = spawnPoints[posNum].transform.position;
		   } else {
				Debug.Log(HP);
				this.transform.position = upPoint.transform.position;
		   }
        } 
    }

	void jump() {
		if (grounded) {
			AudioSource.PlayClipAtPoint(jumpSound, this.transform.position);
		    grounded = false;
			this.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 0.8f), ForceMode2D.Impulse);
			anim.SetTrigger(jumpAnimationTrigger);
		}
	}

	void shoot() {
	    if (Time.time - lastshoot > 1f) {
		   GameObject shootingBollet = Instantiate(bullet, bulletPoint.position, bulletPoint.rotation);
		   anim.SetTrigger(shootingAnimationTrigger);
		   shootingBollet.GetComponent<BulletController>().Shooter = this.gameObject;
		   lastshoot = Time.time;
		}
	}

	void handleUp() {
		bulletPoint = this.transform.Find("lazerUp").transform;
		GetComponent<Animator>().enabled = false;
		GetComponent<SpriteRenderer>().sprite = Up;
	}
	void handleDown() {
		bulletPoint = this.transform.Find("lazerDown").transform;
		GetComponent<Animator>().enabled = false;
		GetComponent<SpriteRenderer>().sprite = Down;
	}

	void handlediagonalUp() {
		bulletPoint = this.transform.Find("lazerDiagonalUp").transform;
		GetComponent<Animator>().enabled = false;
		GetComponent<SpriteRenderer>().sprite = diagonalUp;
	}

	void handlediagonalDown() {
		bulletPoint = this.transform.Find("lazerDiagonalDown").transform;
		GetComponent<Animator>().enabled = false;
		GetComponent<SpriteRenderer>().sprite = diagonalDown;
	}

	void enableAnimation() {
	    GetComponent<Animator>().enabled = true;
	}

	void ifDead() {
	    if (HP <= 0) {
	        this.GetComponent<BoxCollider2D>().isTrigger = true;
			this.GetComponent<Animator>().enabled = false;
		    this.GetComponent<SpriteRenderer>().sprite = Down;
		    HP = 1f;
	    }
	}

}
