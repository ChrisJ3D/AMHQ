public class PickedUpItemReaction : DelayedReaction {

	public Item item;
	private InventoryManager inventory;

	protected override void SpecificInit() {
		inventory = FindObjectOfType<InventoryManager>();
	}

	protected override void ImmediateReaction() {
		inventory.AddItem(item);
	}
}
