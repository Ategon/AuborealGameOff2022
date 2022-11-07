using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Auboreal.Core.EventSystem;

namespace Auboreal.Unity.EventSystem.UnitTest.EventCarrier
{
    [TestFixture]
    public class ForwardsPreOrderEventCarrierTest
    {
        [Test]
        public void Parent_With_Two_Children_Traversal()
        {
            EventDirectory dir0 = new EventDirectory();
            EventDirectory dir1 = new EventDirectory();
            EventDirectory dir2 = new EventDirectory();

            dir0.ConnectBidirectional(dir1);
            dir0.ConnectBidirectional(dir2);

            ForwardsPreOrderEventCarrier carrier = new ForwardsPreOrderEventCarrier();

            List<IEventDirectory> order = carrier.Traverse(dir0).ToList();

            CollectionAssert.AreEqual(order, new List<IEventDirectory> { dir0, dir1, dir2 });
        }

        [Test]
        public void Two_Parent_With_Child_Traversal()
        {
            EventDirectory dir0 = new EventDirectory();
            EventDirectory dir1 = new EventDirectory();
            EventDirectory dir2 = new EventDirectory();

            dir1.ConnectBidirectional(dir0);
            dir2.ConnectBidirectional(dir0);

            ForwardsPreOrderEventCarrier carrier = new ForwardsPreOrderEventCarrier();

            List<IEventDirectory> order0 = carrier.Traverse(dir1).ToList();
            List<IEventDirectory> order1 = carrier.Traverse(dir2).ToList();

            CollectionAssert.AreEqual(order0, new List<IEventDirectory> { dir1, dir0 });
            CollectionAssert.AreEqual(order1, new List<IEventDirectory> { dir2, dir0 });
        }
    }

    [TestFixture]
    public class BackwardsPreOrderEventCarrierTest
    {
        [Test]
        public void Parent_With_Two_Children_Traversal()
        {
            EventDirectory dir0 = new EventDirectory();
            EventDirectory dir1 = new EventDirectory();
            EventDirectory dir2 = new EventDirectory();

            dir0.ConnectBidirectional(dir1);
            dir0.ConnectBidirectional(dir2);

            BackwardsPreOrderEventCarrier carrier = new BackwardsPreOrderEventCarrier();

            List<IEventDirectory> order0 = carrier.Traverse(dir1).ToList();
            List<IEventDirectory> order1 = carrier.Traverse(dir2).ToList();

            CollectionAssert.AreEqual(order0, new List<IEventDirectory> { dir1, dir0 });
            CollectionAssert.AreEqual(order1, new List<IEventDirectory> { dir2, dir0 });
        }

        [Test]
        public void Two_Parent_With_Child_Traversal()
        {
            EventDirectory dir0 = new EventDirectory();
            EventDirectory dir1 = new EventDirectory();
            EventDirectory dir2 = new EventDirectory();

            dir1.ConnectBidirectional(dir0);
            dir2.ConnectBidirectional(dir0);

            BackwardsPreOrderEventCarrier carrier = new BackwardsPreOrderEventCarrier();

            List<IEventDirectory> order = carrier.Traverse(dir0).ToList();

            CollectionAssert.AreEqual(order, new List<IEventDirectory> { dir0, dir1, dir2 });
        }
    }
}