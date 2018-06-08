using System.Collections.Generic;

namespace Common.Api.Builders.Dictionary
{
    public class DictionaryBuilder<TKey, TValue> : IDictionaryBuilder<TKey, TValue>
    {
        private readonly IDictionary<TKey, TValue> _dictionary;

        private DictionaryBuilder()
        {
            _dictionary = new Dictionary<TKey, TValue>();
        }

        public static DictionaryBuilder<TKey, TValue> Create()
        {
            return new DictionaryBuilder<TKey, TValue>();
        }

        public IDictionaryBuilder<TKey, TValue> Add(TKey key, TValue value)
        {
            _dictionary.Add(key, value);

            return this;
        }
            
        public IDictionary<TKey, TValue> Build()
        {
            return _dictionary;
        }
    }

    public interface IDictionaryBuilder<TKey, TValue> : IBuilder<IDictionary<TKey, TValue>>
    {
        IDictionaryBuilder<TKey, TValue> Add(TKey key, TValue value);
    }
}