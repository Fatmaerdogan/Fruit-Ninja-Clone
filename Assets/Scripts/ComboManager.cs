using System.Collections;
using UnityEngine;

public class ComboManager : MonoBehaviour
{
	[SerializeField] ComboScore comboScorePrefab;
	[SerializeField] Canvas canvas;

	[SerializeField] [Min(0f)] float maxTimeToAcceptCombo = 0.5f;
	float timeLeftToAcceptCombo;

	int comboLevel;

	Vector3 previousCollisionPoint;
    private void Start()
    {
		Events.OnTargetHit += OnTargetHit;
    }
    private void OnDestroy()
    {
        Events.OnTargetHit -= OnTargetHit;
    }
    void Update()
	{
		CountTimeLeft();

		if (IsTimeElapsed() && comboLevel > 0)
		{
			ShowScoreAndResetCombo();
		}
	}

	void CountTimeLeft()
	{
		if (timeLeftToAcceptCombo > 0f)
		{
			timeLeftToAcceptCombo -= Time.deltaTime;
		}
	}

	bool IsTimeElapsed()
	{
		return timeLeftToAcceptCombo <= 0f;
	}

	public void OnTargetHit(Target slicedTarget, Vector3 collisionPoint)
	{
		if (slicedTarget.IsLethal)
		{
			ShowScoreAndResetCombo();
		}
		else
		{
			NextComboLevel();
		}

		previousCollisionPoint = collisionPoint;
	}

	void ShowScoreAndResetCombo()
	{
		if (comboLevel >= 3) ShowComboScore(comboLevel);
		ResetCombo();
	}

	void ShowComboScore(int comboLevel)
	{
		ComboScore comboScore = Instantiate(comboScorePrefab, Camera.main.WorldToScreenPoint(previousCollisionPoint), Quaternion.Euler(Vector3.zero), canvas.transform);
		comboScore.Text = comboLevel + " fruit\ncombo\n+" + comboLevel;
		StartCoroutine(DestroyComboScoreAfterTime(comboScore));
	}

	IEnumerator DestroyComboScoreAfterTime(ComboScore comboScore)
	{
		yield return new WaitForSeconds(comboScore.AnimationTime);
		Destroy(comboScore.gameObject);
	}

	void ResetCombo()
	{
		comboLevel = 0;
		timeLeftToAcceptCombo = 0f;
	}

	void NextComboLevel()
	{
		timeLeftToAcceptCombo = maxTimeToAcceptCombo;
		comboLevel++;
	}
}
