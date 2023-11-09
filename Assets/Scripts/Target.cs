using UnityEngine;

public class Target : MonoBehaviour
{

    GameObject spawnManager;

    [SerializeField] GameObject slicedPrefab;
    [SerializeField] ParticleSystem sliceParticles;

    [SerializeField] int points = 1;

    [SerializeField] bool isLethal;
    public bool IsLethal { get => isLethal; }

     float destroyPosY = -8f;

    public Rigidbody MyRigidbody;


    void Start()
    {
        spawnManager = GameObject.Find("Spawn Managers");
    }

    void Update()
    {
        if (transform.position.y < destroyPosY)
		{
            gameObject.SetActive(false);
            if (!isLethal)
            {
                GameManager.instance.RemoveLife();
            }
        }
    }

    public void OnTargetHit(Vector3 hitPosititon)
	{
        if (gameObject != null)
        {
            if (!isLethal)
		    {
                GameObject sliced = Instantiate(slicedPrefab, transform.position, transform.rotation, spawnManager.transform);
                foreach (Rigidbody s in sliced.GetComponentsInChildren<Rigidbody>())
			    {
                    s.velocity = MyRigidbody.velocity;
                    s.angularVelocity = MyRigidbody.angularVelocity;
			    }
                var particles = Instantiate(sliceParticles, transform.position, Quaternion.LookRotation(-(hitPosititon - transform.position).normalized));
                Destroy(particles.gameObject, 2f);

            }
            else
			{
                var particles = Instantiate(sliceParticles, transform.position, Quaternion.Euler(Vector3.up));
                Destroy(particles.gameObject, 5f);
            }

            gameObject.SetActive(false);

            if (isLethal)
            {
                GameManager.instance.RemoveAllLives();
            }
            else
            {
                GameManager.instance.AddScore(points);
            }
        }
    }

    public void ResetForces()
	{
        MyRigidbody.velocity = Vector3.zero;
        MyRigidbody.angularVelocity = Vector3.zero;
	}

    public void AddForce(Vector3 force)
	{
        MyRigidbody.AddForce(force, ForceMode.Impulse);
	}

    public void AddRandomForce(float minVerticalForce, float maxVerticalForce, float maxHorizontalForce)
	{
        AddRandomVerticalForce(minVerticalForce, maxVerticalForce);
        AddRandomHorizontalForce(maxHorizontalForce);
	}

    public void AddRandomVerticalForce(float minVerticalForce, float maxVerticalForce)
	{
        Vector3 randomForce = Vector3.up * Random.Range(minVerticalForce, maxVerticalForce);
        MyRigidbody.AddForce(randomForce, ForceMode.Impulse);
    }

    public void AddRandomHorizontalForce(float maxHorizontalForce)
    {
        Vector3 randomForce = Vector3.right * Random.Range(-maxHorizontalForce, maxHorizontalForce);
        MyRigidbody.AddForce(randomForce, ForceMode.Impulse);
    }

    public void AddRandomTorque(float maxTorque)
    {
        Vector3 randomTorque = new Vector3(Random.Range(-maxTorque, maxTorque), Random.Range(-maxTorque, maxTorque), Random.Range(-maxTorque, maxTorque));
        MyRigidbody.AddTorque(randomTorque, ForceMode.Impulse);
    }
}
