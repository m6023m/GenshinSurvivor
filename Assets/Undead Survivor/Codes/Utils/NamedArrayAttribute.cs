using UnityEngine;

public class NamedArrayAttribute : PropertyAttribute
{
    public string[] names;

    public NamedArrayAttribute(params string[] names)
    {
        this.names = names;
    }
}
