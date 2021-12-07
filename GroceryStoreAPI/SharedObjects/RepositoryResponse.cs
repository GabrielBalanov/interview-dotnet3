using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GroceryStoreAPI.SharedObjects
{
    public class RepositoryResponse<T>
    {
		/// <summary>
		/// Data Retrieved
		/// </summary>
		[JsonPropertyName("Data")]
		public T Data { get; set; }

		/// <summary>
		/// Flag for checking whether the processing has been successful
		/// </summary>
		[JsonPropertyName("IsSuccess")]
		public bool IsSuccess { get; set; } = true;

		/// <summary>
		/// Returns the message text of the error, if any occured
		/// </summary>
		[JsonPropertyName("ErrorMessage")]
		public string ErrorMessage { get; set; } = "";
	}
}
