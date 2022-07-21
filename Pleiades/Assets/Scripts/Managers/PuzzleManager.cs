using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PuzzleManager : MonoBehaviour
{
    //doors to unlock
    public GameObject[] doors;
    public int index;
    private int doorToOpen;
    private int doorToLock;

    public Interactables[] room1UnlockSet;
    public Interactables[] room2UnlockSet;
    public Interactables[] room3UnlockSet;
    public Interactables[] room4UnlockSet;
    public Interactables[] room5UnlockSet;
    public Interactables[] room6UnlockSet;
    public Interactables[] room7UnlockSet;
    public Interactables[] room8UnlockSet;
    public Interactables[] room9UnlockSet;

    public int checkedNo;

    public static PuzzleManager instance;

    public Action<int> OnItemInteracted;
    public Action<int> OnAllInteracted;
    public Action<int> OnNewRoomEnter;

    private void Awake()
    {
        OnItemInteracted += CheckSet;
        OnAllInteracted += UnlockDoor;
        OnNewRoomEnter += LockDoor;

        //roomUnlocks[0] = room1UnlockSet;
    }

    private void LockDoor(int doorToLock)
    {
        doors[doorToLock - 1].gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        OnItemInteracted -= CheckSet;
        OnAllInteracted -= UnlockDoor;
        OnNewRoomEnter -= LockDoor;
    }

    void Start()
    {
        //Singleton
        if(!instance)
        {
            instance = this;
        }
    }

    void Update()
    {
        
    }

    public void CheckSet(int index)
    {
        switch (index)
        {
            case (2):

                for (int i = 0; i < room2UnlockSet.Length; i++)
                {
                    if (checkedNo == (room2UnlockSet.Length + 1))
                    {
                        Debug.Log("Unlocking! All interacted with in Room "+ index);
                        checkedNo = 0;

                        doorToOpen = 1;
                        OnAllInteracted(doorToOpen);

                        doorToOpen = 2;
                        doorToOpen = 2;
                        OnAllInteracted(doorToOpen);

                        break;
                    }
                    else if (room2UnlockSet[i].interactedWith == true)
                    {
                        Debug.Log("RUS2: Checked element " + i + ". CheckNo is " + (checkedNo+1));
                        checkedNo++;
                        //checkedNo++;
                        continue;
                    }
                }

                break;

            case (3):

                for (int i = 0; i < room3UnlockSet.Length; i++)
                {
                    if (room3UnlockSet[i].interactedWith == true)
                    {
                        Debug.Log("RUS3: Checked element " + i + ". CheckNo is " + (checkedNo + 1));
                        if (room3UnlockSet[i].beenChecked == false)
                        {
                            checkedNo++;
                            room3UnlockSet[i].beenChecked = true;
                        }
                        if (checkedNo == (room3UnlockSet.Length))
                        {
                            Debug.Log("Unlocking! All interacted with in Room " + index);
                            checkedNo = 0;

                            doorToOpen = 3;

                            //open door3
                            OnAllInteracted(doorToOpen);

                            doorToOpen = 4;

                            //open door3
                            OnAllInteracted(doorToOpen);

                            break;
                        }
                    }
                }
                break;

            case (4):

                for (int i = 0; i < room4UnlockSet.Length; i++)
                {
                    if (room4UnlockSet[i].interactedWith == true)
                    {
                        Debug.Log("RUS4: Checked element " + i + ". CheckNo is " + (checkedNo + 1));
                        if (room4UnlockSet[i].beenChecked == false)
                        {
                            checkedNo++;
                            room4UnlockSet[i].beenChecked = true;
                        }

                        if (checkedNo == (room4UnlockSet.Length))
                        {
                            Debug.Log("Unlocking! All interacted with in Room " + index);
                            checkedNo = 0;

                            doorToOpen = 5;
                            OnAllInteracted(doorToOpen);


                            break;
                        }
                    }
                }
                    break;
                


            case (5):

                for (int i = 0; i < room5UnlockSet.Length; i++)
                {
                    if (room5UnlockSet[i].interactedWith == true)
                    {
                        Debug.Log("RUS5: Checked element " + i + ". CheckNo is " + (checkedNo + 1));

                        if (room5UnlockSet[i].beenChecked == false)
                        {
                            checkedNo++;
                            room5UnlockSet[i].beenChecked = true;
                        }
                        
                        //checkedNo++;
                        //continue;

                        if (checkedNo == (room5UnlockSet.Length))
                        {
                            Debug.Log("Unlocking! All interacted with in Room " + index);
                            checkedNo = 0;

                            doorToOpen = 6;
                            OnAllInteracted(doorToOpen);



                            break;
                        }
                    }
                }
                break;

      

            case (6):
                for (int i = 0; i < room6UnlockSet.Length; i++)
                {
                    if (room6UnlockSet[i].interactedWith == true)
                    {
                        Debug.Log("RUS6: Checked element " + i + ". CheckNo is " + (checkedNo + 1));
                        if (room6UnlockSet[i].beenChecked == false)
                        {
                            checkedNo++;
                            room6UnlockSet[i].beenChecked = true;
                        }

                        if (checkedNo == (room6UnlockSet.Length))
                        {
                            Debug.Log("Unlocking! All interacted with in Room " + index);
                            checkedNo = 0;


                            doorToOpen = 7;
                            OnAllInteracted(doorToOpen);

                            doorToOpen = 8;
                            OnAllInteracted(doorToOpen);


                            break;
                        }
                    }
                }
                break;


            case (7):

                for (int i = 0; i < room7UnlockSet.Length; i++)
                {
                    if (room7UnlockSet[i].interactedWith == true)
                    {
                        Debug.Log("RUS7: Checked element " + i + ". CheckNo is " + (checkedNo + 1));
                        if (room7UnlockSet[i].beenChecked == false)
                        {
                            checkedNo++;
                            room7UnlockSet[i].beenChecked = true;
                        }

                        if (checkedNo == (room7UnlockSet.Length))
                        {
                            Debug.Log("Unlocking! All interacted with in Room " + index);
                            checkedNo = 0;

                            doorToOpen = 9;
                            OnAllInteracted(doorToOpen);


                            break;
                        }
                    }
                }
                break;

            case (8):

                for (int i = 0; i < room8UnlockSet.Length; i++)
                {
                    if (room8UnlockSet[i].interactedWith == true)
                    {
                        Debug.Log("RUS8: Checked element " + i + ". CheckNo is " + (checkedNo + 1));

                        if (room8UnlockSet[i].beenChecked == false)
                        {
                            checkedNo++;
                            room8UnlockSet[i].beenChecked = true;
                        }

                        //checkedNo++;
                        //continue;

                        if (checkedNo == (room8UnlockSet.Length))
                        {
                            Debug.Log("Unlocking! All interacted with in Room " + index);
                            checkedNo = 0;

                            //!!add more doors to array
                            doorToOpen = 10;
                            OnAllInteracted(doorToOpen);


                            break;
                        }
                    }
                }
                break;

            case (9):

                for (int i = 0; i < room9UnlockSet.Length; i++)
                {
                    if (room9UnlockSet[i].interactedWith == true)
                    {
                        Debug.Log("RUS9: Checked element " + i + ". CheckNo is " + (checkedNo + 1));
                        if (room9UnlockSet[i].beenChecked == false)
                        {
                            checkedNo++;
                            room9UnlockSet[i].beenChecked = true;
                        }

                        if (checkedNo == (room9UnlockSet.Length))
                        {
                            Debug.Log("Unlocking! All interacted with in Room " + index);
                            checkedNo = 0;

                            doorToOpen = 11;
                            OnAllInteracted(doorToOpen);


                            break;
                        }
                    }
                }
                break;
        }
    }

    void UnlockDoor(int doorToOpen)
    {
        // -1 coz index started at 1 (as in Room 1)
        doors[doorToOpen-1].gameObject.SetActive(false);
        Debug.Log("Unlocked door #" + doorToOpen);
        AudioManager.Instance.doorUnlock.Play();
    }

}
