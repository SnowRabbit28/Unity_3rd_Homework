using UnityEngine;
using UnityEngine.InputSystem;

public interface IHit
{
    public void GetHit();
}

public class PlayerController : MonoBehaviour, IHit
{
    public float moveDistance = 1;
    private Vector3 curPos;
    private Vector3 moveValue;
    public float moveTime;
    public float colliderDistCheck;

    public ParticleSystem particle = null;
    public Transform chick = null;
    public bool isDead = false;
    private bool isOnLog = false;

    void Start()
    {
        moveValue = Vector3.zero;
        curPos = transform.position;
    }

    public void Move(InputAction.CallbackContext context)
    {
        Vector3 input = context.ReadValue<Vector3>();

        if (input.magnitude > 1f) return;

        if (context.performed)
        {
            if (input.magnitude == 0f)
            {
                Moving(transform.position + moveValue);
                Rotate(moveValue);
                moveValue = Vector3.zero;
            }
            else
            {
                moveValue = input * moveDistance;
            }
        }
    }

    void Moving(Vector3 pos)
    {
        LeanTween.move(this.gameObject, pos, moveTime).setOnComplete(() => { if (pos.z > curPos.z) SetMoveForwardState(); });
    }

    void Rotate(Vector3 pos)
    {   // 전에는 좌우만 가능했었다,  pos의 x값만 받았기 때문
        //chick.rotation = Quaternion.Euler(270, pos.x * 90, 0);

        // z축을 추가하여 앞뒤 구현, Atan2를 이용하여 벡터에 방향에 따라 y축 회전각도 계산 * 결과값이 라디안이라 각도로 변환
        float yRotation = Mathf.Atan2(pos.x, pos.z) * Mathf.Rad2Deg;
        chick.rotation = Quaternion.Euler(270, yRotation, 0);
    }

    void SetMoveForwardState()
    {
        Manager.instance.UpdateDistanceCount();
        curPos = transform.position;
    }

    public void GetHit()
    {
        Manager.instance.GameOver();
        isDead = true;
        ParticleSystem.EmissionModule em = particle.emission;
        em.enabled = true;
    }

    //void OnTriggerEnter(Collider other)
    //{
    //    // "Water" 태그를 가진 오브젝트와 충돌한 경우
    //    if (other.CompareTag("Water"))
    //    {
    //        if(Physics.Raycast(transform.position,Vector3.down,out RaycastHit hit, 1f))
    //        {
    //            if (hit.collider.CompareTag("Log"))
    //            {
    //                return;
    //            }
    //        }

    //        // 캐릭터가 물에 직접 닿은 경우에만 게임 오버 처리
    //        Destroy(gameObject);
    //        Manager.instance.GameOver();
    //    }
    //}

}
