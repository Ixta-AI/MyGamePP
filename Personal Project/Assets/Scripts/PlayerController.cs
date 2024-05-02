using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Transform playerTransform;

    private float speed = 10;

    public int maxHealth = 5;
    public int currentHealth;

    public HealthBar healthBar;

    void Start()
    {
        playerTransform = transform.GetChild(0);
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {// storing movement values
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Fire1")) //Finding Mouse position by left click(Shooting)
        {
            Vector3 mousePos = Input.mousePosition;
            {
                Debug.Log(mousePos.x);
                Debug.Log(mousePos.y);
            }
        }
        if (currentHealth < 1) //Checking to see if player has died
        {
            Debug.Log("You are dead");
        }
        
        // calling a work function and passing the user input information and speed value
        LocalMove(horizontal, vertical, speed);
       
        // calling a movement limiter  
        playerCamLimit();

        /*PlayerMouseAim();*/

        HorizontalLean(playerTransform, horizontal, 70, .1f);//Calling lean method
    }
 
    void PlayerMouseAim() 
    {
        //Get UI elem and map it to mouse movement
        //have player Y-axis rotation follow UI elem (with a limit similar to HorizontalLean)
        //(Transform target, float y axis, float lookLimit, float lerpTime)
    }
    
    // movement calculation (X-axis, Y-axis, speed)
    void LocalMove(float x, float y, float speed)
    {
        transform.localPosition += new Vector3(x, y, 0) * speed * Time.deltaTime;
    }

    void HorizontalLean(Transform target, float axis, float leanLimit, float lerpTime)//Lean calculation
    {
        Vector3 targetEulerAngles = target.localEulerAngles; //Storing Targets current rotation in 3D Space
        target.localEulerAngles = new Vector3(targetEulerAngles.x, targetEulerAngles.y, Mathf.LerpAngle(targetEulerAngles.z, -axis * leanLimit, lerpTime));
    }

   // locking player into camera boundaries
    void playerCamLimit()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }

    public void TakeDamage(int damage) //Damage function
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }
    private void OnCollisionEnter(Collision collision)//Enemy collision into player event detection
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(1);//calling damage function for collision
            Debug.Log("Enemy crashes into Player");
        }
    }
    private void OnTriggerEnter(Collider other)//Powerup event collision detection
    {
        if (other.gameObject.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
        }
    }
}
//Player should move Up/Down/Left/Right
//Player will be "on rails" Meaning a predetermined path and unaffected by gravity
//player aim with mouse (implement rotation general to mouse cross hair)