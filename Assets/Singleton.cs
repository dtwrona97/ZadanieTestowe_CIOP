using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{

    public Closet closetClothes;
    public Closet closetMasks;
    public Closet closetGuns;
    public Player player;
    public ScenarioManager scenarioManager;

    public static Singleton Instance;
    private void Awake()
    {
        Instance = this;
    }
}
