namespace HslStudio.ClassLibrary.Interface
{
    public interface IDriverAdapter
    {

        bool IsConnected { get; set; }

        bool Connect();

        bool Disconnect();
        TValue[] Read<TValue>(object address, ushort length);
        TValue Read<TValue>(string address);
        bool Write(string address, dynamic value);

    }
}
