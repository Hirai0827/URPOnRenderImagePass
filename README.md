# URPOnRenderImagePass

![image](https://user-images.githubusercontent.com/41163700/135601560-839f1508-c8ef-4999-b7b3-2b69183f2023.png)


## About

URPでは`OnRenderImage`が使えないので、その代替となるPassを作成しました。

`IPostProcessor`を継承した`MonoBehaviour`をCameraのついたComponentに着けると、そのカメラのレンダリングが終わったタイミングで呼び出されるコマンドバッファを`OnPostProcess`から設定できます。

## Usage

利用しているRendererにDrawOnRenderImagePass(RendererFeature)を追加

![image](https://user-images.githubusercontent.com/41163700/135601614-31e4c759-be36-4b82-af58-c611153c8ccb.png)


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

![image](https://user-images.githubusercontent.com/41163700/135601680-c1f812d5-0048-4768-9d59-6bf4b70d1cce.png)


`CommandBuffer cmd`に詰められたコマンド群はポストプロセスのタイミングで実行されます（正確には`RenderPassEvent.BeforeRenderingPostProcessing`のタイミング）


![image](https://user-images.githubusercontent.com/41163700/135601560-839f1508-c8ef-4999-b7b3-2b69183f2023.png)


## Note

レンダリングの仕様変更のためにどうしてもCommandBufferがGPUでメインスレッドとは独立に実行されることを念頭に置く必要があります。レガシーのパイプラインに比べて少し処理を書くのが難しくなった感が否めませんが、自由度はレガシーの比ではないので、うまく使いこなせるといいなぁ、と思っています…
