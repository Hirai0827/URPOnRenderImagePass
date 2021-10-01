using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace URPOnRenderImagePass
{
    public class DrawOnRenderImagePass:ScriptableRendererFeature
    {
        private OnRenderImagePass pass;
        public override void Create()
        {
            pass = new OnRenderImagePass();
        }

        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            renderer.EnqueuePass(pass);
        }
    }
}