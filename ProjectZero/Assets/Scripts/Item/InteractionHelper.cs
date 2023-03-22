using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionHelper : MonoBehaviour
{
    public KeyCode interactKey;  // 交互键
    public float interactRange;  // 交互范围

    private Interactor nearestInteractor;  // 最近的Interactor组件
    private GameObject focusedObject;  // 当前聚焦的物体

    void Update()
    {
        // 按下交互键
        if (Input.GetKeyDown(interactKey))
        {
            // 获取所有距离小于交互范围的物体
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, interactRange);

            // 遍历所有物体，找到最近的Interactor组件
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

            // 如果最近的物体存在Interactor组件，则调用其Interact函数
            if (nearestInteractor != null)
            {
                nearestInteractor.Interact(gameObject);
            }
        }
    }
}
