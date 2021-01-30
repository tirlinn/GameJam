using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomSpawn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        try {

            GameObject[] points = GameObject.FindGameObjectsWithTag( "randomPoint" );
            GameObject[] attractions = GameObject.FindGameObjectsWithTag( "Attraction" );

            var randomNums = Enumerable.Range(0, points.Length);
            System.Random random = new System.Random();
            List<int> randomList= randomNums.OrderBy( num => random.Next() ).ToList();

            for( int i = 0; i < attractions.Length; i++ ) {
                attractions[ i ].transform.position = points[ randomList[i]].transform.position;
            }

        }
        catch(Exception ex ) {
            Debug.Log( ex.Message );
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
