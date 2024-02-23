using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseItem
{
    public GameObject itemIcon;
    public ItemSlot slotClicked;
    public ItemSlot slotHovered;
}

public class InventoryHUD : MonoBehaviour
{
    public SO_Inventory playerInventory;
    public SO_Inventory npcInventory;
    public GameObject itemPanel;
    public GameObject npcItemPanel;
    MouseItem mouse = new MouseItem();
    
    public Dictionary<GameObject, ItemSlot> GetSlotByIcon;

    public TextMeshProUGUI npcCoins;
    public TextMeshProUGUI playerCoins;
    public EquipEvent OnChangeEquipment;
    public Image equipSprite;

    // Start is called before the first frame update
    private void Start()
    {
        DrawInventory();
    }

    void DrawInventory()
    {
        GetSlotByIcon = new Dictionary<GameObject, ItemSlot>();

        foreach (ItemSlot slot in playerInventory.itemList)
        {
            GameObject new_icon = Instantiate(slot.item.icon, itemPanel.transform);
            new_icon.GetComponentInChildren<TextMeshProUGUI>().text = slot.amount.ToString();
            GetSlotByIcon.Add(new_icon, slot);
            AddTriggerEvent(new_icon, EventTriggerType.PointerEnter,delegate { OnEnter(new_icon);});
            AddTriggerEvent(new_icon, EventTriggerType.PointerExit,delegate { OnExit(new_icon);});
            AddTriggerEvent(new_icon, EventTriggerType.BeginDrag,delegate { OnStartDrag(new_icon);});
            AddTriggerEvent(new_icon, EventTriggerType.Drag,delegate { OnDrag(new_icon);});
            AddTriggerEvent(new_icon, EventTriggerType.EndDrag,delegate { OnStopDrag(new_icon);});

            new_icon.GetComponent<Button>().onClick.AddListener(delegate {OnClick(slot, npcInventory , playerInventory);});
        }

        playerCoins.text = playerInventory.coins.ToString();
    }

    public void DrawNPCInventory()
    {
        foreach (Transform child in npcItemPanel.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (ItemSlot slot in npcInventory.itemList)
        {
            GameObject new_icon = Instantiate(slot.item.icon, npcItemPanel.transform);

            new_icon.GetComponentInChildren<TextMeshProUGUI>().text = slot.amount.ToString();

            new_icon.GetComponent<Button>().onClick.AddListener(delegate {OnClick(slot, playerInventory , npcInventory);});
        }

        npcCoins.text = npcInventory.coins.ToString();
    }

    public void UpdateInventory()
    {
        foreach (Transform child in itemPanel.transform)
        {
            Destroy(child.gameObject);
        }

        DrawInventory();
    }

    void AddTriggerEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = type;
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }

    public void OnEnter (GameObject obj) //quando o ponteiro passar pelo icone
    {
        if(GetSlotByIcon.ContainsKey(obj))
        {
            mouse.slotHovered = GetSlotByIcon[obj];
        }
    }

    public void OnExit (GameObject obj) //quando o ponteiro sair de cima do icone
    {
        mouse.slotHovered = null;
    }

    public void OnStartDrag (GameObject obj) //quando começamos a arrastar o icone
    {
        GameObject pointerIcon = new GameObject();
        RectTransform rect = pointerIcon.AddComponent<RectTransform>();
        rect.sizeDelta = new Vector2(32, 32);
        pointerIcon.transform.SetParent(transform);
        Image img = pointerIcon.AddComponent<Image>();
        img.sprite = obj.GetComponent<Image>().sprite;
        img.raycastTarget = false;

        mouse.itemIcon = pointerIcon;
        mouse.slotClicked = GetSlotByIcon[obj];
    }

    public void OnDrag (GameObject obj) //quando estamos arrastando o icone
    {
        if(mouse.itemIcon != null)
        {
            mouse.itemIcon.GetComponent<RectTransform>().position = Input.mousePosition;
        }
    }

    public void OnStopDrag (GameObject obj) //quando paramos de arrastar o icone
    {
        if(mouse.slotHovered != null)
        {
            playerInventory.SwapItems(mouse.slotClicked, mouse.slotHovered);
            UpdateInventory();
        }
        Destroy(mouse.itemIcon);

        mouse.itemIcon = null;
    }

    void OnClick(ItemSlot slot,SO_Inventory buyInventory, SO_Inventory sellInventory)
    {
        if(buyInventory == null || sellInventory ==  null)
        {
            ChangeEquipment(slot);
            return;
        }
        if(buyInventory.coins >= slot.item.value)
        {
            buyInventory.AddItem(slot.item, 1);
            buyInventory.SpendCoins(slot.item.value);
            sellInventory.AddCoins(slot.item.value);
            sellInventory.RemoverItem(slot.item, 1);
            UpdateInventory();
            DrawNPCInventory();
        }
    }
    public void DefineNPCInventory(SO_Inventory inventory)
    {
        npcInventory = inventory;
        if(npcInventory)
        {
            DrawNPCInventory();
            UpdateInventory();
        }
    }

    public void RemoveNPCInventory()
    {
        npcInventory = null;
    }

    void ChangeEquipment(ItemSlot slot)
    {
        if(slot.item.type == ItemType.weapon)
        {
            equipSprite.gameObject.SetActive(true);
            equipSprite.sprite = slot.item.icon.GetComponent<Image>().sprite;
            //playerInventory.RemoveItem(slot.item, 1);
            //UpdateInventory();
            OnChangeEquipment.Invoke(slot.item as SO_Weapons);
        }
    }


}
