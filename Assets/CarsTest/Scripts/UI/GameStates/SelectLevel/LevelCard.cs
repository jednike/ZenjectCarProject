using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CarsTest
{
    public class LevelCard : MonoBehaviour
    {
        [SerializeField] private GameObject activePanel;
        [SerializeField] private Image portraitImage;
        [SerializeField] private Text nameText;

        public int Index { get; private set; }
    
        public void SetInfo(SceneInfo levelInfo, int index, Action<LevelCard> actionOnClick)
        {
            Index = index;

            portraitImage.sprite = levelInfo.Image;
            nameText.text = levelInfo.Name;
        
            GetComponent<Button>().onClick.AddListener(() => { actionOnClick(this); });
        }
    
        public void Select(bool select)
        {
            activePanel.SetActive(select);
            if(select)
                Scores.Instance.SelectedLevel = Index;
        }

        public class Factory: PlaceholderFactory<LevelCard>
        {
        }
    }
}