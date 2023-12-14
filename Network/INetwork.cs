namespace Network
{
    /// <summary>
    /// 네트워크 연결이 되거나 해제됐을때 작동시킬라고 추가해줌.
    /// </summary>
    public interface INetwork
    {
        void D_ConnectEvent(bool connect);
    }
}
