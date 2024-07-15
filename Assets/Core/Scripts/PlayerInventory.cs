using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst.CompilerServices;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


[System.Serializable]
public class Items
{
    public string Name;
    public GameObject gameObject;
	public GameObject OriginalObject;
}
public class PlayerInventory : MonoBehaviour
{
    public Items CurrentItem;
    public int inventoryItemScrollPosition = 0;

    public Transform PlayerCamera;
    [HideInInspector] public bool heldButton = false;

    public LayerMask layerMask;
    public Vector3 itemNewPos;
    private float timeButton;
	private float heldTime = 0.250f;
	[HideInInspector] public bool timeButtonStart = false;
    [HideInInspector] public bool itemDropped = false;
    Vector3 tempPos;
    public float putDistance;
    public float maxDistance = 1.8f;

    public float rotationIndex = 18f;
    public LayerMask ignoreLayer;
    public LayerMask normLayer;

    public Quaternion finalrotation;

    Renderer[] renderers;
    public Material blueGhostMaterial;
    public Material redGhostMaterial;
    private bool blueAdd;
    private bool redAdd;

    public List<Items> inventory = new List<Items>();

    public Dictionary<string, Quaternion> RotationDict = new Dictionary<string, Quaternion>();

    public void SetDictNewRotation()
    {
        GameObject g = CurrentItem.OriginalObject.gameObject;
        if(!RotationDict.ContainsKey(g.name))
        {
            RotationDict.Add(g.name, g.GetComponent<Transform>().rotation);
        }
    }
    void Start()
    {
        inventory.Add(new Items() { Name = "", gameObject = null });
        PlayerCamera = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }
    void Update()
    {
        if(!timeButtonStart)
        {
            ItemScroll();
        }
        else
        {
            ItemRotate();
        }
        
        if (Input.GetKeyUp(KeyCode.F))
        {
            ItemIteract(GetIteract(CurrentItem.gameObject));
        }

        if (!heldButton) _normalRotation = false;

        DropAndPutButton();
    }
    private void ItemScroll()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            inventoryItemScrollPosition += 1;
            if (inventoryItemScrollPosition >= inventory.Count) inventoryItemScrollPosition = 0;

            ItemSwitch();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            inventoryItemScrollPosition -= 1;
            if (inventoryItemScrollPosition < 0) inventoryItemScrollPosition = inventory.Count - 1;

