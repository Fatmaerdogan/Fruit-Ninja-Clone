using UnityEngine;

public class TargetSliced : MonoBehaviour
{
	[SerializeField] float destroyPosY = -6f;

	Transform firstSlice, secondSlice;

	void Start()
	{
		firstSlice = transform.GetChild(0);
		secondSlice = transform.GetChild(1);
	}

	void Update()
    {
		float heighestSlicePosY = firstSlice.position.y >= secondSlice.position.y ? firstSlice.position.y : secondSlice.position.y;
		if (heighestSlicePosY < destroyPosY)
		{
			DestroyObject();
		}
    }

    public void DestroyObject()
	{
		Destroy(firstSlice.gameObject);
		Destroy(secondSlice.gameObject);
		Destroy(gameObject);
	}
}
