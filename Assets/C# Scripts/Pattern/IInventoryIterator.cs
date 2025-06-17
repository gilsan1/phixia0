using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventoryIterator<T>
{
    bool HasNext();
    T Next();
}
