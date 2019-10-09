using UnityEngine;
using Zenject;

namespace Baku.VMagicMirror
{
    public class VMagicMirrorInstaller : MonoInstaller
    {
        [SerializeField] private ReceivedMessageHandler messageHandler = null;
        [SerializeField] private VRMLoadController loadController = null;
        [SerializeField] private MmfServer mmfServer = null;
        
        public override void InstallBindings()
        {
            //メッセージハンドラの依存はここで注入(偽レシーバを入れたい場合、interfaceを切って別インスタンスを捻じ込めばOK)
            Container.BindInstance(messageHandler);

            //VRMLoadControllerがIVRMLoadable(VRMのロード/破棄イベント送信元)の実装を提供する
            Container
                .Bind<IVRMLoadable>()
                .FromInstance(loadController)
                .AsSingle();

            //プロセス間通信の送り手はMemoryMappedFileベースのIPCでやる
            Container
                .Bind<IMessageSender>()
                .FromInstance(mmfServer)
                .AsSingle();
        }
    }
}
