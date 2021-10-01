using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace URPOnRenderImagePass
{
    public class OnRenderImagePass:ScriptableRenderPass
    {

        private RenderTargetHandle src;
        private RenderTargetHandle dest;
        private RenderTargetHandle defaultTarget;

        public OnRenderImagePass()
        {
            renderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing;
            src.Init("postprocess_src");
            dest.Init("postprocess_dest");
        }
        
        public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
        {
            //TODO srcとdest用のRenderTargetを作成する
            var desc = cameraTextureDescriptor;
            cmd.GetTemporaryRT(src.id,desc);
            cmd.GetTemporaryRT(dest.id,desc);
            
        }

        public override void FrameCleanup(CommandBuffer cmd)
        {
            //TODO RenderTargetのクリア
            cmd.ReleaseTemporaryRT(src.id);
            cmd.ReleaseTemporaryRT(dest.id);
            
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            var cameraData = renderingData.cameraData;
            var camera = cameraData.camera;
            var postProcessors = camera.GetComponents<IPostProcessor>();
            var cmd = CommandBufferPool.Get();
            defaultTarget = new RenderTargetHandle("_CameraColorTexture");
            cmd.Blit(defaultTarget.Identifier(),src.Identifier());
            foreach (var postProcessor in postProcessors)
            {
                postProcessor.OnPostProcess(cmd,src,dest,renderingData);
                cmd.Blit(dest.Identifier(),src.Identifier());
            }
            cmd.Blit(src.Identifier(),defaultTarget.Identifier());
            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }
    }
}