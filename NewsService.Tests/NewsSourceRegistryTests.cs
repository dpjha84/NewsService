using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NewsService.Aggregators;
using NewsService.Controllers;
using NewsService.Models;
using NewsService.Paging;
using NewsService.Sources;
using NUnit.Framework;

namespace NewsService.Tests
{
    [TestFixture]
    public class NewsSourceRegistryTests
    {
        [Test]
        public void Verify_Source_Is_Unregistered_Before_Registration()
        {
            var registry = new NewsSourceRegistry<NewsSource>();
            Assert.True(!registry.IsRegistered("source1"));
        }

        [Test]
        public void Verify_Source_Is_Registered_On_Registration()
        {
            var registry = new NewsSourceRegistry<NewsSource>();
            var source = registry.Register("source1");
            Assert.True(registry.IsRegistered("source1"));
            Assert.True(registry.IsRegistered(source.Id));
        }

        [Test]
        public void Verify_Source_Is_Unregistered_On_Unregistration()
        {
            var registry = new NewsSourceRegistry<NewsSource>();
            var source = registry.Register("source1");
            registry.Unregister("source1");
            Assert.True(!registry.IsRegistered("source1"));
            Assert.True(!registry.IsRegistered(source.Id));
        }

        [Test]
        public void Verify_Source_Is_Retrieved_After_Registration()
        {
            var registry = new NewsSourceRegistry<NewsSource>();
            var registeredSource = registry.Register("source1");
            var retrivedSource = registry.GetSource(registeredSource.Id);
            Assert.AreEqual(registeredSource, retrivedSource);
        }

        [Test]
        public void Verify_Sources_Are_Retrieved_After_Registration()
        {
            var registry = new NewsSourceRegistry<NewsSource>();
            var source1 = registry.Register("source1");
            var source2 = registry.Register("source2");
            var source3 = registry.Register("source3");
            var retrivedSources = registry.GetNewsSources().ToList();
            Assert.AreEqual(retrivedSources[0], source1);
            Assert.AreEqual(retrivedSources[1], source2);
            Assert.AreEqual(retrivedSources[2], source3);
        }

        [Test]
        public void Verify_Register_Event_Is_Raised_On_Registration()
        {
            var eventRaised = false;
            var registry = new NewsSourceRegistry<NewsSource>();
            registry.OnSourceRegistered += (o, e) => eventRaised = true;
            registry.Register("source");
            Assert.True(eventRaised);
        }

        [Test]
        public void Verify_Register_Event_Is_Not_Raised_On_Old_Registration()
        {
            var eventRaised = false;
            var registry = new NewsSourceRegistry<NewsSource>();
            registry.Register("source");

            registry.OnSourceRegistered += (o, e) => eventRaised = true;
            registry.Register("source");
            Assert.True(!eventRaised);
        }

        [Test]
        public void Verify_Unregister_Event_Is_Raised_On_Successful_Unregistration()
        {
            var eventRaised = false;
            var registry = new NewsSourceRegistry<NewsSource>();
            registry.Register("source");
            registry.OnSourceUnregistered += (o, e) => eventRaised = true;
            registry.Unregister("source");
            Assert.True(eventRaised);
        }

        [Test]
        public void Verify_Unregister_Event_Is_Not_Raised_Without_Unregistration()
        {
            var eventRaised = false;
            var registry = new NewsSourceRegistry<NewsSource>();
            registry.OnSourceUnregistered += (o, e) => eventRaised = true;
            registry.Unregister("source");
            Assert.True(!eventRaised);
        }
    }
}
