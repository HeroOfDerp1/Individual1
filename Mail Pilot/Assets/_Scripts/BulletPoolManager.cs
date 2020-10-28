using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Bonus - make this class a Singleton!

[System.Serializable]
public class BulletPoolManager : MonoBehaviour
{
    public GameObject bullet;

    //create reference to the game controller so bullets can be children
    public GameObject GameController;

    //TODO: create a structure to contain a collection of bullets
    //make a queue to contain bullets
    private Queue<GameObject> bulletPool;
    //public max number thing and initialize
    public int maxBullets = 6;

    // Start is called before the first frame update
    void Start()
    {
        // TODO: add a series of bullets to the Bullet Pool
        BuildBulletPool();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //TODO: modify this function to return a bullet from the Pool
    public GameObject GetBullet()
    {

        //temp object to hold the bullet pulled from queue
        GameObject temp;

        //check if there is bullet in the queue
        if(CountBullets() > 0)
        {
            //there is a bullet in the queue, so pull it and set to active
            //takes it out of the queue
            temp = bulletPool.Dequeue();
            //make it show up on screen
            temp.SetActive(true);
            //return the bullet to fire :)
            return temp;
        }
        else
        {
            //theres no bullet in the queue
            //makes the bullet
            temp = Instantiate(bullet);
            //make bullet child of GameController
            temp.transform.parent = GameController.transform;
            //sets a reference to the bullet pool manager in the bullet controller
            temp.GetComponent<BulletController>().bulletPoolManager = this;
            //send the bullet
            return temp;
            //bullet will be added to queue and queue expanded when it deloads
        }
    }

    //TODO: modify this function to reset/return a bullet back to the Pool 
    public void ResetBullet(GameObject bullet)
    {
        //deactivates the bullet
        bullet.SetActive(false);
        //puts the bullet back in the queue
        bulletPool.Enqueue(bullet);
    }

    //build the bullet pool
    private void BuildBulletPool()
    {
        //instantiate the pool
        bulletPool = new Queue<GameObject>();
        //create a temp object to add bullets to the pool
        GameObject temp;
        //fill the queue with the bullets
        for (int i = 0; i < maxBullets; i++)
        {
            //makes the bullet
            temp = Instantiate(bullet);
            //make bullet child of GameController
            temp.transform.parent = GameController.transform;
            //sets a reference to the bullet pool manager in the bullet controller
            temp.GetComponent<BulletController>().bulletPoolManager = this;
            //sets bullet to inactive cause it hasn't been fired
            temp.SetActive(false);
            //add to the queue
            bulletPool.Enqueue(temp);
        }
    }

    //counts number of bullets in queue
    public int CountBullets()
    {
        //gets the count of bullets in the pool
        return bulletPool.Count;
    }

    //checks if queue is empty
    public bool IsEmpty()
    {
        //checks if bullets in the pool is equal to 0
        return (CountBullets() == 0);
    }
}
