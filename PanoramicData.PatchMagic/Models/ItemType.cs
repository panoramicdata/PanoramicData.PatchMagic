public partial class ItemType {
	public ItemType() {
		this.deprecated = false;
	}
    
	public TextType[] title { get; set; }

	public NotesType[] notes { get; set; }

	public ReferencesTypeReference[] references { get; set; }

	public CheckType[] check { get; set; }

	public string name { get; set; }

	[System.ComponentModel.DefaultValueAttribute(false)]
	public bool deprecated { get; set; }

	public string deprecated_by { get; set; }

	public System.DateTime deprecation_date { get; set; }

	public bool deprecation_dateSpecified { get; set; }
}