            ItemSwitch();
        }
    }
    
    private bool _normalRotation = false;
    
    //RayCast
    private void ItemRayCast()
    {
        RaycastHit hit;
        if (Physics.Raycast(PlayerCamera.transform.position, PlayerCamera.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(PlayerCamera.transform.position, PlayerCamera.transform.TransformDirection(Vector3.forward) * hit.distance, Color.blue);
            tempPos = hit.point;
            putDistance = hit.distance;
        }
        else
        {
            Debug.DrawRay(PlayerCamera.transform.position, PlayerCamera.transform.TransformDirection(Vector3.forward) * 1000, Color.white);
        }
    }

    public void ItemSwitch()
	{
        if(inventoryItemScrollPosition > inventory.Count-1 || inventoryItemScrollPosition < 0)
        {
            inventoryItemScrollPosition = 0;
        }
		ChangeItem(GetIteract(CurrentItem.gameObject));
		CurrentItem = inventory[inventoryItemScrollPosition];
		PickedItem(GetIteract(CurrentItem.gameObject));
	}

	private ItemInterface GetIteract(GameObject go)
    {
		if (go != null)
		{
			if (go.TryGetComponent<ItemInterface>(out ItemInterface item))
			{
                return item;
			}
		}
        return null;
	}
	
    private void ItemIteract(ItemInterface item)
    {
        if (item == null) return;
        item.Iterction();
    } 

	
    private void ChangeItem(ItemInterface item)
    {
		if (item == null) return;
		item.ChangeItem();
    }
	private void PickedItem(ItemInterface item)
	{
		if (item == null) return;
		item.PickedItem();
	}
	private void RemoveItem(Items item)
	{
		if (item == null) return;
        GetIteract(item.gameObject).ChangeItem();
        inventoryItemScrollPosition -= 1;
        ItemSwitch();
        inventory.Remove(item);
    }

    //Ghost Effects Shaders
    private void PutGhostEffect()
    {
        if (blueAdd) { return; }
        renderers = CurrentItem.OriginalObject.GetComponentsInChildren<Renderer>();
        foreach (var renderer in renderers)
        {
            var materials = renderer.sharedMaterials.ToList();

            materials.Add(blueGhostMaterial);
            MeshRenderer mr = CurrentItem.OriginalObject.GetComponent<MeshRenderer>();
            mr.material = blueGhostMaterial;
            blueAdd = true;

            renderer.materials = materials.ToArray();
        }
        blueAdd = true;

    }
    private void PutGhostEffectOutOfRange()
    {
        if (redAdd) { return; }
        renderers = CurrentItem.OriginalObject.GetComponentsInChildren<Renderer>();
        foreach (var renderer in renderers)
        {
            var materials = renderer.sharedMaterials.ToList();

            materials.Add(redGhostMaterial);
            redAdd = true;

            renderer.materials = materials.ToArray();
        }
        redAdd = true;
    }
    private void RemoveGhostEffets(Material m)
    {
        if (!redAdd && !blueAdd) { return; }
        foreach (var renderer in renderers)
        {
            var materials = renderer.sharedMaterials.ToList();

            materials.Remove(m);
            if (m == blueGhostMaterial) { blueAdd = false; }
            if (m == redGhostMaterial) { redAdd = false; }

            renderer.materials = materials.ToArray();
        }
    }

    //Collision Settings
    private void MakeItemGhost()
    {
        Collider cd = CurrentItem.OriginalObject.GetComponent<Collider>();
        cd.excludeLayers = ignoreLayer;
        Rigidbody rb = CurrentItem.OriginalObject.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }
    private void MakeItemReal()
    {
        Collider cd = CurrentItem.OriginalObject.GetComponent<Collider>();
        cd.excludeLayers = normLayer;
    }

    //Options Interactions with item
    private void ItemDrop()
    {
        GameObject origObj = CurrentItem.OriginalObject;
        if (origObj.GetComponent<Rigidbody>() == null)
        {
            Rigidbody rr = origObj.gameObject.AddComponent<Rigidbody>();
            rr.mass = 1;
            rr.angularDamping = 1.75f;
        }
        Rigidbody rb = origObj.GetComponent<Rigidbody>();
        rb.mass = 1;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.constraints = RigidbodyConstraints.None;
        rb.angularDamping = 1.75f;
        origObj.transform.position = PlayerCamera.transform.position;
        origObj.SetActive(true);
        rb.AddForce(PlayerCamera.transform.TransformDirection(Vector3.forward), ForceMode.Impulse);
        RemoveItem(CurrentItem);
    }
    private void ItemPut()
    {
        SetItemNewPos(tempPos);
        GameObject origObj = CurrentItem.OriginalObject;
        origObj.transform.position = itemNewPos;
        origObj.SetActive(true);
    }

    //Functional Position and Changhing items param
    private void CreateBigWeight()
    {
        Rigidbody rg = CurrentItem.OriginalObject.GetComponent<Rigidbody>();
    rg.linearVelocity = Vector3.zero;
        rg.angularDamping = 50;
        rg.constraints = RigidbodyConstraints.None;
        rg.interpolation = RigidbodyInterpolation.Interpolate;
    }
    private void DetectionNewItemPos()
    {
        if (heldButton && timeButton > heldTime)
        {
            if (!_normalRotation) CurrentItem.OriginalObject.transform.rotation =
                    CurrentItem.OriginalObject.GetComponent<IPickUpInfo>().GetRotation();
            _normalRotation = true;

            MakeItemGhost();
            if (putDistance < maxDistance)
            {
                if (redAdd) { RemoveGhostEffets(redGhostMaterial); }
                PutGhostEffect();
                ItemPut();
            }
            else
            {
                if (blueAdd) { RemoveGhostEffets(blueGhostMaterial); }
                PutGhostEffectOutOfRange();
                ItemPut();
            }
        }
    }



    private void SetItemNewPos(Vector3 h)
    {
        if (CurrentItem.OriginalObject.GetComponent<BoxCollider>() != null)
        {
            itemNewPos = new Vector3(
            h.x,
            h.y,
            h.z + CurrentItem.OriginalObject.GetComponent<BoxCollider>().size.z / 2f

            );
        }
        else if (CurrentItem.OriginalObject.GetComponent<CapsuleCollider>() != null)
        {
            itemNewPos = new Vector3(
            h.x,
            h.y + CurrentItem.OriginalObject.GetComponent<CapsuleCollider>().radius,
            h.z
            );
        }
        else if (CurrentItem.OriginalObject.GetComponent<SphereCollider>() != null)
        {
            itemNewPos = new Vector3(
            h.x,
            h.y + CurrentItem.OriginalObject.GetComponent<SphereCollider>().radius,
            h.z
            );
        }
    }

    //Rotation Item
    private void ItemRotate()
    {
        Rigidbody origObjrg = CurrentItem.OriginalObject.GetComponent<Rigidbody>();
        origObjrg.interpolation = RigidbodyInterpolation.None;
        origObjrg.constraints = RigidbodyConstraints.FreezeRotationY;
        Transform origObjTrans = CurrentItem.OriginalObject.transform;
        if (Input.mouseScrollDelta.y != 0)
        {
            origObjTrans.Rotate(Vector3.up * Input.mouseScrollDelta.y * rotationIndex, Space.World);
        }
    }

    //Final Realisation
    private void PutAndDropItem()
    {
        RemoveGhostEffets(blueGhostMaterial);
        RemoveGhostEffets(redGhostMaterial);
        MakeItemReal();
        timeButtonStart = false;
        if (timeButton < heldTime)
        {
            itemDropped = true;
            ItemDrop();
            timeButton = 0;
        }
        else
        {
            if (putDistance < maxDistance)
            {
                if (!itemDropped)
                {
                    CreateBigWeight();
                }
                RemoveItem(CurrentItem);
            }
            else { Debug.Log("Too far"); CurrentItem.OriginalObject.SetActive(false); }
            timeButton = 0;
        }
        itemDropped = false;
        heldButton = false;
    }
    private void DropAndPutButton()
    {
        if (timeButtonStart)
        {
            timeButton += Time.deltaTime;
        }

        if (CurrentItem.gameObject == null) return;
        else
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                timeButtonStart = true;
                heldButton = true;

            }
            else if (Input.GetKeyUp(KeyCode.G))
            {
                PutAndDropItem();
            }
        }
        ItemRayCast();
        DetectionNewItemPos();
    }
}
