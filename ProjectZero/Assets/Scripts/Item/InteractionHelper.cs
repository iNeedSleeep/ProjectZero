using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionHelper : MonoBehaviour
{
    public KeyCode interactKey;  // ������
    public float interactRange;  // ������Χ

    private Interactor nearestInteractor;  // �����Interactor���
    private GameObject focusedObject;  // ��ǰ�۽�������

    void Update()
    {
        // ���½�����
        if (Input.GetKeyDown(interactKey))
        {
            // ��ȡ���о���С�ڽ�����Χ������
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, interactRange);

            // �����������壬�ҵ������Interactor���
            float nearestDistance = Mathf.Infinity;
            foreach (Collider2D collider in colliders)
            {
                float distance = Vector2.Distance(transform.position, collider.transform.position);
                Interactor interactor = collider.GetComponent<Interactor>();
                if (interactor != null && distance < nearestDistance)
                {
                    nearestInteractor = interactor;
                    nearestDistance = distance;
                }
            }

            // ���������������Interactor������������Interact����
            if (nearestInteractor != null)
            {
                nearestInteractor.Interact(gameObject);
            }
        }
    }
}
