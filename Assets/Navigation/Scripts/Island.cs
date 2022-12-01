using System.Collections;
using System.Collections.Generic;
using Assets.EventSystem;
using Assets.Player.Inventory;
using Assets.Player.Thirst;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

namespace Assets.Navigation
{
    public class Island : MonoBehaviour
    {
        [Header("Island Information")]
        public string islandName;
        public EquipmentType equipmentType;
        public int waterAmount;
        public int woodAmount;
        public int treasureAmount;
        [Header("References")]
        public DockPoint dockPoint;
        [SerializeField] private IslandClickedEvent islandClickedEvent;
        [SerializeField] private IslandMouseEnter islandMouseEnter;
        [SerializeField] private IslandMouseExit islandMouseExit;
        [SerializeField] private ThirstController thirstController;
        [SerializeField] private PlayerLocationController playerLocationController;
        [SerializeField] private InventoryController inventoryController;
        [SerializeField] private string loadedSceneName;
        [Header("Visual Trail")]
        [SerializeField] private RouteTrail routeTrailPrefab;
        private List<RouteTrail> routeTrails;
        [Header("Routes to Other Islands")]
        public List<Route> routes;
        public bool isBoatDockedHere { get; private set; }
        public bool isBoatTraveling { get; private set; }
        private Boat boat;
        private void OnEnable()
        {
            islandClickedEvent.AddListener(OnIslandClick);
            islandMouseEnter.AddListener(OnIslandEnter);
            islandMouseExit.AddListener(OnIslandExit);
        }
        private void OnDisable()
        {
            islandClickedEvent.RemoveListener(OnIslandClick);
            islandMouseEnter.RemoveListener(OnIslandEnter);
            islandMouseExit.RemoveListener(OnIslandExit);
        }
        public void InitializeBoat(Boat boat)
        {
            isBoatDockedHere = true;
            this.boat = boat;
            CreateRouteTrails();
        }

        [SerializeField] private Transform[] exitObjects;
        private bool entered = false;

        public void DockBoat()
        {
            if (entered) return;
            entered = true;
            playerLocationController.islandName = islandName;
            StartCoroutine(swapScene());
        }

        IEnumerator swapScene()
        {
            {
                yield return new WaitForSeconds(0.5f);
                for (int i = 0; i < exitObjects.Length; i++)
                {
                    exitObjects[i].DOLocalMove(Vector3.zero, 2f).SetEase(Ease.OutQuad);
                }
                yield return new WaitForSeconds(2f);
                SceneManager.LoadScene(loadedSceneName);
            }
        }
        
        public int GetThirstCostFromDocked()
        {
            foreach (Route route in routes)
            {
                if (route.destination.isBoatDockedHere)
                {
                    return route.thirstCost;
                }
            }
            return 0;
        }
        private void CreateRouteTrails()
        {
            routeTrails = new List<RouteTrail>();
            foreach (Route route in routes)
            {
                if (!route.destination.isBoatDockedHere)
                { 
                    RouteTrail routeTrail = Instantiate(routeTrailPrefab, transform.position, Quaternion.identity);
                    routeTrail.CreateRouteDots(transform.position, route.destination.transform.position);
                    routeTrail.CreateCostText(transform.position, route.destination.transform.position, route.thirstCost);
                    routeTrail.destination = route.destination;
                    routeTrails.Add(routeTrail);
                }
            }
        }
        // This is a listener to the Island Clicked event: it is called when
        // any island is clicked.
        public void OnIslandClick(object sender, EventParameters arg2)
        {
            if (isBoatDockedHere)
            {
                foreach (Route route in routes)
                {
                    if (ReferenceEquals(sender, route.destination))
                    {
                        SendBoat(route);
                        return;
                    }
                }
            }
        }
        private void SendBoat(Route route)
        {
            isBoatDockedHere = false;
            boat.SetDestination(route.destination);
            DeleteRouteTrails();
            thirstController.ChangeThirst(-route.thirstCost);
        }
        public void OnIslandEnter(object sender, EventParameters arg2)
        {
            if (routeTrails != null)
            {
                foreach (RouteTrail routeTrail in routeTrails)
                {
                    if (ReferenceEquals(sender, routeTrail.destination))
                    {
                        routeTrail.Hover();
                    }
                }
            }
            if (!isBoatDockedHere & inventoryController.nauticalChartOwned & ReferenceEquals(sender, this))
            {
                CreateRouteTrails();
            }
        }
        public void OnIslandExit(object sender, EventParameters arg2)
        {
            if (routeTrails != null)
            {
                routeTrails.ForEach(routeTrail => { routeTrail.StopHover(); });
            }
            if (!isBoatDockedHere & inventoryController.nauticalChartOwned & routeTrails != null)
            {
                DeleteRouteTrails();
            }
        }
        private void DeleteRouteTrails()
        {
            routeTrails.ForEach(routeTrail => { Destroy(routeTrail.gameObject); });
            routeTrails = null;
        }
        public void RaiseIslandClick()
        {
            islandClickedEvent.Raise(this, null);
        }
        public void RaiseIslandMouseEnter()
        {
            islandMouseEnter.Raise(this, null);
        }
        public void RaiseIslandMouseExit()
        {
            islandMouseExit.Raise(this, null);
        }
    }
}