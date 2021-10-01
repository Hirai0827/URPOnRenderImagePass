using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace URPOnRenderImagePass
{
    public class Vignette : MonoBehaviour,IPostProcessor
    {
        [SerializeField] private Material vignette;
        [Range(0,10)]
        [SerializeField] private float val;
        // Start is called before the first frame update
        void Awake()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void OnPostProcess(CommandBuffer cmd, RenderTargetHandle src, RenderTargetHandle dest,RenderingData renderingData)
        {
            cmd.SetGlobalFloat("vignetteVal",val);
            cmd.Blit(src.Identifier(),dest.Identifier(),vignette);
        }
    }
}

