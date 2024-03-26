using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    public float moveSpeed = 4f;
    private Rigidbody rigidBody;
    public float addPowerRingforce = 15f;

    private float clickStartTime;
    private bool isClicking = false;

    private Vector3 initialPosition;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        initialPosition = transform.position; //초기위치 저장
    }

    void Update()
    {
        // 이동 처리
        //float horizontalInput = Input.GetAxis("Horizontal");
        //float verticalInput = Input.GetAxis("Vertical");
        //Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * moveSpeed * Time.deltaTime;
        //transform.Translate(movement);

        if (Input.GetKeyDown(KeyCode.R)) 
        {
            ResetPosition();
        }

        // 오른쪽 마우스 버튼 처리
        if (Input.GetMouseButtonDown(1))
        {
            clickStartTime = Time.time;
            isClicking = true;
        }

        if (isClicking && Input.GetMouseButtonUp(1))
        {
            isClicking = false;

            // 마우스를 누른 시간에 따라 튕기는 힘 계산
            float clickDuration = Time.time - clickStartTime;

            // 마우스 포인터의 방향으로 튕기는 힘을 가함
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Vector3 bounceDirection = (hit.point - transform.position).normalized;
                float bounceForce = Mathf.Clamp(clickDuration, 0f, 1f) * moveSpeed; // 최대값을 moveSpeed로 제한
                rigidBody.AddForce(bounceDirection * bounceForce, ForceMode.VelocityChange);
            }
        }
    }

    void OnTriggerEnter(Collider ring)
    {
        if (ring.CompareTag("PowerRing"))
        {
            Vector3 direction = ring.transform.position - transform.position;
            rigidBody.AddForce(direction.normalized * addPowerRingforce, ForceMode.Impulse);
        }
    }

    void ResetPosition()
    {
        transform.position = initialPosition; // 초기 위치로 오브젝트 위치 변경
    }
}
