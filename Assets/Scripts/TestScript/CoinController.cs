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
        initialPosition = transform.position; //�ʱ���ġ ����
    }

    void Update()
    {
        // �̵� ó��
        //float horizontalInput = Input.GetAxis("Horizontal");
        //float verticalInput = Input.GetAxis("Vertical");
        //Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * moveSpeed * Time.deltaTime;
        //transform.Translate(movement);

        if (Input.GetKeyDown(KeyCode.R)) 
        {
            ResetPosition();
        }

        // ������ ���콺 ��ư ó��
        if (Input.GetMouseButtonDown(1))
        {
            clickStartTime = Time.time;
            isClicking = true;
        }

        if (isClicking && Input.GetMouseButtonUp(1))
        {
            isClicking = false;

            // ���콺�� ���� �ð��� ���� ƨ��� �� ���
            float clickDuration = Time.time - clickStartTime;

            // ���콺 �������� �������� ƨ��� ���� ����
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Vector3 bounceDirection = (hit.point - transform.position).normalized;
                float bounceForce = Mathf.Clamp(clickDuration, 0f, 1f) * moveSpeed; // �ִ밪�� moveSpeed�� ����
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
        transform.position = initialPosition; // �ʱ� ��ġ�� ������Ʈ ��ġ ����
    }
}
