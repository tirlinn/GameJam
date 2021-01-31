using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PickDrop : MonoBehaviour
{

    [SerializeField]
    UnityEngine.UI.Text pickUpText;
    [SerializeField]
    UnityEngine.UI.Text GoalText;

    List<Tuple<string, int>> attractionList = new List<Tuple<string, int>>();

    List<Tuple<GameObject, int>> countryList = new List<Tuple<GameObject, int>>();

    bool _pickUpAllowed;
    bool _dropAllowed;

    int goalComplete = 0;

    private string goalText = "Find the lost attractions: \n";
    private GameObject _item;
    private bool _fullInventory;
    private Collider2D _dropCollision;
    private Collider2D _pickUpCollision;
    List<int> randomNumbers;
    public List<Tuple<string, int>> goalList = new List<Tuple<string, int>>();

    // Start is called before the first frame update
    void Start()
    {
        ListOfAttractionNames();

        GoalRandomizer();

        _fullInventory = false;
        pickUpText.gameObject.SetActive( false );

        var listOfSceneAttractions = GameObject.FindGameObjectsWithTag( "Attraction" );
        for( int i = 0; i < listOfSceneAttractions.Length; i++ ) {
            if( listOfSceneAttractions[ i ].name.Contains( "Kazakhstan" ) ) {
                countryList.Add( (listOfSceneAttractions[ i ], 1).ToTuple() );
            }
            else if( listOfSceneAttractions[ i ].name.Contains( "USA" ) ) {
                countryList.Add( (listOfSceneAttractions[ i ], 2).ToTuple() );
            }
            else if( listOfSceneAttractions[ i ].name.Contains( "Russia" ) ) {
                countryList.Add( (listOfSceneAttractions[ i ], 3).ToTuple() );

            }
            else if( listOfSceneAttractions[ i ].name.Contains( "Egypt" ) ) {
                countryList.Add( (listOfSceneAttractions[ i ], 4).ToTuple() );
            }
            else if( listOfSceneAttractions[ i ].name.Contains( "Brazil" ) ) {
                countryList.Add( (listOfSceneAttractions[ i ], 5).ToTuple() );
            }
            else if( listOfSceneAttractions[ i ].name.Contains( "China" ) ) {
                countryList.Add( (listOfSceneAttractions[ i ], 6).ToTuple() );
            }
            else if( listOfSceneAttractions[ i ].name.Contains( "Chilli" ) ) {
                countryList.Add( (listOfSceneAttractions[ i ], 7).ToTuple() );
            }
            else if( listOfSceneAttractions[ i ].name.Contains( "Italy" ) ) {
                countryList.Add( (listOfSceneAttractions[ i ], 8).ToTuple() );
            }
            else if( listOfSceneAttractions[ i ].name.Contains( "Japan" ) ) {
                countryList.Add( (listOfSceneAttractions[ i ], 9).ToTuple() );
            }
            else if( listOfSceneAttractions[ i ].name.Contains( "Mexico" ) ) {
                countryList.Add( (listOfSceneAttractions[ i ], 10).ToTuple() );
            }
            else if( listOfSceneAttractions[ i ].name.Contains( "France" ) ) {
                countryList.Add( (listOfSceneAttractions[ i ], 11).ToTuple() );
            }
            else if( listOfSceneAttractions[ i ].name.Contains( "Australia" ) ) {
                countryList.Add( (listOfSceneAttractions[ i ], 12).ToTuple() );
            }
            else if( listOfSceneAttractions[ i ].name.Contains( "India" ) ) {
                countryList.Add( (listOfSceneAttractions[ i ], 13).ToTuple() );
            }
            else if( listOfSceneAttractions[ i ].name.Contains( "Turkey" ) ) {
                countryList.Add( (listOfSceneAttractions[ i ], 14).ToTuple() );
            }
            else if( listOfSceneAttractions[ i ].name.Contains( "England" ) ) {
                countryList.Add( (listOfSceneAttractions[ i ], 15).ToTuple() );
            }
            else if( listOfSceneAttractions[ i ].name.Contains( "UAE" ) ) {
                countryList.Add( (listOfSceneAttractions[ i ], 16).ToTuple() );
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if( _pickUpAllowed && Input.GetKeyDown( KeyCode.E ) && _fullInventory == false ) {
            PickUp( _pickUpCollision );
        }
        if( _dropAllowed && Input.GetKeyDown( KeyCode.R ) && _fullInventory == true ) {
            DropItem( _dropCollision );
        }
        if( goalComplete == 5 ) {
            SceneManager.LoadScene("You_won");
        }
    }

    void OnTriggerEnter2D( Collider2D collision )
    {
        if( collision.gameObject.tag.Equals( "Attraction" ) ) {
            pickUpText.text = "Press \'E\' to pick up the attraction";
            pickUpText.gameObject.SetActive( true );
            _pickUpAllowed = true;
            _pickUpCollision = collision;
            if( _fullInventory == true ) {
                pickUpText.text = "You are not allowed to carry more than one\nattraction!";
            }
        }

        if( collision.gameObject.tag.Equals( "RightAttractions" ) ) {
            if( _item != null ) {
                if( collision.gameObject.name.Contains( _item.name ) ) {
                    pickUpText.text = "Press \'R\' to place the attraction";
                    pickUpText.gameObject.SetActive( true );
                    _dropAllowed = true;
                    _dropCollision = collision;
                }
                else {
                    Debug.Log( _item.name );
                    Debug.Log( collision.gameObject.name );
                    pickUpText.text = "That is not the correct place for the attraction!";
                    pickUpText.gameObject.SetActive( true );
                    _dropAllowed = false;
                }
            }
        }
    }

    void OnTriggerExit2D( Collider2D collision )
    {
        if( collision.gameObject.tag.Equals( "Attraction" ) ) {
            pickUpText.gameObject.SetActive( false );
            _pickUpAllowed = false;
        }

        if( collision.gameObject.tag.Equals( "RightAttractions" ) ) {
            pickUpText.gameObject.SetActive( false );
            _dropAllowed = false;
        }
    }

    void PickUp( Collider2D collision )
    {
        _item = collision.gameObject;
        _item.SetActive( false );
        _fullInventory = true;
    }

    private void DropItem( Collider2D collision )
    {
        _item.transform.position = collision.gameObject.transform.position;
        collision.gameObject.SetActive( false );
        collision.gameObject.GetComponent<CircleCollider2D>().enabled = false;

        foreach( var item in countryList ) {
            if( _item == item.Item1 ) {
                foreach( var ids in goalList ) {
                    if( item.Item2 == ids.Item2 ) {
                        goalComplete += 1;
                        goalText = goalText.Replace(ids.Item1, "Done");
                        GoalComplete();
                    }
                }
            }
        }
        Debug.Log( goalComplete );

        _item.SetActive( true );
        _item.GetComponent<CircleCollider2D>().enabled = false;
        _item = null;
        _fullInventory = false;
    }

    private void GoalRandomizer()
    {
        GameObject[] attractions = GameObject.FindGameObjectsWithTag( "Attraction" );
        var randomNums = Enumerable.Range( 0, attractionList.Count );
        System.Random random = new System.Random();
        randomNumbers = randomNums.OrderBy( num => random.Next() ).ToList();

        for( int i = 0; i < 5; i++ ) {
            goalText = goalText + $"{attractionList[ randomNumbers[ i ] ].Item1} \n";
            goalList.Add( (attractionList[ randomNumbers[ i ] ].Item1, attractionList[ randomNumbers[ i ] ].Item2).ToTuple() );
        }

        GoalText.text = goalText;
    }

    private void GoalComplete()
    {
        GoalText.text = goalText;
    }

    private void ListOfAttractionNames()
    {
        attractionList.Add( ("Baiterek", 1).ToTuple() );
        attractionList.Add( ("The Statue of Liberty", 2).ToTuple() );
        attractionList.Add( ("The Kremlin", 3).ToTuple() );
        attractionList.Add( ("the Pyramid of Cheops", 4).ToTuple() );
        attractionList.Add( ("Christo Redentor", 5).ToTuple() );
        attractionList.Add( ("Forbidden City", 6).ToTuple() );
        attractionList.Add( ("Rapa Nui", 7).ToTuple() );
        attractionList.Add( ("Leaning Tower", 8).ToTuple() );
        attractionList.Add( ("Torii", 9).ToTuple() );
        attractionList.Add( ("Teotihuacan", 10).ToTuple() );
        attractionList.Add( ("Eiffel Tower", 11).ToTuple() );
        attractionList.Add( ("Sydney Opera House", 12).ToTuple() );
        attractionList.Add( ("Taj Mahal", 13).ToTuple() );
        attractionList.Add( ("Saint Sophie Cathedal", 14).ToTuple() );
        attractionList.Add( ("Big Ben", 15).ToTuple() );
        attractionList.Add( ("Burj Khalifa", 16).ToTuple() );
    }
}