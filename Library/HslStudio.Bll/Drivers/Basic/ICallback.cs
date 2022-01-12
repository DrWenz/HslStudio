namespace HslStudio.Bll.Drivers.Basic
{
    public interface ICallback
    {
        void Subscribe();
        void Unsubscribe();

        void WriteTag(string tagName, dynamic value);
    }
}
