﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace UniTube.Framework.Utils
{
    public static class IEnumerableUtils
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> list)
            => new ObservableCollection<T>(list);

        public static bool TryAdd<K, V>(this IDictionary<K, V> dictionary, K key, V value)
        {
            if (dictionary.ContainsKey(key))
            {
                return false;
            }
            else
            {
                try
                {
                    dictionary.Add(key, value);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public static int AddRange<T>(this ObservableCollection<T> list, IEnumerable<T> items, bool clearFirst = false)
        {
            if (clearFirst)
            {
                list.Clear();
            }
            foreach (var item in items)
            {
                list.Add(item);
            }
            return list.Count;
        }

        public static T AddAndReturn<T>(this IList<T> list, T item)
        {
            list.Add(item);
            return item;
        }

        public static void ForEach<T>(this IEnumerable<T> list, Action<T> action)
        {
            foreach (var item in list)
            {
                action?.Invoke(item);
            }
        }
    }
}