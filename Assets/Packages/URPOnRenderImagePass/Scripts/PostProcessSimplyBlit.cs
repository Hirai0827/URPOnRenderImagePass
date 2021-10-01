using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine;
namespace URPOnRenderImagePass
{
    public class PostProcessSimplyBlit:MonoBehaviour,IPostProcessor
    {
        [SerializeField] private Material blitMat;
        public void OnPostProcess(CommandBuffer cmd,RenderTargetHandle src,RenderTargetHandle dest,RenderingData renderingData)
        {
            cmd.Blit(src.Identifier(),dest.Identifier(),blitMat);
        }
    }
}