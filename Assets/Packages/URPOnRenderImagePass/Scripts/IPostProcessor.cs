using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace URPOnRenderImagePass
{
    public interface IPostProcessor
    {
        void OnPostProcess(CommandBuffer cmd,RenderTargetHandle src,RenderTargetHandle dest,RenderingData renderingData);
    }
}