using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Auboreal.Core.EventSystem;

namespace Auboreal.Unity.EventSystem.UnitTest
{
    [TestFixture]
    public class EventDirectoryTests
    {
        [Test]
        public void ForwardsConnection()
        {
            EventDirectory dir0 = new EventDirectory();
            EventDirectory dir1 = new EventDirectory();

            dir0.ConnectForwards(dir1);

            CollectionAssert.Contains(dir0.Next, dir1);
            CollectionAssert.DoesNotContain(dir1.Previous, dir0);
        }

        [Test]
        public void ForwardsDisconnection()
        {
            EventDirectory dir0 = new EventDirectory();
            EventDirectory dir1 = new EventDirectory();

            dir0.ConnectForwards(dir1);
            dir0.DisconnectForwards(dir1);

            CollectionAssert.DoesNotContain(dir0.Next, dir1);
        }

        [Test]
        public void BackwardsConnection()
        {
            EventDirectory dir0 = new EventDirectory();
            EventDirectory dir1 = new EventDirectory();

            dir0.ConnectBackwards(dir1);

            CollectionAssert.Contains(dir0.Previous, dir1);
            CollectionAssert.DoesNotContain(dir1.Next, dir0);
        }

        [Test]
        public void BackwardsDisconnection()
        {
            EventDirectory dir0 = new EventDirectory();
            EventDirectory dir1 = new EventDirectory();

            dir0.ConnectBackwards(dir1);
            dir0.DisconnectBackwards(dir1);

            CollectionAssert.DoesNotContain(dir0.Previous, dir1);
        }

        [Test]
        public void BidirectionalConnection()
        {
            EventDirectory dir0 = new EventDirectory();
            EventDirectory dir1 = new EventDirectory();

            dir0.ConnectBidirectional(dir1);

            CollectionAssert.Contains(dir0.Next, dir1);
            CollectionAssert.Contains(dir1.Previous, dir0);
        }

        [Test]
        public void BidirectionalDisconnection()
        {
            EventDirectory dir0 = new EventDirectory();
            EventDirectory dir1 = new EventDirectory();

            dir0.ConnectBidirectional(dir1);
            dir0.DisconnectBidirectional(dir1);

            CollectionAssert.DoesNotContain(dir0.Next, dir1);
            CollectionAssert.DoesNotContain(dir1.Previous, dir0);
        }


    }
}