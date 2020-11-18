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
            case (1):

                for (int i = 0; i < room1UnlockSet.Length; i++)
                {
                    if (checkedNo == (room1UnlockSet.Length + 1))
                    {
                        Debug.Log("Unlocking! All interacted with in Room "+ index);
                        checkedNo = 0;

                        doorToOpen = 1;
                        OnAllInteracted(doorToOpen);

                        doorToOpen = 2;
                        OnAllInteracted(doorToOpen);

                        break;
                    }
                    else if (room1UnlockSet[i].interactedWith == true)
                    {
                        Debug.Log("RUS1: Checked element " + i + ". CheckNo is " + (checkedNo+1));
                        checkedNo++;
                        //checkedNo++;
                        continue;
                    }
                }

                break;

            case (2):

                for (int i = 0; i < room2UnlockSet.Length; i++)
                {
                    if (room2UnlockSet[i].interactedWith == true)
                    {
                        Debug.Log("RUS2: Checked element " + i + ". CheckNo is " + (checkedNo + 1));
                        checkedNo++;
                        //checkedNo++;
                        //continue;

                        if (checkedNo == (room2UnlockSet.Length))
                        {
                            Debug.Log("Unlocking! All interacted with in Room " + index);
                            checkedNo = 0;

                            doorToOpen = 3;

                            //open door3
                            OnAllInteracted(doorToOpen);
                           
                            break;
                        }
                    }

                    //if (checkedNo == (room2UnlockSet.Length))
                    //{
                    //    Debug.Log("Unlocking! All interacted with in Room " + index);
                    //    checkedNo = 0;

                    //    OnAllInteracted(index);
                    //    break;
                    //}

                    //if (room2UnlockSet[i].interactedWith == true)
                    //{
                    //    Debug.Log("RUS2: Checked element " + i + ". CheckNo is " + (checkedNo + 1));
                    //    checkedNo++;
                    //    //checkedNo++;
                    //    continue;
                    //}
                }
                break;

            case (3):

                for (int i = 0; i < room3UnlockSet.Length; i++)
                {
                    if (checkedNo == (room3UnlockSet.Length + 1))
                    {
                        Debug.Log("Unlocking! All interacted with in Room " + index);
                        checkedNo = 0;

                        OnAllInteracted(index);
                        break;
                    }
                    else if (room3UnlockSet[i].interactedWith == true)
                    {
                        Debug.Log("RUS3: Checked element " + i + ". CheckNo is " + (checkedNo + 1));
                        checkedNo++;
                        //checkedNo++;
                        continue;
                    }
                }

                break;

            case (4):

                for (int i = 0; i < room4UnlockSet.Length; i++)
                {
                    if (checkedNo == (room4UnlockSet.Length + 1))
                    {
                        Debug.Log("Unlocking! All interacted with in Room " + index);
                        checkedNo = 0;

                        OnAllInteracted(index);
                        break;
                    }
                    else if (room4UnlockSet[i].interactedWith == true)
                    {
                        Debug.Log("RUS4: Checked element " + i + ". CheckNo is " + (checkedNo + 1));
                        checkedNo++;
                        //checkedNo++;
                        continue;
                    }
                }

                break;

            case (5):

                for (int i = 0; i < room5UnlockSet.Length; i++)
                {
                    if (checkedNo == (room5UnlockSet.Length + 1))
                    {
                        Debug.Log("Unlocking! All interacted with in Room " + index);
                        checkedNo = 0;

                        OnAllInteracted(index);
                        break;
                    }
                    else if (room5UnlockSet[i].interactedWith == true)
                    {
                        Debug.Log("RUS5: Checked element " + i + ". CheckNo is " + (checkedNo + 1));
                        checkedNo++;
                        //checkedNo++;
                        continue;
                    }
                }

                break;

            case (6):

                for (int i = 0; i < room6UnlockSet.Length; i++)
                {
                    if (checkedNo == (room6UnlockSet.Length + 1))
                    {
                        Debug.Log("Unlocking! All interacted with in Room " + index);
                        checkedNo = 0;

                        OnAllInteracted(index);
                        break;
                    }
                    else if (room6UnlockSet[i].interactedWith == true)
                    {
                        Debug.Log("RUS6: Checked element " + i + ". CheckNo is " + (checkedNo + 1));
                        checkedNo++;
                        //checkedNo++;
                        continue;
                    }
                }

                break;

            case (7):

                for (int i = 0; i < room7UnlockSet.Length; i++)
                {
                    if (checkedNo == (room7UnlockSet.Length + 1))
                    {
                        Debug.Log("Unlocking! All interacted with in Room " + index);
                        checkedNo = 0;

                        OnAllInteracted(index);
                        break;
                    }
                    else if (room7UnlockSet[i].interactedWith == true)
                    {
                        Debug.Log("RUS7: Checked element " + i + ". CheckNo is " + (checkedNo + 1));
                        checkedNo++;
                        //checkedNo++;
                        continue;
                    }
                }

                break;

            case (8):

                for (int i = 0; i < room8UnlockSet.Length; i++)
                {
                    if (checkedNo == (room8UnlockSet.Length + 1))
                    {
                        Debug.Log("Unlocking! All interacted with in Room " + index);
                        checkedNo = 0;

                        OnAllInteracted(index);
                        break;
                    }
                    else if (room8UnlockSet[i].interactedWith == true)
                    {
                        Debug.Log("RUS8: Checked element " + i + ". CheckNo is " + (checkedNo + 1));
                        checkedNo++;
                        //checkedNo++;
                        continue;
                    }
                }

                break;

            case (9):

                for (int i = 0; i < room9UnlockSet.Length; i++)
                {
                    if (checkedNo == (room9UnlockSet.Length + 1))
                    {
                        Debug.Log("Unlocking! All interacted with in Room " + index);
                        checkedNo = 0;

                        OnAllInteracted(index);
                        break;
                    }
                    else if (room9UnlockSet[i].interactedWith == true)
                    {
                        Debug.Log("RUS9: Checked element " + i + ". CheckNo is " + (checkedNo + 1));
                        checkedNo++;
                        //checkedNo++;
                        continue;
                    }
                }

                break;
        }
    }

    void UnlockDoor(int doorToOpen)
    {
        // -1 coz index started at 1 (as in Room 1)
        doors[doorToOpen-1].gameObject.SetActive(false);
    }

}
