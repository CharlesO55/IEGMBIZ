using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="ScriptableObjects/Tree", fileName ="Tree Preset")]
public class SO_TreeData : ScriptableObject
{
    public TreeData Template;
    public List<Sprite> Sprites;
}