using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MissionType
{
    Elimination,       // Уничтожение врагов
    Survival,          // Выживание по таймеру
    DataTransfer,      // Защита сигнала
    DestroyCore,       // Уничтожение цели
    CollectResources   // Сбор предметов
}