using System;
using System.Collections.Generic;
using System.Linq;
using TheFipster.Minecraft.Meta.Abstractions;
using TheFipster.Minecraft.Meta.Domain;

namespace TheFipster.Minecraft.Meta.Services
{
    public class TimingBlacklistDecorator : ITimingFinder
    {
        private readonly ITimingFinder _component;
        private readonly ITimingBlacklister _blacklister;

        public TimingBlacklistDecorator(ITimingFinder component, ITimingBlacklister blacklister)
        {
            _component = component;
            _blacklister = blacklister;
        }

        public Dictionary<MetaFeatures, IEnumerable<RunMeta<int>>> Get()
        {
            var result = _component.Get();
            var keys = result.Select(x => x.Key).ToList();
            foreach (var key in keys)
                result[key] = sanitize(result[key]);

            return result;
        }

        public Dictionary<MetaFeatures, IEnumerable<RunMeta<int>>> Get(DateTime inclusiveStart, DateTime exclusiveEnd)
        {
            var result = _component.Get(inclusiveStart, exclusiveEnd);
            var keys = result.Select(x => x.Key).ToList();
            foreach (var key in keys)
                result[key] = sanitize(result[key]);

            return result;
        }

        public IEnumerable<RunMeta<int>> Get(MetaFeatures feature)
        {
            var result = _component.Get(feature);
            return sanitize(result);
        }

        public IEnumerable<RunMeta<int>> Get(MetaFeatures feature, DateTime inclusiveStart, DateTime exclusiveEnd)
        {
            var result = _component.Get(feature, inclusiveStart, exclusiveEnd);
            return sanitize(result);
        }

        private IEnumerable<RunMeta<int>> sanitize(IEnumerable<RunMeta<int>> timings)
        {
            foreach (var item in timings)
                if (!_blacklister.IsBlacklisted(item.Worldname))
                    yield return item;
        }
    }
}
