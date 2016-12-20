using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMove : MonoBehaviour
{

    public CharacterController cc; // キャラクターコントローラーの設定
    public Animator animator; // アニメーターの設定
    public GameObject bullet; // 武器のゲームオブジェクト
    bool isDamage = false; // ダメージを貰ったかどうか

    float speed = 5.0f; // 走るスピード
    float jump = 4.5f; // ジャンプの高さ
    float gravity = 20.0f; // 重力の設定
    float interval = 3f; // ダメージを貰った際に動けなくなるインターバル時間
    public static int keyCount;

    Vector3 moveDirection = new Vector3(0.0f, 0.0f, 0.0f);
    public static Vector3 Direction = new Vector3(0.0f, 0.0f, 0.0f);

    // Use this for initialization
    void Start()
    {
        // キャラクターコンポーネントへの参照
        cc = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        // 常に重力がかかっている状態に
        moveDirection.y -= gravity * Time.deltaTime;
        Direction.y = moveDirection.x - moveDirection.z;
        Direction.z = moveDirection.z;
        Direction.x = moveDirection.x;
        if (Input.GetMouseButton(0))
        {
            //　左クリックで攻撃
            animator.SetBool("Attack", true);
        }
        if (cc.isGrounded) // 地面に接していたら
        {
            // 移動処理を実行
            moveDirection.x = Input.GetAxis("Horizontal") * speed;
            moveDirection.z = Input.GetAxis("Vertical") * speed;

            // キャラクターが動いているとき
            if (moveDirection.x != 0 || moveDirection.z != 0)
            {
                animator.SetBool("run", true); // 走るアニメーション再生

                // 常にキー入力の方向を向くように
                transform.LookAt(transform.position + new Vector3(moveDirection.x, 0, moveDirection.z));
            }
            else
            {
                animator.SetBool("run", false);
            }
            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jump;
                animator.SetBool("isjumping", true);
            }
            else
            {
                animator.SetBool("isjumping", false);
            }
        }

        if (Input.GetButtonDown("Fire1")==true)
        {
            Instantiate(bullet);
        }

        if (isDamage == true)
        {

            interval -= Time.deltaTime;
            if (interval == 0)
            {
                isDamage = false;
            }
        }

        cc.Move(moveDirection * Time.deltaTime);
    }

    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.tag == "Enemy")
        {
            animator.SetTrigger("isdamage");
            Debug.Log("Damage!");
            Destroy(hit.gameObject);
            isDamage = true;
        }
        if (hit.gameObject.tag == "Key")
        {
            Debug.Log("KeyGet!");
            Destroy(hit.gameObject);
            keyCount++;
        }
        if (hit.gameObject.tag == "Goal")
        {
            Debug.Log("CLEAR!!");
            Destroy(hit.gameObject);
        }
    }
}




