using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;


namespace Cloth
{

    public enum ClothType
    {
        COMMON,
        SPEED,
        STRONG,
        BASIC
    }
    public class ClothManager : Singleton<ClothManager>
    {
        public List<ClothSetup> clothSetups;

        public ClothSetup GetSetupByType(ClothType cloth)
        {
            return clothSetups.Find(i => i.clothType == cloth);
        }
    }

    [System.Serializable]
    public class ClothSetup
    {
        public ClothType clothType;
        public Texture2D texture;
    }
}

