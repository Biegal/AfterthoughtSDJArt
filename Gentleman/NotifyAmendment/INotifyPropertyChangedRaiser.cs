using System.ComponentModel;

namespace NotifyAmendment
{
    public interface INotifyPropertyChangedRaiser : INotifyPropertyChanged
    {
        void OnPropertyChanged(PropertyChangedEventArgs args);
    }
}
