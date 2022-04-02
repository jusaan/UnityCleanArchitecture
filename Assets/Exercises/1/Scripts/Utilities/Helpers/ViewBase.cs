using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityExercises.Utilities.Helpers
{
    public class ViewBase : MonoBehaviour
    {
        protected readonly List<IDisposable> _disposables = new List<IDisposable>();

        protected virtual void OnDestroy()
        {
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }
        }
    }
}