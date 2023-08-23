using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum CustomTagOptions
{
    DoorHandle,
    GrabCube,
    LiftGrab,
    CarabinerGrab,
    BrushGrab,
    Lever,
    Baggage,
    BaggageNpc,
    FirstNpcHand,
}

public class CustomTag : MonoBehaviour
{
    [SerializeField]
    public List<CustomTagOptions> tags;

    public int Count => tags.Count;

    private void Awake()
    {
    }

    public bool HasTag(CustomTagOptions tag)
    {
        return tags.Contains(tag);
    }

    public IEnumerable<CustomTagOptions> GetTags()
    {
        foreach (CustomTagOptions tag in tags)
        {
            yield return tag;
        }
    }

    public void Rename(CustomTagOptions newTag, int index)
    {
        tags[index] = newTag;
    }

    public CustomTagOptions GetAtIndex(int index)
    {
        return tags[index];
    }

    

}