namespace Business.Models
{
	public class DiceArguments
	{
		public int AmountOfDice { get; set; }
		public int DiceValue { get; set; }
		public int AbilityModifier { get; set; }
		public string ErrorMessage { get; set; }
		public bool ModifierIsNegative { get; set; }
	}
}
