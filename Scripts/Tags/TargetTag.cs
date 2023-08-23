using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TargetTagOptions : int
{
    Sign1Sphere,
    Sign2Sphere,
    RopeSphere,
    BlueTarget,
    None
}

public class TargetTag : MonoBehaviour
{
    [SerializeField]
    public List<TargetTagOptions> tags;

    private void Awake()
    {
    }

    public bool HasTag(TargetTagOptions tag)
    {
        return tags.Contains(tag);
    }

    public IEnumerable<TargetTagOptions> GetTags()
    {
        foreach (TargetTagOptions tag in tags)
        {
            yield return tag;
        }
    }

    public void Rename(TargetTagOptions newTag, int index)
    {
        tags[index] = newTag;
    }

    public TargetTagOptions GetAtIndex(int index)
    {
        return tags[index];
    }

    public int Count => tags.Count;

}
