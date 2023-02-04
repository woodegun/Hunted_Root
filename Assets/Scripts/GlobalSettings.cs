using UnityEngine;

public class GlobalSettings : MonoBehaviour
{
    public static GlobalSettings INSTANSE;

    [Header("Player")] public float PlayerSpeed = 8.0f;
    public float AccelerationMaxSpeed = 4;
    public float AccelerationMaxStamina = 5;
    public float DigInKdMax = 20;

    [Header("Skeleton")] 
    public float MaxFearTime = 15f;
    public float MaxDecreasedSpeedTime = 2;
    public float SkeletonDecreasedSpeed = 3;
    public float SkeletonNormalSpeed = 5;
    public float HearingRange = 100f;

    [Header("Ghost")] public float GhostSpeed = 3f;

    [Header("Spider")] 
    public float MaxWebShootTime = 15;
    public float SpiderNormalSpeed = 5;
    public float SpiderSuperSpeed = 8;
    public float SpiderMaxSuperSpeedTime = 2;

    [Header("Web")] public float WebLiveTime = 1500f;

    [Header("Roots")] public float CreateRootsMaxTimer = 2;

    [Header("Boss")] public float ShootRate1Phase = 8;
    public float ShootRate2Phase = 7;
    public float ShootRate3Phase = 6;
    public float ShootRate4Phase = 5;
    public float SpawnTime1Phase = 7;
    public float SpawnTime2Phase = 8;
    public float SpawnTime3Phase = 9;
    public float SpawnTime4Phase = 10;
    public float BossMaxHP = 100;

    private void Awake()
    {
        INSTANSE = this;
    }
}