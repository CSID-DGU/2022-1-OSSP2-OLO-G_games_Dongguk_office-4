using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ConsumeData : Item
{
    public enum ConsumeType { Hp, Mp }
    public ConsumeType consumeType;
    public int itemAmount;

}
