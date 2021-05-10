using UnityEngine;
using System.Collections;
using System;
using System.Diagnostics;


public class PlayerController : MonoBehaviour
{
    public delegate void PlayerSurvivalTimeEvent(float time);
    public event PlayerSurvivalTimeEvent PlayerSurvival;

    private float _aliveTime;

    public float Speed;
    private float _fireRate = .5f;
    private float _nextFireTime = 0.0F;

    public GameObject Bullet;

    private bool _bulletFired = true;

    public Animator CharacterAnimator;

    void Start()
    {
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (Globals.CurGameState == GameState.PlayingGame)
        {
            if (collider.gameObject.tag == "Ghost")
            {
                Globals.Health--;

                Destroy(collider.gameObject);

                //if (Globals.Health <= 0)
                //    PlayerSurvival(_aliveTime);
            }
        }
    }

    void Update()
    {
        if (Globals.CurGameState == GameState.PlayingGame)
        {
            _aliveTime += Time.deltaTime;

            Vector3 vel = new Vector3();

            vel.x = Input.GetAxis("Horizontal");
            vel.y = Input.GetAxis("Vertical");

            Vector3.Normalize(vel);
            vel *= Speed;


            if (vel != Vector3.zero && !CharacterAnimator.GetBool("IsWalking"))
            {
                UnityEngine.Debug.Log("Setting IsWalking to true");
                CharacterAnimator.SetBool("IsWalking", true);
            }
            else if (CharacterAnimator.GetBool("IsWalking") && vel == Vector3.zero)
            {
                UnityEngine.Debug.Log("Setting IsWalking to false");
                CharacterAnimator.SetBool("IsWalking", false);
            }

            float x, y;


            GetComponent<Rigidbody2D>().velocity = vel;


            x = Mathf.Clamp(transform.position.x, -4.5f, 4.5f);
            y = Mathf.Clamp(transform.position.y, -3.0f, 3.0f);

            transform.position = new Vector3(x, y, 0);

            Vector2 offset;

            Vector3 mouse = Input.mousePosition;
            Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);
            float angle=0.0f;


            //rotation
            if (Input.GetJoystickNames().Length > 0)
            {
                offset = new Vector2(Input.GetAxis("4"), Input.GetAxis("5"));
                angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg - 90.0f;
            }
            else
            {
                offset = new Vector2(mouse.x - screenPoint.x, mouse.y - screenPoint.y);
                angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg - 90.0f;
            }

            transform.rotation = Quaternion.Euler(0, 0, angle);

            if (_bulletFired)
            {
                _nextFireTime += Time.deltaTime;

                if (_nextFireTime >= _fireRate)
                    _bulletFired = false;
            }

            if (Input.GetButton("Fire1") && !_bulletFired)
            {
                //spawn bullet
                Globals.BulletAudioSource.Play();
                GameObject bullet = (GameObject)Instantiate(Bullet, transform.position, transform.rotation);
                _nextFireTime -= _fireRate;
                _bulletFired = true;
            }
        }
    }
}

