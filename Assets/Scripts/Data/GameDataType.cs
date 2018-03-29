using UnityEngine;
namespace kkkkoyo.basic
{
	public class GameDataType : MonoBehaviour 
	{
		public DataType dataType = DataType.OFF;
	}
		/// <summary>
		/// OFF:
		/// ON:
		/// </summary>
		public enum DataType : int
		{
			OFF,
			ON
		}
}