using UnityEngine;
using System.Collections;

public class followCamera1 : MonoBehaviour {

    [SerializeField]    // privateなメンバもインスペクタで編集したいときに付ける
    private GameObject focusObj = null; // 注視点となるオブジェクト

    private Vector3 oldPos; // マウスの位置を保存する変数
    private Vector3 fixRotate = new Vector3(0.0f,3.0f,0.0f); // カメラ回転後、Y軸が変わってしまうのを修正する為の変数

    enum MouseButtonDown
    {
        MBD_LEFT = 0,
        MBD_RIGHT,
        MBD_MIDDLE,
    };

    //プレイヤートランスフォームへの参照
    Transform playerTransform;
    //プレイヤーからの距離
    Vector3 offset;
    // Use this for initialization
    void Start()
    {
        // PlayerタグがつけられたゲームオブジェクトのTransformを取得
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        // プレイヤーとカメラの距離の差をoffsetに代入
        offset = transform.position - playerTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // 右クリック＋ドラッグで自由視点変更
        if (Input.GetMouseButton(1))
        {
            mouseEvent();
        }
        else // 右クリックが押されていない場合は常にプレイヤーから一定間の距離を保ったままカメラ追従
        {
            transform.rotation = Quaternion.Euler(new Vector3(11.5f, 0.0f, 0.0f));
            transform.position = new Vector3(
            playerTransform.position.x + offset.x,
            fixRotate.y,
            playerTransform.position.z + offset.z
            );
        }

        
    }
    void mouseEvent()
    {
        // マウスホイールの回転量を取得
        float delta = Input.GetAxis("Mouse ScrollWheel");
        // 回転量が0でなければホイールイベントを処理
        if (delta != 0.0f)
            this.mouseWheelEvent(delta);

        // マウスボタンが押されたタイミングで、マウスの位置を保存する
        if (Input.GetMouseButtonDown((int)MouseButtonDown.MBD_LEFT) ||
        Input.GetMouseButtonDown((int)MouseButtonDown.MBD_MIDDLE) ||
        Input.GetMouseButtonDown((int)MouseButtonDown.MBD_RIGHT))
            oldPos = Input.mousePosition;

        // マウスドラッグイベント
            mouseDragEvent(Input.mousePosition);

        return;
    }
    void mouseWheelEvent(float delta)
    {
        // 注視点からカメラまでのベクトルを計算
        Vector3 focusToPosition = transform.position - playerTransform.transform.position;

        // ホイールの回転量を元に上で求めたベクトルの長さを変える
        Vector3 post = focusToPosition * (1.0f + delta);

        // 長さを変えたベクトルの長さが一定以上あれば、カメラの位置を変更する
        if (post.magnitude > 0.01f)
            transform.position = playerTransform.transform.position + post;

        return;
    }
    void mouseDragEvent(Vector3 mousePos)
    {
        // マウスの現在の位置と過去の位置から差分を求める
        Vector3 diff = mousePos - oldPos;

        // 差分の長さが極小数より小さかったら、ドラッグしていないと判断する
        if (diff.magnitude < Vector3.kEpsilon)
            return;

        if (Input.GetMouseButton((int)MouseButtonDown.MBD_LEFT))
        {
            // マウス左ボタンをドラッグした場合(なにもしない)
        }
        else if (Input.GetMouseButton((int)MouseButtonDown.MBD_MIDDLE))
        {
            // マウス中ボタンをドラッグした場合(注視点の移動)
            this.cameraTranslate(-diff / 10.0f);

        }
        else if (Input.GetMouseButton((int)MouseButtonDown.MBD_RIGHT))
        {
            // マウス右ボタンをドラッグした場合(カメラの回転)
            this.cameraRotate(new Vector3(diff.y, diff.x, 0.0f));
        }

        // 現在のマウス位置を、次回のために保存する
        this.oldPos = mousePos;

        return;
    }
    void cameraTranslate(Vector3 vec)
    {
        Transform focusTrans = this.playerTransform.transform;
        Transform trans = this.transform;

        //カメラのローカル座標軸を元に注視点オブジェクトを移動する
        focusTrans.Translate((trans.right * -vec.x) + (trans.up * vec.y));

        return;
    }
    public void cameraRotate(Vector3 eulerAngle)
    {
        Vector3 focusPos = playerTransform.transform.position;
        Transform trans = transform;

        // 回転前のカメラの情報を保存する
        Vector3 preUpV, preAngle, prePos;
        preUpV = trans.up;
        preAngle = trans.localEulerAngles;
        prePos = trans.position;

        // カメラの回転
        // 横方向の回転はグローバル座標系のY軸で回転する
        trans.RotateAround(focusPos, Vector3.up, eulerAngle.y);
        // 縦方向の回転はカメラのローカル座標系のX軸で回転する
        trans.RotateAround(focusPos, trans.right, -eulerAngle.x);

        // カメラを注視点に向ける
        trans.LookAt(focusPos);

        // ジンバルロック対策
        // カメラが真上や真下を向くとジンバルロックがおきる
        // ジンバルロックがおきるとカメラがぐるぐる回ってしまうので、一度に90度以上回るような計算結果になった場合は回転しないようにする(計算を元に戻す)
        Vector3 up = trans.up;
        if (Vector3.Angle(preUpV, up) > 90.0f)
        {
            trans.localEulerAngles = preAngle;
            trans.position = prePos;
        }

        return;
    }
}
