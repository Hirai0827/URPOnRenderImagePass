# URPOnRenderImagePass



## About

URPでは`OnRenderImage`が使えないので、その代替となるPassを作成しました。

`IPostProcessor`を継承した`MonoBehaviour`をCameraのついたComponentに着けると、そのカメラのレンダリングが終わったタイミングで呼び出されるコマンドバッファを`OnPostProcess`から設定できます。

## Usage

利用しているRendererにDrawOnRenderImagePass(RendererFeature)を追加



IPostProcessorを継承したMonoBehaviourを作成し、OnPostProcessに実装を書く。`CommandBuffer cmd`に行いたい処理などを詰める



```C#
//サンプル
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
```





作成したMonoBehaviourをCameraコンポネントと同じオブジェクトに付ける



`CommandBuffer cmd`に詰められたコマンド群はポストプロセスのタイミングで実行されます（正確には`RenderPassEvent.BeforeRenderingPostProcessing`のタイミング）

## Note

レンダリングの仕様変更のためにどうしてもCommandBufferがGPUでメインスレッドとは独立に実行されることを念頭に置く必要があります。レガシーのパイプラインに比べて少し処理を書くのが難しくなった感が否めませんが、自由度はレガシーの比ではないので、うまく使いこなせるといいなぁ、と思っています…