using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private GoalManager _goalManager;
    private bool _active = false;
    private float _time = 0;

    private void Update()
    {
        if (!_active) return;
        _time += Time.deltaTime;
        _goalManager.TrackTime(_time);
    }

    public void Initialize(GoalManager goalManager)
    {
        _active = true;
        _goalManager = goalManager;
    }
}
