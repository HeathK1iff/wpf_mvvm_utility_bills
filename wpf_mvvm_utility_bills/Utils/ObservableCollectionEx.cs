using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace wpf_mvvm_utility_bills.Utils
{
    public class ObservableCollectionEx<T> : ObservableCollection<T>
    {
        public ObservableCollectionEx() { }

        public ObservableCollectionEx(List<T> list) : base(list) { }

        public ObservableCollectionEx(IEnumerable<T> collection) : base(collection) { }

        public void NotifyItemChanged()
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
    }
}
