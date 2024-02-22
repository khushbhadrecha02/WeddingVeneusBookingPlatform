using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Security.Principal;

namespace WeddingVeneus1.Areas.State.Models
{
    public class StateModel
    {
        
        public int? StateID { get; set; }
        [Required]
        public string? StateName { get; set; }
        public int? UserID { get; set; }
        public string? Email { get; set; }
    }
    public class State_DropDown_Model
    {
        public int StateID { get; set; }
        public string StateName { get; set; }
    }
	public class MST_State_SearchModel
	{

		

		public string? StateName { get; set; }
        public string? SubmitType { get; set; }
        

    }
	public class MST_State_ViewModel
	{
		public DataTable? StateDataTable { get; set; }
		public MST_State_SearchModel SearchModel { get; set; }
	}
}
