using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectionButton : MonoBehaviour
{
    private Image collectionImage;

    private void Awake()
    {        
        collectionImage = this.GetComponent<Image>();        
    }       // Awake()

    public void ClickCollectionButton()
    {        
        LobbyManager.Instance.OpenCollection();
        // 카드가 들어있는 UI들을 오픈하여 덱을 만들 수 있음

    }       // ClickCollectionButton()

}       // CollectionButton Class
