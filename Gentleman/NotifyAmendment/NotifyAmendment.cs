using System.ComponentModel;
using Afterthought;

namespace NotifyAmendment
{
    public class NotifyAmendment<T> : Amendment<T, INotifyPropertyChangedRaiser>
    {
        public NotifyAmendment()
        {
            // Dodajemy definicję zdarzenia, zadanego typu i nazwie
            var propertyChanged = Events.Add<PropertyChangedEventHandler>("PropertyChanged");

            // Implementujemy interfejs, przekazując listę elementów, które go implmentują.
            // W naszym przypadku jest to właśnie zdefiniowane zdarzenie propertyChanged
            Implement<INotifyPropertyChanged>(propertyChanged);

            // definiujemy metodę o nazwie OnPropertyChanged, która zgłasza zdarzenie zdefiniowane
            // w zmiennej propertyChanged
            var onChangeMethod = Methods.Raise(propertyChanged, "OnPropertyChanged");

            // Implementujemy interfejs INotifyPropertyChanged, przekazując właśnie dodaną metodę
            // o nazwie OnPropertyChanged
            Implement<INotifyPropertyChangedRaiser>(onChangeMethod);

            // Ze wszystkich właściwości w modyfikowanym typie, znajdujemy te, które możemy odczytać i zapisać
            // oraz które mają publiczny setter.
            // Po setterze, wywołujemy zadany delegat (w tym przypadku ze statycznej klasy)
            Properties
                .Where(p => p.PropertyInfo.CanRead && p.PropertyInfo.CanWrite && p.PropertyInfo.GetSetMethod().IsPublic)
                .AfterSet(RaisePropertyChangedHelper.RaisePropertyChanged);
        }
    }

    public static class RaisePropertyChangedHelper
    {
        /// <summary>
        /// Metoda pomocnicza która odpowiada delegatowi 'AfterSet'.
        /// Dzięki takiej konstrukcji, nie 'doklejamy' do naszej aplikacji klasy dziedziczącej z Amendement,
        /// a tym samym unikamy bezpośredniej zależności do Afterthought
        /// </summary>
        public static void RaisePropertyChanged<P>(INotifyPropertyChangedRaiser instance, string property, P oldValue, P value, P newValue)
        {
            // ten kod możemy urozmaicić np. sprawdzając, czy wartość rzeczywiście się zmieniła
            instance.OnPropertyChanged(new PropertyChangedEventArgs(property));
        }
    }
}
