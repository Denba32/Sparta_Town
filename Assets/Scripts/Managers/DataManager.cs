using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class DataManager
{
    private Dictionary<string, SpriteLibraryAsset> characterDict = new Dictionary<string, SpriteLibraryAsset>();
    public void Init()
    {
        SpriteLibraryAsset[] assets = Managers.Resource.LoadAll<SpriteLibraryAsset>("Data/Character");
        if(assets != null)
        {
            for(int i = 0; i <  assets.Length; i++)
            {
                characterDict.Add(assets[i].name, assets[i]);
            }
        }
    }

    #region ========== Character Options ==========

    public string[] GetCharacterNames()
    {
        return characterDict.Keys.ToArray();
    }

    public SpriteLibraryAsset FindCharacter(string name)
    {
        if(characterDict.TryGetValue(name, out SpriteLibraryAsset assets))
        {
            return assets;
        }
        return null;
    }

    public int GetCharacterCount()
    {
        return characterDict.Count;
    }

    #endregion
}
