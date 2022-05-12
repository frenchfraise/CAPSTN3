using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RoomTemplate
{
    public CardinalDirection direction;
    public List<Room> prefabs = new List<Room>();
}


public class DungeonManager : MonoBehaviour
{
    public static DungeonManager instance;
    [SerializeField] private int amountOfRooms;
    //lets say size is 20:40, half them below so it becomes 10:20
    public float verticalGridSize = 10; //10
    public float horizontalGridSize = 20; // 20
 
    public List<Room> rooms = new List<Room>();

    public List<RoomTemplate> roomTemplates = new List<RoomTemplate>();

    public void OnEnable()
    {
        //Spawn starter room
        NewSpawnNewRoom();
        StartCoroutine(TrySpawn());

    }

    private void Update()
    {
       
    }
    IEnumerator TrySpawn()
    {
        yield return new WaitForSeconds(3f);
        NewSpawnNewRoom();
        yield return new WaitForSeconds(3f);
        NewSpawnNewRoom();
        yield return new WaitForSeconds(3f);
        NewSpawnNewRoom();
        yield return new WaitForSeconds(3f);
        NewSpawnNewRoom();
        yield return new WaitForSeconds(3f);
        NewSpawnNewRoom();
        yield return new WaitForSeconds(3f);
        NewSpawnNewRoom();
        yield return new WaitForSeconds(1f);
        NewSpawnNewRoom();
    }
    public void NewSpawnNewRoom()
    {
        //PROBLEMS
        //IF HAVENT FOUND ANY/LIST IS EMPTY/COUNT IS 0 WHAT TO DO, WHAT STEP TO GO BACK


        //Check for room(X)s in list that has directions/passageways that are unoccupied and make list(XL) to add it

        List<Room> potentialRoomBasis = new List<Room>();
        if (rooms.Count > 0)
        {
            foreach (Room room in rooms)
            {
                if (room.GetSurroundingRoomsThatAreNotOccupied().Count > 0)
                {
                    potentialRoomBasis.Add(room);
                }
            }

            //Choose which room(X) to serve as basis for spawning a new tile from rooms (OL) list
            int chosenRoomBasis = -1;

            //Choose direction/passageway from list(XL) to spawn new room (Y)
            chosenRoomBasis = Random.Range(0, potentialRoomBasis.Count);


            //Choose direction/passageway from list(XL) to spawn new room (Y)
            List<Passageway> newRoomPassageways = potentialRoomBasis[chosenRoomBasis].GetSurroundingRoomsThatAreNotOccupied();
            int chosenNewRoomPotential = -1;
            if (newRoomPassageways.Count > 0)
            {
                foreach(Passageway test in newRoomPassageways)
                {
                    Debug.Log("ADDED TO UNOCCUPIED" + test.name.ToString());
                }
          
                chosenNewRoomPotential = Random.Range(0, newRoomPassageways.Count);
                Debug.Log(potentialRoomBasis[chosenRoomBasis].gameObject.name + " - " + newRoomPassageways.Count + " CHOSEN : " + chosenNewRoomPotential);
            }
            else
            {
                Debug.Log("Chosen room from list is already surrounded by rooms that does not connect to it");
                //Choose which room(X) to serve as basis for spawning a new tile from rooms (OL) list
            }

            Vector2 newRoomPosition = (Vector2)potentialRoomBasis[chosenRoomBasis].transform.position;
            Debug.Log(newRoomPosition);
            if (newRoomPassageways[chosenNewRoomPotential].cardinalDirection == CardinalDirection.North)
            {
                newRoomPosition += new Vector2(0, verticalGridSize);

            }
            else if (newRoomPassageways[chosenNewRoomPotential].cardinalDirection == CardinalDirection.South) //Vertical
            {
                newRoomPosition += new Vector2(0, -verticalGridSize);
            }
            else if (newRoomPassageways[chosenNewRoomPotential].cardinalDirection == CardinalDirection.East) //Horizontal
            {
                newRoomPosition += new Vector2(horizontalGridSize, 0);
            }
            else if (newRoomPassageways[chosenNewRoomPotential].cardinalDirection == CardinalDirection.West) //Horizontal
            {
                newRoomPosition += new Vector2(-horizontalGridSize, 0);
            }
            Debug.Log(newRoomPosition);

            //Check unspawned new room (Y)'s surrounding rooms (Z) if they have direction/passageway at the place of new room (Y) and make bool/direction list (ZL) (NEED TO OPTIMIZE)
            List<Room> roomsSurroundingNewRoom = new List<Room>();
            List<Passageway> surroundingPassageWaysLinking = new List<Passageway>();
            Collider2D[] hitColliders;
            Vector2 detectorSize = new Vector2(5, 5);
            //North
            hitColliders = Physics2D.OverlapBoxAll(newRoomPosition + new Vector2(0, verticalGridSize), detectorSize,0f);
            foreach (Collider2D currentHitCollider in hitColliders)
            {
                Debug.Log(currentHitCollider.gameObject.name.ToString());
                if (currentHitCollider.gameObject.name == "Ground")//Specific tile layer
                {
                    if (currentHitCollider.gameObject.transform.parent.GetComponent<Room>())
                    {
                        Room roomFound = currentHitCollider.gameObject.transform.parent.GetComponent<Room>();
                        roomsSurroundingNewRoom.Add(roomFound);
                        Debug.Log("T");
                        Passageway passagewayRoomSurroundingNewRoom = roomFound.MatchPassagewayDirection(CardinalDirection.South);
                        if (passagewayRoomSurroundingNewRoom != null) //if this room (tr) is north of newroom, I need this room's(tr) south passage way
                        {
                            Debug.Log("R");
                            if (passagewayRoomSurroundingNewRoom.isConnectedToAnotherPassageway == false)
                            {
                                //If it hits this condition, it means passageway exists, else there's no passageway wanting to connect to here
                                surroundingPassageWaysLinking.Add(passagewayRoomSurroundingNewRoom);
                            }
                        }
                    }
                }
                
                
            }
            //East
            hitColliders = Physics2D.OverlapBoxAll(newRoomPosition + new Vector2(horizontalGridSize, 0), detectorSize, 0f);
            foreach (Collider2D currentHitCollider in hitColliders)
            {
                Debug.Log(currentHitCollider.gameObject.name.ToString());
                if (currentHitCollider.gameObject.name == "Ground")//Specific tile layer
                {
                    if (currentHitCollider.gameObject.transform.parent.GetComponent<Room>())
                    {
                        Room roomFound = currentHitCollider.gameObject.transform.parent.GetComponent<Room>();
                        roomsSurroundingNewRoom.Add(roomFound);
                        Debug.Log("T");
                        Passageway passagewayRoomSurroundingNewRoom = roomFound.MatchPassagewayDirection(CardinalDirection.West);
                        if (passagewayRoomSurroundingNewRoom != null) //if this room (tr) is north of newroom, I need this room's(tr) south passage way
                        {
                            Debug.Log("R");
                            if (passagewayRoomSurroundingNewRoom.isConnectedToAnotherPassageway == false)
                            {
                                //If it hits this condition, it means passageway exists, else there's no passageway wanting to connect to here
                                surroundingPassageWaysLinking.Add(passagewayRoomSurroundingNewRoom);
                            }
                        }

                    }
                }
            }
            //South
            hitColliders = Physics2D.OverlapBoxAll(newRoomPosition + new Vector2(0, -verticalGridSize), detectorSize, 0f);
            foreach (Collider2D currentHitCollider in hitColliders)
            {
                Debug.Log(currentHitCollider.gameObject.name.ToString());
                if (currentHitCollider.gameObject.name == "Ground")//Specific tile layer
                {
                    
                    if (currentHitCollider.gameObject.transform.parent.GetComponent<Room>())
                    {
                        Room roomFound = currentHitCollider.gameObject.transform.parent.GetComponent<Room>();
                        roomsSurroundingNewRoom.Add(roomFound);
                        Debug.Log("T");
                        Passageway passagewayRoomSurroundingNewRoom = roomFound.MatchPassagewayDirection(CardinalDirection.North);
                        if (passagewayRoomSurroundingNewRoom != null) //if this room (tr) is north of newroom, I need this room's(tr) south passage way
                        {
                            Debug.Log("R");
                            if (passagewayRoomSurroundingNewRoom.isConnectedToAnotherPassageway == false)
                            {
                                //If it hits this condition, it means passageway exists, else there's no passageway wanting to connect to here
                                surroundingPassageWaysLinking.Add(passagewayRoomSurroundingNewRoom);
                            }
                        }
                    }
                    
                }
            }
            //West
            hitColliders = Physics2D.OverlapBoxAll(newRoomPosition + new Vector2(-horizontalGridSize, 0), detectorSize, 0f);
            foreach (Collider2D currentHitCollider in hitColliders)
            {
                Debug.Log(currentHitCollider.gameObject.name.ToString());
                if (currentHitCollider.gameObject.name == "Ground")//Specific tile layer
                {
                    if (currentHitCollider.gameObject.transform.parent.GetComponent<Room>())
                    {
                        Room roomFound = currentHitCollider.gameObject.transform.parent.GetComponent<Room>();
                        roomsSurroundingNewRoom.Add(roomFound);
                        Debug.Log("T");
                        Passageway passagewayRoomSurroundingNewRoom = roomFound.MatchPassagewayDirection(CardinalDirection.East);
                        if (passagewayRoomSurroundingNewRoom != null) //if this room (tr) is north of newroom, I need this room's(tr) south passage way
                        {
                            Debug.Log("R");
                            if (passagewayRoomSurroundingNewRoom.isConnectedToAnotherPassageway == false)
                            {
                                //If it hits this condition, it means passageway exists, else there's no passageway wanting to connect to here
                                surroundingPassageWaysLinking.Add(passagewayRoomSurroundingNewRoom);
                            }
                        }
                    }
                }
            }

            //Make list of room prefabs (YL) that has all the bools/directions in the bool/direction list (ZL)
            List<Room> prefabThatCanBeSpawned = new List<Room>();

            CardinalDirection cdTemp = newRoomPassageways[chosenNewRoomPotential].cardinalDirection;
            int chosenRoomTemplate = -1;
            if (roomTemplates.Count > 0)
            {
                for (int i = 0; i < roomTemplates.Count; i++)
                {
                    if (roomTemplates[i].direction == cdTemp)
                    {
                        chosenRoomTemplate = i;
                        break;
                    }
                }
            }
            else
            {
                Debug.Log("Cant choose room template, roomTemplates is empty, Please make new roomTemplate in DungeonManager Inspector");
            }


            //TEMP CONVERT PASSAGE WAY TO CARDINAL DIRECTION
            List<CardinalDirection> cdListTemp = new List<CardinalDirection>();
            foreach (Passageway currentSurroundingPassageWaysLinking in surroundingPassageWaysLinking)
            {
                cdListTemp.Add(currentSurroundingPassageWaysLinking.cardinalDirection);
            }

            foreach (Room currentRoomPrefab in roomTemplates[chosenRoomTemplate].prefabs)
            {
                if (currentRoomPrefab.HasPassagewaysForRequiredDirections(cdListTemp))
                {
                    prefabThatCanBeSpawned.Add(currentRoomPrefab);
                }
            }

            //Choose what room prefab to spawn for new room (Y) from list of room prefabs (YL)
            int chosenRoomPrefab = -1;
            if (prefabThatCanBeSpawned.Count > 0)
            {
                //Choose direction/passageway from list(XL) to spawn new room (Y)
                chosenRoomPrefab = Random.Range(0, prefabThatCanBeSpawned.Count);
            }
            else
            {
                Debug.Log("Cant choose room prefab, list is empty. Please make a room prefab that connects to the following directions: " + cdListTemp.ToString());


            }
            Debug.Log(newRoomPosition);

            //INSTATIATING NEW ROOM
            Room newRoom = Instantiate(prefabThatCanBeSpawned[chosenRoomPrefab]);
            rooms.Add(newRoom);
            newRoom.transform.position = newRoomPosition;

            //LINK ALL PASSAGEWAYS
            foreach (Passageway cp in surroundingPassageWaysLinking)
            {
                Debug.Log(cp.gameObject.name);
                //CardinalDirection surroundingRoomPassageway = CardinalDirection.None;
                CardinalDirection newRoomPassageway = CardinalDirection.None;
                if (cp.cardinalDirection == CardinalDirection.North)
                {
                    newRoomPassageway = CardinalDirection.South;
                }
                else if (cp.cardinalDirection == CardinalDirection.East)
                {
                    newRoomPassageway = CardinalDirection.West;
                }
                else if (cp.cardinalDirection == CardinalDirection.South)
                {
                    newRoomPassageway = CardinalDirection.North;
                }
                else if(cp.cardinalDirection == CardinalDirection.West)
                {
                    newRoomPassageway = CardinalDirection.East;
                }
              

                foreach (Passageway cpr in newRoom.GetPassageways())
                {
                    if (cpr.cardinalDirection == newRoomPassageway)
                    {
                        cp.connectedTo = cpr;
                        cp.isConnectedToAnotherPassageway = true;
                        cpr.connectedTo = cp;
                        cpr.isConnectedToAnotherPassageway = true;
                    }
                }
            }

        }
        else
        {
            //Create new room because there are no room in rooms yet
            int chosenRoomPrefab = Random.Range(0, roomTemplates[4].prefabs.Count);
            
            //INSTATIATING NEW ROOM

            Room newRoom = Instantiate(roomTemplates[4].prefabs[chosenRoomPrefab]);
            rooms.Add(newRoom);
            newRoom.transform.position = new Vector2(0,0);
        }
      
       
    }

