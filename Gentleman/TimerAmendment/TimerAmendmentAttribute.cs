using System;
using System.Collections.Generic;
using Afterthought;

namespace TimerAmendment
{
    public class TimerAmendmentAttribute : Attribute, IAmendmentAttribute
    {
        IEnumerable<ITypeAmendment> IAmendmentAttribute.GetAmendments(Type target)
        {
            yield return (ITypeAmendment)typeof(TimerAmendment<>)
                            .MakeGenericType(target)
                            .GetConstructor(Type.EmptyTypes)
                            .Invoke(new object[0]);

        }
    }
}
