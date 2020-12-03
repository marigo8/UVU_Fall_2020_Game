using UnityEngine;

[CreateAssetMenu]
public class IdData : ScriptableObject{

    public bool Compare(IdData compareId)
    {
        return this == compareId;
    }

    public bool Compare(IdBehaviour idBehaviour)
    {
        if (idBehaviour.id == null) return false;
        return Compare(idBehaviour.id);
    }

    public bool Compare(GameObject obj)
    {
        var idBehaviour = obj.GetComponent<IdBehaviour>();
        if(idBehaviour == null) return false; 
        return Compare(idBehaviour);
    }
    
}
