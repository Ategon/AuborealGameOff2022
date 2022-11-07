using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Auboreal.Unity.EventSystem.UnitTests
{
    [TestFixture]
    public class SceneTreeEventNodeTests
    {
        [UnityTest]
        public IEnumerator Single_Initialization()
        {
            GameObject obj0 = new GameObject();
            obj0.AddComponent<SceneTreeEventNode>();

            yield return new WaitForEndOfFrame(); //Awake
            yield return new WaitForEndOfFrame(); //Start

            SceneTreeEventNode node0 = obj0.GetComponent<SceneTreeEventNode>();

            Assert.IsNotNull(node0.Directory);
            CollectionAssert.IsEmpty(node0.Directory.Next);
            CollectionAssert.IsEmpty(node0.Directory.Previous);

            GameObject.Destroy(obj0);
        }

        [UnityTest]
        public IEnumerator Single_NextDirectory()
        {
            GameObject obj0 = new GameObject();
            obj0.AddComponent<SceneTreeEventNode>();

            yield return new WaitForEndOfFrame(); //Awake
            yield return new WaitForEndOfFrame(); //Start

            SceneTreeEventNode node0 = obj0.GetComponent<SceneTreeEventNode>();
            Assert.IsNull(node0.GetNextDirectory());

            GameObject.Destroy(obj0);
        }

        [UnityTest]
        public IEnumerator Parent_With_Child_NextDirectory()
        {
            GameObject obj0 = new GameObject();
            GameObject obj1 = new GameObject();

            obj0.AddComponent<SceneTreeEventNode>();
            obj1.AddComponent<SceneTreeEventNode>();

            obj1.transform.parent = obj0.transform;

            yield return new WaitForEndOfFrame(); //Awake
            yield return new WaitForEndOfFrame(); //Start

            SceneTreeEventNode node0 = obj0.GetComponent<SceneTreeEventNode>();
            SceneTreeEventNode node1 = obj1.GetComponent<SceneTreeEventNode>();

            Assert.AreEqual(node1.GetNextDirectory(), node0, "Child finds next Directory.");

            GameObject.Destroy(obj0);
            GameObject.Destroy(obj1);
        }

        [UnityTest]
        public IEnumerator Parent_With_Grandchild_NextDirectory()
        {
            GameObject obj0 = new GameObject();
            GameObject obj1 = new GameObject();
            GameObject obj2 = new GameObject();

            obj0.AddComponent<SceneTreeEventNode>();
            obj2.AddComponent<SceneTreeEventNode>();

            obj1.transform.parent = obj0.transform;
            obj2.transform.parent = obj1.transform;

            yield return new WaitForEndOfFrame(); //Awake
            yield return new WaitForEndOfFrame(); //Start

            SceneTreeEventNode node0 = obj0.GetComponent<SceneTreeEventNode>();
            SceneTreeEventNode node1 = obj2.GetComponent<SceneTreeEventNode>();

            Assert.AreEqual(node1.GetNextDirectory(), node0, "Grandchild finds next Directory.");

            GameObject.Destroy(obj0);
            GameObject.Destroy(obj1);
            GameObject.Destroy(obj2);
        }

        [UnityTest]
        public IEnumerator Parent_With_Two_Children_Connection()
        {
            GameObject obj0 = new GameObject();
            GameObject obj1 = new GameObject();
            GameObject obj2 = new GameObject();

            obj0.AddComponent<SceneTreeEventNode>();
            obj1.AddComponent<SceneTreeEventNode>();
            obj2.AddComponent<SceneTreeEventNode>();

            obj1.transform.parent = obj0.transform;
            obj2.transform.parent = obj0.transform;

            yield return new WaitForEndOfFrame(); //Awake
            yield return new WaitForEndOfFrame(); //Start

            SceneTreeEventNode node0 = obj0.GetComponent<SceneTreeEventNode>();
            SceneTreeEventNode node1 = obj1.GetComponent<SceneTreeEventNode>();
            SceneTreeEventNode node2 = obj2.GetComponent<SceneTreeEventNode>();

            CollectionAssert.Contains(node0.Directory.Next, node1.Directory, "Is Child1 the next Directory from Parent.");
            CollectionAssert.Contains(node0.Directory.Next, node2.Directory, "Is Child2 the next Directory from Parent.");

            CollectionAssert.Contains(node1.Directory.Previous, node0.Directory, "Is Parent the previous Directory from Child1.");
            CollectionAssert.Contains(node2.Directory.Previous, node0.Directory, "Is Parent the previous Directory from Child2.");

            GameObject.Destroy(obj0);
            GameObject.Destroy(obj1);
            GameObject.Destroy(obj2);
        }
    }
}