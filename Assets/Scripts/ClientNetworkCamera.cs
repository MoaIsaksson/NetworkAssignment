using Unity.Netcode.Components;

public class ClientNetworkCamera : NetworkTransform
{
    protected override bool OnIsServerAuthoritative()
    {
            return false;
    }
}
