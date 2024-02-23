using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceCalculator : MonoBehaviour
{
    public Transform target; // объект, до которого нужно рассчитать дистанцию
    public float speed = 5f; // скорость объекта
    private float distance; // расстояние до объекта

    void Start()
    {
        InvokeRepeating("CalculateDistance", 0f, 1f); // вызываем метод CalculateDistance каждую секунду
    }

    void CalculateDistance()
    {
        distance = Vector3.Distance(transform.position,
            target.position); // рассчитываем дистанцию между текущим объектом и целью
        Debug.Log("Distance to target: " + distance.ToString("F2") + " units"); // выводим дистанцию в консоль
    }
}