    public void SpawnNewRoom()
    {
        //PROBLEMS
        //IF HAVENT FOUND ANY/LIST IS EMPTY/COUNT IS 0 WHAT TO DO, WHAT STEP TO GO BACK
        
        //Choose which room(X) to serve as basis for spawning a new tile from rooms (OL) list
        int chosenRoom = -1;
        if (rooms.Count > 0)
        {
            chosenRoom = Random.Range(0,rooms.Count);
        }
        else
        {
            Debug.Log("There is no room in list yet");
        }

        //Check if room(X) has directions/passageways that are unoccupied and make list(XL) to add it
        List<Passageway> newRoomPotential = rooms[chosenRoom].GetSurroundingRoomsThatAreNotOccupied();

        int chosenNewRoomPotential = -1;
        if (newRoomPotential.Count > 0)
        {
            //Choose direction/passageway from list(XL) to spawn new room (Y)
            chosenNewRoomPotential = Random.Range(0, rooms.Count);
        }
        else
        {
            Debug.Log("Chosen room from list is already surrounded by rooms");
            //Choose which room(X) to serve as basis for spawning a new tile from rooms (OL) list
        }

        Vector2 newRoomPosition = (Vector2)rooms[chosenRoom].transform.position;
        if (newRoomPotential[chosenNewRoomPotential].cardinalDirection == CardinalDirection.North ||
            newRoomPotential[chosenNewRoomPotential].cardinalDirection == CardinalDirection.South) //Vertical
        {
            newRoomPosition += new Vector2(0, verticalGridSize);
        }
        else if (newRoomPotential[chosenNewRoomPotential].cardinalDirection == CardinalDirection.East ||
                newRoomPotential[chosenNewRoomPotential].cardinalDirection == CardinalDirection.West) //Horizontal
        {
            newRoomPosition += new Vector2(horizontalGridSize, 0);
        }

        //Check unspawned new room (Y)'s surrounding rooms (Z) if they have direction/passageway at the place of new room (Y) and make bool/direction list (ZL) (NEED TO OPTIMIZE)
        List<Room> roomsSurroundingNewRoom = new List<Room>();
        List<Passageway> surroundingPassageWaysLinking = new List<Passageway>();
        Collider[] hitColliders;
        Vector2 detectorSize = new Vector2(5, 5);
        //North
        hitColliders = Physics.OverlapBox(newRoomPosition + new Vector2(0, verticalGridSize), detectorSize, Quaternion.identity);
        foreach (Collider currentHitCollider in hitColliders)
        {
            if (currentHitCollider.gameObject.GetComponent<Room>())
            {
                roomsSurroundingNewRoom.Add(currentHitCollider.gameObject.GetComponent<Room>());
                if (currentHitCollider.gameObject.GetComponent<Room>().MatchPassagewayDirection(CardinalDirection.South) != null) //if this room (tr) is north of newroom, I need this room's(tr) south passage way
                {
                    //If it hits this condition, it means passageway exists, else there's no passageway wanting to connect to here
                    surroundingPassageWaysLinking.Add(currentHitCollider.gameObject.GetComponent<Room>().MatchPassagewayDirection(CardinalDirection.South));
                }
            }
        }
        //East
        hitColliders = Physics.OverlapBox(newRoomPosition + new Vector2(horizontalGridSize, 0), detectorSize, Quaternion.identity);
        foreach (Collider currentHitCollider in hitColliders)
        {
            if (currentHitCollider.gameObject.GetComponent<Room>())
            {
                roomsSurroundingNewRoom.Add(currentHitCollider.gameObject.GetComponent<Room>());
                if (currentHitCollider.gameObject.GetComponent<Room>().MatchPassagewayDirection(CardinalDirection.West) != null) //if this room (tr) is north of newroom, I need this room's(tr) west passage way
                {
                    //If it hits this condition, it means passageway exists, else there's no passageway wanting to connect to here
                    surroundingPassageWaysLinking.Add(currentHitCollider.gameObject.GetComponent<Room>().MatchPassagewayDirection(CardinalDirection.West));
                }
            }
        }
        //South
        hitColliders = Physics.OverlapBox(newRoomPosition + new Vector2(0, -verticalGridSize), detectorSize, Quaternion.identity);
        foreach (Collider currentHitCollider in hitColliders)
        {
            if (currentHitCollider.gameObject.GetComponent<Room>())
            {
                roomsSurroundingNewRoom.Add(currentHitCollider.gameObject.GetComponent<Room>());
                if (currentHitCollider.gameObject.GetComponent<Room>().MatchPassagewayDirection(CardinalDirection.North) != null) //if this room (tr) is north of newroom, I need this room's(tr) North passage way
                {
                    //If it hits this condition, it means passageway exists, else there's no passageway wanting to connect to here
                    surroundingPassageWaysLinking.Add(currentHitCollider.gameObject.GetComponent<Room>().MatchPassagewayDirection(CardinalDirection.North));
                }
            }
        }
        //West
        hitColliders = Physics.OverlapBox(newRoomPosition + new Vector2(-horizontalGridSize, 0), detectorSize, Quaternion.identity);
        foreach (Collider currentHitCollider in hitColliders)
        {
            if (currentHitCollider.gameObject.GetComponent<Room>())
            {
                roomsSurroundingNewRoom.Add(currentHitCollider.gameObject.GetComponent<Room>());
                if (currentHitCollider.gameObject.GetComponent<Room>().MatchPassagewayDirection(CardinalDirection.East) != null) //if this room (tr) is north of newroom, I need this room's(tr) East passage way
                {
                    //If it hits this condition, it means passageway exists, else there's no passageway wanting to connect to here
                    surroundingPassageWaysLinking.Add(currentHitCollider.gameObject.GetComponent<Room>().MatchPassagewayDirection(CardinalDirection.East));
                }
            }
        }

        //Make list of room prefabs (YL) that has all the bools/directions in the bool/direction list (ZL)
        List<Room> prefabThatCanBeSpawned = new List<Room>();

        CardinalDirection cdTemp = newRoomPotential[chosenNewRoomPotential].cardinalDirection;
        int chosenRoomTemplate = -1;
        if (roomTemplates.Count > 0)
        {
            for (int i = 0; i < roomTemplates.Count; i++)
            {
                if (roomTemplates[i].direction == cdTemp)
                {
                    chosenRoomTemplate = i;
                    break;
                }
            }
        }
        else
        {
            Debug.Log("Cant choose room template, roomTemplates is empty");
        }
            

        //TEMP CONVERT PASSAGE WAY TO CARDINAL DIRECTION
        List < CardinalDirection > cdListTemp = new List<CardinalDirection>();
        foreach (Passageway currentSurroundingPassageWaysLinking in surroundingPassageWaysLinking)
        {
            cdListTemp.Add(currentSurroundingPassageWaysLinking.cardinalDirection);
        }

        foreach (Room currentRoomPrefab in roomTemplates[chosenRoomTemplate].prefabs)
        {
            if (currentRoomPrefab.HasPassagewaysForRequiredDirections(cdListTemp))
            {
                prefabThatCanBeSpawned.Add(currentRoomPrefab);
            }
        }

        //Choose what room prefab to spawn for new room (Y) from list of room prefabs (YL)
        int chosenRoomPrefab = -1;
        if (prefabThatCanBeSpawned.Count > 0)
        {
            //Choose direction/passageway from list(XL) to spawn new room (Y)
            chosenRoomPrefab = Random.Range(0, prefabThatCanBeSpawned.Count);
        }
        else
        {
            Debug.Log("Cant choose room prefab, list is empty");
     
        }









        ////OLD

        ////Check Directions That are spawnable

        //CardinalDirection directionChosen = RandomizeDirection();//.None;

        //    // Check what directions of the tile selected can be spawned

        //    List<CardinalDirection> directionsAllowed = new List<CardinalDirection>();




        //    if (directionsAllowed.Count > 0) //If there is directions that can be spawned
        //    {
        //        directionChosen = directionsAllowed[Random.Range(0, directionsAllowed.Count)];

        //        //Check if the chosen tile for the direction

        //    }
        //    else //If there is no directions that can be spawned, look for another tile 
        //    {

        //    }


        //    RoomTemplate p_chosenRoomTemplate = MatchDirection(directionChosen);
        //    int p_chosenIndex = ChooseRoom(directionChosen);
        //    if (p_chosenIndex >= 0)
        //    {


        //        Room roomInstance = Instantiate(p_chosenRoomTemplate.prefabs[p_chosenIndex]);
        //        //Check if chosen room matches
        //        roomInstance.transform.position = Vector3.positiveInfinity;//
        //    }
        //    else
        //    {

        //    }
        //    //roomTemplates
    }
    public CardinalDirection RandomizeDirection()
    {
        return (CardinalDirection)Random.Range(0, System.Enum.GetValues(typeof(CardinalDirection)).Length-2); // 2 because there is no direction and center direction
  
    }

    public RoomTemplate MatchDirection(CardinalDirection p_direction)
    {
        foreach (RoomTemplate currentRoomTemplate in roomTemplates) // Loop through directions in list
        {
            if (currentRoomTemplate.direction == p_direction) //Find list that matches direction
            {
                return currentRoomTemplate;
            }
        }
        return null;
    }

    public int ChooseRoom(CardinalDirection p_direction)
    {
        RoomTemplate currentRoomTemplate = MatchDirection(p_direction);
        if (currentRoomTemplate.prefabs.Count > 0)
        {
                    
            return Random.Range(0, currentRoomTemplate.prefabs.Count);
        }
        else
        {

            return -1;

        }
              

    }

    public void GenerateDungeon()
    {

    }
}
