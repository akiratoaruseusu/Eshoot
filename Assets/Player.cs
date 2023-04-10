using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform[] RoutePoints;

    [Range(0, 200)]
    public float Speed = 10f;

    bool _isHitRoutePoint;

    IEnumerator Move()
    {
        var prevPointPos = transform.position;

        foreach (var nextPoint in RoutePoints)
        {
            _isHitRoutePoint = false; //�K��false�ɂ���

            while (!_isHitRoutePoint)
            {

                //�i�s�����̌v�Z
                var vec = nextPoint.position - prevPointPos;
                vec.Normalize();

                // �v���C���[�̈ړ�
                transform.position += vec * Speed * Time.deltaTime;
                //���̏����͐i�s�����������悤�Ɍv�Z���Ă���
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(vec, Vector3.up), 0.5f);

                yield return null;
            }

            prevPointPos = nextPoint.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "RoutePoint")
        {
            other.gameObject.SetActive(false);
            _isHitRoutePoint = true;
        }
    }

    void Start()
    {
        StartCoroutine(Move());
    }
}