using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGroups
{
    public List<Character> EnnemiesList { get; }
    public bool IsDataInitialized { get; set; }
    public int MaxEnnemiesInGroup { get; }
    public int GroupId { get; }
    public string[] EnnemiesPossibleName { get; }
}
