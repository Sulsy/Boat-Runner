using Dreamteck.Splines;
using UnityEngine;

public class SplineProgress : MonoBehaviour
{
    public SplineComputer splineComputer;
    public Transform objectToMove;

    private float progress;

    private void Update()
    {
        progress = (float)splineComputer.Project(objectToMove.position).percent;
        Debug.Log("Прогресс прохождения по сплайну: " + progress);
    }
}