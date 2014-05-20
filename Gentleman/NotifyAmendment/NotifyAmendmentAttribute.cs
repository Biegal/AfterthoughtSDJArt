using System;
using System.Collections.Generic;
using Afterthought;

namespace NotifyAmendment
{
    public class NotifyAmendmentAttribute : Attribute, IAmendmentAttribute
    {
        IEnumerable<ITypeAmendment> IAmendmentAttribute.GetAmendments(Type target)
        {
            if (target.GetInterface("System.ComponentModel.INotifyPropertyChanged") == null)
                yield return (ITypeAmendment)typeof(NotifyAmendment<>)
                                .MakeGenericType(target)
                                .GetConstructor(Type.EmptyTypes)
                                .Invoke(new object[0]);
        }
    }
}