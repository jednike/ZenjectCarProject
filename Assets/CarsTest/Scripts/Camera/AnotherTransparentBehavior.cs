using System.Collections.Generic;
using UnityEngine;

namespace CarsTest
{
    public class AnotherTransparentBehavior: MonoBehaviour
    {
        private struct RendererItem
        {
            public Renderer renderer;
            public Shader sourceShader;
        }
    
        private Shader _transparentShader;
        private List<RendererItem> _rendererItems;
    
        private bool _needTransparency;
        public bool NeedTransparency
        {
            get => _needTransparency;
            set
            {
                _needTransparency = value;
                foreach (var rendererItem in _rendererItems)
                {
                    rendererItem.renderer.material.shader =
                        _needTransparency ? _transparentShader : rendererItem.sourceShader;
                }
            }
        }

        private void Awake()
        {
            _rendererItems = new List<RendererItem>();
        
            foreach (var rend in gameObject.GetComponentsInChildren<Renderer>())
            {
                _rendererItems.Add(new RendererItem{ renderer = rend, sourceShader = rend.material.shader });
            }
        }

        private void Start()
        {
            NeedTransparency = false;
        }
    }
}
