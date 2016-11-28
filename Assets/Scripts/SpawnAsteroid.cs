using UnityEngine;
using Random = System.Random;

public class SpawnAsteroid : MonoBehaviour
{
    private Random _random;
    // Use this for initialization
	void Start ()
	{
	    _random = new Random();
	}

    // Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_random.Next()%2 == 0)
            {
                CreateAsteroid("Voxis", "Textures/Asteroids/Asteroid1Texture");
            }
            else
            {
                CreateAsteroid("Pyrexes", "Textures/Asteroids/Asteroid2Texture");
            }
        }
    }

    private void CreateAsteroid(string asteroidName, string resourcePath)
    {
        var durability = _random.Next(0, 300);
        var asteroidGameObject = new GameObject(string.Format("{0}_{1}", asteroidName, durability));
        var asteroid = asteroidGameObject.AddComponent<Asteroid>();
        asteroid.transform.position = new Vector3(_random.Next(-5, 10), _random.Next(-5, 10), 0);

        var scaleFactor = 1 + (durability - 500)/300f;
        asteroid.transform.localScale += new Vector3(scaleFactor, scaleFactor, 1);
        asteroid.Name = asteroidName;
        asteroid.Durability = durability;
        asteroid.ResourcePath = resourcePath;
        asteroid.ExplosionPrefab = (GameObject) Resources.Load("Prefabs/ExplosionPrefab", typeof (GameObject));
    }
}