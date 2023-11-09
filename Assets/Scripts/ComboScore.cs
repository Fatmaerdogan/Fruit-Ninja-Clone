using UnityEngine;
using TMPro;

public class ComboScore : MonoBehaviour
{
    [SerializeField] AnimationCurve sizeCurve;

	float timeElapsed;

	RectTransform rectTransform;
	TextMeshProUGUI text;

	public string Text { get => text.text; set => text.text = value; }
	public float AnimationTime { get=> sizeCurve.keys[sizeCurve.length - 1].time; }

	void Awake()
	{
		rectTransform = GetComponent<RectTransform>();
		text = GetComponent<TextMeshProUGUI>();
	}

	void Update()
    {
		Animate();
    }

	void Animate()
	{
		rectTransform.localScale = Vector3.one * sizeCurve.Evaluate(timeElapsed);
		timeElapsed += Time.deltaTime;
	}
}
